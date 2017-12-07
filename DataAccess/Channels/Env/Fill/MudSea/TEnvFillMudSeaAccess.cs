using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.MudSea;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.MudSea
{
    /// <summary>
    /// 功能：沉积物（海水）数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillMudSeaAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillMudSea">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_MUD_SEA " + this.BuildWhereStatement(tEnvFillMudSea);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillMudSeaVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_SEA  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillMudSeaVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillMudSea">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillMudSeaVo Details(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_SEA " + this.BuildWhereStatement(tEnvFillMudSea));
            return SqlHelper.ExecuteObject(new TEnvFillMudSeaVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillMudSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillMudSeaVo> SelectByObject(TEnvFillMudSeaVo tEnvFillMudSea, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_MUD_SEA " + this.BuildWhereStatement(tEnvFillMudSea));
            return SqlHelper.ExecuteObjectList(tEnvFillMudSea, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillMudSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillMudSeaVo tEnvFillMudSea, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_MUD_SEA {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillMudSea));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillMudSea"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            string strSQL = "select * from T_ENV_FILL_MUD_SEA " + this.BuildWhereStatement(tEnvFillMudSea);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillMudSea">对象</param>
        /// <returns></returns>
        public TEnvFillMudSeaVo SelectByObject(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            string strSQL = "select * from T_ENV_FILL_MUD_SEA " + this.BuildWhereStatement(tEnvFillMudSea);
            return SqlHelper.ExecuteObject(new TEnvFillMudSeaVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillMudSea">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillMudSea, TEnvFillMudSeaVo.T_ENV_FILL_MUD_SEA_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudSea">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillMudSea, TEnvFillMudSeaVo.T_ENV_FILL_MUD_SEA_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillMudSea.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillMudSea_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillMudSea_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillMudSeaVo tEnvFillMudSea_UpdateSet, TEnvFillMudSeaVo tEnvFillMudSea_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillMudSea_UpdateSet, TEnvFillMudSeaVo.T_ENV_FILL_MUD_SEA_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillMudSea_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_MUD_SEA where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            string strSQL = "delete from T_ENV_FILL_MUD_SEA ";
            strSQL += this.BuildWhereStatement(tEnvFillMudSea);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillMudSea"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillMudSeaVo tEnvFillMudSea)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillMudSea)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillMudSea.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillMudSea.ID.ToString()));
                }
                //断面ID
                if (!String.IsNullOrEmpty(tEnvFillMudSea.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvFillMudSea.SECTION_ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillMudSea.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillMudSea.POINT_ID.ToString()));
                }
                //采样日期
                if (!String.IsNullOrEmpty(tEnvFillMudSea.SAMPLING_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLING_DAY = '{0}'", tEnvFillMudSea.SAMPLING_DAY.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillMudSea.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillMudSea.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillMudSea.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillMudSea.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillMudSea.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillMudSea.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillMudSea.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillMudSea.HOUR.ToString()));
                }
                //分
                if (!String.IsNullOrEmpty(tEnvFillMudSea.MINUTE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MINUTE = '{0}'", tEnvFillMudSea.MINUTE.ToString()));
                }
                //枯水期、平水期、枯水期
                if (!String.IsNullOrEmpty(tEnvFillMudSea.KPF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND KPF = '{0}'", tEnvFillMudSea.KPF.ToString()));
                }
                //评价
                if (!String.IsNullOrEmpty(tEnvFillMudSea.JUDGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND JUDGE = '{0}'", tEnvFillMudSea.JUDGE.ToString()));
                }
                //超标污染类别污染物
                if (!String.IsNullOrEmpty(tEnvFillMudSea.OVERPROOF.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OVERPROOF = '{0}'", tEnvFillMudSea.OVERPROOF.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillMudSea.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillMudSea.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillMudSea.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillMudSea.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillMudSea.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillMudSea.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillMudSea.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillMudSea.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillMudSea.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillMudSea.REMARK5.ToString()));
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
