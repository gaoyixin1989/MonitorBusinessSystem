using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.RiverCity;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.RiverCity
{
    /// <summary>
    /// 功能：城考
    /// 创建日期：2014-01-21
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverCityVAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverCityV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            string strSQL = "select Count(*) from T_ENV_P_RIVER_CITY_V " + this.BuildWhereStatement(tEnvPRiverCityV);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverCityVVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_CITY_V  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPRiverCityVVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverCityV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverCityVVo Details(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_CITY_V " + this.BuildWhereStatement(tEnvPRiverCityV));
            return SqlHelper.ExecuteObject(new TEnvPRiverCityVVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverCityV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverCityVVo> SelectByObject(TEnvPRiverCityVVo tEnvPRiverCityV, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_RIVER_CITY_V " + this.BuildWhereStatement(tEnvPRiverCityV));
            return SqlHelper.ExecuteObjectList(tEnvPRiverCityV, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverCityV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverCityVVo tEnvPRiverCityV, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_RIVER_CITY_V {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPRiverCityV));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverCityV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            string strSQL = "select * from T_ENV_P_RIVER_CITY_V " + this.BuildWhereStatement(tEnvPRiverCityV);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverCityV">对象</param>
        /// <returns></returns>
        public TEnvPRiverCityVVo SelectByObject(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            string strSQL = "select * from T_ENV_P_RIVER_CITY_V " + this.BuildWhereStatement(tEnvPRiverCityV);
            return SqlHelper.ExecuteObject(new TEnvPRiverCityVVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPRiverCityV">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPRiverCityV, TEnvPRiverCityVVo.T_ENV_P_RIVER_CITY_V_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverCityV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverCityV, TEnvPRiverCityVVo.T_ENV_P_RIVER_CITY_V_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPRiverCityV.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverCityV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverCityV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverCityVVo tEnvPRiverCityV_UpdateSet, TEnvPRiverCityVVo tEnvPRiverCityV_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverCityV_UpdateSet, TEnvPRiverCityVVo.T_ENV_P_RIVER_CITY_V_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPRiverCityV_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_RIVER_CITY_V where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            string strSQL = "delete from T_ENV_P_RIVER_CITY_V ";
            strSQL += this.BuildWhereStatement(tEnvPRiverCityV);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPRiverCityV"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPRiverCityVVo tEnvPRiverCityV)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPRiverCityV)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPRiverCityV.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPRiverCityV.ID.ToString()));
                }
                //城考断面ID
                if (!String.IsNullOrEmpty(tEnvPRiverCityV.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvPRiverCityV.SECTION_ID.ToString()));
                }
                //垂线名称
                if (!String.IsNullOrEmpty(tEnvPRiverCityV.VERTICAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERTICAL_NAME = '{0}'", tEnvPRiverCityV.VERTICAL_NAME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPRiverCityV.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPRiverCityV.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPRiverCityV.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPRiverCityV.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPRiverCityV.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPRiverCityV.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPRiverCityV.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPRiverCityV.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPRiverCityV.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPRiverCityV.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
