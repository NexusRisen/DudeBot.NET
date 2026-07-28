using Discord;
using Discord.Commands;
using Discord.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SysBot.Pokemon.Discord
{
    public class HelpModule(CommandService commandService) : ModuleBase<SocketCommandContext>
    {
        private readonly CommandService _commandService = commandService;

        private class CommandHelpInfo
        {
            public required string Name { get; set; }
            public required string Summary { get; set; }
            public List<string> Aliases { get; set; } = [];
        }

        private static string GetCategoryName(string rawModuleName)
        {
            var name = rawModuleName.Split('`')[0].Replace("Module", "");
            return name switch
            {
                "Trade" or "Clone" or "Dump" or "SpecialRequest" or "Mystery" or "Pokepaste" => "🔄 Trading & Distribution",
                "Queue" or "SeedCheck" or "Info" or "Ping" or "Report" or "Hello" or "AI" or "PokemonCommand" or "Pokemon" => "📊 Queue & Info",
                "Bot" or "TradeStart" or "Hub" or "Log" or "Pool" or "Recovery" or "Echo" or "RemoteControl" or "BotAvatar" => "🤖 Bot Management",
                "Sudo" or "Owner" => "🛡️ Sudo & Admin",
                "LegalityCheck" or "Legalizer" or "BatchEditing" or "Joke" => "🎮 Extras & Utilities",
                _ => "📁 General Commands"
            };
        }

        private static readonly string[] CategoryOrder =
        [
            "🔄 Trading & Distribution",
            "📊 Queue & Info",
            "🤖 Bot Management",
            "🛡️ Sudo & Admin",
            "🎮 Extras & Utilities",
            "📁 General Commands"
        ];

        [Command("help")]
        [Summary("Shows the available commands.")]
        public async Task HelpAsync(int page = 1)
        {
            var mgr = SysCordSettings.Manager;
            var app = await Context.Client.GetApplicationInfoAsync().ConfigureAwait(false);
            var owner = app.Owner.Id;
            var uid = Context.User.Id;

            var categorizedCommands = new Dictionary<string, Dictionary<string, CommandHelpInfo>>();

            foreach (var module in _commandService.Modules)
            {
                var category = GetCategoryName(module.Name);
                if (!categorizedCommands.TryGetValue(category, out var cmdDict))
                {
                    cmdDict = new Dictionary<string, CommandHelpInfo>(StringComparer.OrdinalIgnoreCase);
                    categorizedCommands[category] = cmdDict;
                }

                foreach (var command in module.Commands)
                {
                    var preconditionResult = await command.CheckPreconditionsAsync(Context).ConfigureAwait(false);
                    if (!preconditionResult.IsSuccess)
                        continue;

                    if (command.Attributes.Any(a => a is RequireOwnerAttribute) && owner != uid)
                        continue;
                    if (command.Attributes.Any(a => a is RequireSudoAttribute) && !mgr.CanUseSudo(uid))
                        continue;

                    var cmdName = command.Name;
                    var summary = command.Summary ?? "No description available.";
                    var aliases = command.Aliases
                        .Where(a => !a.Equals(cmdName, StringComparison.OrdinalIgnoreCase))
                        .Distinct(StringComparer.OrdinalIgnoreCase)
                        .ToList();

                    if (!cmdDict.TryGetValue(cmdName, out var existing))
                    {
                        cmdDict[cmdName] = new CommandHelpInfo
                        {
                            Name = cmdName,
                            Summary = summary,
                            Aliases = aliases
                        };
                    }
                    else
                    {
                        if (existing.Summary == "No description available." && summary != "No description available.")
                            existing.Summary = summary;

                        foreach (var alias in aliases)
                        {
                            if (!existing.Aliases.Contains(alias, StringComparer.OrdinalIgnoreCase))
                                existing.Aliases.Add(alias);
                        }
                    }
                }
            }

            var pages = new List<string>();
            var currentPage = new StringBuilder();

            foreach (var category in CategoryOrder)
            {
                if (!categorizedCommands.TryGetValue(category, out var commands) || commands.Count == 0)
                    continue;

                var blockBuilder = new StringBuilder();
                blockBuilder.AppendLine($"**{category}**");

                foreach (var cmd in commands.Values.OrderBy(c => c.Name))
                {
                    var aliasText = cmd.Aliases.Count > 0
                        ? $" *(aliases: {string.Join(", ", cmd.Aliases.Select(a => $"`{a}`"))})*"
                        : "";
                    blockBuilder.AppendLine($"• `{cmd.Name}`{aliasText} — {cmd.Summary}");
                }
                blockBuilder.AppendLine();

                var blockStr = blockBuilder.ToString();
                if (currentPage.Length + blockStr.Length > 1800 && currentPage.Length > 0)
                {
                    pages.Add(currentPage.ToString().TrimEnd());
                    currentPage.Clear();
                }

                currentPage.Append(blockStr);
            }

            if (currentPage.Length > 0)
                pages.Add(currentPage.ToString().TrimEnd());

            if (pages.Count == 0)
            {
                await ReplyAsync("No commands available for your current permission level.");
                return;
            }

            var pageCount = pages.Count;
            if (page < 1 || page > pageCount)
            {
                await ReplyAsync($"Invalid page number. Please specify a number between 1 and {pageCount}.");
                return;
            }

            var footerText = $"Page {page}/{pageCount}";
            if (page < pageCount)
                footerText += $" | Type `help {page + 1}` for the next page.";

            var embedBuilder = new EmbedBuilder()
                .WithTitle("📖 Available Commands")
                .WithColor(EmbedStyle.Blurple)
                .WithDescription(pages[page - 1])
                .WithNexusFooter(footerText);

            try
            {
                var dmChannel = await Context.User.CreateDMChannelAsync();
                await dmChannel.SendMessageAsync(embed: embedBuilder.Build());
                await ReplyAsync($"{Context.User.Mention}, I've sent you a DM with the help information!");
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.Forbidden)
            {
                await ReplyAsync($"{Context.User.Mention}, I couldn't send you a DM because you have DMs disabled. Please enable DMs and try again.");
            }
            catch (Exception ex)
            {
                await ReplyAsync($"An error occurred while sending the DM: {ex.Message}");
            }

            if (Context.Message is IUserMessage userMessage)
                await userMessage.DeleteAsync().ConfigureAwait(false);
        }

        [Command("help")]
        [Summary("Shows information about a specific command.")]
        public async Task HelpAsync([Summary("The command to get information for.")] string command)
        {
            var searchResult = _commandService.Search(Context, command);

            if (!searchResult.IsSuccess)
            {
                await ReplyAsync($"Sorry, I couldn't find a command like **{command}**.");
                return;
            }

            var embedBuilder = new EmbedBuilder()
                .WithTitle($"📖 Command Details — `{command}`")
                .WithColor(EmbedStyle.Blurple)
                .WithNexusFooter();

            var addedCommands = new HashSet<string>();

            foreach (var match in searchResult.Commands)
            {
                var cmd = match.Command;
                var paramSyntax = string.Join(" ", cmd.Parameters.Select(p => p.IsOptional ? $"[{p.Name}]" : $"<{p.Name}>"));
                var usage = string.IsNullOrWhiteSpace(paramSyntax) ? $"`{cmd.Name}`" : $"`{cmd.Name} {paramSyntax}`";

                var key = $"{cmd.Name}:{usage}";
                if (!addedCommands.Add(key))
                    continue;

                var aliases = cmd.Aliases
                    .Where(a => !a.Equals(cmd.Name, StringComparison.OrdinalIgnoreCase))
                    .ToList();
                var aliasStr = aliases.Count > 0 ? string.Join(", ", aliases.Select(a => $"`{a}`")) : "None";

                var category = GetCategoryName(cmd.Module.Name);

                var parameters = cmd.Parameters.Select(p =>
                {
                    var opt = p.IsOptional ? $" *(optional, default: {p.DefaultValue ?? "null"})*" : "";
                    var summary = string.IsNullOrWhiteSpace(p.Summary) ? "" : $" — {p.Summary}";
                    return $"• `{p.Name}` ({p.Type.Name}){opt}{summary}";
                });
                var parameterSummary = string.Join("\n", parameters);

                var content = $"**Category:** {category}\n" +
                              $"**Usage:** {usage}\n" +
                              $"**Aliases:** {aliasStr}\n" +
                              $"**Description:** {cmd.Summary ?? "No description available."}\n\n" +
                              $"**Parameters:**\n{(string.IsNullOrEmpty(parameterSummary) ? "None" : parameterSummary)}";

                embedBuilder.AddField($"Command: {cmd.Name}", content, false);
            }

            try
            {
                var dmChannel = await Context.User.CreateDMChannelAsync();
                await dmChannel.SendMessageAsync(embed: embedBuilder.Build());
                await ReplyAsync($"{Context.User.Mention}, I've sent you a DM with the help information for the command **{command}**!");
            }
            catch (HttpException ex) when (ex.HttpCode == HttpStatusCode.Forbidden)
            {
                await ReplyAsync($"{Context.User.Mention}, I couldn't send you a DM because you have DMs disabled. Please enable DMs and try again.");
            }
            catch (Exception ex)
            {
                await ReplyAsync($"An error occurred while sending the DM: {ex.Message}");
            }

            if (Context.Message is IUserMessage userMessage)
                await userMessage.DeleteAsync().ConfigureAwait(false);
        }
    }
}
