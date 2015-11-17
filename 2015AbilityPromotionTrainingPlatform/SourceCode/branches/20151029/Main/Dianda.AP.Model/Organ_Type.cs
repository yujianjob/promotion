using System;

namespace Dianda.AP.Model
{
	public partial class Organ_Type
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public int? ParentId { get; set; }

		public int Permission { get; set; }

		public string Remark { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
