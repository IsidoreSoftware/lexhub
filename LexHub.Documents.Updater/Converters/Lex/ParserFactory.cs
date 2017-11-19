using System;
using LexHub.Documents.Models;
using LexHub.Documents.Updater.Converters.Lex.Parsers;

namespace LexHub.Documents.Updater.Converters.Lex
{
    class ParserFactory : IParserFactory
    {
        public IUnitParser GetParser(UnitType type)
        {
            switch (type)
            {
                case UnitType.Article:
                    return new ArticleParser(this);
                case UnitType.Paragraph:
                    return new ParagraphParser(this);
                case UnitType.Point:
                    return new PointParser(this);
                case UnitType.Letter:
                    return new LetterParser(this);
                case UnitType.Indent:
                    return new IndentParser(this);
                case UnitType.Section:
                    return new SectionParser(this);
                case UnitType.Part:
                    return new PartParser(this);
                case UnitType.Chapter:
                    return new ChapterParser(this);
                case UnitType.Tome:
                    throw new NotImplementedException();
                case UnitType.Title:
                    throw new NotImplementedException();
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
