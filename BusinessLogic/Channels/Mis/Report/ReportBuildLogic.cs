using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Mis.Report;
using i3.DataAccess.Channels.Mis.Report;
using System.Collections;
using System.Data;

namespace i3.BusinessLogic.Channels.Mis.Report
{
    /// <summary>
    /// 功能描述：报告生成业务逻辑层
    /// 创建日期：2012-12-8
    /// 创建人：  邵世卓
    /// </summary>
    public class ReportBuildLogic : LogicBase
    {

        #region 获得监测情况

        /// <summary>
        /// 获得报告首页信息
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <param name="strItemTypeID">监测类别D</param>
        /// <returns>数据集</returns>
        public DataTable getMonitorTaskInfo(string strTaskID, string strItemTypeID)
        {
            return new ReportTestInfoAccess().getMonitorTaskInfo(strTaskID, strItemTypeID);
        }

        #endregion

        #region 获得监测结果

        /// <summary>
        /// 监测项目信息 项目及父项目
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <param name="strItemTypeID">监测类别ID</param>
        /// <returns>数据集</returns>
        public DataTable SelectItemAndParentItem(string strTaskID, string strItemTypeID)
        {
            return new ReportTestResultAccess().SelectItemAndParentItem(strTaskID, strItemTypeID);
        }

        /// <summary>
        /// 获得监测结果
        /// </summary>
        /// <param name="ContractID">合同ID</param>
        /// <param name="TestType">监测类型</param>
        /// <returns>监测结果</returns>
        public DataTable GetTestResult_ST(string ContractID, string ContractType, string strSampleCode)
        {
            ReportTestResultVo reportTestResult = new ReportTestResultVo();
            reportTestResult.ID = ContractID;
            reportTestResult.TEST_TYPE = ContractType;
            DataTable TableTemp = new ReportTestResultAccess().SelectTable_ST(0, 0, reportTestResult, strSampleCode);

            //判断是否非空
            if (null != TableTemp && TableTemp.Rows.Count > 0)
            {
                return TableTemp;
            }
            else
            {
                return TableTemp;
            }
        }

        /// <summary>
        /// 获得监测结果
        /// </summary>
        /// <param name="ContractID">合同ID</param>
        /// <param name="TestType">监测类型</param>
        /// <returns>监测结果</returns>
        public DataTable GetTestResult_ST(string ContractID, string ContractType, string strItemTypeID, string strSampleCode)
        {
            ReportTestResultVo reportTestResult = new ReportTestResultVo();
            reportTestResult.ID = ContractID;
            reportTestResult.TEST_TYPE = ContractType;
            reportTestResult.TEST_TYPE_NAME = strItemTypeID;
            DataTable TableTemp = new ReportTestResultAccess().SelectTable_ST(0, 0, reportTestResult, strSampleCode);

            //判断是否非空
            if (null != TableTemp && TableTemp.Rows.Count > 0)
            {
                return TableTemp;
            }
            else
            {
                return TableTemp;
            }
        }

        /// <summary>
        /// 取得样品号、排口、性状
        /// </summary>
        /// <param name="strContractID"></param>
        /// <returns></returns>
        public DataTable SelSampleInfoWater_ST(string strTaskID, string mItemTypeID)
        {
            DataTable TableTemp = new ReportTestResultAccess().SelSampleInfoWater_ST(strTaskID, mItemTypeID);
            return TableTemp;
        }

        /// <summary>
        /// 取得样品号、排口、性状 （清远）Create By weilin 2014-03-25
        /// </summary>
        /// <param name="strTaskID">监测任务</param>
        /// <param name="mItemTypeID">类别ID串,逗号分隔,必须有值</param>
        /// <param name="strAttType">性状的类型的ID</param>
        /// <returns></returns>
        public DataTable SelSampleInfoWater(string strTaskID, string mItemTypeID, string strAttType)
        {
            DataTable TableTemp = new ReportTestResultAccess().SelSampleInfoWater(strTaskID, mItemTypeID, strAttType);
            return TableTemp;
        }

        /// <summary>
        /// 取得样品号、排口、性状
        /// </summary>
        /// <param name="strContractID"></param>
        /// <returns></returns>
        public DataTable SelSampleInfoWater_ST_forSendSanple(string strTaskID, string mItemTypeID)
        {
            DataTable TableTemp = new ReportTestResultAccess().SelSampleInfoWater_ST_forSendSanple(strTaskID, mItemTypeID);
            return TableTemp;
        }

        /// <summary>
        /// 取得监测参数
        /// </summary>
        /// <param name="strContractID"></param>
        /// <returns></returns>
        public DataTable SelAttribute(string strTaskID, string mItemTypeID, string strOtherWhere)
        {
            DataTable TableTemp = new ReportTestResultAccess().SelAttribute(strTaskID, mItemTypeID, strOtherWhere);
            return TableTemp;
        }

        #region 监测项目及检出限

        /// <summary>
        /// 功能描述：获得监测项目及检出限
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemInfoForReport(string strTaskID, string strItemTypeID)
        {
            return new ReportTestResultAccess().getItemInfoForReport(strTaskID, strItemTypeID);
        }

