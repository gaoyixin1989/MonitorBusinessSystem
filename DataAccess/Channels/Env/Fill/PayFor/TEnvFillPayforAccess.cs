using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.PayFor;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.PayFor
{
    /// <summary>
    /// 功能：生态补偿数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillPayforAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillPayfor">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillPayforVo tEnvFillPayfor)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_PAYFOR " + this.BuildWhereStatement(tEnvFillPayfor);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillPayforVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_PAYFOR  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillPayforVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillPayfor">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillPayforVo Details(TEnvFillPayforVo tEnvFillPayfor)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_PAYFOR " + this.BuildWhereStatement(tEnvFillPayfor));
            return SqlHelper.ExecuteObject(new TEnvFillPayforVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillPayfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillPayforVo> SelectByObject(TEnvFillPayforVo tEnvFillPayfor, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_PAYFOR " + this.BuildWhereStatement(tEnvFillPayfor));
            return SqlHelper.ExecuteObjectList(tEnvFillPayfor, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillPayfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillPayforVo tEnvFillPayfor, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_PAYFOR {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillPayfor));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillPayfor"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillPayforVo tEnvFillPayfor)
        {
            string strSQL = "select * from T_ENV_FILL_PAYFOR " + this.BuildWhereStatement(tEnvFillPayfor);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillPayfor">对象</param>
        /// <returns></returns>
        public TEnvFillPayforVo SelectByObject(TEnvFillPayforVo tEnvFillPayfor)
        {
            string strSQL = "select * from T_ENV_FILL_PAYFOR " + this.BuildWhereStatement(tEnvFillPayfor);
            return SqlHelper.ExecuteObject(new TEnvFillPayforVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillPayfor">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillPayforVo tEnvFillPayfor)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillPayfor, TEnvFillPayforVo.T_ENV_FILL_PAYFOR_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPayfor">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPayforVo tEnvFillPayfor)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPayfor, TEnvFillPayforVo.T_ENV_FILL_PAYFOR_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillPayfor.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPayfor_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillPayfor_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPayforVo tEnvFillPayfor_UpdateSet, TEnvFillPayforVo tEnvFillPayfor_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillPayfor_UpdateSet, TEnvFillPayforVo.T_ENV_FILL_PAYFOR_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillPayfor_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_PAYFOR where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillPayforVo tEnvFillPayfor)
        {
            string strSQL = "delete from T_ENV_FILL_PAYFOR ";
            strSQL += this.BuildWhereStatement(tEnvFillPayfor);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillPayfor"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillPayforVo tEnvFillPayfor)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillPayfor)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillPayfor.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillPayfor.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillPayfor.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillPayfor.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillPayfor.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillPayfor.SAMPLING_DAY.ToString()));
                }
                //扣缴金额
                if (!String.IsNullOrEmpty(tEnvFillPayfor.FEE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FEE = '{0}'", tEnvFillPayfor.FEE.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillPayfor.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillPayfor.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillPayfor.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillPayfor.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillPayfor.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillPayfor.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillPayfor.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillPayfor.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillPayfor.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillPayfor.MINUTE.ToString()));
                }
                //上游断面水质结果COD浓度
                if (!String.IsNullOrEmpty(tEnvFillPayfor.UP_COD.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UP_COD = '{0}'", tEnvFillPayfor.UP_COD.ToString()));
                }
                //上游断面水质结果氨氮浓度
                if (!String.IsNullOrEmpty(tEnvFillPayfor.UP_NH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND UP_NH = '{0}'", tEnvFillPayfor.UP_NH.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillPayfor.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillPayfor.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillPayfor.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillPayfor.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillPayfor.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillPayfor.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillPayfor.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillPayfor.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillPayfor.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillPayfor.REMARK5.ToString()));
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
            dr["code"] = "POINT_ID";
            dr["name"] = "测点名称";
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
            dr["code"] = "FEE";
            dr["name"] = "扣缴金额";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "UP_COD";
            dr["name"] = "上游断面COD浓度";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "UP_NH";
            dr["name"] = "上游断面氨氮浓度";
            dt.Rows.Add(dr);

            return dt;
        }
    }

}
