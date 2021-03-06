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
    /// 功能：工作流节点表单集
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingTaskFormLogic : LogicBase
    {

        TWfSettingTaskFormVo tWfSettingTaskForm = new TWfSettingTaskFormVo();
        TWfSettingTaskFormAccess access;

        public TWfSettingTaskFormLogic()
        {
            access = new TWfSettingTaskFormAccess();
        }

        public TWfSettingTaskFormLogic(TWfSettingTaskFormVo _tWfSettingTaskForm)
        {
            tWfSettingTaskForm = _tWfSettingTaskForm;
            access = new TWfSettingTaskFormAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingTaskForm">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            return access.GetSelectResultCount(tWfSettingTaskForm);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingTaskFormVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingTaskForm">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingTaskFormVo Details(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            return access.Details(tWfSettingTaskForm);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingTaskForm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingTaskFormVo> SelectByObject(TWfSettingTaskFormVo tWfSettingTaskForm, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfSettingTaskForm, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingTaskForm">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingTaskFormVo tWfSettingTaskForm, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfSettingTaskForm, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingTaskForm"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            return access.SelectByTable(tWfSettingTaskForm);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingTaskForm">对象</param>
        /// <returns></returns>
        public TWfSettingTaskFormVo SelectByObject(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            return access.SelectByObject(tWfSettingTaskForm);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            return access.Create(tWfSettingTaskForm);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskForm">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            return access.Edit(tWfSettingTaskForm);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTaskForm_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingTaskForm_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskFormVo tWfSettingTaskForm_UpdateSet, TWfSettingTaskFormVo tWfSettingTaskForm_UpdateWhere)
        {
            return access.Edit(tWfSettingTaskForm_UpdateSet, tWfSettingTaskForm_UpdateWhere);
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
        public bool Delete(TWfSettingTaskFormVo tWfSettingTaskForm)
        {
            return access.Delete(tWfSettingTaskForm);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfSettingTaskForm.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //表单内编号
            if (tWfSettingTaskForm.WF_TF_ID.Trim() == "")
            {
                this.Tips.AppendLine("表单内编号不能为空");
                return false;
            }
            //流程编号
            if (tWfSettingTaskForm.WF_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程编号不能为空");
                return false;
            }
            //节点编号
            if (tWfSettingTaskForm.WF_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("节点编号不能为空");
                return false;
            }
            //主表单编号
            if (tWfSettingTaskForm.UCM_ID.Trim() == "")
            {
                this.Tips.AppendLine("主表单编号不能为空");
                return false;
            }
            //主表单类型
            if (tWfSettingTaskForm.UCM_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("主表单类型不能为空");
                return false;
            }

            return true;
        }

    }
}
