using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace OpenCqrs.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddOpenCQRS(this IServiceCollection services, params Type[] types)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            var typeList = types.ToList();
            typeList.Add(typeof(ServiceCollectionExtensions));

            services.Scan(s => s
                .FromAssembliesOf(typeList)
                .AddClasses()
                .AsImplementedInterfaces());
        }
    }
}
