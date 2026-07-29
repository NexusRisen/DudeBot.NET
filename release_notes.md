# Release Notes

## [9.1.23]
- **Unified Batch Trade Commands**:
  - Fixed and aligned `batchMysteryEgg` (`bme`), `batchMysteryPokemon` (`bmp`), `batchspecialrequestpokemon` (`bsrp`), `batchTrade` (`bt`), `begg`, and `itemTrade` (`bit`, `batchitem`, `bitem`, `batchitemtrade`) across Discord, Stoat, and Kook platforms.
  - Increased default batch configuration limits (`MaxPkmsPerTrade`, `MaxEggsPerBatch`, `MaxMysteryEggsPerBatch`, `MaxMysteryPokemonPerBatch`, `MaxMysteryGiftsPerBatch`, `MaxItemBatchAmount`) from 3 to 10. Omitting count parameters in batch commands now defaults to the full batch limit (10).

- **Flexible Batch Parsing & Delimiters**:
  - `ParseBatchTradeContent` supports multiline Showdown sets separated by `---` block section dividers, single-line Showdown sets with `---` at the end of each line, and single-line Showdown sets separated by newlines (`\n`), commas (`,`), or semicolons (`;`).
  - `ParseBatchItemContent` supports item quantity prefixes (`3 Master Ball`), quantity suffixes (`Master Ball x 3`), and multi-delimiter parsing (`,`, `;`, `\n`, `---`).
  - `ParseEventIndices` supports event index lists and range parsing (e.g. `1-3`, `1,2,5`).
  - Preserved raw message content across Stoat and Kook platforms so batch Showdown trades preserve multiline formatting.

- **Mystery Generation & Cross-Game Context Fixes**:
  - Updated `GenerateLegalMysteryPokemon` to try shiny sets first and gracefully fall back to clean species sets if shiny generation fails or if the species is shiny-locked.
  - Replaced brittle static switch statement in `GetContext()` with dynamic `new T().Context` resolution, preventing false *"Mystery Eggs are not available for Let's Go..."* error messages on non-LGPE games.
