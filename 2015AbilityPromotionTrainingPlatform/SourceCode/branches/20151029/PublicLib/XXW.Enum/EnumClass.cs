using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXW.Enum
{
    public class EnumClass
    {
        private static IList<MsgTypeEnum> _systemMsgTypeEnumList;
        public static IList<MsgTypeEnum> SystemMsgTypeEnumList
        {
            get{
                if (_systemMsgTypeEnumList != null)
                    return _systemMsgTypeEnumList;
                _systemMsgTypeEnumList = new List<MsgTypeEnum>();
                _systemMsgTypeEnumList.Add(MsgTypeEnum.Platform);
                _systemMsgTypeEnumList.Add(MsgTypeEnum.System);
                return _systemMsgTypeEnumList;
            }
            private set { }            
        }

        public static void ClearSystemMsgTypeEnumList()
        {
            _systemMsgTypeEnumList = null;
        }

        public static string GetMsgStatusEnumName(MsgStatusEnum e)
        {
            string a = "";
            switch (e)
            {
                case MsgStatusEnum.Confirmed:
                    a = "同意";
                    break;
                case MsgStatusEnum.Declined:
                    a = "拒绝";
                    break;
            }
            return a;
        }

        public static string GetEducationName(EducationEnum e)
        {
            string a = "";
            switch (e)
            {
                case EducationEnum.Primary:
                    a = "小学";
                    break;
                case EducationEnum.Junior:
                    a = "初中";
                    break;
                case EducationEnum.Senior:
                    a = "高中";
                    break;
                case EducationEnum.Vocational:
                    a = "高职";
                    break;
                case EducationEnum.College:
                    a = "专科";
                    break;
                case EducationEnum.Undergraduate:
                    a = "本科";
                    break;
                case EducationEnum.Postgraduate:
                    a = "研究生";
                    break;
                case EducationEnum.Doctor:
                    a = "博士";
                    break;
                case EducationEnum.Postdoctoral:
                    a = "博士后";
                    break;
            }
            return a;
        }

        /// <summary>
        /// 教育程度列表
        /// </summary>
        /// <returns></returns>
        public static IDictionary<EducationEnum, string> GetEducationDict()
        {
            IDictionary<EducationEnum, string> dict = new Dictionary<EducationEnum, string>();
            dict.Add(EducationEnum.Primary, GetEducationName(EducationEnum.Primary));
            dict.Add(EducationEnum.Junior, GetEducationName(EducationEnum.Junior));
            dict.Add(EducationEnum.Senior, GetEducationName(EducationEnum.Senior));
            dict.Add(EducationEnum.Vocational, GetEducationName(EducationEnum.Vocational));
            dict.Add(EducationEnum.College, GetEducationName(EducationEnum.College));
            dict.Add(EducationEnum.Undergraduate, GetEducationName(EducationEnum.Undergraduate));
            dict.Add(EducationEnum.Postgraduate, GetEducationName(EducationEnum.Postgraduate));
            dict.Add(EducationEnum.Doctor, GetEducationName(EducationEnum.Doctor));
            dict.Add(EducationEnum.Postdoctoral, GetEducationName(EducationEnum.Postdoctoral));
            return dict;
        }

        public static string GetCredentialsTypeName(CredentialsTypeEnum e)
        {
            string a = "";
            switch (e)
            {
                case CredentialsTypeEnum.IDCard:
                    a = "身份证";
                    break;
                case CredentialsTypeEnum.Passport:
                    a = "护照";
                    break;
                case CredentialsTypeEnum.MilitaryID:
                    a = "军官证";
                    break;
            }
            return a;
        }

        /// <summary>
        /// 证件列表
        /// </summary>
        /// <returns></returns>
        public static IDictionary<CredentialsTypeEnum, string> GetCredentialsTypeDict()
        {
            IDictionary<CredentialsTypeEnum, string> dict = new Dictionary<CredentialsTypeEnum, string>();
            dict.Add(CredentialsTypeEnum.IDCard, GetCredentialsTypeName(CredentialsTypeEnum.IDCard));
            dict.Add(CredentialsTypeEnum.Passport, GetCredentialsTypeName(CredentialsTypeEnum.Passport));
            dict.Add(CredentialsTypeEnum.MilitaryID, GetCredentialsTypeName(CredentialsTypeEnum.MilitaryID));
            return dict;
        }

        public static string GetRecordStatusName(RecordStatusEnum e)
        {
            string a = "";
            switch (e)
            {
                case RecordStatusEnum.Ongoing:
                    a = "进行中";
                    break;
                case RecordStatusEnum.Complete:
                    a = "完成";
                    break;
            }
            return a;
        }

        public static string Object2ReferType(ObjectTypeEnum e)
        {
            string a = "";
            switch (e)
            { 
                case ObjectTypeEnum.CourseInfo:
                    a = "course";
                    break;
                case ObjectTypeEnum.ActivitiesInfo:
                    a = "event";
                    break;
                case ObjectTypeEnum.CircleInfo:
                    a = "note";
                    break;
            }
            return a;
        }

        private static IDictionary<ObjectTypeEnum, string> _objectTypeConditionDict;
        public static IDictionary<ObjectTypeEnum, string> ObjectTypeConditionDict
        {
            get {
                if (_objectTypeConditionDict == null)
                {
                    _objectTypeConditionDict = new Dictionary<ObjectTypeEnum, string>();
                    _objectTypeConditionDict.Add(ObjectTypeEnum.All, "全部");
                    _objectTypeConditionDict.Add(ObjectTypeEnum.CourseInfo, "课程");
                    _objectTypeConditionDict.Add(ObjectTypeEnum.ActivitiesInfo, "活动");
                    _objectTypeConditionDict.Add(ObjectTypeEnum.CircleInfo, "团队");
                }
                return _objectTypeConditionDict;
            }
            private set { }
        }

        /*
        public static string GetSortType(SortTypeEnum e)
        {
            string t = "";
            switch (e)
            { 
                case SortTypeEnum.ASC:
                    t = " ASC ";
                    break;
                case SortTypeEnum.DESC:
                    t = " DESC ";
                    break;
            }
            return t;
        }
         * */

        public static string GetObjectTypeName4PortalSpecialRecommend(ObjectTypeEnum e)
        {
            string name = "";
            switch (e)
            {
                case ObjectTypeEnum.All:
                    name = "全部";
                    break;
                case ObjectTypeEnum.CourseInfo:
                    name = "课程";
                    break;
                case ObjectTypeEnum.ActivitiesInfo:
                    name = "活动";
                    break;
                case ObjectTypeEnum.ReadingBook:
                    name = "悦读";
                    break;
                case ObjectTypeEnum.CircleInfo:
                    name = "团队";
                    break;
                case ObjectTypeEnum.TopicInfo:
                    name = "热门话题";
                    break;
                case ObjectTypeEnum.Share:
                    name = "最新分享";
                    break;
                case ObjectTypeEnum.HotMember:
                    name = "热门用户";
                    break;
            }
            return name;
        }

        public static string GetObjectTypeName(ObjectTypeEnum e)
        {
            string name = "";
            switch (e)
            {
                case ObjectTypeEnum.All:
                    name = "全部";
                    break;
                case ObjectTypeEnum.CourseInfo:
                    name = "课程";
                    break;
                case ObjectTypeEnum.ActivitiesInfo:
                    name = "活动";
                    break;
                case ObjectTypeEnum.ReadingBook:
                    name = "悦读";
                    break;
                case ObjectTypeEnum.CircleInfo:
                    name = "团队";
                    break;
                case ObjectTypeEnum.TopicInfo:
                    name = "话题";
                    break;
            }
            return name;
        }

        private static IDictionary<string, string> _ruleCodeDict;
        public static IDictionary<string, string> RuleCodeDict
        {
            get
            {
                if (_ruleCodeDict == null)
                {
                    _ruleCodeDict = new Dictionary<string, string>();
                    _ruleCodeDict.Add(RuleCodeEnum.Member_Register.ToString().ToLower(), "注册");
                    _ruleCodeDict.Add(RuleCodeEnum.Member_Login.ToString().ToLower(), "登录");
                    _ruleCodeDict.Add(RuleCodeEnum.Member_Email.ToString().ToLower(), "邮箱验证");
                    _ruleCodeDict.Add(RuleCodeEnum.Member_Avatar.ToString().ToLower(), "上传头像");
                    _ruleCodeDict.Add(RuleCodeEnum.Member_DetailInfo.ToString().ToLower(), "详细资料");
                    _ruleCodeDict.Add(RuleCodeEnum.Member_Validate.ToString().ToLower(), "实名认证");

                    _ruleCodeDict.Add(RuleCodeEnum.Circle_Join.ToString().ToLower(), "加入团队");
                    _ruleCodeDict.Add(RuleCodeEnum.Circle_AddTopic.ToString().ToLower(), "发表话题（团队）");
                    _ruleCodeDict.Add(RuleCodeEnum.Circle_ReplyTopic.ToString().ToLower(), "回复话题（团队）");
                    _ruleCodeDict.Add(RuleCodeEnum.Circle_AddAttachment.ToString().ToLower(), "添加附件（团队）");
                    _ruleCodeDict.Add(RuleCodeEnum.Circle_Share.ToString().ToLower(), "分享（团队）");
                    _ruleCodeDict.Add(RuleCodeEnum.Circle_SyncShare.ToString().ToLower(), "同步乐享（团队）");

                    _ruleCodeDict.Add(RuleCodeEnum.Course_Resources.ToString().ToLower(), "学习资源（课程）");
                    _ruleCodeDict.Add(RuleCodeEnum.Course_Comment.ToString().ToLower(), "评价（课程）");
                    _ruleCodeDict.Add(RuleCodeEnum.Course_FirstAddSchedule.ToString().ToLower(), "初次加入课表（课程）");
                    _ruleCodeDict.Add(RuleCodeEnum.Course_AddSchedule.ToString().ToLower(), "加入课表（课程）");
                    _ruleCodeDict.Add(RuleCodeEnum.Course_Follow.ToString().ToLower(), "关注课程（课程）");
                    _ruleCodeDict.Add(RuleCodeEnum.Course_Note.ToString().ToLower(), "学习笔记（课程）");
                    _ruleCodeDict.Add(RuleCodeEnum.Course_Share.ToString().ToLower(), "分享（课程）");
                    _ruleCodeDict.Add(RuleCodeEnum.Course_Upload.ToString().ToLower(), "上传课程（课程）");
                    _ruleCodeDict.Add(RuleCodeEnum.Course_SyncShare.ToString().ToLower(), "同步乐享（课程）");

                    _ruleCodeDict.Add(RuleCodeEnum.Act_Join.ToString().ToLower(), "加入活动（活动）");
                    _ruleCodeDict.Add(RuleCodeEnum.Act_Follow.ToString().ToLower(), "关注（活动）");
                    _ruleCodeDict.Add(RuleCodeEnum.Act_Share.ToString().ToLower(), "分享（活动）");
                    _ruleCodeDict.Add(RuleCodeEnum.Act_AddTopic.ToString().ToLower(), "添加话题（活动）");
                    _ruleCodeDict.Add(RuleCodeEnum.Act_BeReplyTopic.ToString().ToLower(), "话题被回复（活动）");
                    _ruleCodeDict.Add(RuleCodeEnum.Act_ReplyTopic.ToString().ToLower(), "回复话题（活动）");
                    _ruleCodeDict.Add(RuleCodeEnum.Act_AddAttachment.ToString().ToLower(), "添加附件（活动）");
                    _ruleCodeDict.Add(RuleCodeEnum.Act_Link.ToString().ToLower(), "关联课程/图书（活动）");
                    _ruleCodeDict.Add(RuleCodeEnum.Act_SyncShare.ToString().ToLower(), "同步乐享（活动）");

                    _ruleCodeDict.Add(RuleCodeEnum.Read_Comment.ToString().ToLower(), "写书评（悦读）");
                    _ruleCodeDict.Add(RuleCodeEnum.Read_ReplyComment.ToString().ToLower(), "回书评（悦读）");
                    _ruleCodeDict.Add(RuleCodeEnum.Read_Collect.ToString().ToLower(), "收藏（悦读）");
                    _ruleCodeDict.Add(RuleCodeEnum.Read_BeRecommend.ToString().ToLower(), "被推荐（悦读）");
                    _ruleCodeDict.Add(RuleCodeEnum.Read_Share.ToString().ToLower(), "分享（悦读）");
                    _ruleCodeDict.Add(RuleCodeEnum.Read_SyncShare.ToString().ToLower(), "同步乐享（悦读）");

                    _ruleCodeDict.Add(RuleCodeEnum.Photo_Add.ToString().ToLower(), "发图（摄影）");
                    _ruleCodeDict.Add(RuleCodeEnum.Photo_Comment.ToString().ToLower(), "评图（摄影）");
                    _ruleCodeDict.Add(RuleCodeEnum.Photo_BeRecommend.ToString().ToLower(), "被推荐（摄影）");
                    _ruleCodeDict.Add(RuleCodeEnum.Photo_FirstBeGreat.ToString().ToLower(), "初次获赞（摄影）");
                    _ruleCodeDict.Add(RuleCodeEnum.Photo_BeGreat.ToString().ToLower(), "获赞（摄影）");

                    _ruleCodeDict.Add(RuleCodeEnum.Share_Publish.ToString().ToLower(), "发布（乐享）");
                    _ruleCodeDict.Add(RuleCodeEnum.Share_Share.ToString().ToLower(), "分享（乐享）");
                    _ruleCodeDict.Add(RuleCodeEnum.Share_BeGreat.ToString().ToLower(), "获赞（乐享）");
                    _ruleCodeDict.Add(RuleCodeEnum.Share_BeReply.ToString().ToLower(), "被回帖（乐享）");

                    _ruleCodeDict.Add(RuleCodeEnum.Map_Comment.ToString().ToLower(), "点评（地图）");
                }
                return _ruleCodeDict;
            }
        }
        

        private static IDictionary<TempActivityCategoryEnum, string> _tempActivityCategoryDict;
        public static IDictionary<TempActivityCategoryEnum, string> TempActivityCategoryDict
        {
            get
            {
                if (_tempActivityCategoryDict == null)
                {
                    _tempActivityCategoryDict.Add(TempActivityCategoryEnum.Announcement, "活动公告");
                    _tempActivityCategoryDict.Add(TempActivityCategoryEnum.LearningEvents, "学习赛事");
                    _tempActivityCategoryDict.Add(TempActivityCategoryEnum.Forum, "主题论坛");
                    _tempActivityCategoryDict.Add(TempActivityCategoryEnum.Exhibition, "学习展示");
                    _tempActivityCategoryDict.Add(TempActivityCategoryEnum.Experience, "课程体验");
                    _tempActivityCategoryDict.Add(TempActivityCategoryEnum.Moment, "精彩瞬间");
                }
                return _tempActivityCategoryDict;
            }
        }

        public static string GetApprovalStatus(ApprovalStatusEnum ase)
        {
            string name = "";
            switch (ase)
            {
                case ApprovalStatusEnum.Back:
                    name = "退回修改";
                    break;
                case ApprovalStatusEnum.Cancelled:
                    name = "已取消";
                    break;
                case ApprovalStatusEnum.Draft:
                    name = "草稿";
                    break;
                case ApprovalStatusEnum.NotPass:
                    name = "未通过";
                    break;
                case ApprovalStatusEnum.Pass:
                    name = "已通过";
                    break;
                case ApprovalStatusEnum.Pending:
                    name = "待审核";
                    break;
            }
            return name;
        }
    }
}
