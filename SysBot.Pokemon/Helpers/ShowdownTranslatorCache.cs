using PKHeX.Core;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace SysBot.Pokemon
{
    public static class ShowdownTranslatorCache
    {
        private static readonly ConcurrentDictionary<LanguageID, Dictionary<string, int>> _speciesCache = new();
        private static readonly ConcurrentDictionary<LanguageID, Dictionary<string, int>> _moveCache = new();

        public static Dictionary<string, int> GetSpeciesCache(LanguageID lang)
        {
            return _speciesCache.GetOrAdd(lang, l =>
            {
                var langCode = l.GetLanguageCode();
                var strings = GameInfo.GetStrings(langCode);
                return strings.Species
                    .Select((name, index) => new { name, index })
                    .Where(x => !string.IsNullOrEmpty(x.name))
                    .OrderByDescending(x => x.name.Length)
                    .ToDictionary(x => x.name, x => x.index);
            });
        }

        public static Dictionary<string, int> GetMoveCache(LanguageID lang)
        {
            return _moveCache.GetOrAdd(lang, l =>
            {
                var langCode = l.GetLanguageCode();
                var strings = GameInfo.GetStrings(langCode);
                return strings.Move
                    .Select((name, index) => new { name, index })
                    .Where(x => !string.IsNullOrEmpty(x.name))
                    .OrderByDescending(x => x.name.Length)
                    .ToDictionary(x => x.name, x => x.index);
            });
        }

        // Keep existing properties for backward compatibility with existing Chinese logic if needed
        public static Dictionary<string, int> SpeciesZhToNo => GetSpeciesCache(LanguageID.ChineseS);
        public static Dictionary<string, int> MoveZhToIndex => GetMoveCache(LanguageID.ChineseS);
    }
}
