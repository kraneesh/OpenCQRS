using System.Threading.Tasks;

namespace OpenCqrs.Commands
{
    public interface ICommandSender
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
    }
}
