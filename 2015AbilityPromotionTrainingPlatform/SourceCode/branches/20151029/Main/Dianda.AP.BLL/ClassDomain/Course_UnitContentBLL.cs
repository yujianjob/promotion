﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianda.AP.BLL
{
    public partial class Course_UnitContentBLL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int GetUnitCountByClassAndUnitType(int classId,string unitType)
        {
            return this.dal.GetUnitCountByClassAndUnitType(classId, unitType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public DataTable GetUnitByClassAndUnitType(int classId, string unitType)
        {
            return this.dal.GetUnitByClassAndUnitType(classId, unitType);
        }
    }
}