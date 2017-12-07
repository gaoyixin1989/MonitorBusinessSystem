using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Mis.Monitor.Retrun;
using System.Data;
using i3.DataAccess.Channels.Mis.Monitor.Return;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Return
{
    /// <summary>
    /// 功能：监测分析各环节退回意见表
    /// 创建日期：2014-04-08
    /// 创建人：魏林
    /// </summary>
    public class TMisReturnInfoLogic : LogicBase
    {

        TMisReturnInfoVo tMisReturnInfo = new TMisReturnInfoVo();
        TMisReturnInfoAccess access;

        public TMisReturnInfoLogic()
        {
            access = new TMisReturnInfoAccess();
        }

        public TMisReturnInfoLogic(TMisReturnInfoVo _tMisReturnInfo)
        {
            tMisReturnInfo = _tMisReturnInfo;
            access = new TMisReturnInfoAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisReturnInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisReturnInfoVo tMisReturnInfo)
        {
            return access.GetSelectResultCount(tMisReturnInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisReturnInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisReturnInfo">对象条件</param>
        /// <returns>对象</returns>
        public TMisReturnInfoVo Details(TMisReturnInfoVo tMisReturnInfo)
        {
            return access.Details(tMisReturnInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisReturnInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisReturnInfoVo> SelectByObject(TMisReturnInfoVo tMisReturnInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisReturnInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisReturnInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisReturnInfoVo tMisReturnInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisReturnInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisReturnInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisReturnInfoVo tMisReturnInfo)
        {
            return access.SelectByTable(tMisReturnInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisReturnInfo">对象</param>
        /// <returns></returns>
        public TMisReturnInfoVo SelectByObject(TMisReturnInfoVo tMisReturnInfo)
        {
            return access.SelectByObject(tMisReturnInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisReturnInfoVo tMisReturnInfo)
        {
            return access.Create(tMisReturnInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisReturnInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisReturnInfoVo tMisReturnInfo)
        {
            return access.Edit(tMisReturnInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisReturnInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisReturnInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisReturnInfoVo tMisReturnInfo_UpdateSet, TMisReturnInfoVo tMisReturnInfo_UpdateWhere)
        {
            return access.Edit(tMisReturnInfo_UpdateSet, tMisReturnInfo_UpdateWhere);
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
        public bool Delete(TMisReturnInfoVo tMisReturnInfo)
        {
            return access.Delete(tMisReturnInfo);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tMisReturnInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //任务ID
            if (tMisReturnInfo.TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("任务ID不能为空");
                return false;
            }
            //子任务ID
            if (tMisReturnInfo.SUBTASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("子任务ID不能为空");
                return false;
            }
            //项目结果ID
            if (tMisReturnInfo.RESULT_ID.Trim() == "")
            {
                this.Tips.AppendLine("项目结果ID不能为空");
                return false;
            }
            //当前环节号
            if (tMisReturnInfo.CURRENT_STATUS.Trim() == "")
            {
                this.Tips.AppendLine("当前环节号不能为空");
                return false;
            }
            //退回环节号
            if (tMisReturnInfo.BACKTO_STATUS.Trim() == "")
            {
                this.Tips.AppendLine("退回环节号不能为空");
                return false;
            }
            //退回意见
            if (tMisReturnInfo.SUGGESTION.Trim() == "")
            {
                this.Tips.AppendLine("退回意见不能为空");
                return false;
            }
            //
            if (tMisReturnInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisReturnInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisReturnInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisReturnInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tMisReturnInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }

    }

}
