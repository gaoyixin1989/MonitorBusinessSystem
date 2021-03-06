using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.WF;
using i3.DataAccess.Sys.WF;

namespace i3.BusinessLogic.Sys.WF
{
    /// <summary>
    /// 功能：流程配置主表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFlowLogic : LogicBase
    {

        TWfSettingFlowVo tWfSettingFlow = new TWfSettingFlowVo();
        TWfSettingFlowAccess access;

        public TWfSettingFlowLogic()
        {
            access = new TWfSettingFlowAccess();
        }

        public TWfSettingFlowLogic(TWfSettingFlowVo _tWfSettingFlow)
        {
            tWfSettingFlow = _tWfSettingFlow;
            access = new TWfSettingFlowAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingFlow">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingFlowVo tWfSettingFlow)
        {
            return access.GetSelectResultCount(tWfSettingFlow);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingFlowVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingFlow">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingFlowVo Details(TWfSettingFlowVo tWfSettingFlow)
        {
            return access.Details(tWfSettingFlow);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingFlow">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingFlowVo> SelectByObject(TWfSettingFlowVo tWfSettingFlow, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfSettingFlow, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingFlow">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingFlowVo tWfSettingFlow, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfSettingFlow, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingFlow"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingFlowVo tWfSettingFlow)
        {
            return access.SelectByTable(tWfSettingFlow);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingFlow">对象</param>
        /// <returns></returns>
        public TWfSettingFlowVo SelectByObject(TWfSettingFlowVo tWfSettingFlow)
        {
            return access.SelectByObject(tWfSettingFlow);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingFlowVo tWfSettingFlow)
        {
            return access.Create(tWfSettingFlow);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFlow">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFlowVo tWfSettingFlow)
        {
            return access.Edit(tWfSettingFlow);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFlow_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingFlow_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFlowVo tWfSettingFlow_UpdateSet, TWfSettingFlowVo tWfSettingFlow_UpdateWhere)
        {
            return access.Edit(tWfSettingFlow_UpdateSet, tWfSettingFlow_UpdateWhere);
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
        public bool Delete(TWfSettingFlowVo tWfSettingFlow)
        {
            return access.Delete(tWfSettingFlow);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfSettingFlow.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //工作流编号
            if (tWfSettingFlow.WF_ID.Trim() == "")
            {
                this.Tips.AppendLine("工作流编号不能为空");
                return false;
            }
            //工作流简称
            if (tWfSettingFlow.WF_CAPTION.Trim() == "")
            {
                this.Tips.AppendLine("工作流简称不能为空");
                return false;
            }
            //类别归属
            if (tWfSettingFlow.WF_CLASS_ID.Trim() == "")
            {
                this.Tips.AppendLine("类别归属不能为空");
                return false;
            }
            //生成的版本
            if (tWfSettingFlow.WF_VERSION.Trim() == "")
            {
                this.Tips.AppendLine("生成的版本不能为空");
                return false;
            }
            //存在状态
            if (tWfSettingFlow.WF_STATE.Trim() == "")
            {
                this.Tips.AppendLine("存在状态不能为空");
                return false;
            }
            //工作流描述
            if (tWfSettingFlow.WF_NOTE.Trim() == "")
            {
                this.Tips.AppendLine("工作流描述不能为空");
                return false;
            }
            //主表单
            if (tWfSettingFlow.WF_FORM_MAIN.Trim() == "")
            {
                this.Tips.AppendLine("主表单不能为空");
                return false;
            }
            //创建人
            if (tWfSettingFlow.CREATE_USER.Trim() == "")
            {
                this.Tips.AppendLine("创建人不能为空");
                return false;
            }
            //创建日期
            if (tWfSettingFlow.CREATE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("创建日期不能为空");
                return false;
            }
            //处理类型
            if (tWfSettingFlow.DEAL_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("处理类型不能为空");
                return false;
            }
            //删除人
            if (tWfSettingFlow.DEAL_USER.Trim() == "")
            {
                this.Tips.AppendLine("删除人不能为空");
                return false;
            }
            //删除日期
            if (tWfSettingFlow.DEAL_DATE.Trim() == "")
            {
                this.Tips.AppendLine("删除日期不能为空");
                return false;
            }
            //备注
            if (tWfSettingFlow.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }

            return true;
        }

    }
}
