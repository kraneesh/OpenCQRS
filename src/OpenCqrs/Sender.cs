using OpenCqrs.Commands;
using OpenCqrs.Queries;
using System.Threading.Tasks;

namespace OpenCqrs
{
    public class Sender : ISender
    {
        private readonly ICommandSender _commandSender;
        private readonly IQuerySender _querySender;

        public Sender(ICommandSender commandSender, IQuerySender querySender)
        {
            _commandSender = commandSender;
            _querySender = querySender;
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            await _commandSender.Send(command);
        }

        public async Task<TResult> Send<TResult>(IQuery<TResult> query)
        {
            return await _querySender.Send(query);
        }
    }
}
