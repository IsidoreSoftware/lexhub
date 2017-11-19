using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LexHub.Documents.Models;

namespace LexHub.Documents.Updater.Converters.Lex.Parsers
{
    internal static class HeadingHelper
    {
        private const RegexOptions Options =
            RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.IgnorePatternWhitespace;

        internal static UnitType? GetTypeOfHeader(this string line)
        {
            return MatchingDictionary
                .FirstOrDefault(x => x.MatchingRule.IsMatch(line))?.MatchedType;
        }

        private class MatchingPair
        {
            public Regex MatchingRule { get; set; }
            public UnitType MatchedType { get; set; }
        }

        private static readonly ICollection<MatchingPair> MatchingDictionary = new List<MatchingPair>()
        {
            new MatchingPair
            {
                MatchingRule = new Regex("^CZĘŚĆ",Options),
                MatchedType = UnitType.Part
            },
            new MatchingPair
            {
                MatchingRule = new Regex("^KSIĘGA", Options),
                MatchedType = UnitType.Tome
            },
            new MatchingPair
            {
                MatchingRule = new Regex("^TYTUŁ", Options),
                MatchedType = UnitType.Title
            },
            new MatchingPair
            {
                MatchingRule = new Regex("^DZIAŁ", Options),
                MatchedType = UnitType.Chapter
            },
            new MatchingPair
            {
                MatchingRule = new Regex("^ROZDZIAŁ", Options),
                MatchedType = UnitType.Section
            },
            new MatchingPair
            {
                MatchingRule = new Regex("^ART.", Options),
                MatchedType = UnitType.Article
            },
            new MatchingPair
            {
                MatchingRule = new Regex("^§", Options),
                MatchedType = UnitType.Paragrath
            },
            new MatchingPair
            {
                MatchingRule = new Regex("^[0-9]+\\)", Options),
                MatchedType = UnitType.Point
            },
            new MatchingPair
            {
                MatchingRule = new Regex("^[a-z]\\)", Options),
                MatchedType = UnitType.Letter
            },
            new MatchingPair
            {
                MatchingRule = new Regex("^-", Options),
                MatchedType = UnitType.Indent
            }
        };
    }
}
