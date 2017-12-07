using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Alkali;
using i3.ValueObject;
using i3.DataAccess.Sys.Resource;

namespace i3.DataAccess.Channels.Env.Fill.Alkali
{
    /// <summary>
    /// 功能：碳酸盐化速率填报
    /// 创建日期：2013-05-08
    /// 创建人：潘德军
    /// modify by 刘静楠
    /// 修改时间：2013/6/21 
    /// </summary>
    public class TEnvFillAlkaliAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAlkali">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_ALKALI " + this.BuildWhereStatement(tEnvFillAlkali);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAlkaliVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ALKALI  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillAlkaliVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAlkali">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAlkaliVo Details(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ALKALI " + this.BuildWhereStatement(tEnvFillAlkali));
            return SqlHelper.ExecuteObject(new TEnvFillAlkaliVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAlkali">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAlkaliVo> SelectByObject(TEnvFillAlkaliVo tEnvFillAlkali, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_ALKALI " + this.BuildWhereStatement(tEnvFillAlkali));
            return SqlHelper.ExecuteObjectList(tEnvFillAlkali, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAlkali">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAlkaliVo tEnvFillAlkali, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_ALKALI {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAlkali));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAlkali"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            string strSQL = "select * from T_ENV_FILL_ALKALI " + this.BuildWhereStatement(tEnvFillAlkali);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAlkali">对象</param>
        /// <returns></returns>
        public TEnvFillAlkaliVo SelectByObject(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            string strSQL = "select * from T_ENV_FILL_ALKALI " + this.BuildWhereStatement(tEnvFillAlkali);
            return SqlHelper.ExecuteObject(new TEnvFillAlkaliVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAlkali">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAlkali, TEnvFillAlkaliVo.T_ENV_FILL_ALKALI_TABLE);
            tEnvFillAlkali.ID = new TSysSerialAccess().GetSerialNumber("alkali_fill_id");
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlkali">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAlkali, TEnvFillAlkaliVo.T_ENV_FILL_ALKALI_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAlkali.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlkali_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAlkali_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlkaliVo tEnvFillAlkali_UpdateSet, TEnvFillAlkaliVo tEnvFillAlkali_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAlkali_UpdateSet, TEnvFillAlkaliVo.T_ENV_FILL_ALKALI_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillAlkali_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_ALKALI where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            string strSQL = "delete from T_ENV_FILL_ALKALI ";
            strSQL += this.BuildWhereStatement(tEnvFillAlkali);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region// 构造填报表需要显示的信息
        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
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
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);


            dr = dt.NewRow();
            dr["code"] = "POINT_ID";
            dr["name"] = "监测点名称";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "BEGIN_MONTH";
            //dr["name"] = "监测起始月";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_DAY";
            dr["name"] = "监测起始日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_HOUR";
            dr["name"] = "监测起始时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "BEGIN_MINUTE";
            dr["name"] = "监测起始分";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "END_MONTH";
            //dr["name"] = "监测结束月";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_DAY";
            dr["name"] = "监测结束日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_HOUR";
            dr["name"] = "监测结束时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "END_MINUTE";
            dr["name"] = "监测结束分";
            dt.Rows.Add(dr);

            return dt;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAlkali"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAlkaliVo tEnvFillAlkali)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAlkali)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillAlkali.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAlkali.ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillAlkali.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillAlkali.YEAR.ToString()));
                }
                //月度
                if (!String.IsNullOrEmpty(tEnvFillAlkali.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillAlkali.MONTH.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillAlkali.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillAlkali.POINT_ID.ToString()));
                }
                //开始月
                if (!String.IsNullOrEmpty(tEnvFillAlkali.BEGIN_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MONTH = '{0}'", tEnvFillAlkali.BEGIN_MONTH.ToString()));
                }
                //开始日
                if (!String.IsNullOrEmpty(tEnvFillAlkali.BEGIN_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_DAY = '{0}'", tEnvFillAlkali.BEGIN_DAY.ToString()));
                }
                //开始时
                if (!String.IsNullOrEmpty(tEnvFillAlkali.BEGIN_HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_HOUR = '{0}'", tEnvFillAlkali.BEGIN_HOUR.ToString()));
                }
                //开始分
                if (!String.IsNullOrEmpty(tEnvFillAlkali.BEGIN_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BEGIN_MINUTE = '{0}'", tEnvFillAlkali.BEGIN_MINUTE.ToString()));
                }
                //结束月
                if (!String.IsNullOrEmpty(tEnvFillAlkali.END_MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_MONTH = '{0}'", tEnvFillAlkali.END_MONTH.ToString()));
                }
                //结束日
                if (!String.IsNullOrEmpty(tEnvFillAlkali.END_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_DAY = '{0}'", tEnvFillAlkali.END_DAY.ToString()));
                }
                //结束时
                if (!String.IsNullOrEmpty(tEnvFillAlkali.END_HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_HOUR = '{0}'", tEnvFillAlkali.END_HOUR.ToString()));
                }
                //结束分
                if (!String.IsNullOrEmpty(tEnvFillAlkali.END_MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND END_MINUTE = '{0}'", tEnvFillAlkali.END_MINUTE.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillAlkali.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAlkali.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillAlkali.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAlkali.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillAlkali.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAlkali.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillAlkali.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAlkali.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillAlkali.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAlkali.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 获取填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public DataTable GetAlkaliFillData(string year, string month)
        {
            StringBuilder sqlStr = new StringBuilder();
            sqlStr.Append("select * from T_ENV_FILL_ALKALI where 1=1 ");
            if (!string.IsNullOrEmpty(year))
                sqlStr.AppendFormat("and [year]='{0}' ", year);
            if (!string.IsNullOrEmpty(month))
                sqlStr.AppendFormat("and SMONTH='{0}'", month);

            return ExecuteDataTable(sqlStr.ToString());
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="dtData">数据集</param>
        /// <returns></returns>
        public string SaveAlkaliFillData(DataTable dtData)
        {
            if (dtData.Rows.Count > 0)
            {
                string year = dtData.Rows[0]["year"].ToString();
                string month = dtData.Rows[0]["SMONTH"].ToString();

                StringBuilder strSql = new StringBuilder();
                strSql.Append("begin Transaction T\n");
                strSql.Append("declare @errorCount as int\n");
                strSql.Append("set @errorCount=0\n");

                strSql.AppendFormat("delete from T_ENV_FILL_ALKALI where year='{0}' and SMONTH='{1}'\n", year, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                foreach (DataRow drData in dtData.Rows)
                {
                    string pointCode = drData["POINT_NUM"].ToString();
                    string pointName = drData["POINT_NAME"].ToString();
                    string smonth = drData["SMONTH"].ToString();
                    string sday = drData["SDAY"].ToString();
                    string shour = drData["SHOUR"].ToString();
                    string sminute = drData["SMINUTE"].ToString();
                    string emonth = drData["EMONTH"].ToString();
                    string eday = drData["EDAY"].ToString();
                    string ehour = drData["EHOUR"].ToString();
                    string eminute = drData["EMINUTE"].ToString();
                    string valuea = drData["VALUEA"].ToString();
                    string id = new TSysSerialAccess().GetSerialNumber("alkali_fill_id");

                    strSql.AppendFormat("insert into T_ENV_FILL_ALKALI(id,POINT_CODE,POINT_NUM,point_name,year,SMONTH,SDAY,SHOUR,SMINUTE,EMONTH,EDAY,EHOUR,EMINUTE,VALUEA) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}')", id, "130300", pointCode, pointName, year, smonth, sday, shour, sminute, emonth, eday, ehour, eminute, valuea);
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
    }
}
