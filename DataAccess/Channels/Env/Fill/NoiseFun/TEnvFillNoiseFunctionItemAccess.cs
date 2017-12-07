using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseFun;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.NoiseFun
{
    /// <summary>
    /// 功能：功能区噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseFunctionItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_NOISE_FUNCTION_ITEM " + this.BuildWhereStatement(tEnvFillNoiseFunctionItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_FUNCTION_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillNoiseFunctionItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseFunctionItemVo Details(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_FUNCTION_ITEM " + this.BuildWhereStatement(tEnvFillNoiseFunctionItem));
            return SqlHelper.ExecuteObject(new TEnvFillNoiseFunctionItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseFunctionItemVo> SelectByObject(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_FUNCTION_ITEM " + this.BuildWhereStatement(tEnvFillNoiseFunctionItem));
            return SqlHelper.ExecuteObjectList(tEnvFillNoiseFunctionItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_NOISE_FUNCTION_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillNoiseFunctionItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_FUNCTION_ITEM " + this.BuildWhereStatement(tEnvFillNoiseFunctionItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseFunctionItemVo SelectByObject(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_FUNCTION_ITEM " + this.BuildWhereStatement(tEnvFillNoiseFunctionItem);
            return SqlHelper.ExecuteObject(new TEnvFillNoiseFunctionItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillNoiseFunctionItem, TEnvFillNoiseFunctionItemVo.T_ENV_FILL_NOISE_FUNCTION_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseFunctionItem, TEnvFillNoiseFunctionItemVo.T_ENV_FILL_NOISE_FUNCTION_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillNoiseFunctionItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseFunctionItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem_UpdateSet, TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseFunctionItem_UpdateSet, TEnvFillNoiseFunctionItemVo.T_ENV_FILL_NOISE_FUNCTION_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillNoiseFunctionItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_NOISE_FUNCTION_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            string strSQL = "delete from T_ENV_FILL_NOISE_FUNCTION_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillNoiseFunctionItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillNoiseFunctionItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillNoiseFunctionItemVo tEnvFillNoiseFunctionItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillNoiseFunctionItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillNoiseFunctionItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillNoiseFunctionItem.FILL_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillNoiseFunctionItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionItem.ANALYSIS_METHOD_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_METHOD_ID = '{0}'", tEnvFillNoiseFunctionItem.ANALYSIS_METHOD_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillNoiseFunctionItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillNoiseFunctionItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillNoiseFunctionItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillNoiseFunctionItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillNoiseFunctionItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillNoiseFunctionItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillNoiseFunctionItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillNoiseFunctionItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
