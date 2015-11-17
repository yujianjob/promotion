using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XXW.BLL;

namespace Dianda.CacheFactory
{
   public  class CacheHelper
    {
        public int ExpirateTime { get; set; }



        private static CacheHelper me;
        private static CacheHelper sessionMe;
        static CacheHelper()
        {
            SysParametersBLL g = new SysParametersBLL();
                me = new CacheHelper("enyim.com/memcached", g.GetCacheExpiredTime ());
                sessionMe = new CacheHelper("enyim.com/memcached_session", g.GetSessionExpiredTime());
          
        }
        public static CacheHelper Instance
        {
            get { return me; }
            set { me = value; }
        }
        public static CacheHelper InstanceSession
        {
            get { return sessionMe; }
            set { sessionMe = value; }
        }

        ICache ic;
        //Serialize.SerializeHelper sh = new Serialize.SerializeHelper();
        public CacheHelper(string domin, int expiresTime)
        {
            ic = new MemCacheD(domin);

            ExpirateTime = expiresTime;
        }

        public bool Add(string key, object obj)
        {
            if (ExpirateTime < 0)
            {
                return ic.Add(key, obj);
            }
            else
            {
                return ic.Add(key, obj, new TimeSpan (0,ExpirateTime,0) );
            }
        }
        public bool Add(string key, object obj, DateTime expiresAt)
        {
            return ic.Add(key, obj, expiresAt);
        }
        public bool Add(string key, object obj, TimeSpan validFor)
        {
            return ic.Add(key, obj, validFor);
        }
        public bool Set(string key, object obj)
        {
            if (ExpirateTime < 0)
            {
                return ic.Set(key, obj);
            }
            else
            {
                return ic.Set(key, obj, new TimeSpan(0, ExpirateTime, 0));
            }
        }
        public bool Set(string key, object obj, DateTime expiresAt)
        {
            return ic.Set(key, obj, expiresAt);
        }
        public bool Set(string key, object obj, TimeSpan validFor)
        {
            return ic.Set(key, obj, validFor);
        }
        public object Get(string key)
        {
            return ic.Get(key);
        }
        public T Get<T>(string key)
        {
            return ic.Get<T>(key);

        }
        public bool Remove(string key)
        {
            return ic.Remove(key);

        }
        public bool TryGet(string key, out object obj)
        {
            return ic.TryGet(key, out obj);
        }
        public bool Lock(string key)
        {
            return ic.Lock(key);
        }
        public bool UnLock(string key)
        {
            return ic.UnLock(key);
        }
        public bool IsLock(string key)
        {
            return ic.IsLock(key);
        }

    }
}
