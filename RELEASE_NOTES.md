### DudeBot.NET V6.0.6

#### New in this Update
* **Super Stable AutoOT (Real-Time Override)**:
    * **Modern Games (SV, SWSH, BDSP, LA, PLZA)**: Perfected live RAM overrides using multi-byte aware sanitization (`StringInfo`) to prevent string corruption.
    * **LGPE Implementation**: Specialized memory-based AutoOT that uses saved data to ensure trade stability and prevent desyncs.
    * **Strict Event Protection**: Improved `FatefulEncounter` logic. The bot now automatically detects and preserves legitimate Event OTs, only overriding those generated with bot defaults or ALM placeholders.
    * **Enhanced Diagnostics**: Added explicit detection and logging for "ALM" (SantaCrab2) fallback OTs to help troubleshoot missing trainer data.
* **Universal Translation Engine**:
    * **Global Support**: Full auto-detection and translation for **Japanese, French, Italian, German, Spanish, Korean, and Chinese (Simplified/Traditional)**.
    * **High-Performance Caching**: Implemented a thread-safe `ConcurrentDictionary` cache for species and moves across all languages, making translations near-instant.
    * **Comprehensive Dictionaries**: Updated language-specific keywords for items, genders, shiny status, stats, and regional forms.
* **Core Cleanup & Workspace Integrity**:
    * **Standardized Folder Naming**: Unified trainer data location to `TrainerDatabase`.
    * **Clean Filesystem Policy**: Prevented automatic creation of `TrainerDatabase` and `records` folders to keep the workspace tidy.
    * **Stability Overhaul**: Wrapped critical trade logic in robust `try-catch` blocks with granular error reporting.

### DudeBot.NET V6.0.6

