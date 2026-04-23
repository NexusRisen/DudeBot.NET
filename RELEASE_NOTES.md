### Major Improvements & New Features
* **Memory Leak Resolution**: Conducted a thorough sweep of the codebase to identify and fix long-standing memory leaks.
    * Implemented IDisposable and proper resource cleanup for Discord, Twitch, and YouTube integrations.
    * Resolved static event forwarder leaks in LogUtil and EchoUtil that occurred during game mode changes.
    * Ensured background tasks (Queue Monitor, DM Relay) correctly respond to cancellation tokens.
* **Update System Enhancements**:
    * **Safety First**: The program now verifies that executable assets are actually available on GitHub before initiating a download, preventing errors when a release is still being processed by the build pipeline.
    * **Rich Changelogs**: Added a custom Markdown renderer to the Update Form, allowing for beautifully formatted release notes with headers, bold text, and bulleted lists.
* **AutoOT & Language Logic**:
    * Improved ApplyAutoOT across all supported games (SV, SWSH, BDSP, LA, PLZA, LGPE).
    * Better preservation of user-requested languages from Showdown sets.
    * Implemented smart OT name truncation: 6-character limit for Asian languages (Japanese, Korean, Chinese) and 12-character limit for others.

### Technical Fixes & Maintenance
* **Upstream Synchronization**: Successfully merged the latest changes and dependencies from the upstream repository, including updated PKHeX and AutoMod libraries.
* **Code Quality**:
    * Eliminated all build warnings in the project.
    * Fixed a potential crash in Discord embed generation due to null move data.
    * Removed redundant code and logging in several bot trade routines.
    * Ensured critical directories like trainerData are automatically created on first run.
* **Project Upgraded**: Fully migrated and tested on .NET 10.0.
