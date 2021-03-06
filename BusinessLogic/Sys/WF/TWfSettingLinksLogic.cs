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
    /// 功能：流程节点连接线
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingLinksLogic : LogicBase
    {

        TWfSettingLinksVo tWfSettingLinks = new TWfSettingLinksVo();
        TWfSettingLinksAccess access;

        public TWfSettingLinksLogic()
        {
            access = new TWfSettingLinksAccess();
        }

        public TWfSettingLinksLogic(TWfSettingLinksVo _tWfSettingLinks)
        {
            tWfSettingLinks = _tWfSettingLinks;
            access = new TWfSettingLinksAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingLinks">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingLinksVo tWfSettingLinks)
        {
            return access.GetSelectResultCount(tWfSettingLinks);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingLinksVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingLinks">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingLinksVo Details(TWfSettingLinksVo tWfSettingLinks)
        {
            return access.Details(tWfSettingLinks);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingLinks">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingLinksVo> SelectByObject(TWfSettingLinksVo tWfSettingLinks, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfSettingLinks, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingLinks">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingLinksVo tWfSettingLinks, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfSettingLinks, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingLinks"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingLinksVo tWfSettingLinks)
        {
            return access.SelectByTable(tWfSettingLinks);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingLinks">对象</param>
        /// <returns></returns>
        public TWfSettingLinksVo SelectByObject(TWfSettingLinksVo tWfSettingLinks)
        {
            return access.SelectByObject(tWfSettingLinks);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingLinksVo tWfSettingLinks)
        {
            return access.Create(tWfSettingLinks);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingLinks">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingLinksVo tWfSettingLinks)
        {
            return access.Edit(tWfSettingLinks);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingLinks_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingLinks_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingLinksVo tWfSettingLinks_UpdateSet, TWfSettingLinksVo tWfSettingLinks_UpdateWhere)
        {
            return access.Edit(tWfSettingLinks_UpdateSet, tWfSettingLinks_UpdateWhere);
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
        public bool Delete(TWfSettingLinksVo tWfSettingLinks)
        {
            return access.Delete(tWfSettingLinks);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfSettingLinks.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //连接编号
            if (tWfSettingLinks.WF_LINK_ID.Trim() == "")
            {
                this.Tips.AppendLine("连接编号不能为空");
                return false;
            }
            //流程编号
            if (tWfSettingLinks.WF_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程编号不能为空");
                return false;
            }
            //起始环节编号
            if (tWfSettingLinks.START_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("起始环节编号不能为空");
                return false;
            }
            //结束环节编号
            if (tWfSettingLinks.END_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("结束环节编号不能为空");
                return false;
            }
            //条件描述
            if (tWfSettingLinks.CONDITION_DES.Trim() == "")
            {
                this.Tips.AppendLine("条件描述不能为空");
                return false;
            }
            //文字简述
            if (tWfSettingLinks.NOTE_DES.Trim() == "")
            {
                this.Tips.AppendLine("文字简述不能为空");
                return false;
            }
            //命令描述
            if (tWfSettingLinks.CMD_DES.Trim() == "")
            {
                this.Tips.AppendLine("命令描述不能为空");
                return false;
            }
            //优先级
            if (tWfSettingLinks.PRIORITY.Trim() == "")
            {
                this.Tips.AppendLine("优先级不能为空");
                return false;
            }

            return true;
        }

    }
}
