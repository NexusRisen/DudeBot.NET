using System;
using System.ComponentModel;
using static SysBot.Pokemon.TradeSettings;

namespace SysBot.Pokemon;

public class DiscordSettings
{
    private const string Cat1_Startup = "1. Credentials & Startup";
    private const string Cat2_Channels = "2. Channels & DM Access";
    private const string Cat3_Roles = "3. Roles & Security";
    private const string Cat4_Commands = "4. Commands & Features";
    private const string Cat5_Appearance = "5. Embeds & Appearance";
    private const string Cat6_AI = "6. AI & Smart Features";
    private const string Cat7_Abuse = "7. Logging & Access Restrictions";

    public enum EmbedColorOption
    {
        Blue,
        Green,
        Red,
        Gold,
        Purple,
        Teal,
        Orange,
        Magenta,
        LightGrey,
        DarkGrey
    }

    public enum ThumbnailOption
    {
        Gengar,
        Pikachu,
        Umbreon,
        Sylveon,
        Charmander,
        Jigglypuff,
        Flareon,
        Custom
    }

    // 1. Credentials & Startup
    [Category(Cat1_Startup), Description("Bot login token."), DisplayName("Discord Bot Token")]
    public string Token { get; set; } = string.Empty;

    [Category(Cat1_Startup), Description("Bot command prefix."), DisplayName("Default Command Prefix")]
    public string CommandPrefix { get; set; } = "$";

    [Category(Cat1_Startup), Description("When True, allows any common command prefix."), DisplayName("Allow Any Prefix")]
    public bool AllowAnyPrefix { get; set; } = false;

    [Category(Cat1_Startup), Description("Custom Status for playing a game."), DisplayName("Bot Game Status")]
    public string BotGameStatus { get; set; } = "Trading Pokémon";

    [Category(Cat1_Startup), Description("Status color reflects trade-only bots."), DisplayName("Status Color Trade Change")]
    public bool BotColorStatusTradeOnly { get; set; } = true;

    [Category(Cat1_Startup), Description("Send online/offline status embed to whitelisted channels."), DisplayName("Bot Embed Status")]
    public bool BotEmbedStatus { get; set; } = true;

    [Category(Cat1_Startup), Description("Update channel name with status emoji."), DisplayName("Channel Status")]
    public bool ChannelStatus { get; set; } = true;

    [Category(Cat1_Startup), Description("List of modules that will not be loaded when the bot starts."), DisplayName("Module Blacklist")]
    public string ModuleBlacklist { get; set; } = string.Empty;

    [Category(Cat1_Startup), Description("Custom emoji for offline status."), DisplayName("Offline Emoji")]
    public string OfflineEmoji { get; set; } = "❌";

    [Category(Cat1_Startup), Description("Custom emoji for online status."), DisplayName("Online Emoji")]
    public string OnlineEmoji { get; set; } = "✅";

    // 2. Channels & DM Access
    [Category(Cat2_Channels), Description("Channels where the bot acknowledges commands."), DisplayName("Channel Whitelist")]
    public RemoteControlAccessList ChannelWhitelist { get; set; } = new();

    [Category(Cat2_Channels), Description("User ID or Channel ID to forward bot DMs to."), DisplayName("Bot DMs Forwarder")]
    public string UserDMsToBotForwarder { get; set; } = string.Empty;

    [Category(Cat2_Channels), Description("Channels that log announcements."), DisplayName("Announcement Channels")]
    public RemoteControlAccessList AnnouncementChannels { get; set; } = new();

    [Category(Cat2_Channels), Description("Channels that log trade start messages."), DisplayName("Trade Starting Channels")]
    public RemoteControlAccessList TradeStartingChannels { get; set; } = new();

    [Category(Cat2_Channels), Description("Reply when user cannot use command in channel."), DisplayName("Reply on Command Error")]
    public bool ReplyCannotUseCommandInChannel { get; set; } = true;

    // 3. Roles & Security
    [Category(Cat3_Roles), Description("Comma separated Discord user IDs with sudo access."), DisplayName("Global Sudo List")]
    public RemoteControlAccessList GlobalSudoList { get; set; } = new();

