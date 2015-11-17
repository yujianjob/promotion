using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.Common
{
    public class QueryString
    {
        public static string KEY
        {
            get
            {
                return "12345678";
            }
        }


        public static string Encrypt(object value)
        {
            return StringSecurity.DES.Encrypt(value.ToString(), KEY);
        }

        /// <summary>
        /// url参数解码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string UrlEncrypt(object value)
        {
            return Utils.UrlEncode(Encrypt(value.ToString()));
        }

        /// <summary>
        /// url解码
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string Decrypt(object value)
        {
            return StringSecurity.DES.Decrypt(value.ToString(), KEY);
        }


        public static string CreateUrlParams(Dictionary<string, string> Params)
        {
            List<string> _param = new List<string>();

            foreach (var item in Params)
            {
                _param.Add(item.Key + ConstantHelper.CONST_SPECIAL_SEPARATOR_B + item.Value);

            }
            return UrlEncrypt(string.Join(ConstantHelper.CONST_SPECIAL_SEPARATOR_A.ToString(), _param.ToArray()));
        }

        public static Dictionary<string, string> GetUrlParams(string value)
        {
            Dictionary<string, string> Params = new Dictionary<string, string>();
            string[] urlparams = Decrypt(value).Split(ConstantHelper.CONST_SPECIAL_SEPARATOR_A);
            foreach (string _prarm in urlparams)
            {
                string[] pra = _prarm.Split(ConstantHelper.CONST_SPECIAL_SEPARATOR_B);
                if (pra.Length > 1)
                {
                    Params.Add(_prarm.Split(ConstantHelper.CONST_SPECIAL_SEPARATOR_B)[0], _prarm.Split(ConstantHelper.CONST_SPECIAL_SEPARATOR_B)[1]);
                }
            }
            return Params;
        }

    }
}
