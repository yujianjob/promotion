using System;

namespace Dianda.AP.Model
{
	public partial class Class_HomeWork
	{
		public int Id { get; set; }

		public int HomeWorkId { get; set; }

		public int ClassId { get; set; }

		public string Content { get; set; }

		public int AccountId { get; set; }

		public int? ParentReplyId { get; set; }

		public string AttList { get; set; }

		public bool Display { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
