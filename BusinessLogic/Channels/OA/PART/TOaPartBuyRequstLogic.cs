using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.PART;
using i3.DataAccess.Channels.OA.PART;

namespace i3.BusinessLogic.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料采购申请
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartBuyRequstLogic : LogicBase
    {

        TOaPartBuyRequstVo tOaPartBuyRequst = new TOaPartBuyRequstVo();
        TOaPartBuyRequstAccess access;

        public TOaPartBuyRequstLogic()
        {
            access = new TOaPartBuyRequstAccess();
        }

        public TOaPartBuyRequstLogic(TOaPartBuyRequstVo _tOaPartBuyRequst)
        {
            tOaPartBuyRequst = _tOaPartBuyRequst;
            access = new TOaPartBuyRequstAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            return access.GetSelectResultCount(tOaPartBuyRequst);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartBuyRequstVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartBuyRequstVo Details(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            return access.Details(tOaPartBuyRequst);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartBuyRequstVo> SelectByObject(TOaPartBuyRequstVo tOaPartBuyRequst, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaPartBuyRequst, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartBuyRequstVo tOaPartBuyRequst, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaPartBuyRequst, iIndex, iCount);
        }

        /// <summary>
        /// 获取对象DataTable,根据物料类型
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象</param>
        /// /// <param name="tOaPartInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByPart(TOaPartBuyRequstVo tOaPartBuyRequst, TOaPartInfoVo tOaPartInfo, int iIndex, int iCount)
        {
            return access.SelectByPart(tOaPartBuyRequst, tOaPartInfo,iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartBuyRequst"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            return access.SelectByTable(tOaPartBuyRequst);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartBuyRequst">对象</param>
        /// <returns></returns>
        public TOaPartBuyRequstVo SelectByObject(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            return access.SelectByObject(tOaPartBuyRequst);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            return access.Create(tOaPartBuyRequst);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartBuyRequst">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            return access.Edit(tOaPartBuyRequst);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartBuyRequst_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaPartBuyRequst_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartBuyRequstVo tOaPartBuyRequst_UpdateSet, TOaPartBuyRequstVo tOaPartBuyRequst_UpdateWhere)
        {
            return access.Edit(tOaPartBuyRequst_UpdateSet, tOaPartBuyRequst_UpdateWhere);
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
        public bool Delete(TOaPartBuyRequstVo tOaPartBuyRequst)
        {
            return access.Delete(tOaPartBuyRequst);
        }

        public DataTable SelectRemarks(string ID)
        {
            return access.SelectRemarks(ID);
        }
        public DataTable SelectName(string ID)
        {
            return access.SelectUserName(ID);
        }

        public DataTable SelectItemInfo(string ID)
        {
            return access.SelectItemInfo(ID);
        }

        public DataTable SelectDept(string Code)
        {
            return access.SelectDept(Code);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tOaPartBuyRequst.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //申请科室
            if (tOaPartBuyRequst.APPLY_DEPT_ID.Trim() == "")
            {
                this.Tips.AppendLine("申请科室不能为空");
                return false;
            }
            //申请人
            if (tOaPartBuyRequst.APPLY_USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("申请人不能为空");
                return false;
            }
            //申请时间
            if (tOaPartBuyRequst.APPLY_DATE.Trim() == "")
            {
                this.Tips.AppendLine("申请时间不能为空");
                return false;
            }
            //申请标题
            if (tOaPartBuyRequst.APPLY_TITLE.Trim() == "")
            {
                this.Tips.AppendLine("申请标题不能为空");
                return false;
            }
            //部门审批人
            if (tOaPartBuyRequst.APP_DEPT_ID.Trim() == "")
            {
                this.Tips.AppendLine("部门审批人不能为空");
                return false;
            }
            //部门审批时间
            if (tOaPartBuyRequst.APP_DEPT_DATE.Trim() == "")
            {
                this.Tips.AppendLine("部门审批时间不能为空");
                return false;
            }
            //部门审批意见
            if (tOaPartBuyRequst.APP_DEPT_INFO.Trim() == "")
            {
                this.Tips.AppendLine("部门审批意见不能为空");
                return false;
            }
            //技术负责人审批人
            if (tOaPartBuyRequst.APP_MANAGER_ID.Trim() == "")
            {
                this.Tips.AppendLine("技术负责人审批人不能为空");
                return false;
            }
            //技术负责人审批时间
            if (tOaPartBuyRequst.APP_MANAGER_DATE.Trim() == "")
            {
                this.Tips.AppendLine("技术负责人审批时间不能为空");
                return false;
            }
            //技术负责人审批意见
            if (tOaPartBuyRequst.APP_MANAGER_INFO.Trim() == "")
            {
                this.Tips.AppendLine("技术负责人审批意见不能为空");
                return false;
            }
            //站长审批人
            if (tOaPartBuyRequst.APP_LEADER_ID.Trim() == "")
            {
                this.Tips.AppendLine("站长审批人不能为空");
                return false;
            }
            //站长审批时间
            if (tOaPartBuyRequst.APP_LEADER_DATE.Trim() == "")
            {
                this.Tips.AppendLine("站长审批时间不能为空");
                return false;
            }
            //站长审批意见
            if (tOaPartBuyRequst.APP_LEADER_INFO.Trim() == "")
            {
                this.Tips.AppendLine("站长审批意见不能为空");
                return false;
            }
            //状态,1待审批，2待采购，3已采购
            if (tOaPartBuyRequst.STATUS.Trim() == "")
            {
                this.Tips.AppendLine("状态,1待审批，2待采购，3已采购不能为空");
                return false;
            }
            //备注1
            if (tOaPartBuyRequst.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tOaPartBuyRequst.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaPartBuyRequst.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tOaPartBuyRequst.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tOaPartBuyRequst.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }
            //办公室意见
            if (tOaPartBuyRequst.APP_OFFER_INFO.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //办公室人
            if (tOaPartBuyRequst.APP_OFFER_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //办公室时间
            if (tOaPartBuyRequst.APP_OFFER_TIME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //物料类别
            if (tOaPartBuyRequst.APPLY_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //申购内容
            if (tOaPartBuyRequst.APPLY_CONTENT.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            return true;
        }

    }
}
