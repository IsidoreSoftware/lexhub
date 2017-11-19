using System.Text.RegularExpressions;

namespace LexHub.Documents.Updater.Converters.Lex
{
    public static class HeadingRegexPatterns
    {
        public const string Part = "^CZĘŚĆ +(.+)";
        public const string Tome = "^KSIĘGA +(.+)";
        public const string Title = "^TYTUŁ +(.+)";
        public const string Chapter = "^DZIAŁ +(.+)";
        public const string Section = "^ROZDZIAŁ +(.+)";
        public const string Article = "^Art\\. +([0-9]+[a-z]*)\\. +\\[(.+)\\]";
        public const string Paragraph = "^§ +([0-9a-zA-Z]+)\\.";
        public const string Point = "^([0-9]+[a-z]?)\\)";
        public const string Letter = "^([a-z])\\)";
        public const string Indent = "^–";

        public const RegexOptions Options =
            RegexOptions.IgnoreCase | RegexOptions.Singleline;
    }
}
