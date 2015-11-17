using System;

namespace Dianda.AP.Model
{
	public partial class Course_UnitQuestion
	{
		public int Id { get; set; }

		public string Verson { get; set; }

		public int UnitContent { get; set; }

		public string Content { get; set; }

		public int QTtype { get; set; }

		public string Question { get; set; }

		public string Answer { get; set; }

		public double Credit { get; set; }

		public bool Display { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
