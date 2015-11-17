using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using XXW.BLL;

namespace Dianda.CacheFactory
{
    public class MemCacheDSession : ISession
    {
        [Serializable]
        public class SessionModel
        {
            public SessionModel(string key, object obj)
            {
                this.Key = key;
                this.Value = obj;
            }
            public string Key { get; set; }
            public object Value { get; set; }
        }

        string cookieDomain;
        public MemCacheDSession()
        {
            SysParametersBLL g = new SysParametersBLL();
            cookieDomain = g.GetValue("Cache", "Domain").ToString();
        }

        public object GetObject(string sessionName)
        {
            //return CacheHelper.InstanceSession.Get(CacheCatalog.GetSession(getSessionId() + "_" + sessionName));

            List<SessionModel> list = getDict();
            foreach (SessionModel model in list)
            {
                if (model.Key == sessionName)
                {                  
                    return model.Value;
                }
            }
            setDict(list);
            return null;
        }

      

        public void SetObject(string key, object value)
        {
            //CacheHelper.InstanceSession.Set(CacheCatalog.GetSession(getSessionId() + "_" + key), value);
            //List<string> list = SessionList;
            //if (!list.Contains(key))
            //{
            //    list.Add(key);
            //}
            //SessionList = list;
            bool isFind = false;
            List<SessionModel> list = getDict();
            foreach (SessionModel model in list)
            {
                if (model.Key == key)
                {
                    model.Value = value;
                    isFind = true;
              
                }
            }
            if(!isFind )
             list.Add(new SessionModel (key,value));
            setDict(list);

        }

      
        public void Clear()
        {
           
                CacheHelper.InstanceSession.Remove(CacheCatalog.GetSession(getSessionId()));

                //foreach (string temp in SessionList)
                //{
                //    CacheHelper.InstanceSession.Remove(CacheCatalog.GetSession(getSessionId() + "_" + temp));
                //}
           


        }
        public void Remove(string key)
        {
            //CacheHelper.InstanceSession.Remove(CacheCatalog.GetSession(getSessionId() + "_" + key));
            //List<string> list = SessionList;
            //if (list.Contains(key))
            //{
            //    list.Remove(key);
            //}
            //SessionList = list;
           
            //Dictionary<string, object> dict = getDict();
            int index=-1;
            List<SessionModel> list = getDict();
            for (int i=0;i<list.Count ;i++)
            {
                if (list[i].Key == key)
                {
                    index = i;
                }
            }
            if (index >= 0&&index<list.Count )
            {
                list.RemoveAt(index);
            }
            setDict(list);
            //if (dict == null)
            //{
            //    return;
            //}
            //else
            //{
            //    if (dict.ContainsKey(key))
            //    {
            //        dict.Remove(key);
            //        setDict(dict);
            //    }

            //}
        }
        const string CONST_SESSION_ID = "NLTSKeyId";
        private string getSessionId()
        {
            if (HttpContext.Current.Request.Cookies[CONST_SESSION_ID] != null && HttpContext.Current.Request.Cookies[CONST_SESSION_ID].Value.Trim()!=string.Empty)
            {
                return HttpContext.Current.Request.Cookies[CONST_SESSION_ID].Value;
            }
            else
            {
                              return setSessionId();
                 
            }

        }
        public string GetSessionKey()
        {
            return getSessionId();
        }
        private string setSessionId()
        {
            string sessionId = genSessionId();
            HttpCookie ck = new HttpCookie(CONST_SESSION_ID, sessionId);
            ck.Expires = DateTime.Now.AddDays(1);
            if (cookieDomain != "localhost")
            {
                ck.Domain = cookieDomain;
                ck.Path = "/";
            }
            HttpContext.Current.Response.Cookies.Add(ck);
            return sessionId;
        }

        private string genSessionId()
        {
            return DateTime.Now.ToString ("yyyyMMddhhmmssffff") + new Random().Next(100, 999).ToString();
        }



        //public List<string> SessionList
        //{
        //    get {
        //        List<string> result;
        //        GetObject<List<string>>("SessionList", out result);
        //        if (result!=null)
        //        {
        //            return result;
        //        }
        //        else
        //        {
        //            result = new List<string>();
        //            CacheHelper.InstanceSession.Set(CacheCatalog.GetSession(getSessionId() + "_SessionList"), result);
        //            return result;
        //        }
                  
        //    }
        //    set
        //    {
        //        CacheHelper.InstanceSession.Set(CacheCatalog.GetSession(getSessionId() + "_SessionList"), value);
             
        //    }
        //}
        private List<SessionModel> getDict()
        {

            string sessionId = getSessionId();
            if (string.IsNullOrEmpty(sessionId))
            {
                return null;
            }
            object obj = CacheHelper.InstanceSession.Get(CacheCatalog.GetSession(sessionId));
            List<SessionModel> result = obj as List<SessionModel>;
            if (result == null)
            {
                result = new List<SessionModel>();
                //CacheHelper.InstanceSession.Remove(CacheCatalog.GetSession(sessionId));
                CacheHelper.InstanceSession.Set(CacheCatalog.GetSession(sessionId), result);
            }
            return result;
        }

        private void setDict(List<SessionModel> dict)
        {
            string sessionId = getSessionId();
            if (string.IsNullOrEmpty(sessionId))
            {
                sessionId = setSessionId();

            }
            //CacheHelper.InstanceSession.Remove(CacheCatalog.GetSession(sessionId));
            CacheHelper.InstanceSession.Set(CacheCatalog.GetSession(sessionId), dict);
            //object obj = CacheHelper.InstanceSession.Get(CacheCatalog.GetSession(sessionId));
        }
    }
}
