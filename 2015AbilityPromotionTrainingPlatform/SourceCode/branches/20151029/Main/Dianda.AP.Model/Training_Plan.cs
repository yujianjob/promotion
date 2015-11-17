using System;

namespace Dianda.AP.Model
{
	public partial class Training_Plan
	{
		public int Id { get; set; }

		public int PartitionId { get; set; }

		public string Title { get; set; }

		public bool IsOpen { get; set; }

		public int Sort { get; set; }

		public bool Display { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

        public DateTime SignUpStartTime { get; set; }

        public DateTime SignUpEndTime { get; set; }

        public DateTime OpenClassFrom { get; set; }

        public DateTime OpenClassTo { get; set; }

	}
}
