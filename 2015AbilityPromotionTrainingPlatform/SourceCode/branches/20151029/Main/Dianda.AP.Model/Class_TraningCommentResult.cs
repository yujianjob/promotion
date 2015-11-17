using System;

namespace Dianda.AP.Model
{
	public partial class Class_TraningCommentResult
	{
		public int Id { get; set; }

		public string Verson { get; set; }

		public int ClassId { get; set; }

		public int Score { get; set; }

		public int AccountId { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
