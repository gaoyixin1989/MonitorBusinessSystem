using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Sulfate;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.Sulfate
{
    /// <summary>
    /// 功能：硫酸盐化速率监测点监测项目表
    /// 创建日期：2013-06-15
    /// 创建人：ljn
    public class TEnvPAlkaliItemAccess : SqlHelper
    {
        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPAlkaliItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_ALKALI_ITEM " + this.BuildWhereStatement(tEnvPAlkaliItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPAlkaliItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_ALKALI_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPAlkaliItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPAlkaliItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPAlkaliItemVo Details(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_ALKALI_ITEM " + this.BuildWhereStatement(tEnvPAlkaliItem));
            return SqlHelper.ExecuteObject(new TEnvPAlkaliItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPAlkaliItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPAlkaliItemVo> SelectByObject(TEnvPAlkaliItemVo tEnvPAlkaliItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_ALKALI_ITEM " + this.BuildWhereStatement(tEnvPAlkaliItem));
            return SqlHelper.ExecuteObjectList(tEnvPAlkaliItem, BuildPagerExpress(strSQL, iIndex, iCount));

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
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPAlkaliItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPAlkaliItemVo tEnvPAlkaliItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_ALKALI_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPAlkaliItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPAlkaliItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            string strSQL = "select * from T_ENV_P_ALKALI_ITEM " + this.BuildWhereStatement(tEnvPAlkaliItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPAlkaliItem">对象</param>
        /// <returns></returns>
        public TEnvPAlkaliItemVo SelectByObject(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            string strSQL = "select * from T_ENV_P_ALKALI_ITEM " + this.BuildWhereStatement(tEnvPAlkaliItem);
            return SqlHelper.ExecuteObject(new TEnvPAlkaliItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPAlkaliItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPAlkaliItem, TEnvPAlkaliItemVo.T_ENV_P_ALKALI_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAlkaliItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPAlkaliItem, TEnvPAlkaliItemVo.T_ENV_P_ALKALI_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPAlkaliItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAlkaliItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPAlkaliItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAlkaliItemVo tEnvPAlkaliItem_UpdateSet, TEnvPAlkaliItemVo tEnvPAlkaliItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPAlkaliItem_UpdateSet, TEnvPAlkaliItemVo.T_ENV_P_ALKALI_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPAlkaliItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_ALKALI_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            string strSQL = "delete from T_ENV_P_ALKALI_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPAlkaliItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPAlkaliItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPAlkaliItemVo tEnvPAlkaliItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPAlkaliItem)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPAlkaliItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPAlkaliItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPAlkaliItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPAlkaliItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPAlkaliItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPAlkaliItem.ITEM_ID.ToString()));
                }
                //分析方法ID
                if (!String.IsNullOrEmpty(tEnvPAlkaliItem.ANALYSIS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSIS_ID = '{0}'", tEnvPAlkaliItem.ANALYSIS_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPAlkaliItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPAlkaliItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPAlkaliItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPAlkaliItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPAlkaliItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPAlkaliItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPAlkaliItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPAlkaliItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPAlkaliItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPAlkaliItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
