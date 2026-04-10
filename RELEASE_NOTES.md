# Release Notes - DudeBot.NET Update

## 🚀 Improvements & New Features

### 📦 Batch Trade Refactoring
- **New Settings Category**: Introduced `BatchTradeConfig` to group all batch-related settings.
- **Improved Defaults**: `MaxPkmsPerTrade` now intelligently defaults to **3** if the configuration is set to less than 1.
- **No Upper Limit**: Removed the arbitrary upper limit on batch trades (use with caution based on your queue volume).
- **Toggle Support**: Added the ability to completely disable Batch Trades in the Hub settings.

### 🤖 Discord Integration & Stability
- **Fixed Error 40003**: Resolved the "Opening DMs too fast" issue by implementing a centralized DM rate-limiting system with a mandatory 2-second cooldown.
- **Exception Fixes**: Fixed `ArgumentOutOfRangeException` in `DiscordManager` and `QueueModule` to ensure smoother command processing.
- **Refactored Helpers**: Consolidated DM handling logic in `EmbedHelper` and `ReusableActions` for better performance.

## 🛠 Technical Updates

### 🔧 Modernization & Automation
- **.NET 10.0 Upgrade**: Unified the solution to target the latest **.NET 10.0** framework.
- **NuGet Overhaul**: Updated all dependencies (including `Discord.Net`, `NLog`, `Newtonsoft.Json`, and `FluentAssertions`) to their latest stable versions.
- **CI/CD Workflows**: Added GitHub Actions for automatic builds, unit testing, and automated releases (triggered via version tags like `v1.5.0`).
- **NLog 6 Compatibility**: Updated logging infrastructure to support the latest NLog breaking changes.
- **FluentAssertions 8 Compatibility**: Refactored unit tests to align with updated assertion syntax.

### 🐛 Bug Fixes
- **Decoder Implementation**: Completed the hex-to-byte decoding logic in `Decoder.cs` which was causing test failures.
- **WinForms Cleanup**: Resolved initialization warnings in the WinForms bot runner by properly passing configuration through constructors.
- **Unit Tests**: Updated `Spammy` string tests to match the current bot logic (now allows all names by default).

## 🔒 Security
- **Credential Protection**: Added `.env` to `.gitignore` to prevent sensitive tokens and API keys from being committed to source control.
