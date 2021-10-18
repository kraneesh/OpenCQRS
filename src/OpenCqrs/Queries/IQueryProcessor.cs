using System.Threading.Tasks;

namespace OpenCqrs.Queries
{
    public interface IQueryProcessor
    {
        Task<TResult> Process<TResult>(IQuery<TResult> query);
    }
}
