using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Soil;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Soil
{
    /// <summary>
    /// 功能：土壤数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSoilAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSoil">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSoilVo tEnvFillSoil)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_SOIL " + this.BuildWhereStatement(tEnvFillSoil);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSoilVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SOIL  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillSoilVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSoil">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSoilVo Details(TEnvFillSoilVo tEnvFillSoil)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SOIL " + this.BuildWhereStatement(tEnvFillSoil));
            return SqlHelper.ExecuteObject(new TEnvFillSoilVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSoil">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSoilVo> SelectByObject(TEnvFillSoilVo tEnvFillSoil, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_SOIL " + this.BuildWhereStatement(tEnvFillSoil));
            return SqlHelper.ExecuteObjectList(tEnvFillSoil, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSoil">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSoilVo tEnvFillSoil, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_SOIL {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillSoil));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSoil"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSoilVo tEnvFillSoil)
        {
            string strSQL = "select * from T_ENV_FILL_SOIL " + this.BuildWhereStatement(tEnvFillSoil);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSoil">对象</param>
        /// <returns></returns>
        public TEnvFillSoilVo SelectByObject(TEnvFillSoilVo tEnvFillSoil)
        {
            string strSQL = "select * from T_ENV_FILL_SOIL " + this.BuildWhereStatement(tEnvFillSoil);
            return SqlHelper.ExecuteObject(new TEnvFillSoilVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillSoil">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSoilVo tEnvFillSoil)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillSoil, TEnvFillSoilVo.T_ENV_FILL_SOIL_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSoil">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSoilVo tEnvFillSoil)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSoil, TEnvFillSoilVo.T_ENV_FILL_SOIL_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillSoil.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSoil_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSoil_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSoilVo tEnvFillSoil_UpdateSet, TEnvFillSoilVo tEnvFillSoil_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSoil_UpdateSet, TEnvFillSoilVo.T_ENV_FILL_SOIL_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillSoil_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_SOIL where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillSoilVo tEnvFillSoil)
        {
            string strSQL = "delete from T_ENV_FILL_SOIL ";
            strSQL += this.BuildWhereStatement(tEnvFillSoil);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillSoil"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillSoilVo tEnvFillSoil)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillSoil)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillSoil.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillSoil.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillSoil.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillSoil.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillSoil.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillSoil.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillSoil.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillSoil.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillSoil.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillSoil.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillSoil.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillSoil.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillSoil.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillSoil.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillSoil.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillSoil.MINUTE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillSoil.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillSoil.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillSoil.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillSoil.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillSoil.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillSoil.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillSoil.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillSoil.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillSoil.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillSoil.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillSoil.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillSoil.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillSoil.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillSoil.REMARK5.ToString()));
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
