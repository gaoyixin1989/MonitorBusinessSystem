using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using System.Data;

namespace i3.DataAccess.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：数据补录
    /// 创建日期：2014-05-27
    /// 创建人：黄进军
    /// </summary>
    public class TMisResultModifyInfoAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisResultModifyInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            string strSQL = "select Count(*) from T_MIS_RESULT_MODIFY_INFO " + this.BuildWhereStatement(tMisResultModifyInfo);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisResultModifyInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_RESULT_MODIFY_INFO  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisResultModifyInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisResultModifyInfo">对象条件</param>
        /// <returns>对象</returns>
        public TMisResultModifyInfoVo Details(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            string strSQL = String.Format("select * from  T_MIS_RESULT_MODIFY_INFO " + this.BuildWhereStatement(tMisResultModifyInfo));
            return SqlHelper.ExecuteObject(new TMisResultModifyInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisResultModifyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisResultModifyInfoVo> SelectByObject(TMisResultModifyInfoVo tMisResultModifyInfo, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_RESULT_MODIFY_INFO " + this.BuildWhereStatement(tMisResultModifyInfo));
            return SqlHelper.ExecuteObjectList(tMisResultModifyInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisResultModifyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisResultModifyInfoVo tMisResultModifyInfo, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_RESULT_MODIFY_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisResultModifyInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisResultModifyInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            string strSQL = "select * from T_MIS_RESULT_MODIFY_INFO " + this.BuildWhereStatement(tMisResultModifyInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisResultModifyInfo">对象</param>
        /// <returns></returns>
        public TMisResultModifyInfoVo SelectByObject(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            string strSQL = "select * from T_MIS_RESULT_MODIFY_INFO " + this.BuildWhereStatement(tMisResultModifyInfo);
            return SqlHelper.ExecuteObject(new TMisResultModifyInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisResultModifyInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisResultModifyInfo, TMisResultModifyInfoVo.T_MIS_RESULT_MODIFY_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisResultModifyInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisResultModifyInfo, TMisResultModifyInfoVo.T_MIS_RESULT_MODIFY_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisResultModifyInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisResultModifyInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisResultModifyInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisResultModifyInfoVo tMisResultModifyInfo_UpdateSet, TMisResultModifyInfoVo tMisResultModifyInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisResultModifyInfo_UpdateSet, TMisResultModifyInfoVo.T_MIS_RESULT_MODIFY_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tMisResultModifyInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_RESULT_MODIFY_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            string strSQL = "delete from T_MIS_RESULT_MODIFY_INFO ";
            strSQL += this.BuildWhereStatement(tMisResultModifyInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisResultModifyInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisResultModifyInfo)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisResultModifyInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisResultModifyInfo.ID.ToString()));
                }
                //监测结果ID
                if (!String.IsNullOrEmpty(tMisResultModifyInfo.RESULT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND RESULT_ID = '{0}'", tMisResultModifyInfo.RESULT_ID.ToString()));
                }
                //修改人
                if (!String.IsNullOrEmpty(tMisResultModifyInfo.MODIFY_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MODIFY_USER = '{0}'", tMisResultModifyInfo.MODIFY_USER.ToString()));
                }
                //修改时间
                if (!String.IsNullOrEmpty(tMisResultModifyInfo.MODIFY_TIME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MODIFY_TIME = '{0}'", tMisResultModifyInfo.MODIFY_TIME.ToString()));
                }
                //批准人
                if (!String.IsNullOrEmpty(tMisResultModifyInfo.CHECK_USER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CHECK_USER = '{0}'", tMisResultModifyInfo.CHECK_USER.ToString()));
                }
                //修改原因
                if (!String.IsNullOrEmpty(tMisResultModifyInfo.MODIFY_SUGGESTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MODIFY_SUGGESTION = '{0}'", tMisResultModifyInfo.MODIFY_SUGGESTION.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisResultModifyInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisResultModifyInfo.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisResultModifyInfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisResultModifyInfo.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisResultModifyInfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisResultModifyInfo.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisResultModifyInfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisResultModifyInfo.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisResultModifyInfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisResultModifyInfo.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }

}
