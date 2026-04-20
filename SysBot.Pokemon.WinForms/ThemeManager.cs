using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;

namespace SysBot.Pokemon.WinForms;

public static class ThemeManager
{
    public static readonly ThemePalette DarkTheme = new()
    {
        Name = "Dark Theme",
        BackColor = Color.FromArgb(40, 44, 52),
        ForeColor = Color.FromArgb(220, 223, 228),
        AccentColor = Color.FromArgb(82, 139, 255),
        TabBackColor = Color.FromArgb(33, 37, 43),
        TabPageBackColor = Color.FromArgb(40, 44, 52),
        TabForeColor = Color.FromArgb(220, 223, 228),
        InputBackColor = Color.FromArgb(44, 49, 58),
        InputForeColor = Color.White,
        InputBorderColor = Color.FromArgb(24, 26, 31),
        ButtonBackColor = Color.FromArgb(44, 49, 58),
        ButtonForeColor = Color.White,
        ButtonBorderColor = Color.FromArgb(24, 26, 31),
        StartButtonBackColor = Color.FromArgb(152, 195, 121),
        StartButtonForeColor = Color.FromArgb(33, 37, 43),
        StopButtonBackColor = Color.FromArgb(224, 108, 117),
        StopButtonForeColor = Color.FromArgb(33, 37, 43),
        RestartButtonBackColor = Color.FromArgb(97, 175, 239),
        RestartButtonForeColor = Color.FromArgb(33, 37, 43),
        PropertyGridBackColor = Color.FromArgb(40, 44, 52),
        PropertyGridLineColor = Color.FromArgb(33, 37, 43),
        PropertyGridCategoryForeColor = Color.FromArgb(97, 175, 239),
        PropertyGridHelpBackColor = Color.FromArgb(33, 37, 43),
        PropertyGridHelpForeColor = Color.FromArgb(220, 223, 228),
        LogBackColor = Color.FromArgb(33, 37, 43),
        LogForeColor = Color.FromArgb(171, 178, 191),
        SidebarBackColor = Color.FromArgb(33, 37, 43),
        SidebarButtonForeColor = Color.FromArgb(171, 178, 191),
        SidebarButtonActiveColor = Color.FromArgb(40, 44, 52),
        HeaderBackColor = Color.FromArgb(40, 44, 52),
        HeaderForeColor = Color.White,
        BotControllerHoverColor = Color.FromArgb(44, 49, 58)
    };

    public static readonly ThemePalette ModernTheme = new()
    {
        Name = "Modern Theme",
        BackColor = Color.FromArgb(24, 24, 24),
        ForeColor = Color.FromArgb(238, 238, 238),
        AccentColor = Color.FromArgb(0, 120, 215),
        TabBackColor = Color.FromArgb(32, 32, 32),
        TabPageBackColor = Color.FromArgb(24, 24, 24),
        TabForeColor = Color.FromArgb(238, 238, 238),
        InputBackColor = Color.FromArgb(40, 40, 40),
        InputForeColor = Color.White,
        InputBorderColor = Color.FromArgb(60, 60, 60),
        ButtonBackColor = Color.FromArgb(45, 45, 45),
        ButtonForeColor = Color.White,
        ButtonBorderColor = Color.FromArgb(60, 60, 60),
        StartButtonBackColor = Color.FromArgb(0, 120, 215),
        StartButtonForeColor = Color.White,
        StopButtonBackColor = Color.FromArgb(196, 43, 28),
        StopButtonForeColor = Color.White,
        RestartButtonBackColor = Color.FromArgb(0, 153, 188),
        RestartButtonForeColor = Color.White,
        PropertyGridBackColor = Color.FromArgb(24, 24, 24),
        PropertyGridLineColor = Color.FromArgb(40, 40, 40),
        PropertyGridCategoryForeColor = Color.FromArgb(0, 120, 215),
        PropertyGridHelpBackColor = Color.FromArgb(32, 32, 32),
        PropertyGridHelpForeColor = Color.FromArgb(238, 238, 238),
        LogBackColor = Color.Black,
        LogForeColor = Color.FromArgb(204, 204, 204),
        SidebarBackColor = Color.FromArgb(32, 32, 32),
        SidebarButtonForeColor = Color.Gainsboro,
        SidebarButtonActiveColor = Color.FromArgb(0, 120, 215),
        HeaderBackColor = Color.FromArgb(32, 32, 32),
        HeaderForeColor = Color.White,
        BotControllerHoverColor = Color.FromArgb(45, 45, 45)
    };

