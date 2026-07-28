using Discord;
using Discord.Net;
using PKHeX.Core;
using SysBot.Base;
using SysBot.Pokemon.Helpers;
using System;
using System.Threading.Tasks;

namespace SysBot.Pokemon.Discord;

public static class EmbedHelper
{
    public static async Task SendNotificationEmbedAsync(IUser user, string message)
    {
        try
        {
            var dm = await SysCordSettings.Manager.GetOrCreateDMAsync(user).ConfigureAwait(false);
            if (dm == null)
            {
                LogUtil.LogError($"Could not create DM channel for user {user.Username} ({user.Id}). Skipping notification.", "SendNotificationEmbedAsync");
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle("⚠️ Notice")
                .WithDescription(message)
                .WithThumbnailUrl(SysBot.Pokemon.Helpers.AssetManager.GetAssetUrl("Assets/Bot/DM/dm-legalityerror.gif"))
                .WithColor(EmbedStyle.Amber)
                .WithNexusFooter("Notification")
                .Build();

            await dm.SendMessageAsync(embed: embed).ConfigureAwait(false);
        }
        catch (ObjectDisposedException)
        {
            LogUtil.LogError("Discord client disposed when sending notification embed.", "SendNotificationEmbedAsync");
        }
        catch (HttpException ex) when (ex.DiscordCode.HasValue && ex.DiscordCode.Value == (DiscordErrorCode)40003)
        {
            LogUtil.LogError($"Opening DMs too fast! User: {user.Username} ({user.Id})", "SendNotificationEmbedAsync");
            SysCordSettings.Manager.ClearDMChannelCache(user.Id);
        }
        catch (Exception ex)
        {
            LogUtil.LogError($"Error sending notification embed: {ex.Message}", "SendNotificationEmbedAsync");
        }
    }

    public static async Task SendTradeCanceledEmbedAsync(IUser user, string reason)
    {
        try
        {
            var dm = await SysCordSettings.Manager.GetOrCreateDMAsync(user).ConfigureAwait(false);
            if (dm == null)
            {
                LogUtil.LogError($"Could not create DM channel for user {user.Username} ({user.Id}). Skipping trade canceled message.", "SendTradeCanceledEmbedAsync");
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle("❌ Trade Canceled")
                .AddField("Reason", reason, inline: false)
                .WithThumbnailUrl(SysBot.Pokemon.Helpers.AssetManager.GetAssetUrl("Assets/Bot/DM/dm-uhoherror.gif"))
                .WithColor(EmbedStyle.Ruby)
                .WithNexusFooter("Please resolve any issues and queue again")
                .Build();

            await dm.SendMessageAsync(embed: embed).ConfigureAwait(false);
        }
        catch (ObjectDisposedException)
        {
            LogUtil.LogError("Discord client disposed when sending trade canceled embed.", "SendTradeCanceledEmbedAsync");
        }
        catch (HttpException ex) when (ex.DiscordCode.HasValue && ex.DiscordCode.Value == (DiscordErrorCode)40003)
        {
            LogUtil.LogError($"Opening DMs too fast! User: {user.Username} ({user.Id})", "SendTradeCanceledEmbedAsync");
            SysCordSettings.Manager.ClearDMChannelCache(user.Id);
        }
        catch (Exception ex)
        {
            LogUtil.LogError($"Error sending trade canceled embed: {ex.Message}", "SendTradeCanceledEmbedAsync");
        }
    }

    public static async Task SendTradeCodeEmbedAsync(IUser user, int code)
    {
        try
        {
            var dm = await SysCordSettings.Manager.GetOrCreateDMAsync(user).ConfigureAwait(false);
            if (dm == null)
            {
                LogUtil.LogError($"Could not create DM channel for user {user.Username} ({user.Id}). Skipping trade code message.", "SendTradeCodeEmbedAsync");
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle("🔗 Link Trade Code")
                .AddField("Code", EmbedStyle.FormatLinkCode(code), inline: false)
                .WithThumbnailUrl(SysBot.Pokemon.Helpers.AssetManager.GetAssetUrl("Assets/Bot/DM/dm-tradecode.gif"))
                .WithColor(EmbedStyle.Amber)
                .WithNexusFooter("I will notify you when it is time to search")
                .Build();

            await dm.SendMessageAsync(embed: embed).ConfigureAwait(false);
        }
        catch (ObjectDisposedException)
        {
            LogUtil.LogError("Discord client disposed when sending trade code embed.", "SendTradeCodeEmbedAsync");
        }
        catch (HttpException ex) when (ex.DiscordCode.HasValue && ex.DiscordCode.Value == (DiscordErrorCode)40003)
        {
            LogUtil.LogError($"Opening DMs too fast! User: {user.Username} ({user.Id})", "SendTradeCodeEmbedAsync");
            SysCordSettings.Manager.ClearDMChannelCache(user.Id);
        }
        catch (Exception ex)
        {
            LogUtil.LogError($"Error sending trade code embed: {ex.Message}", "SendTradeCodeEmbedAsync");
        }
    }

    public static async Task SendTradeFinishedEmbedAsync<T>(IUser user, string message, T pk, bool isMysteryEgg)
        where T : PKM, new()
    {
        try
        {
            string thumbnailUrl = isMysteryEgg 
                ? SysBot.Pokemon.Helpers.AssetManager.GetAssetUrl("Assets/Eggs/mysteryegg3.png") 
                : TradeExtensions<T>.PokeImg(pk, false, true, null);

            var embed = new EmbedBuilder()
                .WithTitle("✨ Trade Completed")
                .AddField("Summary", message, inline: false)
                .WithThumbnailUrl(thumbnailUrl)
                .WithColor(EmbedStyle.Emerald)
                .WithNexusFooter("Thank you for using NexusBot!")
                .Build();

            await user.SendMessageAsync(embed: embed).ConfigureAwait(false);
        }
        catch (ObjectDisposedException)
        {
            LogUtil.LogError("Discord client disposed when sending trade finished embed.", "SendTradeFinishedEmbedAsync");
        }
        catch (Exception ex)
        {
            LogUtil.LogError($"Error sending trade finished embed: {ex.Message}", "SendTradeFinishedEmbedAsync");
        }
    }

    public static async Task SendTradeInitializingEmbedAsync(IUser user, string speciesName, int code, bool isMysteryEgg, string? message = null, IMessageChannel? fallbackChannel = null)
    {
        try
        {
            var dm = await SysCordSettings.Manager.GetOrCreateDMAsync(user).ConfigureAwait(false);
            if (dm == null)
            {
                LogUtil.LogError($"Could not create DM channel for user {user.Username} ({user.Id}). Skipping trade initializing message.", "SendTradeInitializingEmbedAsync");
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle("🔄 Initializing Trade")
                .AddField("Pokémon", isMysteryEgg ? "Mystery Egg" : speciesName, inline: true)
                .AddField("Link Code", EmbedStyle.FormatLinkCode(code), inline: true)
                .WithThumbnailUrl(SysBot.Pokemon.Helpers.AssetManager.GetAssetUrl("Assets/Bot/DM/dm-initializingbot.gif"))
                .WithColor(EmbedStyle.Blurple)
                .WithNexusFooter("Please wait for search signal");

            if (!string.IsNullOrEmpty(message))
            {
                embed.AddField("Notice", message, inline: false);
            }

            await dm.SendMessageAsync(embed: embed.Build()).ConfigureAwait(false);
        }
        catch (ObjectDisposedException)
        {
            LogUtil.LogError("Discord client disposed when sending trade initializing embed.", "SendTradeInitializingEmbedAsync");
        }
        catch (HttpException ex) when (ex.DiscordCode.HasValue && ex.DiscordCode.Value == (DiscordErrorCode)40003)
        {
            LogUtil.LogError($"Opening DMs too fast! User: {user.Username} ({user.Id})", "SendTradeInitializingEmbedAsync");
            SysCordSettings.Manager.ClearDMChannelCache(user.Id);
        }
        catch (HttpException ex) when (ex.DiscordCode.HasValue && ex.DiscordCode.Value == DiscordErrorCode.CannotSendMessageToUser)
        {
            LogUtil.LogError($"Cannot DM {user.Username}. DMs are likely disabled.", "SendTradeInitializingEmbedAsync");
            if (fallbackChannel != null)
                await fallbackChannel.SendMessageAsync($"{user.Mention}, I couldn't send you a DM with your trade info. Please enable DMs from server members!").ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            LogUtil.LogError($"Error sending trade initializing embed: {ex.Message}", "SendTradeInitializingEmbedAsync");
        }
    }

    public static async Task SendTradeSearchingEmbedAsync(IUser user, string trainerName, string inGameName, string? message = null)
    {
        try
        {
            var dm = await SysCordSettings.Manager.GetOrCreateDMAsync(user).ConfigureAwait(false);
            if (dm == null)
            {
                LogUtil.LogError($"Could not create DM channel for user {user.Username} ({user.Id}). Skipping trade searching message.", "SendTradeSearchingEmbedAsync");
                return;
            }

            var embed = new EmbedBuilder()
                .WithTitle("🔍 Searching for Trade")
                .AddField("Waiting For", trainerName, inline: true)
                .AddField("Bot IGN", inGameName, inline: true)
                .WithThumbnailUrl(SysBot.Pokemon.Helpers.AssetManager.GetAssetUrl("Assets/Bot/DM/dm-nowsearching.gif"))
                .WithColor(EmbedStyle.Emerald)
                .WithNexusFooter("Begin searching in-game with your code now!");

            if (!string.IsNullOrEmpty(message))
            {
                embed.AddField("Notice", message, inline: false);
            }

            await dm.SendMessageAsync(embed: embed.Build()).ConfigureAwait(false);
        }
        catch (ObjectDisposedException)
        {
            LogUtil.LogError("Discord client disposed when sending trade searching embed.", "SendTradeSearchingEmbedAsync");
        }
        catch (HttpException ex) when (ex.DiscordCode.HasValue && ex.DiscordCode.Value == (DiscordErrorCode)40003)
        {
            LogUtil.LogError($"Opening DMs too fast! User: {user.Username} ({user.Id})", "SendTradeSearchingEmbedAsync");
            SysCordSettings.Manager.ClearDMChannelCache(user.Id);
        }
        catch (Exception ex)
        {
            LogUtil.LogError($"Error sending trade searching embed: {ex.Message}", "SendTradeSearchingEmbedAsync");
        }
    }
}
