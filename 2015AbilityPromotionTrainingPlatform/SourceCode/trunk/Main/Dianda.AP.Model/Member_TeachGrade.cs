using System;

namespace Dianda.AP.Model
{
	public partial class Member_TeachGrade
	{
		public int Id { get; set; }

		public int AccountId { get; set; }

		public int TeachGrade { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
