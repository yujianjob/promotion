using Dianda.AP.BLL;
using Dianda.AP.Model;
using Dianda.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Web.Attributes;

namespace Web.Areas.Statistics.Controllers
{
    public class StatisticsController : Controller
    {
        //
        // GET: /Statistics/Statistics/
        [ValidateInput(false)]
        [UrlDecrypt]
        public ActionResult StatisticsListView(int? RegionId, int? OrganId, int? IsTest, string SearchTxt)
        {
            int PageIndex = 1;
            ViewBag.GroupId = Code.SiteCache.Instance.GroupId;//当前登录人的权限
            ViewBag.ManageOrganId = Code.SiteCache.Instance.ManageOrganId;//当前登录人的权限
            ViewBag.DetailUrl = Code.SiteCache.Instance.GetSysValue("NLPCView");//评测统计详细页链接
            IsTest = (IsTest == null) ? -1 : IsTest;
            ViewBag.IsTest = (IsTest == null) ? -1 : IsTest;
            ViewBag.SearchTxt = SearchTxt;
            
            //加载区县信息
            var List_Regin = this.GetReginListItem();

            var accBll = new accountBLL();
            var List_accountOther = new List<accountOther>();
            var stbSqlWhere = new StringBuilder();

            stbSqlWhere.Append(" 1 = 1 ");

            switch ((int)ViewBag.GroupId)
            {
                case (int)XXW.Enum.PlatformGroupEnum.ManagerCity:
                    {
                        ViewBag.RegionId = (RegionId == null) ? -1 : RegionId;
                        ViewBag.OrganId = (OrganId == null) ? -1 : OrganId;

                        if (ViewBag.RegionId > 0)
                            stbSqlWhere.Append(" and RegionId=" + ViewBag.RegionId);
                        if (ViewBag.OrganId > 0)
                            stbSqlWhere.Append(" and OrganId=" + ViewBag.OrganId);
                        break;
                    }
                case (int)XXW.Enum.PlatformGroupEnum.ManagerArea:
                    {
                        ViewBag.RegionId = (RegionId == null) ? (Code.SiteCache.Instance.RegionId): RegionId;
                        ViewBag.OrganId = (OrganId == null) ?  -1: OrganId;

                        stbSqlWhere.Append(" and RegionId=" + ViewBag.RegionId);
                        if (ViewBag.OrganId > 0)
                        stbSqlWhere.Append(" and OrganId=" + ViewBag.OrganId);
                        
                        break;
                    }
                case (int)XXW.Enum.PlatformGroupEnum.ManagerTraining:
                    {
                        ViewBag.RegionId = (RegionId == null) ? -1 : RegionId;
                        ViewBag.OrganId = (OrganId == null) ? (Code.SiteCache.Instance.ManageOrganId) : OrganId;

                        stbSqlWhere.Append(" and OrganId=" + ViewBag.OrganId);
                        break;
                    }
                case (int)XXW.Enum.PlatformGroupEnum.ManagerSchool:
                    {
                        ViewBag.RegionId = (RegionId == null) ? -1 : RegionId;
                        ViewBag.OrganId = (OrganId == null) ? (Code.SiteCache.Instance.ManageOrganId) : OrganId;

                        stbSqlWhere.Append(" and OrganId=" + ViewBag.OrganId);
                        break;
                    }
                default:
                    {
                        stbSqlWhere.Append(" and 1 != 1 ");
                        break;
                    }
            }
            
            if (IsTest != -1)
            {
                if (IsTest > 0)
                {
                    stbSqlWhere.Append(" and DiscussCnt > 0 ");
                }
                else
                {
                    stbSqlWhere.Append(" and DiscussCnt = 0 ");
                }
            }

            if (!string.IsNullOrEmpty(SearchTxt))
                stbSqlWhere.AppendFormat(" and (RealName like '%{0}%' or TeacherNo like '%{0}%')", SearchTxt);

            int iRecordCount = accBll.GetListOtherCount(stbSqlWhere.ToString());
            int iPageSize = 10, iPageIndex;
            int iPageCount = (int)Math.Ceiling((double)iRecordCount / iPageSize);
            int.TryParse(Request["PageIndex"], out PageIndex);
            int.TryParse(Convert.ToString(PageIndex), out iPageIndex);
            if (iPageIndex < 1)
                iPageIndex = 1;
            else if (iPageIndex > iPageCount)
                iPageIndex = iPageCount;
            //获取分页数据集合
            List_accountOther = accBll.GetListOther(iPageSize, iPageIndex, stbSqlWhere.ToString(), out iRecordCount);

            ViewBag.List_Regin = List_Regin;
            ViewBag.RecordCount = iRecordCount;
            ViewBag.PageCount = iPageCount;
            ViewBag.PageIndex = iPageIndex;
            ViewBag.PageSize = iPageSize;

            return View(List_accountOther);
        }

