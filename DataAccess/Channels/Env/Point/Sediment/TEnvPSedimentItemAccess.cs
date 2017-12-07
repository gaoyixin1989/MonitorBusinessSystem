using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Sediment;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.Sediment
{
    /// <summary>
    /// 功能：底泥重金属
    /// 创建日期：2014-10-23
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSedimentItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSedimentItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_SEDIMENT_ITEM " + this.BuildWhereStatement(tEnvPSedimentItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSedimentItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEDIMENT_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPSedimentItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSedimentItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSedimentItemVo Details(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEDIMENT_ITEM " + this.BuildWhereStatement(tEnvPSedimentItem));
            return SqlHelper.ExecuteObject(new TEnvPSedimentItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSedimentItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSedimentItemVo> SelectByObject(TEnvPSedimentItemVo tEnvPSedimentItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_SEDIMENT_ITEM " + this.BuildWhereStatement(tEnvPSedimentItem));
            return SqlHelper.ExecuteObjectList(tEnvPSedimentItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSedimentItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSedimentItemVo tEnvPSedimentItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_SEDIMENT_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPSedimentItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSedimentItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            string strSQL = "select * from T_ENV_P_SEDIMENT_ITEM " + this.BuildWhereStatement(tEnvPSedimentItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSedimentItem">对象</param>
        /// <returns></returns>
        public TEnvPSedimentItemVo SelectByObject(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            string strSQL = "select * from T_ENV_P_SEDIMENT_ITEM " + this.BuildWhereStatement(tEnvPSedimentItem);
            return SqlHelper.ExecuteObject(new TEnvPSedimentItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPSedimentItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPSedimentItem, TEnvPSedimentItemVo.T_ENV_P_SEDIMENT_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSedimentItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSedimentItem, TEnvPSedimentItemVo.T_ENV_P_SEDIMENT_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPSedimentItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSedimentItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSedimentItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSedimentItemVo tEnvPSedimentItem_UpdateSet, TEnvPSedimentItemVo tEnvPSedimentItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSedimentItem_UpdateSet, TEnvPSedimentItemVo.T_ENV_P_SEDIMENT_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPSedimentItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_SEDIMENT_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            string strSQL = "delete from T_ENV_P_SEDIMENT_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPSedimentItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPSedimentItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPSedimentItemVo tEnvPSedimentItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPSedimentItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPSedimentItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPSedimentItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPSedimentItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPSedimentItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPSedimentItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPSedimentItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPSedimentItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPSedimentItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPSedimentItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPSedimentItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPSedimentItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPSedimentItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPSedimentItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPSedimentItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPSedimentItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPSedimentItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPSedimentItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPSedimentItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
