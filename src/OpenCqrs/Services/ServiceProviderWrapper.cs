﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace OpenCqrs.Services
{
    public class ServiceProviderWrapper : IServiceProviderWrapper
    {
        private readonly IServiceProvider _serviceProvider;

        public ServiceProviderWrapper(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }

        public IEnumerable<T> GetServices<T>()
        {
            return _serviceProvider.GetServices<T>();
        }
    }
}
