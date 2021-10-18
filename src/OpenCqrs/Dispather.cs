using OpenCqrs.Commands;
using OpenCqrs.Events;
using OpenCqrs.Queries;
using System.Threading.Tasks;

namespace OpenCqrs
{
    public class Dispatcher : IDispatcher
    {
        private readonly ICommandSender _commandSender;
        private readonly IQueryProcessor _queryProcessor;
        private readonly IEventPublisher _eventPublisher;

        public Dispatcher(ICommandSender commandSender, IQueryProcessor queryProcessor, IEventPublisher eventPublisher)
        {
            _commandSender = commandSender;
            _queryProcessor = queryProcessor;
            _eventPublisher = eventPublisher;
        }

        public async Task Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            await _commandSender.Send(command);
        }

        public async Task<TResult> Get<TResult>(IQuery<TResult> query)
        {
            return await _queryProcessor.Process(query);
        }

        public async Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
        {
            await _eventPublisher.Publish(@event);
        }
    }
}
