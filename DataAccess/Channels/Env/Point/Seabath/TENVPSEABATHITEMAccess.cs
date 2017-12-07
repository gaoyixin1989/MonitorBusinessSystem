using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Point.Seabath;

namespace i3.DataAccess.Channels.Env.Point.Seabath
{
    /// <summary>
    /// 功能：海水浴场
    /// 创建日期：2013-06-18
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvPSeabathItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSeabathItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_SEABATH_ITEM " + this.BuildWhereStatement(tEnvPSeabathItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSeabathItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEABATH_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPSeabathItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSeabathItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSeabathItemVo Details(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_SEABATH_ITEM " + this.BuildWhereStatement(tEnvPSeabathItem));
            return SqlHelper.ExecuteObject(new TEnvPSeabathItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSeabathItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSeabathItemVo> SelectByObject(TEnvPSeabathItemVo tEnvPSeabathItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_SEABATH_ITEM " + this.BuildWhereStatement(tEnvPSeabathItem));
            return SqlHelper.ExecuteObjectList(tEnvPSeabathItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSeabathItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSeabathItemVo tEnvPSeabathItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_SEABATH_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPSeabathItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSeabathItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            string strSQL = "select * from T_ENV_P_SEABATH_ITEM " + this.BuildWhereStatement(tEnvPSeabathItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSeabathItem">对象</param>
        /// <returns></returns>
        public TEnvPSeabathItemVo SelectByObject(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            string strSQL = "select * from T_ENV_P_SEABATH_ITEM " + this.BuildWhereStatement(tEnvPSeabathItem);
            return SqlHelper.ExecuteObject(new TEnvPSeabathItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPSeabathItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPSeabathItem, TEnvPSeabathItemVo.T_ENV_P_SEABATH_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSeabathItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSeabathItem, TEnvPSeabathItemVo.T_ENV_P_SEABATH_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPSeabathItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSeabathItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSeabathItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSeabathItemVo tEnvPSeabathItem_UpdateSet, TEnvPSeabathItemVo tEnvPSeabathItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPSeabathItem_UpdateSet, TEnvPSeabathItemVo.T_ENV_P_SEABATH_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPSeabathItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_SEABATH_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            string strSQL = "delete from T_ENV_P_SEABATH_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPSeabathItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPSeabathItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPSeabathItemVo tEnvPSeabathItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPSeabathItem)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPSeabathItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPSeabathItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPSeabathItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPSeabathItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPSeabathItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPSeabathItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPSeabathItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPSeabathItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPSeabathItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPSeabathItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPSeabathItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPSeabathItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPSeabathItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPSeabathItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPSeabathItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPSeabathItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPSeabathItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPSeabathItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
