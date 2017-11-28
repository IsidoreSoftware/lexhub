using System;

namespace LexHub.Documents.Updater.Converters.Lex.Exceptions
{
    class ParserNotFitActualContentException : ArgumentException
    {
        public ParserNotFitActualContentException(string line, IUnitParser parser) 
            : base($"Line '{line}' tried to parsed with {parser.GetType().Name}.")
        {
        }
    }
}
