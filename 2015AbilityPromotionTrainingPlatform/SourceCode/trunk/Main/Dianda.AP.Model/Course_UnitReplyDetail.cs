using System;

namespace Dianda.AP.Model
{
	public partial class Course_UnitReplyDetail
	{
		public int Id { get; set; }

		public int UnitContent { get; set; }

		public int ClassId { get; set; }

		public string Content { get; set; }

		public int AccountId { get; set; }

		public int? ParentReplyId { get; set; }

		public string AttList { get; set; }

		public bool Display { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}

    /// <summary>
    /// Course_UnitReplyDetailĞÅÏ¢×·¼Ó
    /// </summary>
    public partial class Course_UnitReplyDetailOther
    {
        public string NickName { get; set; }

        public string Pic { get; set; }

        public int OrganDetailId { get; set; }

        public string OrganDetailTitle { get; set; }

        public Course_UnitReplyDetail CourseUnitReplyDetail { get; set; }
    }
}
