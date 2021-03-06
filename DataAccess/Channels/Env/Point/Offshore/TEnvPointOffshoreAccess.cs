using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Linq;
using i3.ValueObject.Channels.Env.Point.Offshore;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Point.Offshore
{
    /// <summary>
    /// 功能：近岸直排监测点信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPointOffshoreAccess : SqlHelper
    {
        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointOffshore">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            string strSQL = "select Count(*) from T_ENV_P_OFFSHORE " + this.BuildWhereStatement(tEnvPointOffshore);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointOffshoreVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_OFFSHORE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPointOffshoreVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointOffshore">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointOffshoreVo Details(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            string strSQL = String.Format("select * from  T_ENV_P_OFFSHORE " + this.BuildWhereStatement(tEnvPointOffshore));
            return SqlHelper.ExecuteObject(new TEnvPointOffshoreVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointOffshore">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointOffshoreVo> SelectByObject(TEnvPointOffshoreVo tEnvPointOffshore, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_OFFSHORE " + this.BuildWhereStatement(tEnvPointOffshore));
            return SqlHelper.ExecuteObjectList(tEnvPointOffshore, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointOffshore">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointOffshoreVo tEnvPointOffshore, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_OFFSHORE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPointOffshore));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointOffshore"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            string strSQL = "select * from T_ENV_P_OFFSHORE " + this.BuildWhereStatement(tEnvPointOffshore);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointOffshore">对象</param>
        /// <returns></returns>
        public TEnvPointOffshoreVo SelectByObject(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            string strSQL = "select * from T_ENV_P_OFFSHORE " + this.BuildWhereStatement(tEnvPointOffshore);
            return SqlHelper.ExecuteObject(new TEnvPointOffshoreVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPointOffshore">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPointOffshore, TEnvPointOffshoreVo.T_ENV_POINT_OFFSHORE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象添加(ljn.2013/6/14)
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointOffshoreVo tEnvPointOffshore, string Number)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;
            List<string> values = tEnvPointOffshore.SelectMonths.Split(';').ToList();
            tEnvPointOffshore.SelectMonths = string.Empty;
            foreach (string valueTemp in values)
            {
                tEnvPointOffshore.ID = GetSerialNumber(Number);
                tEnvPointOffshore.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(tEnvPointOffshore, TEnvPointOffshoreVo.T_ENV_POINT_OFFSHORE_TABLE);
                list.Add(strSQL);
            }

            return SqlHelper.ExecuteSQLByTransaction(list);
            //string strSQL = SqlHelper.BuildInsertExpress(tEnvPAir, TEnvPAirVo.T_ENV_P_AIR_TABLE);
            //return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointOffshore">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointOffshore, TEnvPointOffshoreVo.T_ENV_POINT_OFFSHORE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPointOffshore.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointOffshore_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointOffshore_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointOffshoreVo tEnvPointOffshore_UpdateSet, TEnvPointOffshoreVo tEnvPointOffshore_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointOffshore_UpdateSet, TEnvPointOffshoreVo.T_ENV_POINT_OFFSHORE_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPointOffshore_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_OFFSHORE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            string strSQL = "delete from T_ENV_P_OFFSHORE ";
            strSQL += this.BuildWhereStatement(tEnvPointOffshore);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tEnvPointOffshore">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TEnvPointOffshoreVo tEnvPointOffshore, int iIndex, int iCount)
        {
            string strSQL = " select * from T_ENV_P_OFFSHORE {0} ";
            strSQL = String.Format(strSQL, BuildWhereLikeStatement(tEnvPointOffshore));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }
        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tEnvPointOffshore">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TEnvPointOffshoreVo tEnvPointOffshore)
        {

            string strSQL = " select * from T_ENV_P_OFFSHORE {0} ";
            strSQL = String.Format(strSQL, BuildWhereLikeStatement(tEnvPointOffshore));
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 批量保存监测项目数据[用于无垂线监测点]
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

            sql = "select * from " + TEnvPointOffshoreItemVo.T_ENV_POINT_OFFSHORE_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPointOffshoreItemVo.T_ENV_POINT_OFFSHORE_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPointOffshoreItemVo.T_ENV_POINT_OFFSHORE_ITEM_TABLE, row, "", "", strTID, strID);
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
        /// <param name="tEnvPointOffshore"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointOffshore)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPointOffshore.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointOffshore.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPointOffshore.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPointOffshore.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPointOffshore.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPointOffshore.MONTH.ToString()));
                }
                //排污口代码
                if (!string.IsNullOrEmpty(tEnvPointOffshore.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE='{0}'",tEnvPointOffshore.POINT_CODE.ToString()));
                }
                //排污口
                if (!string.IsNullOrEmpty(tEnvPointOffshore.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME='{0}'", tEnvPointOffshore.POINT_CODE.ToString()));
                }
                //企业名称
                if (!String.IsNullOrEmpty(tEnvPointOffshore.COMPANY_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMPANY_NAME = '{0}'", tEnvPointOffshore.COMPANY_NAME.ToString()));
                }
                //功能属性
                if (!String.IsNullOrEmpty(tEnvPointOffshore.FUNCTION_ATTRIBUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUNCTION_ATTRIBUTE = '{0}'", tEnvPointOffshore.FUNCTION_ATTRIBUTE.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPointOffshore.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPointOffshore.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPointOffshore.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPointOffshore.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPointOffshore.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPointOffshore.LATITUDE_SECOND.ToString()));
                }
                //具体位置
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCATION = '{0}'", tEnvPointOffshore.LOCATION.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPointOffshore.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPointOffshore.CONDITION_ID.ToString()));
                }
                //使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tEnvPointOffshore.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPointOffshore.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPointOffshore.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPointOffshore.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPointOffshore.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointOffshore.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPointOffshore.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointOffshore.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPointOffshore.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointOffshore.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPointOffshore.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointOffshore.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPointOffshore.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointOffshore.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }
        /// <summary>
        /// 根据对象构造条件语句 模糊查询  Create By Castle(胡方扬) 2012-11-22
        /// </summary>
        /// <param name="tEnvPointOffshore"></param>
        /// <returns></returns>
        public string BuildWhereLikeStatement(TEnvPointOffshoreVo tEnvPointOffshore)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointOffshore)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPointOffshore.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointOffshore.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPointOffshore.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPointOffshore.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPointOffshore.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPointOffshore.MONTH.ToString()));
                }
                //企业名称
                if (!String.IsNullOrEmpty(tEnvPointOffshore.COMPANY_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMPANY_NAME LIKE '%{0}%'", tEnvPointOffshore.COMPANY_NAME.ToString()));
                }
                //功能属性
                if (!String.IsNullOrEmpty(tEnvPointOffshore.FUNCTION_ATTRIBUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FUNCTION_ATTRIBUTE = '{0}'", tEnvPointOffshore.FUNCTION_ATTRIBUTE.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPointOffshore.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPointOffshore.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPointOffshore.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPointOffshore.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPointOffshore.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPointOffshore.LATITUDE_SECOND.ToString()));
                }
                //具体位置
                if (!String.IsNullOrEmpty(tEnvPointOffshore.LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCATION LIKE '%{0}%'", tEnvPointOffshore.LOCATION.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPointOffshore.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPointOffshore.CONDITION_ID.ToString()));
                }
                //使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tEnvPointOffshore.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPointOffshore.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPointOffshore.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPointOffshore.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPointOffshore.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointOffshore.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPointOffshore.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointOffshore.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPointOffshore.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointOffshore.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPointOffshore.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointOffshore.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPointOffshore.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointOffshore.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
