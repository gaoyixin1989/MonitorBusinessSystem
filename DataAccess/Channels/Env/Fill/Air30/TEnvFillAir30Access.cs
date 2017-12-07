using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.AIR30;
using i3.ValueObject;
using i3.DataAccess.Sys.Resource;

namespace i3.DataAccess.Channels.Env.Fill.AIR30
{
    /// <summary>
    /// 功能：双三十废气填报表
    /// 创建日期：2013-05-08
    /// 创建人：潘德军
    /// modify : 刘静楠
    /// time:2013-6-25
    /// </summary>
    public class TEnvFillAir30Access : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAir30">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAir30Vo tEnvFillAir30)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_AIR30 " + this.BuildWhereStatement(tEnvFillAir30);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAir30Vo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIR30  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillAir30Vo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAir30">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAir30Vo Details(TEnvFillAir30Vo tEnvFillAir30)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIR30 " + this.BuildWhereStatement(tEnvFillAir30));
            return SqlHelper.ExecuteObject(new TEnvFillAir30Vo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAir30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAir30Vo> SelectByObject(TEnvFillAir30Vo tEnvFillAir30, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_AIR30 " + this.BuildWhereStatement(tEnvFillAir30));
            return SqlHelper.ExecuteObjectList(tEnvFillAir30, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAir30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAir30Vo tEnvFillAir30, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_AIR30 {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAir30));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAir30"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAir30Vo tEnvFillAir30)
        {
            string strSQL = "select * from T_ENV_FILL_AIR30 " + this.BuildWhereStatement(tEnvFillAir30);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAir30">对象</param>
        /// <returns></returns>
        public TEnvFillAir30Vo SelectByObject(TEnvFillAir30Vo tEnvFillAir30)
        {
            string strSQL = "select * from T_ENV_FILL_AIR30 " + this.BuildWhereStatement(tEnvFillAir30);
            return SqlHelper.ExecuteObject(new TEnvFillAir30Vo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAir30">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAir30Vo tEnvFillAir30)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAir30, TEnvFillAir30Vo.T_ENV_FILL_AIR30_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAir30">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAir30Vo tEnvFillAir30)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAir30, TEnvFillAir30Vo.T_ENV_FILL_AIR30_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAir30.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAir30_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAir30_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAir30Vo tEnvFillAir30_UpdateSet, TEnvFillAir30Vo tEnvFillAir30_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAir30_UpdateSet, TEnvFillAir30Vo.T_ENV_FILL_AIR30_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillAir30_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_AIR30 where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAir30Vo tEnvFillAir30)
        {
            string strSQL = "delete from T_ENV_FILL_AIR30 ";
            strSQL += this.BuildWhereStatement(tEnvFillAir30);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region// 构造填报表需要显示的信息
        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable  CreateShowDT()
        {
            DataTable dt = new DataTable();
            DataRow dr;
            dt.Columns.Add("code", typeof(String));
            dt.Columns.Add("name", typeof(String));

            dr = dt.NewRow();
            dr["code"] = "YEAR";
            dr["name"] = "年份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "POINT_ID";
            dr["name"] = "监测点名称";
            dt.Rows.Add(dr);
            
            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "WEEK";
            dr["name"] = "周";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "DAY";
            dr["name"] = "日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "TEMPERATRUE";
            dr["name"] = "气温";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "PRESSURE";
            dr["name"] = "气压";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "WIND_SPEED";
            dr["name"] = "风速";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "WIND_DIRECTION";
            dr["name"] = "风向";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "API_CODE";
            dr["name"] = "API指数";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "AQI_CODE";
            dr["name"] = "空气质量指数";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "AIR_LEVEL";
            dr["name"] = "空气质量级别";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "AIR_STATE";
            dr["name"] = "空气质量状况";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MAIN_AIR";
            dr["name"] = "主要污染物";
            dt.Rows.Add(dr);
            return dt;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAir30"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAir30Vo tEnvFillAir30)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAir30)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillAir30.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAir30.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillAir30.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillAir30.POINT_ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillAir30.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillAir30.YEAR.ToString()));
                }
                //月
                if (!String.IsNullOrEmpty(tEnvFillAir30.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillAir30.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillAir30.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillAir30.DAY.ToString()));
                }
                //周
                if (!String.IsNullOrEmpty(tEnvFillAir30.WEEK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WEEK = '{0}'", tEnvFillAir30.WEEK.ToString()));
                }
                //测点号
                if (!String.IsNullOrEmpty(tEnvFillAir30.POINT_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NUM = '{0}'", tEnvFillAir30.POINT_NUM.ToString()));
                }
                //测点名称
                if (!String.IsNullOrEmpty(tEnvFillAir30.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvFillAir30.POINT_NAME.ToString()));
                }
                //气温
                if (!String.IsNullOrEmpty(tEnvFillAir30.TEMPERATRUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEMPERATRUE = '{0}'", tEnvFillAir30.TEMPERATRUE.ToString()));
                }
                //气压
                if (!String.IsNullOrEmpty(tEnvFillAir30.PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PRESSURE = '{0}'", tEnvFillAir30.PRESSURE.ToString()));
                }
                //风速
                if (!String.IsNullOrEmpty(tEnvFillAir30.WIND_SPEED.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WIND_SPEED = '{0}'", tEnvFillAir30.WIND_SPEED.ToString()));
                }
                //风向
                if (!String.IsNullOrEmpty(tEnvFillAir30.WIND_DIRECTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WIND_DIRECTION = '{0}'", tEnvFillAir30.WIND_DIRECTION.ToString()));
                }
                //API指数
                if (!String.IsNullOrEmpty(tEnvFillAir30.API_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND API_CODE = '{0}'", tEnvFillAir30.API_CODE.ToString()));
                }
                //空气质量指数
                if (!String.IsNullOrEmpty(tEnvFillAir30.AQI_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AQI_CODE = '{0}'", tEnvFillAir30.AQI_CODE.ToString()));
                }
                //空气级别
                if (!String.IsNullOrEmpty(tEnvFillAir30.AIR_LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AIR_LEVEL = '{0}'", tEnvFillAir30.AIR_LEVEL.ToString()));
                }
                //空气质量状况
                if (!String.IsNullOrEmpty(tEnvFillAir30.AIR_STATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND AIR_STATE = '{0}'", tEnvFillAir30.AIR_STATE.ToString()));
                }
                //主要污染物
                if (!String.IsNullOrEmpty(tEnvFillAir30.MAIN_AIR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MAIN_AIR = '{0}'", tEnvFillAir30.MAIN_AIR.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillAir30.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAir30.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillAir30.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAir30.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillAir30.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAir30.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillAir30.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAir30.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillAir30.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAir30.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        #region//暂不用代码
        /// <summary>
        /// 获取填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public DataTable GetAir30FillData(string year, string month, string areacode)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select a.*,d.DICT_TEXT as areaName from T_ENV_FILL_AIR_30 a join T_SYS_DICT d on d.DICT_TYPE='dict_30Area' and d.DICT_CODE=a.AREA_CODE  where 1=1 ");
            if (!string.IsNullOrEmpty(year))
                sqlStr.AppendFormat("and a.[year]='{0}' ", year);
            if (!string.IsNullOrEmpty(month))
                sqlStr.AppendFormat("and a.[month]='{0}'", month);
            if (!string.IsNullOrEmpty(areacode))
                sqlStr.AppendFormat("and a.AREA_CODE='{0}'", areacode);

            DataTable dt = ExecuteDataTable(sqlStr.ToString());
            if (dt.Rows.Count > 0)
            {
                return dt;
            }
            else
            {
                int intdays = DateTime.DaysInMonth(int.Parse(year), int.Parse(month));

                StringBuilder strSql = new StringBuilder();
                strSql.Append("begin Transaction T\n");
                strSql.Append("declare @errorCount as int\n");
                strSql.Append("set @errorCount=0\n");

                for (int i = 1; i <= intdays; i++)
                {
                    string id = new TSysSerialAccess().GetSerialNumber("air_30_id");

                    strSql.AppendFormat("insert into T_ENV_FILL_AIR_30(id,year,month,DAY,AREA_CODE) values('{0}','{1}','{2}','{3}','{4}')", id, year, month, i.ToString(),areacode);
                    strSql.Append("set @errorCount=@errorCount+@@error\n");
                }

                strSql.Append("IF @errorCount <> 0\n");
                strSql.Append("begin\n");
                strSql.Append("select 'fail'\n");
                strSql.Append("RollBack Transaction T\n");
                strSql.Append("end\n");
                strSql.Append("else\n");
                strSql.Append("begin\n");
                strSql.Append("select 'success'\n");
                strSql.Append("COMMIT Transaction T\n");
                strSql.Append("end\n");

                string result = ExecuteDataTable(strSql.ToString()).Rows[0][0].ToString();

                return ExecuteDataTable(sqlStr.ToString());
            }
            
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="dtData">数据集</param>
        /// <returns></returns>
        public string SaveAir30FillData(DataTable dtData, string areaCode)
        {
            if (dtData.Rows.Count > 0)
            {
                string year = dtData.Rows[0]["year"].ToString();
                string month = dtData.Rows[0]["month"].ToString();

                StringBuilder strSql = new StringBuilder();
                strSql.Append("begin Transaction T\n");
                strSql.Append("declare @errorCount as int\n");
                strSql.Append("set @errorCount=0\n");

                foreach (DataRow drData in dtData.Rows)
                {
                    string day = drData["DAY"].ToString();
                    string pm10 = drData["PM10"].ToString();
                    string no2 = drData["NO2"].ToString();
                    string so2 = drData["SO2"].ToString();
                    string apiCode = drData["API_CODE"].ToString();
                    string airLevel = drData["AIR_LEVEL"].ToString();
                    string mainAir = drData["MAIN_AIR"].ToString();

                    strSql.AppendFormat(@"update T_ENV_FILL_AIR_30 set SO2='{4}',NO2='{5}',PM10='{6}',API_CODE='{7}',AIR_LEVEL='{8}',MAIN_AIR='{9}' where AREA_CODE='{0}' and year='{1}' and month='{2}' and day ='{3}'",  areaCode, year, month, day, so2, no2, pm10, apiCode, airLevel, mainAir);
                    strSql.Append("set @errorCount=@errorCount+@@error\n");
                }


                strSql.Append("IF @errorCount <> 0\n");
                strSql.Append("begin\n");
                strSql.Append("select 'fail'\n");
                strSql.Append("RollBack Transaction T\n");
                strSql.Append("end\n");
                strSql.Append("else\n");
                strSql.Append("begin\n");
                strSql.Append("select 'success'\n");
                strSql.Append("COMMIT Transaction T\n");
                strSql.Append("end\n");

                string result = ExecuteDataTable(strSql.ToString()).Rows[0][0].ToString();

                return result;
            }
            else
                return "";
        }
        #endregion
    }
}
