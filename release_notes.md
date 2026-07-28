# Release Notes

## [9.1.21]
- **Help Command System Reorganization & Fixes**:
  - Organized Discord help commands into 5 intuitive categories (`Trading & Distribution`, `Queue & Info`, `Bot Management`, `Sudo & Admin`, `Extras & Utilities`).
  - Fixed duplicate command entries caused by multi-instantiated generic modules (`TradeModule<T>`).
  - Added command alias displays (e.g. `trade` (aliases: `t`)) to help lists.
  - Enhanced detailed command help (`help <command>`) with category, usage syntax, parameter types, optional flags, default values, and summaries.
  - Updated Stoat bot help with full coverage of all missing commands grouped by category.

## [9.1.19]
- **Discord Trade Embed Improvements**:
  - Organized the `📍 Origin & Physical` section into structured, multiline key-value pairs (`Met Level`, `Met Date`, `Met Location`, `Scale`).
  - Cleaned up `Met Location` formatting by removing awkward nested bold markdown around location IDs.
  - Removed Home Tracker notice fields (`Home Tracker Detected`, `No Home Tracker`, and `Tracker ID`) from Discord trade embeds to streamline embed display.
