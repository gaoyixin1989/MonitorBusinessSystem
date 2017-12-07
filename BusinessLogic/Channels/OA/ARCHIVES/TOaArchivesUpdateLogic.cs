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
    /// 功能：档案文件查新
    /// 创建日期：2013-01-31
    /// 创建人：邵世卓
    /// </summary>
    public class TOaArchivesUpdateLogic : LogicBase
    {

        TOaArchivesUpdateVo tOaArchivesUpdate = new TOaArchivesUpdateVo();
        TOaArchivesUpdateAccess access;

        public TOaArchivesUpdateLogic()
        {
            access = new TOaArchivesUpdateAccess();
        }

        public TOaArchivesUpdateLogic(TOaArchivesUpdateVo _tOaArchivesUpdate)
        {
            tOaArchivesUpdate = _tOaArchivesUpdate;
            access = new TOaArchivesUpdateAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesUpdate">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            return access.GetSelectResultCount(tOaArchivesUpdate);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesUpdateVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesUpdate">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesUpdateVo Details(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            return access.Details(tOaArchivesUpdate);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaArchivesUpdate">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaArchivesUpdateVo> SelectByObject(TOaArchivesUpdateVo tOaArchivesUpdate, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaArchivesUpdate, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaArchivesUpdate">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaArchivesUpdateVo tOaArchivesUpdate, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaArchivesUpdate, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesUpdate"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            return access.SelectByTable(tOaArchivesUpdate);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesUpdate">对象</param>
        /// <returns></returns>
        public TOaArchivesUpdateVo SelectByObject(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            return access.SelectByObject(tOaArchivesUpdate);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            return access.Create(tOaArchivesUpdate);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesUpdate">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            return access.Edit(tOaArchivesUpdate);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesUpdate_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesUpdate_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesUpdateVo tOaArchivesUpdate_UpdateSet, TOaArchivesUpdateVo tOaArchivesUpdate_UpdateWhere)
        {
            return access.Edit(tOaArchivesUpdate_UpdateSet, tOaArchivesUpdate_UpdateWhere);
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
        public bool Delete(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            return access.Delete(tOaArchivesUpdate);
        }

        #region 特定查询
        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountForSearch(TOaArchivesUpdateVo tOaArchivesUpdate)
        {
            return access.GetSelectResultCountForSearch(tOaArchivesUpdate);
        }

        /// <summary>
        /// 特定查询
        /// </summary>
        /// <param name="tOaArchivesCheck">对象</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectByTableForSearch(TOaArchivesUpdateVo tOaArchivesUpdate, int intPageIndex, int intPageSize)
        {
            return access.SelectByTableForSearch(tOaArchivesUpdate, intPageIndex, intPageSize);
        }
        #endregion


        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tOaArchivesUpdate.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //查新人ID
            if (tOaArchivesUpdate.PERSON_ID.Trim() == "")
            {
                this.Tips.AppendLine("查新人ID不能为空");
                return false;
            }
            //查新日期
            if (tOaArchivesUpdate.UPDATE_TIME.Trim() == "")
            {
                this.Tips.AppendLine("查新日期不能为空");
                return false;
            }
            //方式（1、废止2、替换）
            if (tOaArchivesUpdate.UPDATE_WAY.Trim() == "")
            {
                this.Tips.AppendLine("方式（1、废止2、替换）不能为空");
                return false;
            }
            //查新前档案ID
            if (tOaArchivesUpdate.BEFORE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("查新前档案ID不能为空");
                return false;
            }
            //查新后档案ID
            if (tOaArchivesUpdate.AFTER_NAME.Trim() == "")
            {
                this.Tips.AppendLine("查新后档案ID不能为空");
                return false;
            }
            //备注
            if (tOaArchivesUpdate.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tOaArchivesUpdate.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tOaArchivesUpdate.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaArchivesUpdate.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