    public static readonly ThemePalette PokeballTheme = new()
    {
        Name = "Poké Ball Theme",
        BackColor = Color.FromArgb(46, 49, 54),
        ForeColor = Color.FromArgb(230, 230, 230),
        AccentColor = Color.FromArgb(164, 10, 24),
        TabBackColor = Color.FromArgb(164, 10, 24),
        TabPageBackColor = Color.FromArgb(46, 49, 54),
        TabForeColor = Color.FromArgb(230, 230, 230),
        InputBackColor = Color.FromArgb(164, 10, 24),
        InputForeColor = Color.FromArgb(230, 230, 230),
        InputBorderColor = Color.FromArgb(46, 49, 54),
        ButtonBackColor = Color.FromArgb(206, 12, 30),
        ButtonForeColor = Color.FromArgb(230, 230, 230),
        ButtonBorderColor = Color.FromArgb(164, 10, 24),
        StartButtonBackColor = Color.FromArgb(206, 12, 30),
        StartButtonForeColor = Color.FromArgb(230, 230, 230),
        StopButtonBackColor = Color.FromArgb(206, 12, 30),
        StopButtonForeColor = Color.FromArgb(230, 230, 230),
        RestartButtonBackColor = Color.FromArgb(206, 12, 30),
        RestartButtonForeColor = Color.FromArgb(230, 230, 230),
        PropertyGridBackColor = Color.FromArgb(46, 49, 54),
        PropertyGridLineColor = Color.FromArgb(164, 10, 24),
        PropertyGridCategoryForeColor = Color.FromArgb(230, 230, 230),
        PropertyGridHelpBackColor = Color.FromArgb(46, 49, 54),
        PropertyGridHelpForeColor = Color.FromArgb(230, 230, 230),
        LogBackColor = Color.FromArgb(46, 49, 54),
        LogForeColor = Color.FromArgb(230, 230, 230),
        SidebarBackColor = Color.FromArgb(164, 10, 24),
        SidebarButtonForeColor = Color.White,
        SidebarButtonActiveColor = Color.FromArgb(206, 12, 30),
        HeaderBackColor = Color.FromArgb(164, 10, 24),
        HeaderForeColor = Color.White,
        BotControllerHoverColor = Color.FromArgb(164, 10, 24)
    };

    public static readonly ThemePalette PitchBlackTheme = new()
    {
        Name = "Pitch Black Theme",
        BackColor = Color.Black,
        ForeColor = Color.White,
        AccentColor = Color.Black,
        TabBackColor = Color.Black,
        TabPageBackColor = Color.Black,
        TabForeColor = Color.White,
        InputBackColor = Color.Black,
        InputForeColor = Color.White,
        InputBorderColor = Color.FromArgb(51, 51, 51),
        ButtonBackColor = Color.Black,
        ButtonForeColor = Color.White,
        ButtonBorderColor = Color.FromArgb(51, 51, 51),
        StartButtonBackColor = Color.Black,
        StartButtonForeColor = Color.White,
        StopButtonBackColor = Color.Black,
        StopButtonForeColor = Color.White,
        RestartButtonBackColor = Color.Black,
        RestartButtonForeColor = Color.White,
        PropertyGridBackColor = Color.Black,
        PropertyGridLineColor = Color.FromArgb(51, 51, 51),
        PropertyGridCategoryForeColor = Color.White,
        PropertyGridHelpBackColor = Color.Black,
        PropertyGridHelpForeColor = Color.White,
        LogBackColor = Color.Black,
        LogForeColor = Color.White,
        SidebarBackColor = Color.Black,
        SidebarButtonForeColor = Color.Gray,
        SidebarButtonActiveColor = Color.White,
        HeaderBackColor = Color.Black,
        HeaderForeColor = Color.White,
        BotControllerHoverColor = Color.FromArgb(51, 51, 51)
    };

    public static IEnumerable<ThemePalette> AllThemes { get; } = new ThemePalette[]
    {
        DarkTheme,
        ModernTheme,
        PokeballTheme,
        PitchBlackTheme
    };

    private static ThemePalette _currentTheme = DarkTheme;
    public static ThemePalette CurrentTheme 
    { 
        get => _currentTheme ?? DarkTheme; 
        private set => _currentTheme = value; 
    }

    public static void ApplyTheme(Form form, string themeName)
    {
        var theme = AllThemes.FirstOrDefault(t => t.Name == themeName) ?? DarkTheme;
        CurrentTheme = theme;
        ApplyTheme(form, CurrentTheme);
    }

    public static void ApplyTheme(Control control, ThemePalette palette)
    {
        if (control is Form form)
        {
            form.BackColor = palette.BackColor;
            form.ForeColor = palette.ForeColor;
            form.Font = new Font("Segoe UI", 9F);
        }

        foreach (Control c in control.Controls)
        {
            ApplyToControl(c, palette);
        }
    }

