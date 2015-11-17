using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model.Entrance.Login
{
    [Serializable]
    public class LoginInfo
    {
        public int UserId { get; set; }

        public int PartitionId { get; set; }
    }
}
