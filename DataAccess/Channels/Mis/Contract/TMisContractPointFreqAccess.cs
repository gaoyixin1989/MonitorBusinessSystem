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
    /// 功能：委托书点位频次表
    /// 创建日期：2012-11-29
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPointFreqAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPointFreq">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPointFreqVo tMisContractPointFreq)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_POINT_FREQ " + this.BuildWhereStatement(tMisContractPointFreq);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPointFreqVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_POINT_FREQ  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisContractPointFreqVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPointFreq">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPointFreqVo Details(TMisContractPointFreqVo tMisContractPointFreq)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_POINT_FREQ " + this.BuildWhereStatement(tMisContractPointFreq));
            return SqlHelper.ExecuteObject(new TMisContractPointFreqVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractPointFreq">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractPointFreqVo> SelectByObject(TMisContractPointFreqVo tMisContractPointFreq, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_CONTRACT_POINT_FREQ " + this.BuildWhereStatement(tMisContractPointFreq));
            return SqlHelper.ExecuteObjectList(tMisContractPointFreq, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractPointFreq">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractPointFreqVo tMisContractPointFreq, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_CONTRACT_POINT_FREQ {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractPointFreq));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPointFreqVo tMisContractPointFreq)
        {
            string strSQL = "select * from T_MIS_CONTRACT_POINT_FREQ " + this.BuildWhereStatement(tMisContractPointFreq);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPointFreq">对象</param>
        /// <returns></returns>
        public TMisContractPointFreqVo SelectByObject(TMisContractPointFreqVo tMisContractPointFreq)
        {
            string strSQL = "select * from T_MIS_CONTRACT_POINT_FREQ " + this.BuildWhereStatement(tMisContractPointFreq);
            return SqlHelper.ExecuteObject(new TMisContractPointFreqVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractPointFreq">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPointFreqVo tMisContractPointFreq)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractPointFreq, TMisContractPointFreqVo.T_MIS_CONTRACT_POINT_FREQ_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPointFreq">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointFreqVo tMisContractPointFreq)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPointFreq, TMisContractPointFreqVo.T_MIS_CONTRACT_POINT_FREQ_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractPointFreq.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPointFreq_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractPointFreq_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointFreqVo tMisContractPointFreq_UpdateSet, TMisContractPointFreqVo tMisContractPointFreq_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPointFreq_UpdateSet, TMisContractPointFreqVo.T_MIS_CONTRACT_POINT_FREQ_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractPointFreq_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_POINT_FREQ where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractPointFreqVo tMisContractPointFreq)
        {
            string strSQL = "delete from T_MIS_CONTRACT_POINT_FREQ ";
            strSQL += this.BuildWhereStatement(tMisContractPointFreq);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 插入委托书监测点位频次信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtDict"></param>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public bool CreatePlanPoint(DataTable dt, DataTable dtDict, string task_id)
        {
            TMisContractPointFreqVo tMisContractPointFreq = new TMisContractPointFreqVo();
            int IntFreq = 0;
            ArrayList arrVo = new ArrayList();
            foreach (DataRow drr in dt.Rows)
            {
                if (!String.IsNullOrEmpty(drr["FREQ"].ToString()))
                {
                    IntFreq = Convert.ToInt32(drr["FREQ"].ToString().ToString());
                }
                else {
                    IntFreq = 1;
                }
                if (IntFreq > 0)
                {
                    for (int i = 0; i < IntFreq; i++)
                    {
                        tMisContractPointFreq.ID = GetSerialNumber("t_mis_contractplanfreqID");
                        tMisContractPointFreq.CONTRACT_ID = task_id;
                        tMisContractPointFreq.FREQ = drr["FREQ"].ToString();
                        //tMisContractPointFreq.FREQ = (i + 1).ToString();
                        tMisContractPointFreq.CONTRACT_POINT_ID = drr["ID"].ToString();
                        tMisContractPointFreq.NUM = (i + 1).ToString();
                        tMisContractPointFreq.IF_PLAN = "0";
                        tMisContractPointFreq.SAMPLENUM = drr["SAMPLENUM"].ToString();
                        string strSQL = String.Format("INSERT INTO  T_MIS_CONTRACT_POINT_FREQ(ID,CONTRACT_ID,CONTRACT_POINT_ID,FREQ,NUM,IF_PLAN,SAMPLENUM) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", tMisContractPointFreq.ID, tMisContractPointFreq.CONTRACT_ID, tMisContractPointFreq.CONTRACT_POINT_ID, tMisContractPointFreq.FREQ, tMisContractPointFreq.NUM, tMisContractPointFreq.IF_PLAN,tMisContractPointFreq.SAMPLENUM);
                        arrVo.Add(strSQL);
                    }
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 插入委托书监测点位频次,采样频次信息 胡方扬 2013-03-27 统一版本使用
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtDict"></param>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public bool CreatePlanPoint(DataTable dt,  string task_id)
        {
            TMisContractPointFreqVo tMisContractPointFreq = new TMisContractPointFreqVo();

            ArrayList arrVo = new ArrayList();
            foreach (DataRow drr in dt.Rows)
            {
                tMisContractPointFreq.ID = GetSerialNumber("t_mis_contractplanfreqID");
                tMisContractPointFreq.CONTRACT_ID = task_id;
                tMisContractPointFreq.FREQ = drr["FREQ"].ToString();
                tMisContractPointFreq.SAMPLE_FREQ = drr["SAMPLE_FREQ"].ToString();
                tMisContractPointFreq.CONTRACT_POINT_ID = drr["ID"].ToString();
                tMisContractPointFreq.NUM ="1";
                tMisContractPointFreq.IF_PLAN = "0";
                string strSQL = String.Format("INSERT INTO  T_MIS_CONTRACT_POINT_FREQ(ID,CONTRACT_ID,CONTRACT_POINT_ID,FREQ,NUM,IF_PLAN,SAMPLE_FREQ) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", tMisContractPointFreq.ID, tMisContractPointFreq.CONTRACT_ID, tMisContractPointFreq.CONTRACT_POINT_ID, tMisContractPointFreq.FREQ, tMisContractPointFreq.NUM, tMisContractPointFreq.IF_PLAN, tMisContractPointFreq.SAMPLE_FREQ);
                arrVo.Add(strSQL);

            }
            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 插入委托书环境质量类监测点位频次,采样频次信息 胡方扬 2013-04-02 统一版本使用 --质量类监测点位频次
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtDict"></param>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public bool CreatePlanPointForEvn(DataTable dt, string task_id)
        {
            TMisContractPointFreqVo tMisContractPointFreq = new TMisContractPointFreqVo();

            ArrayList arrVo = new ArrayList();
            foreach (DataRow drr in dt.Rows)
            {
                tMisContractPointFreq.ID = GetSerialNumber("t_mis_contractplanfreqID");
                tMisContractPointFreq.CONTRACT_ID = task_id;
                tMisContractPointFreq.FREQ ="1";
                tMisContractPointFreq.SAMPLE_FREQ ="1";
                tMisContractPointFreq.CONTRACT_POINT_ID = drr["ID"].ToString();
                tMisContractPointFreq.NUM = "1";
                //tMisContractPointFreq.IF_PLAN = "0";
                string strSQL = String.Format("INSERT INTO  T_MIS_CONTRACT_POINT_FREQ(ID,CONTRACT_ID,CONTRACT_POINT_ID,FREQ,NUM,IF_PLAN,SAMPLE_FREQ) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}')", tMisContractPointFreq.ID, tMisContractPointFreq.CONTRACT_ID, tMisContractPointFreq.CONTRACT_POINT_ID, tMisContractPointFreq.FREQ, tMisContractPointFreq.NUM, tMisContractPointFreq.IF_PLAN, tMisContractPointFreq.SAMPLE_FREQ);
                arrVo.Add(strSQL);

            }
            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 快捷方式生成委托书信息 插入委托书监测点位频次信息
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtDict"></param>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public bool CreatePlanPointQuck(DataTable dt, string task_id)
        {
            TMisContractPointFreqVo tMisContractPointFreq = new TMisContractPointFreqVo();
            ArrayList arrVo = new ArrayList();


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        tMisContractPointFreq.ID = GetSerialNumber("t_mis_contractplanfreqID");
                        tMisContractPointFreq.CONTRACT_ID = task_id;
                        tMisContractPointFreq.FREQ = "1";
                        tMisContractPointFreq.CONTRACT_POINT_ID = dt.Rows[i]["ID"].ToString();
                        tMisContractPointFreq.NUM = "1";
                        tMisContractPointFreq.IF_PLAN = "0";
                        string strSQL = String.Format("INSERT INTO  T_MIS_CONTRACT_POINT_FREQ(ID,CONTRACT_ID,CONTRACT_POINT_ID,FREQ,NUM,IF_PLAN) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", tMisContractPointFreq.ID, tMisContractPointFreq.CONTRACT_ID, tMisContractPointFreq.CONTRACT_POINT_ID, tMisContractPointFreq.FREQ, tMisContractPointFreq.NUM, tMisContractPointFreq.IF_PLAN);
                        arrVo.Add(strSQL);
                    }
            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 创建人：插入指令性任务监测点位频次信息，并更新对应监测计划对应点位的POINT_FREQ_ID
        /// 创建人：胡方扬
        /// 创建日期：2013-07-04
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="dtDict"></param>
        /// <param name="task_id"></param>
        /// <returns></returns>
        public bool CreateUnContractPlanPoint(DataTable dt, string strPlanId)
        {
            TMisContractPointFreqVo tMisContractPointFreq = new TMisContractPointFreqVo();
            ArrayList arrVo = new ArrayList();


            for (int i = 0; i < dt.Rows.Count; i++)
            {
                tMisContractPointFreq.ID = GetSerialNumber("t_mis_contractplanfreqID");
                tMisContractPointFreq.FREQ = "1";
                tMisContractPointFreq.CONTRACT_POINT_ID = dt.Rows[i]["ID"].ToString();
                tMisContractPointFreq.NUM = "1";
                tMisContractPointFreq.IF_PLAN = "0";
                string strSQL = String.Format("INSERT INTO  T_MIS_CONTRACT_POINT_FREQ(ID,CONTRACT_ID,CONTRACT_POINT_ID,FREQ,NUM,IF_PLAN) VALUES('{0}','{1}','{2}','{3}','{4}','{5}')", tMisContractPointFreq.ID, tMisContractPointFreq.CONTRACT_ID, tMisContractPointFreq.CONTRACT_POINT_ID, tMisContractPointFreq.FREQ, tMisContractPointFreq.NUM, tMisContractPointFreq.IF_PLAN);
                arrVo.Add(strSQL);
                strSQL = String.Format(" UPDATE T_MIS_CONTRACT_PLAN_POINT SET POINT_FREQ_ID='{0}' WHERE PLAN_ID='{1}' AND CONTRACT_POINT_ID='{2}'", tMisContractPointFreq.ID, strPlanId, tMisContractPointFreq.CONTRACT_POINT_ID);
                arrVo.Add(strSQL);
            }
            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }
        public DataTable SelectContractPlanInfor(TMisContractPointFreqVo tMisContractPointFreq, TMisContractVo tMisContract, TMisContractCompanyVo tMisContractCompany, int iIndex, int iCount)
        {
            string strSQL = String.Format("SELECT A.*,E.COMPANY_NAME,F.DICT_TEXT AS AREA FROM dbo.T_MIS_CONTRACT A " +
                                    " INNER JOIN " +
                                    " (SELECT * FROM (SELECT DISTINCT (B.CONTRACT_ID) FROM dbo.T_MIS_CONTRACT_POINT_FREQ B WHERE IF_PLAN='{0}')T) D" +
                                    " ON D.CONTRACT_ID=A.ID"+
                                    " LEFT JOIN T_MIS_CONTRACT_COMPANY E ON E.ID=A.TESTED_COMPANY_ID" +
                                     " LEFT JOIN dbo.T_SYS_DICT F ON F.DICT_TYPE='administrative_area' AND F.DICT_CODE=E.AREA WHERE 1=1 ", tMisContractPointFreq.IF_PLAN);
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format("  AND A.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format("  AND A.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.TEST_TYPES))
            {
                strSQL += String.Format(" AND A.TEST_TYPES LIKE '%{0}%'", tMisContract.TEST_TYPES);
            }
            if (!String.IsNullOrEmpty(tMisContractCompany.COMPANY_NAME))
            {
                strSQL += String.Format(" AND E.COMPANY_NAME LIKE '%{0}%'", tMisContractCompany.COMPANY_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContractCompany.AREA))
            {
                strSQL += String.Format(" AND E.AREA = '{0}'", tMisContractCompany.AREA);
            }

            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += String.Format("  AND A.CONTRACT_STATUS = '{0}'", tMisContract.CONTRACT_STATUS);
            }
            return SqlHelper.ExecuteDataTable( BuildPagerExpress( strSQL,iIndex,iCount));
        }

        public int SelectContractPlanInforCount(TMisContractPointFreqVo tMisContractPointFreq, TMisContractVo tMisContract, TMisContractCompanyVo tMisContractCompany)
        {
            string strSQL = String.Format("SELECT A.*,E.COMPANY_NAME,F.DICT_TEXT AS AREA FROM dbo.T_MIS_CONTRACT A " +
                                    " INNER JOIN " +
                                    " (SELECT * FROM (SELECT DISTINCT (B.CONTRACT_ID) FROM dbo.T_MIS_CONTRACT_POINT_FREQ B WHERE IF_PLAN='{0}')T) D" +
                                    " ON D.CONTRACT_ID=A.ID" +
                                    " LEFT JOIN T_MIS_CONTRACT_COMPANY E ON E.ID=A.TESTED_COMPANY_ID" +
                                     " LEFT JOIN dbo.T_SYS_DICT F ON F.DICT_TYPE='administrative_area' AND F.DICT_CODE=E.AREA WHERE 1=1 ", tMisContractPointFreq.IF_PLAN);
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_TYPE))
            {
                strSQL += String.Format("  AND A.CONTRACT_TYPE = '{0}'", tMisContract.CONTRACT_TYPE);
            }
            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_CODE))
            {
                strSQL += String.Format("  AND A.CONTRACT_CODE LIKE '%{0}%'", tMisContract.CONTRACT_CODE);
            }
            if (!String.IsNullOrEmpty(tMisContract.TEST_TYPES))
            {
                strSQL += String.Format(" AND A.TEST_TYPES LIKE '%{0}%'", tMisContract.TEST_TYPES);
            }
            if (!String.IsNullOrEmpty(tMisContractCompany.COMPANY_NAME))
            {
                strSQL += String.Format(" AND E.COMPANY_NAME LIKE '%{0}%'", tMisContractCompany.COMPANY_NAME);
            }
            if (!String.IsNullOrEmpty(tMisContractCompany.AREA))
            {
                strSQL += String.Format(" AND E.AREA = '{0}'", tMisContractCompany.AREA);
            }

            if (!String.IsNullOrEmpty(tMisContract.CONTRACT_STATUS))
            {
                strSQL += String.Format("  AND A.CONTRACT_STATUS = '{0}'", tMisContract.CONTRACT_STATUS);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        /// <summary>
        /// 获取委托书监测点位频次信息
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable GetPointInfor(TMisContractPointFreqVo tMisContractPointFreq)
        {
            string strSQL =String.Format( "SELECT * FROM dbo.T_MIS_CONTRACT_POINT_FREQ A "+
                                                            " INNER JOIN dbo.T_MIS_CONTRACT_POINT  B ON A.CONTRACT_POINT_ID=B.ID"+
                                                            " INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO C ON C.ID=B.MONITOR_ID WHERE 1=1");
            if (!String.IsNullOrEmpty(tMisContractPointFreq.CONTRACT_ID)) {
                strSQL += String.Format(" AND A.CONTRACT_ID ='{0}'", tMisContractPointFreq.CONTRACT_ID);
            }
            if (!String.IsNullOrEmpty(tMisContractPointFreq.IF_PLAN))
            {
                strSQL += String.Format(" AND A.IF_PLAN='{0}'", tMisContractPointFreq.IF_PLAN);
            }
            strSQL += " ORDER BY B.MONITOR_ID,B.NUM,A.NUM";
            
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取当前即将进行的委托书监测点位频次信息 清远需求 胡方扬 2013-03-13
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable GetPointInforForFreq(TMisContractPointFreqVo tMisContractPointFreq)
        {
            string strSQL = String.Format(@"SELECT * FROM (SELECT A.ID,A.CONTRACT_ID,A.CONTRACT_POINT_ID,A.FREQ, A.NUM FROM dbo.T_MIS_CONTRACT_POINT_FREQ A
WHERE  A.CONTRACT_ID='{0}' AND A.IF_PLAN='0' 
AND A.NUM IN (SELECT MIN(CONVERT(INT,B.NUM)) FROM T_MIS_CONTRACT_POINT_FREQ B WHERE  B.CONTRACT_ID='{1}' AND B.IF_PLAN='0')) T

INNER JOIN dbo.T_MIS_CONTRACT_POINT  B ON T.CONTRACT_POINT_ID=B.ID
INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO C ON C.ID=B.MONITOR_ID WHERE 1=1", tMisContractPointFreq.CONTRACT_ID, tMisContractPointFreq.CONTRACT_ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取委托书已完成预约的监测点位频次信息
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable GetPlanFinishedPointInfor(TMisContractPointFreqVo tMisContractPointFreq)
        {
            string strSQL = String.Format("SELECT * FROM dbo.T_MIS_CONTRACT_POINT_FREQ A " +
                                                            " INNER JOIN dbo.T_MIS_CONTRACT_POINT  B ON A.CONTRACT_POINT_ID=B.ID" +
                                                            " INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO C ON C.ID=B.MONITOR_ID "+
                                                            " INNER JOIN dbo.T_MIS_CONTRACT_PLAN_POINT D ON D.CONTRACT_POINT_ID=B.ID"+
                                                            " INNER JOIN dbo.T_MIS_CONTRACT_PLAN E ON E.ID=D.PLAN_ID WHERE 1=1");
            if (!String.IsNullOrEmpty(tMisContractPointFreq.CONTRACT_ID))
            {
                strSQL += String.Format(" AND A.CONTRACT_ID ='{0}'", tMisContractPointFreq.CONTRACT_ID);
            }
            if (!String.IsNullOrEmpty(tMisContractPointFreq.IF_PLAN))
            {
                strSQL += String.Format(" AND A.IF_PLAN='{0}'", tMisContractPointFreq.IF_PLAN);
            }
            strSQL += " ORDER BY B.MONITOR_ID,B.NUM,A.NUM";

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取委托书下下有监测点位的所有监测类别 Create By Castle (胡方扬) 2012-12-21
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable GetPointMonitorInfor(TMisContractPointFreqVo tMisContractPointFreq,string strPlanId)
        {
            string strSQL = String.Format("SELECT  DISTINCT (C.ID),C.MONITOR_TYPE_NAME FROM dbo.T_MIS_CONTRACT_POINT_FREQ A" +
                                                            " INNER JOIN dbo.T_MIS_CONTRACT_POINT  B ON A.CONTRACT_POINT_ID=B.ID" +
                                                              " INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO C ON C.ID=B.MONITOR_ID ");
            if (!String.IsNullOrEmpty(strPlanId)) {
                strSQL += String.Format(" INNER JOIN dbo.T_MIS_CONTRACT_USERDUTY D ON D.MONITOR_TYPE_ID=C.ID");
            }

            strSQL+=String.Format(" WHERE 1=1");
            if (!String.IsNullOrEmpty(tMisContractPointFreq.CONTRACT_ID))
            {
                strSQL += String.Format(" AND A.CONTRACT_ID ='{0}'", tMisContractPointFreq.CONTRACT_ID);
            }
            if (!String.IsNullOrEmpty(tMisContractPointFreq.IF_PLAN))
            {
                strSQL += String.Format(" AND A.IF_PLAN='{0}'", tMisContractPointFreq.IF_PLAN);
            }
            if (!String.IsNullOrEmpty(strPlanId)) {
                strSQL += String.Format(" AND D.CONTRACT_PLAN_ID='{0}'", strPlanId);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取委托书下当前委托计划下监测点位的监测类别 Create By Castle (胡方扬) 2013-04-01
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public DataTable GetPointMonitorInforForPlan(TMisContractPointFreqVo tMisContractPointFreq, string strPlanId)
        {
            string strSQL = String.Format("SELECT  DISTINCT (C.ID),C.MONITOR_TYPE_NAME FROM dbo.T_MIS_CONTRACT_POINT_FREQ A" +
                                                            " INNER JOIN dbo.T_MIS_CONTRACT_POINT  B ON A.CONTRACT_POINT_ID=B.ID" +
                                                              " INNER JOIN dbo.T_BASE_MONITOR_TYPE_INFO C ON C.ID=B.MONITOR_ID ");
            if (!String.IsNullOrEmpty(strPlanId))
            {
                strSQL += String.Format(" INNER JOIN dbo.T_MIS_CONTRACT_PLAN_POINT D ON D.POINT_FREQ_ID=A.ID");
            }

            strSQL += String.Format(" WHERE 1=1");
            if (!String.IsNullOrEmpty(tMisContractPointFreq.CONTRACT_ID))
            {
                strSQL += String.Format(" AND A.CONTRACT_ID ='{0}'", tMisContractPointFreq.CONTRACT_ID);
            }
            if (!String.IsNullOrEmpty(tMisContractPointFreq.IF_PLAN))
            {
                strSQL += String.Format(" AND A.IF_PLAN='{0}'", tMisContractPointFreq.IF_PLAN);
            }
            if (!String.IsNullOrEmpty(strPlanId))
            {
                strSQL += String.Format(" AND D.PLAN_ID='{0}'", strPlanId);
            }
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取委托书下预约办理环节的监测类别 Create　By weilin 2014-04-09
        /// </summary>
        /// <param name="strContractID"></param>
        /// <param name="strPLanID"></param>
        /// <param name="strStatus"></param>
        /// <returns></returns>
        public DataTable GetMonitorInfoForPlan(string strContractID, string strPLanID, string strStatus)
        {
            string strSQL = @"select distinct c.ID,c.MONITOR_TYPE_NAME 
                            from T_MIS_MONITOR_TASK a left join T_MIS_MONITOR_SUBTASK b on(b.TASK_ID=a.ID) left join T_BASE_MONITOR_TYPE_INFO c on(c.ID=b.MONITOR_ID)
                            where a.CONTRACT_ID ='{0}' AND a.PLAN_ID='{1}' and b.TASK_STATUS='{2}'";
            strSQL = string.Format(strSQL, strContractID, strPLanID, strStatus);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractPointFreq"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractPointFreqVo tMisContractPointFreq)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractPointFreq)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisContractPointFreq.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractPointFreq.ID.ToString()));
                }
                //委托书ID
                if (!String.IsNullOrEmpty(tMisContractPointFreq.CONTRACT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisContractPointFreq.CONTRACT_ID.ToString()));
                }
                //委托书监测点位ID
                if (!String.IsNullOrEmpty(tMisContractPointFreq.CONTRACT_POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_POINT_ID = '{0}'", tMisContractPointFreq.CONTRACT_POINT_ID.ToString()));
                }
                //采样频次
                if (!String.IsNullOrEmpty(tMisContractPointFreq.SAMPLE_FREQ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_FREQ = '{0}'", tMisContractPointFreq.SAMPLE_FREQ.ToString()));
                }
                //监测频次
                if (!String.IsNullOrEmpty(tMisContractPointFreq.FREQ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FREQ = '{0}'", tMisContractPointFreq.FREQ.ToString()));
                }
                //执行序号
                if (!String.IsNullOrEmpty(tMisContractPointFreq.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tMisContractPointFreq.NUM.ToString()));
                }
                //是否已预约
                if (!String.IsNullOrEmpty(tMisContractPointFreq.IF_PLAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IF_PLAN = '{0}'", tMisContractPointFreq.IF_PLAN.ToString()));
                }
                //每次采样样品数
                if (!String.IsNullOrEmpty(tMisContractPointFreq.SAMPLENUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLENUM = '{0}'", tMisContractPointFreq.SAMPLENUM.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisContractPointFreq.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractPointFreq.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisContractPointFreq.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractPointFreq.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisContractPointFreq.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractPointFreq.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisContractPointFreq.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractPointFreq.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisContractPointFreq.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractPointFreq.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
