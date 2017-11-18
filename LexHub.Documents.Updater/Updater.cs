using LexHub.Documents.Contract;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace LexHub.Documents.Updater
{
    public class Updater
    {
        public Updater(ILoggerFactory loggerFactory, IDocumentsService documentsService)
        {
            Logger = loggerFactory.CreateLogger<Updater>();
            DocumentsService = documentsService;
        }

        private readonly ILogger Logger;
        private readonly IDocumentsService DocumentsService;

        public void Update()
        {
            Logger.LogInformation("Started updating");

            var legislation = DocumentsService.GetLegislation("Dz.U.16.1137");

            Logger.LogDebug(legislation.Name);
        }
    }
}
