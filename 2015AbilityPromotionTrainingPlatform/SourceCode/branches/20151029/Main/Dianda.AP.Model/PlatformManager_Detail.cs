using System;

namespace Dianda.AP.Model
{
	public partial class PlatformManager_Detail
	{
		public int Id { get; set; }

		public int AccountId { get; set; }

		public int PartitionId { get; set; }

		public int Status { get; set; }

		public int OrganId { get; set; }

		public int Level { get; set; }

		public int GroupId { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

	}
}
