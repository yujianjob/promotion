using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Web.Code
{
    public static class ExtendHelper
    {
        #region int类型转换的方法
        /// <summary>
        /// int 类型转换的方法
        /// </summary>
        /// <param name="value">string 值</param>
        /// <returns>返回转换后的值</returns>
        public static int ToInt(this object value)
        {
            int result = 0;

            if (value != null)
            {
                return int.TryParse(value.ToString(), out result) ? result : 0;
            }

            return result;
        }
        #endregion
        #region int类型转换的方法
        /// <summary>
        /// int 类型转换的方法
        /// </summary>
        /// <param name="value">string 值</param>
        /// <returns>返回转换后的值</returns>
        public static double ToDouble(this object value)
        {
            double result = 0;

            if (value != null)
            {
                return double.TryParse(value.ToString(), out result) ? result : 0;
            }

            return result;
        }
        #endregion

        #region datetime类型转换的方法
        /// <summary>
        /// datetime 类型转换的方法
        /// </summary>
        /// <param name="id">string 值</param>
        /// <returns>返回转换后的值</returns>
        public static DateTime ToDateTime(this string value)
        {
            DateTime result = DateTime.Now;

            if (!string.IsNullOrEmpty(value))
            {
                return DateTime.TryParse(value, out result) ? result : DateTime.Now;
            }

            return result;
        }
        #endregion

        public static string Split(string inputString) //防止SQL注入方法
        {
            inputString = inputString.Trim();
            inputString = inputString.Replace("'", "");
            inputString = inputString.Replace(";--", "");
            inputString = inputString.Replace("--", "");
            inputString = inputString.Replace("=", "");
            //and|exec|insert|select|delete|update|chr|mid|master|or|truncate|char|declare|join|count|*|%|union 等待关键字过滤
            //不要忘记为你的用户名框，密码框设定 允许输入的最多字符长度 maxlength的值哦，这样他们就无法编写太长的东西来再次拼成第一次过滤掉的关键字 如 oorr一次replace过滤后又成了 or 喔。
            inputString = inputString.Replace("and", "");
            inputString = inputString.Replace("exec", "");
            inputString = inputString.Replace("insert", "");
            inputString = inputString.Replace("select", "");
            inputString = inputString.Replace("delete", "");
            inputString = inputString.Replace("update", "");
            inputString = inputString.Replace("chr", "");
            inputString = inputString.Replace("mid", "");
            inputString = inputString.Replace("master", "");
            inputString = inputString.Replace("or", "");
            inputString = inputString.Replace("truncate", "");
            inputString = inputString.Replace("char", "");
            inputString = inputString.Replace("declare", "");
            inputString = inputString.Replace("join", "");
            inputString = inputString.Replace("count", "");
            inputString = inputString.Replace("*", "");
            inputString = inputString.Replace("%", "");
            inputString = inputString.Replace("union", "");
            return inputString;
        }
    }

    public static class DataTableToListHelper<T> where T : new()
    {
        public static IList<T> ConvertToModel(DataTable dt)
        {
            // 定义集合   
            IList<T> ts = new List<T>();

            // 获得此模型的类型  
            Type type = typeof(T);
            string tempName = "";

            foreach (DataRow dr in dt.Rows)
            {
                T t = new T();
                // 获得此模型的公共属性     
                PropertyInfo[] propertys = t.GetType().GetProperties();
                foreach (PropertyInfo pi in propertys)
                {
                    tempName = pi.Name;  // 检查DataTable是否包含此列   

                    if (dt.Columns.Contains(tempName))
                    {
                        // 判断此属性是否有Setter     
                        if (!pi.CanWrite) continue;

                        object value = dr[tempName];
                        if (value != DBNull.Value)
                            pi.SetValue(t, value, null);
                    }
                }
                ts.Add(t);
            }
            return ts;
        }

        /// <summary>
        /// Convert a List{T} to a DataTable.
        /// </summary>
        public static DataTable ToDataTable<T>(List<T> items)
        {
            var tb = new DataTable(typeof(T).Name);

            PropertyInfo[] props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (PropertyInfo prop in props)
            {
                Type t = GetCoreType(prop.PropertyType);
                tb.Columns.Add(prop.Name, t);
            }

            foreach (T item in items)
            {
                var values = new object[props.Length];

                for (int i = 0; i < props.Length; i++)
                {
                    values[i] = props[i].GetValue(item, null);
                }

                tb.Rows.Add(values);
            }

            return tb;
        }

        /// <summary>
        /// Determine of specified type is nullable
        /// </summary>
        public static bool IsNullable(Type t)
        {
            return !t.IsValueType || (t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>));
        }

        /// <summary>
        /// Return underlying type if type is Nullable otherwise return the type
        /// </summary>
        public static Type GetCoreType(Type t)
        {
            if (t != null && IsNullable(t))
            {
                if (!t.IsValueType)
                {
                    return t;
                }
                else
                {
                    return Nullable.GetUnderlyingType(t);
                }
            }
            else
            {
                return t;
            }
        }
    }   

        
}