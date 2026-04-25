### DudeBot.NET V6.0.5

#### New in this Update
* **Codebase Refactoring & Cleanup**:
    * **AutoLegalityWrapper**: Simplified trainer info retrieval and refactored fixed OT checks using modern C# switch expressions.
    * **LanguageHelper**: Unified species name retrieval, removed redundant methods, and improved language detection logic.
    * **LegalitySettings**: Corrected `CreateDefaults` logic to properly initialize trainer directories and improved setting descriptions.
* **Upstream Synchronization**:
    * Synchronized with the latest upstream changes (v1.4.4).
    * Integrated LGPE language fixes and updated trainer data for WA9.
    * Improved trainer directory handling to align with current ALM standards.
* **Build & Performance**:
    * Verified successful builds for Console and WinForms projects on .NET 10.0.
    * Cleaned up redundant string allocations in language processing routines.
