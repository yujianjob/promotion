using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model
{
    public partial class MyTestRound
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        public DateTime? UploadDate { get; set; }
    }
}
