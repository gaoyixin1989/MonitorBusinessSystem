using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.NoiseArea;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.NoiseArea
{
    /// <summary>
    /// 功能：区域环境噪声
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPNoiseAreaItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_NOISE_AREA_ITEM " + this.BuildWhereStatement(tEnvPNoiseAreaItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPNoiseAreaItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_NOISE_AREA_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPNoiseAreaItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPNoiseAreaItemVo Details(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_NOISE_AREA_ITEM " + this.BuildWhereStatement(tEnvPNoiseAreaItem));
            return SqlHelper.ExecuteObject(new TEnvPNoiseAreaItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPNoiseAreaItemVo> SelectByObject(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_NOISE_AREA_ITEM " + this.BuildWhereStatement(tEnvPNoiseAreaItem));
            return SqlHelper.ExecuteObjectList(tEnvPNoiseAreaItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_NOISE_AREA_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPNoiseAreaItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            string strSQL = "select * from T_ENV_P_NOISE_AREA_ITEM " + this.BuildWhereStatement(tEnvPNoiseAreaItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">对象</param>
        /// <returns></returns>
        public TEnvPNoiseAreaItemVo SelectByObject(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            string strSQL = "select * from T_ENV_P_NOISE_AREA_ITEM " + this.BuildWhereStatement(tEnvPNoiseAreaItem);
            return SqlHelper.ExecuteObject(new TEnvPNoiseAreaItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPNoiseAreaItem, TEnvPNoiseAreaItemVo.T_ENV_P_NOISE_AREA_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPNoiseAreaItem, TEnvPNoiseAreaItemVo.T_ENV_P_NOISE_AREA_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPNoiseAreaItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPNoiseAreaItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem_UpdateSet, TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPNoiseAreaItem_UpdateSet, TEnvPNoiseAreaItemVo.T_ENV_P_NOISE_AREA_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPNoiseAreaItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_NOISE_AREA_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            string strSQL = "delete from T_ENV_P_NOISE_AREA_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPNoiseAreaItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPNoiseAreaItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPNoiseAreaItemVo tEnvPNoiseAreaItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPNoiseAreaItem)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPNoiseAreaItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPNoiseAreaItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPNoiseAreaItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPNoiseAreaItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPNoiseAreaItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPNoiseAreaItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPNoiseAreaItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPNoiseAreaItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPNoiseAreaItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPNoiseAreaItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPNoiseAreaItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPNoiseAreaItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPNoiseAreaItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPNoiseAreaItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPNoiseAreaItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPNoiseAreaItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPNoiseAreaItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPNoiseAreaItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
