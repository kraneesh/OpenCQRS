using System.Threading.Tasks;
using OpenCqrs.Services;

namespace OpenCqrs.Queries
{
    internal abstract class QueryHandlerWrapperBase<TResult>
    {
        protected static THandler GetHandler<THandler>(IServiceProviderWrapper serviceProvider)
        {
            return serviceProvider.GetService<THandler>();
        }

        public abstract Task<TResult> Handle(IQuery<TResult> query, IServiceProviderWrapper serviceProvider);
    }
}
