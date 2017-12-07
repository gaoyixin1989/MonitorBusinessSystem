using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Sys.Duty;
using i3.ValueObject;

namespace i3.DataAccess.Sys.Duty
{
    /// <summary>
    /// 功能：岗位职责
    /// 创建日期：2012-11-12
    /// 创建人：胡方扬
    /// </summary>
    public class TSysDutyAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysDuty">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysDutyVo tSysDuty)
        {
            string strSQL = "select Count(*) from T_SYS_DUTY " + this.BuildWhereStatement(tSysDuty);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysDutyVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_DUTY  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TSysDutyVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysDuty">对象条件</param>
        /// <returns>对象</returns>
        public TSysDutyVo Details(TSysDutyVo tSysDuty)
        {
            string strSQL = String.Format("select * from  T_SYS_DUTY " + this.BuildWhereStatement(tSysDuty));
            return SqlHelper.ExecuteObject(new TSysDutyVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysDuty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysDutyVo> SelectByObject(TSysDutyVo tSysDuty, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_SYS_DUTY " + this.BuildWhereStatement(tSysDuty));
            return SqlHelper.ExecuteObjectList(tSysDuty, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysDuty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysDutyVo tSysDuty, int iIndex, int iCount)
        {

            string strSQL = " select * from T_SYS_DUTY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysDuty));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysDutyVo tSysDuty)
        {
            string strSQL = "select * from T_SYS_DUTY " + this.BuildWhereStatement(tSysDuty);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysDuty">对象</param>
        /// <returns></returns>
        public TSysDutyVo SelectByObject(TSysDutyVo tSysDuty)
        {
            string strSQL = "select * from T_SYS_DUTY " + this.BuildWhereStatement(tSysDuty);
            return SqlHelper.ExecuteObject(new TSysDutyVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysDuty">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysDutyVo tSysDuty)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysDuty, TSysDutyVo.T_SYS_DUTY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysDuty">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysDutyVo tSysDuty)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysDuty, TSysDutyVo.T_SYS_DUTY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysDuty.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysDuty_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tSysDuty_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysDutyVo tSysDuty_UpdateSet, TSysDutyVo tSysDuty_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysDuty_UpdateSet, TSysDutyVo.T_SYS_DUTY_TABLE);
            strSQL += this.BuildWhereStatement(tSysDuty_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_SYS_DUTY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TSysDutyVo tSysDuty)
        {
            string strSQL = "delete from T_SYS_DUTY ";
            strSQL += this.BuildWhereStatement(tSysDuty);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 获取指定监测类型的岗位职责人员
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public DataTable SelectTableDutyUser(TSysDutyVo tSysDuty)
        {
            string strSQL = String.Format("SELECT A.*,B.MONITOR_TYPE_ID,C.REAL_NAME FROM T_SYS_USER_DUTY A " +
                                                            " LEFT JOIN T_SYS_DUTY B ON A.DUTY_ID=B.ID " +
                                                            " INNER JOIN T_SYS_USER C ON C.ID=A.USERID  AND C.IS_DEL='0' AND IS_USE='1' AND IS_HIDE='0' " +
                                                            " WHERE B.MONITOR_ITEM_ID IS NULL ");
            if (!String.IsNullOrEmpty(tSysDuty.MONITOR_TYPE_ID)) {
                strSQL += String.Format(" AND B.MONITOR_TYPE_ID='{0}'", tSysDuty.MONITOR_TYPE_ID);
            }
            if (!String.IsNullOrEmpty(tSysDuty.DICT_CODE)) {
                strSQL += String.Format(" AND B.DICT_CODE='{0}' ", tSysDuty.DICT_CODE);
            }

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取已设置采样任务的人员列表
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public DataTable SelectByUnionTable(TSysDutyVo tSysDuty, int iIndex, int iCount)
        {
            string strSQL = String.Format("SELECT A.ID AS USERDUTYID,A.USERID AS ID,A.DUTY_ID,A.IF_DEFAULT,A.IF_DEFAULT_EX,C.REAL_NAME FROM T_SYS_USER_DUTY A" +
                                                            " INNER JOIN t_sys_user C on C.id=a.userid and c.is_del='0' and is_hide='0' and is_use='1'" +
                                                               " INNER JOIN T_SYS_DUTY B ON A.DUTY_ID=B.ID  AND B.MONITOR_TYPE_ID='{0}'  AND B.DICT_CODE='{1}' ", tSysDuty.MONITOR_TYPE_ID, tSysDuty.DICT_CODE);
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 功能描述：获取所有已设置采样任务的人员列表
        /// 创建时间：2013-2-28
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public DataTable SelectByUnionTableForWhere(TSysDutyVo tSysDuty, int iIndex, int iCount)
        {
            string strSQL = String.Format("SELECT A.ID AS USERDUTYID,A.USERID AS ID,A.DUTY_ID,A.IF_DEFAULT,A.IF_DEFAULT_EX,C.REAL_NAME FROM T_SYS_USER_DUTY A" +
                                                            " INNER JOIN t_sys_user C on C.id=a.userid and c.is_del='0' and is_hide='0' and is_use='1'" +
                                                               " INNER JOIN (select * from T_SYS_DUTY {0}) B ON A.DUTY_ID=B.ID", BuildWhereStatement(tSysDuty));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取已设置采样任务的人员列表总数
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public int GetSelectByUnionResultCount(TSysDutyVo tSysDuty)
        {
            string strSQL = String.Format("SELECT A.ID AS USERDUTYID,A.USERID AS ID,A.DUTY_ID,A.IF_DEFAULT,A.IF_DEFAULT_EX,C.REAL_NAME FROM T_SYS_USER_DUTY A" +
                                                            " INNER JOIN t_sys_user C on C.id=a.userid and c.is_del='0' and is_hide='0' and is_use='1'" +
                                                               " INNER JOIN T_SYS_DUTY B ON A.DUTY_ID=B.ID  AND B.MONITOR_TYPE_ID='{0}'  AND B.DICT_CODE='{1}' ", tSysDuty.MONITOR_TYPE_ID, tSysDuty.DICT_CODE);
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }


        /// <summary>
        /// 创建原因：获取指定监测类别  指定岗位职责的项目人员
        /// 创建人：胡方扬
        /// 创建日期：20130-07-18
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public DataTable GetDutyUser(TSysDutyVo tSysDuty) {
                        string strSQL = @" SELECT A.* FROM dbo.T_SYS_USER_DUTY A 
LEFT JOIN dbo.T_SYS_DUTY B ON A.DUTY_ID=B.ID
WHERE 1=1 ";
                        if (!String.IsNullOrEmpty(tSysDuty.MONITOR_TYPE_ID)) {
                            strSQL += String.Format("  AND B.MONITOR_TYPE_ID='{0}'", tSysDuty.MONITOR_TYPE_ID);
                        }
                        if (!String.IsNullOrEmpty(tSysDuty.DICT_CODE))
                        {
                            strSQL += String.Format(" AND B.DICT_CODE='{0}'", tSysDuty.DICT_CODE);
                        }
                        if (!String.IsNullOrEmpty(tSysDuty.MONITOR_ITEM_ID))
                        {
                            strSQL += String.Format(" AND B.MONITOR_ITEM_ID='{0}'", tSysDuty.MONITOR_ITEM_ID);
                        }
            return SqlHelper.ExecuteDataTable(strSQL);

        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysDutyVo tSysDuty)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysDuty)
            {

                //ID
                if (!String.IsNullOrEmpty(tSysDuty.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysDuty.ID.ToString()));
                }
                //监测类别ID
                if (!String.IsNullOrEmpty(tSysDuty.MONITOR_TYPE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_TYPE_ID = '{0}'", tSysDuty.MONITOR_TYPE_ID.ToString()));
                }
                //监测项目ID
                if (!String.IsNullOrEmpty(tSysDuty.MONITOR_ITEM_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_ITEM_ID = '{0}'", tSysDuty.MONITOR_ITEM_ID.ToString()));
                }
                //岗位职责编码(字典项目获取)
                if (!String.IsNullOrEmpty(tSysDuty.DICT_CODE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DICT_CODE = '{0}'", tSysDuty.DICT_CODE.ToString()));
                }
                //所有者
                if (!String.IsNullOrEmpty(tSysDuty.OWNER.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND OWNER = '{0}'", tSysDuty.OWNER.ToString()));
                }
                //备注
                if (!String.IsNullOrEmpty(tSysDuty.REMARK.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK = '{0}'", tSysDuty.REMARK.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tSysDuty.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysDuty.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tSysDuty.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysDuty.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tSysDuty.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysDuty.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tSysDuty.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tSysDuty.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tSysDuty.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tSysDuty.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion

    }
}