    [Category(Cat3_Roles), Description("Enable global sudo support."), DisplayName("Allow Global Sudo")]
    public bool AllowGlobalSudo { get; set; } = true;

    [Category(Cat3_Roles), Description("Roles allowed to enter the Trade queue."), DisplayName("Role can Trade")]
    public RemoteControlAccessList RoleCanTrade { get; set; } = new() { AllowIfEmpty = true };

    [Category(Cat3_Roles), Description("Roles allowed to enter the Clone queue."), DisplayName("Role can Clone")]
    public RemoteControlAccessList RoleCanClone { get; set; } = new() { AllowIfEmpty = true };

    [Category(Cat3_Roles), Description("Roles allowed to enter the Dump queue."), DisplayName("Role can Dump")]
    public RemoteControlAccessList RoleCanDump { get; set; } = new() { AllowIfEmpty = true };

    [Category(Cat3_Roles), Description("Roles allowed to enter the FixOT queue."), DisplayName("Role can FixOT")]
    public RemoteControlAccessList RoleCanFixOT { get; set; } = new() { AllowIfEmpty = true };

    [Category(Cat3_Roles), Description("Roles allowed to enter Seed Check/Special Request queue."), DisplayName("Role can Seed/Special")]
    public RemoteControlAccessList RoleCanSeedCheckorSpecialRequest { get; set; } = new() { AllowIfEmpty = true };

    [Category(Cat3_Roles), Description("Roles allowed for Favored Priority."), DisplayName("Favored Roles")]
    public RemoteControlAccessList RoleFavored { get; set; } = new() { AllowIfEmpty = false };

    [Category(Cat3_Roles), Description("Roles given Tier 1 Queue Priority."), DisplayName("Role Tier 1")]
    public RemoteControlAccessList RoleTier1 { get; set; } = new() { AllowIfEmpty = false };

    [Category(Cat3_Roles), Description("Roles given Tier 2 Queue Priority."), DisplayName("Role Tier 2")]
    public RemoteControlAccessList RoleTier2 { get; set; } = new() { AllowIfEmpty = false };

    [Category(Cat3_Roles), Description("Roles given Tier 3 Queue Priority."), DisplayName("Role Tier 3")]
    public RemoteControlAccessList RoleTier3 { get; set; } = new() { AllowIfEmpty = false };

    [Category(Cat3_Roles), Description("Roles given Tier 4 Queue Priority."), DisplayName("Role Tier 4")]
    public RemoteControlAccessList RoleTier4 { get; set; } = new() { AllowIfEmpty = false };

    [Category(Cat3_Roles), Description("Roles allowed for console remote control."), DisplayName("User Remote Control Roles")]
    public RemoteControlAccessList RoleRemoteControl { get; set; } = new() { AllowIfEmpty = false };

    [Category(Cat3_Roles), Description("Roles allowed to bypass command restrictions."), DisplayName("Allowed Sudo Roles")]
    public RemoteControlAccessList RoleSudo { get; set; } = new() { AllowIfEmpty = false };

    // 4. Commands & Features
    [Category(Cat4_Commands), Description("Convert PKM attachments to Showdown set."), DisplayName("Convert PKMs to Showdown")]
    public bool ConvertPKMToShowdownSet { get; set; } = true;

    [Category(Cat4_Commands), Description("Reply with Showdown set in any channel."), DisplayName("Convert PKMs (All Channels)")]
    public bool ConvertPKMReplyAnyChannel { get; set; } = false;

    [Category(Cat4_Commands), Description("Enable medals system."), DisplayName("Enable Medals System")]
    public bool EnableMedals { get; set; } = true;

    [Category(Cat4_Commands), Description("Reply when user thanks the bot."), DisplayName("Reply to Thanks")]
    public bool ReplyToThanks { get; set; } = false;

    [Category(Cat4_Commands), Description("Return user traded PKM files."), DisplayName("Return User-Traded PKM Files")]
    public bool ReturnPKMs { get; set; } = true;

    [Category(Cat4_Commands), Description("Automatic error/command message deletion."), DisplayName("Message Deletion Enabled")]
    public bool MessageDeletionEnabled { get; set; } = true;

    [Category(Cat4_Commands), Description("Seconds before deleting error messages."), DisplayName("Delete Message Delay")]
    public int ErrorMessageDeleteDelaySeconds { get; set; } = 10;

