using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace LexHub.Documents.Updater.DI
{
    public static class DependencyInjectionBuilder
    {
        public static AutofacServiceProvider Build(IServiceCollection serviceCollection)
        {
            var containerBuilder = new ContainerBuilder();

            containerBuilder.Populate(serviceCollection);
            containerBuilder.RegisterModule<UpdaterModule>();
            
            var container = containerBuilder.Build();

            return new AutofacServiceProvider(container);
        }
    }
}
