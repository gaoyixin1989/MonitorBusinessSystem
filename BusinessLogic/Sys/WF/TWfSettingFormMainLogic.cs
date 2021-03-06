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
    /// 功能：流程主表单配置
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFormMainLogic : LogicBase
    {

        TWfSettingFormMainVo tWfSettingFormMain = new TWfSettingFormMainVo();
        TWfSettingFormMainAccess access;

        public TWfSettingFormMainLogic()
        {
            access = new TWfSettingFormMainAccess();
        }

        public TWfSettingFormMainLogic(TWfSettingFormMainVo _tWfSettingFormMain)
        {
            tWfSettingFormMain = _tWfSettingFormMain;
            access = new TWfSettingFormMainAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingFormMain">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingFormMainVo tWfSettingFormMain)
        {
            return access.GetSelectResultCount(tWfSettingFormMain);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingFormMainVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingFormMain">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingFormMainVo Details(TWfSettingFormMainVo tWfSettingFormMain)
        {
            return access.Details(tWfSettingFormMain);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingFormMain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingFormMainVo> SelectByObject(TWfSettingFormMainVo tWfSettingFormMain, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfSettingFormMain, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingFormMain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingFormMainVo tWfSettingFormMain, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfSettingFormMain, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingFormMain"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingFormMainVo tWfSettingFormMain)
        {
            return access.SelectByTable(tWfSettingFormMain);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingFormMain">对象</param>
        /// <returns></returns>
        public TWfSettingFormMainVo SelectByObject(TWfSettingFormMainVo tWfSettingFormMain)
        {
            return access.SelectByObject(tWfSettingFormMain);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingFormMainVo tWfSettingFormMain)
        {
            return access.Create(tWfSettingFormMain);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormMain">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormMainVo tWfSettingFormMain)
        {
            return access.Edit(tWfSettingFormMain);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormMain_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingFormMain_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormMainVo tWfSettingFormMain_UpdateSet, TWfSettingFormMainVo tWfSettingFormMain_UpdateWhere)
        {
            return access.Edit(tWfSettingFormMain_UpdateSet, tWfSettingFormMain_UpdateWhere);
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
        public bool Delete(TWfSettingFormMainVo tWfSettingFormMain)
        {
            return access.Delete(tWfSettingFormMain);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfSettingFormMain.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //主表单编号
            if (tWfSettingFormMain.UCM_ID.Trim() == "")
            {
                this.Tips.AppendLine("主表单编号不能为空");
                return false;
            }
            //主表单简称
            if (tWfSettingFormMain.UCM_CAPTION.Trim() == "")
            {
                this.Tips.AppendLine("主表单简称不能为空");
                return false;
            }
            //主表单内编码
            if (tWfSettingFormMain.UCM_NOTE.Trim() == "")
            {
                this.Tips.AppendLine("主表单内编码不能为空");
                return false;
            }
            //主表单类型
            if (tWfSettingFormMain.UCM_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("主表单类型不能为空");
                return false;
            }
            //主表单描述
            if (tWfSettingFormMain.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("主表单描述不能为空");
                return false;
            }

            return true;
        }

    }
}
