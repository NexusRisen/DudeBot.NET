using Discord;
using SysBot.Base;
using SysBot.Pokemon.Helpers;
using System;

namespace SysBot.Pokemon.Discord;

/// <summary>
/// Defines standard color palettes and formatting helpers for Discord embeds.
/// </summary>
public static class EmbedStyle
{
    // Modern Discord-aligned Color Palette
    public static readonly Color Blurple = new(0x58, 0x65, 0xF2);      // Primary / Info / Standard
    public static readonly Color Emerald = new(0x2E, 0xCC, 0x71);      // Success / Trade Completed
    public static readonly Color Ruby = new(0xE7, 0x4C, 0x3C);         // Error / Canceled / Warning
    public static readonly Color Amber = new(0xF1, 0xC4, 0x0F);        // Warning / Link Code / Up Next
    public static readonly Color Amethyst = new(0x9B, 0x59, 0xB6);     // Special / AI / Mystery Egg
    public static readonly Color Teal = new(0x1A, 0xBC, 0x9C);         // Stats / Diagnostics
    public static readonly Color DarkSlate = new(0x2C, 0x2F, 0x33);    // Subtle / Neutral Dark

    /// <summary>
    /// Applies standard NexusBot footer formatting to an embed.
    /// </summary>
    public static EmbedBuilder WithNexusFooter(this EmbedBuilder builder, string? extraText = null)
    {
        string versionText = $"NexusBot.NET v{NexusBot.Version}";
        string footerText = string.IsNullOrWhiteSpace(extraText)
            ? versionText
            : $"{extraText} • {versionText}";

        return builder
            .WithFooter(footerText)
            .WithTimestamp(DateTimeOffset.UtcNow);
    }

    /// <summary>
    /// Applies standard NexusBot author header to an embed.
    /// </summary>
    public static EmbedBuilder WithNexusAuthor(this EmbedBuilder builder, string title, string? iconUrl = null, string? url = null)
    {
        return builder.WithAuthor(new EmbedAuthorBuilder
        {
            Name = title,
            IconUrl = iconUrl,
            Url = url
        });
    }

    /// <summary>
    /// Formats a link trade code into a highlighted mono-spaced display block.
    /// </summary>
    public static string FormatLinkCode(int code)
    {
        return $"` {code:0000 0000} `";
    }

    /// <summary>
    /// Formats text into a clean blockquote.
    /// </summary>
    public static string FormatQuote(string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return string.Empty;
        var lines = text.Split('\n');
        return string.Join("\n", Array.ConvertAll(lines, l => $"> {l}"));
    }
}
