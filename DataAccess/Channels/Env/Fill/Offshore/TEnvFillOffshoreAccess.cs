using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Offshore;
using i3.ValueObject;
using i3.DataAccess.Sys.Resource;

namespace i3.DataAccess.Channels.Env.Fill.Offshore
{
    /// <summary>
    /// 功能：近岸直排数据填报
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-25
    /// </summary>
    public class TEnvFillOffshoreAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillOffshore">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_OFFSHORE " + this.BuildWhereStatement(tEnvFillOffshore);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillOffshoreVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_OFFSHORE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillOffshoreVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillOffshore">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillOffshoreVo Details(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_OFFSHORE " + this.BuildWhereStatement(tEnvFillOffshore));
            return SqlHelper.ExecuteObject(new TEnvFillOffshoreVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillOffshore">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillOffshoreVo> SelectByObject(TEnvFillOffshoreVo tEnvFillOffshore, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_OFFSHORE " + this.BuildWhereStatement(tEnvFillOffshore));
            return SqlHelper.ExecuteObjectList(tEnvFillOffshore, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillOffshore">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillOffshoreVo tEnvFillOffshore, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_OFFSHORE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillOffshore));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillOffshore"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            string strSQL = "select * from T_ENV_FILL_OFFSHORE " + this.BuildWhereStatement(tEnvFillOffshore);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillOffshore">对象</param>
        /// <returns></returns>
        public TEnvFillOffshoreVo SelectByObject(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            string strSQL = "select * from T_ENV_FILL_OFFSHORE " + this.BuildWhereStatement(tEnvFillOffshore);
            return SqlHelper.ExecuteObject(new TEnvFillOffshoreVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillOffshore">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillOffshore, TEnvFillOffshoreVo.T_ENV_FILL_OFFSHORE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillOffshore">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillOffshore, TEnvFillOffshoreVo.T_ENV_FILL_OFFSHORE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillOffshore.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillOffshore_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillOffshore_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillOffshoreVo tEnvFillOffshore_UpdateSet, TEnvFillOffshoreVo tEnvFillOffshore_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillOffshore_UpdateSet, TEnvFillOffshoreVo.T_ENV_FILL_OFFSHORE_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillOffshore_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_OFFSHORE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            string strSQL = "delete from T_ENV_FILL_OFFSHORE ";
            strSQL += this.BuildWhereStatement(tEnvFillOffshore);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region// 构造填报表需要显示的信息(ljn)
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
            dr["code"] = "POINT_ID";
            dr["name"] = "排污口名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "SAMPLING_DAY";
            //dr["name"] = "采样日期";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "DAY";
            dr["name"] = "日";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "HOUR";
            //dr["name"] = "时";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "MINUTE";
            //dr["name"] = "分";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "COMPANY_NAME";
            dr["name"] = "企业名称";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "POINT_CODE";
            //dr["name"] = "排污口代码";
            //dt.Rows.Add(dr);
             
            //dr = dt.NewRow();
            //dr["code"] = "POINT_NAME";
            //dr["name"] = "排污口名称";
            //dt.Rows.Add(dr); 

            dr = dt.NewRow();
            dr["code"] = "JUDGE";
            dr["name"] = "是否达标";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "OVERPROOF";
            dr["name"] = "不达标项目";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "FLUX";
            dr["name"] = "污水流量";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "LET_TIME";
            dr["name"] = "污水排放时间";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "WATER_TOTAL";
            dr["name"] = "污水量";
            dt.Rows.Add(dr);

            return dt;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillOffshore"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillOffshore)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillOffshore.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillOffshore.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillOffshore.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillOffshore.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillOffshore.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillOffshore.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillOffshore.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillOffshore.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillOffshore.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillOffshore.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillOffshore.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillOffshore.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillOffshore.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillOffshore.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillOffshore.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillOffshore.MINUTE.ToString()));
                }
                //企业代码
                if (!String.IsNullOrEmpty(tEnvFillOffshore.COMPANY_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND COMPANY_NAME = '{0}'", tEnvFillOffshore.COMPANY_NAME.ToString()));
                }
                //排污口代码
                if (!String.IsNullOrEmpty(tEnvFillOffshore.POINT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_CODE = '{0}'", tEnvFillOffshore.POINT_CODE.ToString()));
                }
                //排污口名称
                if (!String.IsNullOrEmpty(tEnvFillOffshore.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tEnvFillOffshore.POINT_NAME.ToString()));
                }
                //是否达标
                if (!String.IsNullOrEmpty(tEnvFillOffshore.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillOffshore.JUDGE.ToString()));
                }
                //不达标项目
                if (!String.IsNullOrEmpty(tEnvFillOffshore.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillOffshore.OVERPROOF.ToString()));
                }
                //污水流量
                if (!String.IsNullOrEmpty(tEnvFillOffshore.FLUX.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FLUX = '{0}'", tEnvFillOffshore.FLUX.ToString()));
                }
                //污水排放时间
                if (!String.IsNullOrEmpty(tEnvFillOffshore.LET_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LET_TIME = '{0}'", tEnvFillOffshore.LET_TIME.ToString()));
                }
                //污水量
                if (!String.IsNullOrEmpty(tEnvFillOffshore.WATER_TOTAL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WATER_TOTAL = '{0}'", tEnvFillOffshore.WATER_TOTAL.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillOffshore.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillOffshore.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillOffshore.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillOffshore.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillOffshore.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillOffshore.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillOffshore.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillOffshore.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillOffshore.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillOffshore.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 获取填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月</param>
        /// <param name="pointId">监测点</param>
        /// <returns></returns>
        public DataTable GetOffShoreFillData(string year, string month, string pointId)
        {
            string sqlStr = @"select 
	                                            t1.id as point_id,
	                                            t1.[year],
	                                            t1.company_name,
	                                            '{0}' as [month],
	                                            '' as sampling_day
                                            from 
	                                            T_ENV_POINT_OFFSHORE t1
                                            where 
                                                t1.year='{1}' and
                                                t1.is_del='0'";

            if (!string.IsNullOrEmpty(pointId))
                sqlStr += " and t1.id='" + pointId + "'";

            sqlStr = string.Format(sqlStr, month, year);

            return ExecuteDataTable(sqlStr);
        }

        /// <summary>
        /// 获取要填报的监测项
        /// </summary>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetFillItem(string pointId)
        {
            string strSql = @"select 
	                                            t2.id,
	                                            t2.item_name
                                            from 
                                            (
	                                            select 
		                                            item_id 
	                                            from 
		                                            T_ENV_POINT_OFFSHORE_ITEM
		                                        {0}
	                                            group by
		                                            item_id
                                            ) t1
                                            left join 
	                                            T_BASE_ITEM_INFO t2 on t1.item_id=t2.id
                                            where
	                                            t2.is_del='0'";
            strSql = string.Format(strSql, (!string.IsNullOrEmpty(pointId) && pointId != "''" ? " where OFFSHORE_POINT_ID in(" + pointId + ")" : ""));

            return ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 保存填报数据
        /// </summary>
        /// <param name="dtData">填报数据</param>
        /// <returns></returns>
        public bool SaveOffShoreFillData(DataTable dtData)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("begin Transaction T\n");
            strSql.Append("declare @errorCount as int\n");
            strSql.Append("set @errorCount=0\n");

            foreach (DataRow drData in dtData.Rows)
            {
                string pointId = drData["point_id"].ToString();
                string month = drData["month"].ToString();
                string samplingDay = drData["sampling_day"].ToString();
                string fillId = new TSysSerialAccess().GetSerialNumber("offshore_fill_id");

                //删除原有数据
                strSql.AppendFormat("delete from T_ENV_FILL_OFFSHORE_ITEM where OFFSHORE_FILL_ID=(select id from T_ENV_FILL_OFFSHORE where OFFSHORE_POINT_ID='{0}' and MONTH='{1}')\n", pointId, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");
                strSql.AppendFormat("delete from T_ENV_FILL_OFFSHORE where OFFSHORE_POINT_ID='{0}' and MONTH='{1}'\n", pointId, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                //基础项
                strSql.AppendFormat("insert into T_ENV_FILL_OFFSHORE(id,OFFSHORE_POINT_ID,month,sampling_day) values('{0}','{1}','{2}','{3}')\n", fillId, pointId, month, samplingDay);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                //监测项
                foreach (DataColumn dcData in dtData.Columns)
                {
                    if (dcData.ColumnName.Contains("_id_unSure"))
                    {
                        string itemId = drData[dcData].ToString();
                        string itemValue = drData[dcData.ColumnName.Replace("_id", "")].ToString();
                        string fillItemId = new TSysSerialAccess().GetSerialNumber("offshore_fill_item_id");

                        strSql.AppendFormat("insert into T_ENV_FILL_OFFSHORE_ITEM(id,OFFSHORE_FILL_ID,item_id,item_value) values('{0}','{1}','{2}','{3}')\n", fillItemId, fillId, itemId, itemValue);
                        strSql.Append("set @errorCount=@errorCount+@@error\n");
                    }
                }
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

            DataTable dtResult = ExecuteDataTable(strSql.ToString());
            bool result = (dtResult.Rows[0][0].ToString() == "success" ? true : false);

            return result;
        }
    }
}
