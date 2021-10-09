using Moq;
using NUnit.Framework;
using OpenCqrs.Commands;
using OpenCqrs.Queries;
using System.Threading.Tasks;

namespace OpenCqrs.Tests
{
    public class SenderTests
    {
        [Test]
        public async Task Should_send_command()
        {
            var command = new SampleCommand();

            var commandSender = new Mock<ICommandSender>();
            commandSender.Setup(x => x.Send(command)).Returns(Task.CompletedTask);

            var querySender = new Mock<IQuerySender>();

            var sut = new Sender(commandSender.Object, querySender.Object);

            await sut.Send(command);

            commandSender.Verify(x => x.Send(command), Times.Once);
        }

        [Test]
        public async Task Should_send_query()
        {
            var query = new SampleQuery();
            var result = new SampleResult();

            var commandSender = new Mock<ICommandSender>();

            var querySender = new Mock<IQuerySender>();
            querySender.Setup(x => x.Send(query)).ReturnsAsync(result);

            var sut = new Sender(commandSender.Object, querySender.Object);

            var actual = await sut.Send(query);

            querySender.Verify(x => x.Send(query), Times.Once);
            Assert.AreEqual(result, actual);
        }
    }
}
