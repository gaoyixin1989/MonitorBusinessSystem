using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.AirHour;
using System.Data;

namespace i3.DataAccess.Channels.Env.Fill.AirHour
{
    /// <summary>
    /// 功能：环境空气填报（小时）
    /// 创建日期：2013-06-27
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillAirhourAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAirhour">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirhourVo tEnvFillAirhour)
        {
            string strSQL = "select Count(*) from T_ENV_FILL_AIRHOUR " + this.BuildWhereStatement(tEnvFillAirhour);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirhourVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIRHOUR  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvFillAirhourVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAirhour">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirhourVo Details(TEnvFillAirhourVo tEnvFillAirhour)
        {
            string strSQL = String.Format("select * from  T_ENV_FILL_AIRHOUR " + this.BuildWhereStatement(tEnvFillAirhour));
            return SqlHelper.ExecuteObject(new TEnvFillAirhourVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAirhour">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirhourVo> SelectByObject(TEnvFillAirhourVo tEnvFillAirhour, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_FILL_AIRHOUR " + this.BuildWhereStatement(tEnvFillAirhour));
            return SqlHelper.ExecuteObjectList(tEnvFillAirhour, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAirhour">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirhourVo tEnvFillAirhour, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_FILL_AIRHOUR {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvFillAirhour));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAirhour"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirhourVo tEnvFillAirhour)
        {
            string strSQL = "select * from T_ENV_FILL_AIRHOUR " + this.BuildWhereStatement(tEnvFillAirhour);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAirhour">对象</param>
        /// <returns></returns>
        public TEnvFillAirhourVo SelectByObject(TEnvFillAirhourVo tEnvFillAirhour)
        {
            string strSQL = "select * from T_ENV_FILL_AIRHOUR " + this.BuildWhereStatement(tEnvFillAirhour);
            return SqlHelper.ExecuteObject(new TEnvFillAirhourVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvFillAirhour">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirhourVo tEnvFillAirhour)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvFillAirhour, TEnvFillAirhourVo.T_ENV_FILL_AIRHOUR_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirhour">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirhourVo tEnvFillAirhour)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAirhour, TEnvFillAirhourVo.T_ENV_FILL_AIRHOUR_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvFillAirhour.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirhour_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAirhour_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirhourVo tEnvFillAirhour_UpdateSet, TEnvFillAirhourVo tEnvFillAirhour_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvFillAirhour_UpdateSet, TEnvFillAirhourVo.T_ENV_FILL_AIRHOUR_TABLE);
            strSQL += this.BuildWhereStatement(tEnvFillAirhour_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_FILL_AIRHOUR where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvFillAirhourVo tEnvFillAirhour)
        {
            string strSQL = "delete from T_ENV_FILL_AIRHOUR ";
            strSQL += this.BuildWhereStatement(tEnvFillAirhour);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region// 构造填报表需要显示的信息
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
            dr["name"] = "监测点名称";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "MONTH";
            dr["name"] = "月份";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "DAY";
            dr["name"] = "日";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "HOUR";
            dr["name"] = "时";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "TEMPERATRUE";
            dr["name"] = "气温";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "PRESSURE";
            dr["name"] = "气压";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "WIND_SPEED";
            dr["name"] = "风速";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "WIND_DIRECTION";
            dr["name"] = "风向";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "VISIBLITY";
            dr["name"] = "能见度";
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["code"] = "HUMIDITY";
            dr["name"] = "相对湿度";
            dt.Rows.Add(dr);
            return dt;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvFillAirhour"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvFillAirhourVo tEnvFillAirhour)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvFillAirhour)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvFillAirhour.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvFillAirhour.ID.ToString()));
                }
                //监测点ID
                if (!String.IsNullOrEmpty(tEnvFillAirhour.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tEnvFillAirhour.POINT_ID.ToString()));
                }
                //年度
                if (!String.IsNullOrEmpty(tEnvFillAirhour.YEAR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND YEAR = '{0}'", tEnvFillAirhour.YEAR.ToString()));
                }
                //月份
                if (!String.IsNullOrEmpty(tEnvFillAirhour.MONTH.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONTH = '{0}'", tEnvFillAirhour.MONTH.ToString()));
                }
                //日
                if (!String.IsNullOrEmpty(tEnvFillAirhour.DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DAY = '{0}'", tEnvFillAirhour.DAY.ToString()));
                }
                //时
                if (!String.IsNullOrEmpty(tEnvFillAirhour.HOUR.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HOUR = '{0}'", tEnvFillAirhour.HOUR.ToString()));
                }
                //气温
                if (!String.IsNullOrEmpty(tEnvFillAirhour.TEMPERATRUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEMPERATRUE = '{0}'", tEnvFillAirhour.TEMPERATRUE.ToString()));
                }
                //气压
                if (!String.IsNullOrEmpty(tEnvFillAirhour.PRESSURE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PRESSURE = '{0}'", tEnvFillAirhour.PRESSURE.ToString()));
                }
                //风速
                if (!String.IsNullOrEmpty(tEnvFillAirhour.WIND_SPEED.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WIND_SPEED = '{0}'", tEnvFillAirhour.WIND_SPEED.ToString()));
                }
                //风向
                if (!String.IsNullOrEmpty(tEnvFillAirhour.WIND_DIRECTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND WIND_DIRECTION = '{0}'", tEnvFillAirhour.WIND_DIRECTION.ToString()));
                }
                //API指数
                if (!String.IsNullOrEmpty(tEnvFillAirhour.VISIBLITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VISIBLITY = '{0}'", tEnvFillAirhour.VISIBLITY.ToString()));
                }
                //空气质量指数
                if (!String.IsNullOrEmpty(tEnvFillAirhour.HUMIDITY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HUMIDITY = '{0}'", tEnvFillAirhour.HUMIDITY.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvFillAirhour.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvFillAirhour.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvFillAirhour.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvFillAirhour.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvFillAirhour.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvFillAirhour.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvFillAirhour.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvFillAirhour.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvFillAirhour.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvFillAirhour.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
