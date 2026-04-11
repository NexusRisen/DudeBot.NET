using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SysBot.Pokemon.WinForms
{
    public class UpdateForm : Form
    {
        private Button buttonDownload = null!;
        private Label labelUpdateInfo = null!;
        private readonly Label labelChangelogTitle = new();
        private RichTextBox textBoxChangelog = null!;
        private ProgressBar progressBarDownload = null!;
        private Label labelProgress = null!;
        private Button buttonCopy = null!;
        private readonly bool isUpdateRequired;
        private readonly bool isUpdateAvailable;
        private readonly string newVersion;

        // Colors based on the app's Dark Theme
        private static readonly Color DarkGrey = Color.FromArgb(30, 30, 30);
        private static readonly Color LightGrey = Color.FromArgb(60, 60, 60);
        private static readonly Color SoftWhite = Color.FromArgb(245, 245, 245);
        private static readonly Color AccentBlue = Color.FromArgb(0, 120, 215);

        public UpdateForm(bool updateRequired, string newVersion, bool updateAvailable)
        {
            isUpdateRequired = updateRequired;
            this.newVersion = newVersion;
            isUpdateAvailable = updateAvailable;
            
            InitializeComponent();
            ThemeManager.ApplyTheme(this, ThemeManager.CurrentTheme);
            if (isUpdateRequired)
            {
                labelUpdateInfo.ForeColor = Color.FromArgb(255, 100, 100); // Re-apply alert color after theme
            }
            Load += async (sender, e) => {
                await FetchAndDisplayChangelog();
                labelChangelogTitle.Focus(); // Move focus away from the textbox to prevent auto-selection
            };
            UpdateFormText();
        }

        private void InitializeComponent()
        {
            labelUpdateInfo = new Label();
            buttonDownload = new Button { Name = "ButtonUpdate" };
            progressBarDownload = new ProgressBar();
            labelProgress = new Label();
            textBoxChangelog = new RichTextBox();
            buttonCopy = new Button();

            SuspendLayout();

            // Form Settings
            ClientSize = new Size(520, 450);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            StartPosition = FormStartPosition.CenterScreen;
            Text = isUpdateAvailable ? $"Update Available ({newVersion})" : "Re-Download Latest Version";

            // labelUpdateInfo
            labelUpdateInfo.AutoSize = false;
            labelUpdateInfo.Location = new Point(15, 15);
            labelUpdateInfo.Size = new Size(490, 50);
            labelUpdateInfo.Font = new Font("Segoe UI", 9.5F);
            labelUpdateInfo.TextAlign = ContentAlignment.MiddleLeft;
            if (isUpdateRequired)
            {
                labelUpdateInfo.Text = "A required update is available. You must update to continue using this application.";
                labelUpdateInfo.ForeColor = Color.FromArgb(255, 100, 100); // Light red for alert
                ControlBox = false;
            }
            else if (isUpdateAvailable)
            {
                labelUpdateInfo.Text = "A new version is available. Please download the latest version.";
            }
            else
            {
                labelUpdateInfo.Text = "You are on the latest version. You can re-download if needed.";
                buttonDownload.Text = "Re-Download Latest Version";
            }

            // labelChangelogTitle
            labelChangelogTitle.AutoSize = true;
            labelChangelogTitle.Location = new Point(15, 75);
            labelChangelogTitle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            labelChangelogTitle.Text = $"Release Notes ({newVersion}):";

            // textBoxChangelog
            textBoxChangelog.ReadOnly = true;
            textBoxChangelog.ScrollBars = RichTextBoxScrollBars.Vertical;
            textBoxChangelog.Location = new Point(15, 100);
            textBoxChangelog.Size = new Size(490, 220);
            textBoxChangelog.BackColor = Color.FromArgb(20, 20, 20);
            textBoxChangelog.ForeColor = Color.Gainsboro;
            textBoxChangelog.BorderStyle = BorderStyle.None;
            textBoxChangelog.Font = new Font("Consolas", 9F);
            // Prevent selection highlighting on click by capturing the Enter event
            textBoxChangelog.Enter += (s, e) => { labelChangelogTitle.Focus(); };

            // buttonCopy
            buttonCopy.Size = new Size(60, 25);
            buttonCopy.Location = new Point(445, 72);
            buttonCopy.FlatStyle = FlatStyle.Flat;
            buttonCopy.FlatAppearance.BorderSize = 0;
            buttonCopy.BackColor = Color.FromArgb(45, 45, 45);
            buttonCopy.ForeColor = Color.Silver;
            buttonCopy.Font = new Font("Segoe UI", 8F);
            buttonCopy.Text = "Copy";
            buttonCopy.Click += (s, e) => {
                if (!string.IsNullOrEmpty(textBoxChangelog.Text))
                {
                    Clipboard.SetText(textBoxChangelog.Text);
                    buttonCopy.Text = "Copied!";
                    Task.Delay(2000).ContinueWith(_ => Invoke(() => buttonCopy.Text = "Copy"));
                }
            };

            // progressBarDownload
            progressBarDownload.Location = new Point(15, 335);
            progressBarDownload.Size = new Size(410, 23);
            progressBarDownload.Visible = false;

            // labelProgress
            labelProgress.AutoSize = true;
            labelProgress.Location = new Point(435, 338);
            labelProgress.Size = new Size(50, 20);
            labelProgress.Text = "0%";
            labelProgress.Visible = false;

            // buttonDownload
            buttonDownload.Size = new Size(180, 40);
            buttonDownload.Location = new Point(170, 385);
            buttonDownload.FlatStyle = FlatStyle.Flat;
            buttonDownload.FlatAppearance.BorderSize = 1;
            buttonDownload.FlatAppearance.BorderColor = LightGrey;
            buttonDownload.BackColor = Color.FromArgb(50, 50, 50);
            buttonDownload.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            if (string.IsNullOrEmpty(buttonDownload.Text))
            {
                buttonDownload.Text = "Download Update";
            }
            buttonDownload.Click += ButtonDownload_Click;

            Controls.Add(labelUpdateInfo);
            Controls.Add(labelChangelogTitle);
            Controls.Add(textBoxChangelog);
            Controls.Add(buttonCopy);
            Controls.Add(progressBarDownload);
            Controls.Add(labelProgress);
            Controls.Add(buttonDownload);

            ResumeLayout(false);
            PerformLayout();
        }

        private void UpdateFormText()
        {
            Text = isUpdateAvailable ? $"Update Available ({newVersion})" : "Re-Download Latest Version";
        }

        private async Task FetchAndDisplayChangelog()
        {
            textBoxChangelog.Text = "Fetching changelog...";
            textBoxChangelog.Text = await UpdateChecker.FetchChangelogAsync();
        }

        private async void ButtonDownload_Click(object? sender, EventArgs? e)
        {
            buttonDownload.Enabled = false;
            buttonDownload.Text = "Initializing...";
            progressBarDownload.Visible = true;
            labelProgress.Visible = true;
            progressBarDownload.Value = 0;

            try
            {
                string? downloadUrl = await UpdateChecker.FetchDownloadUrlAsync();
                if (!string.IsNullOrWhiteSpace(downloadUrl))
                {
                    IProgress<int> progress = new Progress<int>(v => {
                        progressBarDownload.Value = v;
                        labelProgress.Text = $"{v}%";
                    });

                    string downloadedFilePath = await StartDownloadProcessAsync(downloadUrl, progress);
                    if (!string.IsNullOrEmpty(downloadedFilePath))
                    {
                        buttonDownload.Text = "Installing...";
                        InstallUpdate(downloadedFilePath);
                    }
                }
                else
                {
                    MessageBox.Show("Failed to fetch the download URL.", "Download Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    ResetUI();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update failed: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetUI();
            }
        }

        private void ResetUI()
        {
            buttonDownload.Enabled = true;
            buttonDownload.Text = isUpdateAvailable ? "Download Update" : "Re-Download Latest Version";
            progressBarDownload.Visible = false;
            labelProgress.Visible = false;
        }

        private static async Task<string> StartDownloadProcessAsync(string downloadUrl, IProgress<int> progress)
        {
            Main.IsUpdating = true;
            string tempPath = Path.Combine(Path.GetTempPath(), $"dudebot_{Guid.NewGuid()}.exe");

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "DudeBot");
                
                using (var response = await client.GetAsync(downloadUrl, HttpCompletionOption.ResponseHeadersRead))
                {
                    response.EnsureSuccessStatusCode();
                    var totalBytes = response.Content.Headers.ContentLength ?? -1L;
                    var buffer = new byte[8192];
                    var totalRead = 0L;

                    using (var contentStream = await response.Content.ReadAsStreamAsync())
                    using (var fileStream = new FileStream(tempPath, FileMode.Create, FileAccess.Write, FileShare.None, 8192, true))
                    {
                        int bytesRead;
                        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) != 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            totalRead += bytesRead;

                            if (totalBytes != -1)
                            {
                                int percentage = (int)((totalRead * 100) / totalBytes);
                                progress.Report(percentage);
                            }
                        }
                    }
                }
            }

            return tempPath;
        }

        private void InstallUpdate(string downloadedFilePath)
        {
            try
            {
                string currentExePath = Application.ExecutablePath;
                string applicationDirectory = Path.GetDirectoryName(currentExePath) ?? "";
                string targetExePath = Path.Combine(applicationDirectory, "dudebot.exe");
                string backupPath = Path.Combine(applicationDirectory, "dudebot.exe.backup");

                string batchPath = Path.Combine(Path.GetTempPath(), "UpdateDudeBot.bat");
                
                string cleanupCommand = !currentExePath.Equals(targetExePath, StringComparison.OrdinalIgnoreCase) 
                    ? $"del \"{currentExePath}\"" 
                    : "";

                string batchContent = @$"
@echo off
timeout /t 2 /nobreak >nul
echo Updating DudeBot...

if exist ""{targetExePath}"" (
    if exist ""{backupPath}"" (
        del ""{backupPath}""
    )
    move ""{targetExePath}"" ""{backupPath}""
)

move ""{downloadedFilePath}"" ""{targetExePath}""
{cleanupCommand}

start """" ""{targetExePath}""
del ""%~f0""
";

                File.WriteAllText(batchPath, batchContent);

                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = batchPath,
                    CreateNoWindow = true,
                    UseShellExecute = true,
                    WindowStyle = ProcessWindowStyle.Hidden
                };

                Process.Start(startInfo);
                Application.Exit();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to install update: {ex.Message}", "Update Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                ResetUI();
            }
        }
    }
}
