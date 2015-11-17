using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.Common
{
   public  class ConstantHelper
    {
       /// <summary>
        /// yyyy-MM-dd
       /// </summary>
       public static string DATE_FORMAT
       {
           get { return "yyyy-MM-dd"; }
       }
       /// <summary>
       /// yyyy-MM-dd HH:mm
       /// </summary>
       public static string DATE_FORMAT_WITH_HHMM
       {
           get { return "yyyy-MM-dd HH:mm"; }
       }
       /// <summary>
       /// 特殊符号，用于同一个字段内要存放多个内容的分隔符，第一层
       /// </summary>
       public const char CONST_SPECIAL_SEPARATOR_A = 'δ';
       /// <summary>
       /// 特殊符号，用于同一个字段内要存放多个内容的分隔符，第二层
       /// </summary>
       public const char CONST_SPECIAL_SEPARATOR_B = '┬';
       /// <summary>
       /// 特殊符号，用于同一个字段内要存放多个内容的分隔符，第三层
       /// </summary>
       public const char CONST_SPECIAL_SEPARATOR_C = 'Γ';

       public const int CONST_PAGE_SIZE = 10;


       public static string GetDefaultImg(XXW.Enum.DefaultImg pic)
       {
           return "/file/Images/Default/"+pic.ToString()+".jpg";
       }
    }
}
