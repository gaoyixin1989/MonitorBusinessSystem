using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Sample;
using i3.DataAccess.Channels.Mis.Monitor.Sample;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Sample
{
    /// <summary>
    /// 功能：样品表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorSampleInfoLogic : LogicBase
    {

        TMisMonitorSampleInfoVo tMisMonitorSampleInfo = new TMisMonitorSampleInfoVo();
        TMisMonitorSampleInfoAccess access;

        public TMisMonitorSampleInfoLogic()
        {
            access = new TMisMonitorSampleInfoAccess();
        }

        public TMisMonitorSampleInfoLogic(TMisMonitorSampleInfoVo _tMisMonitorSampleInfo)
        {
            tMisMonitorSampleInfo = _tMisMonitorSampleInfo;
            access = new TMisMonitorSampleInfoAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            return access.GetSelectResultCount(tMisMonitorSampleInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleInfoVo Details(string id)
        {
            return access.Details(id);
        }

        public DataTable SelectByTable1(string strResultID)
        {
            return access.SelectByTable1(strResultID);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorSampleInfoVo Details(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            return access.Details(tMisMonitorSampleInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorSampleInfoVo> SelectByObject(TMisMonitorSampleInfoVo tMisMonitorSampleInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorSampleInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorSampleInfoVo tMisMonitorSampleInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorSampleInfo, iIndex, iCount);
        }

        /// <summary>
        /// 获取对象DataTable，点位、质控信息显示
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableForPoint(TMisMonitorSampleInfoVo tMisMonitorSampleInfo, int iIndex, int iCount)
        {
            return access.SelectByTableForPoint(tMisMonitorSampleInfo, iIndex, iCount);
        }
        public int SelectByTableForPointCount(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            return access.SelectByTableForPointCount(tMisMonitorSampleInfo);
        }
        /// <summary>
        /// 获取采样前质控样品信息 by xwh 2013.9.23
        /// </summary>
        /// <param name="strSubTaskId">子任务ID</param>
        /// <param name="strQcSourceId">原始样ID</param>
        /// <returns></returns>
        public DataTable getSampleInfoInQcBeginSampling(string strSubTaskId, string strQcSourceId)
        {
            return access.getSampleInfoInQcBeginSampling(strSubTaskId, strQcSourceId);
        }
        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorSampleInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            return access.SelectByTable(tMisMonitorSampleInfo);
        }
        /// <summary>
        /// 获取子任务中包含有“'氮氧化物','二氧化硫','烟尘'”项目的样品 Create By weilin 2014-04-14
        /// </summary>
        /// <param name="strSubTaskID"></param>
        /// <returns></returns>
        public DataTable SelectByTableForSO2(string strSubTaskID)
        {
            return access.SelectByTableForSO2(strSubTaskID);
        }
        /// <summary> 
        /// 获取样品中包含有“'氮氧化物','二氧化硫','烟尘'”项目的项目结果  Create By weilin 2014-04-14
        /// </summary>
        /// <param name="strSubTaskID"></param>
        /// <returns></returns>
        public DataTable SelectResultForSO2(string strSampleID)
        {
            return access.SelectResultForSO2(strSampleID);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">对象</param>
        /// <returns></returns>
        public TMisMonitorSampleInfoVo SelectByObject(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            return access.SelectByObject(tMisMonitorSampleInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            return access.Create(tMisMonitorSampleInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            return access.Edit(tMisMonitorSampleInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorSampleInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorSampleInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorSampleInfoVo tMisMonitorSampleInfo_UpdateSet, TMisMonitorSampleInfoVo tMisMonitorSampleInfo_UpdateWhere)
        {
            return access.Edit(tMisMonitorSampleInfo_UpdateSet, tMisMonitorSampleInfo_UpdateWhere);
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
        public bool Delete(TMisMonitorSampleInfoVo tMisMonitorSampleInfo)
        {
            return access.Delete(tMisMonitorSampleInfo);
        }

        /// <summary>
        /// 功能描述：根据点位ID获取样品信息
        /// 创建时间：2012-12-6
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strPointId">点位 ID</param>
        /// <returns>样品对象</returns>
        public TMisMonitorSampleInfoVo GetSampleInfoByPointID(string strPointId)
        {
            return access.GetSampleInfoByPointID(strPointId);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息
        /// 创建时间：2013-1-2
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId"></param>
        /// <returns></returns>
        public List<TMisMonitorSampleInfoVo> GetSampleInfoSourceByTask(string strTaskId)
        {
            return access.GetSampleInfoSourceByTask(strTaskId);
        }

        // <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息
        /// 创建时间：2013-4-27
        /// 创建人：潘德军
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strItemType">选择的监测类型ID串</param>
        /// <returns></returns>
        public List<TMisMonitorSampleInfoVo> GetSampleInfoSourceByTask_ByItemType(string strTaskId, string strItemType)
        {
            return access.GetSampleInfoSourceByTask_ByItemType(strTaskId, strItemType);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息
        /// 创建时间：2013-1-2
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public DataTable GetSampleInfoSourceByTask(string strTaskId, string strType, int intPageIndex, int intPageSize)
        {
            return access.GetSampleInfoSourceByTask(strTaskId, strType, intPageIndex, intPageSize);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息总数
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public int GetSampleInfoCountByTask(string strTaskId, string strType)
        {
            return access.GetSampleInfoCountByTask(strTaskId, strType);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息
        /// 创建时间：2013-1-2
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public DataTable GetSampleInfoSourceByTaskForQY_XC(string strSubTaskID, string strTaskId, string strType, int intPageIndex, int intPageSize)
        {
            return access.GetSampleInfoSourceByTaskForQY_XC(strSubTaskID, strTaskId, strType, intPageIndex, intPageSize);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息总数
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public int GetSampleInfoCountByTaskForQY_XC(string strTaskId, string strType)
        {
            return access.GetSampleInfoCountByTaskForQY_XC(strTaskId, strType);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有样样品信息
        /// 创建时间：2013-1-2
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public DataTable GetAllSampleInfoSourceByTask(string strTaskId, string strType)
        {
            return access.GetAllSampleInfoSourceByTask(strTaskId, strType);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息
        /// 创建时间：2013-7-6
        /// 创建人：潘德军
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public DataTable GetSampleInfoSourceByTask_Ex(string strTaskId, string strType, int intPageIndex, int intPageSize)
        {
            return access.GetSampleInfoSourceByTask_Ex(strTaskId, strType, intPageIndex, intPageSize);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有原始样样品信息总数
        /// 创建时间：2013-7-6
        /// 创建人：潘德军
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public int GetSampleInfoCountByTask_Ex(string strTaskId, string strType)
        {
            return access.GetSampleInfoCountByTask_Ex(strTaskId, strType);
        }

        /// <summary>
        /// 功能描述：获得监测任务中所有样样品信息总数
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskId">监测任务ID</param>
        /// <param name="strType">监测类别</param>
        /// <returns></returns>
        public int GetAllSampleInfoCountByTask(string strTaskId, string strType)
        {
            return access.GetAllSampleInfoCountByTask(strTaskId, strType);
        }

        #region 采样任务分配单
        /// <summary>
        /// 获取采样任务分配单信息
        /// </summary>
        /// <param name="strContractType">合同类型</param>
        /// <param name="strMonitorType">监测类别</param>
        /// <param name="strSamplingAskDate">采样日期</param>
        /// <param name="strFlowCode">采样环节代码：duty_sampling</param>
        /// <param name="strCurrentUserId">当前登录用户</param>
        /// <param name="strTaskStatus">任务状态，采样为02</param>
        /// <returns></returns>
        public DataTable getSamplingSheetInfo(string strContractType, string strMonitorType, string strSamplingAskDate, string strFlowCode, string strCurrentUserId, string strTaskStatus)
        {
            return access.getSamplingSheetInfo(strContractType, strMonitorType, strSamplingAskDate, strFlowCode, strCurrentUserId, strTaskStatus);
        }
        /// <summary>
        /// 根据子任务ID获取任务详细信息
        /// </summary>
        /// <param name="strSubTaskId">子任务Id</param>
        /// <param name="strTaskStatus">任务状态，采样为02</param>
        /// <returns></returns>
        public DataTable getSamplingSheetInfoBySubTaskId(string strSubTaskId, string strTaskStatus)
        {
            return access.getSamplingSheetInfoBySubTaskId(strSubTaskId, strTaskStatus);
        }
        #region 样品交接记录表
        /// <summary>
        /// 获取样品交接表信息
        /// </summary>
        /// <param name="strContractType">合同类型</param>
        /// <param name="strMonitorType">监测类别</param>
        /// <param name="strSamplingDate">采样日期</param>
        /// <param name="strSubTaskStatus">子任务状态 分析任务分配：03</param>
        /// <returns></returns>
        public DataTable getSamplingAllocationSheet(string strTaskID, string strMonitorType, string strSubTaskStatus)
        {
            return access.getSamplingAllocationSheet(strTaskID, strMonitorType, strSubTaskStatus);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <param name="strSubTaskID"></param>
        /// <param name="ItemCondition_WithOut">监测项目条件(逗号隔开,默认“1”)：1-现场项目，2-分析类现场项目</param>
        /// <returns></returns>
        public DataTable getSamplingAllocationSheet_MAS(string strTaskID, string strSubTaskID, string ItemCondition_WithOut = "1")
        {
            return access.getSamplingAllocationSheet_MAS(strTaskID, strSubTaskID, ItemCondition_WithOut);
        }
        /// <summary>
        /// 样品交接-样品相关信息
        /// </summary>
        /// <param name="strContractType">合同类型</param>
        /// <param name="strMonitorType">监测类别</param>
        /// <param name="strSamplingDate">采样日期</param>
        /// <param name="strSubTaskStatus">子任务状态 分析任务分配：03</param>
        /// <returns></returns>
        public DataTable getSamplingAllocationSheet_QHD(string strTaskID, string strMonitorType, string strSubTaskStatus)
        {
            return access.getSamplingAllocationSheet_QHD(strTaskID, strMonitorType, strSubTaskStatus);
        }

        /// <summary>
        /// 获取样品交接表信息
        /// </summary>
        /// <param name="strContractType">合同类型</param>
        /// <param name="strMonitorType">监测类别</param>
        /// <param name="strSamplingDate">采样日期</param>
        /// <param name="strSubTaskStatus">子任务状态 分析任务分配：03</param>
        /// <returns></returns>
        public DataTable getSamplingAllocationSheetInfo(string strContractType, string strMonitorType, string strSamplingDate, string strSubTaskStatus)
        {
            return access.getSamplingAllocationSheetInfo(strContractType, strMonitorType, strSamplingDate, strSubTaskStatus);
        }
        /// <summary>
        /// 根据样品信息获取样品交接表信息
        /// </summary>
        /// <param name="strSampleIds">样品ID</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <returns></returns>
        public DataTable getSamplingAllocationSheetInfoBySampleId(string strSampleIds, string strSubTaskStatus, string strNOSAMPLE)
        {
            return access.getSamplingAllocationSheetInfoBySampleId(strSampleIds, strSubTaskStatus, strNOSAMPLE);
        }
        /// <summary>
        /// 更新样品交接表样品打印状态
        /// </summary>
        /// <param name="strSampleIds">样品状态</param>
        /// <param name="strSubTaskStatus">子任务状态</param>
        /// <param name="strPrintedStatus">打印状态</param>
        /// <returns></returns>
        public bool updateSamplingAllocationSheetInfoStatus(string strSampleIds, string strSubTaskStatus, string strPrintedStatus)
        {
            return access.updateSamplingAllocationSheetInfoStatus(strSampleIds, strSubTaskStatus, strPrintedStatus);
        }
        #endregion
        #endregion

        /// <summary>
        /// 功能描述：获取监测任务的某种监测类型样品名称
        /// 创建时间：2013-1-19
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <param name="strType"></param>
        /// <returns></returns>
        public DataTable GetSampleInfoSourceByTaskForLicense(string strTaskID, string strType)
        {
            return access.GetSampleInfoSourceByTaskForLicense(strTaskID, strType);
        }

        /// <summary>
        /// 功能描述：获取现场监测项目所属的样品信息
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <param name="strTaskStatus">子任务状态</param>
        /// <param name="intPageIndex">分页码</param>
        /// <param name="intPageSize">总页数</param>
        /// <returns></returns>
        public DataTable getSamplingForSampleItem(string strSubTaksId, string strTaskStatus, int intPageIndex, int intPageSize)
        {
            return access.getSamplingForSampleItem(strSubTaksId, strTaskStatus, intPageIndex, intPageSize);
        }

        /// <summary>
        /// 功能描述：获取现场监测项目所属的样品信息总数
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <param name="strTaskStatus">子任务状态</param>
        /// <returns></returns>
        public int getSamplingCountForSampleItem(string strSubTaksId, string strTaskStatus)
        {
            return access.getSamplingCountForSampleItem(strSubTaksId, strTaskStatus);
        }

        public DataTable getSamplingForSampleItem_MAS(string strSubTaksId, int intPageIndex, int intPageSize)
        {
            return access.getSamplingForSampleItem_MAS(strSubTaksId, intPageIndex, intPageSize);
        }
        public int getSamplingCountForSampleItem_MAS(string strSubTaksId)
        {
            return access.getSamplingCountForSampleItem_MAS(strSubTaksId);
        }

        /// <summary>
        /// 功能描述：获取现场监测项目所属的样品信息
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <param name="strTaskStatus">子任务状态</param>
        /// <param name="intPageIndex">分页码</param>
        /// <param name="intPageSize">总页数</param>
        /// <returns></returns>
        public DataTable getSamplingForSampleItemOne(string strSubTaksId, string strTaskStatus, int intPageIndex, int intPageSize)
        {
            return access.getSamplingForSampleItemOne(strSubTaksId, strTaskStatus, intPageIndex, intPageSize);
        }

        /// <summary>
        /// 功能描述：获取现场监测项目所属的样品信息总数
        /// 创建时间：2013-3-14
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <param name="strTaskStatus">子任务状态</param>
        /// <returns></returns>
        public int getSamplingCountForSampleItemOne(string strSubTaksId, string strTaskStatus)
        {
            return access.getSamplingCountForSampleItemOne(strSubTaksId, strTaskStatus);
        }

        /// <summary>
        /// 功能描述：获取有现场监测项目的监测点位
        /// 创建时间：2015-04-25
        /// 创建人：黄进军
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <returns></returns>
        public DataTable SelectSampleSitePoint(string strSubTaksId)
        {
            return access.SelectSampleSitePoint(strSubTaksId);
        }

        /// <summary>
        /// 功能描述：获取有现场监测项目的监测点位总数
        /// 创建时间：2015-04-25
        /// 创建人：黄进军
        /// </summary>
        /// <param name="strSubTaksId">子任务ID</param>
        /// <returns></returns>
        public int SelectSampleSitePointCount(string strSubTaksId)
        {
            return access.SelectSampleSitePointCount(strSubTaksId);
        }

        public DataTable getSamplingForSampleItemOne_MAS(string strSubTaksId, int intPageIndex, int intPageSize)
        {
            return access.getSamplingForSampleItemOne_MAS(strSubTaksId, intPageIndex, intPageSize);
        }
        public int getSamplingCountForSampleItemOne_MAS(string strSubTaksId)
        {
            return access.getSamplingCountForSampleItemOne_MAS(strSubTaksId);
        }

        /// <summary>
        /// 获取同一子任务下同一监测类别样品已编码数量
        /// </summary>
        /// <param name="tMisMonitorSampleInfo"></param>
        /// <param name="strMonitorId"></param>
        /// <returns></returns>
        public int GetMonitorTypeCountForSubTask(TMisMonitorSampleInfoVo tMisMonitorSampleInfo, string strMonitorId)
        {
            return access.GetMonitorTypeCountForSubTask(tMisMonitorSampleInfo, strMonitorId);
        }

        /// <summary>
        /// 创建原因：根据监测任务点位 获取环境质量当前年度当前月份的质控计划列表
        /// 创建人：胡方扬
        /// 创建日期：2013-06-26
        /// </summary>
        /// <param name="strTaskPointId"></param>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <returns></returns>
        public DataTable GetSampleInforForEnvQcSettingTable(string strTaskPointId, string strYear, string strMonth)
        {
            return access.GetSampleInforForEnvQcSettingTable(strTaskPointId, strYear, strMonth);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorSampleInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //监测子任务ID
            if (tMisMonitorSampleInfo.SUBTASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测子任务ID不能为空");
                return false;
            }
            //点位
            if (tMisMonitorSampleInfo.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("点位不能为空");
                return false;
            }
            //样品号
            if (tMisMonitorSampleInfo.SAMPLE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("样品号不能为空");
                return false;
            }
            //样品类型
            if (tMisMonitorSampleInfo.SAMPLE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("样品类型不能为空");
                return false;
            }
            //样品名称
            if (tMisMonitorSampleInfo.SAMPLE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("样品名称不能为空");
                return false;
            }
            //样品数量
            if (tMisMonitorSampleInfo.SAMPLE_COUNT.Trim() == "")
            {
                this.Tips.AppendLine("样品数量不能为空");
                return false;
            }
            //状态
            if (tMisMonitorSampleInfo.STATUS.Trim() == "")
            {
                this.Tips.AppendLine("状态不能为空");
                return false;
            }
            //未采样
            if (tMisMonitorSampleInfo.NOSAMPLE.Trim() == "")
            {
                this.Tips.AppendLine("未采样不能为空");
                return false;
            }
            //未采样说明
            if (tMisMonitorSampleInfo.NOSAMPLEREMARK.Trim() == "")
            {
                this.Tips.AppendLine("未采样说明不能为空");
                return false;
            }
            //质控类型（现场空白、现场加标、现场平行、实验室密码平行，实验室空白、实验室加标、实验室明码平行、标准样）
            if (tMisMonitorSampleInfo.QC_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("质控类型（现场空白、现场加标、现场平行、实验室密码平行，实验室空白、实验室加标、实验室明码平行、标准样）不能为空");
                return false;
            }
            //质控原始样ID
            if (tMisMonitorSampleInfo.QC_SOURCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("质控原始样ID不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorSampleInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorSampleInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorSampleInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorSampleInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorSampleInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }
            //
            if (tMisMonitorSampleInfo.D_SOURCE.Trim() == "")
            {
                this.Tips.AppendLine("昼间主要声源不能为空");
                return false;
            }
            //
            if (tMisMonitorSampleInfo.N_SOURCE.Trim() == "")
            {
                this.Tips.AppendLine("夜间主要声源不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取计划任务的所有样品信息 Create By: weilin 2014-03-18
        /// </summary>
        /// <param name="strPlanID"></param>
        /// <returns></returns>
        public DataTable GetSampleInfoByPlanID(string strPlanID)
        {
            return access.GetSampleInfoByPlanID(strPlanID);
        }
        /// <summary>
        /// 获取样品的监测项目信息 Create By: weilin 2014-03-18
        /// </summary>
        /// <param name="strSampleID"></param>
        /// <returns></returns>
        public DataTable GetItemInfoBySampleID(string strSampleID)
        {
            return access.GetItemInfoBySampleID(strSampleID);
        }

        public bool UpdateSampleCell(string ID, string strCellName, string strCellValue)
        {
            return access.UpdateSampleCell(ID, strCellName, strCellValue);
        }
        public bool UpdateSetWhere(string strTableName, string strSet, string strWhere)
        {
            return access.UpdateSetWhere(strTableName, strSet, strWhere);
        }

        public string GetPlanID(string strSampleID)
        {
            return access.GetPlanID(strSampleID);
        }
    }
}
