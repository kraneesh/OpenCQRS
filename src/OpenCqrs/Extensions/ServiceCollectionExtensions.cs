﻿using System;
using System.Linq;
using OpenCqrs.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace OpenCqrs.Extensions
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Adds OpenCQRS. 
        /// Pass any of the types that are contained in the assemblies where your messages and handlers are.
        /// One for each assembly.
        /// e.g. typeOf(CreatePost) where CreatePost is one of your commands.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="types">The types.</param>
        public static IOpenCqrsServiceBuilder AddOpenCQRS(this IServiceCollection services, params Type[] types)
        {
            return AddOpenCQRS(services, opt => { }, types);
        }

        /// <summary>
        /// Adds OpenCQRS. 
        /// Pass any of the types that are contained in the assemblies where your messages and handlers are.
        /// One for each assembly.
        /// e.g. typeOf(CreatePost) where CreatePost is one of your commands.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <param name="setupAction">The options.</param>
        /// <param name="types">The types.</param>
        public static IOpenCqrsServiceBuilder AddOpenCQRS(this IServiceCollection services, Action<MainOptions> setupAction, params Type[] types)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            var typeList = types.ToList();
            typeList.Add(typeof(ServiceCollectionExtensions));

            services.Scan(s => s
                .FromAssembliesOf(typeList)
                .AddClasses()
                .AsImplementedInterfaces());

            services.Configure(setupAction);

            return new OpenCqrsServiceBuilder(services);
        }
    }
}
