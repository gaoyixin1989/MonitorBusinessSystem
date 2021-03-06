using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Sea;
using i3.ValueObject;
using i3.DataAccess.Sys.Resource;

namespace i3.DataAccess.Channels.Env.Fill.Sea
{
    /// <summary>
    /// 功能：近海海域数据填报
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvFillSeaAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSea">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSeaVo tEnvFillSea)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_SEA " + this.BuildWhereStatement(tEnvFillSea);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSeaVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SEA  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillSeaVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSea">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSeaVo Details(TEnvFillSeaVo tEnvFillSea)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SEA " + this.BuildWhereStatement(tEnvFillSea));
            return SqlHelper.ExecuteObject(new TEnvFillSeaVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSeaVo> SelectByObject(TEnvFillSeaVo tEnvFillSea, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_SEA " + this.BuildWhereStatement(tEnvFillSea));
            return SqlHelper.ExecuteObjectList(tEnvFillSea, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSeaVo tEnvFillSea, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_SEA {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillSea));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSea"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSeaVo tEnvFillSea)
        {
            string strSQL = "select * from T_ENV_FILL_SEA " + this.BuildWhereStatement(tEnvFillSea);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSea">对象</param>
        /// <returns></returns>
        public TEnvFillSeaVo SelectByObject(TEnvFillSeaVo tEnvFillSea)
        {
            string strSQL = "select * from T_ENV_FILL_SEA " + this.BuildWhereStatement(tEnvFillSea);
            return SqlHelper.ExecuteObject(new TEnvFillSeaVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillSea">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSeaVo tEnvFillSea)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillSea, TEnvFillSeaVo.T_ENV_FILL_SEA_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSea">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeaVo tEnvFillSea)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSea, TEnvFillSeaVo.T_ENV_FILL_SEA_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillSea.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSea_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSea_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeaVo tEnvFillSea_UpdateSet, TEnvFillSeaVo tEnvFillSea_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSea_UpdateSet, TEnvFillSeaVo.T_ENV_FILL_SEA_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillSea_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_SEA where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillSeaVo tEnvFillSea)
        {
            string strSQL = "delete from T_ENV_FILL_SEA ";
            strSQL += this.BuildWhereStatement(tEnvFillSea);

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
            dr["code"] = "POINT_ID";
            dr["name"] = "监测点名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "DAY";
            dr["name"] = "日";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "SAMPLING_DAY";
            //dr["name"] = "采样日期";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "HOUR";
            //dr["name"] = "时";
            //dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "MINUTE";
            //dr["name"] = "分";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "SEA_AREA_CODE";
            dr["name"] = "海区代码";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "KEY_AREA_CODE";
            dr["name"] = "重点海域代码";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "DEPTH";
            dr["name"] = "水深";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "KPF";
            dr["name"] = "水期";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "Season";
            dr["name"] = "季度";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "LEVEL";
            dr["name"] = "层次";
            dt.Rows.Add(dr);

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
        /// <param name="tEnvFillSea"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillSeaVo tEnvFillSea)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillSea)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillSea.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillSea.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillSea.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillSea.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillSea.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillSea.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillSea.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillSea.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillSea.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillSea.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillSea.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillSea.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillSea.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillSea.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillSea.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillSea.MINUTE.ToString()));
                }
                //水深
                if (!String.IsNullOrEmpty(tEnvFillSea.DEPTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DEPTH = '{0}'", tEnvFillSea.DEPTH.ToString()));
                }
                //海区代码
                if (!String.IsNullOrEmpty(tEnvFillSea.SEA_AREA_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SEA_AREA_CODE = '{0}'", tEnvFillSea.SEA_AREA_CODE.ToString()));
                }
                //重点海域代码
                if (!String.IsNullOrEmpty(tEnvFillSea.KEY_AREA_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KEY_AREA_CODE = '{0}'", tEnvFillSea.KEY_AREA_CODE.ToString()));
                }
                //枯水期、平水期、枯水期
                if (!String.IsNullOrEmpty(tEnvFillSea.KPF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KPF = '{0}'", tEnvFillSea.KPF.ToString()));
                }
                //层次
                if (!String.IsNullOrEmpty(tEnvFillSea.LEVEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LEVEL = '{0}'", tEnvFillSea.LEVEL.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillSea.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillSea.JUDGE.ToString()));
                }
                //超标污染物
                if (!String.IsNullOrEmpty(tEnvFillSea.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillSea.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillSea.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillSea.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillSea.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillSea.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillSea.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillSea.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillSea.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillSea.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillSea.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillSea.REMARK5.ToString()));
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
        public DataTable GetSeaFillData(string year, string month, string pointId)
        {
            string sqlStr = @"select 
	                                            t1.id as point_id,
	                                            t1.[year],
	                                            t1.sea_name,
	                                            '{0}' as [month],
	                                            '' as sampling_day,
                                                '' as sample_num
                                            from 
	                                            T_ENV_POINT_SEA t1
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
		                                            T_ENV_POINT_SEA_ITEM
	                                            {0}
	                                            group by
		                                            item_id
                                            ) t1
                                            left join 
	                                            T_BASE_ITEM_INFO t2 on t1.item_id=t2.id
                                            where
	                                            t2.is_del='0'";
            strSql = string.Format(strSql, (!string.IsNullOrEmpty(pointId) && pointId != "''" ? " where SEA_POINT_ID in(" + pointId + ")" : ""));

            return ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 保存填报数据
        /// </summary>
        /// <param name="dtData">填报数据</param>
        /// <returns></returns>
        public bool SaveSeaFillData(DataTable dtData)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("begin Transaction T\n");
            strSql.Append("declare @errorCount as int\n");

            foreach (DataRow drData in dtData.Rows)
            {
                string pointId = drData["point_id"].ToString();
                string month = drData["month"].ToString();
                string samplingDay = drData["sampling_day"].ToString();
                string sampleNum = drData["sample_num"].ToString();
                string fillId = new TSysSerialAccess().GetSerialNumber("sea_fill_id");

                //删除原有数据
                strSql.AppendFormat("delete from T_ENV_FILL_SEA_ITEM where SEA_FILL_ID=(select id from T_ENV_FILL_SEA where SEA_POINT_ID='{0}' and MONTH='{1}')\n", pointId, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");
                strSql.AppendFormat("delete from T_ENV_FILL_SEA where SEA_POINT_ID='{0}' and MONTH='{1}'\n", pointId, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                //基础项
                strSql.AppendFormat("insert into T_ENV_FILL_SEA(id,SEA_POINT_ID,month,sampling_day,sample_num) values('{0}','{1}','{2}','{3}','{4}')\n", fillId, pointId, month, samplingDay, sampleNum);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                //监测项
                foreach (DataColumn dcData in dtData.Columns)
                {
                    if (dcData.ColumnName.Contains("_id_unSure"))
                    {
                        string itemId = drData[dcData].ToString();
                        string itemValue = drData[dcData.ColumnName.Replace("_id", "")].ToString();
                        string fillItemId = new TSysSerialAccess().GetSerialNumber("sea_fill_item_id");

                        strSql.AppendFormat("insert into T_ENV_FILL_SEA_ITEM(id,SEA_FILL_ID,item_id,item_value) values('{0}','{1}','{2}','{3}')\n", fillItemId, fillId, itemId, itemValue);
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
