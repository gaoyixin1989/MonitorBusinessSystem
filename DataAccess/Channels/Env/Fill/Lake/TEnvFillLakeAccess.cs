﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Lake;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Lake
{
    /// <summary>
    /// 功能：湖库填报
    /// 创建日期：2013-06-22
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillLakeAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillLake">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillLakeVo tEnvFillLake)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_LAKE " + this.BuildWhereStatement(tEnvFillLake);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillLakeVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_LAKE  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillLakeVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillLake">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillLakeVo Details(TEnvFillLakeVo tEnvFillLake)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_LAKE " + this.BuildWhereStatement(tEnvFillLake));
            return SqlHelper.ExecuteObject(new TEnvFillLakeVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillLake">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillLakeVo> SelectByObject(TEnvFillLakeVo tEnvFillLake, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_LAKE " + this.BuildWhereStatement(tEnvFillLake));
            return SqlHelper.ExecuteObjectList(tEnvFillLake, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillLake">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillLakeVo tEnvFillLake, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_LAKE {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillLake));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillLake"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillLakeVo tEnvFillLake)
        {
            string strSQL = "select * from T_ENV_FILL_LAKE " + this.BuildWhereStatement(tEnvFillLake);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillLake">对象</param>
        /// <returns></returns>
        public TEnvFillLakeVo SelectByObject(TEnvFillLakeVo tEnvFillLake)
        {
            string strSQL = "select * from T_ENV_FILL_LAKE " + this.BuildWhereStatement(tEnvFillLake);
            return SqlHelper.ExecuteObject(new TEnvFillLakeVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillLake">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillLakeVo tEnvFillLake)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillLake, TEnvFillLakeVo.T_ENV_FILL_LAKE_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillLake">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillLakeVo tEnvFillLake)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillLake, TEnvFillLakeVo.T_ENV_FILL_LAKE_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillLake.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillLake_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillLake_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillLakeVo tEnvFillLake_UpdateSet, TEnvFillLakeVo tEnvFillLake_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillLake_UpdateSet, TEnvFillLakeVo.T_ENV_FILL_LAKE_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillLake_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_LAKE where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillLakeVo tEnvFillLake)
        {
            string strSQL = "delete from T_ENV_FILL_LAKE ";
            strSQL += this.BuildWhereStatement(tEnvFillLake);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillLake"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillLakeVo tEnvFillLake)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillLake)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillLake.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillLake.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvFillLake.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvFillLake.SECTION_ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillLake.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillLake.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillLake.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillLake.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillLake.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillLake.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillLake.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillLake.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillLake.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillLake.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillLake.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillLake.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillLake.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillLake.MINUTE.ToString()));
                }
                //枯水期、平水期、枯水期
                if (!String.IsNullOrEmpty(tEnvFillLake.KPF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KPF = '{0}'", tEnvFillLake.KPF.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillLake.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillLake.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillLake.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillLake.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillLake.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillLake.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillLake.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillLake.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillLake.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillLake.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillLake.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillLake.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillLake.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillLake.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

        #region//构造填报表需要显示的信息 (QHD)
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
        #endregion

        #region//构造填报表需要显示的信息 (QHD)
        /// <summary>
        /// 构造填报表需要显示的信息 
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT_ZZ()
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
            return dt;
        }
        #endregion
    }

}