        [UrlDecrypt]
        public ActionResult Statistics(int? organId)
        {
            ViewBag.OrganId = organId;

            //if (organId == null)
            //    organId = Code.SiteCache.Instance.ManageOrganId;
            int groupId = Code.SiteCache.Instance.GroupId;

            //Organ_Detail organ = new Organ_DetailBLL().GetModel(Convert.ToInt32(organId));
            DataSet ds = null;
            //if (organ != null)
            //{
            var bll = new accountBLL();
            if (organId == null)
            {
                if (groupId == (int)XXW.Enum.PlatformGroupEnum.ManagerCity)//市级数据
                {
                    ViewBag.ParentId = 0;
                    ds = bll.Statistics();
                }
                else if (groupId == (int)XXW.Enum.PlatformGroupEnum.ManagerArea || groupId == (int)XXW.Enum.PlatformGroupEnum.ManagerTraining)//区级、培训机构数据
                {
                    ViewBag.ParentId = 1;
                    ds = bll.Statistics(Code.SiteCache.Instance.RegionId);//取区县（RegionId）
                }
                else if (groupId == (int)XXW.Enum.PlatformGroupEnum.ManagerSchool)//校级数据
                {
                    ViewBag.ParentId = 1;
                    ds = bll.StatisticsSub(Code.SiteCache.Instance.OrganId);
                }
            }
            else
            {
                ViewBag.ParentId = 1;
                ds = bll.Statistics(Convert.ToInt32(organId));
            }
            //}

            return View(ds);
        }

