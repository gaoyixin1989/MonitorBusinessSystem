using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Lake;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Point.Lake
{
    /// <summary>
    /// 功能：湖库
    /// 创建日期：2013-06-13
    /// 创建人：魏林
    /// </summary>
    public class TEnvPLakeAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPLake">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPLakeVo tEnvPLake)
        {
            string strSQL = "select Count(*) from T_ENV_P_LAKE " + this.BuildWhereStatement(tEnvPLake);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPLakeVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_LAKE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPLakeVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPLake">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPLakeVo Details(TEnvPLakeVo tEnvPLake)
        {
            string strSQL = String.Format("select * from  T_ENV_P_LAKE " + this.BuildWhereStatement(tEnvPLake));
            return SqlHelper.ExecuteObject(new TEnvPLakeVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPLake">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPLakeVo> SelectByObject(TEnvPLakeVo tEnvPLake, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_LAKE " + this.BuildWhereStatement(tEnvPLake));
            return SqlHelper.ExecuteObjectList(tEnvPLake, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPLake">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPLakeVo tEnvPLake, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_LAKE {0}  order by YEAR desc,len(MONTH) desc,MONTH desc ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPLake));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPLake"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPLakeVo tEnvPLake)
        {
            string strSQL = "select * from T_ENV_P_LAKE " + this.BuildWhereStatement(tEnvPLake);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPLake">对象</param>
        /// <returns></returns>
        public TEnvPLakeVo SelectByObject(TEnvPLakeVo tEnvPLake)
        {
            string strSQL = "select * from T_ENV_P_LAKE " + this.BuildWhereStatement(tEnvPLake);
            return SqlHelper.ExecuteObject(new TEnvPLakeVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPLake">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPLakeVo tEnvPLake)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPLake, TEnvPLakeVo.T_ENV_P_LAKE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPLake">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPLakeVo tEnvPLake)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPLake, TEnvPLakeVo.T_ENV_P_LAKE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPLake.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPLake_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPLake_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPLakeVo tEnvPLake_UpdateSet, TEnvPLakeVo tEnvPLake_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPLake_UpdateSet, TEnvPLakeVo.T_ENV_P_LAKE_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPLake_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_LAKE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPLakeVo tEnvPLake)
        {
            string strSQL = "delete from T_ENV_P_LAKE ";
            strSQL += this.BuildWhereStatement(tEnvPLake);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPLake"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPLakeVo tEnvPLake)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPLake)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPLake.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPLake.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvPLake.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvPLake.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvPLake.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvPLake.MONTH.ToString()));
                }
                //测站ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPLake.SATAIONS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SATAIONS_ID = '{0}'", tEnvPLake.SATAIONS_ID.ToString()));
                }
                //断面代码
                if (!String.IsNullOrEmpty(tEnvPLake.SECTION_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_CODE = '{0}'", tEnvPLake.SECTION_CODE.ToString()));
                }
                //断面名称
                if (!String.IsNullOrEmpty(tEnvPLake.SECTION_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_NAME = '{0}'", tEnvPLake.SECTION_NAME.ToString()));
                }
                //所在地区ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPLake.AREA_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AREA_ID = '{0}'", tEnvPLake.AREA_ID.ToString()));
                }
                //所属省份ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPLake.PROVINCE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PROVINCE_ID = '{0}'", tEnvPLake.PROVINCE_ID.ToString()));
                }
                //控制级别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPLake.CONTRAL_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRAL_LEVEL = '{0}'", tEnvPLake.CONTRAL_LEVEL.ToString()));
                }
                //河流ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPLake.RIVER_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RIVER_ID = '{0}'", tEnvPLake.RIVER_ID.ToString()));
                }
                //流域ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPLake.VALLEY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VALLEY_ID = '{0}'", tEnvPLake.VALLEY_ID.ToString()));
                }
                //水库ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPLake.LAKE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LAKE_ID = '{0}'", tEnvPLake.LAKE_ID.ToString()));
                }
                //水质目标ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPLake.WATER_QUALITY_GOALS_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_QUALITY_GOALS_ID = '{0}'", tEnvPLake.WATER_QUALITY_GOALS_ID.ToString()));
                }
                //每月监测、单月监测
                if (!String.IsNullOrEmpty(tEnvPLake.MONITOR_TIMES.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_TIMES = '{0}'", tEnvPLake.MONITOR_TIMES.ToString()));
                }
                //类别ID（字典项）
                if (!String.IsNullOrEmpty(tEnvPLake.CATEGORY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CATEGORY_ID = '{0}'", tEnvPLake.CATEGORY_ID.ToString()));
                }
                //是否交接（0-否，1-是）
                if (!String.IsNullOrEmpty(tEnvPLake.IS_HANDOVER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_HANDOVER = '{0}'", tEnvPLake.IS_HANDOVER.ToString()));
                }
                //经度（度）
                if (!String.IsNullOrEmpty(tEnvPLake.LONGITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_DEGREE = '{0}'", tEnvPLake.LONGITUDE_DEGREE.ToString()));
                }
                //经度（分）
                if (!String.IsNullOrEmpty(tEnvPLake.LONGITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_MINUTE = '{0}'", tEnvPLake.LONGITUDE_MINUTE.ToString()));
                }
                //经度（秒）
                if (!String.IsNullOrEmpty(tEnvPLake.LONGITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE_SECOND = '{0}'", tEnvPLake.LONGITUDE_SECOND.ToString()));
                }
                //纬度（度）
                if (!String.IsNullOrEmpty(tEnvPLake.LATITUDE_DEGREE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_DEGREE = '{0}'", tEnvPLake.LATITUDE_DEGREE.ToString()));
                }
                //纬度（分）
                if (!String.IsNullOrEmpty(tEnvPLake.LATITUDE_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_MINUTE = '{0}'", tEnvPLake.LATITUDE_MINUTE.ToString()));
                }
                //纬度（秒）
                if (!String.IsNullOrEmpty(tEnvPLake.LATITUDE_SECOND.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE_SECOND = '{0}'", tEnvPLake.LATITUDE_SECOND.ToString()));
                }
                //条件项
                if (!String.IsNullOrEmpty(tEnvPLake.CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONDITION_ID = '{0}'", tEnvPLake.CONDITION_ID.ToString()));
                }
                //删除标记
                if (!String.IsNullOrEmpty(tEnvPLake.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tEnvPLake.IS_DEL.ToString()));
                }
                //断面性质
                if (!String.IsNullOrEmpty(tEnvPLake.SECTION_PORPERTIES_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_PORPERTIES_ID = '{0}'", tEnvPLake.SECTION_PORPERTIES_ID.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tEnvPLake.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tEnvPLake.NUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPLake.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPLake.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPLake.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPLake.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPLake.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPLake.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPLake.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPLake.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPLake.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPLake.REMARK5.ToString()));
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
        public bool Create(TEnvPLakeVo TEnvPLake, string strSerial)
        {
            ArrayList list = new ArrayList();
            string strSQL = string.Empty;

            List<string> values = TEnvPLake.SelectMonths.Split(';').ToList();
            TEnvPLake.SelectMonths = "";
            foreach (string valueTemp in values)
            {
                TEnvPLake.ID = GetSerialNumber(strSerial);
                TEnvPLake.MONTH = valueTemp;
                strSQL = SqlHelper.BuildInsertExpress(TEnvPLake, TEnvPLakeVo.T_ENV_P_LAKE_TABLE);
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

            sql = "select * from " + TEnvPLakeVItemVo.T_ENV_P_LAKE_V_ITEM_TABLE + " where POINT_ID='" + strFID + "'";
            dt = SqlHelper.ExecuteDataTable(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "delete from " + TEnvPLakeVItemVo.T_ENV_P_LAKE_V_ITEM_TABLE + " where POINT_ID='" + strTID + "'";
                list.Add(sql);

                foreach (DataRow row in dt.Rows)
                {
                    strID = GetSerialNumber(strSerial);
                    sql = com.getCopySql(TEnvPLakeVItemVo.T_ENV_P_LAKE_V_ITEM_TABLE, row, "", "", strTID, strID);
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
        /// 根据年份和月份获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth)
        {
            string strSQL = "select a.ID,SECTION_NAME,VERTICAL_NAME from " + TEnvPLakeVo.T_ENV_P_LAKE_TABLE + " a inner join " + TEnvPLakeVVo.T_ENV_P_LAKE_V_TABLE + " b on(a.ID=b.SECTION_ID) where YEAR='" + strYear + "' and MONTH='" + strMonth + "' and a.IS_DEL='0'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
    }

}
