using PKHeX.Core;
using PKHeX.Core.AutoMod;
using SysBot.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using static SysBot.Pokemon.TradeSettings;

namespace SysBot.Pokemon.Helpers
{
public abstract class TradeExtensions<T> where T : PKM, new()
{
    public static readonly ushort[] ExplicitlyBlockedHeldItems =
    [
        534, // Red Orb
        535, // Blue Orb
    ];

    public static readonly string[] MarkTitle =
    [
        " The Peckish", " The Sleepy", " The Dozy", " The Early Riser", " The Cloud Watcher", " The Sodden",
        " The Thunderstruck", " The Snow Frolicker", " The Shivering", " The Parched", " The Sandswept",
        " The Mist Drifter", " The Chosen One", " The Catch of The Day", " The Curry Connoisseur",
        " The Sociable", " The Recluse", " The Rowdy", " The Spacey", " The Anxious", " The Giddy",
        " The Radiant", " The Serene", " The Feisty", " The Daydreamer", " The Joyful", " The Furious",
        " The Beaming", " The Teary-Eyed", " The Chipper", " The Grumpy", " The Scholar", " The Rampaging",
        " The Opportunist", " The Stern", " The Kindhearted", " The Easily Flustered", " The Driven",
        " The Apathetic", " The Arrogant", " The Reluctant", " The Humble", " The Pompous", " The Lively",
        " The Worn-Out", " Of The Distant Past", " The Twinkling Star", " The Paldea Champion", " The Great",
        " The Teeny", " The Treasure Hunter", " The Reliable Partner", " The Gourmet", " The One-in-a-Million",
        " The Former Alpha", " The Unrivaled", " The Former Titan",
    ];

    public static readonly ushort[] ShinyLock = [ (ushort)Species.Victini, (ushort)Species.Keldeo, (ushort)Species.Volcanion, (ushort)Species.Cosmog, (ushort)Species.Cosmoem, (ushort)Species.Magearna, (ushort)Species.Marshadow, (ushort)Species.Eternatus, (ushort)Species.Kubfu, (ushort)Species.Urshifu, (ushort)Species.Zarude, (ushort)Species.Glastrier, (ushort)Species.Spectrier, (ushort)Species.Calyrex ];

    public static T CherishHandler(MysteryGift mg, ITrainerInfo info)
    {
        var result = EntityConverterResult.None;
        var mgPkm = mg.ConvertToPKM(info);
        bool canConvert = EntityConverter.IsConvertibleToFormat(mgPkm, info.Generation);
        mgPkm = canConvert ? EntityConverter.ConvertToType(mgPkm, typeof(T), out result) : mgPkm;

        if (mgPkm is not null && result is EntityConverterResult.Success)
        {
            var laTemp = new LegalityAnalysis(mgPkm);
            var enc = laTemp.EncounterMatch;
            mgPkm.SetHandlerandMemory(info, enc);

            if (mgPkm.TID16 is 0 && mgPkm.SID16 is 0)
            {
                mgPkm.TID16 = info.TID16;
                mgPkm.SID16 = info.SID16;
            }

            mgPkm.CurrentLevel = mg.LevelMin;
            if (mgPkm.Species is (ushort)Species.Giratina && mgPkm.Form > 0)
                mgPkm.HeldItem = 112;
            else if (mgPkm.Species is (ushort)Species.Silvally && mgPkm.Form > 0)
                mgPkm.HeldItem = mgPkm.Form + 903;
            else mgPkm.HeldItem = 0;

            mgPkm = TrashBytes((T)mgPkm, laTemp);

            var la = new LegalityAnalysis(mgPkm);
            if (!la.Valid)
            {
                mgPkm.SetRandomIVs(6);
                var text = ShowdownParsing.GetShowdownText(mgPkm);
                var set = new ShowdownSet(text);
                var template = AutoLegalityWrapper.GetTemplate(set);
                var pk = AutoLegalityWrapper.GetLegal(info, template, out _);
                pk.SetAllTrainerData(info);
                return (T)pk;
            }
            return (T)mgPkm;
        }
        return new();
    }