        //图表统计
        [UrlDecrypt]
        public ActionResult Chart(int? provId, string cityId, int? sexId, int? ageId, int? gradeId, int? subjectId,
            int? areaId, int? expId, int? mJobId, int? sJobId, int? titleId, int? eduId, int? idenId)
        {
            #region 初始化字段
            //省份
            provId = 25;
            Dictionary<string, string> prov = new Dictionary<string, string>();
            prov.Add(QueryString.UrlEncrypt(1), "安徽省");
            prov.Add(QueryString.UrlEncrypt(3), "北京市");
            prov.Add(QueryString.UrlEncrypt(4), "福建省");
            prov.Add(QueryString.UrlEncrypt(5), "甘肃省");
            prov.Add(QueryString.UrlEncrypt(6), "广东省");
            prov.Add(QueryString.UrlEncrypt(7), "广西壮族自治区");
            prov.Add(QueryString.UrlEncrypt(8), "贵州省");
            prov.Add(QueryString.UrlEncrypt(9), "海南省");
            prov.Add(QueryString.UrlEncrypt(10), "河北省");
            prov.Add(QueryString.UrlEncrypt(11), "河南省");
            prov.Add(QueryString.UrlEncrypt(12), "黑龙江省");
            prov.Add(QueryString.UrlEncrypt(13), "湖北省");
            prov.Add(QueryString.UrlEncrypt(14), "湖南省");
            prov.Add(QueryString.UrlEncrypt(15), "吉林省");
            prov.Add(QueryString.UrlEncrypt(16), "江苏省");
            prov.Add(QueryString.UrlEncrypt(17), "江西省");
            prov.Add(QueryString.UrlEncrypt(18), "辽宁省");
            prov.Add(QueryString.UrlEncrypt(19), "内蒙古自治区");
            prov.Add(QueryString.UrlEncrypt(20), "宁夏回族自治区");
            prov.Add(QueryString.UrlEncrypt(21), "青海省");
            prov.Add(QueryString.UrlEncrypt(22), "山东省");
            prov.Add(QueryString.UrlEncrypt(23), "山西省");
            prov.Add(QueryString.UrlEncrypt(24), "陕西省");
            prov.Add(QueryString.UrlEncrypt(25), "上海市");
            prov.Add(QueryString.UrlEncrypt(26), "四川省");
            prov.Add(QueryString.UrlEncrypt(27), "天津市");
            prov.Add(QueryString.UrlEncrypt(28), "西藏自治区");
            prov.Add(QueryString.UrlEncrypt(29), "新疆维吾尔自治区");
            prov.Add(QueryString.UrlEncrypt(30), "云南省");
            prov.Add(QueryString.UrlEncrypt(31), "浙江省");
            prov.Add(QueryString.UrlEncrypt(32), "重庆市");
            prov.Add(QueryString.UrlEncrypt(33), "新疆生产建设兵团");
            ViewBag.ProvData = new SelectList(prov, "key", "value", QueryString.UrlEncrypt(provId.ToString()));
            //地市
            Dictionary<string, string> city = new Dictionary<string, string>();
            city.Add(QueryString.UrlEncrypt("黄浦区"), "黄浦区");
            city.Add(QueryString.UrlEncrypt("卢湾区"), "卢湾区");
            city.Add(QueryString.UrlEncrypt("徐汇区"), "徐汇区");
            city.Add(QueryString.UrlEncrypt("长宁区"), "长宁区");
            city.Add(QueryString.UrlEncrypt("静安区"), "静安区");
            city.Add(QueryString.UrlEncrypt("普陀区"), "普陀区");
            city.Add(QueryString.UrlEncrypt("闸北区"), "闸北区");
            city.Add(QueryString.UrlEncrypt("虹口区"), "虹口区");
            city.Add(QueryString.UrlEncrypt("杨浦区"), "杨浦区");
            city.Add(QueryString.UrlEncrypt("宝山区"), "宝山区");
            city.Add(QueryString.UrlEncrypt("闵行区"), "闵行区");
            city.Add(QueryString.UrlEncrypt("嘉定区"), "嘉定区");
            city.Add(QueryString.UrlEncrypt("松江区"), "松江区");
            city.Add(QueryString.UrlEncrypt("金山区"), "金山区");
            city.Add(QueryString.UrlEncrypt("青浦区"), "青浦区");
            city.Add(QueryString.UrlEncrypt("浦东新区"), "浦东新区");
            city.Add(QueryString.UrlEncrypt("南汇区(惠南镇)"), "南汇区(惠南镇)");
            city.Add(QueryString.UrlEncrypt("奉贤区(南桥镇)"), "奉贤区(南桥镇)");
            city.Add(QueryString.UrlEncrypt("崇明县(城桥镇)"), "崇明县(城桥镇)");
            string tempCityId = cityId == null ? "" : cityId;
            ViewBag.CityData = new SelectList(city, "key", "value", QueryString.UrlEncrypt(tempCityId));
            //性别
            Dictionary<string, string> sex = new Dictionary<string, string>();
            sex.Add(QueryString.UrlEncrypt(1), "男");
            sex.Add(QueryString.UrlEncrypt(2), "女");
            ViewBag.SexData = new SelectList(sex, "key", "value", QueryString.UrlEncrypt(sexId.ToString()));
            //年龄段
            Dictionary<string, string> age = new Dictionary<string, string>();
            age.Add(QueryString.UrlEncrypt(1), "20—30");
            age.Add(QueryString.UrlEncrypt(2), "31—40");
            age.Add(QueryString.UrlEncrypt(3), "41—50");
            age.Add(QueryString.UrlEncrypt(4), "50");
            ViewBag.AgeData = new SelectList(age, "key", "value", QueryString.UrlEncrypt(ageId.ToString()));
            //年级段
            Dictionary<string, string> grade = new Dictionary<string, string>();
            grade.Add(QueryString.UrlEncrypt(1), "幼儿园");
            grade.Add(QueryString.UrlEncrypt(2), "小学");
            grade.Add(QueryString.UrlEncrypt(3), "初中");
            grade.Add(QueryString.UrlEncrypt(4), "高中");
            grade.Add(QueryString.UrlEncrypt(6), "中职校");
            grade.Add(QueryString.UrlEncrypt(5), "其他");
            ViewBag.GradeData = new SelectList(grade, "key", "value", QueryString.UrlEncrypt(gradeId.ToString()));
            //学科
            Dictionary<string, string> subject = new Dictionary<string, string>();
            subject.Add(QueryString.UrlEncrypt(1), "语文");
            subject.Add(QueryString.UrlEncrypt(2), "数学");
            subject.Add(QueryString.UrlEncrypt(3), "英语");
            subject.Add(QueryString.UrlEncrypt(4), "物理");
            subject.Add(QueryString.UrlEncrypt(5), "化学");
            subject.Add(QueryString.UrlEncrypt(6), "生物");
            subject.Add(QueryString.UrlEncrypt(7), "思品/政治");
            subject.Add(QueryString.UrlEncrypt(8), "历史");
            subject.Add(QueryString.UrlEncrypt(9), "地理");
            subject.Add(QueryString.UrlEncrypt(10), "体育");
            subject.Add(QueryString.UrlEncrypt(11), "美术");
            subject.Add(QueryString.UrlEncrypt(12), "音乐");
            subject.Add(QueryString.UrlEncrypt(13), "信息技术");
            subject.Add(QueryString.UrlEncrypt(14), "综合实践");
            subject.Add(QueryString.UrlEncrypt(15), "通用技术");
            subject.Add(QueryString.UrlEncrypt(16), "幼儿园");
            subject.Add(QueryString.UrlEncrypt(17), "其他");
            subject.Add(QueryString.UrlEncrypt(18), "教科研");
            subject.Add(QueryString.UrlEncrypt(19), "心理咨询");
            subject.Add(QueryString.UrlEncrypt(20), "拓展课程");
            ViewBag.SubjectData = new SelectList(subject, "key", "value", QueryString.UrlEncrypt(subjectId.ToString()));
            //学校所在区域
            Dictionary<string, string> area = new Dictionary<string, string>();
            area.Add(QueryString.UrlEncrypt(1), "地市级或以上城市");
            area.Add(QueryString.UrlEncrypt(2), "区或县城");
            area.Add(QueryString.UrlEncrypt(3), "乡镇");
            area.Add(QueryString.UrlEncrypt(4), "农村");
            ViewBag.AreaData = new SelectList(area, "key", "value", QueryString.UrlEncrypt(areaId.ToString()));
            //教学经验
            Dictionary<string, string> exp = new Dictionary<string, string>();
            exp.Add(QueryString.UrlEncrypt(1), "1-5年");
            exp.Add(QueryString.UrlEncrypt(2), "6年—15年");
            exp.Add(QueryString.UrlEncrypt(3), "16年—25年");
            exp.Add(QueryString.UrlEncrypt(4), "26年—35年");
            exp.Add(QueryString.UrlEncrypt(5), "36年以上");
            ViewBag.ExpData = new SelectList(exp, "key", "value", QueryString.UrlEncrypt(expId.ToString()));
            //职称
            List<Job> mJob = new List<Job>();
            mJob.Add(new Job() { ParentId = 0, Id = QueryString.UrlEncrypt(1), Title = "初级职称" });
            mJob.Add(new Job() { ParentId = 0, Id = QueryString.UrlEncrypt(2), Title = "中级职称" });
            mJob.Add(new Job() { ParentId = 0, Id = QueryString.UrlEncrypt(3), Title = "高级职称" });
            ViewBag.MJobData = new SelectList(mJob, "Id", "Title", QueryString.UrlEncrypt(mJobId.ToString()));
            List<Job> sJob = new List<Job>();
            sJob.Add(new Job() { ParentId = 1, Id = QueryString.UrlEncrypt(11), Title = "幼教一级" });
            sJob.Add(new Job() { ParentId = 1, Id = QueryString.UrlEncrypt(12), Title = "幼教二级" });
            sJob.Add(new Job() { ParentId = 1, Id = QueryString.UrlEncrypt(13), Title = "小教一级" });
            sJob.Add(new Job() { ParentId = 1, Id = QueryString.UrlEncrypt(14), Title = "小教二级" });
            sJob.Add(new Job() { ParentId = 1, Id = QueryString.UrlEncrypt(15), Title = "中教二级" });
            sJob.Add(new Job() { ParentId = 2, Id = QueryString.UrlEncrypt(21), Title = "幼教高级" });
            sJob.Add(new Job() { ParentId = 2, Id = QueryString.UrlEncrypt(22), Title = "小学高级" });
            sJob.Add(new Job() { ParentId = 2, Id = QueryString.UrlEncrypt(23), Title = "中教一级" });
            sJob.Add(new Job() { ParentId = 3, Id = QueryString.UrlEncrypt(31), Title = "中教高级" });
            int tempMJobId = Web.Code.ExtendHelper.ToInt(mJobId);
            ViewBag.SJobData = new SelectList(sJob.Where(t => t.ParentId == tempMJobId), "Id", "Title", QueryString.UrlEncrypt(sJobId.ToString()));
            //教学称号
            Dictionary<string, string> title = new Dictionary<string, string>();
            title.Add(QueryString.UrlEncrypt(1), "省级名师/特级教师");
            title.Add(QueryString.UrlEncrypt(2), "市级骨干");
            title.Add(QueryString.UrlEncrypt(3), "区或县级骨干");
            title.Add(QueryString.UrlEncrypt(4), "一般教师");
            ViewBag.TitleData = new SelectList(title, "key", "value", QueryString.UrlEncrypt(titleId.ToString()));
            //最高学历
            Dictionary<string, string> edu = new Dictionary<string, string>();
            edu.Add(QueryString.UrlEncrypt(1), "硕士研究生或博士研究生");
            edu.Add(QueryString.UrlEncrypt(2), "本科");
            edu.Add(QueryString.UrlEncrypt(3), "专科");
            edu.Add(QueryString.UrlEncrypt(4), "专科以下");
            ViewBag.EduData = new SelectList(edu, "key", "value", QueryString.UrlEncrypt(eduId.ToString()));
            //在校身份
            Dictionary<string, string> iden = new Dictionary<string, string>();
            iden.Add(QueryString.UrlEncrypt(1), "学校管理者");
            iden.Add(QueryString.UrlEncrypt(2), "教研组长");
            iden.Add(QueryString.UrlEncrypt(3), "班主任");
            iden.Add(QueryString.UrlEncrypt(4), "学科教师");
            iden.Add(QueryString.UrlEncrypt(5), "师训人员");
            iden.Add(QueryString.UrlEncrypt(6), "其他");
            ViewBag.IdenData = new SelectList(iden, "key", "value", QueryString.UrlEncrypt(idenId.ToString()));

            //国家能力等级
            List<string> tNantion = new List<string>();
            tNantion.AddRange(new string[]{
                "T1信息技术引发的教育教学变革",
                "T2多媒体教学环境认知与常用设备使用",
                "T3学科资源检索与获取",
                "T4素材的处理与加工",
                "T5多媒体课件制作",
                "T6学科软件的使用",
                "T7信息道德与信息安全",
                "T8简易多媒体教学环境下的学科教学",
                "T9交互多媒体环境下的学科教学",
                "T10学科教学资源支持下的课程教学",
                "T11技术支持的课堂导入",
                "T12技术支持的课堂讲授",
                "T13技术支持的学生技能训练与指导",
                "T14技术支持的总结与复习",
                "T15技术支持的教学评价",
                "T16网络学习空间的构建与管理",
                "T17网络教学平台的应用",
                "T18适用于移动设备的软件应用",
                "T19网络教学环境中的自主合作探究学习",
                "T20移动学习环境中的自主合作探究学习",
                "T21技术支持的探究学习任务设计",
                "T22技术支持的学习小组的组织与管理",
                "T23技术支持的学习过程监控",
                "T24技术支持的学习评价",
                "T25中小学教师信息技术应用能力标准解读",
                "T26教师工作坊与教师专业发展",
                "T27网络研修社区与教师专业发展" });
            ViewBag.TNationData = tNantion;
            #endregion

            #region 统计数据
            StringBuilder sb = new StringBuilder();
            sb.Append("provid=" + provId);
            if(!string.IsNullOrEmpty(cityId))
                sb.Append(" and city='" + cityId + "'");
            if (sexId != null)
                sb.Append(" and sex=" + sexId);
            if (ageId != null)
                sb.Append(" and age=" + ageId);
            if (gradeId != null)
                sb.Append(" and grade=" + gradeId);
            if (subjectId != null)
                sb.Append(" and subject=" + subjectId);
            if (areaId != null)
                sb.Append(" and area=" + areaId);
            if (expId != null)
                sb.Append(" and experience=" + expId);
            if (sJobId != null)
                sb.Append(" and job=" + sJobId);
            if (titleId != null)
                sb.Append(" and designation=" + titleId);
            if (eduId != null)
                sb.Append(" and record=" + eduId);
            if (idenId != null)
                sb.Append(" and iden=" + idenId);

            DataSet ds = new accountBLL().ChartData(sb.ToString());
            ViewBag.TotalNum = Convert.ToInt32(ds.Tables[0].Rows[0][0]);//总人数
            ViewBag.TData = ds.Tables[2];//选课比例

            int[] dimensionKey1 = { 1, 2, 3, 4, 5 };
            int[] dimensionKey2 = { 6, 7, 8, 9, 10 };
            string[] dimensionValue1 = { "I技术素养", "I计划与准备", "I组织与管理", "I评论与诊断", "I学习与发展" };
            string[] dimensionValue2 = { "II技术素养", "II计划与准备", "II组织与管理", "II评论与诊断", "II学习与发展" };

            ViewBag.Dimension1 = string.Join(",", dimensionValue1);
            ViewBag.Dimension2 = string.Join(",", dimensionValue2);

            StringBuilder sbRadarData1 = new StringBuilder();
            StringBuilder sbColumnData1_Rank1 = new StringBuilder();
            StringBuilder sbColumnData1_Rank2 = new StringBuilder();
            StringBuilder sbColumnData1_Rank3 = new StringBuilder();
            foreach (int item in dimensionKey1)
            {
                DataRow row = ds.Tables[1].Select("dimensionid=" + item).FirstOrDefault();
                if (row != null)
                {
                    sbRadarData1.Append(row["AvgRank"] + ",");
                    sbColumnData1_Rank1.Append(Math.Round(Convert.ToDouble(row["Rank1"]) / Convert.ToDouble(row["CNT"]), 2) + ",");
                    sbColumnData1_Rank2.Append(Math.Round(Convert.ToDouble(row["Rank2"]) / Convert.ToDouble(row["CNT"]), 2) + ",");
                    sbColumnData1_Rank3.Append(Math.Round(Convert.ToDouble(row["Rank3"]) / Convert.ToDouble(row["CNT"]), 2) + ",");
                }
                else
                {
                    sbRadarData1.Append("0,");
                    sbColumnData1_Rank1.Append("0,");
                    sbColumnData1_Rank2.Append("0,");
                    sbColumnData1_Rank3.Append("0,");
                }
            }
            ViewBag.RadarData1 = sbRadarData1.ToString();//雷达图1数据
            //柱状图1数据
            ViewBag.ColumnData1_Rank1 = sbColumnData1_Rank1.ToString();
            ViewBag.ColumnData1_Rank2 = sbColumnData1_Rank2.ToString();
            ViewBag.ColumnData1_Rank3 = sbColumnData1_Rank3.ToString();

            StringBuilder sbRadarData2 = new StringBuilder();
            StringBuilder sbColumnData2_Rank1 = new StringBuilder();
            StringBuilder sbColumnData2_Rank2 = new StringBuilder();
            StringBuilder sbColumnData2_Rank3 = new StringBuilder();
            foreach (int item in dimensionKey2)
            {
                DataRow row = ds.Tables[1].Select("dimensionid=" + item).FirstOrDefault();
                if (row != null)
                {
                    sbRadarData2.Append(row["AvgRank"] + ",");
                    sbColumnData2_Rank1.Append(Math.Round(Convert.ToDouble(row["Rank1"]) / Convert.ToDouble(row["CNT"]), 2) + ",");
                    sbColumnData2_Rank2.Append(Math.Round(Convert.ToDouble(row["Rank2"]) / Convert.ToDouble(row["CNT"]), 2) + ",");
                    sbColumnData2_Rank3.Append(Math.Round(Convert.ToDouble(row["Rank3"]) / Convert.ToDouble(row["CNT"]), 2) + ",");
                }
                else
                {
                    sbRadarData2.Append("0,");
                    sbColumnData2_Rank1.Append("0,");
                    sbColumnData2_Rank2.Append("0,");
                    sbColumnData2_Rank3.Append("0,");
                }
            }
            ViewBag.RadarData2 = sbRadarData2.ToString();//雷达图2数据
            //柱状图2数据
            ViewBag.ColumnData2_Rank1 = sbColumnData2_Rank1.ToString();
            ViewBag.ColumnData2_Rank2 = sbColumnData2_Rank2.ToString();
            ViewBag.ColumnData2_Rank3 = sbColumnData2_Rank3.ToString();
            #endregion

            return View();
        }
        
