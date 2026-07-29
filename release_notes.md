# Release Notes

## [9.1.22]
- **Direct Message (DM) Support & Configuration Reorganization**:
  - Added full support for executing trade commands (`.trade`, `.t`), general commands, and uploading PKHeX files (`.pk8`, `.pk9`, etc.) directly via Direct Messages (DMs) with the bot.
  - Enhanced precondition attributes (`RequireQueueRoleAttribute`, `RequireRoleAccessAttribute`) to resolve user roles across shared Discord servers during DM interactions.
  - Added null safety checks and DM message deletion error prevention.
  - Reorganized program settings into clean, numbered, logically grouped categories across `DiscordSettings`, `PokeTradeHubConfig`, and `TradeSettings`.

- **Enhanced Trade Abuse System & Nintendo NID Auto-Banning**:
  - Added `CheckMultiAccountAllTrades` setting to enforce multi-account seller detection across trade queues.
  - Enhanced `CheckPartnerReputation` to automatically add an offender's 64-bit Nintendo Account Network ID (`TrainerNID` / console identifier) to `BannedIDs` when multi-account abuse or cooldown evasion occurs during standard trade requests (distribution trades excluded).
  - Instantly blocks all future trade attempts from auto-banned console NIDs across any Discord account or in-game Trainer Name.

- **Discord Presence & Status Fixes (Online, Idle, DND)**:
  - Fixed status monitoring in `SysCord.cs` to eliminate rapid 20-second status flickering.
  - **Online (Green)**: Active when queue is open and trades are in queue or recent activity occurred within 5 minutes.
  - **Idle (Yellow/Orange)**: Active when queue is open and bot is running, but standing by with no trades for over 5 minutes.
  - **Do Not Disturb (DND/Red)**: Active when queue requests are closed/paused or bot runner is stopped.
