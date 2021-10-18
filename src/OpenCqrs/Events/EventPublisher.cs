﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenCqrs.Services;

namespace OpenCqrs.Events
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IServiceProviderWrapper _serviceProvider;

        public EventPublisher(IServiceProviderWrapper serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            if (@event == null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            var handlers = _serviceProvider.GetServices<IEventHandler<TEvent>>();

            var tasks = new List<Task>();

            foreach (var handler in handlers)
            {
                tasks.Add(handler.Handle(@event));
            }

            await Task.WhenAll(tasks);
        }
    }
}
