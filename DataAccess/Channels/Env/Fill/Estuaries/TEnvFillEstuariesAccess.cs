using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Estuaries;
using i3.ValueObject;
using i3.DataAccess.Sys.Resource;

namespace i3.DataAccess.Channels.Env.Fill.Estuaries
{
    /// <summary>
    /// 功能：入海河口数据填报
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvFillEstuariesAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillEstuaries">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillEstuariesVo tEnvFillEstuaries)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_ESTUARIES " + this.BuildWhereStatement(tEnvFillEstuaries);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillEstuariesVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ESTUARIES  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillEstuariesVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillEstuaries">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillEstuariesVo Details(TEnvFillEstuariesVo tEnvFillEstuaries)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ESTUARIES " + this.BuildWhereStatement(tEnvFillEstuaries));
            return SqlHelper.ExecuteObject(new TEnvFillEstuariesVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillEstuaries">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillEstuariesVo> SelectByObject(TEnvFillEstuariesVo tEnvFillEstuaries, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_ESTUARIES " + this.BuildWhereStatement(tEnvFillEstuaries));
            return SqlHelper.ExecuteObjectList(tEnvFillEstuaries, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillEstuaries">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillEstuariesVo tEnvFillEstuaries, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_ESTUARIES {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillEstuaries));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillEstuaries"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillEstuariesVo tEnvFillEstuaries)
        {
            string strSQL = "select * from T_ENV_FILL_ESTUARIES " + this.BuildWhereStatement(tEnvFillEstuaries);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillEstuaries">对象</param>
        /// <returns></returns>
        public TEnvFillEstuariesVo SelectByObject(TEnvFillEstuariesVo tEnvFillEstuaries)
        {
            string strSQL = "select * from T_ENV_FILL_ESTUARIES " + this.BuildWhereStatement(tEnvFillEstuaries);
            return SqlHelper.ExecuteObject(new TEnvFillEstuariesVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillEstuaries">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillEstuariesVo tEnvFillEstuaries)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillEstuaries, TEnvFillEstuariesVo.T_ENV_FILL_ESTUARIES_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillEstuaries">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillEstuariesVo tEnvFillEstuaries)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillEstuaries, TEnvFillEstuariesVo.T_ENV_FILL_ESTUARIES_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillEstuaries.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillEstuaries_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillEstuaries_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillEstuariesVo tEnvFillEstuaries_UpdateSet, TEnvFillEstuariesVo tEnvFillEstuaries_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillEstuaries_UpdateSet, TEnvFillEstuariesVo.T_ENV_FILL_ESTUARIES_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillEstuaries_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_ESTUARIES where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillEstuariesVo tEnvFillEstuaries)
        {
            string strSQL = "delete from T_ENV_FILL_ESTUARIES ";
            strSQL += this.BuildWhereStatement(tEnvFillEstuaries);

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
            dr["code"] = "SECTION_ID";
            dr["name"] = "断面";
            dt.Rows.Add(dr);


            dr = dt.NewRow();
            dr["code"] = "POINT_ID";
            dr["name"] = "监测点名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "YEAR";
            dr["name"] = "年份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);
             
            //dr = dt.NewRow();
            //dr["code"] = "DAY";
            //dr["name"] = "日";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "HOUR";
            //dr["name"] = "时";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "MINUTE";
            //dr["name"] = "分";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "JUDGE";
            //dr["name"] = "评价";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "OVERPROOF";
            //dr["name"] = "超标污染物";
            //dt.Rows.Add(dr);

            return dt;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillEstuaries"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillEstuariesVo tEnvFillEstuaries)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillEstuaries)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillEstuaries.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvFillEstuaries.SECTION_ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillEstuaries.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillEstuaries.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillEstuaries.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillEstuaries.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillEstuaries.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillEstuaries.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillEstuaries.MINUTE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillEstuaries.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillEstuaries.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillEstuaries.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillEstuaries.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillEstuaries.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillEstuaries.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillEstuaries.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillEstuaries.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 获取入海河口数据填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetEstuariesFillData(string year, string month, string pointId)
        {
            string sqlStr = @"select 
	                                            t1.id as point_id,
	                                            t1.[year],
	                                            t1.section_name,
	                                            t2.id as vertical_id,
	                                            t2.vertical_name,
	                                            '{0}' as [month],
	                                            '' as sampling_day,
	                                            '' as kpf
                                            from 
	                                            T_ENV_POINT_Estuaries t1
                                            left join 
	                                            T_ENV_POINT_Estuaries_vertical t2 on t1.id=t2.estuaries_id
                                             where 
                                                t1.year='{1}' and
                                                t1.is_del='0'";

            if (!string.IsNullOrEmpty(pointId))
                sqlStr += " and t1.id='" + pointId + "'";

            sqlStr = string.Format(sqlStr, month, year);

            return ExecuteDataTable(sqlStr);
        }

        /// <summary>
        /// 查询要监测的监测项
        /// </summary>
        /// <param name="verticalId">垂线ID</param>
        /// <returns></returns>
        public DataTable GetFillItem(string verticalId)
        {
            string strSql = @"select 
	                                            t2.id,
	                                            t2.item_name
                                            from 
                                            (
	                                            select 
		                                            item_id 
	                                            from 
		                                            T_ENV_POINT_Estuaries_v_ITEM
	                                            where
		                                            vertical_id in({0})
	                                            group by
		                                            item_id
                                            ) t1
                                            left join 
	                                            T_BASE_ITEM_INFO t2 on t1.item_id=t2.id
                                            where
	                                            t2.is_del='0'";
            strSql = string.Format(strSql, verticalId);

            return ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="dtData">填报数据</param>
        /// <returns></returns>
        public bool SaveEstuariesFillData(DataTable dtData)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("begin Transaction T\n");
            strSql.Append("declare @errorCount as int\n");
            strSql.Append("set @errorCount=0\n");

            foreach (DataRow drData in dtData.Rows)
            {
                string pointId = drData["point_id"].ToString();
                string month = drData["month"].ToString();
                string verticalId = drData["vertical_id"].ToString();
                string samplingDay = drData["sampling_day"].ToString();
                string fillId = new TSysSerialAccess().GetSerialNumber("estuaries_fill_id");

                //删除原有数据
                strSql.AppendFormat("delete from T_ENV_FILL_Estuaries_ITEM where RIVER_METAL_FILL_ID=(select id from T_ENV_FILL_Estuaries where RIVER_METAL_POINT_ID='{0}' and vertical_id='{1}' and MONTH='{2}')\n", pointId, verticalId, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");
                strSql.AppendFormat("delete from T_ENV_FILL_Estuaries where RIVER_METAL_POINT_ID='{0}' and vertical_id='{1}' and MONTH='{2}'\n", pointId, verticalId, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                //基础项
                strSql.AppendFormat("insert into T_ENV_FILL_Estuaries(id,RIVER_METAL_POINT_ID,month,vertical_id,sampling_day) values('{0}','{1}','{2}','{3}','{4}')\n", fillId, pointId, month, verticalId, samplingDay);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                //监测项
                foreach (DataColumn dcData in dtData.Columns)
                {
                    if (dcData.ColumnName.Contains("_id_unSure"))
                    {
                        string itemId = drData[dcData].ToString();
                        string itemValue = drData[dcData.ColumnName.Replace("_id", "")].ToString();
                        string fillItemId = new TSysSerialAccess().GetSerialNumber("river_fill_item_id");

                        strSql.AppendFormat("insert into T_ENV_FILL_Estuaries_ITEM(id,RIVER_METAL_FILL_ID,item_id,item_value) values('{0}','{1}','{2}','{3}')\n", fillItemId, fillId, itemId, itemValue);
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
