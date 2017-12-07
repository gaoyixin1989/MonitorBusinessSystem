using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverPlan;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.RiverPlan
{
    /// <summary>
    /// 功能：规划断面
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverPlanVAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverPlanV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            string strSQL = "select Count(*) from T_ENV_P_RIVER_PLAN_V " + this.BuildWhereStatement(tEnvPRiverPlanV);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverPlanVVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_PLAN_V  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPRiverPlanVVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverPlanV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverPlanVVo Details(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_PLAN_V " + this.BuildWhereStatement(tEnvPRiverPlanV));
            return SqlHelper.ExecuteObject(new TEnvPRiverPlanVVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverPlanV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverPlanVVo> SelectByObject(TEnvPRiverPlanVVo tEnvPRiverPlanV, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_RIVER_PLAN_V " + this.BuildWhereStatement(tEnvPRiverPlanV));
            return SqlHelper.ExecuteObjectList(tEnvPRiverPlanV, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverPlanV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverPlanVVo tEnvPRiverPlanV, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_RIVER_PLAN_V {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPRiverPlanV));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverPlanV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            string strSQL = "select * from T_ENV_P_RIVER_PLAN_V " + this.BuildWhereStatement(tEnvPRiverPlanV);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverPlanV">对象</param>
        /// <returns></returns>
        public TEnvPRiverPlanVVo SelectByObject(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            string strSQL = "select * from T_ENV_P_RIVER_PLAN_V " + this.BuildWhereStatement(tEnvPRiverPlanV);
            return SqlHelper.ExecuteObject(new TEnvPRiverPlanVVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPRiverPlanV">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPRiverPlanV, TEnvPRiverPlanVVo.T_ENV_P_RIVER_PLAN_V_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverPlanV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverPlanV, TEnvPRiverPlanVVo.T_ENV_P_RIVER_PLAN_V_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPRiverPlanV.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverPlanV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverPlanV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverPlanVVo tEnvPRiverPlanV_UpdateSet, TEnvPRiverPlanVVo tEnvPRiverPlanV_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverPlanV_UpdateSet, TEnvPRiverPlanVVo.T_ENV_P_RIVER_PLAN_V_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPRiverPlanV_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_RIVER_PLAN_V where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            string strSQL = "delete from T_ENV_P_RIVER_PLAN_V ";
            strSQL += this.BuildWhereStatement(tEnvPRiverPlanV);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPRiverPlanV"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPRiverPlanVVo tEnvPRiverPlanV)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPRiverPlanV)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPRiverPlanV.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPRiverPlanV.ID.ToString()));
                }
                //规划断面ID
                if (!String.IsNullOrEmpty(tEnvPRiverPlanV.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvPRiverPlanV.SECTION_ID.ToString()));
                }
                //垂线名称
                if (!String.IsNullOrEmpty(tEnvPRiverPlanV.VERTICAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERTICAL_NAME = '{0}'", tEnvPRiverPlanV.VERTICAL_NAME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPRiverPlanV.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPRiverPlanV.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPRiverPlanV.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPRiverPlanV.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPRiverPlanV.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPRiverPlanV.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPRiverPlanV.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPRiverPlanV.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPRiverPlanV.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPRiverPlanV.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
