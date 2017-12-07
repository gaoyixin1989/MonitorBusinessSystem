using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.River;
using System.Data;
using System.Collections;

namespace i3.DataAccess.Channels.Env.Fill.River
{
    /// <summary>
    /// 功能：河流填报
    /// 创建日期：2013-06-18
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiver">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverVo tEnvFillRiver)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_RIVER " + this.BuildWhereStatement(tEnvFillRiver);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillRiverVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiver">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverVo Details(TEnvFillRiverVo tEnvFillRiver)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER " + this.BuildWhereStatement(tEnvFillRiver));
            return SqlHelper.ExecuteObject(new TEnvFillRiverVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiver">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverVo> SelectByObject(TEnvFillRiverVo tEnvFillRiver, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_RIVER " + this.BuildWhereStatement(tEnvFillRiver));
            return SqlHelper.ExecuteObjectList(tEnvFillRiver, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiver">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverVo tEnvFillRiver, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_RIVER {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillRiver));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiver"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverVo tEnvFillRiver)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER " + this.BuildWhereStatement(tEnvFillRiver);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiver">对象</param>
        /// <returns></returns>
        public TEnvFillRiverVo SelectByObject(TEnvFillRiverVo tEnvFillRiver)
        {
            string strSQL = "select * from T_ENV_FILL_RIVER " + this.BuildWhereStatement(tEnvFillRiver);
            return SqlHelper.ExecuteObject(new TEnvFillRiverVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillRiver">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverVo tEnvFillRiver)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillRiver, TEnvFillRiverVo.T_ENV_FILL_RIVER_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiver">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverVo tEnvFillRiver)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiver, TEnvFillRiverVo.T_ENV_FILL_RIVER_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillRiver.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiver_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiver_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverVo tEnvFillRiver_UpdateSet, TEnvFillRiverVo tEnvFillRiver_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillRiver_UpdateSet, TEnvFillRiverVo.T_ENV_FILL_RIVER_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillRiver_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_RIVER where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillRiverVo tEnvFillRiver)
        {
            string strSQL = "delete from T_ENV_FILL_RIVER ";
            strSQL += this.BuildWhereStatement(tEnvFillRiver);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillRiver"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillRiverVo tEnvFillRiver)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillRiver)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillRiver.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillRiver.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvFillRiver.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvFillRiver.SECTION_ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillRiver.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillRiver.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillRiver.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillRiver.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillRiver.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillRiver.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillRiver.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillRiver.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillRiver.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillRiver.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillRiver.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillRiver.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillRiver.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillRiver.MINUTE.ToString()));
                }
                //枯水期、平水期、枯水期
                if (!String.IsNullOrEmpty(tEnvFillRiver.KPF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KPF = '{0}'", tEnvFillRiver.KPF.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillRiver.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillRiver.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillRiver.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillRiver.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillRiver.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillRiver.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillRiver.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillRiver.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillRiver.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillRiver.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillRiver.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillRiver.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillRiver.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillRiver.REMARK5.ToString()));
                }
                //状态
                if (!String.IsNullOrEmpty(tEnvFillRiver.STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND STATUS = '{0}'", tEnvFillRiver.STATUS.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        /// <summary>
        /// 获取河流数据填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="pointId">断面ID</param>
        /// <returns></returns>
        public DataTable GetRiverFillData(string year, string month, string SectionID)
        {
            string sqlStr = @"select '' FillID,a.ID SECTION_ID,a.YEAR,a.MONTH,a.SECTION_NAME,b.ID POINT_ID,b.VERTICAL_NAME,'' SAMPLING_DAY,'' KPF,'' OVERPROOF
                              from T_ENV_P_RIVER a
                              inner join T_ENV_P_RIVER_V b on(a.ID=b.SECTION_ID) 
                              where YEAR='{0}' and MONTH='{1}' and a.IS_DEL='0'";

            if (!string.IsNullOrEmpty(SectionID))
                sqlStr += " and a.ID='" + SectionID + "'";

            sqlStr = string.Format(sqlStr, year, month);

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
	                                            b.ID,
	                                            b.ITEM_NAME
                                            from 
                                            (
	                                            select 
		                                            ITEM_ID 
	                                            from 
		                                            T_ENV_P_RIVER_V_ITEM
	                                            where
		                                            POINT_ID in({0})
	                                            group by
		                                            ITEM_ID
                                            ) a
                                            left join 
	                                            T_BASE_ITEM_INFO b on a.ITEM_ID=b.ID
                                            where
	                                            b.is_del='0'";
            strSql = string.Format(strSql, verticalId);

            return ExecuteDataTable(strSql);
        }
        /// <summary>
        /// 根据条件查询所有填报数据
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="SectionID"></param>
        /// <returns></returns>
        public DataTable GetFillData(string year, string month, string SectionID)
        {
            string sqlStr = @"select a.ID,SECTION_ID,POINT_ID,SAMPLING_DAY,YEAR,MONTH,KPF,OVERPROOF,ITEM_ID,ITEM_VALUE 
                              from T_ENV_FILL_RIVER a left join T_ENV_FILL_RIVER_ITEM b on(a.ID=b.FILL_ID) 
                              where a.YEAR='{0}' and a.MONTH='{1}'";

            if (!string.IsNullOrEmpty(SectionID))
                sqlStr += " and a.SECTION_ID='" + SectionID + "'";

            sqlStr = string.Format(sqlStr, year, month);

            return ExecuteDataTable(sqlStr);
        }

        /// <summary>
        /// 去除不必要更新的数据字段
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="UpdateName"></param>
        /// <returns></returns>
        public DataTable ChangeDataTable(DataTable dt, string UpdateName, string unSureMark)
        {
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (dt.Columns[i].ColumnName.IndexOf("_NAME") != -1 || dt.Columns[i].ColumnName.IndexOf("__") != -1 || (dt.Columns[i].ColumnName.IndexOf(unSureMark) != -1 && dt.Columns[i].ColumnName != UpdateName))
                {
                    dt.Columns.Remove(dt.Columns[i].ColumnName);
                    i--;
                }
            }

            return dt;
        }

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
            dr["code"] = "SECTION_ID";
            dr["name"] = "断面名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "POINT_ID";
            dr["name"] = "垂线名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "DAY";
            dr["name"] = "日期";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "KPF";
            dr["name"] = "水期";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "OVERPROOF";
            dr["name"] = "超标污染物";
            dt.Rows.Add(dr);

            return dt;
        }
    }

}
