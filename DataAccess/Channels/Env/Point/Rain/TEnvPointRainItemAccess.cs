using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Point.Rain;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Point.Rain
{
    /// <summary>
    /// 功能：降水监测点监测项目表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPointRainItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointRainItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointRainItemVo tEnvPointRainItem) 
        {
            string strSQL = "select Count(*) from T_ENV_P_RAIN_ITEM " + this.BuildWhereStatement(tEnvPointRainItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointRainItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RAIN_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPointRainItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointRainItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointRainItemVo Details(TEnvPointRainItemVo tEnvPointRainItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RAIN_ITEM " + this.BuildWhereStatement(tEnvPointRainItem));
            return SqlHelper.ExecuteObject(new TEnvPointRainItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointRainItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointRainItemVo> SelectByObject(TEnvPointRainItemVo tEnvPointRainItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_RAIN_ITEM " + this.BuildWhereStatement(tEnvPointRainItem));
            return SqlHelper.ExecuteObjectList(tEnvPointRainItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointRainItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointRainItemVo tEnvPointRainItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_RAIN_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPointRainItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointRainItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointRainItemVo tEnvPointRainItem)
        {
            string strSQL = "select * from T_ENV_P_RAIN_ITEM " + this.BuildWhereStatement(tEnvPointRainItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointRainItem">对象</param>
        /// <returns></returns>
        public TEnvPointRainItemVo SelectByObject(TEnvPointRainItemVo tEnvPointRainItem)
        {
            string strSQL = "select * from T_ENV_P_RAIN_ITEM " + this.BuildWhereStatement(tEnvPointRainItem);
            return SqlHelper.ExecuteObject(new TEnvPointRainItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPointRainItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointRainItemVo tEnvPointRainItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPointRainItem, TEnvPointRainItemVo.T_ENV_POINT_RAIN_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointRainItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointRainItemVo tEnvPointRainItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointRainItem, TEnvPointRainItemVo.T_ENV_POINT_RAIN_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPointRainItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointRainItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointRainItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointRainItemVo tEnvPointRainItem_UpdateSet, TEnvPointRainItemVo tEnvPointRainItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointRainItem_UpdateSet, TEnvPointRainItemVo.T_ENV_POINT_RAIN_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPointRainItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_RAIN_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPointRainItemVo tEnvPointRainItem)
        {
            string strSQL = "delete from T_ENV_P_RAIN_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPointRainItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPointRainItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPointRainItemVo tEnvPointRainItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointRainItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPointRainItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointRainItem.ID.ToString()));
                }
                //降水监测点ID，对应T_BAS_POINT_RAIN表主键
                if (!String.IsNullOrEmpty(tEnvPointRainItem.POINT_ID.ToString().Trim())) 
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPointRainItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPointRainItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPointRainItem.ITEM_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPointRainItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPointRainItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPointRainItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointRainItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPointRainItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointRainItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPointRainItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointRainItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPointRainItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointRainItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPointRainItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointRainItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
