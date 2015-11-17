using System;

namespace Dianda.AP.Model
{
	public partial class Class_HomeWorkMission
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int ClassId { get; set; }

		public string Content { get; set; }

		public DateTime? StartDate { get; set; }

		public DateTime? EndDate { get; set; }

		public string AttList { get; set; }

		public int? Creater { get; set; }

		public bool Display { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
