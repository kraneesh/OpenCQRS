using System.Threading.Tasks;

namespace OpenCqrs.Events
{
    public interface IEventHandler<in TEvent> where TEvent : IEvent
    {
        Task Handle(TEvent @event);
    }
}
