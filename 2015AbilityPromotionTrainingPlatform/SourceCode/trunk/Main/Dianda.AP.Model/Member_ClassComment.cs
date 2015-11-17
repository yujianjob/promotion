using System;

namespace Dianda.AP.Model
{
	public partial class Member_ClassComment
	{
		public int Id { get; set; }

		public int ClassId { get; set; }

		public int AccountId { get; set; }

		public string Content { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
