using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.Resource;
using i3.ValueObject;

namespace i3.DataAccess.Sys.Resource
{
    /// <summary>
    /// 功能：采样分析监测流程排序配置
    /// 创建日期：2013-04-01
    /// 创建人：邵世卓
    /// </summary>
    public class TSysConfigFlownumAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysConfigFlownum">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysConfigFlownumVo tSysConfigFlownum)
        {
            string strSQL = "select Count(*) from T_SYS_CONFIG_FLOWNUM " + this.BuildWhereStatement(tSysConfigFlownum);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysConfigFlownumVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_CONFIG_FLOWNUM  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TSysConfigFlownumVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysConfigFlownum">对象条件</param>
        /// <returns>对象</returns>
        public TSysConfigFlownumVo Details(TSysConfigFlownumVo tSysConfigFlownum)
        {
            string strSQL = String.Format("select * from  T_SYS_CONFIG_FLOWNUM " + this.BuildWhereStatement(tSysConfigFlownum));
            return SqlHelper.ExecuteObject(new TSysConfigFlownumVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysConfigFlownum">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysConfigFlownumVo> SelectByObject(TSysConfigFlownumVo tSysConfigFlownum, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_SYS_CONFIG_FLOWNUM " + this.BuildWhereStatement(tSysConfigFlownum));
            return SqlHelper.ExecuteObjectList(tSysConfigFlownum, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysConfigFlownum">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysConfigFlownumVo tSysConfigFlownum, int iIndex, int iCount)
        {

            string strSQL = " select * from T_SYS_CONFIG_FLOWNUM {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysConfigFlownum));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysConfigFlownum"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysConfigFlownumVo tSysConfigFlownum)
        {
            string strSQL = "select * from T_SYS_CONFIG_FLOWNUM " + this.BuildWhereStatement(tSysConfigFlownum);
            strSQL += " order by FLOW_NUM";
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysConfigFlownum">对象</param>
        /// <returns></returns>
        public TSysConfigFlownumVo SelectByObject(TSysConfigFlownumVo tSysConfigFlownum)
        {
            string strSQL = "select * from T_SYS_CONFIG_FLOWNUM " + this.BuildWhereStatement(tSysConfigFlownum);
            return SqlHelper.ExecuteObject(new TSysConfigFlownumVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysConfigFlownum">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysConfigFlownumVo tSysConfigFlownum)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysConfigFlownum, TSysConfigFlownumVo.T_SYS_CONFIG_FLOWNUM_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysConfigFlownum">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysConfigFlownumVo tSysConfigFlownum)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysConfigFlownum, TSysConfigFlownumVo.T_SYS_CONFIG_FLOWNUM_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysConfigFlownum.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysConfigFlownum_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tSysConfigFlownum_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysConfigFlownumVo tSysConfigFlownum_UpdateSet, TSysConfigFlownumVo tSysConfigFlownum_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysConfigFlownum_UpdateSet, TSysConfigFlownumVo.T_SYS_CONFIG_FLOWNUM_TABLE);
            strSQL += this.BuildWhereStatement(tSysConfigFlownum_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_SYS_CONFIG_FLOWNUM where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TSysConfigFlownumVo tSysConfigFlownum)
        {
            string strSQL = "delete from T_SYS_CONFIG_FLOWNUM ";
            strSQL += this.BuildWhereStatement(tSysConfigFlownum);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysConfigFlownum"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysConfigFlownumVo tSysConfigFlownum)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysConfigFlownum)
            {

                //ID
                if (!String.IsNullOrEmpty(tSysConfigFlownum.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysConfigFlownum.ID.ToString()));
                }
                //环节名称
                if (!String.IsNullOrEmpty(tSysConfigFlownum.FLOW_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FLOW_NAME = '{0}'", tSysConfigFlownum.FLOW_NAME.ToString()));
                }
                //第一编号
                if (!String.IsNullOrEmpty(tSysConfigFlownum.FIRST_FLOW_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FIRST_FLOW_CODE = '{0}'", tSysConfigFlownum.FIRST_FLOW_CODE.ToString()));
                }
                //第二编号
                if (!String.IsNullOrEmpty(tSysConfigFlownum.SECOND_FLOW_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SECOND_FLOW_CODE = '{0}'", tSysConfigFlownum.SECOND_FLOW_CODE.ToString()));
                }
                //第三编号
                if (!String.IsNullOrEmpty(tSysConfigFlownum.THIRD_FLOW_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND THIRD_FLOW_CODE = '{0}'", tSysConfigFlownum.THIRD_FLOW_CODE.ToString()));
                }
                //环节序号
                if (!String.IsNullOrEmpty(tSysConfigFlownum.FLOW_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FLOW_NUM = '{0}'", tSysConfigFlownum.FLOW_NUM.ToString()));
                }
                //是否并行
                if (!String.IsNullOrEmpty(tSysConfigFlownum.IS_COL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_COL = '{0}'", tSysConfigFlownum.IS_COL.ToString()));
                }
                //是否删除
                if (!String.IsNullOrEmpty(tSysConfigFlownum.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tSysConfigFlownum.IS_DEL.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tSysConfigFlownum.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysConfigFlownum.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tSysConfigFlownum.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysConfigFlownum.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tSysConfigFlownum.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysConfigFlownum.REMARK3.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
