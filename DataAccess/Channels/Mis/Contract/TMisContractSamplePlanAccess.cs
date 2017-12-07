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
    /// 功能：自送样预约表
    /// 创建日期：2012-11-29
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractSamplePlanAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractSamplePlan">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_SAMPLE_PLAN " + this.BuildWhereStatement(tMisContractSamplePlan);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractSamplePlanVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_SAMPLE_PLAN  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisContractSamplePlanVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractSamplePlan">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractSamplePlanVo Details(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_SAMPLE_PLAN " + this.BuildWhereStatement(tMisContractSamplePlan));
            return SqlHelper.ExecuteObject(new TMisContractSamplePlanVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractSamplePlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractSamplePlanVo> SelectByObject(TMisContractSamplePlanVo tMisContractSamplePlan, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_CONTRACT_SAMPLE_PLAN " + this.BuildWhereStatement(tMisContractSamplePlan));
            return SqlHelper.ExecuteObjectList(tMisContractSamplePlan, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractSamplePlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractSamplePlanVo tMisContractSamplePlan, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_CONTRACT_SAMPLE_PLAN {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractSamplePlan));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractSamplePlan"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            string strSQL = "select * from T_MIS_CONTRACT_SAMPLE_PLAN " + this.BuildWhereStatement(tMisContractSamplePlan);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractSamplePlan">对象</param>
        /// <returns></returns>
        public TMisContractSamplePlanVo SelectByObject(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            string strSQL = "select * from T_MIS_CONTRACT_SAMPLE_PLAN " + this.BuildWhereStatement(tMisContractSamplePlan);
            return SqlHelper.ExecuteObject(new TMisContractSamplePlanVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractSamplePlan">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractSamplePlan, TMisContractSamplePlanVo.T_MIS_CONTRACT_SAMPLE_PLAN_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSamplePlan">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractSamplePlan, TMisContractSamplePlanVo.T_MIS_CONTRACT_SAMPLE_PLAN_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractSamplePlan.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSamplePlan_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractSamplePlan_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSamplePlanVo tMisContractSamplePlan_UpdateSet, TMisContractSamplePlanVo tMisContractSamplePlan_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractSamplePlan_UpdateSet, TMisContractSamplePlanVo.T_MIS_CONTRACT_SAMPLE_PLAN_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractSamplePlan_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_SAMPLE_PLAN where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            string strSQL = "delete from T_MIS_CONTRACT_SAMPLE_PLAN ";
            strSQL += this.BuildWhereStatement(tMisContractSamplePlan);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 自送样流程结束自动插入采样计划数据
        /// </summary>
        /// <param name="task_id"></param>
        /// <param name="strFreq"></param>
        /// <returns></returns>
        public bool CreateSamplePlan(string task_id, string strFreq) {
            ArrayList arrVo = new ArrayList();
             int IntFreq = 0;
             IntFreq=Convert.ToInt32(strFreq);
            TMisContractSamplePlanVo objItems=new TMisContractSamplePlanVo();
            if (IntFreq > 0)
            {
                for (int i = 0; i < IntFreq; i++)
                {
                    objItems.ID = GetSerialNumber("t_mis_contract_SamplePlanId");
                    objItems.CONTRACT_ID = task_id;
                    objItems.FREQ = strFreq;
                    objItems.IF_PLAN = "0";
                    objItems.NUM = (i + 1).ToString();
                    string strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_SAMPLE_PLAN(ID,CONTRACT_ID,FREQ,NUM,IF_PLAN) VALUES('{0}','{1}','{2}','{3}','{4}')",objItems.ID,objItems.CONTRACT_ID,objItems.FREQ,objItems.NUM,objItems.IF_PLAN);
                    arrVo.Add(strSQL);
                    strSQL = String.Format(" UPDATE T_MIS_CONTRACT_SAMPLE SET SAMPLE_PLAN_ID='{0}' WHERE CONTRACT_ID='{1}'",objItems.ID,task_id);
                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 自送样插入采样计划数据
        /// </summary>
        /// <param name="task_id"></param>
        /// <param name="strFreq"></param>
        /// <returns></returns>
        public bool CreateSamplePlan(TMisContractSamplePlanVo tMisContractSamplePlan, string strCompany_Name)
        {
            ArrayList arrVo = new ArrayList();
            int IntFreq = 0;
            //string sql = "select ID from T_BASE_COMPANY_INFO  Where 1=1  AND IS_DEL = '0' and Company_Name ='" + strCompany_Name + "'";
            //strNew_id = SqlHelper.ExecuteScalar(sql).ToString();
            if (!String.IsNullOrEmpty(tMisContractSamplePlan.FREQ))
            {
                IntFreq = Convert.ToInt32(tMisContractSamplePlan.FREQ);
                if (IntFreq > 0)
                {
                    for (int i = 0; i < IntFreq; i++)
                    {
                        tMisContractSamplePlan.NUM = (i + 1).ToString();
                        string strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_SAMPLE_PLAN(ID,CONTRACT_ID,FREQ,NUM,IF_PLAN) VALUES('{0}','{1}','{2}','{3}','{4}')", tMisContractSamplePlan.ID, tMisContractSamplePlan.CONTRACT_ID, tMisContractSamplePlan.FREQ, tMisContractSamplePlan.NUM, tMisContractSamplePlan.IF_PLAN);
                        arrVo.Add(strSQL);
                    }
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(arrVo);
        }

        /// <summary>
        /// 获取自送样计划监测类别 胡方扬 2012-12-29
        /// </summary>
        /// <param name="tMisContractSamplePlan"></param>
        /// <returns></returns>
        public DataTable GetSamplePlanMonitor(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            string strSQL = String.Format("SELECT  MONITOR_ID FROM dbo.T_MIS_CONTRACT_SAMPLE A " +
                                                           " INNER JOIN dbo.T_MIS_CONTRACT_SAMPLE_PLAN B ON B.ID=A.SAMPLE_PLAN_ID " +
                                                           " WHERE B.ID='{0}' GROUP BY A.MONITOR_ID", tMisContractSamplePlan.ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 获取自送样计划样品监测项目 胡方扬 2012-12-29
        /// </summary>
        /// <param name="tMisContractSamplePlan"></param>
        /// <returns></returns>
        public DataTable GetSamplePlanItems(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            string strSQL = String.Format("SELECT "+ 
                                                            "  A.ID AS SAMPLE_ID,A.CONTRACT_ID,A.MONITOR_ID,A.SAMPLE_TYPE,A.SAMPLE_NAME,A.SAMPLE_COUNT, "+
                                                            "  B.ID AS PLAN_ID,B.FREQ,B.NUM,B.IF_PLAN,"+
                                                             " C.ID AS PLAN_ITEM_ID,C.ITEM_ID,"+
                                                            "  D.ITEM_NAME,E.MONITOR_TYPE_NAME"+
                                                            "  FROM dbo.T_MIS_CONTRACT_SAMPLE A"+
                                                            "  LEFT JOIN dbo.T_MIS_CONTRACT_SAMPLE_PLAN B ON B.ID=A.SAMPLE_PLAN_ID " +
                                                            "  LEFT JOIN dbo.T_MIS_CONTRACT_SAMPLEITEM  C ON C.CONTRACT_SAMPLE_ID=A.ID" +
                                                             " LEFT JOIN dbo.T_BASE_ITEM_INFO D ON D.ID=C.ITEM_ID" +
                                                            "  LEFT JOIN dbo.T_BASE_MONITOR_TYPE_INFO E ON E.ID=A.MONITOR_ID" +
                                                             " WHERE  B.ID='{0}'", tMisContractSamplePlan.ID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractSamplePlan"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractSamplePlanVo tMisContractSamplePlan)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractSamplePlan)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisContractSamplePlan.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractSamplePlan.ID.ToString()));
                }
                //委托书ID
                if (!String.IsNullOrEmpty(tMisContractSamplePlan.CONTRACT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisContractSamplePlan.CONTRACT_ID.ToString()));
                }
                //监测频次
                if (!String.IsNullOrEmpty(tMisContractSamplePlan.FREQ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FREQ = '{0}'", tMisContractSamplePlan.FREQ.ToString()));
                }
                //执行序号
                if (!String.IsNullOrEmpty(tMisContractSamplePlan.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tMisContractSamplePlan.NUM.ToString()));
                }
                //是否已预约
                if (!String.IsNullOrEmpty(tMisContractSamplePlan.IF_PLAN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IF_PLAN = '{0}'", tMisContractSamplePlan.IF_PLAN.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisContractSamplePlan.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractSamplePlan.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisContractSamplePlan.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractSamplePlan.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisContractSamplePlan.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractSamplePlan.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisContractSamplePlan.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractSamplePlan.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisContractSamplePlan.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractSamplePlan.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
