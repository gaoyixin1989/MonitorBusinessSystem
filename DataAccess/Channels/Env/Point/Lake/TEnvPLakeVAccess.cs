using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Lake;
using System.Data;

namespace i3.DataAccess.Channels.Env.Point.Lake
{
    /// <summary>
    /// 功能：湖库
    /// 创建日期：2013-06-13
    /// 创建人：魏林
    /// </summary>
    public class TEnvPLakeVAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPLakeV">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPLakeVVo tEnvPLakeV)
        {
            string strSQL = "select Count(*) from T_ENV_P_LAKE_V " + this.BuildWhereStatement(tEnvPLakeV);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPLakeVVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_LAKE_V  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPLakeVVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPLakeV">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPLakeVVo Details(TEnvPLakeVVo tEnvPLakeV)
        {
            string strSQL = String.Format("select * from  T_ENV_P_LAKE_V " + this.BuildWhereStatement(tEnvPLakeV));
            return SqlHelper.ExecuteObject(new TEnvPLakeVVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPLakeV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPLakeVVo> SelectByObject(TEnvPLakeVVo tEnvPLakeV, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_LAKE_V " + this.BuildWhereStatement(tEnvPLakeV));
            return SqlHelper.ExecuteObjectList(tEnvPLakeV, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPLakeV">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPLakeVVo tEnvPLakeV, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_LAKE_V {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPLakeV));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPLakeV"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPLakeVVo tEnvPLakeV)
        {
            string strSQL = "select * from T_ENV_P_LAKE_V " + this.BuildWhereStatement(tEnvPLakeV);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPLakeV">对象</param>
        /// <returns></returns>
        public TEnvPLakeVVo SelectByObject(TEnvPLakeVVo tEnvPLakeV)
        {
            string strSQL = "select * from T_ENV_P_LAKE_V " + this.BuildWhereStatement(tEnvPLakeV);
            return SqlHelper.ExecuteObject(new TEnvPLakeVVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPLakeV">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPLakeVVo tEnvPLakeV)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPLakeV, TEnvPLakeVVo.T_ENV_P_LAKE_V_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPLakeV">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPLakeVVo tEnvPLakeV)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPLakeV, TEnvPLakeVVo.T_ENV_P_LAKE_V_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPLakeV.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPLakeV_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPLakeV_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPLakeVVo tEnvPLakeV_UpdateSet, TEnvPLakeVVo tEnvPLakeV_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPLakeV_UpdateSet, TEnvPLakeVVo.T_ENV_P_LAKE_V_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPLakeV_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_LAKE_V where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPLakeVVo tEnvPLakeV)
        {
            string strSQL = "delete from T_ENV_P_LAKE_V ";
            strSQL += this.BuildWhereStatement(tEnvPLakeV);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPLakeV"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPLakeVVo tEnvPLakeV)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPLakeV)
            {

                //主键ID
                if (!String.IsNullOrEmpty(tEnvPLakeV.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPLakeV.ID.ToString()));
                }
                //湖库断面ID
                if (!String.IsNullOrEmpty(tEnvPLakeV.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvPLakeV.SECTION_ID.ToString()));
                }
                //垂线名称
                if (!String.IsNullOrEmpty(tEnvPLakeV.VERTICAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERTICAL_NAME = '{0}'", tEnvPLakeV.VERTICAL_NAME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPLakeV.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPLakeV.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPLakeV.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPLakeV.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPLakeV.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPLakeV.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPLakeV.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPLakeV.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPLakeV.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPLakeV.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
