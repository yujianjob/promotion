using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.Model.Entrance.Home
{
    [Serializable]
    public class PlatformGroups
    {
        /// <summary>
        /// 权限组Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 权限组名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 拥有的权限
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 管理员组织Id
        /// </summary>
        public int ManageOrganId { get; set; }

        /// <summary>
        /// 管理员Id
        /// </summary>
        public int ManagerId { get; set; }
    }
}
