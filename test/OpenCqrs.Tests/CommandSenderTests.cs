using Moq;
using NUnit.Framework;
using OpenCqrs.Commands;
using System;
using System.Threading.Tasks;

namespace OpenCqrs.Tests
{
    public class CommandSenderTests
    {
        [Test]
        public void Should_throw_argument_null_exception_when_sending_null_command()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var sut = new CommandSender(serviceProvider.Object);
            Assert.ThrowsAsync<ArgumentNullException>(async () => await sut.Send<SampleCommand>(null));
        }

        [Test]
        public void Should_throw_exception_when_handler_not_found()
        {
            var serviceProvider = new Mock<IServiceProvider>();
            var sut = new CommandSender(serviceProvider.Object);
            Assert.ThrowsAsync<Exception>(async () => await sut.Send(new SampleCommand()));
        }

        [Test]
        public async Task Should_handle_command()
        {
            var command = new SampleCommand();

            var handler = new Mock<ICommandHandler<SampleCommand>>();
            handler.Setup(x => x.Handle(command)).Returns(Task.CompletedTask);

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(typeof(ICommandHandler<SampleCommand>))).Returns(handler.Object);

            var sut = new CommandSender(serviceProvider.Object);

            await sut.Send(command);

            handler.Verify(x => x.Handle(command), Times.Once);
        }
    }
}
