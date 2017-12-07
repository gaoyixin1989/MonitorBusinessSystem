using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.DrinkUnder;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.DrinkUnder
{
    /// <summary>
    /// 功能：地下饮用水
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPDrinkUnderItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_DRINK_UNDER_ITEM " + this.BuildWhereStatement(tEnvPDrinkUnderItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDrinkUnderItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DRINK_UNDER_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPDrinkUnderItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDrinkUnderItemVo Details(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DRINK_UNDER_ITEM " + this.BuildWhereStatement(tEnvPDrinkUnderItem));
            return SqlHelper.ExecuteObject(new TEnvPDrinkUnderItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDrinkUnderItemVo> SelectByObject(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_DRINK_UNDER_ITEM " + this.BuildWhereStatement(tEnvPDrinkUnderItem));
            return SqlHelper.ExecuteObjectList(tEnvPDrinkUnderItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_DRINK_UNDER_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPDrinkUnderItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            string strSQL = "select * from T_ENV_P_DRINK_UNDER_ITEM " + this.BuildWhereStatement(tEnvPDrinkUnderItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">对象</param>
        /// <returns></returns>
        public TEnvPDrinkUnderItemVo SelectByObject(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            string strSQL = "select * from T_ENV_P_DRINK_UNDER_ITEM " + this.BuildWhereStatement(tEnvPDrinkUnderItem);
            return SqlHelper.ExecuteObject(new TEnvPDrinkUnderItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPDrinkUnderItem, TEnvPDrinkUnderItemVo.T_ENV_P_DRINK_UNDER_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDrinkUnderItem, TEnvPDrinkUnderItemVo.T_ENV_P_DRINK_UNDER_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPDrinkUnderItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDrinkUnderItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem_UpdateSet, TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDrinkUnderItem_UpdateSet, TEnvPDrinkUnderItemVo.T_ENV_P_DRINK_UNDER_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPDrinkUnderItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_DRINK_UNDER_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            string strSQL = "delete from T_ENV_P_DRINK_UNDER_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPDrinkUnderItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPDrinkUnderItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPDrinkUnderItemVo tEnvPDrinkUnderItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPDrinkUnderItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPDrinkUnderItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPDrinkUnderItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPDrinkUnderItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPDrinkUnderItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPDrinkUnderItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPDrinkUnderItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPDrinkUnderItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPDrinkUnderItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPDrinkUnderItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPDrinkUnderItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPDrinkUnderItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPDrinkUnderItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPDrinkUnderItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPDrinkUnderItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPDrinkUnderItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPDrinkUnderItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPDrinkUnderItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPDrinkUnderItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
