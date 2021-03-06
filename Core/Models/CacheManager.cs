﻿
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interfaces;

namespace Core.Models
{
    public class CacheManager : ICacheManager
    {

        private readonly IConfiguration _config;

        private int _expiredInMinute => int.Parse(_config.GetSection("cache:expiredInMinute").Value);
        private DateTime expressionDate;
        private Dictionary<string, object> dic = new Dictionary<string, object>();

        public CacheManager(IConfiguration config)
        {
            _config = config;
        }

        public T Get<T>(string key, Func<T> func)
        {
            if (!dic.ContainsKey(key))
            {
                expressionDate = DateTime.Now.AddMinutes(_expiredInMinute);
                Insert<T>(key, func());
            }
            else
            {
                if ((expressionDate - DateTime.Now).TotalMinutes <=0 )
                {
                    expressionDate = DateTime.Now.AddMinutes(_expiredInMinute);
                    dic[key] = func();
                }
            }

            return (T)dic[key];
        }


        public void Insert<T>(string key, T obj)   
        {
            if (!dic.ContainsKey(key))
                dic[key] = obj;           
        }        
    }
}
