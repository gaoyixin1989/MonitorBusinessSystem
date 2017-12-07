using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Point.MudSea;

namespace i3.DataAccess.Channels.Env.Point.MudSea
{
    /// <summary>
    /// 功能：沉积物（海水）
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPMudSeaVItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_MUD_SEA_V_ITEM " + this.BuildWhereStatement(tEnvPMudSeaVItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPMudSeaVItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_MUD_SEA_V_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPMudSeaVItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPMudSeaVItemVo Details(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_MUD_SEA_V_ITEM " + this.BuildWhereStatement(tEnvPMudSeaVItem));
            return SqlHelper.ExecuteObject(new TEnvPMudSeaVItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPMudSeaVItemVo> SelectByObject(TEnvPMudSeaVItemVo tEnvPMudSeaVItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_MUD_SEA_V_ITEM " + this.BuildWhereStatement(tEnvPMudSeaVItem));
            return SqlHelper.ExecuteObjectList(tEnvPMudSeaVItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPMudSeaVItemVo tEnvPMudSeaVItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_MUD_SEA_V_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPMudSeaVItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPMudSeaVItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            string strSQL = "select * from T_ENV_P_MUD_SEA_V_ITEM " + this.BuildWhereStatement(tEnvPMudSeaVItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">对象</param>
        /// <returns></returns>
        public TEnvPMudSeaVItemVo SelectByObject(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            string strSQL = "select * from T_ENV_P_MUD_SEA_V_ITEM " + this.BuildWhereStatement(tEnvPMudSeaVItem);
            return SqlHelper.ExecuteObject(new TEnvPMudSeaVItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPMudSeaVItem, TEnvPMudSeaVItemVo.T_ENV_P_MUD_SEA_V_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudSeaVItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPMudSeaVItem, TEnvPMudSeaVItemVo.T_ENV_P_MUD_SEA_V_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPMudSeaVItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPMudSeaVItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPMudSeaVItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPMudSeaVItemVo tEnvPMudSeaVItem_UpdateSet, TEnvPMudSeaVItemVo tEnvPMudSeaVItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPMudSeaVItem_UpdateSet, TEnvPMudSeaVItemVo.T_ENV_P_MUD_SEA_V_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPMudSeaVItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_MUD_SEA_V_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            string strSQL = "delete from T_ENV_P_MUD_SEA_V_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPMudSeaVItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPMudSeaVItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPMudSeaVItemVo tEnvPMudSeaVItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPMudSeaVItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPMudSeaVItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPMudSeaVItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPMudSeaVItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPMudSeaVItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPMudSeaVItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPMudSeaVItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPMudSeaVItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPMudSeaVItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPMudSeaVItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPMudSeaVItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPMudSeaVItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPMudSeaVItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPMudSeaVItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPMudSeaVItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPMudSeaVItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPMudSeaVItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPMudSeaVItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPMudSeaVItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
