using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Env.Point.Estuaries;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Env.Point.Estuaries
{
    /// <summary>
    /// 功能：入海河口垂线表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvPointEstuariesVerticalAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            string strSQL = "select Count(*) from T_ENV_P_ESTUARIES_V " + this.BuildWhereStatement(tEnvPointEstuariesVertical);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVerticalVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_ENV_P_ESTUARIES_V  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TEnvPointEstuariesVerticalVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVerticalVo Details(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            string strSQL = String.Format("select * from  T_ENV_P_ESTUARIES_V " + this.BuildWhereStatement(tEnvPointEstuariesVertical));
            return SqlHelper.ExecuteObject(new TEnvPointEstuariesVerticalVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointEstuariesVerticalVo> SelectByObject(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_ENV_P_ESTUARIES_V " + this.BuildWhereStatement(tEnvPointEstuariesVertical));
            return SqlHelper.ExecuteObjectList(tEnvPointEstuariesVertical, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical, int iIndex, int iCount)
        {

            string strSQL = " select * from T_ENV_P_ESTUARIES_V {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tEnvPointEstuariesVertical));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            string strSQL = "select * from T_ENV_P_ESTUARIES_V " + this.BuildWhereStatement(tEnvPointEstuariesVertical);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">对象</param>
        /// <returns></returns>
        public TEnvPointEstuariesVerticalVo SelectByObject(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            string strSQL = "select * from T_ENV_P_ESTUARIES_V " + this.BuildWhereStatement(tEnvPointEstuariesVertical);
            return SqlHelper.ExecuteObject(new TEnvPointEstuariesVerticalVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tEnvPointEstuariesVertical, TEnvPointEstuariesVerticalVo.T_ENV_POINT_ESTUARIES_VERTICAL_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointEstuariesVertical, TEnvPointEstuariesVerticalVo.T_ENV_POINT_ESTUARIES_VERTICAL_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tEnvPointEstuariesVertical.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointEstuariesVertical_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical_UpdateSet, TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tEnvPointEstuariesVertical_UpdateSet, TEnvPointEstuariesVerticalVo.T_ENV_POINT_ESTUARIES_VERTICAL_TABLE);
            strSQL += this.BuildWhereStatement(tEnvPointEstuariesVertical_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_ENV_P_ESTUARIES_V where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            string strSQL = "delete from T_ENV_P_ESTUARIES_V ";
            strSQL += this.BuildWhereStatement(tEnvPointEstuariesVertical);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tEnvPointEstuariesVertical"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TEnvPointEstuariesVerticalVo tEnvPointEstuariesVertical)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tEnvPointEstuariesVertical)
            {

                //ID
                if (!String.IsNullOrEmpty(tEnvPointEstuariesVertical.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tEnvPointEstuariesVertical.ID.ToString()));
                }
                //入海河口监测点ID
                if (!String.IsNullOrEmpty(tEnvPointEstuariesVertical.SECTION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECTION_ID = '{0}'", tEnvPointEstuariesVertical.SECTION_ID.ToString()));
                }
                //垂线名称
                if (!String.IsNullOrEmpty(tEnvPointEstuariesVertical.VERTICAL_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND VERTICAL_NAME = '{0}'", tEnvPointEstuariesVertical.VERTICAL_NAME.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tEnvPointEstuariesVertical.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tEnvPointEstuariesVertical.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tEnvPointEstuariesVertical.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tEnvPointEstuariesVertical.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tEnvPointEstuariesVertical.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tEnvPointEstuariesVertical.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tEnvPointEstuariesVertical.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tEnvPointEstuariesVertical.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tEnvPointEstuariesVertical.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tEnvPointEstuariesVertical.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
