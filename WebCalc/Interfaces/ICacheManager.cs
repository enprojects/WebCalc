using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCalc.Interfaces
{
    public interface ICacheManager
    {
        void Insert<T>(string key, T obj);
        T Get<T>(string key, Func<T> func);
    }
}
