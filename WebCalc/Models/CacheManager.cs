using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCalc.Interfaces;

namespace WebCalc.Models
{
    public class CacheManager : ICacheManager
    {

        private readonly IConfiguration _config;
        private readonly string _expiredInMunute;
        private DateTime dateTime; 

        private Dictionary<string, object> dic = new Dictionary<string, object>();

        public CacheManager(IConfiguration config)
        {
            _config = config;
            _expiredInMunute = _config.GetSection("cache:expiredInMinute").Value;
        }

        public T Get<T>(string key , Func<T> func )
        {
            if (!dic.ContainsKey(key))
            {
                Insert<T>(key, func());
               
            }
             
            return (T)dic[key];
        }


        public void Insert<T>(string key, T obj)
        {
            if (!dic.ContainsKey(key))
            {
                dic[key] = obj;
            }
        }
    }
}
