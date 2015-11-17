using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace Dianda.AP.Model
{
	public partial class News_Detail
    {
        [DisplayName("ID")]
        [Required(ErrorMessage = "ID不能为空")]
		public int Id { get; set; }

        [DisplayName("标题")]
        [Required(ErrorMessage = "标题不能为空")]
        [StringLength(20, ErrorMessage = "标题字数不能超过20")]
        public string Title { get; set; }

        [DisplayName("内容")]
        [Required(ErrorMessage = "内容不能为空")]
        [StringLength(2000, ErrorMessage = "内容字数不能超过2000")]
		public string Content { get; set; }

        [DisplayName("级别")]
        [Required(ErrorMessage = "级别不能为空")]
		public int? Level { get; set; }

        [DisplayName("显示顺序")]
        [Required(ErrorMessage = "显示顺序不能为空")]
		public int sort { get; set; }

        [DisplayName("发布组织")]
        [Required(ErrorMessage = "发布组织不能为空")]
		public int OrganId { get; set; }

        [DisplayName("发布组织名称")]
        [Required(ErrorMessage = "发布组织名称不能为空")]
        public string OrganTitle { get; set; }

        [DisplayName("发布人")]
        [Required(ErrorMessage = "发布人不能为空")]
		public int Creater { get; set; }

        [DisplayName("所属计划")]
        [Required(ErrorMessage = "所属计划不能为空")]
		public int PlanId { get; set; }

        [DisplayName("是否显示")]
        [Required(ErrorMessage = "是否显示不能为空")]
		public bool Display { get; set; }

        [DisplayName("删除标记")]
        [Required(ErrorMessage = "删除标记不能为空")]
		public bool Delflag { get; set; }

        [DisplayName("计划名称")]
        [Required(ErrorMessage = "计划名称不能为空")]
        public string PlanTitle { get; set; }

        [DisplayName("发布日期")]
        [Required(ErrorMessage = "发布日期不能为空")]
		public DateTime PublishDate { get; set; }

        [DisplayName("发布时间")]
        [Required(ErrorMessage = "发布时间不能为空")]
		public DateTime CreateDate { get; set; }

	}
}
