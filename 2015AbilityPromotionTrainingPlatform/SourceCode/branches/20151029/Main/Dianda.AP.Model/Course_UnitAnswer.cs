using System;

namespace Dianda.AP.Model
{
	public partial class Course_UnitAnswer
	{
		public int Id { get; set; }

		public int AnswerResult { get; set; }

		public int Question { get; set; }

		public string Answer { get; set; }

		public bool Result { get; set; }

		public int AccountId { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
