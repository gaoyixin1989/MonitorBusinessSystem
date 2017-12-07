using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;
using System.Linq;
using i3.ValueObject.Channels.Env.Point.Rain;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Point.Rain
{
    /// <summary>
    /// 功能：降水监测点信息表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPointRainAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointRain">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointRainVo tEnvPointRain)
        {
            string strSQL = "select Count(*) from T_ENV_P_RAIN " + this.BuildWhereStatement(tEnvPointRain);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointRainVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RAIN  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPointRainVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointRain">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointRainVo Details(TEnvPointRainVo tEnvPointRain)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RAIN " + this.BuildWhereStatement(tEnvPointRain));
            return SqlHelper.ExecuteObject(new TEnvPointRainVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointRain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointRainVo> SelectByObject(TEnvPointRainVo tEnvPointRain, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_RAIN " + this.BuildWhereStatement(tEnvPointRain));
            return SqlHelper.ExecuteObjectList(tEnvPointRain, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointRain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointRainVo tEnvPointRain, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_RAIN {0} order by YEAR desc,len(MONTH) desc,MONTH desc ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPointRain));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointRain"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointRainVo tEnvPointRain)
        {
            string strSQL = "select * from T_ENV_P_RAIN " + this.BuildWhereStatement(tEnvPointRain);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointRain">对象</param>
        /// <returns></returns>
        public TEnvPointRainVo SelectByObject(TEnvPointRainVo tEnvPointRain)
        {
            string strSQL = "select * from T_ENV_P_RAIN " + this.BuildWhereStatement(tEnvPointRain);
            return SqlHelper.ExecuteObject(new TEnvPointRainVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPointRain">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointRainVo tEnvPointRain)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPointRain, TEnvPointRainVo.T_ENV_POINT_RAIN_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象添加(ljn.2013/6/14)
        /// </summary>
        /// <param name="tEnvPAir">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointRainVo tEnvPointRain, string Number)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;
            List<string> values = tEnvPointRain.SelectMonths.Split(';').ToList();
            tEnvPointRain.SelectMonths = string.Empty;
            foreach (string valueTemp in values)
            {
                tEnvPointRain.ID = GetSerialNumber(Number);
                tEnvPointRain.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(tEnvPointRain, TEnvPointRainVo.T_ENV_POINT_RAIN_TABLE);
                list.Add(strSQL);
            }

            return SqlHelper.ExecuteSQLByTransaction(list);
            //string strSQL = SqlHelper.BuildInsertExpress(tEnvPAir, TEnvPAirVo.T_ENV_P_AIR_TABLE);
            //return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointRain">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointRainVo tEnvPointRain)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointRain, TEnvPointRainVo.T_ENV_POINT_RAIN_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPointRain.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointRain_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointRain_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointRainVo tEnvPointRain_UpdateSet, TEnvPointRainVo tEnvPointRain_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointRain_UpdateSet, TEnvPointRainVo.T_ENV_POINT_RAIN_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPointRain_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_RAIN where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPointRainVo tEnvPointRain)
        {
            string strSQL = "delete from T_ENV_P_RAIN ";
            strSQL += this.BuildWhereStatement(tEnvPointRain);

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

            sql = "select * from " + TEnvPointRainItemVo.T_ENV_POINT_RAIN_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPointRainItemVo.T_ENV_POINT_RAIN_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial); 
                    sql = com.getCopySql(TEnvPointRainItemVo.T_ENV_POINT_RAIN_ITEM_TABLE, row, "", "", strTID, strID);
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
        /// <param name="tEnvPointRain"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPointRainVo tEnvPointRain)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointRain)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPointRain.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointRain.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPointRain.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPointRain.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvPointRain.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPointRain.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPointRain.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPointRain.SATAIONS_ID.ToString()));
                }
                //测点编号
                if (!String.IsNullOrEmpty(tEnvPointRain.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPointRain.POINT_CODE.ToString()));
                }
                //测点名称
                if (!String.IsNullOrEmpty(tEnvPointRain.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPointRain.POINT_NAME.ToString()));
                }
                //行政区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPointRain.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPointRain.AREA_ID.ToString()));
                }
                //控制级别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPointRain.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPointRain.CONTRAL_LEVEL.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPointRain.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPointRain.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPointRain.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPointRain.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPointRain.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPointRain.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPointRain.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPointRain.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPointRain.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPointRain.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPointRain.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPointRain.LATITUDE_SECOND.ToString()));
                }
                //具体位置
                if (!String.IsNullOrEmpty(tEnvPointRain.LOCATION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCATION = '{0}'", tEnvPointRain.LOCATION.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPointRain.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPointRain.CONDITION_ID.ToString()));
                }
                //使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tEnvPointRain.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPointRain.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPointRain.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPointRain.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPointRain.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointRain.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPointRain.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointRain.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPointRain.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointRain.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPointRain.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointRain.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPointRain.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointRain.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
