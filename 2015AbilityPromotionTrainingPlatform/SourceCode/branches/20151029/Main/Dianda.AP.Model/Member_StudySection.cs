using System;

namespace Dianda.AP.Model
{
	public partial class Member_StudySection
	{
		public int Id { get; set; }

		public int AccountId { get; set; }

		public int StudySection { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
