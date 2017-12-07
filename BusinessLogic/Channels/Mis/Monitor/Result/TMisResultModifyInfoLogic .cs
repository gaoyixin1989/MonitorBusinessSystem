using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.DataAccess.Channels.Mis.Monitor.Result;
using System.Data;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：数据补录
    /// 创建日期：2014-05-27
    /// 创建人：黄进军
    /// </summary>
    public class TMisResultModifyInfoLogic : LogicBase
    {

        TMisResultModifyInfoVo tMisResultModifyInfo = new TMisResultModifyInfoVo();
        TMisResultModifyInfoAccess access;

        public TMisResultModifyInfoLogic()
        {
            access = new TMisResultModifyInfoAccess();
        }

        public TMisResultModifyInfoLogic(TMisResultModifyInfoVo _tMisResultModifyInfo)
        {
            tMisResultModifyInfo = _tMisResultModifyInfo;
            access = new TMisResultModifyInfoAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisResultModifyInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            return access.GetSelectResultCount(tMisResultModifyInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisResultModifyInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisResultModifyInfo">对象条件</param>
        /// <returns>对象</returns>
        public TMisResultModifyInfoVo Details(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            return access.Details(tMisResultModifyInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisResultModifyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisResultModifyInfoVo> SelectByObject(TMisResultModifyInfoVo tMisResultModifyInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisResultModifyInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisResultModifyInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisResultModifyInfoVo tMisResultModifyInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisResultModifyInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisResultModifyInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            return access.SelectByTable(tMisResultModifyInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisResultModifyInfo">对象</param>
        /// <returns></returns>
        public TMisResultModifyInfoVo SelectByObject(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            return access.SelectByObject(tMisResultModifyInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            return access.Create(tMisResultModifyInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisResultModifyInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            return access.Edit(tMisResultModifyInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisResultModifyInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisResultModifyInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisResultModifyInfoVo tMisResultModifyInfo_UpdateSet, TMisResultModifyInfoVo tMisResultModifyInfo_UpdateWhere)
        {
            return access.Edit(tMisResultModifyInfo_UpdateSet, tMisResultModifyInfo_UpdateWhere);
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
        public bool Delete(TMisResultModifyInfoVo tMisResultModifyInfo)
        {
            return access.Delete(tMisResultModifyInfo);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisResultModifyInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //监测结果ID
            if (tMisResultModifyInfo.RESULT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测结果ID不能为空");
                return false;
            }
            //修改人
            if (tMisResultModifyInfo.MODIFY_USER.Trim() == "")
            {
                this.Tips.AppendLine("修改人不能为空");
                return false;
            }
            //修改时间
            if (tMisResultModifyInfo.MODIFY_TIME.Trim() == "")
            {
                this.Tips.AppendLine("修改时间不能为空");
                return false;
            }
            //批准人
            if (tMisResultModifyInfo.CHECK_USER.Trim() == "")
            {
                this.Tips.AppendLine("批准人不能为空");
                return false;
            }
            //修改原因
            if (tMisResultModifyInfo.MODIFY_SUGGESTION.Trim() == "")
            {
                this.Tips.AppendLine("修改原因不能为空");
                return false;
            }
            //备注1
            if (tMisResultModifyInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisResultModifyInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisResultModifyInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisResultModifyInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisResultModifyInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
