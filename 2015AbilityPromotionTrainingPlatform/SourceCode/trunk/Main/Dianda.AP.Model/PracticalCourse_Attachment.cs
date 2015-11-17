using System;

namespace Dianda.AP.Model
{
	public partial class PracticalCourse_Attachment
	{
		public int Id { get; set; }

		public int PracticalCourseId { get; set; }

		public string Title { get; set; }

		public string Link { get; set; }

		public int Sort { get; set; }

		public bool Display { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
