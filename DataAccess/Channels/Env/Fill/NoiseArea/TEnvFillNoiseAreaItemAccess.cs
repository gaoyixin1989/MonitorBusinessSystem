using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseArea;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.NoiseArea
{
    /// <summary>
    /// 功能：区域环境噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseAreaItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_NOISE_AREA_ITEM " + this.BuildWhereStatement(tEnvFillNoiseAreaItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_AREA_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillNoiseAreaItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseAreaItemVo Details(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_AREA_ITEM " + this.BuildWhereStatement(tEnvFillNoiseAreaItem));
            return SqlHelper.ExecuteObject(new TEnvFillNoiseAreaItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseAreaItemVo> SelectByObject(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_AREA_ITEM " + this.BuildWhereStatement(tEnvFillNoiseAreaItem));
            return SqlHelper.ExecuteObjectList(tEnvFillNoiseAreaItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_NOISE_AREA_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillNoiseAreaItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_AREA_ITEM " + this.BuildWhereStatement(tEnvFillNoiseAreaItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseAreaItemVo SelectByObject(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_AREA_ITEM " + this.BuildWhereStatement(tEnvFillNoiseAreaItem);
            return SqlHelper.ExecuteObject(new TEnvFillNoiseAreaItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillNoiseAreaItem, TEnvFillNoiseAreaItemVo.T_ENV_FILL_NOISE_AREA_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseAreaItem, TEnvFillNoiseAreaItemVo.T_ENV_FILL_NOISE_AREA_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillNoiseAreaItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseAreaItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem_UpdateSet, TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseAreaItem_UpdateSet, TEnvFillNoiseAreaItemVo.T_ENV_FILL_NOISE_AREA_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillNoiseAreaItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_NOISE_AREA_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            string strSQL = "delete from T_ENV_FILL_NOISE_AREA_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillNoiseAreaItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillNoiseAreaItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillNoiseAreaItemVo tEnvFillNoiseAreaItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillNoiseAreaItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillNoiseAreaItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillNoiseAreaItem.FILL_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillNoiseAreaItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaItem.ANALYSIS_METHOD_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_METHOD_ID = '{0}'", tEnvFillNoiseAreaItem.ANALYSIS_METHOD_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillNoiseAreaItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillNoiseAreaItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillNoiseAreaItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillNoiseAreaItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillNoiseAreaItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillNoiseAreaItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillNoiseAreaItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillNoiseAreaItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
