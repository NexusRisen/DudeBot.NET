using System.ComponentModel;

// ReSharper disable AutoPropertyCanBeMadeGetOnly.Global

namespace SysBot.Pokemon;

public sealed class PokeTradeHubConfig : BaseConfig
{
    private const string Cat1_General = "1. General & Program Info";
    private const string Cat2_Trade = "2. Trade & Distribution";
    private const string Cat3_Operations = "3. Queue & System Operations";
    private const string Cat4_Integrations = "4. Platform Integrations";
    private const string BotEncounter = nameof(BotEncounter);

    // 1. General & Program Info
    [Category(Cat1_General), Description("Name of the Discord Bot the Program is Running. Titles the window for easier recognition.")]
    public string BotName { get; set; } = string.Empty;

    [Browsable(false)]
    [Category(Cat1_General), Description("User's Theme Option Choice.")]
    public string ThemeOption { get; set; } = string.Empty;

    // 2. Trade & Distribution
    [Category(Cat2_Trade)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public TradeSettings Trade { get; set; } = new();

    [Category(Cat2_Trade), Description("Settings for idle distribution trades.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public DistributionSettings Distribution { get; set; } = new();

    [Category(Cat2_Trade)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public TradeAbuseSettings TradeAbuse { get; set; } = new();

    // 3. Queue & System Operations
    [Category(Cat3_Operations)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public QueueSettings Queues { get; set; } = new();

    [Category(Cat3_Operations), Description("Add extra time for slower Switches.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public TimingSettings Timings { get; set; } = new();

    [Category(Cat3_Operations), Description("Settings for automatic bot recovery after crashes.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public RecoverySettings Recovery { get; set; } = new();

    // 4. Platform Integrations
    [Category(Cat4_Integrations)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public DiscordSettings Discord { get; set; } = new();

    [Category(Cat4_Integrations)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public StoatSettings Stoat { get; set; } = new();

    [Category(Cat4_Integrations)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public KookSettings Kook { get; set; } = new();

    [Category(Cat4_Integrations), Description("Allows favored users to join the queue with a more favorable position.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public FavoredPrioritySettings Favoritism { get; set; } = new();

    [Category(Cat4_Integrations), Description("Configure generation of assets for streaming.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public StreamSettings Stream { get; set; } = new();

    // Encounter Bots - Hidden/Advanced
    [Browsable(false)]
    [Category(BotEncounter)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public EncounterSettings EncounterSWSH { get; set; } = new();

    [Browsable(false)]
    [Category(BotEncounter)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public RaidSettings RaidSWSH { get; set; } = new();

    [Browsable(false)]
    [Category(Cat2_Trade)]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public SeedCheckSettings SeedCheckSWSH { get; set; } = new();

    [Browsable(false)]
    public override bool Shuffled => Distribution.Shuffled;

    [Browsable(false)]
    [Category(BotEncounter), Description("Stop conditions for EncounterBot.")]
    [TypeConverter(typeof(ExpandableObjectConverter))]
    public StopConditionSettings StopConditions { get; set; } = new();
}
