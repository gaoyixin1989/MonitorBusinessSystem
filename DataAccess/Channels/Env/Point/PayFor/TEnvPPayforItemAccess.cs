using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.PayFor;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.PayFor
{
    /// <summary>
    /// 功能：生态补偿
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPPayforItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPPayforItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPPayforItemVo tEnvPPayforItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_PAYFOR_ITEM " + this.BuildWhereStatement(tEnvPPayforItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPPayforItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_PAYFOR_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPPayforItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPPayforItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPPayforItemVo Details(TEnvPPayforItemVo tEnvPPayforItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_PAYFOR_ITEM " + this.BuildWhereStatement(tEnvPPayforItem));
            return SqlHelper.ExecuteObject(new TEnvPPayforItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPPayforItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPPayforItemVo> SelectByObject(TEnvPPayforItemVo tEnvPPayforItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_PAYFOR_ITEM " + this.BuildWhereStatement(tEnvPPayforItem));
            return SqlHelper.ExecuteObjectList(tEnvPPayforItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPPayforItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPPayforItemVo tEnvPPayforItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_PAYFOR_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPPayforItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPPayforItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPPayforItemVo tEnvPPayforItem)
        {
            string strSQL = "select * from T_ENV_P_PAYFOR_ITEM " + this.BuildWhereStatement(tEnvPPayforItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPPayforItem">对象</param>
        /// <returns></returns>
        public TEnvPPayforItemVo SelectByObject(TEnvPPayforItemVo tEnvPPayforItem)
        {
            string strSQL = "select * from T_ENV_P_PAYFOR_ITEM " + this.BuildWhereStatement(tEnvPPayforItem);
            return SqlHelper.ExecuteObject(new TEnvPPayforItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPPayforItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPPayforItemVo tEnvPPayforItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPPayforItem, TEnvPPayforItemVo.T_ENV_P_PAYFOR_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPayforItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPayforItemVo tEnvPPayforItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPPayforItem, TEnvPPayforItemVo.T_ENV_P_PAYFOR_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPPayforItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPayforItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPPayforItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPayforItemVo tEnvPPayforItem_UpdateSet, TEnvPPayforItemVo tEnvPPayforItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPPayforItem_UpdateSet, TEnvPPayforItemVo.T_ENV_P_PAYFOR_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPPayforItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_PAYFOR_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPPayforItemVo tEnvPPayforItem)
        {
            string strSQL = "delete from T_ENV_P_PAYFOR_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPPayforItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPPayforItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPPayforItemVo tEnvPPayforItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPPayforItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPPayforItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPPayforItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPPayforItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPPayforItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPPayforItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPPayforItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPPayforItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPPayforItem.ANALYSIS_ID.ToString()));
                }
                //考核水质标准
                if (!String.IsNullOrEmpty(tEnvPPayforItem.STANDARD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STANDARD = '{0}'", tEnvPPayforItem.STANDARD.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPPayforItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPPayforItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPPayforItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPPayforItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPPayforItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPPayforItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPPayforItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPPayforItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPPayforItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPPayforItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
