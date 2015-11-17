using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Dianda.AP.Model
{
	public partial class Class_Notice
	{
		public int Id { get; set; }

        [DisplayName("班级ID")]
        [Required(ErrorMessage = "班级ID不能为空")]
		public int ClassId { get; set; }

        [DisplayName("标题")]
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(30, ErrorMessage = "标题字数不能超过30")]
		public string Title { get; set; }

        [DisplayName("内容")]
        [Required(ErrorMessage = "内容不能为空")]
        [StringLength(4000, ErrorMessage = "标题字数不能超过4000")]
		public string Content { get; set; }

        [DisplayName("发布人")]
        [Required(ErrorMessage = "发布人不能为空")]
		public int? Creater { get; set; }

        [DisplayName("是否显示")]
		public bool Display { get; set; }

		public bool Delflag { get; set; }

		public DateTime CreateDate { get; set; }

        public string OrganTitle { get; set; }
	}
}
