using System;

namespace Dianda.AP.Model
{
	public partial class Course_UnitDetail
	{
		public int Id { get; set; }

        public int? TrainingId { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public int? ParentId { get; set; }

		public bool Display { get; set; }

		public int Sort { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
