using System.Threading.Tasks;

namespace OpenCqrs.Commands
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
