using Dianda.AP.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;

namespace Web.Areas.Api.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Api/Test/

        public ActionResult Index()
        {
            CBK_SHERC_USERINFO model = new CBK_SHERC_USERINFO();
            model.ADDRESS = "";
            model.BIRTHDAY = DateTime.Now.ToString();
            model.DELFLAG = 0;
            model.EMAIL = "sad@dmkf.com";
            model.ENAME = "sad3k";
            model.ID = 334455;
            model.IDCARD = "32058119801238123X";
            model.IMAGES = "";
            model.ISNEWS = 1;
            model.LOGINNUMS = 0;
            model.LOGINTIMES = 0;
            model.ModifyDatetime = DateTime.Now;
            model.NATIONALID = 56;
            model.NIANJIID = "1";
            model.OLDID = "0";
            model.POLITICSID = 1;
            model.POST = "112233";
            model.PWD = "123456";
            model.QUXIANID = 14;
            model.REALNAME = "王尼玛";
            model.SCHOOL_TYPEID = 2;
            model.SCHOOLAGES = 10;
            model.SCHOOLID = 233;
            model.SchoolTag = "";
            model.SEX = "男";
            model.STATUS = "正常";
            model.TEACHNUMBER = "9638003823";
            model.TELE = "23093@03453";
            model.TIMESOFGRADUATE = DateTime.Now;
            model.TrainingObjectID = 1;
            model.TrainingTypeID = "1";
            model.USERGroupsID = "1";
            model.USERJOBID = "1";
            model.USERLEVES = "1";
            model.Usersessionid = "";
            model.WorkTerm = "1";
            model.XUEDUANID = "1";
            model.XUEKEID = "1";
            model.XUELIID = 1;
            model.XUEWEIID = 1;
            model.XUQUID = 1;
            model.ZHICHENGID = "1";
            model.ZYDM = 1;
            return View();
        }


        private byte[] SerializeObject(object obj)
        {
            byte[] rst;
            MemoryStream memoryStream = new MemoryStream();
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(memoryStream, obj);
            rst = memoryStream.GetBuffer();
            memoryStream.Dispose();
            memoryStream.Close();
            return rst;
        }

        private object DeserializeObject(byte[] bytes)
        {
            BinaryFormatter binaryFormatter = new BinaryFormatter();
            //binaryFormatter.Binder = new UBinder();
            return binaryFormatter.Deserialize(new MemoryStream(bytes));
        }

        private class UBinder : SerializationBinder
        {
            public override Type BindToType(string assemblyName, string typeName)
            {
                if (typeName.EndsWith("CBK_SHERC_USERINFO"))
                    return typeof(Dianda.AP.Model.CBK_SHERC_USERINFO);

                if (typeName.EndsWith("schoolcode"))
                    return typeof(Dianda.AP.Model.schoolcode);

                if (typeName.EndsWith("SchoolManageTypeEnum"))
                    return typeof(Web.API.SchoolManageTypeEnum);

                if (typeName.EndsWith("TempClass"))
                    return typeof(Dianda.AP.Model.TempClass);

                Assembly ass = Assembly.GetExecutingAssembly();
                return ass.GetType(typeName);
            }

        }
    }
}
