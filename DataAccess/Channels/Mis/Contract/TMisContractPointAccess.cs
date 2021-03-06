using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Mis.Contract;
using i3.ValueObject;
using i3.ValueObject.Channels.Base.Point;

namespace i3.DataAccess.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书监测点信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPointAccess : SqlHelper
    {

        #region 处理函数

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPoint">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPointVo tMisContractPoint)
        {
            string strSQL = "select Count(*) from T_MIS_CONTRACT_POINT " + this.BuildWhereStatement(tMisContractPoint);
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPointVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_POINT  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TMisContractPointVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPoint">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPointVo Details(TMisContractPointVo tMisContractPoint)
        {
            string strSQL = String.Format("select * from  T_MIS_CONTRACT_POINT " + this.BuildWhereStatement(tMisContractPoint));
            return SqlHelper.ExecuteObject(new TMisContractPointVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractPointVo> SelectByObject(TMisContractPointVo tMisContractPoint, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_MIS_CONTRACT_POINT " + this.BuildWhereStatement(tMisContractPoint));
            return SqlHelper.ExecuteObjectList(tMisContractPoint, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractPointVo tMisContractPoint, int iIndex, int iCount)
        {

            string strSQL = " select * from T_MIS_CONTRACT_POINT {0}  order by MONITOR_ID,NUM";
            strSQL = String.Format(strSQL, BuildWhereStatement(tMisContractPoint));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPoint"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPointVo tMisContractPoint)
        {
            string strSQL = "select * from T_MIS_CONTRACT_POINT " + this.BuildWhereStatement(tMisContractPoint);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPoint">对象</param>
        /// <returns></returns>
        public TMisContractPointVo SelectByObject(TMisContractPointVo tMisContractPoint)
        {
            string strSQL = "select * from T_MIS_CONTRACT_POINT " + this.BuildWhereStatement(tMisContractPoint);
            return SqlHelper.ExecuteObject(new TMisContractPointVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tMisContractPoint">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPointVo tMisContractPoint)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tMisContractPoint, TMisContractPointVo.T_MIS_CONTRACT_POINT_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPoint">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointVo tMisContractPoint)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPoint, TMisContractPointVo.T_MIS_CONTRACT_POINT_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tMisContractPoint.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPoint_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractPoint_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointVo tMisContractPoint_UpdateSet, TMisContractPointVo tMisContractPoint_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tMisContractPoint_UpdateSet, TMisContractPointVo.T_MIS_CONTRACT_POINT_TABLE);
            strSQL += this.BuildWhereStatement(tMisContractPoint_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_MIS_CONTRACT_POINT where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractPointVo tMisContractPoint)
        {
            string strSQL = "delete from T_MIS_CONTRACT_POINT ";
            strSQL += this.BuildWhereStatement(tMisContractPoint);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 复制企业监测点位信息到委托企业点位信息临时表  Create By Castle(胡方扬) 2012-11-30
        /// </summary>
        /// <param name="strContractId">委托书ID</param>
        /// <param name="strCompanyPointId">企业点位ID</param>
        /// <returns>True Or False</returns>
        public bool InsertContratPoint(string strContractId, string strCompanyPointId)
        {
            ArrayList arrVo = new ArrayList();
            string strSQL = "";
            string[] strCompanyPointArr = strCompanyPointId.Split(';');
            foreach (string companyPointId in strCompanyPointArr)
            {
                if (!String.IsNullOrEmpty(companyPointId))
                {
                    TMisContractPointVo objTp = new TMisContractPointVo();
                    objTp.ID = GetSerialNumber("t_mis_contract_PointID");
                    strSQL = String.Format("INSERT INTO dbo.T_MIS_CONTRACT_POINT(ID,CONTRACT_ID,POINT_ID,MONITOR_ID,POINT_NAME,DYNAMIC_ATTRIBUTE_ID,CREATE_DATE,ADDRESS,SAMPLE_FREQ,SAMPLE_DAY,FREQ,IS_DEL,NUM, LOCAL_ST_CONDITION_ID,NATIONAL_ST_CONDITION_ID)" +
                        " SELECT '{0}','{1}',ID,MONITOR_ID,POINT_NAME,DYNAMIC_ATTRIBUTE_ID,CREATE_DATE,ADDRESS,CASE  WHEN SAMPLE_FREQ IS NULL THEN '01' ELSE SAMPLE_FREQ END AS SAMPLE_FREQ,CASE  WHEN SAMPLE_DAY IS NULL THEN '01' ELSE SAMPLE_DAY END AS SAMPLE_DAY,FREQ,IS_DEL,NUM, LOCAL_ST_CONDITION_ID,NATIONAL_ST_CONDITION_ID FROM dbo.T_BASE_COMPANY_POINT  WHERE ID='{2}' AND IS_DEL='0'"
                    , objTp.ID, strContractId, companyPointId);

                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);

        }


        /// <summary>
        /// 复制企业监测点位信息到委托企业点位信息临时表  Create By Castle(胡方扬) 2013-07-19
        /// </summary>
        /// <param name="strID">委托书点位ID</param>
        /// <param name="strCompanyPointId">企业点位ID</param>
        /// <returns>True Or False</returns>
        public bool InsertContratPointForPlan(string strID, string strCompanyPointId)
        {
            ArrayList arrVo = new ArrayList();
            string strSQL = "";
            string[] strCompanyPointArr = strCompanyPointId.Split(';');
            foreach (string companyPointId in strCompanyPointArr)
            {
                if (!String.IsNullOrEmpty(companyPointId)&&!String.IsNullOrEmpty(strID))
                {

                    strSQL = String.Format("INSERT INTO dbo.T_MIS_CONTRACT_POINT(ID,POINT_ID,MONITOR_ID,POINT_NAME,DYNAMIC_ATTRIBUTE_ID,CREATE_DATE,ADDRESS,SAMPLE_FREQ,SAMPLE_DAY,FREQ,IS_DEL,NUM, LOCAL_ST_CONDITION_ID,NATIONAL_ST_CONDITION_ID)" +
                        " SELECT '{0}',ID,MONITOR_ID,POINT_NAME,DYNAMIC_ATTRIBUTE_ID,CREATE_DATE,ADDRESS,CASE  WHEN SAMPLE_FREQ IS NULL THEN '01' ELSE SAMPLE_FREQ END AS SAMPLE_FREQ,CASE  WHEN SAMPLE_DAY IS NULL THEN '01' ELSE SAMPLE_DAY END AS SAMPLE_FREQ,FREQ,IS_DEL,NUM, LOCAL_ST_CONDITION_ID,NATIONAL_ST_CONDITION_ID FROM dbo.T_BASE_COMPANY_POINT  WHERE ID='{1}' AND IS_DEL='0'"
                    , strID, companyPointId);

                    arrVo.Add(strSQL);
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(arrVo);

        }

        /// <summary>
        /// 功能描述：获取预约点位明细信息
        /// 创建时间：2012-12-20
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strPlanID">预约ID</param>
        /// <returns></returns>
        public DataTable SelectByTableForPlan(string strPlanID)
        {
            string strSQL = @"select point.*,pointitem.CONTRACT_POINT_ID,pointitem.ITEM_ID,pointitem.CONDITION_ID,pointitem.CONDITION_TYPE,pointitem.ST_UPPER,pointitem.ST_LOWER,
                                            dbo.Rpt_AnalysisManagerID(pointitem.ITEM_ID) as ANALYSIS_MANAGER,
                                            dbo.Rpt_GetAnalysisID(pointitem.ITEM_ID) as ANALYSIS_ID
                                            from T_MIS_CONTRACT_POINT point
                                            left join T_MIS_CONTRACT_POINTITEM pointitem on pointitem.CONTRACT_POINT_ID=point.ID
                                            where point.IS_DEL='0' and point.ID in 
                                            (select CONTRACT_POINT_ID from T_MIS_CONTRACT_PLAN_POINT where PLAN_ID='{0}')";
            strSQL = string.Format(strSQL, strPlanID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：获取预约点位信息
        /// 创建时间：2012-12-20
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strPlanID">预约ID</param>
        /// <returns></returns>
        public DataTable SelectPointTable(string strPlanID)
        {
            string strSQL = @"select  * from T_MIS_CONTRACT_POINT point
                                            where IS_DEL='0' and ID in 
                                            (select CONTRACT_POINT_ID from T_MIS_CONTRACT_PLAN_POINT where PLAN_ID='{0}')";
            strSQL = string.Format(strSQL, strPlanID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 功能描述：预约点位的监测类别
        /// 创建时间：2012-12-21
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strPlanID">预约ID</param>
        /// <returns></returns>
        public DataTable GetTestType(string strPlanID)
        {
            string strSQL = @"select  MONITOR_ID from T_MIS_CONTRACT_POINT point
                                            where IS_DEL='0' and ID in 
                                            (select CONTRACT_POINT_ID from T_MIS_CONTRACT_PLAN_POINT where PLAN_ID='{0}') group by MONITOR_ID";
            strSQL = string.Format(strSQL, strPlanID);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 插入企业点位到任务点位、点位频次、监测项目中 Create By 胡方扬 2013-06-06
        /// </summary>
        /// <param name="objTable"></param>
        /// <param name="strContractId"></param>
        /// <param name="strPlanId"></param>
        /// <returns></returns>
        public bool InsertPointsInfor(DataTable objTable, string strContractId, string strPlanId, string Company_Names, string strReqContractType, ref string New_ID)
        {
            ArrayList objVo = new ArrayList();
            //int count = 0;
            if (objTable.Rows.Count > 0)
            {
                //string sql = "select ID from T_BASE_COMPANY_INFO  Where 1=1  AND IS_DEL = '0' and Company_Name ='" + Company_Names + "'";
                //string rtn_id = SqlHelper.ExecuteScalar(sql).ToString();
                for (int i = 0; i < objTable.Rows.Count; i++)
                {
                    //if (objTable.Rows[i]["COMPANY_ID"].ToString() == rtn_id)
                    //{
                    //New_ID = rtn_id;
                    //插入点位
                    string strID = GetSerialNumber("t_mis_contract_pointID");
                    string strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINT(ID,POINT_ID,POINT_NAME,IS_DEL,MONITOR_ID,CONTRACT_ID) VALUES('{0}','{1}','{2}','0','{3}','{4}')", strID, objTable.Rows[i]["ID"].ToString(), objTable.Rows[i]["POINT_NAME"].ToString(), objTable.Rows[i]["MONITOR_ID"].ToString(), strContractId);
                    objVo.Add(strSQL);
                    ////插入点位频次
                    //if (blFreq)
                    //{
                    //    string strPointFreqID = GetSerialNumber("t_mis_contractplanfreqID");
                    //    strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINT_FREQ(ID,CONTRACT_POINT_ID,FREQ,NUM,IF_PLAN,CONTRACT_ID) VALUES('{0}','{1}','1','1','0','{2}')", strPointFreqID, strID, strContractId);
                    //    objVo.Add(strSQL);

                    //    strSQL = String.Format("INSERT INTO T_MIS_CONTRACT_PLAN_POINT(ID,PLAN_ID,CONTRACT_POINT_ID,POINT_FREQ_ID) VALUES('{0}','{1}','{2}','{3}')", GetSerialNumber("t_mis_contract_planpointId"), strPlanId, strID, strPointFreqID);
                    //    objVo.Add(strSQL);
                    //}
                    //else
                    //{
                    strSQL = String.Format("INSERT INTO T_MIS_CONTRACT_PLAN_POINT(ID,PLAN_ID,CONTRACT_POINT_ID) VALUES('{0}','{1}','{2}')", GetSerialNumber("t_mis_contract_planpointId"), strPlanId, strID);

                    objVo.Add(strSQL);
                    //}
                    //}
                    //else
                    //{
                    //    count++;
                    //}
                }
                #region//暂不用的代码
                //if (objTable.Rows.Count == count)
                //{
                //    TBaseCompanyPointVo objCompany = new TBaseCompanyPointVo();
                //    objCompany.COMPANY_ID = rtn_id;
                //    objCompany.POINT_TYPE = strReqContractType;
                //    objCompany.IS_DEL = "0";
                //    New_ID = rtn_id;
                //    DataTable objTables = SelectByTable(objCompany);
                //    if (objTables.Rows.Count > 0)
                //    {
                //        for (int j = 0; j < objTables.Rows.Count; j++)
                //        {
                //            //插入点位
                //            string strID = GetSerialNumber("t_mis_contract_pointID");
                //            string strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINT(ID,POINT_ID,POINT_NAME,IS_DEL,MONITOR_ID,CONTRACT_ID) VALUES('{0}','{1}','{2}','0','{3}','{4}')", strID, objTables.Rows[j]["ID"].ToString(), objTables.Rows[j]["POINT_NAME"].ToString(), objTables.Rows[j]["MONITOR_ID"].ToString(), strContractId);
                //            objVo.Add(strSQL);
                //            strSQL = String.Format("INSERT INTO T_MIS_CONTRACT_PLAN_POINT(ID,PLAN_ID,CONTRACT_POINT_ID) VALUES('{0}','{1}','{2}')", GetSerialNumber("t_mis_contract_planpointId"), strPlanId, strID);
                //            objVo.Add(strSQL);
                //        }
                //    }
                //}
                #endregion
            }
            return SqlHelper.ExecuteSQLByTransaction(objVo);
        }
        public DataTable SelectByTable(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            string strSQL = "select * from T_BASE_COMPANY_POINT  where IS_DEL='0' and POINT_TYPE='" + tBaseCompanyPoint.POINT_TYPE + "' and COMPANY_ID='" + tBaseCompanyPoint.COMPANY_ID + "'";
            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 插入环境质量点位及其相关点位、垂线、监测项目
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public bool InsertEnvPoints(TMisContractPointVo tMisContractPoint,string PlanId,string KeyCloumns,string TableName)
        {
            ArrayList objVo = new ArrayList();
            if (!String.IsNullOrEmpty(tMisContractPoint.POINT_ID) && !String.IsNullOrEmpty(tMisContractPoint.POINT_NAME))
            {
                string[] itemArr = tMisContractPoint.POINT_ID.Split(';');
                string[] itemName = tMisContractPoint.POINT_NAME.Split(';');
                if (itemArr.Length > 0&&(itemArr.Length==itemName.Length))
                {

                    for (int i = 0; i < itemArr.Length;i++ )
                    {
                        //插入点位
                        string strID = GetSerialNumber("t_mis_contract_pointID");
                        string strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINT(ID,POINT_ID,POINT_NAME,IS_DEL,MONITOR_ID) VALUES('{0}','{1}','{2}','0','{3}')", strID, itemArr[i], itemName[i],tMisContractPoint.MONITOR_ID);
                        objVo.Add(strSQL);
                        //插入点位频次
                        string strPointFreqID = GetSerialNumber("t_mis_contractplanfreqID");
                        strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINT_FREQ(ID,CONTRACT_POINT_ID,FREQ,NUM,IF_PLAN) VALUES('{0}','{1}','1','1','0')", strPointFreqID,strID);
                        objVo.Add(strSQL);
                        //插入点位监测项目信息
                        string tempSql =String.Format( " SELECT ITEM_ID FROM {0} WHERE {1}='{2}'",TableName,KeyCloumns,itemArr[i]);
                        DataTable dtItmes = SqlHelper.ExecuteDataTable(tempSql);
                        if (dtItmes.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dtItmes.Rows)
                            {
                                strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINTITEM(ID,CONTRACT_POINT_ID,ITEM_ID) VALUES('{0}','{1}','{2}')", GetSerialNumber("t_mis_contract_pointItemId"),strID,dr["ITEM_ID"].ToString());
                                objVo.Add(strSQL);
                            }
                        }
                        //插入环境质量计划与点位关系数据
                        strSQL = String.Format("INSERT INTO T_MIS_CONTRACT_PLAN_POINT(ID,PLAN_ID,CONTRACT_POINT_ID,POINT_FREQ_ID) VALUES('{0}','{1}','{2}','{3}')", GetSerialNumber("t_mis_contract_planpointId"), PlanId, strID,strPointFreqID);
                        objVo.Add(strSQL);
                    }
                }
            }
            return SqlHelper.ExecuteSQLByTransaction(objVo);
        }

        /// <summary>
        /// 插入环境质量点位及其相关点位、垂线、监测项目,并返回相关的点位信息（扩展）Create By : weilin 2014-11-14
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public DataTable InsertEnvPointsEx(DataTable dtEnv, string PlanId, string TypeId, string KeyCloumns, string TableName, string strEnvTypeName)
        {
            DataTable dt = new DataTable();
            ArrayList objVo = new ArrayList();
            string strSQL = string.Empty;
            //清除数据
            strSQL = String.Format(@" delete c from T_MIS_CONTRACT_PLAN_POINT a 
                        left join T_MIS_CONTRACT_POINT b on(a.CONTRACT_POINT_ID=b.ID)
                        left join T_MIS_CONTRACT_POINTITEM c on(b.ID=c.CONTRACT_POINT_ID)
                        where a.PLAN_ID='{0}' and b.MONITOR_ID='{1}'", PlanId, TypeId);
            objVo.Add(strSQL);
            strSQL = String.Format(@" delete b from T_MIS_CONTRACT_PLAN_POINT a 
                        left join T_MIS_CONTRACT_POINT b on(a.CONTRACT_POINT_ID=b.ID)
                        left join T_MIS_CONTRACT_POINTITEM c on(b.ID=c.CONTRACT_POINT_ID)
                        where a.PLAN_ID='{0}' and b.MONITOR_ID='{1}'", PlanId, TypeId);
            objVo.Add(strSQL);
            strSQL = String.Format(@" delete a from T_MIS_CONTRACT_PLAN_POINT a 
                        left join T_MIS_CONTRACT_POINT b on(a.CONTRACT_POINT_ID=b.ID)
                        left join T_MIS_CONTRACT_POINTITEM c on(b.ID=c.CONTRACT_POINT_ID)
                        where a.PLAN_ID='{0}' and b.MONITOR_ID='{1}'", PlanId, TypeId);
            objVo.Add(strSQL);

            for (int i = 0; i < dtEnv.Rows.Count; i++)
            {
                //插入点位
                string strID = GetSerialNumber("t_mis_contract_pointID");
                if (TypeId == "FunctionNoise" || TypeId == "AreaNoise" || TypeId == "EnvRoadNoise")
                    strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINT(ID,POINT_ID,POINT_NAME,IS_DEL,MONITOR_ID,REMARK5,REMARK4) VALUES('{0}','{1}','{2}','0','{3}','{4}','{5}')", strID, dtEnv.Rows[i]["ID"].ToString(), dtEnv.Rows[i]["POINT_NAME"].ToString(), dtEnv.Rows[i]["ENVTYPE_ID"].ToString(), dtEnv.Rows[i]["POINT_CODE"].ToString(), dtEnv.Rows[i]["MONTH"].ToString());
                else
                    strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINT(ID,POINT_ID,POINT_NAME,IS_DEL,MONITOR_ID) VALUES('{0}','{1}','{2}','0','{3}')", strID, dtEnv.Rows[i]["ID"].ToString(), dtEnv.Rows[i]["POINT_NAME"].ToString(), dtEnv.Rows[i]["ENVTYPE_ID"].ToString());
                objVo.Add(strSQL);
                //插入点位频次
                string strPointFreqID = GetSerialNumber("t_mis_contractplanfreqID");
                strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINT_FREQ(ID,CONTRACT_POINT_ID,FREQ,NUM,IF_PLAN) VALUES('{0}','{1}','1','1','0')", strPointFreqID, strID);
                objVo.Add(strSQL);
                //插入点位监测项目信息
                string tempSql = String.Format(" SELECT ITEM_ID FROM {0} WHERE {1}='{2}'", TableName, KeyCloumns, dtEnv.Rows[i]["ID"].ToString());
                DataTable dtItmes = SqlHelper.ExecuteDataTable(tempSql);
                if (dtItmes.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtItmes.Rows)
                    {
                        strSQL = String.Format(" INSERT INTO T_MIS_CONTRACT_POINTITEM(ID,CONTRACT_POINT_ID,ITEM_ID) VALUES('{0}','{1}','{2}')", GetSerialNumber("t_mis_contract_pointItemId"), strID, dr["ITEM_ID"].ToString());
                        objVo.Add(strSQL);
                    }
                }
                //插入环境质量计划与点位关系数据
                strSQL = String.Format("INSERT INTO T_MIS_CONTRACT_PLAN_POINT(ID,PLAN_ID,CONTRACT_POINT_ID,POINT_FREQ_ID) VALUES('{0}','{1}','{2}','{3}')", GetSerialNumber("t_mis_contract_planpointId"), PlanId, strID, strPointFreqID);
                objVo.Add(strSQL);
            }

            if (SqlHelper.ExecuteSQLByTransaction(objVo))
            {
                strSQL = String.Format(@"select b.ID,b.POINT_ID,b.POINT_NAME,b.MONITOR_ID ENVTYPE_ID, '{0}' ENVTYPE_NAME from T_MIS_CONTRACT_PLAN_POINT a 
                        left join T_MIS_CONTRACT_POINT b on(a.CONTRACT_POINT_ID=b.ID)
                        where a.PLAN_ID='{1}' and b.MONITOR_ID='{2}'", strEnvTypeName, PlanId, TypeId);

                dt = SqlHelper.ExecuteDataTable(strSQL);
            }
            return dt;
        }
        /// <summary>
        /// 获取常规监测任务的点位信息 Create By : weilin 2014-11-15
        /// </summary>
        /// <param name="PlanId"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public DataTable GetPlanPoints(string PlanId, string TypeId)
        {
            DataTable dt = new DataTable();
            string strSQL = string.Empty;
            strSQL = String.Format(@"select b.ID,b.POINT_ID,b.POINT_NAME,b.MONITOR_ID ENVTYPE_ID,c.MONITOR_TYPE_NAME ENVTYPE_NAME from T_MIS_CONTRACT_PLAN_POINT a 
                        left join T_MIS_CONTRACT_POINT b on(a.CONTRACT_POINT_ID=b.ID)
                        left join T_BASE_MONITOR_TYPE_INFO c on(b.MONITOR_ID=c.ID)
                        where a.PLAN_ID='{0}' and b.MONITOR_ID='{1}'", PlanId, TypeId);

            dt = SqlHelper.ExecuteDataTable(strSQL);

            return dt;
        }
        
        /// <summary>
        /// 动态表获取不同的环境质量点位
        /// </summary>
        /// <param name="FatherKeyColumn"></param>
        /// <param name="FatherKeyValue"></param>
        /// <param name="KeyColumn"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GetEnvInforForDefineTable(string strEnvTypeId, string strEnvTypeName, string FatherKeyColumn, string FatherKeyValue, string KeyColumn, string TableName, string strEnvYear, string strEnvMonth)
        {
            //string strSQL = String.Format(" SELECT ID,{0} AS POINT_NAME FROM {1} WHERE {2}='{3}'",KeyColumn,TableName,FatherKeyColumn,FatherKeyValue);

            string strSQL = "";

            FatherKeyValue = FatherKeyValue.Replace(",", "','");

            if (strEnvTypeId == "EnvPollute")//常规污染源
            {
                strSQL = String.Format(" SELECT P.ID,P.{0}+'监测点' AS POINT_NAME,'{1}' AS ENVTYPE_ID,'{2}' AS ENVTYPE_NAME,Q.ID,Q.ENTER_NAME " +
                     "FROM {3} P LEFT JOIN T_ENV_P_POLLUTE_TYPE T ON P.TYPE_ID = T.ID LEFT JOIN T_ENV_P_ENTERINFO Q  on Q.ID = T.SATAIONS_ID " +
                    "WHERE P.YEAR='{6}' and P.MONTH='{7}' and Q.IS_DEL='0'" + (FatherKeyValue == "" ? "" : " and P.{4} IN ('{5}')"), KeyColumn, strEnvTypeId, strEnvTypeName, TableName, FatherKeyColumn, FatherKeyValue, strEnvYear, strEnvMonth);
                strSQL += " order by Q.ENTER_NAME";
            }
            else if (strEnvTypeId == "SewageCharges")//排污收费企业 add 黄进军 20151217
            {
                strSQL = String.Format(" SELECT P.ID,P.{0}+'监测点' AS POINT_NAME,'{1}' AS ENVTYPE_ID,'{2}' AS ENVTYPE_NAME,Q.ID,Q.ENTER_NAME " +
                     "FROM {3} P LEFT JOIN T_ENV_P_POLLUTE_TYPE T ON P.TYPE_ID = T.ID LEFT JOIN T_ENV_P_ENTERINFO Q  on Q.ID = T.SATAIONS_ID " +
                    "WHERE Q.REMARK1='SewageCharges' and P.YEAR='{6}' and P.MONTH='{7}' and Q.IS_DEL='0'" + (FatherKeyValue == "" ? "" : " and P.{4} IN ('{5}')"), KeyColumn, strEnvTypeId, strEnvTypeName, TableName, FatherKeyColumn, FatherKeyValue, strEnvYear, strEnvMonth);
                strSQL += " order by Q.ENTER_NAME";
            }
            else if (strEnvTypeId == "SewagePlant")//污水厂企业 add 黄进军 20150106
            {
                strSQL = String.Format(" SELECT P.ID,P.{0}+'监测点' AS POINT_NAME,'{1}' AS ENVTYPE_ID,'{2}' AS ENVTYPE_NAME,Q.ID,Q.ENTER_NAME " +
                     "FROM {3} P LEFT JOIN T_ENV_P_POLLUTE_TYPE T ON P.TYPE_ID = T.ID LEFT JOIN T_ENV_P_ENTERINFO Q  on Q.ID = T.SATAIONS_ID " +
                    "WHERE Q.REMARK1='SewagePlant' and P.YEAR='{6}' and P.MONTH='{7}' and Q.IS_DEL='0'" + (FatherKeyValue == "" ? "" : " and P.{4} IN ('{5}')"), KeyColumn, strEnvTypeId, strEnvTypeName, TableName, FatherKeyColumn, FatherKeyValue, strEnvYear, strEnvMonth);
                strSQL += " order by Q.ENTER_NAME";
            }
            else if (strEnvTypeId == "StateControlledWastewater")//国控企业（废水） add 黄进军 20150106
            {
                strSQL = String.Format(" SELECT P.ID,P.{0}+'监测点' AS POINT_NAME,'{1}' AS ENVTYPE_ID,'{2}' AS ENVTYPE_NAME,Q.ID,Q.ENTER_NAME " +
                     "FROM {3} P LEFT JOIN T_ENV_P_POLLUTE_TYPE T ON P.TYPE_ID = T.ID LEFT JOIN T_ENV_P_ENTERINFO Q  on Q.ID = T.SATAIONS_ID " +
                    "WHERE Q.REMARK1='StateControlledWastewater' and P.YEAR='{6}' and P.MONTH='{7}' and Q.IS_DEL='0'" + (FatherKeyValue == "" ? "" : " and P.{4} IN ('{5}')"), KeyColumn, strEnvTypeId, strEnvTypeName, TableName, FatherKeyColumn, FatherKeyValue, strEnvYear, strEnvMonth);
                strSQL += " order by Q.ENTER_NAME";
            }
            else if (strEnvTypeId == "StateControlledGas")//国控企业（废气） add 黄进军 20150106
            {
                strSQL = String.Format(" SELECT P.ID,P.{0}+'监测点' AS POINT_NAME,'{1}' AS ENVTYPE_ID,'{2}' AS ENVTYPE_NAME,Q.ID,Q.ENTER_NAME " +
                     "FROM {3} P LEFT JOIN T_ENV_P_POLLUTE_TYPE T ON P.TYPE_ID = T.ID LEFT JOIN T_ENV_P_ENTERINFO Q  on Q.ID = T.SATAIONS_ID " +
                    "WHERE Q.REMARK1='StateControlledGas' and P.YEAR='{6}' and P.MONTH='{7}' and Q.IS_DEL='0'" + (FatherKeyValue == "" ? "" : " and P.{4} IN ('{5}')"), KeyColumn, strEnvTypeId, strEnvTypeName, TableName, FatherKeyColumn, FatherKeyValue, strEnvYear, strEnvMonth);
                strSQL += " order by Q.ENTER_NAME";
            }
            else if (strEnvTypeId == "HeavyMetal")//重金属企业 add 黄进军 20150106
            {
                strSQL = String.Format(" SELECT P.ID,P.{0}+'监测点' AS POINT_NAME,'{1}' AS ENVTYPE_ID,'{2}' AS ENVTYPE_NAME,Q.ID,Q.ENTER_NAME " +
                     "FROM {3} P LEFT JOIN T_ENV_P_POLLUTE_TYPE T ON P.TYPE_ID = T.ID LEFT JOIN T_ENV_P_ENTERINFO Q  on Q.ID = T.SATAIONS_ID " +
                    "WHERE Q.REMARK1='HeavyMetal' and P.YEAR='{6}' and P.MONTH='{7}' and Q.IS_DEL='0'" + (FatherKeyValue == "" ? "" : " and P.{4} IN ('{5}')"), KeyColumn, strEnvTypeId, strEnvTypeName, TableName, FatherKeyColumn, FatherKeyValue, strEnvYear, strEnvMonth);
                strSQL += " order by Q.ENTER_NAME";
            }
            else if (strEnvTypeId == "Control")//省控企业 add 黄进军 20150106
            {
                strSQL = String.Format(" SELECT P.ID,P.{0}+'监测点' AS POINT_NAME,'{1}' AS ENVTYPE_ID,'{2}' AS ENVTYPE_NAME,Q.ID,Q.ENTER_NAME " +
                     "FROM {3} P LEFT JOIN T_ENV_P_POLLUTE_TYPE T ON P.TYPE_ID = T.ID LEFT JOIN T_ENV_P_ENTERINFO Q  on Q.ID = T.SATAIONS_ID " +
                    "WHERE Q.REMARK1='Control' and P.YEAR='{6}' and P.MONTH='{7}' and Q.IS_DEL='0'" + (FatherKeyValue == "" ? "" : " and P.{4} IN ('{5}')"), KeyColumn, strEnvTypeId, strEnvTypeName, TableName, FatherKeyColumn, FatherKeyValue, strEnvYear, strEnvMonth);
                strSQL += " order by Q.ENTER_NAME";
            }
            else if (strEnvTypeId == "SensitiveGroundwater")//敏感地下水企业 add 黄进军 20150106
            {
                strSQL = String.Format(" SELECT P.ID,P.{0}+'监测点' AS POINT_NAME,'{1}' AS ENVTYPE_ID,'{2}' AS ENVTYPE_NAME,Q.ID,Q.ENTER_NAME " +
                     "FROM {3} P LEFT JOIN T_ENV_P_POLLUTE_TYPE T ON P.TYPE_ID = T.ID LEFT JOIN T_ENV_P_ENTERINFO Q  on Q.ID = T.SATAIONS_ID " +
                    "WHERE Q.REMARK1='SensitiveGroundwater' and P.YEAR='{6}' and P.MONTH='{7}' and Q.IS_DEL='0'" + (FatherKeyValue == "" ? "" : " and P.{4} IN ('{5}')"), KeyColumn, strEnvTypeId, strEnvTypeName, TableName, FatherKeyColumn, FatherKeyValue, strEnvYear, strEnvMonth);
                strSQL += " order by Q.ENTER_NAME";
            }
            else
            {
                strSQL = String.Format(" SELECT ID,{0}+'监测点' AS POINT_NAME,'{1}' AS ENVTYPE_ID,'{2}' AS ENVTYPE_NAME FROM {3} WHERE YEAR='{6}' and MONTH='{7}' and IS_DEL='0'" + (FatherKeyValue == "" ? "" : " and {4} IN ('{5}')"), KeyColumn, strEnvTypeId, strEnvTypeName, TableName, FatherKeyColumn, FatherKeyValue, strEnvYear, strEnvMonth);
            }

            // string strSQL = String.Format(" SELECT ID,{0}+'监测点' AS POINT_NAME,'{1}' AS ENVTYPE_ID,'{2}' AS ENVTYPE_NAME FROM {3} WHERE YEAR='{6}' and MONTH='{7}' and IS_DEL='0'" + (FatherKeyValue == "" ? "" : " and {4} IN ('{5}')"), KeyColumn, strEnvTypeId, strEnvTypeName, TableName, FatherKeyColumn, FatherKeyValue, strEnvYear, strEnvMonth);
            return SqlHelper.ExecuteDataTable(strSQL);
        }


        /// <summary>
        /// 动态表获取不同的环境质量点位
        /// </summary>
        /// <param name="FatherKeyColumn"></param>
        /// <param name="FatherKeyValue"></param>
        /// <param name="KeyColumn"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GetEnvInforForDefineTable(string strEnvTypeId, string strEnvTypeName, string FatherKeyColumn, string FatherKeyValue, string KeyColumn, string TableName, string GridFatherTable, string GridFatherKeyColumn, string strEnvYear, string strEnvMonth)
        {
            //string strSQL = String.Format(" SELECT ID,{0} AS POINT_NAME FROM {1} WHERE {2}='{3}'",KeyColumn,TableName,FatherKeyColumn,FatherKeyValue);
            FatherKeyValue = FatherKeyValue.Replace(",", "','");
            string strSQL = String.Format(" SELECT A.ID,B.{0}+A.{1} AS POINT_NAME,'{2}' AS ENVTYPE_ID,'{3}' AS ENVTYPE_NAME FROM {4} A LEFT JOIN {5} B ON B.ID=A.{6} WHERE B.YEAR='{8}' and B.MONTH='{9}' and B.IS_DEL='0'" + (FatherKeyValue == "" ? "" : " and B.ID IN('{7}')"), GridFatherKeyColumn, KeyColumn, strEnvTypeId, strEnvTypeName, TableName, GridFatherTable, FatherKeyColumn, FatherKeyValue, strEnvYear, strEnvMonth);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取环境质量点位--通用 胡方扬 2013-05-06
        /// </summary>
        /// <param name="strEnvTypeId">环境质量类别ID</param>
        /// <param name="strEnvTypeName">环境质量类别名称</param>
        /// <param name="strYear">年度</param>
        /// <param name="strPoint_Code">点位代码</param>
        /// <param name="strPoint_Name">点位名称</param>
        /// <param name="strPointArea">所属区域</param>
        /// <param name="strPoint_Name">所属河流  可选</param>
        /// <param name="strPointArea">所属流域 可选</param>
        /// <param name="strTableName">主表名称</param>
        /// <param name="strConditionAndValue">筛选条件  格式 USER_NAME='admin'|IS_DEL='0'</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">每页显示几条</param>
        /// <returns></returns>
        public DataTable GetPoint(string strEnvTypeId,string strEnvTypeName,string strYear, string strPoint_Code, string strPoint_Name, string strPointArea,string strRiverArea,string strValleyArea, string strTableName,string strConditionAndValue, int intPageIndex, int intPageSize)
        {
            //何海亮添加，常规污染源
            string strSQL = "";
            if (strEnvTypeId == "EnvPollute" || strEnvTypeId == "SewagePlant" || strEnvTypeId == "StateControlledWastewater" || strEnvTypeId == "StateControlledGas" || strEnvTypeId == "HeavyMetal" || strEnvTypeId == "Control" || strEnvTypeId == "SensitiveGroundwater" || strEnvTypeId == "SewageCharges")
            {
                strSQL = String.Format(@" SELECT A.ID,A.{0} AS YEAR,A.MONTH,A.{1} AS POINT_CODE,A.{2} AS POINT_NAME,B.DICT_TEXT AS POINT_AREA,'{3}' AS ENVTYPE_ID,'{4}' AS ENVTYPE_NAME ,Q.ENTER_NAME,Q.ID", strYear, strPoint_Code, strPoint_Name, strEnvTypeId, strEnvTypeName);
            }
            else
            {
                strSQL = String.Format(@" SELECT A.ID,A.{0} AS YEAR,A.MONTH,A.{1} AS POINT_CODE,A.{2} AS POINT_NAME,B.DICT_TEXT AS POINT_AREA,'{3}' AS ENVTYPE_ID,'{4}' AS ENVTYPE_NAME ", strYear, strPoint_Code, strPoint_Name, strEnvTypeId, strEnvTypeName);
            }

            if (!String.IsNullOrEmpty(strRiverArea))
            {
                strSQL += String.Format(@" ,A.{0} AS RIVER_ID", strRiverArea);
            }
            if (!String.IsNullOrEmpty(strValleyArea))
            {
                strSQL += String.Format(@" ,A.{0} AS VALLEY_ID", strValleyArea);
            }
            strSQL += String.Format(@" FROM {0} A ", strTableName);// add huangjinjun 20150108
            if (!(strEnvTypeId == "EnvPollute" || strEnvTypeId == "SewagePlant" || strEnvTypeId == "StateControlledWastewater" || strEnvTypeId == "StateControlledGas" || strEnvTypeId == "HeavyMetal" || strEnvTypeId == "Control" || strEnvTypeId == "SensitiveGroundwater" || strEnvTypeId == "SewageCharges"))
            {
                strSQL += String.Format(@" LEFT JOIN dbo.T_SYS_DICT B ON B.DICT_CODE=A.{0} AND B.DICT_TYPE='administrative_area'
                WHERE  A.IS_DEL='0' ", strPointArea);
            }
            else
            {
                strSQL += String.Format(@" LEFT JOIN dbo.T_SYS_DICT B ON B.DICT_CODE=A.{0} AND B.DICT_TYPE='administrative_area'
                LEFT JOIN T_ENV_P_POLLUTE_TYPE T ON A.TYPE_ID = T.ID 
                LEFT JOIN T_ENV_P_ENTERINFO Q  on Q.ID = T.SATAIONS_ID
                WHERE Q.REMARK1='" + strEnvTypeId + "' and Q.IS_DEL='0' ", strPointArea);
            }
            if (!String.IsNullOrEmpty(strConditionAndValue))
            {
                string[] strConArr = strConditionAndValue.Split('|');
                if (strConArr.Length > 0)
                {
                    foreach (string str in strConArr)
                    {
                        strSQL += String.Format(" AND {0}", str);
                    }
                }
            }
            //黄进军添加 20150108
            if (strEnvTypeId == "EnvPollute" || strEnvTypeId == "SewagePlant" || strEnvTypeId == "StateControlledWastewater" || strEnvTypeId == "StateControlledGas" || strEnvTypeId == "HeavyMetal" || strEnvTypeId == "Control" || strEnvTypeId == "SensitiveGroundwater" || strEnvTypeId == "SewageCharges")
            {
                strSQL += " order by Q.ENTER_NAME";
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));

//            //string strSQL = String.Format(" SELECT ID,{0} AS YEAR,{1} AS POINT_CODE,{2} AS POINT_NAME,{3} AS POINT_AREA FROM {4} WHERE IS_DEL='0' AND YEAR='{5}'", strYear, strPoint_Code, strPoint_Name, strPointArea, strTableName, DateTime.Now.ToString("yyyy"));
//            //string strSQL = String.Format(@" SELECT A.ID,A.{0} AS YEAR,A.MONTH,A.{1} AS POINT_CODE,A.{2} AS POINT_NAME,B.DICT_TEXT AS POINT_AREA,'{3}' AS ENVTYPE_ID,'{4}' AS ENVTYPE_NAME FROM {5} A ", strYear, strPoint_Code, strPoint_Name,strEnvTypeId,strEnvTypeName, strTableName);
//            string strSQL = "";
//            if (strTableName.Contains("|"))
//                strSQL = String.Format(@" SELECT C.ID,A.{0} AS YEAR,A.MONTH,A.{1} AS POINT_CODE,A.{2} AS POINT_NAME,C.VERTICAL_NAME,B.DICT_TEXT AS POINT_AREA,'{3}' AS ENVTYPE_ID,'{4}' AS ENVTYPE_NAME ", strYear, strPoint_Code, strPoint_Name, strEnvTypeId, strEnvTypeName);
//            else
//                strSQL = String.Format(@" SELECT A.ID,A.{0} AS YEAR,A.MONTH,A.{1} AS POINT_CODE,A.{2} AS POINT_NAME,B.DICT_TEXT AS POINT_AREA,'{3}' AS ENVTYPE_ID,'{4}' AS ENVTYPE_NAME ", strYear, strPoint_Code, strPoint_Name, strEnvTypeId, strEnvTypeName);

//            if (!String.IsNullOrEmpty(strRiverArea)) {
//                strSQL += String.Format(@" ,A.{0} AS RIVER_ID",strRiverArea);
//            }
//            if (!String.IsNullOrEmpty(strValleyArea))
//            {
//                strSQL += String.Format(@" ,A.{0} AS VALLEY_ID", strValleyArea);
//            }
            
//            if (strTableName.Contains("|"))
//            {
//                strSQL += String.Format(@" FROM {0} A ", strTableName.Split('|')[0]);
//                strSQL += String.Format(@" LEFT JOIN {0} C ON A.ID=C.SECTION_ID ", strTableName.Split('|')[1]);
//            }
//            else
//                strSQL += String.Format(@" FROM {0} A ", strTableName);

//            strSQL += String.Format(@" LEFT JOIN dbo.T_SYS_DICT B ON B.DICT_CODE=A.{0} AND B.DICT_TYPE='administrative_area'
//                                        WHERE A.IS_DEL='0' ",strPointArea);
//            if (!String.IsNullOrEmpty(strConditionAndValue))
//            {
//                string[] strConArr = strConditionAndValue.Split('|');
//                if (strConArr.Length > 0) {
//                    foreach (string str in strConArr) {
//                        strSQL += String.Format(" AND {0}", str);
//                    }
//                }
//            }
//            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 获取环境质量点位总条数--通用 胡方扬 2013-05-06
        /// </summary>
        /// <param name="strEnvTypeId">环境质量类别ID</param>
        /// <param name="strEnvTypeName">环境质量类别名称</param>
        /// <param name="strYear">年度</param>
        /// <param name="strPoint_Code">点位代码</param>
        /// <param name="strPoint_Name">点位名称</param>
        /// <param name="strPointArea">所属区域</param>
        /// <param name="strPoint_Name">所属河流  可选</param>
        /// <param name="strPointArea">所属流域 可选</param>
        /// <param name="strTableName">主表名称</param>
        /// <param name="strConditionAndValue">筛选条件  格式 USER_NAME='admin'|IS_DEL='0'</param>
        /// <returns></returns>
        public int GetPointCount(string strEnvTypeId, string strEnvTypeName, string strYear, string strPoint_Code, string strPoint_Name, string strPointArea, string strRiverArea, string strValleyArea, string strTableName, string strConditionAndValue)
        {
            //string strSQL = String.Format(" SELECT ID,{0} AS YEAR,{1} AS POINT_CODE,{2} AS POINT_NAME,{3} AS POINT_AREA FROM {4} WHERE IS_DEL='0' AND YEAR='{5}'", strYear, strPoint_Code, strPoint_Name, strPointArea, strTableName, DateTime.Now.ToString("yyyy"));
            //if (!String.IsNullOrEmpty(strConditionAndValue))
            //{
            //    string[] strConArr = strConditionAndValue.Split('|');
            //    if (strConArr.Length > 0)
            //    {
            //        foreach (string str in strConArr)
            //        {
            //            strSQL += String.Format(" AND {0}", str);
            //        }
            //    }
            //}
            //string strSQL = String.Format(@" SELECT A.ID,A.{0} AS YEAR,A.MONTH,A.{1} AS POINT_CODE,A.{2} AS POINT_NAME,B.DICT_TEXT AS POINT_AREA,'{3}' AS ENVTYPE_ID,'{4}' AS ENVTYPE_NAME FROM {5} A ", strYear, strPoint_Code, strPoint_Name, strEnvTypeId, strEnvTypeName, strTableName);
            string strSQL = String.Format(@" SELECT A.ID,A.{0} AS YEAR,A.MONTH,A.{1} AS POINT_CODE,A.{2} AS POINT_NAME,B.DICT_TEXT AS POINT_AREA,'{3}' AS ENVTYPE_ID,'{4}' AS ENVTYPE_NAME ", strYear, strPoint_Code, strPoint_Name, strEnvTypeId, strEnvTypeName);
            if (!String.IsNullOrEmpty(strRiverArea))
            {
                strSQL += String.Format(@" ,A.{0} AS RIVER_ID", strRiverArea);
            }
            if (!String.IsNullOrEmpty(strValleyArea))
            {
                strSQL += String.Format(@" ,A.{0} AS VALLEY_ID", strValleyArea);
            }
            strSQL += String.Format(@" FROM {0} A ", strTableName);
            strSQL += String.Format(@" LEFT JOIN dbo.T_SYS_DICT B ON B.DICT_CODE=A.{0} AND B.DICT_TYPE='administrative_area'
                                        WHERE A.IS_DEL='0' ", strPointArea);
            if (!String.IsNullOrEmpty(strConditionAndValue))
            {
                string[] strConArr = strConditionAndValue.Split('|');
                if (strConArr.Length > 0)
                {
                    foreach (string str in strConArr)
                    {
                        strSQL += String.Format(" AND {0}", str);
                    }
                }
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 获取环境质量点位--通用 胡方扬 2013-05-06
        /// </summary>
        /// <param name="strEnvTypeId">环境质量类别ID</param>
        /// <param name="strEnvTypeName">环境质量类别名称</param>
        /// <param name="strYear">年度</param>
        /// <param name="strPoint_Code">点位代码</param>
        /// <param name="strPoint_Name">点位名称</param>
        /// <param name="strPointArea">所属区域</param>
        /// <param name="strPoint_Name">所属河流  可选</param>
        /// <param name="strPointArea">所属流域 可选</param>
        /// <param name="strTableName">主表名称</param>
        /// <param name="strConditionAndValue">筛选条件  格式 USER_NAME='admin'|IS_DEL='0'</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">每页显示几条</param>
        /// <returns></returns>
        public DataTable GetPointForSome(string strEnvTypeId, string strEnvTypeName, string strYear, string strPoint_Code, string strPoint_Name, string strRiverArea, string strValleyArea, string strTableName, string strConditionAndValue, int intPageIndex, int intPageSize)
        {
            //string strSQL = String.Format(" SELECT ID,{0} AS YEAR,{1} AS POINT_NAME FROM {2} WHERE IS_DEL='0' AND YEAR='{3}'", strYear, strPoint_Name, strTableName, DateTime.Now.ToString("yyyy"));
            //string strSQL = String.Format(@" SELECT A.ID,A.{0} AS YEAR,A.MONTH,A.{1} AS POINT_CODE,A.{2} AS POINT_NAME,'{3}' AS ENVTYPE_ID,'{4}' AS ENVTYPE_NAME FROM {5} A ", strYear, strPoint_Code, strPoint_Name, strEnvTypeId, strEnvTypeName, strTableName);
            string strSQL = String.Format(@" SELECT A.ID,A.{0} AS YEAR,A.MONTH,A.{1} AS POINT_CODE,A.{2} AS POINT_NAME,'{3}' AS ENVTYPE_ID,'{4}' AS ENVTYPE_NAME ", strYear, strPoint_Code, strPoint_Name, strEnvTypeId, strEnvTypeName);
            if (!String.IsNullOrEmpty(strRiverArea))
            {
                strSQL += String.Format(@" ,A.{0} AS RIVER_ID", strRiverArea);
            }
            if (!String.IsNullOrEmpty(strValleyArea))
            {
                strSQL += String.Format(@" ,A.{0} AS VALLEY_ID", strValleyArea);
            }
            strSQL += String.Format(@" FROM {0} A ", strTableName);

            strSQL += String.Format(@" WHERE A.IS_DEL='0' ");
            if (!String.IsNullOrEmpty(strConditionAndValue))
            {
                string[] strConArr = strConditionAndValue.Split('|');
                if (strConArr.Length > 0)
                {
                    foreach (string str in strConArr)
                    {
                        strSQL += String.Format(" AND {0}", str);
                    }
                }
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, intPageIndex, intPageSize));
        }

        /// <summary>
        /// 获取环境质量点位总条数--通用 胡方扬 2013-05-06
        /// </summary>
        /// <param name="strEnvTypeId">环境质量类别ID</param>
        /// <param name="strEnvTypeName">环境质量类别名称</param>
        /// <param name="strYear">年度</param>
        /// <param name="strPoint_Code">点位代码</param>
        /// <param name="strPoint_Name">点位名称</param>
        /// <param name="strPointArea">所属区域</param>
        /// <param name="strPoint_Name">所属河流  可选</param>
        /// <param name="strPointArea">所属流域 可选</param>
        /// <param name="strTableName">主表名称</param>
        /// <param name="strConditionAndValue">筛选条件  格式 USER_NAME='admin'|IS_DEL='0'</param>
        /// <returns></returns>
        public int GetPointForSomeCount(string strEnvTypeId, string strEnvTypeName, string strYear, string strPoint_Code, string strPoint_Name, string strRiverArea, string strValleyArea, string strTableName, string strConditionAndValue)
        {
            //string strSQL = String.Format(" SELECT ID,{0} AS YEAR,{1} AS POINT_NAME, FROM {2} WHERE IS_DEL='0' AND YEAR='{3}'", strYear, strPoint_Name, strTableName, DateTime.Now.ToString("yyyy"));
            //string strSQL = String.Format(@" SELECT A.ID,A.{0} AS YEAR,A.MONTH,A.{1} AS POINT_CODE,A.{2} AS POINT_NAME,'{3}' AS ENVTYPE_ID,'{4}' AS ENVTYPE_NAME FROM {5} A ", strYear, strPoint_Code, strPoint_Name, strEnvTypeId, strEnvTypeName, strTableName);
            string strSQL = String.Format(@" SELECT A.ID,A.{0} AS YEAR,A.MONTH,A.{1} AS POINT_CODE,A.{2} AS POINT_NAME,'{3}' AS ENVTYPE_ID,'{4}' AS ENVTYPE_NAME ", strYear, strPoint_Code, strPoint_Name, strEnvTypeId, strEnvTypeName);
            if (!String.IsNullOrEmpty(strRiverArea))
            {
                strSQL += String.Format(@" ,A.{0} AS RIVER_ID", strRiverArea);
            }
            if (!String.IsNullOrEmpty(strValleyArea))
            {
                strSQL += String.Format(@" ,A.{0} AS VALLEY_ID", strValleyArea);
            }
            strSQL += String.Format(@" FROM {0} A ", strTableName);

            strSQL += String.Format(@" WHERE A.IS_DEL='0' ");
            if (!String.IsNullOrEmpty(strConditionAndValue))
            {
                string[] strConArr = strConditionAndValue.Split('|');
                if (strConArr.Length > 0)
                {
                    foreach (string str in strConArr)
                    {
                        strSQL += String.Format(" AND {0}", str);
                    }
                }
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        
        /// <summary>
        /// 获取监测计划点位信息 胡方扬
        /// </summary>
        /// <param name="strPlanId"></param>
        /// <returns></returns>
        public DataTable GetPlanEnvListTable(string strPlanId)
        {
            string strSQL = @"select point.*,pointitem.CONTRACT_POINT_ID,pointitem.ITEM_ID,pointitem.CONDITION_ID,pointitem.CONDITION_TYPE,pointitem.ST_UPPER,pointitem.ST_LOWER,
                                            dbo.Rpt_AnalysisManagerID(pointitem.ITEM_ID) as ANALYSIS_MANAGER,
                                            dbo.Rpt_GetAnalysisID(pointitem.ITEM_ID) as ANALYSIS_ID
                                            from T_MIS_CONTRACT_POINT point
                                            left join T_MIS_CONTRACT_POINTITEM pointitem on pointitem.CONTRACT_POINT_ID=point.ID
                                            where point.IS_DEL='0' and point.ID in 
                                            (select CONTRACT_POINT_ID from T_MIS_CONTRACT_PLAN_POINT where PLAN_ID='{0}')";
            strSQL = string.Format(strSQL, strPlanId);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 创建原因：获取指定委托书尚未预约的点位列表
        /// 创建人：胡方扬
        /// 创建日期：2013-07-04
        /// </summary>
        /// <param name="strPointId"></param>
        /// <returns></returns>
        public DataTable GetContractPointCondtion(TMisContractPointVo tMisContractPoint, int iIndex, int iCount)
        {
            string strPointId = tMisContractPoint.ID.Replace(";", "','");
            string strSQL = " SELECT * FROM T_MIS_CONTRACT_POINT WHERE 1=1";
            if (!String.IsNullOrEmpty(strPointId)) {
                strSQL +=String.Format( " AND ID NOT IN('{0}')",strPointId);
            }
            if (!String.IsNullOrEmpty(tMisContractPoint.CONTRACT_ID))
            {
                strSQL += String.Format(" AND CONTRACT_ID ='{0}'", tMisContractPoint.CONTRACT_ID);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL,iIndex,iCount));
        }

        /// <summary>
        /// 创建原因：获取指定监测计划尚未预约的点位列表
        /// 创建人：胡方扬
        /// 创建日期：2013-07-17
        /// </summary>
        /// <param name="strPointId"></param>
        /// <returns></returns>
        public DataTable GetContractPointCondtionForPlanId(string strPlanId,string strPointIdList, int iIndex, int iCount)
        {
            string strPointId = strPointIdList.Replace(";", "','");
            string strSQL = @" SELECT B.*
                                        FROM dbo.T_MIS_CONTRACT_PLAN_POINT A
                                        INNER JOIN dbo.T_MIS_CONTRACT_POINT B ON B.ID=A.CONTRACT_POINT_ID
                                         WHERE 1=1 ";
            if (!String.IsNullOrEmpty(strPlanId))
            {
                strSQL += String.Format(" AND A.PLAN_ID='{0}'", strPlanId);
            }
            if (!String.IsNullOrEmpty(strPointId))
            {
                strSQL += String.Format(" AND ID NOT IN('{0}')", strPointId);
            }

            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }


        /// <summary>
        /// 创建原因：获取指定监测计划尚未预约的点位列表
        /// 创建人：胡方扬
        /// 创建日期：2013-07-17
        /// </summary>
        /// <param name="strPointId"></param>
        /// <returns></returns>
        public int GetContractPointCondtionForPlanIdCount(string strPlanId, string strPointIdList)
        {
            string strPointId = strPointIdList.Replace(";", "','");
            string strSQL = @" SELECT B.*
                                        FROM dbo.T_MIS_CONTRACT_PLAN_POINT A
                                        INNER JOIN dbo.T_MIS_CONTRACT_POINT B ON B.ID=A.CONTRACT_POINT_ID
                                        INNER JOIN dbo.T_MIS_CONTRACT_POINT_FREQ C ON C.ID=A.POINT_FREQ_ID
                                         WHERE 1=1 ";
            if (!String.IsNullOrEmpty(strPlanId))
            {
                strSQL += String.Format(" AND A.PLAN_ID='{0}'", strPlanId);
            }
            if (!String.IsNullOrEmpty(strPointId))
            {
                strSQL += String.Format(" AND ID NOT IN('{0}')", strPointId);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        
        /// <summary>
        /// 创建原因：获取指定委托书尚未预约的点位个数
        /// 创建人：胡方扬
        /// 创建日期：2013-07-04
        /// </summary>
        /// <param name="strPointId"></param>
        /// <returns></returns>
        public int GetContractPointCondtionCount(TMisContractPointVo tMisContractPoint)
        {
            string strPointId = tMisContractPoint.ID.Replace(";", "','");
            string strSQL = " SELECT * FROM T_MIS_CONTRACT_POINT WHERE 1=1";
            if (!String.IsNullOrEmpty(strPointId))
            {
                strSQL += String.Format(" AND ID NOT IN('{0}')", strPointId);
            }
            if (!String.IsNullOrEmpty(tMisContractPoint.CONTRACT_ID))
            {
                strSQL += String.Format(" AND CONTRACT_ID ='{0}'", tMisContractPoint.CONTRACT_ID);
            }

            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }

        /// <summary>
        /// 创建原因：根据计划获取指令性任务尚未预约的点位
        /// 创建人：胡方扬
        /// 创建时间：2013-07-04
        /// </summary>
        /// <param name="strPlanId"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable GetPointListForPlan(string strPlanId,string strPointId,int iIndex,int iCount){
            strPointId = strPointId.Replace(";", "','");
            string strSQL=String.Format(@" SELECT B.* FROM dbo.T_MIS_CONTRACT_PLAN_POINT A
                            LEFT JOIN  dbo.T_MIS_CONTRACT_POINT B ON A.CONTRACT_POINT_ID=B.ID
                            WHERE 1=1");
            if (!String.IsNullOrEmpty(strPointId))
            {
                strSQL += String.Format(" AND B.ID NOT IN('{0}')", strPointId);
            }
            if (!String.IsNullOrEmpty(strPlanId))
            {
                strSQL += String.Format(" AND A.PLAN_ID ='{0}'", strPlanId);
            }
            return SqlHelper.ExecuteDataTable(BuildPagerExpress( strSQL,iIndex,iCount));
        }

        /// <summary>
        /// 创建原因：根据计划获取指令性任务尚未预约的点位
        /// 创建人：胡方扬
        /// 创建时间：2013-07-04
        /// </summary>
        /// <param name="strPlanId"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetPointListForPlanCount(string strPlanId, string strPointId)
        {
            strPointId = strPointId.Replace(";", "','");
            string strSQL = String.Format(@" SELECT B.* FROM dbo.T_MIS_CONTRACT_PLAN_POINT A
                            LEFT JOIN  dbo.T_MIS_CONTRACT_POINT B ON A.CONTRACT_POINT_ID=B.ID
                            WHERE 1=1");
            if (!String.IsNullOrEmpty(strPointId))
            {
                strSQL += String.Format(" AND B.ID NOT IN('{0}')", strPointId);
            }
            if (!String.IsNullOrEmpty(strPlanId))
            {
                strSQL += String.Format(" AND A.PLAN_ID ='{0}'", strPlanId);
            }
            return SqlHelper.ExecuteDataTable(strSQL).Rows.Count;
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tMisContractPoint"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TMisContractPointVo tMisContractPoint)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tMisContractPoint)
            {

                //ID
                if (!String.IsNullOrEmpty(tMisContractPoint.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tMisContractPoint.ID.ToString()));
                }
                //合同ID
                if (!String.IsNullOrEmpty(tMisContractPoint.CONTRACT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CONTRACT_ID = '{0}'", tMisContractPoint.CONTRACT_ID.ToString()));
                }
                //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
                if (!String.IsNullOrEmpty(tMisContractPoint.MONITOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tMisContractPoint.MONITOR_ID.ToString()));
                }
                //基础资料监测点ID
                if (!String.IsNullOrEmpty(tMisContractPoint.POINT_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_ID = '{0}'", tMisContractPoint.POINT_ID.ToString()));
                }
                //监测点名称
                if (!String.IsNullOrEmpty(tMisContractPoint.POINT_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND POINT_NAME = '{0}'", tMisContractPoint.POINT_NAME.ToString()));
                }
                //动态属性ID,从静态数据表拷贝设备信息，必须拷贝动态属性信息
                if (!String.IsNullOrEmpty(tMisContractPoint.DYNAMIC_ATTRIBUTE_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DYNAMIC_ATTRIBUTE_ID = '{0}'", tMisContractPoint.DYNAMIC_ATTRIBUTE_ID.ToString()));
                }
                //建成时间
                if (!String.IsNullOrEmpty(tMisContractPoint.CREATE_DATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CREATE_DATE = '{0}'", tMisContractPoint.CREATE_DATE.ToString()));
                }
                //监测点位置
                if (!String.IsNullOrEmpty(tMisContractPoint.ADDRESS.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADDRESS = '{0}'", tMisContractPoint.ADDRESS.ToString()));
                }
                //经度
                if (!String.IsNullOrEmpty(tMisContractPoint.LONGITUDE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LONGITUDE = '{0}'", tMisContractPoint.LONGITUDE.ToString()));
                }
                //纬度
                if (!String.IsNullOrEmpty(tMisContractPoint.LATITUDE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LATITUDE = '{0}'", tMisContractPoint.LATITUDE.ToString()));
                }
                //采样频次
                if (!String.IsNullOrEmpty(tMisContractPoint.SAMPLE_FREQ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_FREQ = '{0}'", tMisContractPoint.SAMPLE_FREQ.ToString()));
                }
                //监测频次
                if (!String.IsNullOrEmpty(tMisContractPoint.FREQ.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND FREQ = '{0}'", tMisContractPoint.FREQ.ToString()));
                }
                //每次采几个样品
                if (!String.IsNullOrEmpty(tMisContractPoint.SAMPLENUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLENUM = '{0}'", tMisContractPoint.SAMPLENUM.ToString()));
                }
                //点位描述
                if (!String.IsNullOrEmpty(tMisContractPoint.DESCRIPTION.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND DESCRIPTION = '{0}'", tMisContractPoint.DESCRIPTION.ToString()));
                }
                //国标条件项
                if (!String.IsNullOrEmpty(tMisContractPoint.NATIONAL_ST_CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NATIONAL_ST_CONDITION_ID = '{0}'", tMisContractPoint.NATIONAL_ST_CONDITION_ID.ToString()));
                }
                //行标条件项ID
                if (!String.IsNullOrEmpty(tMisContractPoint.INDUSTRY_ST_CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND INDUSTRY_ST_CONDITION_ID = '{0}'", tMisContractPoint.INDUSTRY_ST_CONDITION_ID.ToString()));
                }
                //地标条件项_ID
                if (!String.IsNullOrEmpty(tMisContractPoint.LOCAL_ST_CONDITION_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LOCAL_ST_CONDITION_ID = '{0}'", tMisContractPoint.LOCAL_ST_CONDITION_ID.ToString()));
                }
                //使用状态(0为启用、1为停用)
                if (!String.IsNullOrEmpty(tMisContractPoint.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tMisContractPoint.IS_DEL.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tMisContractPoint.NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND NUM = '{0}'", tMisContractPoint.NUM.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tMisContractPoint.SAMPLE_DAY.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_DAY = '{0}'", tMisContractPoint.SAMPLE_DAY.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tMisContractPoint.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tMisContractPoint.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tMisContractPoint.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tMisContractPoint.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tMisContractPoint.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tMisContractPoint.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tMisContractPoint.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tMisContractPoint.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tMisContractPoint.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tMisContractPoint.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
