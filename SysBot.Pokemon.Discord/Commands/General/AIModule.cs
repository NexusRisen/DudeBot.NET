using Discord;
using Discord.Commands;
using System.Threading.Tasks;

namespace SysBot.Pokemon.Discord;

[Summary("Commands related to the AI Chatbot.")]
public class AIModule : ModuleBase<SocketCommandContext>
{
    [Command("ai")]
    [Summary("Shows information about how to use the AI Chatbot.")]
    public async Task AIHelpAsync()
    {
        var botMention = Context.Client.CurrentUser.Mention;
        var prefix = SysCordSettings.Settings.CommandPrefix;
        
        var embed = new EmbedBuilder()
            .WithTitle("🤖 AI Chatbot Assistant")
            .WithDescription("Chat with me to generate competitive Pokémon sets or ask strategy questions!")
            .AddField("💬 How to Chat", $"Mention me in chat with your request:\n> {botMention} Give me a competitive Garchomp set for Gen 9.")
            .AddField("🧠 Conversation Memory", "I remember our recent context so you can ask follow-up questions or adjustments.")
            .AddField("🧹 Commands", $"`{prefix}clearAI` — Resets conversation history for a fresh start.")
            .WithColor(EmbedStyle.Amethyst)
            .WithNexusFooter("Legal Pokémon sets verified automatically")
            .Build();

        await ReplyAsync(embed: embed).ConfigureAwait(false);
    }

    [Command("clearAI")]
    [Summary("Clears your AI conversation history to start a fresh chat.")]
    public async Task ClearAIAsync()
    {
        if (SysCordSettings.AIService == null)
        {
            var prefix = SysCordSettings.Settings.CommandPrefix;
            await ReplyAsync($"AI Chatbot is not enabled. Enable it in the settings and use {prefix}clearAI after it's active.").ConfigureAwait(false);
            return;
        }

        SysCordSettings.AIService.ClearHistory(Context.User.Id);
        await ReplyAsync("Your AI conversation history has been cleared! 🧹").ConfigureAwait(false);
    }
}
