using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using System.Data;

namespace i3.DataAccess.Channels.Mis.Monitor.Return
{
    /// <summary>
    /// 功能：监测分析各环节退回意见表
    /// 创建日期：2014-04-08
    /// 创建人：魏林
    /// </summary>
    public class TMisReturnInfoAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisReturnInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisReturnInfoVo tMisReturnInfo)
        {
            string strSQL = "select Count(*) from T_MIS_RETURN_INFO " + this.BuildWhereStatement(tMisReturnInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisReturnInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_RETURN_INFO  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisReturnInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisReturnInfo">对象条件</param>
        /// <returns>对象</returns>
        public TMisReturnInfoVo Details(TMisReturnInfoVo tMisReturnInfo)
        {
            string strSQL = String.Format("select * from  T_MIS_RETURN_INFO " + this.BuildWhereStatement(tMisReturnInfo));
            return SqlHelper.ExecuteObject(new TMisReturnInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisReturnInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisReturnInfoVo> SelectByObject(TMisReturnInfoVo tMisReturnInfo, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_RETURN_INFO " + this.BuildWhereStatement(tMisReturnInfo));
            return SqlHelper.ExecuteObjectList(tMisReturnInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisReturnInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisReturnInfoVo tMisReturnInfo, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_RETURN_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisReturnInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisReturnInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisReturnInfoVo tMisReturnInfo)
        {
            string strSQL = "select * from T_MIS_RETURN_INFO " + this.BuildWhereStatement(tMisReturnInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisReturnInfo">对象</param>
        /// <returns></returns>
        public TMisReturnInfoVo SelectByObject(TMisReturnInfoVo tMisReturnInfo)
        {
            string strSQL = "select * from T_MIS_RETURN_INFO " + this.BuildWhereStatement(tMisReturnInfo);
            return SqlHelper.ExecuteObject(new TMisReturnInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisReturnInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisReturnInfoVo tMisReturnInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisReturnInfo, TMisReturnInfoVo.T_MIS_RETURN_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisReturnInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisReturnInfoVo tMisReturnInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisReturnInfo, TMisReturnInfoVo.T_MIS_RETURN_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisReturnInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisReturnInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisReturnInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisReturnInfoVo tMisReturnInfo_UpdateSet, TMisReturnInfoVo tMisReturnInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisReturnInfo_UpdateSet, TMisReturnInfoVo.T_MIS_RETURN_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tMisReturnInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_RETURN_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisReturnInfoVo tMisReturnInfo)
        {
            string strSQL = "delete from T_MIS_RETURN_INFO ";
            strSQL += this.BuildWhereStatement(tMisReturnInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisReturnInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisReturnInfoVo tMisReturnInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisReturnInfo)
            {

                //
                if (!String.IsNullOrEmpty(tMisReturnInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisReturnInfo.ID.ToString()));
                }
                //任务ID
                if (!String.IsNullOrEmpty(tMisReturnInfo.TASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TASK_ID = '{0}'", tMisReturnInfo.TASK_ID.ToString()));
                }
                //子任务ID
                if (!String.IsNullOrEmpty(tMisReturnInfo.SUBTASK_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SUBTASK_ID = '{0}'", tMisReturnInfo.SUBTASK_ID.ToString()));
                }
                //项目结果ID
                if (!String.IsNullOrEmpty(tMisReturnInfo.RESULT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID = '{0}'", tMisReturnInfo.RESULT_ID.ToString()));
                }
                //当前环节号
                if (!String.IsNullOrEmpty(tMisReturnInfo.CURRENT_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CURRENT_STATUS = '{0}'", tMisReturnInfo.CURRENT_STATUS.ToString()));
                }
                //退回环节号
                if (!String.IsNullOrEmpty(tMisReturnInfo.BACKTO_STATUS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND BACKTO_STATUS = '{0}'", tMisReturnInfo.BACKTO_STATUS.ToString()));
                }
                //退回意见
                if (!String.IsNullOrEmpty(tMisReturnInfo.SUGGESTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SUGGESTION = '{0}'", tMisReturnInfo.SUGGESTION.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisReturnInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisReturnInfo.REMARK1.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisReturnInfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisReturnInfo.REMARK2.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisReturnInfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisReturnInfo.REMARK3.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisReturnInfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisReturnInfo.REMARK4.ToString()));
                }
                //
                if (!String.IsNullOrEmpty(tMisReturnInfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisReturnInfo.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
