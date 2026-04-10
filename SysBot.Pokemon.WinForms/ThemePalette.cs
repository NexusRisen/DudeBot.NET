using System.Drawing;

namespace SysBot.Pokemon.WinForms;

public class ThemePalette
{
    public string Name { get; set; } = string.Empty;

    // General
    public Color BackColor { get; set; }
    public Color ForeColor { get; set; }
    public Color AccentColor { get; set; }

    // Tabs
    public Color TabBackColor { get; set; }
    public Color TabPageBackColor { get; set; }
    public Color TabForeColor { get; set; }

    // Inputs (TextBox, ComboBox, NumericUpDown)
    public Color InputBackColor { get; set; }
    public Color InputForeColor { get; set; }
    public Color InputBorderColor { get; set; }

    // Buttons
    public Color ButtonBackColor { get; set; }
    public Color ButtonForeColor { get; set; }
    public Color ButtonBorderColor { get; set; }

    // Specialized Buttons
    public Color StartButtonBackColor { get; set; }
    public Color StartButtonForeColor { get; set; }
    public Color StopButtonBackColor { get; set; }
    public Color StopButtonForeColor { get; set; }
    public Color RestartButtonBackColor { get; set; }
    public Color RestartButtonForeColor { get; set; }

    // PropertyGrid
    public Color PropertyGridBackColor { get; set; }
    public Color PropertyGridLineColor { get; set; }
    public Color PropertyGridCategoryForeColor { get; set; }
    public Color PropertyGridHelpBackColor { get; set; }
    public Color PropertyGridHelpForeColor { get; set; }

    // Logs
    public Color LogBackColor { get; set; }
    public Color LogForeColor { get; set; }

    // Sidebar & Header
    public Color SidebarBackColor { get; set; }
    public Color SidebarButtonForeColor { get; set; }
    public Color SidebarButtonActiveColor { get; set; }
    public Color HeaderBackColor { get; set; }
    public Color HeaderForeColor { get; set; }

    // Bot Controller
    public Color BotControllerHoverColor { get; set; }
}
