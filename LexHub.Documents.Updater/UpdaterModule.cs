using Autofac;
using LexHub.Documents.Updater.Services;

namespace LexHub.Documents.Updater
{
    public class UpdaterModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Updater>();
            builder.RegisterType<LexDocumentsService>().AsImplementedInterfaces();
        }
    }
}
