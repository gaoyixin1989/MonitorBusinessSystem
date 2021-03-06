using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Fill.Algae;
using i3.ValueObject;
using i3.DataAccess.Sys.Resource;

namespace i3.DataAccess.Channels.Env.Fill.Algae
{
    /// <summary>
    /// 功能：蓝藻水华数据填报
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillAlgaeAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAlgae">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_ALGAE " + this.BuildWhereStatement(tEnvFillAlgae);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAlgaeVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ALGAE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillAlgaeVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAlgae">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAlgaeVo Details(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_ALGAE " + this.BuildWhereStatement(tEnvFillAlgae));
            return SqlHelper.ExecuteObject(new TEnvFillAlgaeVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAlgae">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAlgaeVo> SelectByObject(TEnvFillAlgaeVo tEnvFillAlgae, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_ALGAE " + this.BuildWhereStatement(tEnvFillAlgae));
            return SqlHelper.ExecuteObjectList(tEnvFillAlgae, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAlgae">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAlgaeVo tEnvFillAlgae, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_ALGAE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAlgae));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAlgae"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            string strSQL = "select * from T_ENV_FILL_ALGAE " + this.BuildWhereStatement(tEnvFillAlgae);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAlgae">对象</param>
        /// <returns></returns>
        public TEnvFillAlgaeVo SelectByObject(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            string strSQL = "select * from T_ENV_FILL_ALGAE " + this.BuildWhereStatement(tEnvFillAlgae);
            return SqlHelper.ExecuteObject(new TEnvFillAlgaeVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAlgae">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAlgae, TEnvFillAlgaeVo.T_ENV_FILL_ALGAE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlgae">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAlgae, TEnvFillAlgaeVo.T_ENV_FILL_ALGAE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAlgae.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlgae_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAlgae_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlgaeVo tEnvFillAlgae_UpdateSet, TEnvFillAlgaeVo tEnvFillAlgae_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAlgae_UpdateSet, TEnvFillAlgaeVo.T_ENV_FILL_ALGAE_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillAlgae_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_ALGAE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            string strSQL = "delete from T_ENV_FILL_ALGAE ";
            strSQL += this.BuildWhereStatement(tEnvFillAlgae);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAlgae"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAlgae)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillAlgae.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAlgae.ID.ToString()));
                }
                //饮用水断面监测点ID
                if (!String.IsNullOrEmpty(tEnvFillAlgae.ALGAE_POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ALGAE_POINT_ID = '{0}'", tEnvFillAlgae.ALGAE_POINT_ID.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillAlgae.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillAlgae.MONTH.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillAlgae.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillAlgae.SAMPLING_DAY.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillAlgae.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAlgae.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillAlgae.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAlgae.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillAlgae.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAlgae.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillAlgae.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAlgae.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillAlgae.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAlgae.REMARK5.ToString()));
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
        public DataTable GetAlgaeFillData(string year, string month, string pointId)
        {
            string sqlStr = @"select 
	                                            t1.id as point_id,
	                                            t1.[year],
	                                            t1.point_name,
	                                            '{0}' as [month],
	                                            '' as sampling_day
                                            from 
	                                            T_ENV_POINT_ALGAE t1
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
		                                            T_ENV_POINT_ALGAE_ITEM
	                                            {0}
	                                            group by
		                                            item_id
                                            ) t1
                                            left join 
	                                            T_BASE_ITEM_INFO t2 on t1.item_id=t2.id
                                            where
	                                            t2.is_del='0'";
            strSql = string.Format(strSql, (!string.IsNullOrEmpty(pointId) && pointId != "''" ? " where ALGAE_POINT_ID in(" + pointId + ")" : ""));

            return ExecuteDataTable(strSql);
        }

        /// <summary>
        /// 保存填报数据
        /// </summary>
        /// <param name="dtData">填报数据</param>
        /// <returns></returns>
        public bool SaveAlgaeFillData(DataTable dtData)
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
                string fillId = new TSysSerialAccess().GetSerialNumber("algae_fill_id");

                //删除原有数据
                strSql.AppendFormat("delete from T_ENV_FILL_ALGAE_ITEM where ALGAE_FILL_ID=(select id from T_ENV_FILL_ALGAE where ALGAE_POINT_ID='{0}' and MONTH='{1}')\n", pointId, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");
                strSql.AppendFormat("delete from T_ENV_FILL_ALGAE where ALGAE_POINT_ID='{0}' and MONTH='{1}'\n", pointId, month);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                //基础项
                strSql.AppendFormat("insert into T_ENV_FILL_ALGAE(id,ALGAE_POINT_ID,month,sampling_day) values('{0}','{1}','{2}','{3}')\n", fillId, pointId, month, samplingDay);
                strSql.Append("set @errorCount=@errorCount+@@error\n");

                //监测项
                foreach (DataColumn dcData in dtData.Columns)
                {
                    if (dcData.ColumnName.Contains("_id_unSure"))
                    {
                        string itemId = drData[dcData].ToString();
                        string itemValue = drData[dcData.ColumnName.Replace("_id", "")].ToString();
                        string fillItemId = new TSysSerialAccess().GetSerialNumber("algae_fill_item_id");

                        strSql.AppendFormat("insert into T_ENV_FILL_ALGAE_ITEM(id,ALGAE_FILL_ID,item_id,item_value) values('{0}','{1}','{2}','{3}')\n", fillItemId, fillId, itemId, itemValue);
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
