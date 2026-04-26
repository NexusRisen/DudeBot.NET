### DudeBot.NET V6.0.7

#### New in this Update
* **Universal Translation Engine**:
    * **Full Auto-Detection**: Automatically identifies and translates Showdown sets in **Japanese, French, Italian, German, Spanish, Korean, and Chinese (Simplified/Traditional)** without requiring manual language selection.
    * **High-Performance Caching**: Implemented a thread-safe `ConcurrentDictionary` cache for species and moves across all languages, making translations near-instant.
    * **Comprehensive Dictionaries**: Updated language-specific keywords for items, genders, shiny status, stats, and regional forms.
* **High-Performance Logic (BDSP)**:
    * **Zero-Allocation Memory Management**: Refactored BDSP trade routines using `Span<byte>` and `MemoryMarshal` to maximize throughput and minimize GC pressure.
    * **Async Modernization**: Fully transitioned core bot loops and batch trade sequences to non-blocking `Task`-based operations.
* **CI/CD & Workspace Integrity**:
    * **Linux-Friendly Build Fix**: Resolved dependency submission failures in GitHub Actions by enabling `EnableWindowsTargeting` for the WinForms project on non-Windows runners.
    * **Standardized Folder Naming**: Unified trainer data location to `TrainerDatabase` and prevented automatic creation of redundant folders.
* **Dependency & Ecosystem Updates**:
    * **Twitch Integration**: Upgraded `TwitchLib.Client` to **v4.0.1** for improved stability and modern feature support.
    * **Core Libraries**: Updated `Microsoft.Extensions` and `System.Drawing.Common` to **v10.0.7**.
* **Contributor Recognition**:
    * Added **kwsch** as the **Original Creator** of SysBot.NET and PKHeX.
    * Added **Secludedly** for significant contributions to **Medals, Refactoring, and Feature Enhancements**.
    * Fixed spelling and image URLs for all core contributors (**Lusamine**, **SantaCrab2**, **Hexbyt3**).
