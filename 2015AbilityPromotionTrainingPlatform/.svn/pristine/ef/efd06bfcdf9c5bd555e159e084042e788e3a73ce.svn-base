using Dianda.AP.DAL;
using Dianda.AP.Model;
using System;
using System.Collections.Generic;

namespace Dianda.AP.BLL
{
    public class Sys_MessageBLL
    {
        Sys_MessageDAL dal = new Sys_MessageDAL();

        /// <summary>
        /// 取得一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public Sys_Message GetModel(int id, string where)
        {
            return dal.GetModel(id, where);
        }

        /// <summary>
        /// 新增一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(Sys_Message model)
        {
            return dal.Add(model) > 0;
        }

        /// <summary>
        /// 更新一条记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Update(Sys_Message model)
        {
            return dal.Update(model) > 0;
        }

        /// <summary>
        /// 获取数据集
        /// </summary>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public List<Sys_Message> GetList(string where, string orderBy)
        {
            return dal.GetList(where, orderBy);
        }

        /// <summary>
        /// 获取分页数据集
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public List<Sys_Message> GetList(int pageSize, int pageIndex, string where, string orderBy, out int recordCount)
        {
            return dal.GetList(pageSize, pageIndex, where, orderBy, out recordCount);
        }

        public int UpdStatus(int id)
        {
            return dal.UpdStatus(id);
        }
        public int DelDetails(int id,string UpdString)
        {
            return dal.DelDetails(id, UpdString);
        }
        public int BatchDel(string IdList,string UpdString)
        {
            return dal.BatchDel(IdList, UpdString);
        }
        public bool AddMessage(Sys_Message model)
        {
            return dal.AddMessage(model);
        }
    }
}
