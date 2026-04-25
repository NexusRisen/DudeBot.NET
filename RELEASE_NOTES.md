### DudeBot.NET V6.0.4

#### New in this Update
* **Critical Build Stabilization**: 
    * Synchronized the project with a verified, compatible pair of `PKHeX.Core` and `AutoMod` binaries.
    * Resolved persistent `MissingMethodException` and `EntryPointNotFoundException` that caused crashes during Pokémon generation.
    * Passed all 53 core unit tests on .NET 10.0.
* **Enhanced Alcremie Logic**:
    * Integrated advanced flavor and topping parsing for Alcremie forms.
    * Added full support for Showdown sets with nicknames (e.g., `Cream (Alcremie-Ruby-Cream-Strawberry)`).
    * Automatically injects the correct `.FormArgument` while preserving legality-critical species data.
* **Maintenance & Improvements**:
    * Optimized the `BatchNormalizer` to handle more complex nickname formats.
    * Refined API calls in `AutoLegalityWrapper` and `FossilCount` to align with the custom core engine.
