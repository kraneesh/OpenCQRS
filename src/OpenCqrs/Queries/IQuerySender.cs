using System.Threading.Tasks;

namespace OpenCqrs.Queries
{
    public interface IQuerySender
    {
        Task<TResult> Send<TResult>(IQuery<TResult> query);
    }
}
