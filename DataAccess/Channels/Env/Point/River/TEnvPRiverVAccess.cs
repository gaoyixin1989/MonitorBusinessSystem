using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.River;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.River
{
    /// <summary>
    /// 功能：河流
    /// 创建日期：2013-06-13
    /// 创建人：魏林
    /// </summary>
    public class TEnvPRiverVAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPRiverV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPRiverVVo tEnvPRiverV)
        {
            string strSQL = "select Count(*) from T_ENV_P_RIVER_V " + this.BuildWhereStatement(tEnvPRiverV);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPRiverVVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_V  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPRiverVVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPRiverV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPRiverVVo Details(TEnvPRiverVVo tEnvPRiverV)
        {
            string strSQL = String.Format("select * from  T_ENV_P_RIVER_V " + this.BuildWhereStatement(tEnvPRiverV));
            return SqlHelper.ExecuteObject(new TEnvPRiverVVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPRiverV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPRiverVVo> SelectByObject(TEnvPRiverVVo tEnvPRiverV, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_RIVER_V " + this.BuildWhereStatement(tEnvPRiverV));
            return SqlHelper.ExecuteObjectList(tEnvPRiverV, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPRiverV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPRiverVVo tEnvPRiverV, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_RIVER_V {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPRiverV));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPRiverV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPRiverVVo tEnvPRiverV)
        {
            string strSQL = "select * from T_ENV_P_RIVER_V " + this.BuildWhereStatement(tEnvPRiverV);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPRiverV">对象</param>
        /// <returns></returns>
        public TEnvPRiverVVo SelectByObject(TEnvPRiverVVo tEnvPRiverV)
        {
            string strSQL = "select * from T_ENV_P_RIVER_V " + this.BuildWhereStatement(tEnvPRiverV);
            return SqlHelper.ExecuteObject(new TEnvPRiverVVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPRiverV">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPRiverVVo tEnvPRiverV)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPRiverV, TEnvPRiverVVo.T_ENV_P_RIVER_V_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverVVo tEnvPRiverV)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverV, TEnvPRiverVVo.T_ENV_P_RIVER_V_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPRiverV.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPRiverV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPRiverV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPRiverVVo tEnvPRiverV_UpdateSet, TEnvPRiverVVo tEnvPRiverV_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPRiverV_UpdateSet, TEnvPRiverVVo.T_ENV_P_RIVER_V_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPRiverV_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_RIVER_V where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPRiverVVo tEnvPRiverV)
        {
            string strSQL = "delete from T_ENV_P_RIVER_V ";
            strSQL += this.BuildWhereStatement(tEnvPRiverV);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPRiverV"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPRiverVVo tEnvPRiverV)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPRiverV)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPRiverV.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPRiverV.ID.ToString()));
                }
                //河流断面ID
                if (!String.IsNullOrEmpty(tEnvPRiverV.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvPRiverV.SECTION_ID.ToString()));
                }
                //垂线名称
                if (!String.IsNullOrEmpty(tEnvPRiverV.VERTICAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERTICAL_NAME = '{0}'", tEnvPRiverV.VERTICAL_NAME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPRiverV.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPRiverV.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPRiverV.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPRiverV.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPRiverV.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPRiverV.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPRiverV.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPRiverV.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPRiverV.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPRiverV.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
