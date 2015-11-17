using System;

namespace Dianda.AP.Model
{
	public partial class Training_Credits
	{
		public int Id { get; set; }

		public int PlanId { get; set; }

		public int TraningField { get; set; }

		public int Level { get; set; }

		public double MinValue { get; set; }

		public double MaxValue { get; set; }

		public int Sort { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
