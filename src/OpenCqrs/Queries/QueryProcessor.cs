﻿using OpenCqrs.Services;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace OpenCqrs.Queries
{
    public class QueryProcessor : IQueryProcessor
    {
        private readonly IServiceProviderWrapper _serviceProvider;

        private static readonly ConcurrentDictionary<Type, object> _queryHandlerWrappers = new();

        public QueryProcessor(IServiceProviderWrapper serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> Process<TResult>(IQuery<TResult> query)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            var queryType = query.GetType();

            var handler = (QueryHandlerWrapperBase<TResult>)_queryHandlerWrappers.GetOrAdd(queryType,
                t => Activator.CreateInstance(typeof(QueryHandlerWrapper<,>).MakeGenericType(queryType, typeof(TResult))));

            return await handler.Handle(query, _serviceProvider);
        }
    }
}
