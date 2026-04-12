# DudeBot.NET
![License](https://img.shields.io/badge/License-AGPLv3-blue.svg)

## Support Discord:

For support on setting up your own instance of DudeBot.NET, feel free to join the discord! Note: this bot is a fork of the original Sysbot.NET, don't bother the devs at PKHEX Development Project for support. 

[sys-botbase](https://github.com/olliz0r/sys-botbase) client for remote control automation of Nintendo Switch consoles.

## Screenshots

### Bots Dashboard
![Bots](pictures/Bots.bmp)

### Settings
![Settings](pictures/settings.bmp)

### Logs
![Logs](pictures/logs.bmp)

## 🌟 Key Features

### 🎮 Multi-Game Support
Automated trading and encounter bots for all modern Nintendo Switch Pokémon titles:
- **Pokémon Legends: Z-A (PLZA)**: Full support for the latest generation.
- **Pokémon Scarlet & Violet (SV)**: Including Tera Type handling and Scale information.
- **Pokémon Legends: Arceus (LA)**: Specialized support for Alpha Pokémon and research tasks.
- **Pokémon Brilliant Diamond & Shining Pearl (BDSP)**: High-performance trade logic using modernized async operations.
- **Pokémon Sword & Shield (SWSH)**: Comprehensive support for all distribution types.
- **Pokémon Let's Go, Pikachu! & Eevee! (LGPE)**: Legacy support for Kanto-based distributions.

### 🤖 Automation & Intelligence
- **Auto-Legality Mod (ALM)**: Integrated on-the-fly legalization ensures all distributed Pokémon meet strict legality standards.
- **High-Performance Logic**: BDSP trade routines refactored with `Span<byte>` and `MemoryMarshal` for maximum speed and zero-allocation memory management.
- **Async Modernization**: Fully non-blocking batch trade sequences using `Task`-based operations.
- **AutoOT Integration**: Personalize Pokémon with the receiver's trainer information automatically.

### 📊 Enhanced Discord Experience
- **Visual Embeds**: Multi-column layouts for clean and professional data visualization.
- **Advanced Metadata**: 
  - **Hyper Trained (HT)** indicators for IVs.
  - **Origin & Physical**: Clear display of Met Level, Met Date, and Met Location (with ID).
  - **Scale Visualization**: See exactly how big or small your Pokémon is.
- **Refined Nature Logic**: Detailed display for minted natures, showing both intended stats and visual nature.
- **Special Symbols**: Professional iconography for Shiny, Alpha, Marks, and Ribbons.

### 🛠️ Core Technology
- **Custom Core Integration**: Powered by a highly optimized fork of `PKHeX.Core` for early feature support and API stability.
- **Robust Testing**: Over 50 automated unit tests ensuring distribution stability and logic correctness.
- **Modern UI**: Dashboard with system tray support, theme engine, and real-time log streaming.

## SysBot.Base:
- Base logic library to be built upon in game-specific projects.
- Contains a synchronous and asynchronous Bot connection class to interact with sys-botbase.

## SysBot.Tests:
- Unit Tests for ensuring logic behaves as intended :)

# Example Implementations

The driving force to develop this project is automated bots for Nintendo Switch Pokémon games. An example implementation is provided in this repo to demonstrate interesting tasks this framework is capable of performing. Refer to the [Wiki](https://github.com/NexusRisen/DudeBot.NET/wiki) for more details on the supported Pokémon features.

## SysBot.Pokemon:
- Class library using SysBot.Base to contain logic related to creating & running Sword/Shield bots.

## SysBot.Pokemon.WinForms:
- Simple GUI Launcher for adding, starting, and stopping Pokémon bots (as described above).
- Configuration of program settings is performed in-app and is saved as a local json file.

## SysBot.Pokemon.Discord:
- Discord interface for remotely interacting with the WinForms GUI.
- Provide a discord login token and the Roles that are allowed to interact with your bots.
- Commands are provided to manage & join the distribution queue.

## SysBot.Pokemon.Twitch:
- Twitch.tv interface for remotely announcing when the distribution starts.
- Provide a Twitch login token, username, and channel for login.

## SysBot.Pokemon.YouTube:
- YouTube.com interface for remotely announcing when the distribution starts.
- Provide a YouTube login ClientID, ClientSecret, and ChannelID for login.

Uses [Discord.Net](https://github.com/discord-net/Discord.Net) , [TwitchLib](https://github.com/TwitchLib/TwitchLib) and [StreamingClientLibary](https://github.com/SaviorXTanren/StreamingClientLibrary) as a dependency via Nuget.

## Other Dependencies
Pokémon API logic is provided by [PKHeX](https://github.com/kwsch/PKHeX/), and template generation is provided by [Auto-Legality Mod](https://github.com/architdate/PKHeX-Plugins/). Current template generation uses [@santacrab2](https://www.github.com/santacrab2)'s [Auto-Legality Mod fork](https://github.com/santacrab2/PKHeX-Plugins).

# License
Refer to the `License.md` for details regarding licensing.
