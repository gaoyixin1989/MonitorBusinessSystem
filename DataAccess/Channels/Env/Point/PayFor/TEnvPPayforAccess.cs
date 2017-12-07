using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.PayFor;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.PayFor
{
    /// <summary>
    /// 功能：生态补偿
    /// 创建日期：2013-06-14
    /// 创建人：魏林
    /// </summary>
    public class TEnvPPayforAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPPayfor">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPPayforVo tEnvPPayfor)
        {
            string strSQL = "select Count(*) from T_ENV_P_PAYFOR " + this.BuildWhereStatement(tEnvPPayfor);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPPayforVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_PAYFOR  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPPayforVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPPayfor">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPPayforVo Details(TEnvPPayforVo tEnvPPayfor)
        {
            string strSQL = String.Format("select * from  T_ENV_P_PAYFOR " + this.BuildWhereStatement(tEnvPPayfor));
            return SqlHelper.ExecuteObject(new TEnvPPayforVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPPayfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPPayforVo> SelectByObject(TEnvPPayforVo tEnvPPayfor, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_PAYFOR " + this.BuildWhereStatement(tEnvPPayfor));
            return SqlHelper.ExecuteObjectList(tEnvPPayfor, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPPayfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPPayforVo tEnvPPayfor, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_PAYFOR {0} ";
            if (!string.IsNullOrEmpty(tEnvPPayfor.SORT_FIELD))
            {
                strSQL += " order by " + tEnvPPayfor.SORT_FIELD;
            }
            if (!string.IsNullOrEmpty(tEnvPPayfor.SORT_TYPE))
            {
                strSQL += " " + tEnvPPayfor.SORT_TYPE;
            }
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPPayfor));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPPayfor"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPPayforVo tEnvPPayfor)
        {
            string strSQL = "select * from T_ENV_P_PAYFOR " + this.BuildWhereStatement(tEnvPPayfor);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPPayfor">对象</param>
        /// <returns></returns>
        public TEnvPPayforVo SelectByObject(TEnvPPayforVo tEnvPPayfor)
        {
            string strSQL = "select * from T_ENV_P_PAYFOR " + this.BuildWhereStatement(tEnvPPayfor);
            return SqlHelper.ExecuteObject(new TEnvPPayforVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPPayfor">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPPayforVo tEnvPPayfor)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPPayfor, TEnvPPayforVo.T_ENV_P_PAYFOR_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPayfor">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPayforVo tEnvPPayfor)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPPayfor, TEnvPPayforVo.T_ENV_P_PAYFOR_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPPayfor.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPPayfor_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPPayfor_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPPayforVo tEnvPPayfor_UpdateSet, TEnvPPayforVo tEnvPPayfor_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPPayfor_UpdateSet, TEnvPPayforVo.T_ENV_P_PAYFOR_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPPayfor_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_PAYFOR where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPPayforVo tEnvPPayfor)
        {
            string strSQL = "delete from T_ENV_P_PAYFOR ";
            strSQL += this.BuildWhereStatement(tEnvPPayfor);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPPayfor"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPPayforVo tEnvPPayfor)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPPayfor)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPPayfor.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPPayfor.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPPayfor.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPPayfor.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPPayfor.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPPayfor.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPPayfor.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPPayfor.SATAIONS_ID.ToString()));
                }
                //断面代码
                if (!String.IsNullOrEmpty(tEnvPPayfor.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvPPayfor.POINT_CODE.ToString()));
                }
                //断面名称
                if (!String.IsNullOrEmpty(tEnvPPayfor.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvPPayfor.POINT_NAME.ToString()));
                }
                //上游断面
                if (!String.IsNullOrEmpty(tEnvPPayfor.UP_POINT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UP_POINT = '{0}'", tEnvPPayfor.UP_POINT.ToString()));
                }
                //所在地区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPPayfor.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPPayfor.AREA_ID.ToString()));
                }
                //所属省份ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPPayfor.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPPayfor.PROVINCE_ID.ToString()));
                }
                //控制级别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPPayfor.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPPayfor.CONTRAL_LEVEL.ToString()));
                }
                //河流ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPPayfor.RIVER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RIVER_ID = '{0}'", tEnvPPayfor.RIVER_ID.ToString()));
                }
                //流域ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPPayfor.VALLEY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VALLEY_ID = '{0}'", tEnvPPayfor.VALLEY_ID.ToString()));
                }
                //水质目标ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPPayfor.WATER_QUALITY_GOALS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_QUALITY_GOALS_ID = '{0}'", tEnvPPayfor.WATER_QUALITY_GOALS_ID.ToString()));
                }
                //每月监测、单月监测
                if (!String.IsNullOrEmpty(tEnvPPayfor.MONITOR_TIMES.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_TIMES = '{0}'", tEnvPPayfor.MONITOR_TIMES.ToString()));
                }
                //类别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPPayfor.CATEGORY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CATEGORY_ID = '{0}'", tEnvPPayfor.CATEGORY_ID.ToString()));
                }
                //是否交接（0-否，1-是）
                if (!String.IsNullOrEmpty(tEnvPPayfor.IS_HANDOVER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_HANDOVER = '{0}'", tEnvPPayfor.IS_HANDOVER.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPPayfor.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPPayfor.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPPayfor.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPPayfor.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPPayfor.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPPayfor.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPPayfor.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPPayfor.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPPayfor.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPPayfor.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPPayfor.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPPayfor.LATITUDE_SECOND.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPPayfor.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPPayfor.CONDITION_ID.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPPayfor.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPPayfor.IS_DEL.ToString()));
                }
                //断面性质
                if (!String.IsNullOrEmpty(tEnvPPayfor.SECTION_PORPERTIES_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_PORPERTIES_ID = '{0}'", tEnvPPayfor.SECTION_PORPERTIES_ID.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPPayfor.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPPayfor.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPPayfor.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPPayfor.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPPayfor.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPPayfor.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPPayfor.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPPayfor.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPPayfor.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPPayfor.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPPayfor.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPPayfor.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPDrinkSrc">对象</param>
        /// <param name="strSerial">序列类型</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPPayforVo TEnvPPayfor, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPPayfor.SelectMonths.Split(';').ToList();
            TEnvPPayfor.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                TEnvPPayfor.ID = GetSerialNumber(strSerial);
                TEnvPPayfor.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPPayfor, TEnvPPayforVo.T_ENV_P_PAYFOR_TABLE);
                list.Add(strSQL);
            }


            return SqlHelper.ExecuteSQLByTransaction(list);
        }



        /// <summary>
        /// 生态补偿监测项目的复制逻辑
        /// </summary>
        /// <param name="strFID"></param>
        /// <param name="strTID"></param>
        /// <param name="strSerial"></param>
        /// <returns></returns>
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            i3.DataAccess.Channels.Env.Point.Common.CommonAccess com = new Common.CommonAccess();
            bool b = true;
            string Msg = string.Empty;
            DataTable dt = new DataTable();
            string sql = string.Empty;
            ArrayList list = new ArrayList();
            string strID = string.Empty;

            sql = "select * from " + TEnvPPayforItemVo.T_ENV_P_PAYFOR_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPPayforItemVo.T_ENV_P_PAYFOR_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPPayforItemVo.T_ENV_P_PAYFOR_ITEM_TABLE, row, "", "", strTID, strID);
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

        /// <summary>
        /// 保存监测项目的考核标准值
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public bool SaveItemStData(DataTable dt)
        {
            ArrayList list = new ArrayList();
            string sql = string.Empty;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                sql = "update " + TEnvPPayforItemVo.T_ENV_P_PAYFOR_ITEM_TABLE + " set " + TEnvPPayforItemVo.STANDARD_FIELD + "='" + dt.Rows[i]["\"STANDARD\""].ToString() + "' where " + TEnvPPayforItemVo.ID_FIELD + "='" + dt.Rows[i]["\"ID\""].ToString() + "'";
                list.Add(sql);
            }
            return SqlHelper.ExecuteSQLByTransaction(list);
        }
        /// <summary>
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            string strSQL = "select ID,POINT_NAME from " + TEnvPPayforVo.T_ENV_P_PAYFOR_TABLE + " where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and IS_DEL='0'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }

}
