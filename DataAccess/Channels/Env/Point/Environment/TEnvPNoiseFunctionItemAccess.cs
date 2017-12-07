using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Environment;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.Environment
{
    /// <summary>
    /// 功能：
    /// 创建日期：2013-06-13
    /// 创建人：
    /// </summary>
    public class TEnvPNoiseFunctionItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_AIR_ITEM " + this.BuildWhereStatement(tEnvPNoiseFunctionItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 批量保存监测项目数据[用于无垂线监测点](ljn, 2013/6/15)
        /// </summary>
        /// <param name="strTableName">数据表名</param>
        /// <param name="strColumnName">数据表列名</param>
        /// <param name="strSerialId">序列号</param>
        /// <param name="strPointId">监测点ID</param>
        /// <param name="strValue">监测项目值</param>
        /// <returns></returns>
        public bool SaveItemByTransaction(string strTableName, string strColumnName, string strSerialId, string strPointId, string strValue)
        {
            ArrayList arrVo = new ArrayList();
            string strsql = "delete from {0} where {1}='{2}'";
            strsql = string.Format(strsql, strTableName, strColumnName, strPointId);
            arrVo.Add(strsql);

            List<string> values = strValue.Split(',').ToList();
            foreach (string valueTemp in values)
            {
                string strAnalysisId = "";
                i3.ValueObject.Channels.Base.Item.TBaseItemAnalysisVo TBaseItemAnalysisVo = new ValueObject.Channels.Base.Item.TBaseItemAnalysisVo();
                TBaseItemAnalysisVo.ITEM_ID = valueTemp;
                TBaseItemAnalysisVo.IS_DEL = "0";
                DataTable dt = new i3.DataAccess.Channels.Base.Item.TBaseItemAnalysisAccess().SelectByTable_ByJoin(TBaseItemAnalysisVo);
                if (dt.Rows.Count == 1)
                {
                    strAnalysisId = dt.Rows[0]["ANALYSIS_METHOD_ID"].ToString();
                }
                if (dt.Rows.Count >= 1)
                {
                    bool hasDefault = false;
                    foreach (DataRow row in dt.Rows)
                    {
                        if (row["IS_DEFAULT"].ToString() == "是")
                        {
                            strAnalysisId = row["ANALYSIS_METHOD_ID"].ToString();
                            hasDefault = true; break;
                        }
                    }
                    if (hasDefault == false)
                        strAnalysisId = dt.Rows[0]["ANALYSIS_METHOD_ID"].ToString();
                }
                if (valueTemp != "")
                {
                    strsql = "insert into {0}(ID,{1},ITEM_ID,ANALYSIS_ID) values('{2}','{3}','{4}','{5}')";
                    strsql = string.Format(strsql, strTableName, strColumnName, GetSerialNumber(strSerialId), strPointId, valueTemp, strAnalysisId);
                    arrVo.Add(strsql);
                }
            }
            return ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPNoiseFunctionItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_AIR_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPNoiseFunctionItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPNoiseFunctionItemVo Details(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_AIR_ITEM " + this.BuildWhereStatement(tEnvPNoiseFunctionItem));
            return SqlHelper.ExecuteObject(new TEnvPNoiseFunctionItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPNoiseFunctionItemVo> SelectByObject(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_AIR_ITEM " + this.BuildWhereStatement(tEnvPNoiseFunctionItem));
            return SqlHelper.ExecuteObjectList(tEnvPNoiseFunctionItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_AIR_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPNoiseFunctionItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            string strSQL = "select * from T_ENV_P_AIR_ITEM " + this.BuildWhereStatement(tEnvPNoiseFunctionItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">对象</param>
        /// <returns></returns>
        public TEnvPNoiseFunctionItemVo SelectByObject(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            string strSQL = "select * from T_ENV_P_AIR_ITEM " + this.BuildWhereStatement(tEnvPNoiseFunctionItem);
            return SqlHelper.ExecuteObject(new TEnvPNoiseFunctionItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPNoiseFunctionItem, TEnvPNoiseFunctionItemVo.T_ENV_P_NOISE_FUNCTION_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPNoiseFunctionItem, TEnvPNoiseFunctionItemVo.T_ENV_P_NOISE_FUNCTION_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPNoiseFunctionItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPNoiseFunctionItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem_UpdateSet, TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPNoiseFunctionItem_UpdateSet, TEnvPNoiseFunctionItemVo.T_ENV_P_NOISE_FUNCTION_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPNoiseFunctionItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_AIR_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            string strSQL = "delete from T_ENV_P_AIR_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPNoiseFunctionItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPNoiseFunctionItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPNoiseFunctionItemVo tEnvPNoiseFunctionItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPNoiseFunctionItem)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPNoiseFunctionItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPNoiseFunctionItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPNoiseFunctionItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPNoiseFunctionItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPNoiseFunctionItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPNoiseFunctionItem.ITEM_ID.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tEnvPNoiseFunctionItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPNoiseFunctionItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPNoiseFunctionItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPNoiseFunctionItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPNoiseFunctionItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPNoiseFunctionItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPNoiseFunctionItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPNoiseFunctionItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPNoiseFunctionItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPNoiseFunctionItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPNoiseFunctionItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPNoiseFunctionItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
