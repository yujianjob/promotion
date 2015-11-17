using System;

namespace Dianda.AP.Model
{
	public partial class Member_Job
	{
		public int Id { get; set; }

		public int AccountId { get; set; }

		public int Job { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
