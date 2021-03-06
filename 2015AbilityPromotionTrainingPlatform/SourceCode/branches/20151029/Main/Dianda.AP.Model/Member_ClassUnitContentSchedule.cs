using System;

namespace Dianda.AP.Model
{
	public partial class Member_ClassUnitContentSchedule
	{
		public int Id { get; set; }

		public int ClassId { get; set; }

		public int? TrainingId { get; set; }

		public int AccountId { get; set; }

		public int UnitContent { get; set; }

		public bool Status { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

		public double? score { get; set; }

	}
}
