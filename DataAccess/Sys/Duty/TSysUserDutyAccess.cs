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
    /// 功能：上岗资质管理
    /// 创建日期：2012-11-12
    /// 创建人：胡方扬
    /// </summary>
    public class TSysUserDutyAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysUserDuty">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysUserDutyVo tSysUserDuty)
        {
            string strSQL = "select Count(*) from T_SYS_USER_DUTY " + this.BuildWhereStatement(tSysUserDuty);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysUserDutyVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_SYS_USER_DUTY  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TSysUserDutyVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysUserDuty">对象条件</param>
        /// <returns>对象</returns>
        public TSysUserDutyVo Details(TSysUserDutyVo tSysUserDuty)
        {
            string strSQL = String.Format("select * from  T_SYS_USER_DUTY " + this.BuildWhereStatement(tSysUserDuty));
            return SqlHelper.ExecuteObject(new TSysUserDutyVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysUserDuty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysUserDutyVo> SelectByObject(TSysUserDutyVo tSysUserDuty, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_SYS_USER_DUTY " + this.BuildWhereStatement(tSysUserDuty));
            return SqlHelper.ExecuteObjectList(tSysUserDuty, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysUserDuty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysUserDutyVo tSysUserDuty, int iIndex, int iCount)
        {

            string strSQL = " select * from T_SYS_USER_DUTY {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tSysUserDuty));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysUserDuty"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysUserDutyVo tSysUserDuty)
        {
            string strSQL = "select * from T_SYS_USER_DUTY " + this.BuildWhereStatement(tSysUserDuty);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysUserDuty">对象</param>
        /// <returns></returns>
        public TSysUserDutyVo SelectByObject(TSysUserDutyVo tSysUserDuty)
        {
            string strSQL = "select * from T_SYS_USER_DUTY " + this.BuildWhereStatement(tSysUserDuty);
            return SqlHelper.ExecuteObject(new TSysUserDutyVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tSysUserDuty">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysUserDutyVo tSysUserDuty)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tSysUserDuty, TSysUserDutyVo.T_SYS_USER_DUTY_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserDuty">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserDutyVo tSysUserDuty)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysUserDuty, TSysUserDutyVo.T_SYS_USER_DUTY_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tSysUserDuty.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysUserDuty_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tSysUserDuty_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysUserDutyVo tSysUserDuty_UpdateSet, TSysUserDutyVo tSysUserDuty_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tSysUserDuty_UpdateSet, TSysUserDutyVo.T_SYS_USER_DUTY_TABLE);
            strSQL += this.BuildWhereStatement(tSysUserDuty_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_SYS_USER_DUTY where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TSysUserDutyVo tSysUserDuty)
        {
            string strSQL = "delete from T_SYS_USER_DUTY ";
            strSQL += this.BuildWhereStatement(tSysUserDuty);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }
        #region Create By Castle(胡方扬) 2012-11-15
        /// <summary>
        /// 获取已经设置了监测项目岗位用户岗位相关信息 Create by Castle(胡方扬) 2012-11-14
        /// </summary>
        /// <param name="MonitorId">监测类别ID</param>
        /// <param name="strUserId">用户ID</param>
        /// <returns></returns>
        public DataTable GetExistMonitor(string MonitorId, string strUserId, bool blDefaultAu, bool blDefaultEx)
        {

            string strSQL = "SELECT D.ID AS ITEM_ID,D.ITEM_NAME,B.DICT_CODE,A.* FROM dbo.T_SYS_USER_DUTY A" +
" INNER JOIN dbo.T_SYS_DUTY B ON A.DUTY_ID=B.ID AND B.MONITOR_TYPE_ID='" + MonitorId + "'" +
" INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO C ON B.MONITOR_TYPE_ID=C.ID" +
" INNER JOIN dbo.T_BASE_ITEM_INFO D ON D.ID=B.MONITOR_ITEM_ID" +
" WHERE 1=1";
            if (!String.IsNullOrEmpty(strUserId))
            {
                strSQL += "  AND A.USERID='" + strUserId + "'";
            }
            if (blDefaultAu)
            {
                strSQL += "  AND A.IF_DEFAULT='0'";
            }
            if (blDefaultEx)
            {
                strSQL += "  AND A.IF_DEFAULT_EX='0'";
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取分析岗位职责中 对监测类别的设置数据的加载获取
        /// </summary>
        /// <param name="MonitorId"></param>
        /// <param name="strUserId"></param>
        /// <param name="blDefaultAu"></param>
        /// <param name="blDefaultEx"></param>
        /// <returns></returns>
        public DataTable GetExistMonitorType(string MonitorId, string strUserId, bool blDefaultAu, bool blDefaultEx)
        {

            string strSQL = String.Format("SELECT C.ID AS ITEM_ID,C.MONITOR_TYPE_NAME AS ITEM_NAME,B.DICT_CODE,A.* FROM dbo.T_SYS_USER_DUTY A" +
 " INNER JOIN dbo.T_SYS_DUTY B ON A.DUTY_ID=B.ID AND B.MONITOR_TYPE_ID='{0}' AND B.MONITOR_ITEM_ID IS NULL" +
 " INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO C ON B.MONITOR_TYPE_ID=C.ID" +
" WHERE 1=1", MonitorId);
            if (!String.IsNullOrEmpty(strUserId))
            {
                strSQL += "  AND A.USERID='" + strUserId + "'";
            }
            if (blDefaultAu)
            {
                strSQL += "  AND A.IF_DEFAULT='0'";
            }
            if (blDefaultEx)
            {
                strSQL += "  AND A.IF_DEFAULT_EX='0'";
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 对象删除，删除移除的岗位职责用户关联的默认负责人或协同人的岗位职责 Create By Castle(胡方扬)  2012-11-15
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="MonitorId">监测类别ID</param>
        /// <param name="MonitorItemId">监测项目ID</param>
        /// <param name="strDutyType">岗位职责类型</param>
        /// <param name="DefaultAuValue">默认负责人</param>
        /// <param name="DefaultExValue">默认责任人</param>
        /// <returns>是否成功</returns>
        public bool UpdateDefaultSet(string strUserId, string MonitorId, string[] MonitorItemId, string strDutyType, string DefaultAuValue, string DefaultExValue, bool isMonitorType)
        {
            ArrayList arrVo = new ArrayList();
            foreach (string strItemId in MonitorItemId)
            {
                if (!String.IsNullOrEmpty(strItemId))
                {
                    string strSQL = "", SetCondition = "";

                    string strConditon = String.Format("SELECT A.ID FROM dbo.T_SYS_USER_DUTY A" +
                                                        " INNER JOIN dbo.T_SYS_DUTY B ON A.DUTY_ID=B.ID AND B.MONITOR_TYPE_ID='{0}'", MonitorId);
                    if (isMonitorType)
                    {
                        strConditon += " AND B.MONITOR_ITEM_ID IS NULL";
                    }
                    else
                    {
                        strConditon += String.Format("AND B.MONITOR_ITEM_ID='{0}'", strItemId);
                    }
                    strConditon += String.Format(" AND B.DICT_CODE='{0}'", strDutyType);

                    if (!String.IsNullOrEmpty(DefaultAuValue))
                    {
                        SetCondition = String.Format("  IF_DEFAULT={0}", DefaultAuValue);
                    }

                    if (!String.IsNullOrEmpty(DefaultExValue))
                    {
                        SetCondition = String.Format("  IF_DEFAULT_EX={0}", DefaultExValue);
                    }
                    strSQL = String.Format("UPDATE dbo.T_SYS_USER_DUTY SET {0} WHERE ID IN ({1}) AND USERID='{2}'", SetCondition, strConditon, strUserId);
                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);

        }

        /// <summary>
        /// 移除默认负责人和默认协同人 Create By Castle(胡方扬) 2012-11-15
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="MonitorId">监测类别ID</param>
        /// <param name="MonitorItemId">监测项目ID</param>
        /// <param name="strDutyType">岗位职责类型</param>
        /// <param name="MoveDefaultAu">移除默认负责人</param>
        /// <param name="MoveDefaultEx">移除默认协同人</param>
        /// <returns>返回值 true false</returns>
        public bool MoveDefaultSet(string strUserId, string MonitorId, string[] MonitorItemId, string strDutyType, bool MoveDefaultAu, bool MoveDefaultEx, bool isMonitorType)
        {
            ArrayList arrVo = new ArrayList();
            foreach (string strItemId in MonitorItemId)
            {
                if (!String.IsNullOrEmpty(strItemId))
                {
                    string strSQL = "", SetCondition = ""; ;
                    string strConditon = String.Format("SELECT A.ID FROM dbo.T_SYS_USER_DUTY A" +
                                                        " INNER JOIN dbo.T_SYS_DUTY B ON A.DUTY_ID=B.ID AND B.MONITOR_TYPE_ID='{0}'", MonitorId);

                    if (isMonitorType)
                    {
                        strConditon += " AND B.MONITOR_ITEM_ID IS NULL";
                    }
                    else
                    {
                        strConditon += String.Format("AND B.MONITOR_ITEM_ID='{0}'", strItemId);
                    }

                    strConditon += String.Format(" AND B.DICT_CODE='{0}'", strDutyType);
                    if (MoveDefaultAu)
                    {
                        SetCondition = " IF_DEFAULT=NULL";
                    }
                    if (MoveDefaultEx)
                    {
                        SetCondition = " IF_DEFAULT_EX=NULL";
                    }

                    strSQL = String.Format("UPDATE dbo.T_SYS_USER_DUTY SET {0} WHERE ID IN ({1}) AND USERID='{2}'", SetCondition, strConditon, strUserId);
                    arrVo.Add(strSQL);
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 移除默认负责人和默认协同人 Create By Castle(胡方扬) 2012-11-15
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="MonitorId">监测类别ID</param>
        /// <param name="MonitorItemId">监测项目ID</param>
        /// <param name="strDutyType">岗位职责类型</param>
        /// <param name="MoveDefaultAu">移除默认负责人</param>
        /// <param name="MoveDefaultEx">移除默认协同人</param>
        /// <returns>返回值 true false</returns>
        public bool MoveDefaultSet(string[] strUserId, string MonitorId, string strDutyType, bool MoveDefaultAu, bool MoveDefaultEx)
        {
            ArrayList arrVo = new ArrayList();
            foreach (string strUser in strUserId)
            {
                if (!String.IsNullOrEmpty(strUser))
                {
                    string strSQL = "";
                    string strConditon = String.Format("SELECT A.ID FROM dbo.T_SYS_USER_DUTY A" +
                                                        " INNER JOIN dbo.T_SYS_DUTY B ON A.DUTY_ID=B.ID AND B.MONITOR_TYPE_ID='{0}'" +

                                                        "  AND DICT_CODE='{1}'", MonitorId, strDutyType);
                    string SetCondition = "";
                    if (MoveDefaultAu)
                    {
                        SetCondition = " IF_DEFAULT=NULL";
                    }
                    if (MoveDefaultEx)
                    {
                        SetCondition = " IF_DEFAULT_EX=NULL";
                    }

                    strSQL = String.Format("UPDATE dbo.T_SYS_USER_DUTY SET {0} WHERE ID IN ({1}) AND USERID='{2}'", SetCondition, strConditon, strUser);
                    arrVo.Add(strSQL);
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 对象删除，删除移除的岗位职责用户关联的岗位职责  Create By Castle(胡方扬)  2012-11-14
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="MonitorId">监测类别ID</param>
        /// <param name="MonitorItemId">监测项目ID</param>
        /// <param name="strDutyType">岗位职责类型</param>
        /// <returns>是否成功</returns>
        public bool DeleteUserMonitorSet(string strUserId, string MonitorId, string[] MonitorItemId, string strDutyType, bool isMonitorType)
        {
            ArrayList arrVo = new ArrayList();
            foreach (string strItemId in MonitorItemId)
            {
                if (!String.IsNullOrEmpty(strItemId))
                {
                    string strConditon = String.Format("SELECT ID FROM T_SYS_DUTY WHERE MONITOR_TYPE_ID='{0}' AND DICT_CODE='{1}'", MonitorId, strDutyType);
                    if (isMonitorType)
                    {
                        strConditon += String.Format("  AND MONITOR_ITEM_ID IS NULL ");
                    }
                    else
                    {
                        strConditon += String.Format("  AND MONITOR_ITEM_ID='{0}'", strItemId);
                    }

                    string strSQL = String.Format("DELETE T_SYS_USER_DUTY WHERE DUTY_ID IN ({0}) AND USERID='{1}'", strConditon, strUserId);
                    arrVo.Add(strSQL);
                }
            }

            return ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 修改保存移除监测项目用户
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="MonitorId">监测项目ID</param>
        /// <param name="strDutyType">监测项目类别</param>
        /// <returns></returns>
        public bool DeleteUserMonitorSet(string[] strUserId, string MonitorId, string strDutyType)
        {
            ArrayList arrVo = new ArrayList();
            if (strUserId != null)
            {
                foreach (string strUser in strUserId)
                {
                    if (!String.IsNullOrEmpty(strUser))
                    {
                        string strConditon = String.Format("SELECT ID FROM T_SYS_DUTY WHERE MONITOR_TYPE_ID='{0}' AND DICT_CODE='{1}'", MonitorId, strDutyType);

                        string strSQL = String.Format("DELETE T_SYS_USER_DUTY WHERE DUTY_ID IN ({0}) AND USERID='{1}'", strConditon, strUser);
                        arrVo.Add(strSQL);
                    }
                }
            }
            else
            {
                string strConditon = String.Format("SELECT ID FROM T_SYS_DUTY WHERE MONITOR_TYPE_ID='{0}' AND DICT_CODE='{1}'", MonitorId, strDutyType);

                string strSQL = String.Format("DELETE T_SYS_USER_DUTY WHERE DUTY_ID IN ({0})", strConditon);
                arrVo.Add(strSQL);
            }

            return ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 获取采样用户和负责人协同人设置信息
        /// </summary>
        /// <param name="strMonitor">监测类别ID</param>
        /// <param name="strDutyType">岗位职责ID</param>
        /// <returns></returns>
        public DataTable GetSamplingSetUser(string strMonitor, string strDutyType)
        {
            string strSQL = "SELECT A.* FROM T_SYS_USER_DUTY A" +
                           " INNER JOIN T_SYS_DUTY B ON A.DUTY_ID=B.ID";
            if (!String.IsNullOrEmpty(strMonitor))
            {
                strSQL += "  AND B.MONITOR_TYPE_ID='" + strMonitor + "'";
            }
            if (!String.IsNullOrEmpty(strDutyType))
            {
                strSQL += "  AND B.DICT_CODE='" + strDutyType + "'";
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 插入采样岗位资质
        /// </summary>
        /// <param name="strUserId">用户ID</param>
        /// <param name="DutyId">岗位职责ID</param>
        /// <param name="isDefault">是否为默认负责人</param>
        /// <returns></returns>
        public bool InsertSamplingUser(string[] strUserId, string DutyId, bool isDefault)
        {
            ArrayList arrVo = new ArrayList();
            string defValue = "0";
            string strCondtion = "", strCondtionValue = "";
            if (isDefault)
            {
                strCondtion = String.Format(" IF_DEFAULT");
                strCondtionValue = String.Format(" '{0}'", defValue);
            }
            else
            {
                strCondtion = String.Format(" IF_DEFAULT_EX");
                strCondtionValue = String.Format(" '{0}'", defValue);
            }
            foreach (string struser in strUserId)
            {
                if (!String.IsNullOrEmpty(struser))
                {
                    if (SetDefaultUser(DutyId, isDefault))
                    {
                        string strSQL = "";
                        DataTable dt = new DataTable();
                        //根据用户和岗位职责编号进行查找是否存在如果不存在 则新增
                        dt = SqlHelper.ExecuteDataTable(String.Format("SELECT ID FROM T_SYS_USER_DUTY WHERE USERID='{0}' AND DUTY_ID='{1}'", struser, DutyId));
                        int count = dt.Rows.Count;

                        if (count == 0)
                        {
                            string SearMumId = GetSerialNumber("user_duty_infor");
                            strSQL = String.Format("INSERT INTO T_SYS_USER_DUTY(ID,USERID,DUTY_ID,{0}) VALUES('{1}','{2}','{3}',{4})", strCondtion, SearMumId, struser, DutyId, strCondtionValue);
                            arrVo.Add(strSQL);
                        }
                        else
                        {
                            strSQL = String.Format("UPDATE T_SYS_USER_DUTY SET {0}={1} WHERE ID='{2}'", strCondtion, strCondtionValue, dt.Rows[0]["ID"].ToString());
                            arrVo.Add(strSQL);
                        }
                    }
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }


        /// <summary>
        /// 插入当前选择监测类别选择的用户
        /// </summary>
        /// <param name="strUserId"></param>
        /// <param name="DutyId"></param>
        /// <returns></returns>
        public bool InsertSelectedUser(string[] strUserId, string DutyId)
        {
            ArrayList arrVo = new ArrayList();
            foreach (string struser in strUserId)
            {
                if (!String.IsNullOrEmpty(struser))
                {
                    string strSQL = "";
                    DataTable dt = new DataTable();
                    //根据用户和岗位职责编号进行查找是否存在如果不存在 则新增
                    dt = SqlHelper.ExecuteDataTable(String.Format("SELECT ID FROM T_SYS_USER_DUTY WHERE USERID='{0}' AND DUTY_ID='{1}'", struser, DutyId));
                    int count = dt.Rows.Count;

                    if (count == 0)
                    {
                        string SearMumId = GetSerialNumber("user_duty_infor");
                        strSQL = String.Format("INSERT INTO T_SYS_USER_DUTY(ID,USERID,DUTY_ID) VALUES('{0}','{1}','{2}')", SearMumId, struser, DutyId);
                        arrVo.Add(strSQL);
                    }
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 对设置了默认负责人的监测项目，在重置默认负责人时，对默认负责人进行重置
        /// </summary>
        /// <param name="DutyId">岗位职责ID</param>
        /// <param name="isDefault">是否为默认负责人</param>
        /// <returns></returns>
        public bool SetDefaultUser(string DutyId, bool isDefault)
        {
            string strSQL = "";
            DataTable dt = new DataTable();
            ArrayList arrVo = new ArrayList();
            //如果已设置了默认负责人，则清空已设置的
            if (isDefault)
            {
                dt = SqlHelper.ExecuteDataTable(String.Format("SELECT ID FROM T_SYS_USER_DUTY WHERE DUTY_ID='{0}'", DutyId));
                int counte = dt.Rows.Count;
                if (counte > 0)
                {
                    foreach (DataRow drr in dt.Rows)
                    {
                        strSQL = String.Format("UPDATE T_SYS_USER_DUTY SET IF_DEFAULT=NULL WHERE ID IN ({0})", drr["ID"].ToString());

                        arrVo.Add(strSQL);
                    }
                }
                return SqlHelper.ExecuteSQLByTransaction(arrVo);
            }
            else
            {
                return true;
            }

        }

        /// <summary>
        /// 获取用户监测项目设置数据
        /// 修改时间：2013-5-12
        /// 修改人：邵世卓
        /// 修改目的：过滤无用（已删、停用、隐藏）的用户信息
        /// </summary>
        /// <param name="strDutyType">岗位类别</param>
        /// <param name="strMonitorId">监测类别ID</param>
        /// <returns></returns>
        public DataTable SelectUserDuty(string strDutyType, string strMonitorId)
        {
            string strSQL = String.Format(@"SELECT * FROM T_SYS_USER_DUTY A INNER JOIN T_SYS_DUTY B ON B.ID=A.DUTY_ID AND B.MONITOR_TYPE_ID='{0}' AND B.DICT_CODE='{1}'
                                            INNER JOIN T_SYS_USER u ON u.IS_DEL='0' AND u.IS_USE='1' AND u.IS_HIDE='0' AND A.USERID=u.ID"
                , strMonitorId, strDutyType);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tSysUserDuty"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TSysUserDutyVo tSysUserDuty)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tSysUserDuty)
            {

                //ID
                if (!String.IsNullOrEmpty(tSysUserDuty.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tSysUserDuty.ID.ToString()));
                }
                //用户表ID
                if (!String.IsNullOrEmpty(tSysUserDuty.USERID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND USERID = '{0}'", tSysUserDuty.USERID.ToString()));
                }
                //已关联岗位职责ID
                if (!String.IsNullOrEmpty(tSysUserDuty.DUTY_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DUTY_ID = '{0}'", tSysUserDuty.DUTY_ID.ToString()));
                }
                //是否默认负责人
                if (!String.IsNullOrEmpty(tSysUserDuty.IF_DEFAULT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IF_DEFAULT = '{0}'", tSysUserDuty.IF_DEFAULT.ToString()));
                }
                //是否默认协同人
                if (!String.IsNullOrEmpty(tSysUserDuty.IF_DEFAULT_EX.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IF_DEFAULT_EX = '{0}'", tSysUserDuty.IF_DEFAULT_EX.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tSysUserDuty.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tSysUserDuty.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tSysUserDuty.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tSysUserDuty.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tSysUserDuty.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tSysUserDuty.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tSysUserDuty.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tSysUserDuty.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tSysUserDuty.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tSysUserDuty.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
