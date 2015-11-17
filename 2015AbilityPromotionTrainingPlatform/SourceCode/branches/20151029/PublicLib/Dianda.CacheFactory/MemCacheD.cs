using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching;
namespace Dianda.CacheFactory
{
    public class MemCacheD : ICache
    {

        private MemcachedClient client;
        public MemCacheD(string setion)
        {
            client = new MemcachedClient(setion);
        
        }
      
        public bool Add(string key,object obj)
        {
            //return client.set(key, obj);
            return client.Store(Enyim.Caching.Memcached.StoreMode.Add, key, obj);
        
        }
        public bool Add(string key, object obj,DateTime expiresAt)
        {
            //return client.set(key, obj);
            return client.Store(Enyim.Caching.Memcached.StoreMode.Add, key, obj, expiresAt);
        }
        public bool Add(string key, object obj, TimeSpan  validFor)
        {
            //return client.set(key, obj);
            return client.Store(Enyim.Caching.Memcached.StoreMode.Add, key, obj,   validFor);
        }
        public bool Set(string key, object obj)
        {
            return client.Store(Enyim.Caching.Memcached.StoreMode.Set, key, obj);
        }
        public bool Set(string key, object obj, DateTime expiresAt)
        {         
            return client.Store(Enyim.Caching.Memcached.StoreMode.Set, key, obj, expiresAt);
        }
        public bool Set(string key, object obj, TimeSpan validFor)
        {
            return client.Store(Enyim.Caching.Memcached.StoreMode.Set, key, obj, validFor);
        }
        public bool Replace(string key, object obj)
        {
            return client.Store(Enyim.Caching.Memcached.StoreMode.Replace, key, obj);
        }
        public bool Replace(string key, object obj, DateTime expiresAt)
        {
            return client.Store(Enyim.Caching.Memcached.StoreMode.Replace, key, obj, expiresAt);
        }
        public bool Replace(string key, object obj, TimeSpan validFor)
        {
            return client.Store(Enyim.Caching.Memcached.StoreMode.Replace, key, obj, validFor);
        }

        public object Get(string key)
        {
            //while (IsLock(key))
            //{
            //    System.Threading.Thread.Sleep(CONST_LOCK_WAIT_TIME);
            //}
            return client.Get(key);
        }
        public T Get<T>(string key)
        {
            //while (IsLock(key))
            //{
            //    System.Threading.Thread.Sleep(CONST_LOCK_WAIT_TIME);
            //}
            object obj = client.Get(key);
            if (obj != null && obj is T)
            {
                return (T)obj;
            }
            else
                return default(T);
        }
        public bool Remove(string key)
        {
            return client.Remove(key);
           
        }
        public bool TryGet(string key, out object obj)
        {
            return client.TryGet(key, out obj);
        }

        /// <summary>
        /// 如果资源被锁定，则默认等待多少毫秒。
        /// </summary>
        const int CONST_LOCK_WAIT_TIME = 200;

        /// <summary>
        /// 锁定资源的过期时间(秒)
        /// </summary>
        const int CONST_LOCK_EXPIRES_TIME = 300;

        /// <summary>
        /// 锁定资源的key形式
        /// </summary>
        const string CONST_LOCK_FORMAT = "Lock({0})";


        public bool Lock(string key)
        {
            return Add(string.Format(CONST_LOCK_FORMAT, key), "1", DateTime.Now.AddSeconds(CONST_LOCK_EXPIRES_TIME));
        }

        public bool UnLock(string key)
        {
            object obj;
            if (client.TryGet(string.Format(CONST_LOCK_FORMAT, key), out obj))
            {
                return client.Remove(string.Format(CONST_LOCK_FORMAT, key));
            }
            else
            {
                return true;
            }
        }

        public bool IsLock(string key)
        {
            object obj;
            return client.TryGet(string.Format(CONST_LOCK_FORMAT, key), out obj);
        }
    }
}
