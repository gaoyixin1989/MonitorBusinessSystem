using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Bottom;
using i3.ValueObject;
using i3.DataAccess.Sys.Resource;

namespace i3.DataAccess.Channels.Env.Fill.Bottom
{
    /// <summary>
    /// 功能：河流底泥数据填报表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillRiverBottomAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverBottom">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_RIVER_BOTTOM " + this.BuildWhereStatement(tEnvFillRiverBottom);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverBottomVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_BOTTOM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillRiverBottomVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverBottom">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverBottomVo Details(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_BOTTOM " + this.BuildWhereStatement(tEnvFillRiverBottom));
            return SqlHelper.ExecuteObject(new TEnvFillRiverBottomVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverBottom">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverBottomVo> SelectByObject(TEnvFillRiverBottomVo tEnvFillRiverBottom, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER_BOTTOM " + this.BuildWhereStatement(tEnvFillRiverBottom));
            return SqlHelper.ExecuteObjectList(tEnvFillRiverBottom, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverBottom">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverBottomVo tEnvFillRiverBottom, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_RIVER_BOTTOM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillRiverBottom));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverBottom"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_BOTTOM " + this.BuildWhereStatement(tEnvFillRiverBottom);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverBottom">对象</param>
        /// <returns></returns>
        public TEnvFillRiverBottomVo SelectByObject(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER_BOTTOM " + this.BuildWhereStatement(tEnvFillRiverBottom);
            return SqlHelper.ExecuteObject(new TEnvFillRiverBottomVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillRiverBottom">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillRiverBottom, TEnvFillRiverBottomVo.T_ENV_FILL_RIVER_BOTTOM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverBottom">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverBottom, TEnvFillRiverBottomVo.T_ENV_FILL_RIVER_BOTTOM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillRiverBottom.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverBottom_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverBottom_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverBottomVo tEnvFillRiverBottom_UpdateSet, TEnvFillRiverBottomVo tEnvFillRiverBottom_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiverBottom_UpdateSet, TEnvFillRiverBottomVo.T_ENV_FILL_RIVER_BOTTOM_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillRiverBottom_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_RIVER_BOTTOM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            string strSQL = "delete from T_ENV_FILL_RIVER_BOTTOM ";
            strSQL += this.BuildWhereStatement(tEnvFillRiverBottom);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 获取填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月</param>
        /// <param name="pointId">监测点</param>
        /// <returns></returns>
        public DataTable GetBottomFillData(string year, string month, string pointId)
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
	                                            T_ENV_POINT_BOTTOM t1
                                            left join 
	                                            T_ENV_POINT_BOTTOM_V t2 on t1.id=t2.river_sediment_id
                                            where 
                                                t1.is_del='0' and
                                                t1.year='{1}'";

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
		                                            T_ENV_POINT_BOTTOM_V_ITEM
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
        /// 保存填报数据
        /// </summary>
        /// <param name="dtData">填报数据</param>
        /// <returns></returns>
        public bool SaveBottomFillData(DataTable dtData)
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
                string kpf = drData["kpf"].ToString();
                string samplingDay = drData["sampling_day"].ToString();
                string fillId = new TSysSerialAccess().GetSerialNumber("bottom_fill_id");

                //删除原有数据
                strSql.AppendFormat("delete from T_ENV_FILL_RIVER_BOTTOM_ITEM where river_sediment_fill_id=(select id from T_ENV_FILL_RIVER_BOTTOM where river_sediment_point_id='{0}' and vertical_id='{1}' and MONTH='{2}')\n", pointId, verticalId, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");
                strSql.AppendFormat("delete from T_ENV_FILL_RIVER_BOTTOM where river_sediment_point_id='{0}' and vertical_id='{1}' and MONTH='{2}'\n", pointId, verticalId, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                //基础项
                strSql.AppendFormat("insert into T_ENV_FILL_RIVER_BOTTOM(id,river_sediment_point_id,month,vertical_id,kpf,sampling_day) values('{0}','{1}','{2}','{3}','{4}','{5}')\n", fillId, pointId, month, verticalId, kpf, samplingDay);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                //监测项
                foreach (DataColumn dcData in dtData.Columns)
                {
                    if (dcData.ColumnName.Contains("_id_unSure"))
                    {
                        string itemId = drData[dcData].ToString();
                        string itemValue = drData[dcData.ColumnName.Replace("_id", "")].ToString();
                        string fillItemId = new TSysSerialAccess().GetSerialNumber("bottom_fill_item_id");

                        strSql.AppendFormat("insert into T_ENV_FILL_RIVER_BOTTOM_ITEM(id,river_sediment_fill_id,item_id,item_value) values('{0}','{1}','{2}','{3}')\n", fillItemId, fillId, itemId, itemValue);
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
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillRiverBottom"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillRiverBottom)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillRiverBottom.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillRiverBottom.ID.ToString()));
                }
                //饮用水断面监测点ID
                if (!String.IsNullOrEmpty(tEnvFillRiverBottom.RIVER_SEDIMENT_POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RIVER_SEDIMENT_POINT_ID = '{0}'", tEnvFillRiverBottom.RIVER_SEDIMENT_POINT_ID.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillRiverBottom.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillRiverBottom.MONTH.ToString()));
                }
                //垂线ID，对应T_BAS_POINT_RIVER_VERTICAL主键
                if (!String.IsNullOrEmpty(tEnvFillRiverBottom.VERTICAL_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERTICAL_ID = '{0}'", tEnvFillRiverBottom.VERTICAL_ID.ToString()));
                }
                //枯水期、平水期、枯水期
                if (!String.IsNullOrEmpty(tEnvFillRiverBottom.KPF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KPF = '{0}'", tEnvFillRiverBottom.KPF.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillRiverBottom.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillRiverBottom.SAMPLING_DAY.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillRiverBottom.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillRiverBottom.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillRiverBottom.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillRiverBottom.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillRiverBottom.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillRiverBottom.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillRiverBottom.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillRiverBottom.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillRiverBottom.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillRiverBottom.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
