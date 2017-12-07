using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Point.Solid;

namespace i3.DataAccess.Channels.Env.Point.Solid
{
    /// <summary>
    /// 功能：固废
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPSolidItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSolidItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSolidItemVo tEnvPSolidItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_SOLID_ITEM " + this.BuildWhereStatement(tEnvPSolidItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSolidItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SOLID_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPSolidItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSolidItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSolidItemVo Details(TEnvPSolidItemVo tEnvPSolidItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SOLID_ITEM " + this.BuildWhereStatement(tEnvPSolidItem));
            return SqlHelper.ExecuteObject(new TEnvPSolidItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSolidItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSolidItemVo> SelectByObject(TEnvPSolidItemVo tEnvPSolidItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_SOLID_ITEM " + this.BuildWhereStatement(tEnvPSolidItem));
            return SqlHelper.ExecuteObjectList(tEnvPSolidItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSolidItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSolidItemVo tEnvPSolidItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_SOLID_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPSolidItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSolidItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSolidItemVo tEnvPSolidItem)
        {
            string strSQL = "select * from T_ENV_P_SOLID_ITEM " + this.BuildWhereStatement(tEnvPSolidItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSolidItem">对象</param>
        /// <returns></returns>
        public TEnvPSolidItemVo SelectByObject(TEnvPSolidItemVo tEnvPSolidItem)
        {
            string strSQL = "select * from T_ENV_P_SOLID_ITEM " + this.BuildWhereStatement(tEnvPSolidItem);
            return SqlHelper.ExecuteObject(new TEnvPSolidItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPSolidItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSolidItemVo tEnvPSolidItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPSolidItem, TEnvPSolidItemVo.T_ENV_P_SOLID_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSolidItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSolidItemVo tEnvPSolidItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSolidItem, TEnvPSolidItemVo.T_ENV_P_SOLID_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPSolidItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSolidItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSolidItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSolidItemVo tEnvPSolidItem_UpdateSet, TEnvPSolidItemVo tEnvPSolidItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSolidItem_UpdateSet, TEnvPSolidItemVo.T_ENV_P_SOLID_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPSolidItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_SOLID_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPSolidItemVo tEnvPSolidItem)
        {
            string strSQL = "delete from T_ENV_P_SOLID_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPSolidItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPSolidItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPSolidItemVo tEnvPSolidItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPSolidItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPSolidItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPSolidItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPSolidItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPSolidItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPSolidItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPSolidItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPSolidItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPSolidItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPSolidItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPSolidItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPSolidItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPSolidItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPSolidItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPSolidItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPSolidItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPSolidItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPSolidItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPSolidItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
