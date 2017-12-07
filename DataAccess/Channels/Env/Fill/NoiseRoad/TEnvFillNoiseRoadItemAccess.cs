using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.NoiseRoad;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.NoiseRoad
{
    /// <summary>
    /// 功能：道路交通噪声数据填报
    /// 创建日期：2013-06-26
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillNoiseRoadItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_NOISE_ROAD_ITEM " + this.BuildWhereStatement(tEnvFillNoiseRoadItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseRoadItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_ROAD_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillNoiseRoadItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillNoiseRoadItemVo Details(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_ROAD_ITEM " + this.BuildWhereStatement(tEnvFillNoiseRoadItem));
            return SqlHelper.ExecuteObject(new TEnvFillNoiseRoadItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillNoiseRoadItemVo> SelectByObject(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_NOISE_ROAD_ITEM " + this.BuildWhereStatement(tEnvFillNoiseRoadItem));
            return SqlHelper.ExecuteObjectList(tEnvFillNoiseRoadItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_NOISE_ROAD_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillNoiseRoadItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_ROAD_ITEM " + this.BuildWhereStatement(tEnvFillNoiseRoadItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">对象</param>
        /// <returns></returns>
        public TEnvFillNoiseRoadItemVo SelectByObject(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            string strSQL = "select * from T_ENV_FILL_NOISE_ROAD_ITEM " + this.BuildWhereStatement(tEnvFillNoiseRoadItem);
            return SqlHelper.ExecuteObject(new TEnvFillNoiseRoadItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillNoiseRoadItem, TEnvFillNoiseRoadItemVo.T_ENV_FILL_NOISE_ROAD_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseRoadItem, TEnvFillNoiseRoadItemVo.T_ENV_FILL_NOISE_ROAD_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillNoiseRoadItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillNoiseRoadItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem_UpdateSet, TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillNoiseRoadItem_UpdateSet, TEnvFillNoiseRoadItemVo.T_ENV_FILL_NOISE_ROAD_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillNoiseRoadItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_NOISE_ROAD_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            string strSQL = "delete from T_ENV_FILL_NOISE_ROAD_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvFillNoiseRoadItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillNoiseRoadItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillNoiseRoadItemVo tEnvFillNoiseRoadItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillNoiseRoadItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillNoiseRoadItem.ID.ToString()));
                }
                //数据填报ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadItem.FILL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FILL_ID = '{0}'", tEnvFillNoiseRoadItem.FILL_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvFillNoiseRoadItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadItem.ANALYSIS_METHOD_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_METHOD_ID = '{0}'", tEnvFillNoiseRoadItem.ANALYSIS_METHOD_ID.ToString()));
                }
                //监测值
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadItem.ITEM_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_VALUE = '{0}'", tEnvFillNoiseRoadItem.ITEM_VALUE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadItem.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillNoiseRoadItem.JUDGE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillNoiseRoadItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillNoiseRoadItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillNoiseRoadItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillNoiseRoadItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillNoiseRoadItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillNoiseRoadItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
