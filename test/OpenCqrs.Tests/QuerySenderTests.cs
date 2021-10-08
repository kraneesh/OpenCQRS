using Moq;
using NUnit.Framework;
using OpenCqrs.Queries;
using System;
using System.Threading.Tasks;

namespace OpenCqrs.Tests
{
    public class QuerySenderTests
    {
        [Test]
        public void Should_throw_argument_null_exception_when_sending_null_query()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var sut = new QuerySender(serviceProvider.Object);
            Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.Send<SampleQuery>(null));
        }

        [Test]
        public void Should_throw_exception_when_handler_not_found()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var sut = new QuerySender(serviceProvider.Object);
            Assert.ThrowsAsync<Exception>(async () => await sut.Send(new SampleQuery()));
        }

        [Test]
        public async Task Should_handle_query()
        {
            var query = new SampleQuery();
            var result = new SampleResult();

            var handler = new Mock<IQueryHandler<SampleQuery, SampleResult>>();
            handler.Setup(x => x.Handle(query)).ReturnsAsync(result);

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(typeof(IQueryHandler<SampleQuery, SampleResult>))).Returns(handler.Object);

            var sut = new QuerySender(serviceProvider.Object);

            var actual = await sut.Send(query);

            Assert.AreEqual(result, actual);
        }
    }
}
