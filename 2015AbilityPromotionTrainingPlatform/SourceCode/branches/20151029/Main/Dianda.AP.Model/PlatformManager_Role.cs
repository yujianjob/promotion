using System;

namespace Dianda.AP.Model
{
	public partial class PlatformManager_Role
	{
		public int Id { get; set; }

		public string Title { get; set; }

		public string Content { get; set; }

		public string PagePath { get; set; }

		public int sort { get; set; }

		public string MenuText { get; set; }

		public string MenuPath { get; set; }

		public int? ParentId { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

		public bool? IsFolder { get; set; }

	}
}
