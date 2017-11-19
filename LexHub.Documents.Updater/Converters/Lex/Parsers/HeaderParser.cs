using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using LexHub.Documents.Models;

namespace LexHub.Documents.Updater.Converters.Lex.Parsers
{
    class HeaderParser : IMetaDataParser
    {
        public async Task<Act> GetMetaData<T>(T source)
        {
            var toParse = source as string;
            if (toParse == null)
            {
                throw new ArgumentException("This parser supports only string as a parameter");
            }

            if (string.IsNullOrWhiteSpace(toParse))
            {
                return null;
            }

            return await CreateBaseAct<T>(toParse);
        }

        private IList<Func<string, Act, Act>> _assigmentsList = new List<Func<string, Act, Act>>
        {
            SetActId,
            IgnoreLine,
            SetDate,
            SetName
        };

        private static readonly Regex DateRegex = new Regex("^z dnia ([0-9]+) ([a-z]+) ([0-9]{4}) r.",RegexOptions.Singleline);

        private async Task<Act> CreateBaseAct<T>(string toParse)
        {
            StringReader reader = new StringReader(toParse);
            Act newAct = new Act();
            string line;
            foreach (var func in _assigmentsList)
            {
                do
                {
                    line = reader.ReadLine();
                } while (String.IsNullOrWhiteSpace(line));
                newAct = func(line, newAct);
            }

            return newAct;
        }

        private static Act SetActId(string line, Act currentAct)
        {
            currentAct.Id = line;
            currentAct.Version = line;
            currentAct.LastUpdate = DateTime.Today;
            return currentAct;
        }

        private static Act IgnoreLine(string line, Act currentAct)
        {
            return currentAct;
        }

        private static Act SetDate(string line, Act currentAct)
        {
            var matches = DateRegex.Match(line);
            if (matches.Success && matches.Groups.Count == 4)
            {
                var creationDate = new DateTime(int.Parse(matches.Groups[3].Value), MonthsToNumbers[matches.Groups[2].Value],
                    int.Parse(matches.Groups[1].Value));
                currentAct.CreationDate = creationDate;
            }

            return currentAct;
        }

        private static Act SetName(string line, Act currentAct)
        {
            currentAct.Name = line;
            return currentAct;
        }

        private static readonly ReadOnlyDictionary<string, int> MonthsToNumbers = new ReadOnlyDictionary<string, int>(
            new Dictionary<string, int>
            {
                {"stycznia", 1},
                {"lutego", 2},
                {"marca", 3},
                {"kwietnia", 4},
                {"maja", 5},
                {"czerwca", 6},
                {"lipca", 7},
                {"sierpnia", 8},
                {"września", 9},
                {"października", 10},
                {"listopada", 11},
                {"grudnia", 12},
            });
    }
}
