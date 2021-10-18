﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using OpenCqrs.Events;
using OpenCqrs.Services;

namespace OpenCqrs.Tests
{
    [TestFixture]
    public class EventPublisherTests
    {
        [Test]
        public void Should_throw_exception_when_event_is_null()
        {
            var serviceProvider = new Mock<IServiceProviderWrapper>();

            var sut = new EventPublisher(serviceProvider.Object);

            Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.Publish<SampleEvent>(null));
        }

        [Test]
        public async Task Should_publish_event()
        {
            var sampleEvent = new SampleEvent();

            var eventHandler1 = new Mock<IEventHandler<SampleEvent>>();
            eventHandler1 .Setup(x => x.Handle(sampleEvent));

            var eventHandler2 = new Mock<IEventHandler<SampleEvent>>();
            eventHandler2 .Setup(x => x.Handle(sampleEvent));

            var serviceProvider = new Mock<IServiceProviderWrapper>();
            serviceProvider
                .Setup(x => x.GetServices<IEventHandler<SampleEvent>>())
                .Returns(new List<IEventHandler<SampleEvent>> { eventHandler1.Object, eventHandler2.Object });

            var sut = new EventPublisher(serviceProvider.Object);

            await sut.Publish(sampleEvent);

            eventHandler1.Verify(x => x.Handle(sampleEvent), Times.Once);
            eventHandler2.Verify(x => x.Handle(sampleEvent), Times.Once);
        }
    }
}