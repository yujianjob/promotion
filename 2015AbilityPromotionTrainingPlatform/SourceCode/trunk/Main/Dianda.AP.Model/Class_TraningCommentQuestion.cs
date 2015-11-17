using System;

namespace Dianda.AP.Model
{
    public partial class Class_TraningCommentQuestion
    {
        public int Id { get; set; }

        public string Verson { get; set; }

        public string Content { get; set; }

        public int QTtype { get; set; }

        public string Question { get; set; }

        public bool Display { get; set; }

        public bool Delflag { get; set; }

        public DateTime CreateDate { get; set; }

    }

    public partial class Class_TraningCommentQuestionOther
    {
        /// <summary>
        /// Class_TraningCommentQuestion Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 题目选项
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 该选项分值
        /// </summary>
        public double? Credits { get; set; }
    }
}
