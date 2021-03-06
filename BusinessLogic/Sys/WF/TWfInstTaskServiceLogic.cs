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
    /// 功能：工作流实例环节附属业务明细表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstTaskServiceLogic : LogicBase
    {

        TWfInstTaskServiceVo tWfInstTaskService = new TWfInstTaskServiceVo();
        TWfInstTaskServiceAccess access;

        public TWfInstTaskServiceLogic()
        {
            access = new TWfInstTaskServiceAccess();
        }

        public TWfInstTaskServiceLogic(TWfInstTaskServiceVo _tWfInstTaskService)
        {
            tWfInstTaskService = _tWfInstTaskService;
            access = new TWfInstTaskServiceAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstTaskService">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstTaskServiceVo tWfInstTaskService)
        {
            return access.GetSelectResultCount(tWfInstTaskService);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstTaskServiceVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstTaskService">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstTaskServiceVo Details(TWfInstTaskServiceVo tWfInstTaskService)
        {
            return access.Details(tWfInstTaskService);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfInstTaskService">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstTaskServiceVo> SelectByObject(TWfInstTaskServiceVo tWfInstTaskService, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfInstTaskService, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfInstTaskService">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfInstTaskServiceVo tWfInstTaskService, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfInstTaskService, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstTaskService"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstTaskServiceVo tWfInstTaskService)
        {
            return access.SelectByTable(tWfInstTaskService);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstTaskService">对象</param>
        /// <returns></returns>
        public TWfInstTaskServiceVo SelectByObject(TWfInstTaskServiceVo tWfInstTaskService)
        {
            return access.SelectByObject(tWfInstTaskService);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstTaskServiceVo tWfInstTaskService)
        {
            return access.Create(tWfInstTaskService);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskService">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskServiceVo tWfInstTaskService)
        {
            return access.Edit(tWfInstTaskService);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskService_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfInstTaskService_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskServiceVo tWfInstTaskService_UpdateSet, TWfInstTaskServiceVo tWfInstTaskService_UpdateWhere)
        {
            return access.Edit(tWfInstTaskService_UpdateSet, tWfInstTaskService_UpdateWhere);
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
        public bool Delete(TWfInstTaskServiceVo tWfInstTaskService)
        {
            return access.Delete(tWfInstTaskService);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfInstTaskService.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //环节实例编号
            if (tWfInstTaskService.WF_INST_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("环节实例编号不能为空");
                return false;
            }
            //流程实例编号
            if (tWfInstTaskService.WF_INST_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程实例编号不能为空");
                return false;
            }
            //业务编号
            if (tWfInstTaskService.SERVICE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("业务编号不能为空");
                return false;
            }
            //业务单类型
            if (tWfInstTaskService.SERVICE_KEY_NAME.Trim() == "")
            {
                this.Tips.AppendLine("业务单类型不能为空");
                return false;
            }
            //业务单主键值
            if (tWfInstTaskService.SERVICE_KEY_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("业务单主键值不能为空");
                return false;
            }
            //联合单据分组
            if (tWfInstTaskService.SERVICE_ROW_SIGN.Trim() == "")
            {
                this.Tips.AppendLine("联合单据分组不能为空");
                return false;
            }

            return true;
        }

    }
}
