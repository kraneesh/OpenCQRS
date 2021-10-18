using System.Threading.Tasks;

namespace OpenCqrs.Events
{
    public interface IEventPublisher
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
