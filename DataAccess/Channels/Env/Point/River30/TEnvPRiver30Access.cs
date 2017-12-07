﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.River30;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.River30
{
    /// <summary>
    /// 功能：双三十废水
    /// 创建日期：2013-06-17
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiver30Access : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiver30">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiver30Vo tEnvPRiver30)
        {
            string strSQL = "select Count(*) from T_ENV_P_RIVER30 " + this.BuildWhereStatement(tEnvPRiver30);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiver30Vo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER30  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPRiver30Vo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiver30">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiver30Vo Details(TEnvPRiver30Vo tEnvPRiver30)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER30 " + this.BuildWhereStatement(tEnvPRiver30));
            return SqlHelper.ExecuteObject(new TEnvPRiver30Vo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiver30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiver30Vo> SelectByObject(TEnvPRiver30Vo tEnvPRiver30, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_RIVER30 " + this.BuildWhereStatement(tEnvPRiver30));
            return SqlHelper.ExecuteObjectList(tEnvPRiver30, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiver30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiver30Vo tEnvPRiver30, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_RIVER30 {0} ";
            if (!string.IsNullOrEmpty(tEnvPRiver30.SORT_FIELD))
            {
                strSQL += " order by " + tEnvPRiver30.SORT_FIELD;
            }
            if (!string.IsNullOrEmpty(tEnvPRiver30.SORT_TYPE))
            {
                strSQL += " " + tEnvPRiver30.SORT_TYPE;
            }
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPRiver30));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiver30"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiver30Vo tEnvPRiver30)
        {
            string strSQL = "select * from T_ENV_P_RIVER30 " + this.BuildWhereStatement(tEnvPRiver30);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiver30">对象</param>
        /// <returns></returns>
        public TEnvPRiver30Vo SelectByObject(TEnvPRiver30Vo tEnvPRiver30)
        {
            string strSQL = "select * from T_ENV_P_RIVER30 " + this.BuildWhereStatement(tEnvPRiver30);
            return SqlHelper.ExecuteObject(new TEnvPRiver30Vo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPRiver30">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiver30Vo tEnvPRiver30)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPRiver30, TEnvPRiver30Vo.T_ENV_P_RIVER30_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiver30">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiver30Vo tEnvPRiver30)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiver30, TEnvPRiver30Vo.T_ENV_P_RIVER30_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPRiver30.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiver30_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiver30_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiver30Vo tEnvPRiver30_UpdateSet, TEnvPRiver30Vo tEnvPRiver30_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiver30_UpdateSet, TEnvPRiver30Vo.T_ENV_P_RIVER30_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPRiver30_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_RIVER30 where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPRiver30Vo tEnvPRiver30)
        {
            string strSQL = "delete from T_ENV_P_RIVER30 ";
            strSQL += this.BuildWhereStatement(tEnvPRiver30);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPRiver30"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPRiver30Vo tEnvPRiver30)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPRiver30)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPRiver30.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPRiver30.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPRiver30.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPRiver30.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPRiver30.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPRiver30.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPRiver30.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPRiver30.SATAIONS_ID.ToString()));
                }
                //断面代码
                if (!String.IsNullOrEmpty(tEnvPRiver30.SECTION_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_CODE = '{0}'", tEnvPRiver30.SECTION_CODE.ToString()));
                }
                //断面名称
                if (!String.IsNullOrEmpty(tEnvPRiver30.SECTION_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_NAME = '{0}'", tEnvPRiver30.SECTION_NAME.ToString()));
                }
                //所在地区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPRiver30.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPRiver30.AREA_ID.ToString()));
                }
                //所属省份ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPRiver30.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPRiver30.PROVINCE_ID.ToString()));
                }
                //控制级别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPRiver30.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPRiver30.CONTRAL_LEVEL.ToString()));
                }
                //河流ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPRiver30.RIVER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RIVER_ID = '{0}'", tEnvPRiver30.RIVER_ID.ToString()));
                }
                //流域ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPRiver30.VALLEY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VALLEY_ID = '{0}'", tEnvPRiver30.VALLEY_ID.ToString()));
                }
                //水质目标ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPRiver30.WATER_QUALITY_GOALS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_QUALITY_GOALS_ID = '{0}'", tEnvPRiver30.WATER_QUALITY_GOALS_ID.ToString()));
                }
                //每月监测、单月监测
                if (!String.IsNullOrEmpty(tEnvPRiver30.MONITOR_TIMES.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_TIMES = '{0}'", tEnvPRiver30.MONITOR_TIMES.ToString()));
                }
                //类别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPRiver30.CATEGORY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CATEGORY_ID = '{0}'", tEnvPRiver30.CATEGORY_ID.ToString()));
                }
                //是否交接（0-否，1-是）
                if (!String.IsNullOrEmpty(tEnvPRiver30.IS_HANDOVER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_HANDOVER = '{0}'", tEnvPRiver30.IS_HANDOVER.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPRiver30.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPRiver30.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPRiver30.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPRiver30.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPRiver30.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPRiver30.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPRiver30.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPRiver30.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPRiver30.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPRiver30.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPRiver30.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPRiver30.LATITUDE_SECOND.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPRiver30.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPRiver30.CONDITION_ID.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPRiver30.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPRiver30.IS_DEL.ToString()));
                }
                //断面性质
                if (!String.IsNullOrEmpty(tEnvPRiver30.SECTION_PORPERTIES_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_PORPERTIES_ID = '{0}'", tEnvPRiver30.SECTION_PORPERTIES_ID.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPRiver30.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPRiver30.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPRiver30.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPRiver30.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPRiver30.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPRiver30.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPRiver30.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPRiver30.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPRiver30.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPRiver30.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPRiver30.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPRiver30.REMARK5.ToString()));
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
        public bool Create(TEnvPRiver30Vo TEnvPRiver30, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPRiver30.SelectMonths.Split(';').ToList();
            TEnvPRiver30.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                TEnvPRiver30.ID = GetSerialNumber(strSerial);
                TEnvPRiver30.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPRiver30, TEnvPRiver30Vo.T_ENV_P_RIVER30_TABLE);
                list.Add(strSQL);
            }


            return SqlHelper.ExecuteSQLByTransaction(list);
        }


        /// <summary>
        /// 河流垂线监测项目的复制逻辑
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

            sql = "select * from " + TEnvPRiver30VItemVo.T_ENV_P_RIVER30_V_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPRiver30VItemVo.T_ENV_P_RIVER30_V_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPRiver30VItemVo.T_ENV_P_RIVER30_V_ITEM_TABLE, row, "", "", strTID, strID);
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
    }

}
