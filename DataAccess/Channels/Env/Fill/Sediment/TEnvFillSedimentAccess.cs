using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Sediment;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.Sediment
{
    /// <summary>
    /// 功能：底泥重金属填报
    /// 创建日期：2014-10-23
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillSedimentAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSediment">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSedimentVo tEnvFillSediment)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_SEDIMENT " + this.BuildWhereStatement(tEnvFillSediment);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSedimentVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SEDIMENT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillSedimentVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSediment">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSedimentVo Details(TEnvFillSedimentVo tEnvFillSediment)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_SEDIMENT " + this.BuildWhereStatement(tEnvFillSediment));
            return SqlHelper.ExecuteObject(new TEnvFillSedimentVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSediment">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSedimentVo> SelectByObject(TEnvFillSedimentVo tEnvFillSediment, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_SEDIMENT " + this.BuildWhereStatement(tEnvFillSediment));
            return SqlHelper.ExecuteObjectList(tEnvFillSediment, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSediment">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSedimentVo tEnvFillSediment, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_SEDIMENT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillSediment));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSediment"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSedimentVo tEnvFillSediment)
        {
            string strSQL = "select * from T_ENV_FILL_SEDIMENT " + this.BuildWhereStatement(tEnvFillSediment);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSediment">对象</param>
        /// <returns></returns>
        public TEnvFillSedimentVo SelectByObject(TEnvFillSedimentVo tEnvFillSediment)
        {
            string strSQL = "select * from T_ENV_FILL_SEDIMENT " + this.BuildWhereStatement(tEnvFillSediment);
            return SqlHelper.ExecuteObject(new TEnvFillSedimentVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillSediment">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSedimentVo tEnvFillSediment)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillSediment, TEnvFillSedimentVo.T_ENV_FILL_SEDIMENT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSediment">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSedimentVo tEnvFillSediment)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSediment, TEnvFillSedimentVo.T_ENV_FILL_SEDIMENT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillSediment.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSediment_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSediment_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSedimentVo tEnvFillSediment_UpdateSet, TEnvFillSedimentVo tEnvFillSediment_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillSediment_UpdateSet, TEnvFillSedimentVo.T_ENV_FILL_SEDIMENT_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillSediment_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_SEDIMENT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillSedimentVo tEnvFillSediment)
        {
            string strSQL = "delete from T_ENV_FILL_SEDIMENT ";
            strSQL += this.BuildWhereStatement(tEnvFillSediment);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillSediment"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillSedimentVo tEnvFillSediment)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillSediment)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillSediment.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillSediment.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillSediment.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillSediment.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillSediment.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillSediment.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillSediment.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillSediment.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillSediment.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillSediment.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillSediment.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillSediment.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillSediment.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillSediment.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillSediment.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillSediment.MINUTE.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillSediment.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillSediment.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillSediment.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillSediment.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillSediment.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillSediment.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillSediment.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillSediment.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillSediment.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillSediment.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillSediment.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillSediment.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillSediment.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillSediment.REMARK5.ToString()));
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
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "DAY";
            dr["name"] = "日期";
            dt.Rows.Add(dr);

            //dr = dt.NewRow();
            //dr["code"] = "POINT_CODE";
            //dr["name"] = "河流名称";
            //dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "POINT_ID";
            dr["name"] = "断面名称";
            dt.Rows.Add(dr);

            

            return dt;
        }
    }

}
