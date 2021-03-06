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
    /// 功能：流程子表单配置
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingFormSubLogic : LogicBase
    {

        TWfSettingFormSubVo tWfSettingFormSub = new TWfSettingFormSubVo();
        TWfSettingFormSubAccess access;

        public TWfSettingFormSubLogic()
        {
            access = new TWfSettingFormSubAccess();
        }

        public TWfSettingFormSubLogic(TWfSettingFormSubVo _tWfSettingFormSub)
        {
            tWfSettingFormSub = _tWfSettingFormSub;
            access = new TWfSettingFormSubAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingFormSub">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingFormSubVo tWfSettingFormSub)
        {
            return access.GetSelectResultCount(tWfSettingFormSub);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingFormSubVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingFormSub">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingFormSubVo Details(TWfSettingFormSubVo tWfSettingFormSub)
        {
            return access.Details(tWfSettingFormSub);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingFormSub">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingFormSubVo> SelectByObject(TWfSettingFormSubVo tWfSettingFormSub, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfSettingFormSub, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingFormSub">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingFormSubVo tWfSettingFormSub, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfSettingFormSub, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingFormSub"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingFormSubVo tWfSettingFormSub)
        {
            return access.SelectByTable(tWfSettingFormSub);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingFormSub">对象</param>
        /// <returns></returns>
        public TWfSettingFormSubVo SelectByObject(TWfSettingFormSubVo tWfSettingFormSub)
        {
            return access.SelectByObject(tWfSettingFormSub);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingFormSubVo tWfSettingFormSub)
        {
            return access.Create(tWfSettingFormSub);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormSub">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormSubVo tWfSettingFormSub)
        {
            return access.Edit(tWfSettingFormSub);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingFormSub_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingFormSub_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingFormSubVo tWfSettingFormSub_UpdateSet, TWfSettingFormSubVo tWfSettingFormSub_UpdateWhere)
        {
            return access.Edit(tWfSettingFormSub_UpdateSet, tWfSettingFormSub_UpdateWhere);
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
        public bool Delete(TWfSettingFormSubVo tWfSettingFormSub)
        {
            return access.Delete(tWfSettingFormSub);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfSettingFormSub.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //子表单编号
            if (tWfSettingFormSub.UCS_ID.Trim() == "")
            {
                this.Tips.AppendLine("子表单编号不能为空");
                return false;
            }
            //子表单简称
            if (tWfSettingFormSub.UCS_CAPTION.Trim() == "")
            {
                this.Tips.AppendLine("子表单简称不能为空");
                return false;
            }
            //子表单类型
            if (tWfSettingFormSub.UCS_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("子表单类型不能为空");
                return false;
            }
            //相对路径
            if (tWfSettingFormSub.UCS_PATH.Trim() == "")
            {
                this.Tips.AppendLine("相对路径不能为空");
                return false;
            }
            //子表单全名
            if (tWfSettingFormSub.UCS_NAME.Trim() == "")
            {
                this.Tips.AppendLine("子表单全名不能为空");
                return false;
            }
            //子表单内编码
            if (tWfSettingFormSub.UCS_NOTE.Trim() == "")
            {
                this.Tips.AppendLine("子表单内编码不能为空");
                return false;
            }
            //子表单描述
            if (tWfSettingFormSub.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("子表单描述不能为空");
                return false;
            }

            return true;
        }

    }
}
