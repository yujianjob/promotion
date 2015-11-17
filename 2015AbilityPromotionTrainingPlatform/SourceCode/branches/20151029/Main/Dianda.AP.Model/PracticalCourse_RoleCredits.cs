using System;

namespace Dianda.AP.Model
{
	public partial class PracticalCourse_RoleCredits
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int TraingField { get; set; }

		public int TraingCategory { get; set; }

		public int? TraingTopic { get; set; }

		public int? RoleId { get; set; }

		public double Credits { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
