using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Mis.Monitor.Result;
using i3.DataAccess.Channels.Mis.Monitor.Result;

namespace i3.BusinessLogic.Channels.Mis.Monitor.Result
{
    /// <summary>
    /// 功能：分析原始数据附件表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TMisMonitorResultAttLogic : LogicBase
    {

        TMisMonitorResultAttVo tMisMonitorResultAtt = new TMisMonitorResultAttVo();
        TMisMonitorResultAttAccess access;

        public TMisMonitorResultAttLogic()
        {
            access = new TMisMonitorResultAttAccess();
        }

        public TMisMonitorResultAttLogic(TMisMonitorResultAttVo _tMisMonitorResultAtt)
        {
            tMisMonitorResultAtt = _tMisMonitorResultAtt;
            access = new TMisMonitorResultAttAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tMisMonitorResultAtt">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            return access.GetSelectResultCount(tMisMonitorResultAtt);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TMisMonitorResultAttVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tMisMonitorResultAtt">对象条件</param>
        /// <returns>对象</returns>
        public TMisMonitorResultAttVo Details(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            return access.Details(tMisMonitorResultAtt);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tMisMonitorResultAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TMisMonitorResultAttVo> SelectByObject(TMisMonitorResultAttVo tMisMonitorResultAtt, int iIndex, int iCount)
        {
            return access.SelectByObject(tMisMonitorResultAtt, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tMisMonitorResultAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TMisMonitorResultAttVo tMisMonitorResultAtt, int iIndex, int iCount)
        {
            return access.SelectByTable(tMisMonitorResultAtt, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tMisMonitorResultAtt"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            return access.SelectByTable(tMisMonitorResultAtt);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tMisMonitorResultAtt">对象</param>
        /// <returns></returns>
        public TMisMonitorResultAttVo SelectByObject(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            return access.SelectByObject(tMisMonitorResultAtt);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            return access.Create(tMisMonitorResultAtt);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultAtt">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            return access.Edit(tMisMonitorResultAtt);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tMisMonitorResultAtt_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tMisMonitorResultAtt_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TMisMonitorResultAttVo tMisMonitorResultAtt_UpdateSet, TMisMonitorResultAttVo tMisMonitorResultAtt_UpdateWhere)
        {
            return access.Edit(tMisMonitorResultAtt_UpdateSet, tMisMonitorResultAtt_UpdateWhere);
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
        public bool Delete(TMisMonitorResultAttVo tMisMonitorResultAtt)
        {
            return access.Delete(tMisMonitorResultAtt);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tMisMonitorResultAtt.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //样品结果ID
            if (tMisMonitorResultAtt.RESULT_ID.Trim() == "")
            {
                this.Tips.AppendLine("样品结果ID不能为空");
                return false;
            }
            //附件ID
            if (tMisMonitorResultAtt.FILE_ID.Trim() == "")
            {
                this.Tips.AppendLine("附件ID不能为空");
                return false;
            }
            //备注1
            if (tMisMonitorResultAtt.REMARK_1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tMisMonitorResultAtt.REMARK_2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tMisMonitorResultAtt.REMARK_3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tMisMonitorResultAtt.REMARK_4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tMisMonitorResultAtt.REMARK_5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