    public static void DittoTrade(PKM pkm)
    {
        var dittoStats = new string[] { "atk", "spe", "spa" };
        var nickname = pkm.Nickname.ToLower();
        pkm.StatNature = pkm.Nature;
        pkm.MetLocation = pkm switch { PB8 => 400, PK9 => 28, _ => 162 };
        pkm.MetLevel = pkm switch { PB8 => 29, PK9 => 34, _ => pkm.MetLevel };
        if (pkm is PK9 pk9)
        {
            pk9.ObedienceLevel = pk9.MetLevel;
            pk9.TeraTypeOriginal = PKHeX.Core.MoveType.Normal;
            pk9.TeraTypeOverride = (PKHeX.Core.MoveType)19;
        }
        pkm.Ball = 21;
        pkm.IVs = [31, nickname.Contains(dittoStats[0]) ? 0 : 31, 31, nickname.Contains(dittoStats[1]) ? 0 : 31, nickname.Contains(dittoStats[2]) ? 0 : 31, 31];
        TrashBytes(pkm, new LegalityAnalysis(pkm));
    }

    public static string FormOutput(ushort species, byte form, out string[] formString)
    {
        var strings = GameInfo.GetStrings("en");
        formString = FormConverter.GetFormList(species, strings.Types, strings.forms, GameInfo.GenderSymbolASCII, typeof(T) == typeof(PK8) ? EntityContext.Gen8 : EntityContext.Gen4);
        if (formString.Length is 0) return string.Empty;
        formString[0] = "";
        if (form >= formString.Length) form = (byte)(formString.Length - 1);
        return formString[form].Contains('-') ? formString[form] : formString[form] == "" ? "" : $"-{formString[form]}";
    }

    public static bool HasAdName(T pk, out string ad)
    {
        const string domainPattern = @"(?<=\w)\.(com|org|net|gg|xyz|io|tv|co|me|us|uk|ca|de|fr|jp|au|eu|ch|it|nl|ru|br|in|fun|info|blog|int|gov|edu|app|art|biz|bot|buzz|dev|eco|fan|fans|forum|free|game|help|host|inc|icu|live|lol|market|media|news|ninja|now|one|ong|online|page|porn|pro|red|sale|sex|sexy|shop|site|store|stream|tech|tel|top|tube|uno|vip|website|wiki|work|world|wtf|xxx|zero|youtube|zone|nyc|onion|bit|crypto|meme)\b";
        bool ot = Regex.IsMatch(pk.OriginalTrainerName, domainPattern, RegexOptions.IgnoreCase);
        bool nick = Regex.IsMatch(pk.Nickname, domainPattern, RegexOptions.IgnoreCase);
        ad = ot ? pk.OriginalTrainerName : nick ? pk.Nickname : "";
        return ot || nick;
    }

    public static bool HasMark(IRibbonIndex pk, out RibbonIndex result, out string markTitle)
    {
        result = default;
        markTitle = string.Empty;
        if (pk is IRibbonSetMark9 ribbonSetMark)
        {
            if (ribbonSetMark.RibbonMarkMightiest) { result = RibbonIndex.MarkMightiest; markTitle = " The Unrivaled"; return true; }
            if (ribbonSetMark.RibbonMarkAlpha) { result = RibbonIndex.MarkAlpha; markTitle = pk is PA9 ? (new LegalityAnalysis((PA9)pk).EncounterOriginal.Context == ((PA9)pk).Context ? " The Alpha" : " The Former Alpha") : " The Former Alpha"; return true; }
            if (ribbonSetMark.RibbonMarkTitan) { result = RibbonIndex.MarkTitan; markTitle = " The Former Titan"; return true; }
            if (ribbonSetMark.RibbonMarkJumbo) { result = RibbonIndex.MarkJumbo; markTitle = " The Great"; return true; }
            if (ribbonSetMark.RibbonMarkMini) { result = RibbonIndex.MarkMini; markTitle = " The Teeny"; return true; }
        }
        for (var mark = RibbonIndex.MarkLunchtime; mark <= RibbonIndex.MarkSlump; mark++)
        {
            if (pk.GetRibbon((int)mark)) { result = mark; markTitle = MarkTitle[(int)mark - (int)RibbonIndex.MarkLunchtime]; return true; }
        }
        return false;
    }

