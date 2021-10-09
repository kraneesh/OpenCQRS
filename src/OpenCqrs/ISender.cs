using OpenCqrs.Commands;
using OpenCqrs.Queries;
using System.Threading.Tasks;

namespace OpenCqrs
{
    public interface ISender
    {
        Task Send<TCommand>(TCommand command) where TCommand : ICommand;
        Task<TResult> Send<TResult>(IQuery<TResult> query);
    }
}
