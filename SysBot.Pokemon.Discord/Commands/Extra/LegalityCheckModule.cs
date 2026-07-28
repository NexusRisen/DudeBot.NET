using Discord;
using Discord.Commands;
using PKHeX.Core;
using System.Threading.Tasks;

namespace SysBot.Pokemon.Discord;

public class LegalityCheckModule : ModuleBase<SocketCommandContext>
{
    [Command("lc"), Alias("check", "validate", "verify")]
    [Summary("Verifies the attachment for legality.")]
    public async Task LegalityCheck()
    {
        foreach (var att in (System.Collections.Generic.IReadOnlyCollection<Attachment>)Context.Message.Attachments)
            await LegalityCheck(att, false).ConfigureAwait(false);
    }

    [Command("lcv"), Alias("verbose")]
    [Summary("Verifies the attachment for legality with a verbose output.")]
    public async Task LegalityCheckVerbose()
    {
        foreach (var att in (System.Collections.Generic.IReadOnlyCollection<Attachment>)Context.Message.Attachments)
            await LegalityCheck(att, true).ConfigureAwait(false);
    }

    private async Task LegalityCheck(IAttachment att, bool verbose)
    {
        var download = await DiscordNetUtil.DownloadPKMAsync(att).ConfigureAwait(false);
        if (!download.Success)
        {
            await ReplyAsync(download.ErrorMessage).ConfigureAwait(false);
            return;
        }

        var pkm = download.Data!;
        var la = new LegalityAnalysis(pkm);
        var embed = new EmbedBuilder()
            .WithTitle(la.Valid ? "✅ Legality Verification Passed" : "❌ Legality Verification Failed")
            .WithDescription($"Legality Analysis Report for `{download.SanitizedFileName}`:")
            .AddField(la.Valid ? "Result: Valid" : "Result: Invalid", $"```\n{la.Report(verbose)}\n```", inline: false)
            .WithColor(la.Valid ? EmbedStyle.Emerald : EmbedStyle.Ruby)
            .WithNexusFooter()
            .Build();

        await ReplyAsync(embed: embed).ConfigureAwait(false);
    }
}
