using System;

namespace Dianda.AP.Model
{
	public partial class Member_InOut
	{
		public int Id { get; set; }

		public int AccountId { get; set; }

		public int? SourceOrganId { get; set; }

		public int? TargetOrganId { get; set; }

		public int? Creater { get; set; }

		public int Status { get; set; }

		public string ApplyRemark { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
