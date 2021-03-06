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
    /// 功能：流程表单列表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFormListLogic : LogicBase
    {

        TWfSettingFormListVo tWfSettingFormList = new TWfSettingFormListVo();
        TWfSettingFormListAccess access;

        public TWfSettingFormListLogic()
        {
            access = new TWfSettingFormListAccess();
        }

        public TWfSettingFormListLogic(TWfSettingFormListVo _tWfSettingFormList)
        {
            tWfSettingFormList = _tWfSettingFormList;
            access = new TWfSettingFormListAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingFormList">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingFormListVo tWfSettingFormList)
        {
            return access.GetSelectResultCount(tWfSettingFormList);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingFormListVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingFormList">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingFormListVo Details(TWfSettingFormListVo tWfSettingFormList)
        {
            return access.Details(tWfSettingFormList);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingFormList">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingFormListVo> SelectByObject(TWfSettingFormListVo tWfSettingFormList, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfSettingFormList, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingFormList">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingFormListVo tWfSettingFormList, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfSettingFormList, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingFormList"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingFormListVo tWfSettingFormList)
        {
            return access.SelectByTable(tWfSettingFormList);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingFormList">对象</param>
        /// <returns></returns>
        public TWfSettingFormListVo SelectByObject(TWfSettingFormListVo tWfSettingFormList)
        {
            return access.SelectByObject(tWfSettingFormList);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingFormListVo tWfSettingFormList)
        {
            return access.Create(tWfSettingFormList);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormList">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormListVo tWfSettingFormList)
        {
            return access.Edit(tWfSettingFormList);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormList_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingFormList_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormListVo tWfSettingFormList_UpdateSet, TWfSettingFormListVo tWfSettingFormList_UpdateWhere)
        {
            return access.Edit(tWfSettingFormList_UpdateSet, tWfSettingFormList_UpdateWhere);
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
        public bool Delete(TWfSettingFormListVo tWfSettingFormList)
        {
            return access.Delete(tWfSettingFormList);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfSettingFormList.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //列表编号
            if (tWfSettingFormList.UC_LIST_ID.Trim() == "")
            {
                this.Tips.AppendLine("列表编号不能为空");
                return false;
            }
            //主表单编号
            if (tWfSettingFormList.UCM_ID.Trim() == "")
            {
                this.Tips.AppendLine("主表单编号不能为空");
                return false;
            }
            //子表单编号
            if (tWfSettingFormList.UCS_ID.Trim() == "")
            {
                this.Tips.AppendLine("子表单编号不能为空");
                return false;
            }
            //内部排序
            if (tWfSettingFormList.CTRL_ORDER.Trim() == "")
            {
                this.Tips.AppendLine("内部排序不能为空");
                return false;
            }
            //子表单状态
            if (tWfSettingFormList.CTRL_STATE.Trim() == "")
            {
                this.Tips.AppendLine("子表单状态不能为空");
                return false;
            }

            return true;
        }

    }
}
