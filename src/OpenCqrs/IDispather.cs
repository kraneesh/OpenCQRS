using OpenCqrs.Commands;
using OpenCqrs.Events;
using OpenCqrs.Queries;
using System.Threading.Tasks;

namespace OpenCqrs
{
    public interface IDispather
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> Get<TResult>(IQuery<TResult> query);
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
