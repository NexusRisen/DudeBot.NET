### Major Improvements & New Features
* **Upstream Synchronization**: 
    * Integrated the latest Alcremie fix, which properly handles Alcremie flavor and topping combinations (e.g., Alcremie-Ruby-Cream-Strawberry).
    * Improved the Alcremie normalizer to handle nicknames correctly.
* **Memory Leak Resolution**: Conducted a thorough sweep of the codebase to identify and fix long-standing memory leaks.
    * Implemented IDisposable and proper resource cleanup for Discord, Twitch, and YouTube integrations.
    * Resolved static event forwarder leaks in LogUtil and EchoUtil that occurred during game mode changes.
    * Ensured background tasks (Queue Monitor, DM Relay) correctly respond to cancellation tokens.
* **Update System Enhancements**:
    * **Safety First**: The program now verifies that executable assets are actually available on GitHub before initiating a download.
    * **Rich Changelogs**: Added a custom Markdown renderer to the Update Form for beautifully formatted release notes.
* **AutoOT & Language Logic**:
    * Improved ApplyAutoOT across all supported games (SV, SWSH, BDSP, LA, PLZA, LGPE).
    * Better preservation of user-requested languages from Showdown sets.
    * Implemented smart OT name truncation based on language-specific limits (6 for Asian, 12 for Latin).

### Technical Fixes & Maintenance
* **Legality Updates**: Updated PKHeX and AutoMod libraries to the latest versions for improved game compatibility.
* **Code Quality**:
    * Eliminated all build warnings in the project.
    * Fixed a potential crash in Discord embed generation due to null move data.
    * Removed redundant code and logging in several bot trade routines.
    * Ensured critical directories like trainerData are automatically created on first run.
* **Project Upgraded**: Fully migrated and tested on .NET 10.0.