    public static string PokeImg(PKM pkm, bool canGmax, bool fullSize, ImageSize? preferredImageSize = null)
    {
        bool md = false, fd = false;
        string[] baseLink = (fullSize ? "https://raw.githubusercontent.com/Havokx89/HomeImages/master/512x512/poke_capture_0001_000_mf_n_00000000_f_n.png" : "https://raw.githubusercontent.com/Havokx89/HomeImages/master/256x256/poke_capture_0001_000_mf_n_00000000_f_n.png").Split('_');
        if (Enum.IsDefined(typeof(GenderDependent), pkm.Species) && !canGmax && pkm.Form is 0) { if (pkm.Gender == 0 && pkm.Species != (int)Species.Torchic) md = true; else fd = true; }
        int form = pkm.Species switch { (int)Species.Sinistea or (int)Species.Polteageist or (int)Species.Rockruff or (int)Species.Mothim => 0, (int)Species.Alcremie when pkm.IsShiny || canGmax => 0, _ => pkm.Form };
        if (pkm.Species is (ushort)Species.Sneasel) { if (pkm.Gender is 0) md = true; else fd = true; }
        if (pkm.Species is (ushort)Species.Basculegion) { if (pkm.Gender is 0) { md = true; pkm.Form = 0; } else { pkm.Form = 1; } string s = pkm.IsShiny ? "r" : "n", g = md && pkm.Gender is not 1 ? "md" : "fd"; return "https://raw.githubusercontent.com/Havokx89/HomeImages/master/256x256/poke_capture_0" + $"{pkm.Species}" + "_00" + $"{pkm.Form}" + "_" + $"{g}" + "_n_00000000_f_" + $"{s}" + ".png"; }
        baseLink[2] = pkm.Species < 10 ? $"000{pkm.Species}" : pkm.Species < 100 ? $"00{pkm.Species}" : pkm.Species >= 1000 ? $"{pkm.Species}" : $"0{pkm.Species}";
        baseLink[3] = pkm.Form < 10 ? $"00{form}" : $"0{form}";
        baseLink[4] = pkm.PersonalInfo.OnlyFemale ? "fo" : pkm.PersonalInfo.OnlyMale ? "mo" : pkm.PersonalInfo.Genderless ? "uk" : fd ? "fd" : md ? "md" : "mf";
        baseLink[5] = canGmax ? "g" : "n";
        baseLink[6] = "0000000" + ((pkm.Species == (int)Species.Alcremie && !canGmax) ? ((IFormArgument)pkm).FormArgument.ToString() : "0");
        baseLink[8] = pkm.IsShiny ? "r.png" : "n.png";
        return string.Join("_", baseLink);
    }

    public static bool ShinyLockCheck(ushort species, string form, string ball = "")
    {
        if (ShinyLock.Contains(species)) return true;
        if (form is not "" && (species is (ushort)Species.Zapdos or (ushort)Species.Moltres or (ushort)Species.Articuno)) return true;
        if (ball.Contains("Beast") && (species is (ushort)Species.Poipole or (ushort)Species.Naganadel)) return true;
        if (typeof(T) == typeof(PB8) && (species is (ushort)Species.Manaphy or (ushort)Species.Mew or (ushort)Species.Jirachi)) return true;
        if (species is (ushort)Species.Pikachu && form is not "" && form is not "-Partner") return true;
        if ((species is (ushort)Species.Zacian or (ushort)Species.Zamazenta) && !ball.Contains("Cherish")) return true;
        return false;
    }

