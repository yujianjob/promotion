using System;
namespace Dianda.AP.Model
{
	/// <summary>
	/// CBK_SHERC_USERINFO:实体类(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public partial class CBK_SHERC_USERINFO
	{
		public CBK_SHERC_USERINFO()
		{}
		#region Model
		private int _id;
		private string _oldid;
		private string _ename;
		private string _pwd;
		private string _realname;
		private string _sex;
		private string _email;
		private string _idcard;
		private string _birthday;
		private string _tele;
		private string _post;
		private string _address;
		private int? _delflag;
		private int? _isnews;
		private int? _politicsid;
		private int? _nationalid;
		private int? _quxianid;
		private int? _xuquid;
		private int? _school_typeid;
		private int? _schoolid;
		private string _usergroupsid;
        private string _userleves;
        private string _userjobid;
        private string _xueduanid;
        private string _xuekeid;
        private string _nianjiid;
        private string _zhichengid;
		private string _teachnumber;
		private int? _zydm;
		private string _schooltag;
		private DateTime? _timesofgraduate;
		private int? _xueliid;
		private int? _xueweiid;
        private int? _schoolages;
        private string _trainingtypeid;
		private int? _trainingobjectid;
		private string _images;
		private int? _logintimes;
		private int? _loginnums;
		private string _status;
        private DateTime? _modifydatetime;
        private string _workterm;
        private string _usersessionid;

        public string Usersessionid
        {
            get { return _usersessionid; }
            set { _usersessionid = value; }
        } 
		/// <summary>
		/// 
		/// </summary>
		public int ID
		{
			set{ _id=value;}
			get{return _id;}
		}
		/// <summary>
		/// 用户在资源库中的老的ID值
		/// </summary>
		public string OLDID
		{
			set{ _oldid=value;}
			get{return _oldid;}
		}
		/// <summary>
		/// 用户名
		/// </summary>
		public string ENAME
		{
			set{ _ename=value;}
			get{return _ename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string PWD
		{
			set{ _pwd=value;}
			get{return _pwd;}
		}
		/// <summary>
		/// 用户的真实姓名
		/// </summary>
		public string REALNAME
		{
			set{ _realname=value;}
			get{return _realname;}
		}
		/// <summary>
		/// 用户的性别
		/// </summary>
		public string SEX
		{
			set{ _sex=value;}
			get{return _sex;}
		}
		/// <summary>
		/// 用户的邮件地址
		/// </summary>
		public string EMAIL
		{
			set{ _email=value;}
			get{return _email;}
		}
		/// <summary>
		/// 身份证号码
		/// </summary>
		public string IDCARD
		{
			set{ _idcard=value;}
			get{return _idcard;}
		}
		/// <summary>
		/// 用户的生日
		/// </summary>
		public string BIRTHDAY
		{
			set{ _birthday=value;}
			get{return _birthday;}
		}
		/// <summary>
		/// 用户的联系电话
		/// </summary>
		public string TELE
		{
			set{ _tele=value;}
			get{return _tele;}
		}
		/// <summary>
		/// 用户的邮编号码
		/// </summary>
		public string POST
		{
			set{ _post=value;}
			get{return _post;}
		}
		/// <summary>
		/// 用户的联系地址
		/// </summary>
		public string ADDRESS
		{
			set{ _address=value;}
			get{return _address;}
		}
		/// <summary>
		/// 删除标记（0表示正常，1表示删除）
		/// </summary>
		public int? DELFLAG
		{
			set{ _delflag=value;}
			get{return _delflag;}
		}
		/// <summary>
		/// 是否是新的用户，1表示新用户；0表示老用户。新用户要更新自己的学段等信息，才能进入系统
		/// </summary>
		public int? ISNEWS
		{
			set{ _isnews=value;}
			get{return _isnews;}
		}
		/// <summary>
		/// 政治面貌
		/// </summary>
		public int? POLITICSID
		{
			set{ _politicsid=value;}
			get{return _politicsid;}
		}
		/// <summary>
		/// 民族的ID
		/// </summary>
		public int? NATIONALID
		{
			set{ _nationalid=value;}
			get{return _nationalid;}
		}
		/// <summary>
		/// 所属区县ID
		/// </summary>
		public int? QUXIANID
		{
			set{ _quxianid=value;}
			get{return _quxianid;}
		}
		/// <summary>
		/// 学区ID
		/// </summary>
		public int? XUQUID
		{
			set{ _xuquid=value;}
			get{return _xuquid;}
		}
		/// <summary>
		/// 学校类别
		/// </summary>
		public int? SCHOOL_TYPEID
		{
			set{ _school_typeid=value;}
			get{return _school_typeid;}
		}
		/// <summary>
		/// 学校的ID
		/// </summary>
		public int? SCHOOLID
		{
			set{ _schoolid=value;}
			get{return _schoolid;}
		}
		/// <summary>
		/// 用户类型（用户组的ID）
		/// </summary>
		public string USERGroupsID
		{
			set{ _usergroupsid=value;}
			get{return _usergroupsid;}
		}
		/// <summary>
		/// 用户级别（数字越低，级别越高）
		/// </summary>
		public string USERLEVES
		{
			set{ _userleves=value;}
			get{return _userleves;}
        }
        /// <summary>
        /// 用户的职务ID
        /// </summary>
        public string USERJOBID
        {
            set { _userjobid = value; }
            get { return _userjobid; }
        }
        /// <summary>
        /// 学段
        /// </summary>
        public string XUEDUANID
        {
            set { _xueduanid = value; }
            get { return _xueduanid; }
        }
        /// <summary>
        /// 学科ID
        /// </summary>
        public string XUEKEID
        {
            set { _xuekeid = value; }
            get { return _xuekeid; }
        }
        /// <summary>
        /// 任职年级
        /// </summary>
        public string NIANJIID
        {
            set { _nianjiid = value; }
            get { return _nianjiid; }
        }
        /// <summary>
        /// 职称ID
        /// </summary>
        public string ZHICHENGID
        {
            set { _zhichengid = value; }
            get { return _zhichengid; }
        }
		/// <summary>
		/// 进修编号
		/// </summary>
		public string TEACHNUMBER
		{
			set{ _teachnumber=value;}
			get{return _teachnumber;}
		}
		/// <summary>
		/// 专业代码
		/// </summary>
		public int? ZYDM
		{
			set{ _zydm=value;}
			get{return _zydm;}
		}
		/// <summary>
		/// 毕业学校
		/// </summary>
		public string SchoolTag
		{
			set{ _schooltag=value;}
			get{return _schooltag;}
		}
		/// <summary>
		/// 毕业时间
		/// </summary>
		public DateTime? TIMESOFGRADUATE
		{
			set{ _timesofgraduate=value;}
			get{return _timesofgraduate;}
		}
		/// <summary>
		/// 学历ID
		/// </summary>
		public int? XUELIID
		{
			set{ _xueliid=value;}
			get{return _xueliid;}
		}
		/// <summary>
		/// 学位ID
		/// </summary>
		public int? XUEWEIID
		{
			set{ _xueweiid=value;}
			get{return _xueweiid;}
		}
		/// <summary>
		/// 教龄
		/// </summary>
		public int? SCHOOLAGES
		{
			set{ _schoolages=value;}
			get{return _schoolages;}
        }
        /// <summary>
        /// 培训类型
        /// </summary>
        public string TrainingTypeID
        {
            set { _trainingtypeid = value; }
            get { return _trainingtypeid; }
        }
		/// <summary>
		/// 培训对象的ID
		/// </summary>
		public int? TrainingObjectID
		{
			set{ _trainingobjectid=value;}
			get{return _trainingobjectid;}
		}
		/// <summary>
		/// 照片
		/// </summary>
		public string IMAGES
		{
			set{ _images=value;}
			get{return _images;}
		}
		/// <summary>
		/// 总的登录时间（秒）
		/// </summary>
		public int? LOGINTIMES
		{
			set{ _logintimes=value;}
			get{return _logintimes;}
		}
		/// <summary>
		/// 总的登录次数
		/// </summary>
		public int? LOGINNUMS
		{
			set{ _loginnums=value;}
			get{return _loginnums;}
		}
		/// <summary>
		/// 学生的状态（正常、被调出）
		/// </summary>
		public string STATUS
		{
			set{ _status=value;}
			get{return _status;}
		}
		/// <summary>
		/// 用户最后一次更新数据的时间
		/// </summary>
		public DateTime? ModifyDatetime
		{
			set{ _modifydatetime=value;}
			get{return _modifydatetime;}
        }
        /// <summary>
        /// 工作学期
        /// </summary>
        public string WorkTerm
        {
            set { _workterm = value; }
            get { return _workterm; }
        }
		#endregion Model

	}
}

