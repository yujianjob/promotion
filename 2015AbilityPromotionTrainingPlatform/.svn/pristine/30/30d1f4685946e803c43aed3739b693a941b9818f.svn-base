using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianda.CacheFactory
{
    public interface ICache
    {

        bool Add(string key, object value);
        bool Add(string key, object value, DateTime expiresAt);
        bool Add(string key, object value, TimeSpan validFor);
        bool Set(string key, object value);
        bool Set(string key, object value, DateTime expiresAt);
        bool Set(string key, object value, TimeSpan validFor);
        bool Replace(string key, object obj);
        bool Replace(string key, object obj, DateTime expiresAt);
        bool Replace(string key, object obj, TimeSpan validFor);
        object Get(string key);
        T Get<T>(string key);
        bool TryGet(string key, out object value);
        bool Remove(string key);
        bool Lock(string key);
        bool UnLock(string key);
        bool IsLock(string key);
    }
}
