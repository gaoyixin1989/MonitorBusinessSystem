using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.ARCHIVES;
using i3.DataAccess.Channels.OA.ARCHIVES;

namespace i3.BusinessLogic.Channels.OA.ARCHIVES
{
    /// <summary>
    /// 功能：档案文件修订
    /// 创建日期：2013-01-31
    /// 创建人：邵世卓
    /// </summary>
    public class TOaArchivesCheckLogic : LogicBase
    {

        TOaArchivesCheckVo tOaArchivesCheck = new TOaArchivesCheckVo();
        TOaArchivesCheckAccess access;

        public TOaArchivesCheckLogic()
        {
            access = new TOaArchivesCheckAccess();
        }

        public TOaArchivesCheckLogic(TOaArchivesCheckVo _tOaArchivesCheck)
        {
            tOaArchivesCheck = _tOaArchivesCheck;
            access = new TOaArchivesCheckAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesCheckVo tOaArchivesCheck)
        {
            return access.GetSelectResultCount(tOaArchivesCheck);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesCheckVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesCheck">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesCheckVo Details(TOaArchivesCheckVo tOaArchivesCheck)
        {
            return access.Details(tOaArchivesCheck);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaArchivesCheckVo> SelectByObject(TOaArchivesCheckVo tOaArchivesCheck, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaArchivesCheck, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaArchivesCheckVo tOaArchivesCheck, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaArchivesCheck, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesCheck"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesCheckVo tOaArchivesCheck)
        {
            return access.SelectByTable(tOaArchivesCheck);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <returns></returns>
        public TOaArchivesCheckVo SelectByObject(TOaArchivesCheckVo tOaArchivesCheck)
        {
            return access.SelectByObject(tOaArchivesCheck);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesCheckVo tOaArchivesCheck)
        {
            return access.Create(tOaArchivesCheck);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesCheck">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesCheckVo tOaArchivesCheck)
        {
            return access.Edit(tOaArchivesCheck);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesCheck_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesCheck_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesCheckVo tOaArchivesCheck_UpdateSet, TOaArchivesCheckVo tOaArchivesCheck_UpdateWhere)
        {
            return access.Edit(tOaArchivesCheck_UpdateSet, tOaArchivesCheck_UpdateWhere);
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
        public bool Delete(TOaArchivesCheckVo tOaArchivesCheck)
        {
            return access.Delete(tOaArchivesCheck);
        }

        #region 特定查询
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountForSearch(TOaArchivesCheckVo tOaArchivesCheck)
        {
            return access.GetSelectResultCountForSearch(tOaArchivesCheck);
        }

        /// <summary>
        /// 特定查询
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectByTableForSearch(TOaArchivesCheckVo tOaArchivesCheck, int intPageIndex, int intPageSize)
        {
            return access.SelectByTableForSearch(tOaArchivesCheck, intPageIndex, intPageSize);
        }
        #endregion

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tOaArchivesCheck.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //档案文件ID
            if (tOaArchivesCheck.DOCUMENT_ID.Trim() == "")
            {
                this.Tips.AppendLine("档案文件ID不能为空");
                return false;
            }
            //修订类别（换页、改版）
            if (tOaArchivesCheck.UPDATE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("修订类别（换页、改版）不能为空");
                return false;
            }
            //页号
            if (tOaArchivesCheck.PAGE_NUM.Trim() == "")
            {
                this.Tips.AppendLine("页号不能为空");
                return false;
            }
            //改版前名称
            if (tOaArchivesCheck.OLD_FILE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("改版前名称不能为空");
                return false;
            }
            //原附件名
            if (tOaArchivesCheck.OLD_ATT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("原附件名不能为空");
                return false;
            }
            //原附件说明
            if (tOaArchivesCheck.OLD_ATT_INFO.Trim() == "")
            {
                this.Tips.AppendLine("原附件说明不能为空");
                return false;
            }
            //原附件路径
            if (tOaArchivesCheck.OLD_ATT_URL.Trim() == "")
            {
                this.Tips.AppendLine("原附件路径不能为空");
                return false;
            }
            //修改人ID
            if (tOaArchivesCheck.UPDATE_ID.Trim() == "")
            {
                this.Tips.AppendLine("修改人ID不能为空");
                return false;
            }
            //修改日期
            if (tOaArchivesCheck.UPDATE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("修改日期不能为空");
                return false;
            }
            //是否销毁
            if (tOaArchivesCheck.IS_DESTROY.Trim() == "")
            {
                this.Tips.AppendLine("是否销毁不能为空");
                return false;
            }
            //备注1
            if (tOaArchivesCheck.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tOaArchivesCheck.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaArchivesCheck.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tOaArchivesCheck.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tOaArchivesCheck.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
