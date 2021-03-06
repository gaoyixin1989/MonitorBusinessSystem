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
    /// 功能：委托书信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractLogic : LogicBase
    {

        TMisContractVo tMisContract = new TMisContractVo();
        TMisContractAccess access;

        public TMisContractLogic()
        {
            access = new TMisContractAccess();
        }

        public TMisContractLogic(TMisContractVo _tMisContract)
        {
            tMisContract = _tMisContract;
            access = new TMisContractAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractVo tMisContract)
        {
            return access.GetSelectResultCount(tMisContract);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContract">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractVo Details(TMisContractVo tMisContract)
        {
            return access.Details(tMisContract);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractVo> SelectByObject(TMisContractVo tMisContract, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContract, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractVo tMisContract, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContract, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractVo tMisContract)
        {
            return access.SelectByTable(tMisContract);
        }

        /// <summary>
        /// 获取对象DataTable Create By : weilin 2014-09-17
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableEx(TMisContractVo tMisContract, string strStatus, int iIndex, int iCount)
        {
            return access.SelectByTableEx(tMisContract, strStatus, iIndex, iCount);
        }
        /// <summary>
        /// 获得查询结果总行数，用于分页 Create By : weilin 2014-09-17
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountEx(TMisContractVo tMisContract, string strStatus)
        {
            return access.GetSelectResultCountEx(tMisContract, strStatus);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <returns></returns>
        public TMisContractVo SelectByObject(TMisContractVo tMisContract)
        {
            return access.SelectByObject(tMisContract);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractVo tMisContract)
        {
            return access.Create(tMisContract);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContract">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractVo tMisContract)
        {
            return access.Edit(tMisContract);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContract_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContract_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractVo tMisContract_UpdateSet, TMisContractVo tMisContract_UpdateWhere)
        {
            return access.Edit(tMisContract_UpdateSet, tMisContract_UpdateWhere);
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
        public bool Delete(TMisContractVo tMisContract)
        {
            return access.Delete(tMisContract);
        }

        #region 自定义查询
        /// <summary>
        /// 自定义查询 总数
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="strDutyCode">任务单号</param>
        /// <param name="strReportCode">报告单号</param>
        /// <returns>总数</returns>
        public int GetSelectResultCountForSearch(TMisContractVo tMisContract, string strDutyCode, string strReportCode)
        {
            return access.GetSelectResultCountForSearch(tMisContract, strDutyCode, strReportCode);
        }

        /// <summary>
        /// 自定义查询 数据集
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="strDutyCode">任务单号</param>
        /// <param name="strReportCode">报考单号</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">单页显示数</param>
        /// <returns>数据集</returns>
        public DataTable SelectByTableForSearch(TMisContractVo tMisContract, string strDutyCode, string strReportCode, int intPageIndex, int intPageSize)
        {
            return access.SelectByTableForSearch(tMisContract, strDutyCode, strReportCode, intPageIndex, intPageSize);
        }
        #endregion

        #region 自定义查询委托书列表 胡方扬 2012-12-06
        /// <summary>
        /// 自定义查询 总数
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <returns>总数</returns>
        public int GetSelectResultCountForSearchList(TMisContractVo tMisContract)
        {
            return access.GetSelectResultCountForSearchList(tMisContract);
        }

        /// <summary>
        /// 自定义查询 数据集
        /// </summary>
        /// <param name="tMisContract">对象</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">单页显示数</param>
        /// <returns>数据集</returns>
        public DataTable SelectByTableForSearchList(TMisContractVo tMisContract,int intPageIndex, int intPageSize)
        {
            return access.SelectByTableForSearchList(tMisContract, intPageIndex, intPageSize);
        }
         /// 获取委托书和受检企业关联信息条目数
        public DataTable SelectDefineTableContractResult(TMisContractVo tMisContract, string strCompanyName, string strArea)
        {
            return access.SelectDefineTableContractResult(tMisContract, strCompanyName,strArea);
        }
       /// 获取委托书和受检企业关联信息
        public DataTable SelectDefineTableContract(TMisContractVo tMisContract,string strCompanyName,string strArea,int intPageIndex, int intPageSize)
        {
            return access.SelectDefineTableContract(tMisContract, strCompanyName, strArea, intPageIndex, intPageSize);
        }
        /// <summary>
        /// 获取委托书列表关联获取委托、受检企业信息 Create By weilin 2014-10-16
        /// </summary>
        /// <returns></returns>
        public DataTable SelectAcceptContract(string strContractID)
        {
            return access.SelectAcceptContract(strContractID);
        }

        //获取委托书和受检企业关联信息
        public DataTable SelectByUnionTable(TMisContractVo tMisContract, int intPageIndex, int intPageSize)
        {
            return access.SelectByUnionTable(tMisContract, intPageIndex, intPageSize);
        }
        //获取委托书和委托企业关联信息委托书总数
        public int SelectByUnionTableResult(TMisContractVo tMisContract) {
            return access.SelectByUnionTableResult(tMisContract);
        }

        //获取自送样委托书监测计划信息
        public DataTable GetContractInforUnionSamplePlan(TMisContractVo tMisContract,TMisContractSamplePlanVo tMisContractSamplePlan) { 
        return access.GetContractInforUnionSamplePlan(tMisContract,tMisContractSamplePlan);
        }
         /// <summary>
        /// 获取非快捷模式委托书的总计数,用于该类委托书单号生成
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public int GetContractCodeCount(TMisContractVo tMisContract) {
            return access.GetContractCodeCount(tMisContract);
        }
                /// <summary>
        /// 获取委托书导出数据 胡方扬 2013-04-23
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable GetExportInforData(TMisContractVo tMisContract) {
            return access.GetExportInforData(tMisContract);
        }
        /// <summary>
        /// 获取委托书费用明细导出数据 魏林 2014-02-25(清远)
        /// </summary>
        /// <param name="tMisContract"></param>
        /// <returns></returns>
        public DataTable GetContractFreeData(TMisContractVo tMisContract)
        {
            return access.GetContractFreeData(tMisContract);
        }
        /// <summary>
        /// 获取委托书附加费用信息 魏林 2014-02-25
        /// </summary>
        /// <param name="strContractID"></param>
        /// <returns></returns>
        public DataTable GetContractAttFeeData(string strContractID)
        {
            return access.GetContractAttFeeData(strContractID);
        }
        #endregion


        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisContract.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //合同编号
            if (tMisContract.CONTRACT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("合同编号不能为空");
                return false;
            }
            //委托年度
            if (tMisContract.CONTRACT_YEAR.Trim() == "")
            {
                this.Tips.AppendLine("委托年度不能为空");
                return false;
            }
            //项目名称
            if (tMisContract.PROJECT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("项目名称不能为空");
                return false;
            }
            //委托类型
            if (tMisContract.CONTRACT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("委托类型不能为空");
                return false;
            }
            //监测类型,冗余字段，如：废水,废气,噪声
            if (tMisContract.TEST_TYPES.Trim() == "")
            {
                this.Tips.AppendLine("监测类型,冗余字段，如：废水,废气,噪声不能为空");
                return false;
            }
            //报告类型
            if (tMisContract.TEST_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("报告类型不能为空");
                return false;
            }
            //监测目的
            if (tMisContract.TEST_PURPOSE.Trim() == "")
            {
                this.Tips.AppendLine("监测目的不能为空");
                return false;
            }
            //委托企业ID
            if (tMisContract.CLIENT_COMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托企业ID不能为空");
                return false;
            }
            //受检企业ID
            if (tMisContract.TESTED_COMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("受检企业ID不能为空");
                return false;
            }
            //要求完成日期
            if (tMisContract.ASKING_DATE.Trim() == "")
            {
                this.Tips.AppendLine("要求完成日期不能为空");
                return false;
            }
            //报告领取方式
            if (tMisContract.RPT_WAY.Trim() == "")
            {
                this.Tips.AppendLine("报告领取方式不能为空");
                return false;
            }
            //是否同意分包
            if (tMisContract.AGREE_OUTSOURCING.Trim() == "")
            {
                this.Tips.AppendLine("是否同意分包不能为空");
                return false;
            }
            //是否同意使用的监测方法
            if (tMisContract.AGREE_METHOD.Trim() == "")
            {
                this.Tips.AppendLine("是否同意使用的监测方法不能为空");
                return false;
            }
            //是否同意使用非标准方法
            if (tMisContract.AGREE_NONSTANDARD.Trim() == "")
            {
                this.Tips.AppendLine("是否同意使用非标准方法不能为空");
                return false;
            }
            //是否同意其他
            if (tMisContract.AGREE_OTHER.Trim() == "")
            {
                this.Tips.AppendLine("是否同意其他不能为空");
                return false;
            }
            //样品来源,1,抽样，2，自送样
            if (tMisContract.SAMPLE_SOURCE.Trim() == "")
            {
                this.Tips.AppendLine("样品来源,1,抽样，2，自送样不能为空");
                return false;
            }
            //送样人
            if (tMisContract.SAMPLE_SEND_MAN.Trim() == "")
            {
                this.Tips.AppendLine("送样人不能为空");
                return false;
            }
            //接样人ID
            if (tMisContract.SAMPLE_ACCEPTER_ID.Trim() == "")
            {
                this.Tips.AppendLine("接样人ID不能为空");
                return false;
            }
            //项目负责人ID
            if (tMisContract.PROJECT_ID.Trim() == "")
            {
                this.Tips.AppendLine("项目负责人ID不能为空");
                return false;
            }
            //状态
            if (tMisContract.STATE.Trim() == "")
            {
                this.Tips.AppendLine("状态不能为空");
                return false;
            }
            //委托书状态
            if (tMisContract.CONTRACT_STATUS.Trim() == "")
            {
                this.Tips.AppendLine("委托书状态不能为空");
                return false;
            }
            //备注1
            if (tMisContract.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisContract.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisContract.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisContract.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisContract.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
