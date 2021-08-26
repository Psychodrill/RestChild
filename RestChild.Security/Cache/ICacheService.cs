using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestChild.Security.Cache
{
    /// <summary>
    /// интерфейс хранения в кэше
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// достать из кэша
        /// </summary>
        T Get<T>(string cacheKey) where T : class;

        /// <summary>
        /// положить в кэш
        /// </summary>
        void Set<T>(string cacheKey, T data) where T : class;
    }
}
