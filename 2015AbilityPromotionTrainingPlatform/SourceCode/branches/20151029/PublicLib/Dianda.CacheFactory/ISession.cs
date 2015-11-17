using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianda.CacheFactory
{
   public  interface ISession
    {
      
        object GetObject(string key);
        void SetObject(string key, object value);
        void Clear();
        void Remove(string key);
        string GetSessionKey();
        
       
    }
}
