using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Solid;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Solid
{
    /// <summary>
    /// 功能：固废数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSolidAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSolid">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSolidVo tEnvFillSolid)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_SOLID " + this.BuildWhereStatement(tEnvFillSolid);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSolidVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SOLID  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillSolidVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSolid">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSolidVo Details(TEnvFillSolidVo tEnvFillSolid)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SOLID " + this.BuildWhereStatement(tEnvFillSolid));
            return SqlHelper.ExecuteObject(new TEnvFillSolidVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSolid">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSolidVo> SelectByObject(TEnvFillSolidVo tEnvFillSolid, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_SOLID " + this.BuildWhereStatement(tEnvFillSolid));
            return SqlHelper.ExecuteObjectList(tEnvFillSolid, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSolid">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSolidVo tEnvFillSolid, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_SOLID {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillSolid));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSolid"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSolidVo tEnvFillSolid)
        {
            string strSQL = "select * from T_ENV_FILL_SOLID " + this.BuildWhereStatement(tEnvFillSolid);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSolid">对象</param>
        /// <returns></returns>
        public TEnvFillSolidVo SelectByObject(TEnvFillSolidVo tEnvFillSolid)
        {
            string strSQL = "select * from T_ENV_FILL_SOLID " + this.BuildWhereStatement(tEnvFillSolid);
            return SqlHelper.ExecuteObject(new TEnvFillSolidVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillSolid">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSolidVo tEnvFillSolid)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillSolid, TEnvFillSolidVo.T_ENV_FILL_SOLID_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSolid">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSolidVo tEnvFillSolid)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSolid, TEnvFillSolidVo.T_ENV_FILL_SOLID_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillSolid.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSolid_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSolid_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSolidVo tEnvFillSolid_UpdateSet, TEnvFillSolidVo tEnvFillSolid_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSolid_UpdateSet, TEnvFillSolidVo.T_ENV_FILL_SOLID_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillSolid_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_SOLID where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillSolidVo tEnvFillSolid)
        {
            string strSQL = "delete from T_ENV_FILL_SOLID ";
            strSQL += this.BuildWhereStatement(tEnvFillSolid);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillSolid"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillSolidVo tEnvFillSolid)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillSolid)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillSolid.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillSolid.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillSolid.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillSolid.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillSolid.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillSolid.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillSolid.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillSolid.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillSolid.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillSolid.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillSolid.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillSolid.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillSolid.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillSolid.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillSolid.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillSolid.MINUTE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillSolid.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillSolid.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillSolid.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillSolid.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillSolid.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillSolid.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillSolid.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillSolid.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillSolid.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillSolid.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillSolid.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillSolid.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillSolid.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillSolid.REMARK5.ToString()));
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
            dr["code"] = "OVERPROOF";
            dr["name"] = "超标污染物";
            dt.Rows.Add(dr);

            return dt;
        }
    }

}