    private static void ApplyToControl(Control c, ThemePalette palette)
    {
        if (c.Name == "P_Sidebar" || c.Name == "P_LogoArea" || c.Name == "P_Bottom")
        {
            c.BackColor = palette.SidebarBackColor;
        }
        else if (c.Name == "P_Header")
        {
            c.BackColor = palette.HeaderBackColor;
            c.ForeColor = palette.HeaderForeColor;
        }
        else if (c.Name.StartsWith("B_Nav") || c.Name == "B_Credits" || c.Name == "B_HideTray")
        {
            c.BackColor = palette.SidebarBackColor;
            c.ForeColor = palette.SidebarButtonForeColor;
            if (c is Button btn)
            {
                btn.FlatAppearance.MouseOverBackColor = palette.SidebarButtonActiveColor;
                btn.FlatAppearance.MouseDownBackColor = palette.SidebarButtonActiveColor;
            }
        }
        else if (c is not Label && c is not LinkLabel && c is not PictureBox)
        {
            c.BackColor = palette.BackColor;
        }
        else if (c is not PictureBox)
        {
            c.BackColor = Color.Transparent;
        }
        
        c.ForeColor = palette.ForeColor;

        if (c.ContextMenuStrip != null)
        {
            ApplyToContextMenu(c.ContextMenuStrip, palette);
        }

        switch (c)
        {
            case TabControl tc:
                tc.BackColor = palette.TabBackColor;
                break;
            case TabPage tp:
                tp.BackColor = palette.TabPageBackColor;
                tp.ForeColor = palette.TabForeColor;
                break;
            case TextBox tb:
                tb.BackColor = palette.InputBackColor;
                tb.ForeColor = palette.InputForeColor;
                tb.BorderStyle = BorderStyle.FixedSingle;
                break;
            case ComboBox cb:
                cb.BackColor = palette.InputBackColor;
                cb.ForeColor = palette.InputForeColor;
                cb.FlatStyle = FlatStyle.Flat;
                cb.DropDownStyle = ComboBoxStyle.DropDownList;
                break;
            case NumericUpDown nud:
                nud.BackColor = palette.InputBackColor;
                nud.ForeColor = palette.InputForeColor;
                nud.BorderStyle = BorderStyle.FixedSingle;
                break;
            case Button btn:
                if (!btn.Name.StartsWith("B_Nav"))
                    ApplyButtonTheme(btn, palette);
                break;
            case PropertyGrid pg:
                pg.BackColor = palette.PropertyGridBackColor;
                pg.ViewBackColor = palette.PropertyGridBackColor;
                pg.ViewForeColor = palette.ForeColor;
                pg.LineColor = palette.PropertyGridLineColor;
                pg.CategoryForeColor = palette.PropertyGridCategoryForeColor;
                pg.CategorySplitterColor = palette.PropertyGridLineColor;
                pg.HelpBackColor = palette.PropertyGridHelpBackColor;
                pg.HelpForeColor = palette.PropertyGridHelpForeColor;
                break;
            case RichTextBox rtb:
                rtb.BackColor = palette.LogBackColor;
                rtb.ForeColor = palette.LogForeColor;
                rtb.BorderStyle = BorderStyle.None;
                break;
            case LinkLabel ll:
                ll.LinkColor = palette.SidebarButtonForeColor;
                ll.ActiveLinkColor = palette.SidebarButtonActiveColor;
                ll.VisitedLinkColor = palette.SidebarButtonForeColor;
                break;
            case PictureBox pb:
                if (pb.Name == "PB_Logo" || pb.Name == "PB_LogoSidebar")
                {
                    pb.BackColor = Color.Transparent;
                }
                break;
        }

        foreach (Control child in c.Controls)
        {
            ApplyToControl(child, palette);
        }
    }

    private static void ApplyToContextMenu(ContextMenuStrip cms, ThemePalette palette)
    {
        cms.BackColor = palette.BackColor;
        cms.ForeColor = palette.ForeColor;
        cms.RenderMode = ToolStripRenderMode.Professional;
        foreach (ToolStripItem item in cms.Items)
        {
            ApplyToToolStripItem(item, palette);
        }
    }

    private static void ApplyToToolStripItem(ToolStripItem item, ThemePalette palette)
    {
        item.BackColor = palette.BackColor;
        item.ForeColor = palette.ForeColor;
        if (item is ToolStripDropDownItem dd)
        {
            foreach (ToolStripItem subItem in dd.DropDownItems)
            {
                ApplyToToolStripItem(subItem, palette);
            }
        }
    }

    private static void ApplyButtonTheme(Button btn, ThemePalette palette)
    {
        btn.FlatStyle = FlatStyle.Flat;
        btn.FlatAppearance.BorderColor = palette.ButtonBorderColor;
        btn.FlatAppearance.BorderSize = 1;
        btn.FlatAppearance.MouseOverBackColor = palette.AccentColor;

        if (btn.Name.Contains("Start", StringComparison.OrdinalIgnoreCase))
        {
            btn.BackColor = palette.StartButtonBackColor;
            btn.ForeColor = palette.StartButtonForeColor;
        }
        else if (btn.Name.Contains("Stop", StringComparison.OrdinalIgnoreCase))
        {
            btn.BackColor = palette.StopButtonBackColor;
            btn.ForeColor = palette.StopButtonForeColor;
        }
        else if (btn.Name.Contains("Restart", StringComparison.OrdinalIgnoreCase))
        {
            btn.BackColor = palette.RestartButtonBackColor;
            btn.ForeColor = palette.RestartButtonForeColor;
        }
        else
        {
            btn.BackColor = palette.ButtonBackColor;
            btn.ForeColor = palette.ButtonForeColor;
        }
    }
}
