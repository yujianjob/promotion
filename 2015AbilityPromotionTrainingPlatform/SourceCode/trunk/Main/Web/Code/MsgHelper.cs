using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Dianda.AP.Model;
using Dianda.AP.BLL;

namespace Web.Code
{
    public static class MsgHelper
    {
        static Sys_MessageBLL bll = new Sys_MessageBLL();
        /// <summary>
        /// 发送通知提醒
        /// </summary>
        /// <param name="AccountId">接收人ID</param>
        /// <param name="Creater">发送人ID</param>
        /// <param name="PartitionId">接收人所属大区ID</param>
        /// <param name="Title">通知标题</param>
        /// <param name="Content">通知内容</param>
        /// <returns>返回是否发送成功 值：true false</returns>
        public static bool sendMsg(int AccountId, int Creater, int PartitionId, string Title, string Content)
        {
            Sys_Message model = new Sys_Message();
            model.AccountId = AccountId;
            model.Creater = Creater;
            model.PartitionId = PartitionId;
            model.Title = Title;
            model.Content = Content;
            model.Status = 0;
            model.Display = true;
            model.Delflag = false;
            model.CreateDate = DateTime.Now;
            return bll.AddMessage(model);
        }
    }
}