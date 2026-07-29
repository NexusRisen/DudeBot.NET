using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBot.Pokemon.Discord;

/// <summary>
/// Same as <see cref="RequireRoleAccessAttribute"/> with extra consideration for bots accepting Queue requests.
/// </summary>
public sealed class RequireQueueRoleAttribute(string RoleName) : PreconditionAttribute
{
    // Create a field to store the specified name

    // Create a constructor so the name can be specified

    public override Task<PreconditionResult> CheckPermissionsAsync(ICommandContext context, CommandInfo command, IServiceProvider services)
    {
        var mgr = SysCordSettings.Manager;
        if (mgr.Config.AllowGlobalSudo && mgr.CanUseSudo(context.User.Id))
            return Task.FromResult(PreconditionResult.FromSuccess());

        IEnumerable<string> roles;
        if (context.User is SocketGuildUser gUser)
        {
            roles = gUser.Roles.Select(z => z.Name);
        }
        else if (context.Channel is IDMChannel && context.Client is DiscordSocketClient client)
        {
            roles = client.Guilds
                .SelectMany(g => g.GetUser(context.User.Id)?.Roles ?? Enumerable.Empty<SocketRole>())
                .Select(z => z.Name);
        }
        else
        {
            return Task.FromResult(PreconditionResult.FromError("You must be sending the message from a guild or DM to run this command."));
        }

        if (mgr.CanUseSudo(roles))
            return Task.FromResult(PreconditionResult.FromSuccess());

        bool canQueue = SysCordSettings.HubConfig.Queues.CanQueue;
        if (!canQueue)
            return Task.FromResult(PreconditionResult.FromError("Sorry, I am not currently accepting queue requests!"));

        if (!mgr.GetHasRoleAccess(RoleName, roles))
            return Task.FromResult(PreconditionResult.FromError("You do not have the required role to run this command."));

        return Task.FromResult(PreconditionResult.FromSuccess());
    }
}
