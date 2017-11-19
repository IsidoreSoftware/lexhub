using System;
using System.IO;
using System.Text.RegularExpressions;
using HtmlAgilityPack;

namespace LexHub.Documents.Updater.Converters.Lex.Html
{
    public class HtmlToText : IHtmlToText
    {
        private readonly Regex _regex;

        public HtmlToText(string[] lineBreaksExceptionsRegEx)
        {
            var lineBreaksExceptions = lineBreaksExceptionsRegEx??new string[] { };

            _regex = new Regex($"^({String.Join('|', lineBreaksExceptions)})");
        }

        public string ConvertFile(string path)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.Load(File.OpenRead(path));

            StringWriter sw = new StringWriter();
            ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString();
        }

        public string ConvertHtml(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            StringWriter sw = new StringWriter();
            ConvertTo(doc.DocumentNode, sw);
            sw.Flush();
            return sw.ToString();
        }

        private void ConvertContentTo(HtmlNode node, TextWriter outText)
        {
            foreach (HtmlNode subnode in node.ChildNodes)
            {
                ConvertTo(subnode, outText);
            }
        }

        private void ConvertTo(HtmlNode node, TextWriter outText)
        {
            string html;
            switch (node.NodeType)
            {
                case HtmlNodeType.Comment:
                    // don't output comments
                    break;

                case HtmlNodeType.Document:
                    ConvertContentTo(node, outText);
                    break;

                case HtmlNodeType.Text:
                    // script and style must not be output
                    string parentName = node.ParentNode.Name;
                    if ((parentName == "script") || (parentName == "style"))
                        break;

                    // get text
                    html = ((HtmlTextNode)node).Text;

                    // is it in fact a special closing node output as text?
                    if (HtmlNode.IsOverlappedClosingElement(html))
                        break;

                    // check the text is meaningful and not a bunch of whitespaces
                    if (html.Trim().Length > 0)
                    {
                        var text = HtmlEntity.DeEntitize(html);
                        outText.Write(text);
                        if (!_regex.IsMatch(text))
                        {
                            outText.Write(Environment.NewLine);
                        }
                    }
                    break;

                case HtmlNodeType.Element:
                    if (node.HasChildNodes)
                    {
                        ConvertContentTo(node, outText);
                    }
                    break;
            }
        }
    }
}