        #region 加载区县信息
        private List<SelectListItem> GetReginListItem()
        {            
            var bll = new AccountEditBLL();
            var Regindt = bll.GetRegin(5);
            var Regin = new List<SelectListItem>();
            for (int i = 0; i < Regindt.Rows.Count; i++)
            {
                var item = new SelectListItem();
                item.Text = Regindt.Rows[i][1].ToString();
                item.Value = Regindt.Rows[i][0].ToString();
                Regin.Add(item);
            }

            return Regin;
        }
        #endregion

        #region 获取机构信息
        public ActionResult GetRegion(int id)
        {
            var bll = new AccountEditBLL();
            var Regindt = bll.GetRegionTo(id);
            var Regin = new List<SelectListItem>();
            for (int i = 0; i < Regindt.Rows.Count; i++)
            {
                var item = new SelectListItem();
                item.Text = Regindt.Rows[i][1].ToString();
                item.Value = Regindt.Rows[i][0].ToString();
                Regin.Add(item);
            }
            return Json(Regin, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region 获取Url参数加密字符串
        public ActionResult EnCode(string Code)
        {
            string EnCode = Dianda.Common.QueryString.UrlEncrypt(Code);
            return Json(new { Data = EnCode }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region ajax
        //职称
        public string AjaxSJob(string mJobId)
        {
            int parentId;
            int.TryParse(QueryString.Decrypt(mJobId), out parentId);
            List<Job> sJob = new List<Job>();
            sJob.Add(new Job() { ParentId = 1, Id = QueryString.UrlEncrypt(11), Title = "幼教一级" });
            sJob.Add(new Job() { ParentId = 1, Id = QueryString.UrlEncrypt(12), Title = "幼教二级" });
            sJob.Add(new Job() { ParentId = 1, Id = QueryString.UrlEncrypt(13), Title = "小教一级" });
            sJob.Add(new Job() { ParentId = 1, Id = QueryString.UrlEncrypt(14), Title = "小教二级" });
            sJob.Add(new Job() { ParentId = 1, Id = QueryString.UrlEncrypt(15), Title = "中教二级" });
            sJob.Add(new Job() { ParentId = 2, Id = QueryString.UrlEncrypt(21), Title = "幼教高级" });
            sJob.Add(new Job() { ParentId = 2, Id = QueryString.UrlEncrypt(22), Title = "小学高级" });
            sJob.Add(new Job() { ParentId = 2, Id = QueryString.UrlEncrypt(23), Title = "中教一级" });
            sJob.Add(new Job() { ParentId = 3, Id = QueryString.UrlEncrypt(31), Title = "中教高级" });
            return new JavaScriptSerializer().Serialize(sJob.Where(t => t.ParentId == parentId));
        }
        #endregion 

        [Serializable]
        internal class Job
        {
            public int ParentId { get; set; }
            public string Id { get; set; }
            public string Title { get; set; }
        }
    }
}
