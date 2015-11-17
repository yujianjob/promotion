using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XXW.Enum
{
    /// <summary>
    /// 用户为url添加参数key的。
    /// </summary>
    public enum QueryStringKeyEnum//一级缓存大类类别枚举类型。
    {
        /// <summary>
        /// Session类别缓存类别名称。
        /// </summary>
        Id,
        /// <summary>
        /// 数据表
        /// </summary>
        MemberId,
        /// <summary>
        ///其他，索引
        /// </summary>
        PageIndex,
        /// <summary>
        ///标签类数据
        /// </summary>
        SearcheKey,
        /// <summary>
        ///课程Id
        /// </summary>
        CourseId,
        /// <summary>
        ///活动Id
        /// </summary>
        ActivitieId,
        /// <summary>
        /// 圈子Id
        /// </summary>
        CircleId,
        /// <summary>
        /// 话题Id
        /// </summary>
        TopicId,
        /// <summary>
        /// 管理员Id
        /// </summary>
        UserId,
        /// <summary>
        /// 组织机构Id
        /// </summary>
        OrganId,
        /// <summary>
        /// 图书Id
        /// </summary>
        BookId,
        /// <summary>
        /// 区域Id
        /// </summary>
        RegionId,
        /// <summary>
        /// 分享Id
        /// </summary>
        ShareId,
        /// <summary>
        /// 标签Id
        /// </summary>
        TagId
    }

    /// <summary>
    /// 用于SiteAddressHelper的，
    /// </summary>
    public enum SiteTypeEnum
    {
        //这些名称要和数据库中sys_parameter中range是site的parameterKey要一模一样。用在SiteAddressHelper.cs
        Portal, Course, Activities, Reading, Share, Circle, Member, Management, ResourceUrl, ShortUrl, ResourceSite, Information, Map, SubjectsDetail, MapPlaceDetail
    }



    /// <summary>
    /// 主体类别对照表,
    /// 在SistaicFilesGen.cs用到一部分，要求与数据库SysParameters的range是template下的所有数据的ParameterKey要相同。
    /// </summary>
    public enum ObjectTypeEnum
    {
        All = -1,
        None = 0,
        /// <summary>
        /// 课程信息
        /// </summary>
        CourseInfo = 11,
        /// <summary>
        /// 课件信息
        /// </summary>
        CourseWareInfo = 12,
        /// <summary>
        /// 课程英语频道
        /// </summary>
        CourseEnglish = 13,
        /// <summary>
        /// 活动信息
        /// </summary>
        ActivitiesInfo = 21,
        /// <summary>
        /// 活动申请参加的处理结果
        /// </summary>
        ActivitiesApplyResult = 22,

        /// <summary>
        /// 环保袋活动
        /// </summary>
        ActivitiesReusableBagsDesign = 23,

        /// <summary>
        /// 电子书信息
        /// </summary>
        ReadingBook = 31,
        /// <summary>
        /// 圈子信息
        /// </summary>
        CircleInfo = 41,
        CircleInfoResult = 42,
        InformationNews=51,
        TopicInfo = 4101,
        /// <summary>
        /// 好友申请
        /// </summary>
        MemberFriendApply = 61,
        /// <summary>
        /// 好友申请的处理结果
        /// </summary>
        MemberFriendResult = 62,

        MsgRequest = 71,
        MsgResponse = 72,
        MapPlaceDetail = 81, //地图
        SubjectsDetail = 82, //对象
        System = 99,
        Plan = -999,
        Share = -998,
        HotMember = -997,   
        //特别策划
        SpPlan = 996
    }

    public enum MsgTypeEnum
    {
        Friend = 1,
        Platform = 2,
        System = 3
    }

    public enum MsgActionTypeEnum
    {
        All = 10000,
        Send = 10001,
        Receive = 10002
    }

    public enum MsgStatusEnum
    {
        Unread = 1,
        Readed = 2,
        Disabled = 3,
        Ignored = 4,
        Confirmed = 5,
        Declined = 6
    }

    public enum MemberStatusEnum
    {
        All = 0,
        /// <summary>
        /// 禁用
        /// </summary>
        Disable = -2,
        /// <summary>
        /// 未激活
        /// </summary>
        Inactive = -1,
        Normal = 1,
    }

    /// <summary>
    /// 证件类型
    /// </summary>
    public enum CredentialsTypeEnum
    { 
        IDCard = 1,
        Passport = 2,
        MilitaryID = 3
    }

    public enum GenderEnum
    { 
        Male = 1,
        Female = 2,
        Secret = 3
    }

    public enum EducationEnum
    {
        None = 0,
        /// <summary>
        /// 小学
        /// </summary>
        Primary = 1,
        /// <summary>
        /// 初中
        /// </summary>
        Junior = 2,
        /// <summary>
        /// 高中
        /// </summary>
        Senior = 3,
        Vocational = 4,
        /// <summary>
        /// 大专院校
        /// </summary>
        College = 5,
        /// <summary>
        /// 本科
        /// </summary>
        Undergraduate = 6,
        /// <summary>
        /// 研究生
        /// </summary>
        Postgraduate = 7,
        /// <summary>
        /// 博士
        /// </summary>
        Doctor = 8,
        /// <summary>
        /// 博士后
        /// </summary>
        Postdoctoral = 9
    }

    public enum RecordStatusEnum
    { 
        All = 0,
        /// <summary>
        /// 进行中
        /// </summary>
        Ongoing = 1,
        /// <summary>
        /// 完成
        /// </summary>
        Complete = 2,
        /// <summary>
        /// 收藏
        /// </summary>
        Collect = 3
    }

    public enum MemberTypeEnum
    { 
        All = 0,
        Member = 1,
        Admin = 2
    }

    public enum ActivitiesStatusEnum
    {
        All = 0,
        /// <summary>
        /// 草稿
        /// </summary>
        Draft = 1,
        /// <summary>
        /// 等待审核
        /// </summary>
        ForReview = 2,
        /// <summary>
        /// 通过
        /// </summary>
        Pass = 3,
        /// <summary>
        /// 未通过
        /// </summary>
        NotPass = 4,
        /// <summary>
        /// 已发布
        /// </summary>
        Publish = 5,
        /// <summary>
        /// 申请撤销
        /// </summary>
        Revocation = 6
    }

    public enum AttachmentTypeEnum
    { 
        None = 0,
        Image = 1,
        File = 2
    }

    public enum SortTypeEnum
    { 
        ASC,
        DESC
    }

    public enum SubscribeTypeEnum
    { 
        All = 0,
        RecommendCourse = 11,
        HotCourse = 12,
        RecommendActivities = 21,
        HotActivities = 22,
        RecommendReading = 31,
        HotReading = 32,
        RecommendCircle = 41,
        HotCircle = 42
    }

    public enum CommonSortConditionEnum
    { 
        ID,
        Sort,
        CreateDate,
        IsTop
    }

    public enum TopicSortConditionEnum
    {
        LikeCnt,
        ReplyCnt,
        LastReplyTime,
        CreateDate,
        Sort
    }

    public enum CircleSortConditionEnum
    { 
        ID,
        Sort
    }

    public enum LinkTargetEnum
    { 
        _Parent = 1,
        _Blank = 2,
        _Self = 3,
        _Top = 4
    }

    public enum ActionEnum
    { 
        None = -1,
        Add = 1,
        Edit = 2
    }

    public enum PostingAuthorityEnum
    { 
        All = -1,
        None = 0,
        Every = 1,
        Member = 2,
        Close = 3
    }

    public enum UploadErrorEnum
    { 
        None = 0,
        TooLarge = 1,
        InvalidExtension = 2
    }

    /// <summary>
    /// 平台各类数据的默认图片
    /// </summary>
    public enum DefaultImg
    {
        circle_default_106_106, course_default_184_184, map_default_241_167, member_default_120_120, public_default_150_200, public_default_200_150, public_default_200_200, reading_default_110_164, Activities_default_184_184
    }

    public enum MemberSearchTypeEnum
    { 
        All = -1,
        UserName,
        RealName,
        NickName,
        Email,
        CredentialsNumber,
    }

    public enum BonusOperationTypeEnum
    {
        All = -1,
        Plus = 1,
        Minus = 0
    }

    public enum RuleRangeTypeEnum
    { 
        Neither = 0,
        ObjectTypeOnly = 1,
        Both = 2
    }

    public enum RuleRepeatTypeEnum
    { 
        Once = 0,
        Second = 1,
        Minute = 2,
        Hour = 3,
        Day = 4,
        Week = 5,
        Month = 6,
        Year = 7
    }

    public enum RuleCodeEnum
    {
        /// <summary>
        /// 注册
        /// </summary>
        Member_Register,
        /// <summary>
        /// 登录
        /// </summary>
        Member_Login,
        /// <summary>
        /// 邮箱验证
        /// </summary>
        Member_Email,
        /// <summary>
        /// 上传头像
        /// </summary>
        Member_Avatar,
        /// <summary>
        /// 完善详细资料
        /// </summary>
        Member_DetailInfo,
        /// <summary>
        /// 实名认证
        /// </summary>
        Member_Validate,

        /// <summary>
        /// 加入学圈
        /// </summary>
        Circle_Join,
        /// <summary>
        /// 发表话题
        /// </summary>
        Circle_AddTopic,
        /// <summary>
        /// 回复话题
        /// </summary>
        Circle_ReplyTopic,
        /// <summary>
        /// 添加附件
        /// </summary>
        Circle_AddAttachment,
        /// <summary>
        /// 分享
        /// </summary>
        Circle_Share,
        /// <summary>
        /// 同步乐享
        /// </summary>
        Circle_SyncShare,

        /// <summary>
        /// 学习资源
        /// </summary>
        Course_Resources,
        /// <summary>
        /// 评价
        /// </summary>
        Course_Comment,
        /// <summary>
        /// 初次加入课表
        /// </summary>
        Course_FirstAddSchedule,
        /// <summary>
        /// 加入课表
        /// </summary>
        Course_AddSchedule,
        /// <summary>
        /// 关注课程
        /// </summary>
        Course_Follow,
        /// <summary>
        /// 学习笔记
        /// </summary>
        Course_Note,
        /// <summary>
        /// 分享
        /// </summary>
        Course_Share,
        /// <summary>
        /// 上传课程
        /// </summary>
        Course_Upload,
        /// <summary>
        /// 同步乐享
        /// </summary>
        Course_SyncShare,

        /// <summary>
        /// 加入活动
        /// </summary>
        Act_Join,
        /// <summary>
        /// 关注
        /// </summary>
        Act_Follow,
        /// <summary>
        /// 分享
        /// </summary>
        Act_Share,
        /// <summary>
        /// 添加话题
        /// </summary>
        Act_AddTopic,
        /// <summary>
        /// 话题被回复
        /// </summary>
        Act_BeReplyTopic,
        /// <summary>
        /// 回复话题
        /// </summary>
        Act_ReplyTopic,
        /// <summary>
        /// 添加附件
        /// </summary>
        Act_AddAttachment,
        /// <summary>
        /// 关联课程/图书
        /// </summary>
        Act_Link,
        /// <summary>
        /// 同步乐享
        /// </summary>
        Act_SyncShare,

        /// <summary>
        /// 写书评
        /// </summary>
        Read_Comment,
        /// <summary>
        /// 回书评
        /// </summary>
        Read_ReplyComment,
        /// <summary>
        /// 收藏
        /// </summary>
        Read_Collect,
        /// <summary>
        /// 被推荐
        /// </summary>
        Read_BeRecommend,
        /// <summary>
        /// 分享
        /// </summary>
        Read_Share,
        /// <summary>
        /// 同步乐享
        /// </summary>
        Read_SyncShare,

        /// <summary>
        /// 摄影发图
        /// </summary>
        Photo_Add,
        /// <summary>
        /// 评图
        /// </summary>
        Photo_Comment,
        /// <summary>
        /// 被推荐
        /// </summary>
        Photo_BeRecommend,
        /// <summary>
        /// 初次获赞
        /// </summary>
        Photo_FirstBeGreat,
        /// <summary>
        /// 获赞
        /// </summary>
        Photo_BeGreat,

        /// <summary>
        /// 发布
        /// </summary>
        Share_Publish,
        /// <summary>
        /// 分享
        /// </summary>
        Share_Share,
        /// <summary>
        /// 获赞
        /// </summary>
        Share_BeGreat,
        /// <summary>
        /// 被回帖
        /// </summary>
        Share_BeReply,

        /// <summary>
        /// 点评
        /// </summary>
        Map_Comment,
        /// <summary>
        /// 关注场馆
        /// </summary>
        Map_Follow
    }

    public enum TempActivityCategoryEnum
    {
        All = -1,
        Announcement = 1,
        LearningEvents = 2,
        Forum = 3,
        Exhibition = 4,
        Experience = 5,
        Moment = 6
    }

    public enum SSOResultEnum
    { 
        Faild = 0,
        Success = 1,
        RequestCorrect = 10010,
        RequestError = 10011,        
        CodeCorrect = 10020,
        CodeError = 10021,
        TokenError = 10031,
        SessionEmpty = 10041
    }

    public enum ApprovalStatusEnum
    { 
        All = -1,
        Draft = 0,
        Pending = 1,
        Pass = 2,
        NotPass = 3,
        Back = 4,
        Cancelled = 5
    }

    public enum PlatformGroupEnum
    {
        Other = 0,
        /// <summary>
        /// 市级管理员
        /// </summary>
        ManagerCity = 1,
        /// <summary>
        /// 区级管理员
        /// </summary>
        ManagerArea = 2,
        /// <summary>
        /// 培训机构管理员
        /// </summary>
        ManagerTraining = 3,
        /// <summary>
        /// 学校管理员
        /// </summary>
        ManagerSchool = 4,
        /// <summary>
        /// 课程教师
        /// </summary>
        TeacherCourse = 5,
        /// <summary>
        /// 课程辅导员
        /// </summary>
        TeacherInstructor = 6,
        /// <summary>
        /// 普通教师
        /// </summary>
        TeacherGeneral = 7,
        /// <summary>
        /// 区县进修学校
        /// </summary>
        School = 9
    }
}
