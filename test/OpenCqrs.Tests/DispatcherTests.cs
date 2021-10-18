using Moq;
using NUnit.Framework;
using OpenCqrs.Commands;
using OpenCqrs.Events;
using OpenCqrs.Queries;
using System.Threading.Tasks;

namespace OpenCqrs.Tests
{
    public class DispatcherTests
    {
        [Test]
        public async Task Should_send_command()
        {
            var command = new SampleCommand();

            var commandSender = new Mock<ICommandSender>();
            commandSender.Setup(x => x.Send(command)).Returns(Task.CompletedTask);

            var queryProcessor = new Mock<IQueryProcessor>();

            var eventPublisher = new Mock<IEventPublisher>();

            var sut = new Dispather(commandSender.Object, queryProcessor.Object, eventPublisher.Object);

            await sut.Send(command);

            commandSender.Verify(x => x.Send(command), Times.Once);
        }

        [Test]
        public async Task Should_get_result()
        {
            var query = new SampleQuery();
            var result = new SampleResult();

            var commandSender = new Mock<ICommandSender>();

            var queryProcessor = new Mock<IQueryProcessor>();
            queryProcessor.Setup(x => x.Process(query)).ReturnsAsync(result);

            var eventPublisher = new Mock<IEventPublisher>();

            var sut = new Dispather(commandSender.Object, queryProcessor.Object, eventPublisher.Object);

            var actual = await sut.Get(query);

            queryProcessor.Verify(x => x.Process(query), Times.Once);
            Assert.AreEqual(result, actual);
        }

        [Test]
        public async Task Should_publish_event()
        {
            var @event = new SampleEvent();

            var commandSender = new Mock<ICommandSender>();

            var queryProcessor = new Mock<IQueryProcessor>();

            var eventPublisher = new Mock<IEventPublisher>();
            eventPublisher.Setup(x => x.Publish(@event)).Returns(Task.CompletedTask);

            var sut = new Dispather(commandSender.Object, queryProcessor.Object, eventPublisher.Object);

            await sut.Publish(@event);

            eventPublisher.Verify(x => x.Publish(@event), Times.Once);
        }
    }
}
