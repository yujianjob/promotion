using System;

namespace Dianda.AP.Model
{
	public partial class Member_TeachSubject
	{
		public int Id { get; set; }

		public int AccountId { get; set; }

		public int TeachSubject { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
