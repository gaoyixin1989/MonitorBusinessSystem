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
    /// 功能：委托书样品
    /// 创建日期：2012-11-27
    /// 创建人：潘德军
    /// </summary>
    public class TMisContractSampleLogic : LogicBase
    {

        TMisContractSampleVo tMisContractSample = new TMisContractSampleVo();
        TMisContractSampleAccess access;

        public TMisContractSampleLogic()
        {
            access = new TMisContractSampleAccess();
        }

        public TMisContractSampleLogic(TMisContractSampleVo _tMisContractSample)
        {
            tMisContractSample = _tMisContractSample;
            access = new TMisContractSampleAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisContractSample">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisContractSampleVo tMisContractSample)
        {
            return access.GetSelectResultCount(tMisContractSample);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisContractSampleVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisContractSample">对象条件</param>
        /// <returns>对象</returns>
        public TMisContractSampleVo Details(TMisContractSampleVo tMisContractSample)
        {
            return access.Details(tMisContractSample);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisContractSample">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisContractSampleVo> SelectByObject(TMisContractSampleVo tMisContractSample, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisContractSample, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisContractSample">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisContractSampleVo tMisContractSample, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisContractSample, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisContractSample"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisContractSampleVo tMisContractSample)
        {
            return access.SelectByTable(tMisContractSample);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisContractSample">对象</param>
        /// <returns></returns>
        public TMisContractSampleVo SelectByObject(TMisContractSampleVo tMisContractSample)
        {
            return access.SelectByObject(tMisContractSample);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisContractSampleVo tMisContractSample)
        {
            return access.Create(tMisContractSample);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSample">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSampleVo tMisContractSample)
        {
            return access.Edit(tMisContractSample);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisContractSample_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisContractSample_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisContractSampleVo tMisContractSample_UpdateSet, TMisContractSampleVo tMisContractSample_UpdateWhere)
        {
            return access.Edit(tMisContractSample_UpdateSet, tMisContractSample_UpdateWhere);
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
        public bool Delete(TMisContractSampleVo tMisContractSample)
        {
            return access.Delete(tMisContractSample);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisContractSample.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //委托书ID
            if (tMisContractSample.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书ID不能为空");
                return false;
            }
            //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
            if (tMisContractSample.MONITOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）不能为空");
                return false;
            }
            //样品类型
            if (tMisContractSample.SAMPLE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("样品类型不能为空");
                return false;
            }
            //样品名称
            if (tMisContractSample.SAMPLE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("样品名称不能为空");
                return false;
            }
            //样品数量
            if (tMisContractSample.SAMPLE_COUNT.Trim() == "")
            {
                this.Tips.AppendLine("样品数量不能为空");
                return false;
            }
            //备注1
            if (tMisContractSample.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisContractSample.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisContractSample.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisContractSample.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisContractSample.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