    [Category(Cat4_Commands), Description("Delete user command messages alongside bot responses."), DisplayName("Delete Bot Commands")]
    public bool DeleteUserCommandMessages { get; set; } = true;

    // 5. Embeds & Appearance
    [Category(Cat5_Appearance), Description("Additional text for embed description."), DisplayName("Additional Embed Text")]
    public string[] AdditionalEmbedText { get; set; } = [];

    [Category(Cat5_Appearance), Description("Custom hello reply message."), DisplayName("Hello Response")]
    public string HelloResponse { get; set; } = "Hi {0}!";

    [Category(Cat5_Appearance)]
    public AnnouncementSettingsCategory AnnouncementSettings { get; set; } = new();

    // 6. AI & Smart Features
    [Category(Cat6_AI)]
    public AISettingsCategory AISettings { get; set; } = new();

    // 7. Logging & Access Restrictions
    [Category(Cat7_Abuse), Description("Channel IDs that echo bot logs."), DisplayName("Logging Channels")]
    public RemoteControlAccessList LoggingChannels { get; set; } = new();

    [Category(Cat7_Abuse), Description("Channels that log abuse messages."), DisplayName("Abuse Log Channels")]
    public RemoteControlAccessList AbuseLogChannels { get; set; } = new();

    [Category(Cat7_Abuse), Description("Blacklisted Discord user IDs."), DisplayName("User Blacklist")]
    public RemoteControlAccessList UserBlacklist { get; set; } = new();

    [Category(Cat7_Abuse), Description("Blacklisted Discord server IDs."), DisplayName("Server Blacklist")]
    public RemoteControlAccessList ServerBlacklist { get; set; } = new() { AllowIfEmpty = false };

    public override string ToString() => "Discord Integration Settings";

    [Category(Cat6_AI), TypeConverter(typeof(CategoryConverter<AISettingsCategory>))]
    public class AISettingsCategory
    {
        [Category(Cat6_AI), Description("Hugging Face API Key."), DisplayName("Hugging Face API Key")]
        public string HuggingFaceApiKey { get; set; } = string.Empty;

        [Category(Cat6_AI), Description("Hugging Face Model ID (e.g., 'Qwen/Qwen2.5-7B-Instruct')."), DisplayName("Hugging Face Model")]
        public string HuggingFaceModel { get; set; } = "Qwen/Qwen2.5-7B-Instruct";

        [Category(Cat6_AI), Description("The maximum number of tokens the AI can generate."), DisplayName("Max Tokens")]
        public int MaxTokens { get; set; } = 800;

        [Category(Cat6_AI), Description("Controls randomness. Lower is more deterministic, higher is more creative."), DisplayName("Temperature")]
        public float Temperature { get; set; } = 0.7f;

        [Category(Cat6_AI), Description("Controls diversity via nucleus sampling."), DisplayName("Top P")]
        public float TopP { get; set; } = 0.9f;

        [Category(Cat6_AI), Description("Enable AI Chatbot functionality."), DisplayName("Enable AI Chatbot")]
        public bool EnableAIChatbot { get; set; } = false;

        public override string ToString() => "AI Chatbot Settings";
    }

    [Category(Cat5_Appearance), TypeConverter(typeof(CategoryConverter<AnnouncementSettingsCategory>))]
    public class AnnouncementSettingsCategory
    {
        public EmbedColorOption AnnouncementEmbedColor { get; set; } = EmbedColorOption.Purple;

        [Category(Cat5_Appearance), Description("Thumbnail option for announcements.")]
        public ThumbnailOption AnnouncementThumbnailOption { get; set; } = ThumbnailOption.Gengar;

        [Category(Cat5_Appearance), Description("Custom thumbnail URL for announcements.")]
        public string CustomAnnouncementThumbnailUrl { get; set; } = string.Empty;

        [Category(Cat5_Appearance), Description("Enable random color selection for announcements.")]
        public bool RandomAnnouncementColor { get; set; } = false;

        [Category(Cat5_Appearance), Description("Enable random thumbnail selection for announcements.")]
        public bool RandomAnnouncementThumbnail { get; set; } = false;

        public override string ToString() => "Announcement Settings";
    }

}
