using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Dust;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.Dust
{
    /// <summary>
    /// 功能：降尘监测点信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPDustItemAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPDustItem">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPDustItemVo tEnvPDustItem)
        {
            string strSQL = "select Count(*) from T_ENV_P_DUST_ITEM " + this.BuildWhereStatement(tEnvPDustItem);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPDustItemVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DUST_ITEM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPDustItemVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPDustItem">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPDustItemVo Details(TEnvPDustItemVo tEnvPDustItem)
        {
            string strSQL = String.Format("select * from  T_ENV_P_DUST_ITEM " + this.BuildWhereStatement(tEnvPDustItem));
            return SqlHelper.ExecuteObject(new TEnvPDustItemVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPDustItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPDustItemVo> SelectByObject(TEnvPDustItemVo tEnvPDustItem, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_DUST_ITEM " + this.BuildWhereStatement(tEnvPDustItem));
            return SqlHelper.ExecuteObjectList(tEnvPDustItem, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPDustItem">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPDustItemVo tEnvPDustItem, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_DUST_ITEM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPDustItem));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPDustItem"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPDustItemVo tEnvPDustItem)
        {
            string strSQL = "select * from T_ENV_P_DUST_ITEM " + this.BuildWhereStatement(tEnvPDustItem);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPDustItem">对象</param>
        /// <returns></returns>
        public TEnvPDustItemVo SelectByObject(TEnvPDustItemVo tEnvPDustItem)
        {
            string strSQL = "select * from T_ENV_P_DUST_ITEM " + this.BuildWhereStatement(tEnvPDustItem);
            return SqlHelper.ExecuteObject(new TEnvPDustItemVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPDustItem">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPDustItemVo tEnvPDustItem)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPDustItem, TEnvPDustItemVo.T_ENV_P_DUST_ITEM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDustItem">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDustItemVo tEnvPDustItem)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDustItem, TEnvPDustItemVo.T_ENV_P_DUST_ITEM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPDustItem.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPDustItem_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPDustItem_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPDustItemVo tEnvPDustItem_UpdateSet, TEnvPDustItemVo tEnvPDustItem_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPDustItem_UpdateSet, TEnvPDustItemVo.T_ENV_P_DUST_ITEM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPDustItem_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_DUST_ITEM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPDustItemVo tEnvPDustItem)
        {
            string strSQL = "delete from T_ENV_P_DUST_ITEM ";
            strSQL += this.BuildWhereStatement(tEnvPDustItem);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        #region// 批量保存监测项目数据[用于无垂线监测点](ljn, 2013/6/15)
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
                    strsql = "insert into {0}(ID,{1},ITEM_ID) values('{2}','{3}','{4}')";
                    strsql = string.Format(strsql, strTableName, strColumnName, GetSerialNumber(strSerialId), strPointId, valueTemp);
                    arrVo.Add(strsql);
                }
            }
            return ExecuteSQLByTransaction(arrVo);
        }
        #endregion



        #region//监测项目复制
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            i3.DataAccess.Channels.Env.Point.Common.CommonAccess com = new Common.CommonAccess();
            bool b = true;
            string Msg = string.Empty;
            DataTable dt = new DataTable();
            string sql = string.Empty;
            ArrayList list = new ArrayList();
            string strID = string.Empty;

            sql = "select * from " + TEnvPDustItemVo.T_ENV_P_DUST_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPDustItemVo.T_ENV_P_DUST_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPDustItemVo.T_ENV_P_DUST_ITEM_TABLE, row, "", "", strTID, strID);
                    list.Add(sql);

                }
                if (SqlHelper.ExecuteSQLByTransaction(list))
                {
                    b = true;
                }
                else
                {
                    b = false;
                    Msg = "数据库更新失败";
                }
            }

            if (b)
                return "({result:true,msg:''})";
            else
                return "({result:false,msg:'" + Msg + "'})";
        }
        #endregion

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPDustItem"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPDustItemVo tEnvPDustItem)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPDustItem)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPDustItem.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPDustItem.ID.ToString()));
                }
                //点位ID
                if (!String.IsNullOrEmpty(tEnvPDustItem.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvPDustItem.POINT_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tEnvPDustItem.ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_ID = '{0}'", tEnvPDustItem.ITEM_ID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPDustItem.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPDustItem.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPDustItem.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPDustItem.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPDustItem.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPDustItem.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPDustItem.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPDustItem.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPDustItem.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPDustItem.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
