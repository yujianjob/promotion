using System;

namespace Dianda.AP.Model
{
	public partial class Member_PlanOverall
	{
		public int PlanId { get; set; }

		public int AccountId { get; set; }

		public int? Result { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