    public static PKM TrashBytes(PKM pkm, LegalityAnalysis? la = null)
    {
        var result = (T)pkm.Clone();
        if (result.Version is not GameVersion.GO) result.MetDate = DateOnly.FromDateTime(DateTime.Now);
        if (la?.Valid == true) { var withNickname = (T)result.Clone(); withNickname.IsNicknamed = true; withNickname.Nickname = "UwU"; withNickname.SetDefaultNickname(la); result = withNickname; }
        return result;
    }

    public static bool IsEggCheck(string showdownSet)
    {
        var firstLine = showdownSet.Split('\n').FirstOrDefault();
        if (string.IsNullOrWhiteSpace(firstLine)) return false;
        if (firstLine.IndexOf('@') > 0) firstLine = firstLine[..firstLine.IndexOf('@')].Trim();
        return firstLine.Split([' ', '('], StringSplitOptions.RemoveEmptyEntries).Any(word => string.Equals(word, "Egg", StringComparison.OrdinalIgnoreCase));
    }

    public static byte DetectShowdownLanguage(string content)
    {
        var batchMatch = Regex.Match(content, @"\.Language=(\d+)");
        if (batchMatch.Success && byte.TryParse(batchMatch.Groups[1].Value, out byte langCode)) return langCode >= 1 && langCode <= 11 ? langCode : (byte)2;
        var lines = content.Split('\n');
        foreach (var line in lines) if (line.StartsWith("Language", StringComparison.OrdinalIgnoreCase)) { var language = line.Substring(line.IndexOf(':') + 1).Trim().ToLowerInvariant(); return language switch { "english" or "eng" or "en" => 2, "french" or "français" or "fra" or "fr" => 3, "italian" or "italiano" or "ita" or "it" => 4, "german" or "deutsch" or "deu" or "de" => 5, "spanish" or "español" or "spa" or "es" => 7, "spanish-latam" or "spanishl" or "es-419" or "latam" => 11, "japanese" or "日本語" or "jpn" or "ja" => 1, "korean" or "한국어" or "kor" or "ko" => 8, "chinese simplified" or "中文简体" or "chs" or "zh-cn" => 9, "chinese traditional" or "中文繁體" or "cht" or "zh-tw" => 10, _ => 2 }; }
        if (content.Contains("Talent") || content.Contains("Bonheur") || content.Contains("Chromatique") || content.Contains("Niveau") || content.Contains("Type Téra")) return 3;
        if (content.Contains("Abilità") || content.Contains("Natura") || content.Contains("Amicizia") || content.Contains("Cromatico") || content.Contains("Livello") || content.Contains("Teratipo")) return 4;
        if (content.Contains("Fähigkeit") || content.Contains("Wesen") || content.Contains("Freundschaft") || content.Contains("Schillerndes") || content.Contains("Tera-Typ")) return 5;
        if (content.Contains("Habilidad") || content.Contains("Naturaleza") || content.Contains("Felicidad") || content.Contains("Teratipo")) return 7;
        if (content.Contains("特性") || content.Contains("性格") || content.Contains("なつき度") || content.Contains("光ひかる") || content.Contains("テラスタイプ")) return 1;
        if (content.Contains("특성") || content.Contains("성격") || content.Contains("친밀도") || content.Contains("빛나는") || content.Contains("테라스탈타입")) return 8;
        if (content.Contains("亲密度") || content.Contains("异色") || content.Contains("太晶属性")) return 9;
        if (content.Contains("親密度") || content.Contains("發光寶") || content.Contains("太晶屬性")) return 10;
        return 0;
    }

    public static bool IsItemBlocked(PKM pkm)
    {
        var held = pkm.HeldItem;
        if (held <= 0) return false;
        if (pkm.Context == EntityContext.Gen9a && ExplicitlyBlockedHeldItems.Contains((ushort)held)) { pkm.HeldItem = 796; LogUtil.LogInfo($"Replaced Illegal item '{GameInfo.Strings.Item[held]}' with '{GameInfo.Strings.Item[796]}' for {GameInfo.Strings.Species[pkm.Species]}", "BlockItem"); }
        return !ItemRestrictions.IsHeldItemAllowed(held, pkm.Context) || TradeRestrictions.IsUntradableHeld(pkm.Context, held);
    }
}
}
