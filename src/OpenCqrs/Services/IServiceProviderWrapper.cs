using System.Collections.Generic;

namespace OpenCqrs.Services
{
    public interface IServiceProviderWrapper
    {
        T GetService<T>();
        IEnumerable<T> GetServices<T>();
    }
}
