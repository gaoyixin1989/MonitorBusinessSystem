using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.MudRiver;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.MudRiver
{
    /// <summary>
    /// 功能：沉积物（河流）
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPMudRiverVAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPMudRiverV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            string strSQL = "select Count(*) from T_ENV_P_MUD_RIVER_V " + this.BuildWhereStatement(tEnvPMudRiverV);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPMudRiverVVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_MUD_RIVER_V  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPMudRiverVVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPMudRiverV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPMudRiverVVo Details(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            string strSQL = String.Format("select * from  T_ENV_P_MUD_RIVER_V " + this.BuildWhereStatement(tEnvPMudRiverV));
            return SqlHelper.ExecuteObject(new TEnvPMudRiverVVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPMudRiverV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPMudRiverVVo> SelectByObject(TEnvPMudRiverVVo tEnvPMudRiverV, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_MUD_RIVER_V " + this.BuildWhereStatement(tEnvPMudRiverV));
            return SqlHelper.ExecuteObjectList(tEnvPMudRiverV, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPMudRiverV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPMudRiverVVo tEnvPMudRiverV, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_MUD_RIVER_V {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPMudRiverV));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPMudRiverV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            string strSQL = "select * from T_ENV_P_MUD_RIVER_V " + this.BuildWhereStatement(tEnvPMudRiverV);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPMudRiverV">对象</param>
        /// <returns></returns>
        public TEnvPMudRiverVVo SelectByObject(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            string strSQL = "select * from T_ENV_P_MUD_RIVER_V " + this.BuildWhereStatement(tEnvPMudRiverV);
            return SqlHelper.ExecuteObject(new TEnvPMudRiverVVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPMudRiverV">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPMudRiverV, TEnvPMudRiverVVo.T_ENV_P_MUD_RIVER_V_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudRiverV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPMudRiverV, TEnvPMudRiverVVo.T_ENV_P_MUD_RIVER_V_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPMudRiverV.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudRiverV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPMudRiverV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudRiverVVo tEnvPMudRiverV_UpdateSet, TEnvPMudRiverVVo tEnvPMudRiverV_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPMudRiverV_UpdateSet, TEnvPMudRiverVVo.T_ENV_P_MUD_RIVER_V_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPMudRiverV_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_MUD_RIVER_V where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            string strSQL = "delete from T_ENV_P_MUD_RIVER_V ";
            strSQL += this.BuildWhereStatement(tEnvPMudRiverV);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPMudRiverV"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPMudRiverVVo tEnvPMudRiverV)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPMudRiverV)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPMudRiverV.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPMudRiverV.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvPMudRiverV.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvPMudRiverV.SECTION_ID.ToString()));
                }
                //垂线名称
                if (!String.IsNullOrEmpty(tEnvPMudRiverV.VERTICAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERTICAL_NAME = '{0}'", tEnvPMudRiverV.VERTICAL_NAME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPMudRiverV.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPMudRiverV.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPMudRiverV.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPMudRiverV.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPMudRiverV.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPMudRiverV.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPMudRiverV.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPMudRiverV.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPMudRiverV.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPMudRiverV.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
