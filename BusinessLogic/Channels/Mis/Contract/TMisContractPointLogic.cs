using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Contract;
using i3.DataAccess.Channels.Mis.Contract;

namespace i3.BusinessLogic.Channels.Mis.Contract
{
    /// <summary>
    /// 功能：委托书监测点信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractPointLogic : LogicBase
    {

        TMisContractPointVo tMisContractPoint = new TMisContractPointVo();
        TMisContractPointAccess access;

        public TMisContractPointLogic()
        {
            access = new TMisContractPointAccess();
        }

        public TMisContractPointLogic(TMisContractPointVo _tMisContractPoint)
        {
            tMisContractPoint = _tMisContractPoint;
            access = new TMisContractPointAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractPoint">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractPointVo tMisContractPoint)
        {
            return access.GetSelectResultCount(tMisContractPoint);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractPointVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractPoint">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractPointVo Details(TMisContractPointVo tMisContractPoint)
        {
            return access.Details(tMisContractPoint);
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
            return access.SelectByObject(tMisContractPoint, iIndex, iCount);

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
            return access.SelectByTable(tMisContractPoint, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractPoint"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractPointVo tMisContractPoint)
        {
            return access.SelectByTable(tMisContractPoint);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractPoint">对象</param>
        /// <returns></returns>
        public TMisContractPointVo SelectByObject(TMisContractPointVo tMisContractPoint)
        {
            return access.SelectByObject(tMisContractPoint);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractPointVo tMisContractPoint)
        {
            return access.Create(tMisContractPoint);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPoint">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointVo tMisContractPoint)
        {
            return access.Edit(tMisContractPoint);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractPoint_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractPoint_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractPointVo tMisContractPoint_UpdateSet, TMisContractPointVo tMisContractPoint_UpdateWhere)
        {
            return access.Edit(tMisContractPoint_UpdateSet, tMisContractPoint_UpdateWhere);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            return access.Delete(Id);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TMisContractPointVo tMisContractPoint)
        {
            return access.Delete(tMisContractPoint);
        }


        /// <summary>
        /// 复制企业监测点位信息到委托企业点位信息临时表  Create By Castle(胡方扬) 2012-11-30
        /// </summary>
        /// <param name="strContractId">委托书ID</param>
        /// <param name="strCompanyPointId">企业点位ID</param>
        /// <returns>True Or False</returns>
        public bool InsertContratPoint(string strContractId, string strCompanyPointId)
        {
            return access.InsertContratPoint(strContractId, strCompanyPointId);
        }
                /// <summary>
        /// 复制企业监测点位信息到委托企业点位信息临时表  Create By Castle(胡方扬) 2013-07-19
        /// </summary>
        /// <param name="strID">委托书点位ID</param>
        /// <param name="strCompanyPointId">企业点位ID</param>
        /// <returns>True Or False</returns>
        public bool InsertContratPointForPlan(string strID, string strCompanyPointId)
        {
            return access.InsertContratPointForPlan(strID, strCompanyPointId);
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
            return access.InsertPointsInfor(objTable, strContractId, strPlanId, Company_Names, strReqContractType, ref New_ID);
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
            return access.SelectByTableForPlan(strPlanID);
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
            return access.SelectPointTable(strPlanID);
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
            return access.GetTestType(strPlanID);
        }

                /// <summary>
        /// 插入环境质量点位及其相关点位项目
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public bool InsertEnvPoints(TMisContractPointVo tMisContractPoint, string PlanId, string KeyCloumns, string TableName)
        {
            return access.InsertEnvPoints(tMisContractPoint, PlanId,KeyCloumns,TableName);
        }
        /// <summary>
        /// 插入环境质量点位及其相关点位、垂线、监测项目,并返回相关的点位信息（扩展）Create By : weilin 2014-11-14
        /// </summary>
        /// <param name="tMisContractPointitem"></param>
        /// <returns></returns>
        public DataTable InsertEnvPointsEx(DataTable dtEnv, string PlanId, string TypeId, string KeyCloumns, string TableName, string strEnvTypeName)
        {
            return access.InsertEnvPointsEx(dtEnv, PlanId, TypeId, KeyCloumns, TableName, strEnvTypeName);
        }
        /// <summary>
        /// 获取常规监测任务的点位信息 Create By : weilin 2014-11-15
        /// </summary>
        /// <param name="PlanId"></param>
        /// <param name="TypeId"></param>
        /// <returns></returns>
        public DataTable GetPlanPoints(string PlanId, string TypeId)
        {
            return access.GetPlanPoints(PlanId, TypeId);
        }
         /// <summary>
        /// 获取环境质量点位--通用 胡方扬 2013-05-06
        /// </summary>
        /// <param name="FatherKeyColumn"></param>
        /// <param name="FatherKeyValue"></param>
        /// <param name="KeyColumn"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GetPoint(string strEnvTypeId, string strEnvTypeName, string strYear, string strPoint_Code, string strPoint_Name, string strPointArea, string strRiverArea, string strValleyArea, string strTableName, string strConditionAndValue, int intPageIndex, int intPageSize)
        {
            return access.GetPoint(strEnvTypeId, strEnvTypeName, strYear, strPoint_Code, strPoint_Name, strPointArea, strRiverArea, strValleyArea, strTableName, strConditionAndValue, intPageIndex, intPageSize);
        }

        /// <summary>
        /// 获取环境质量点位条数--通用 胡方扬 2013-05-06
        /// </summary>
        /// <param name="FatherKeyColumn"></param>
        /// <param name="FatherKeyValue"></param>
        /// <param name="KeyColumn"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public int GetPointCount(string strEnvTypeId, string strEnvTypeName, string strYear, string strPoint_Code, string strPoint_Name, string strPointArea, string strRiverArea, string strValleyArea, string strTableName, string strConditionAndValue)
        {
            return access.GetPointCount(strEnvTypeId, strEnvTypeName, strYear, strPoint_Code, strPoint_Name, strPointArea, strRiverArea, strValleyArea, strTableName, strConditionAndValue);
        }

                /// <summary>
        /// 获取环境质量点位--通用 胡方扬 2013-05-07
        /// </summary>
        /// <param name="FatherKeyColumn"></param>
        /// <param name="FatherKeyValue"></param>
        /// <param name="KeyColumn"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GetPointForSome(string strEnvTypeId, string strEnvTypeName, string strYear, string strPoint_Code, string strPoint_Name, string strRiverArea, string strValleyArea, string strTableName, string strConditionAndValue, int intPageIndex, int intPageSize)
        {
            return access.GetPointForSome(strEnvTypeId,strEnvTypeName, strYear, strPoint_Code,strPoint_Name,strRiverArea, strValleyArea, strTableName,strConditionAndValue,intPageIndex,intPageSize);
        }

         /// <summary>
        /// 获取环境质量点位--通用 胡方扬 2013-05-07
        /// </summary>
        /// <param name="FatherKeyColumn"></param>
        /// <param name="FatherKeyValue"></param>
        /// <param name="KeyColumn"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public int GetPointForSomeCount(string strEnvTypeId, string strEnvTypeName, string strYear, string strPoint_Code, string strPoint_Name,string strRiverArea, string strValleyArea, string strTableName, string strConditionAndValue)
        {
            return access.GetPointForSomeCount(strEnvTypeId, strEnvTypeName, strYear, strPoint_Code, strPoint_Name, strRiverArea, strValleyArea, strTableName, strConditionAndValue);
        }
        /// <summary>
        /// 动态表获取不同的环境质量点位
        /// </summary>
        /// <param name="FatherKeyColumn"></param>
        /// <param name="FatherKeyValue"></param>
        /// <param name="KeyColumn"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public DataTable GetEnvInforForDefineTable(string strEnvTypeId, string strEnvTypeName, string FatherKeyColumn, string FatherKeyValue, string SubKeyColumn, string TableName, string strEnvYear, string strEnvMonth)
        {
            return access.GetEnvInforForDefineTable(strEnvTypeId,strEnvTypeName,FatherKeyColumn, FatherKeyValue, SubKeyColumn, TableName, strEnvYear, strEnvMonth);
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
            return access.GetEnvInforForDefineTable(strEnvTypeId,strEnvTypeName,FatherKeyColumn, FatherKeyValue, KeyColumn, TableName, GridFatherTable, GridFatherKeyColumn, strEnvYear, strEnvMonth);
        }
        /// <summary>
        /// 获取监测计划点位信息
        /// </summary>
        /// <param name="strPlanId"></param>
        /// <returns></returns>
        public DataTable GetPlanEnvListTable(string strPlanId) {
            return access.GetPlanEnvListTable(strPlanId);
        }
                /// <summary>
        /// 创建原因：取指定委托书尚未预约的点位列表
        /// 创建人：胡方扬
        /// 创建日期：2013-07-04
        /// </summary>
        /// <param name="strPointId"></param>
        /// <returns></returns>
        public DataTable GetContractPointCondtion(TMisContractPointVo tMisContractPoint, int iIndex, int iCount)
        {
            return access.GetContractPointCondtion(tMisContractPoint, iIndex, iCount);
        }

        /// <summary>
        /// 创建原因：获取指定监测计划尚未预约的点位列表
        /// 创建人：胡方扬
        /// 创建日期：2013-07-17
        /// </summary>
        /// <param name="strPointId"></param>
        /// <returns></returns>
        public DataTable GetContractPointCondtionForPlanId(string strPlanId, string strPointIdList, int iIndex, int iCount)
        {
            return access.GetContractPointCondtionForPlanId(strPlanId, strPointIdList, iIndex, iCount);
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
            return access.GetContractPointCondtionForPlanIdCount(strPlanId, strPointIdList);
        }
                /// <summary>
        /// 创建原因：取指定委托书尚未预约的点位个数
        /// 创建人：胡方扬
        /// 创建日期：2013-07-04
        /// </summary>
        /// <param name="strPointId"></param>
        /// <returns></returns>
        public int GetContractPointCondtionCount(TMisContractPointVo tMisContractPoint)
        {
            return access.GetContractPointCondtionCount(tMisContractPoint);
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
        public DataTable GetPointListForPlan(string strPlanId, string strPointId, int iIndex, int iCount) {
            return access.GetPointListForPlan(strPlanId, strPointId, iIndex, iCount);
        }

        /// <summary>
        /// 创建原因：根据计划获取指令性任务尚未预约的点位个数
        /// 创建人：胡方扬
        /// 创建时间：2013-07-04
        /// </summary>
        /// <param name="strPlanId"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetPointListForPlanCount(string strPlanId, string strPointId)
        {
            return access.GetPointListForPlanCount(strPlanId, strPointId);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisContractPoint.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //合同ID
            if (tMisContractPoint.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("合同ID不能为空");
                return false;
            }
            //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
            if (tMisContractPoint.MONITOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）不能为空");
                return false;
            }
            //监测点名称
            if (tMisContractPoint.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("监测点名称不能为空");
                return false;
            }
            //动态属性ID,从静态数据表拷贝设备信息，必须拷贝动态属性信息
            if (tMisContractPoint.DYNAMIC_ATTRIBUTE_ID.Trim() == "")
            {
                this.Tips.AppendLine("动态属性ID,从静态数据表拷贝设备信息，必须拷贝动态属性信息不能为空");
                return false;
            }
            //建成时间
            if (tMisContractPoint.CREATE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("建成时间不能为空");
                return false;
            }
            //监测点位置
            if (tMisContractPoint.ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("监测点位置不能为空");
                return false;
            }
            //经度
            if (tMisContractPoint.LONGITUDE.Trim() == "")
            {
                this.Tips.AppendLine("经度不能为空");
                return false;
            }
            //纬度
            if (tMisContractPoint.LATITUDE.Trim() == "")
            {
                this.Tips.AppendLine("纬度不能为空");
                return false;
            }
            //监测频次
            if (tMisContractPoint.FREQ.Trim() == "")
            {
                this.Tips.AppendLine("监测频次不能为空");
                return false;
            }
            //点位描述
            if (tMisContractPoint.DESCRIPTION.Trim() == "")
            {
                this.Tips.AppendLine("点位描述不能为空");
                return false;
            }
            //国标条件项
            if (tMisContractPoint.NATIONAL_ST_CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("国标条件项不能为空");
                return false;
            }
            //行标条件项ID
            if (tMisContractPoint.INDUSTRY_ST_CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("行标条件项ID不能为空");
                return false;
            }
            //地标条件项_ID
            if (tMisContractPoint.LOCAL_ST_CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("地标条件项_ID不能为空");
                return false;
            }
            //使用状态(0为启用、1为停用)
            if (tMisContractPoint.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
            //序号
            if (tMisContractPoint.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tMisContractPoint.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisContractPoint.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisContractPoint.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisContractPoint.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisContractPoint.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
