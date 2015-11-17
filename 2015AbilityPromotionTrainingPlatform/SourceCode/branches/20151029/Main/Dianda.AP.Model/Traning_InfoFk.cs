using System;

namespace Dianda.AP.Model
{
	public partial class Traning_InfoFk
	{
		public int Id { get; set; }

		public int CategoryType { get; set; }

		public string Title { get; set; }

		public string Remark { get; set; }

		public int? ParentId { get; set; }

		public int Sort { get; set; }

		public bool Display { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
