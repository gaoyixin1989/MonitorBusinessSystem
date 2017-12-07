using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject;

namespace i3.DataAccess.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：监测任务预约点位表
    /// 创建日期：2012-11-29
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPlanPointAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_PLAN_POINT " + this.BuildWhereStatement(tMisContractPlanPoint);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPlanPointVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_PLAN_POINT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisContractPlanPointVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPlanPointVo Details(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_PLAN_POINT " + this.BuildWhereStatement(tMisContractPlanPoint));
            return SqlHelper.ExecuteObject(new TMisContractPlanPointVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractPlanPointVo> SelectByObject(TMisContractPlanPointVo tMisContractPlanPoint, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_CONTRACT_PLAN_POINT " + this.BuildWhereStatement(tMisContractPlanPoint));
            return SqlHelper.ExecuteObjectList(tMisContractPlanPoint, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractPlanPointVo tMisContractPlanPoint, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_CONTRACT_PLAN_POINT {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractPlanPoint));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPlanPoint"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            string strSQL = "select * from T_MIS_CONTRACT_PLAN_POINT " + this.BuildWhereStatement(tMisContractPlanPoint);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象</param>
        /// <returns></returns>
        public TMisContractPlanPointVo SelectByObject(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            string strSQL = "select * from T_MIS_CONTRACT_PLAN_POINT " + this.BuildWhereStatement(tMisContractPlanPoint);
            return SqlHelper.ExecuteObject(new TMisContractPlanPointVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractPlanPoint, TMisContractPlanPointVo.T_MIS_CONTRACT_PLAN_POINT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlanPoint">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPlanPoint, TMisContractPlanPointVo.T_MIS_CONTRACT_PLAN_POINT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractPlanPoint.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPlanPoint_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractPlanPoint_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPlanPointVo tMisContractPlanPoint_UpdateSet, TMisContractPlanPointVo tMisContractPlanPoint_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPlanPoint_UpdateSet, TMisContractPlanPointVo.T_MIS_CONTRACT_PLAN_POINT_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractPlanPoint_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_PLAN_POINT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            string strSQL = "delete from T_MIS_CONTRACT_PLAN_POINT ";  
            strSQL += this.BuildWhereStatement(tMisContractPlanPoint);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 插入监测任务预约点位表信息  胡方扬 2012-12-18
        /// </summary>
        /// <param name="strFreqId"></param>
        /// <param name="strPointId"></param>
        /// <param name="strPlanId"></param>
        /// <returns></returns>
        public bool SavePlanPoint(string[]  strFreqId,string[]  strPointId,string strPlanId)
        {
            ArrayList arrVo = new ArrayList();
            TMisContractPlanPointVo tMisContractPlanPoint=new TMisContractPlanPointVo();
            string strSQL = "";
            if(strFreqId.Length>0 && strFreqId.Length==strPointId.Length)
            {

                for(int i=0;i<strFreqId.Length;i++){
                    tMisContractPlanPoint.ID = GetSerialNumber("t_mis_contract_planpointId");
                    tMisContractPlanPoint.POINT_FREQ_ID=strFreqId[i].ToString();
                    tMisContractPlanPoint.CONTRACT_POINT_ID=strPointId[i].ToString();
                    tMisContractPlanPoint.PLAN_ID=strPlanId;
                    strSQL=String.Format("INSERT INTO T_MIS_CONTRACT_PLAN_POINT(ID,POINT_FREQ_ID,CONTRACT_POINT_ID,PLAN_ID) VALUES('{0}','{1}','{2}','{3}')",
                    tMisContractPlanPoint.ID,tMisContractPlanPoint.POINT_FREQ_ID,tMisContractPlanPoint.CONTRACT_POINT_ID,tMisContractPlanPoint.PLAN_ID);
                    arrVo.Add(strSQL);
                    strSQL = String.Format("UPDATE T_MIS_CONTRACT_POINT_FREQ SET IF_PLAN='1' WHERE ID='{0}'", tMisContractPlanPoint.POINT_FREQ_ID);
                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }
        /// <summary>
        /// 保存选择的监测计划点位
        /// 创建时间：2013-06-07
        /// 创建人：胡方扬
        /// 修改时间：
        /// 修改人：
        /// 修改内容：
        /// </summary>
        public bool SaveSelectPlanPoint(string[] strFreqId, string[] strPointId, string strPlanId,bool isDo)
        {
            ArrayList arrVo = new ArrayList();
            TMisContractPlanPointVo tMisContractPlanPoint = new TMisContractPlanPointVo();
            string strSQL = "", strPoint_Id = "";

            foreach (string strpoint in strPointId)
            {
                strPoint_Id += strpoint + ",";
            }
            strPoint_Id = strPoint_Id.Substring(0, strPoint_Id.Length - 1);
            strPoint_Id = strPoint_Id.Replace(",", "','");
            strSQL = String.Format(@"DELETE T_MIS_CONTRACT_PLAN_POINT WHERE PLAN_ID='{0}' AND CONTRACT_POINT_ID NOT IN ('{1}')", strPlanId, strPoint_Id);
            arrVo.Add(strSQL);
            if (isDo)
            {
                if (strFreqId.Length > 0 && strFreqId.Length == strPointId.Length)
                {

                    for (int i = 0; i < strFreqId.Length; i++)
                    {
                        strSQL = String.Format("UPDATE T_MIS_CONTRACT_POINT_FREQ SET IF_PLAN='1' WHERE ID='{0}'", strFreqId);
                        arrVo.Add(strSQL);
                    }
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 插入监测任务预约点位表信息  胡方扬 2013-03-27 统一版本
        /// </summary>
        /// <param name="strFreqId"></param>
        /// <param name="strPointId"></param>
        /// <param name="strPlanId"></param>
        /// <returns></returns>
        public bool SavePlanPoint(string strContractId,string strPlanId)
        {
            ArrayList arrVo = new ArrayList();
            string strSQL = String.Format(@" SELECT ID,CONTRACT_POINT_ID,FREQ,NUM,IF_PLAN,SAMPLE_FREQ FROM dbo.T_MIS_CONTRACT_POINT_FREQ WHERE CONTRACT_ID='{0}'", strContractId);
            DataTable dt=SqlHelper.ExecuteDataTable(strSQL);
                        TMisContractPlanPointVo tMisContractPlanPoint=new TMisContractPlanPointVo();
            if(dt.Rows.Count>0){
                for(int i=0;i<dt.Rows.Count;i++){
                    tMisContractPlanPoint.ID = GetSerialNumber("t_mis_contract_planpointId");
                    tMisContractPlanPoint.POINT_FREQ_ID = dt.Rows[i]["ID"].ToString();
                    tMisContractPlanPoint.CONTRACT_POINT_ID = dt.Rows[i]["CONTRACT_POINT_ID"].ToString();
                    tMisContractPlanPoint.PLAN_ID = strPlanId;
                    strSQL = String.Format("INSERT INTO T_MIS_CONTRACT_PLAN_POINT(ID,POINT_FREQ_ID,CONTRACT_POINT_ID,PLAN_ID) VALUES('{0}','{1}','{2}','{3}')",
tMisContractPlanPoint.ID, tMisContractPlanPoint.POINT_FREQ_ID, tMisContractPlanPoint.CONTRACT_POINT_ID, tMisContractPlanPoint.PLAN_ID);
                    arrVo.Add(strSQL);
                }

            }
            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 采样任务分配，取消采样任务后可重新预约接口（停产） Create By 胡方扬 2012-02-04
        /// </summary>
        /// <param name="strSubTaskId">采样子任务点位ID</param>
        /// <returns></returns>
        public bool DelPlanPointForSampleDistr(string strSubTaskArrId) {
            bool flag = false;
            int Record=0;
            DataTable dt=new DataTable();
            if (!String.IsNullOrEmpty(strSubTaskArrId))
            {
                //构造子任务点位ID SQL语法
                string strSubTaskId = strSubTaskArrId.Replace(",", ",'");
                //记录数执行查询
                string strSQLSearch = String.Format("SELECT A.PLAN_ID, A.POINT_FREQ_ID FROM T_MIS_CONTRACT_PLAN_POINT A LEFT JOIN " +
                                        " dbo.T_MIS_MONITOR_TASK B ON B.PLAN_ID=A.PLAN_ID" +
                                        " LEFT JOIN dbo.T_MIS_MONITOR_TASK_POINT C ON C.TASK_ID=B.ID WHERE C.ID IN ('{0}') AND C.CONTRACT_POINT_ID=A. CONTRACT_POINT_ID", strSubTaskId);
                dt = SqlHelper.ExecuteDataTable(strSQLSearch);
                Record = dt.Rows.Count;
                //如果记录数大于0则执行以下
                if (Record > 0)
                {
                    //删除委托书监测预约计划点位
                    string strSQL = String.Format("DELETE dbo.T_MIS_CONTRACT_PLAN_POINT " +
        " WHERE ID IN (SELECT A.ID FROM T_MIS_CONTRACT_PLAN_POINT A LEFT JOIN " +
        " dbo.T_MIS_MONITOR_TASK B ON B.PLAN_ID=A.PLAN_ID" +
        " LEFT JOIN dbo.T_MIS_MONITOR_TASK_POINT C ON C.TASK_ID=B.ID WHERE C.ID IN ('{0}') AND C.CONTRACT_POINT_ID=A.CONTRACT_POINT_ID) ", strSubTaskId);
                    int iFlag = SqlHelper.ExecuteNonQuery(strSQL);
                    flag = iFlag > 0 ? true : false;
                    //如果执行成功，则监测该计划是否还存在其他点位信息，如果不存在，则删除该监测任务计划，如果存在，则跳出
                    if (flag)
                    {
                        //查询指定监测计划的点位记录数
                        string strTempSQL = String.Format(" SELECT ID,POINT_FREQ_ID FROM T_MIS_CONTRACT_PLAN_POINT  WHERE PLAN_ID='{0}'", dt.Rows[0]["PLAN_ID"].ToString());
                        DataTable tempTable = SqlHelper.ExecuteDataTable(strTempSQL);
                        int RecordCount = tempTable.Rows.Count;
                        //如果不存在了预约点位数据，则将该预约删除并同时更新指定点位频次预约状态
                        if (RecordCount == 0)
                        {
                            //删除委托书监测任务预约计划 Start
                            strSQL = String.Format("DELETE T_MIS_CONTRACT_PLAN  WHERE ID='{0}'", dt.Rows[0]["PLAN_ID"].ToString());
                            flag = SqlHelper.ExecuteNonQuery(strSQL) > 0 ? true : false;
                            //End
                            if (flag)
                            {
                                //更新委托书监测任务点位频次预约状态 Start
                                strSQL = String.Format("UPDATE T_MIS_CONTRACT_POINT_FREQ SET IF_PLAN='0'  WHERE ID='{0}'", dt.Rows[0]["POINT_FREQ_ID"].ToString());
                                flag = SqlHelper.ExecuteNonQuery(strSQL) > 0 ? true : false;
                                //End
                            }
                            else
                            {
                                flag = false;
                            }
                        }
                    }
                }
            }
            return flag;
        }

        /// <summary>
        /// 获取指定监测计划的监测点位信息
        /// </summary>
        /// <param name="tMisContractPlanPoint"></param>
        /// <returns></returns>
        public DataTable GetPendingPlanPointDataTable(TMisContractPlanPointVo tMisContractPlanPoint) {
            string strSQL = String.Format("SELECT A.ID,A.PLAN_ID,A.CONTRACT_POINT_ID,A.POINT_FREQ_ID,B.POINT_NAME,B.MONITOR_ID,C.MONITOR_TYPE_NAME,B.SAMPLE_FREQ,B.SAMPLE_DAY FROM dbo.T_MIS_CONTRACT_PLAN_POINT A" +
                                                        " INNER JOIN dbo.T_MIS_CONTRACT_POINT B ON B.ID=A.CONTRACT_POINT_ID"+
                                                          " INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO C ON B.MONITOR_ID=C.ID" +
                                                        " WHERE A.PLAN_ID='{0}'", tMisContractPlanPoint.PLAN_ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        ///获取指定监测计划的监测类别信息
        /// </summary>
        /// <param name="tMisContractPlanPoint"></param>
        /// <returns></returns>
        public DataTable GetPendingPlanDistinctMonitorDataTable(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            string strSQL = String.Format("SELECT DISTINCT(B.MONITOR_ID)  FROM dbo.T_MIS_CONTRACT_PLAN_POINT A" +
                                                        " LEFT JOIN dbo.T_MIS_CONTRACT_POINT B ON B.ID=A.CONTRACT_POINT_ID" +
                                                          " LEFT JOIN dbo.T_BASE_MONITOR_TYPE_INFO C ON B.MONITOR_ID=C.ID" +
                                                        " WHERE A.PLAN_ID='{0}'", tMisContractPlanPoint.PLAN_ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 对象添加,胡方扬  2013-05-15
        /// </summary>
        /// <param name="tMisContractPlanPoint">对象</param>
        /// <returns>是否成功</returns>
        public bool CreateDefine(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            string[] strPointId = tMisContractPlanPoint.CONTRACT_POINT_ID.Split(',');
            ArrayList objVo = new ArrayList();
            foreach (string str in strPointId)
            {
                if (!String.IsNullOrEmpty(str))
                {
                    string strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_PLAN_POINT(ID,PLAN_ID,CONTRACT_POINT_ID) VALUES('{0}','{1}','{2}')", GetSerialNumber("t_mis_contract_planpointId"), tMisContractPlanPoint.PLAN_ID, str);
                    objVo.Add(strSQL);
                } 
            }
            return SqlHelper.ExecuteSQLByTransaction(objVo);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractPlanPoint"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractPlanPointVo tMisContractPlanPoint)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractPlanPoint)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisContractPlanPoint.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractPlanPoint.ID.ToString()));
                }
                //监测任务预约表ID
                if (!String.IsNullOrEmpty(tMisContractPlanPoint.PLAN_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PLAN_ID = '{0}'", tMisContractPlanPoint.PLAN_ID.ToString()));
                }
                //委托书监测点位ID
                if (!String.IsNullOrEmpty(tMisContractPlanPoint.CONTRACT_POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_POINT_ID = '{0}'", tMisContractPlanPoint.CONTRACT_POINT_ID.ToString()));
                }
                //委托书点位频次表ID
                if (!String.IsNullOrEmpty(tMisContractPlanPoint.POINT_FREQ_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_FREQ_ID = '{0}'", tMisContractPlanPoint.POINT_FREQ_ID.ToString()));
                }
                //REAMRK1
                if (!String.IsNullOrEmpty(tMisContractPlanPoint.REAMRK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAMRK1 = '{0}'", tMisContractPlanPoint.REAMRK1.ToString()));
                }
                //REAMRK2
                if (!String.IsNullOrEmpty(tMisContractPlanPoint.REAMRK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAMRK2 = '{0}'", tMisContractPlanPoint.REAMRK2.ToString()));
                }
                //REAMRK3
                if (!String.IsNullOrEmpty(tMisContractPlanPoint.REAMRK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAMRK3 = '{0}'", tMisContractPlanPoint.REAMRK3.ToString()));
                }
                //REAMRK4
                if (!String.IsNullOrEmpty(tMisContractPlanPoint.REAMRK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAMRK4 = '{0}'", tMisContractPlanPoint.REAMRK4.ToString()));
                }
                //REAMRK5
                if (!String.IsNullOrEmpty(tMisContractPlanPoint.REAMRK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REAMRK5 = '{0}'", tMisContractPlanPoint.REAMRK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
