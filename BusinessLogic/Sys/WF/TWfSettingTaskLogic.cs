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
    /// 功能：流程节点集合表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingTaskLogic : LogicBase
    {

        TWfSettingTaskVo tWfSettingTask = new TWfSettingTaskVo();
        TWfSettingTaskAccess access;

        public TWfSettingTaskLogic()
        {
            access = new TWfSettingTaskAccess();
        }

        public TWfSettingTaskLogic(TWfSettingTaskVo _tWfSettingTask)
        {
            tWfSettingTask = _tWfSettingTask;
            access = new TWfSettingTaskAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingTaskVo tWfSettingTask)
        {
            return access.GetSelectResultCount(tWfSettingTask);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingTaskVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingTask">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingTaskVo Details(TWfSettingTaskVo tWfSettingTask)
        {
            return access.Details(tWfSettingTask);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingTaskVo> SelectByObject(TWfSettingTaskVo tWfSettingTask, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfSettingTask, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingTaskVo tWfSettingTask, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfSettingTask, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingTask"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingTaskVo tWfSettingTask)
        {
            return access.SelectByTable(tWfSettingTask);
        }

        //搜索指定流程的所有环节
        public DataTable SelectByTable_byWFID(string strWF_ID)
        {
            return access.SelectByTable_byWFID(strWF_ID);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <returns></returns>
        public TWfSettingTaskVo SelectByObject(TWfSettingTaskVo tWfSettingTask)
        {
            return access.SelectByObject(tWfSettingTask);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingTaskVo tWfSettingTask)
        {
            return access.Create(tWfSettingTask);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTask">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskVo tWfSettingTask)
        {
            return access.Edit(tWfSettingTask);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingTask_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingTask_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingTaskVo tWfSettingTask_UpdateSet, TWfSettingTaskVo tWfSettingTask_UpdateWhere)
        {
            return access.Edit(tWfSettingTask_UpdateSet, tWfSettingTask_UpdateWhere);
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
        public bool Delete(TWfSettingTaskVo tWfSettingTask)
        {
            return access.Delete(tWfSettingTask);
        }

        /// <summary>
        /// 根据流程编号获取所有子环节数据列表
        /// </summary>
        /// <param name="tWfSettingTask">用户对象</param>
        /// <returns>返回对象列表</returns>
        public List<TWfSettingTaskVo> SelectByObjectList(TWfSettingTaskVo tWfSettingTask)
        {
            return access.SelectByObjectList(tWfSettingTask);
        }

        /// <summary>
        /// 根据WF_ID来获取对象List，主要为获取第一个其实节点设置
        /// </summary>
        /// <param name="tWfSettingTask">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingTaskVo> SelectByObjectListForSetp(TWfSettingTaskVo tWfSettingTask)
        {
            return access.SelectByObjectListForSetp(tWfSettingTask);
        }


        /// <summary>
        /// 获得前一个环节的配置数据
        /// </summary>
        /// <param name="tWfSettingTask">环节数据</param>
        /// <returns></returns>
        public TWfSettingTaskVo GetPreStep(TWfSettingTaskVo tWfSettingTask)
        {
            //此结构的ID和WF_TASK_ID相同
            tWfSettingTask.WF_TASK_ID = string.IsNullOrEmpty(tWfSettingTask.ID) ? tWfSettingTask.WF_TASK_ID : tWfSettingTask.ID;
            if (!string.IsNullOrEmpty(tWfSettingTask.WF_ID) && !(string.IsNullOrEmpty(tWfSettingTask.WF_TASK_ID)))
            {
                List<TWfSettingTaskVo> list = access.SelectByObjectList(new TWfSettingTaskVo() { WF_ID = tWfSettingTask.WF_ID });
                int iIndex = 0;
                //排完序列后，可以进行
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].WF_TASK_ID == tWfSettingTask.WF_TASK_ID)
                    {
                        iIndex = i;
                        break;
                    }
                }

                if (iIndex > 0)
                {
                    return list[iIndex - 1];
                }
            }
            return new TWfSettingTaskVo();
        }

        /// <summary>
        /// 获得下一个环节的配置数据
        /// </summary>
        /// <param name="tWfSettingTask">环节数据</param>
        /// <returns></returns>
        public TWfSettingTaskVo GetNextStep(TWfSettingTaskVo tWfSettingTask)
        {
            if (!string.IsNullOrEmpty(tWfSettingTask.WF_ID) && !string.IsNullOrEmpty(tWfSettingTask.WF_TASK_ID))
            {
                List<TWfSettingTaskVo> list = access.SelectByObjectList(new TWfSettingTaskVo() { WF_ID = tWfSettingTask.WF_ID });
                int iIndex = 0;
                //排完序列后，可以进行
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].WF_TASK_ID == tWfSettingTask.WF_TASK_ID)
                    {
                        iIndex = i;
                        break;
                    }
                }
                if (iIndex < list.Count - 1)
                {
                    return list[iIndex + 1];
                }
            }
            return new TWfSettingTaskVo();
        }

        /// <summary>
        /// 获取指定流程的第一个环节配置数据
        /// </summary>
        /// <param name="strWFID">流程代码</param>
        /// <returns></returns>
        public TWfSettingTaskVo GetFirstStep(string strWFID)
        {
            List<TWfSettingTaskVo> list = access.SelectByObjectList(new TWfSettingTaskVo() { WF_ID = strWFID, SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD, SORT_TYPE = " ASC " });
            return list.Count > 0 ? list[0] : new TWfSettingTaskVo();
        }


        /// <summary>
        /// 是否是开始环节
        /// </summary>
        /// <param name="strWFID"></param>
        /// <param name="strStepID"></param>
        /// <returns></returns>
        public bool IsStartStep(string strWFID, string strStepID)
        {
            string strStep1 = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = strStepID }).WF_TASK_ID;
            DataTable dt = new TWfSettingTaskLogic().SelectByTable(new TWfSettingTaskVo() { WF_ID = strWFID, SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD, SORT_TYPE = " asc " });
            if (dt.Rows.Count > 0 && dt.Rows[0][TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strStep1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 是否是结束环节
        /// </summary>
        /// <param name="strWFID"></param>
        /// <param name="strStepID"></param>
        /// <returns></returns>
        public bool IsEndStep(string strWFID, string strStepID)
        {
            string strStep1 = new TWfSettingTaskLogic().Details(new TWfSettingTaskVo() { WF_TASK_ID = strStepID }).WF_TASK_ID;
            DataTable dt = new TWfSettingTaskLogic().SelectByTable(new TWfSettingTaskVo() { WF_ID = strWFID, SORT_FIELD = TWfSettingTaskVo.TASK_ORDER_FIELD, SORT_TYPE = " asc " });
            if (dt.Rows.Count > 0 && dt.Rows[dt.Rows.Count - 1][TWfSettingTaskVo.WF_TASK_ID_FIELD].ToString() == strStep1)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfSettingTask.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //节点编号
            if (tWfSettingTask.WF_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("节点编号不能为空");
                return false;
            }
            //流程编号
            if (tWfSettingTask.WF_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程编号不能为空");
                return false;
            }
            //节点类型
            if (tWfSettingTask.TASK_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("节点类型不能为空");
                return false;
            }
            //节点简称
            if (tWfSettingTask.TASK_CAPTION.Trim() == "")
            {
                this.Tips.AppendLine("节点简称不能为空");
                return false;
            }
            //节点描述
            if (tWfSettingTask.TASK_NOTE.Trim() == "")
            {
                this.Tips.AppendLine("节点描述不能为空");
                return false;
            }
            //节点与非类型
            if (tWfSettingTask.TASK_AND_OR.Trim() == "")
            {
                this.Tips.AppendLine("节点与非类型不能为空");
                return false;
            }
            //操作人类型
            if (tWfSettingTask.OPER_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("操作人类型不能为空");
                return false;
            }
            //操作人值
            if (tWfSettingTask.OPER_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("操作人值不能为空");
                return false;
            }
            //命令名称
            if (tWfSettingTask.COMMAND_NAME.Trim() == "")
            {
                this.Tips.AppendLine("命令名称不能为空");
                return false;
            }
            //跳过自己
            if (tWfSettingTask.SELF_DEAL.Trim() == "")
            {
                this.Tips.AppendLine("跳过自己不能为空");
                return false;
            }
            //绘图X坐标
            if (tWfSettingTask.POSITION_IX.Trim() == "")
            {
                this.Tips.AppendLine("绘图X坐标不能为空");
                return false;
            }
            //绘图Y坐标
            if (tWfSettingTask.POSITION_IY.Trim() == "")
            {
                this.Tips.AppendLine("绘图Y坐标不能为空");
                return false;
            }

            return true;
        }

    }
}
