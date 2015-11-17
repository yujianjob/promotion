using System;
namespace Dianda.AP.Model
{
	/// <summary>
	/// 实体类schoolcode 。(属性说明自动提取数据库字段的描述信息)
	/// </summary>
	[Serializable]
	public class schoolcode
	{
		public schoolcode()
		{}
		#region Model
		private int _typeid;
		private string _type;
		private string _typename;
		private string _class;
		private string _classname;
		private string _scope;
		/// <summary>
		/// 
		/// </summary>
		public int typeid
		{
			set{ _typeid=value;}
			get{return _typeid;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string type
		{
			set{ _type=value;}
			get{return _type;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string typename
		{
			set{ _typename=value;}
			get{return _typename;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string class1
		{
			set{ _class=value;}
			get{return _class;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string classname
		{
			set{ _classname=value;}
			get{return _classname;}
		}
		/// <summary>
		/// 
		/// </summary>
		public string scope
		{
			set{ _scope=value;}
			get{return _scope;}
		}
		#endregion Model

        public string DistrictID { get; set; }
        public string XueQuID { get; set; }
        public string SchoolCategoryID { get; set; }
        public string ID { get; set; }

	}
}