        /// <summary>
        /// 功能描述：获得监测项目及是否现场监测项目
        /// 创建时间：2013-4-28
        /// 创建人：潘德军
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemInfoForReport_andIsSampleDept(string strTaskID, string strItemTypeID)
        {
            return new ReportTestResultAccess().getItemInfoForReport_andIsSampleDept(strTaskID, strItemTypeID);
        }

        /// <summary>
        /// 功能描述：获得监测项目及检出限
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>数据集</returns>
        public DataTable getItemInfoForReportQHD(string strTaskID, string strItemTypeID)
        {
            return new ReportTestResultAccess().getItemInfoForReportQHD(strTaskID, strItemTypeID);
        }
        #endregion

        /// <summary>
        /// 功能描述：获得监测报告结果
        /// 创建时间：2012-12-13
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResult(string strTaskID, string strItemTypeID, string mContractType)
        {
            return new ReportTestResultAccess().getSampleResult(strTaskID, strItemTypeID, mContractType);
        }

        /// <summary>
        /// 功能描述：获得监测报告结果（秦皇岛）
        /// 创建时间：2013-01-15
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResultForQHD(string strTaskID, string strItemTypeID)
        {
            return new ReportTestResultAccess().getSampleResultForQHD(strTaskID, strItemTypeID);
        }

        /// <summary>
        /// 功能描述：获得许可证监测报告结果（秦皇岛）
        /// 创建时间：2013-01-15
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResultForLicense(string strTaskID)
        {
            return new ReportTestResultAccess().getSampleResultForLicense(strTaskID);
        }

        /// <summary>
        /// 功能描述：获得验收监测报告结果（秦皇岛）
        /// 创建时间：2013-01-15
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResultForAcceptance(string strTaskID)
        {
            return new ReportTestResultAccess().getSampleResultForAcceptance(strTaskID);
        }

        /// <summary>
        /// 功能描述：获得天气情况
        /// 创建时间：2012-12-15
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <param name="strItemTypeID"></param>
        /// <param name="strWeatherType"></param>
        /// <returns></returns>
        public DataTable getWeatherInfo(string strTaskID, string strItemTypeID, string strWeatherType)
        {
            return new ReportTestInfoAccess().getWeatherInfo(strTaskID, strItemTypeID, strWeatherType);
        }

        /// <summary>
        /// 功能描述：获得样品、点位
        /// 创建时间“2012-12-15
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns>arrlist</returns>
        public DataTable GetSampleInfoSourceByTask(string strTaskID)
        {
            return new ReportTestInfoAccess().GetSampleInfoSourceByTask(strTaskID);
        }

        /// <summary>
        /// 功能描述：获得企业概况
        /// 创建时间“2013-1-17
        /// 创建人：邵世卓
        /// <param name="strTaskID">监测任务ID</param>
        /// <returns></returns>
        public DataTable getCompanyInfo(string strTestCompanyID)
        {
            return new ReportTestInfoAccess().getCompanyInfo(strTestCompanyID);
        }
        #endregion

        #region 2.0方法
        /// <summary>
        /// 功能描述：获得监测报告结果，为2.0制作，不再使用行列转换函数，需要的数据全部取出，在页面linQ
        /// 创建时间：2013-4-27
        /// 创建人：潘德军
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResult_ForV2(string strTaskID, string strItemTypeID, string strWhere)
        {
            return new ReportTestResultAccess().getSampleResult_ForV2(strTaskID, strItemTypeID, strWhere);
        }
        /// <summary>
        /// 功能描述：获得监测报告结果(原始记录表结果)，为2.0制作
        /// 创建时间：2014-9-24
        /// 创建人：魏林
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResult_Dustinfor(string strTaskID, string strItemTypeID, string strWhere)
        {
            return new ReportTestResultAccess().getSampleResult_Dustinfor(strTaskID, strItemTypeID, strWhere);
        }
        #endregion
        #region 2.0方法
        /// <summary>
        /// 功能描述：获得监测报告结果，为2.0制作，不再使用行列转换函数，需要的数据全部取出，在页面linQ
        /// 创建时间：2013-4-27
        /// 创建人：潘德军
        /// 修改人：weilin 2014-04-15
        /// </summary>
        /// <param name="strTaskID"></param>
        /// <returns></returns>
        public DataTable getSampleResult_ForV2Ex(string strTaskID, string strItemTypeID)
        {
            return new ReportTestResultAccess().getSampleResult_ForV2Ex(strTaskID, strItemTypeID);
        }
        #endregion

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            return true;
        }
        public DataTable getItemByTaskID(string strTaskID, string strItemTypeID)
        {
            return new ReportTestResultAccess().getItemByTaskID(strTaskID, strItemTypeID);
        }
        /// <summary>
        /// 获取任务中油烟的信息 Create By:weilin 2014-06-26
        /// </summary>
        /// <param name="strTaskID">监测任务</param>
        /// <returns></returns>
        public DataTable SelAttribute_YY(string strTaskID)
        {
            return new ReportTestResultAccess().SelAttribute_YY(strTaskID);
        }
    }
}
