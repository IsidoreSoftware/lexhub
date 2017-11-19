namespace LexHub.Documents.Updater.Converters.Lex.Html
{
    internal interface IHtmlToText
    {
        string ConvertFile(string path);
        string ConvertHtml(string html);
    }
}
