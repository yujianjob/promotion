using System;

namespace Dianda.AP.Model
{
	public partial class Traning_Teacher
	{
		public int Id { get; set; }

		public int TraningId { get; set; }

		public int? PlatformManagerId { get; set; }

		public int Status { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
