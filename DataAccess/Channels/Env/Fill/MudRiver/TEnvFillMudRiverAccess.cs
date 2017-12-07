using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.MudRiver;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.MudRiver
{
    /// <summary>
    /// 功能：沉积物（河流）数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillMudRiverAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillMudRiver">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_MUD_RIVER " + this.BuildWhereStatement(tEnvFillMudRiver);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillMudRiverVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_RIVER  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillMudRiverVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillMudRiver">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillMudRiverVo Details(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_RIVER " + this.BuildWhereStatement(tEnvFillMudRiver));
            return SqlHelper.ExecuteObject(new TEnvFillMudRiverVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillMudRiver">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillMudRiverVo> SelectByObject(TEnvFillMudRiverVo tEnvFillMudRiver, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_RIVER " + this.BuildWhereStatement(tEnvFillMudRiver));
            return SqlHelper.ExecuteObjectList(tEnvFillMudRiver, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillMudRiver">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillMudRiverVo tEnvFillMudRiver, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_MUD_RIVER {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillMudRiver));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillMudRiver"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            string strSQL = "select * from T_ENV_FILL_MUD_RIVER " + this.BuildWhereStatement(tEnvFillMudRiver);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillMudRiver">对象</param>
        /// <returns></returns>
        public TEnvFillMudRiverVo SelectByObject(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            string strSQL = "select * from T_ENV_FILL_MUD_RIVER " + this.BuildWhereStatement(tEnvFillMudRiver);
            return SqlHelper.ExecuteObject(new TEnvFillMudRiverVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillMudRiver">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillMudRiver, TEnvFillMudRiverVo.T_ENV_FILL_MUD_RIVER_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudRiver">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillMudRiver, TEnvFillMudRiverVo.T_ENV_FILL_MUD_RIVER_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillMudRiver.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudRiver_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillMudRiver_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudRiverVo tEnvFillMudRiver_UpdateSet, TEnvFillMudRiverVo tEnvFillMudRiver_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillMudRiver_UpdateSet, TEnvFillMudRiverVo.T_ENV_FILL_MUD_RIVER_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillMudRiver_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_MUD_RIVER where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            string strSQL = "delete from T_ENV_FILL_MUD_RIVER ";
            strSQL += this.BuildWhereStatement(tEnvFillMudRiver);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillMudRiver"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillMudRiverVo tEnvFillMudRiver)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillMudRiver)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillMudRiver.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvFillMudRiver.SECTION_ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillMudRiver.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillMudRiver.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillMudRiver.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillMudRiver.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillMudRiver.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillMudRiver.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillMudRiver.MINUTE.ToString()));
                }
                //枯水期、平水期、枯水期
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.KPF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KPF = '{0}'", tEnvFillMudRiver.KPF.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillMudRiver.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillMudRiver.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillMudRiver.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillMudRiver.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillMudRiver.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillMudRiver.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillMudRiver.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillMudRiver.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

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
