using System;

namespace Dianda.AP.Model
{
	public partial class Member_PlanExemption
	{
		public int Id { get; set; }

		public int PlanId { get; set; }

		public int Status { get; set; }

		public string Remark { get; set; }

		public double? Credits { get; set; }

		public int? PEType { get; set; }

		public int? Creater { get; set; }

		public int? AccountId { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }
	}
}
