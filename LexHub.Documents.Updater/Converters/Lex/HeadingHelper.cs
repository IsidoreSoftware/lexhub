using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex.Parsers;

namespace LexHub.Documents.Updater.Converters.Lex
{
    internal static class HeadingHelper
    {
        internal static UnitType? GetTypeOfHeader(this string line)
        {
            if (line == null)
            {
                return null;
            }

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
                MatchingRule = new Regex(HeadingRegexPatterns.Part,HeadingRegexPatterns.Options),
                MatchedType = UnitType.Part
            },
            new MatchingPair
            {
                MatchingRule = new Regex(HeadingRegexPatterns.Tome, HeadingRegexPatterns.Options),
                MatchedType = UnitType.Tome
            },
            new MatchingPair
            {
                MatchingRule = new Regex(HeadingRegexPatterns.Title, HeadingRegexPatterns.Options),
                MatchedType = UnitType.Title
            },
            new MatchingPair
            {
                MatchingRule = new Regex(HeadingRegexPatterns.Chapter, HeadingRegexPatterns.Options),
                MatchedType = UnitType.Chapter
            },
            new MatchingPair
            {
                MatchingRule = new Regex(HeadingRegexPatterns.Section, HeadingRegexPatterns.Options),
                MatchedType = UnitType.Section
            },
            new MatchingPair
            {
                MatchingRule = new Regex(HeadingRegexPatterns.Article, HeadingRegexPatterns.Options),
                MatchedType = UnitType.Article
            },
            new MatchingPair
            {
                MatchingRule = new Regex(HeadingRegexPatterns.Paragraph, HeadingRegexPatterns.Options),
                MatchedType = UnitType.Paragraph
            },
            new MatchingPair
            {
                MatchingRule = new Regex(HeadingRegexPatterns.Point, HeadingRegexPatterns.Options),
                MatchedType = UnitType.Point
            },
            new MatchingPair
            {
                MatchingRule = new Regex(HeadingRegexPatterns.Letter, HeadingRegexPatterns.Options),
                MatchedType = UnitType.Letter
            },
            new MatchingPair
            {
                MatchingRule = new Regex(HeadingRegexPatterns.Indent, HeadingRegexPatterns.Options),
                MatchedType = UnitType.Indent
            }
        };
    }
}
