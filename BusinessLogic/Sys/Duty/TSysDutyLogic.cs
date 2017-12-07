using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.Duty;
using i3.DataAccess.Sys.Duty;

namespace i3.BusinessLogic.Sys.Duty
{
    /// <summary>
    /// 功能：岗位职责
    /// 创建日期：2012-11-12
    /// 创建人：胡方扬
    /// </summary>
    public class TSysDutyLogic : LogicBase
    {

        TSysDutyVo tSysDuty = new TSysDutyVo();
        TSysDutyAccess access;

        public TSysDutyLogic()
        {
            access = new TSysDutyAccess();
        }

        public TSysDutyLogic(TSysDutyVo _tSysDuty)
        {
            tSysDuty = _tSysDuty;
            access = new TSysDutyAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysDuty">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysDutyVo tSysDuty)
        {
            return access.GetSelectResultCount(tSysDuty);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysDutyVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysDuty">对象条件</param>
        /// <returns>对象</returns>
        public TSysDutyVo Details(TSysDutyVo tSysDuty)
        {
            return access.Details(tSysDuty);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysDuty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysDutyVo> SelectByObject(TSysDutyVo tSysDuty, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysDuty, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysDuty">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysDutyVo tSysDuty, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysDuty, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysDutyVo tSysDuty)
        {
            return access.SelectByTable(tSysDuty);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysDuty">对象</param>
        /// <returns></returns>
        public TSysDutyVo SelectByObject(TSysDutyVo tSysDuty)
        {
            return access.SelectByObject(tSysDuty);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysDutyVo tSysDuty)
        {
            return access.Create(tSysDuty);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysDuty">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysDutyVo tSysDuty)
        {
            return access.Edit(tSysDuty);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysDuty_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tSysDuty_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysDutyVo tSysDuty_UpdateSet, TSysDutyVo tSysDuty_UpdateWhere)
        {
            return access.Edit(tSysDuty_UpdateSet, tSysDuty_UpdateWhere);
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
        public bool Delete(TSysDutyVo tSysDuty)
        {
            return access.Delete(tSysDuty);
        }

        /// <summary>
        /// 获取指定监测类型的岗位职责人员
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public DataTable SelectTableDutyUser(TSysDutyVo tSysDuty)
        {
            return access.SelectTableDutyUser(tSysDuty);
        }

        /// <summary>
        /// 获取已设置采样任务的人员列表
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public DataTable SelectByUnionTable(TSysDutyVo tSysDuty, int iIndex, int iCount)
        {
            return access.SelectByUnionTable(tSysDuty, iIndex, iCount);
        }

        /// <summary>
        /// 功能描述：获取所有已设置采样任务的人员列表
        /// 创建时间：2013-2-28
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public DataTable SelectByUnionTableForWhere(TSysDutyVo tSysDuty, int iIndex, int iCount)
        {
            return access.SelectByUnionTableForWhere(tSysDuty, iIndex, iCount);

        }
        /// <summary>
        /// 获取已设置采样任务的人员列表总数
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public int GetSelectByUnionResultCount(TSysDutyVo tSysDuty)
        {
            return access.GetSelectByUnionResultCount(tSysDuty);
        }

                /// <summary>
        /// 创建原因：获取指定监测类别  指定岗位职责的项目人员
        /// 创建人：胡方扬
        /// 创建日期：20130-07-18
        /// </summary>
        /// <param name="tSysDuty"></param>
        /// <returns></returns>
        public DataTable GetDutyUser(TSysDutyVo tSysDuty) {
            return access.GetDutyUser(tSysDuty);

        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tSysDuty.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //监测类别ID
            if (tSysDuty.MONITOR_TYPE_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别ID不能为空");
                return false;
            }
            //监测项目ID
            if (tSysDuty.MONITOR_ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项目ID不能为空");
                return false;
            }

            //岗位职责编码(字典项目获取)
            if (tSysDuty.DICT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("岗位职责编码(字典项目获取)不能为空");
                return false;
            }

            //所有者
            if (tSysDuty.OWNER.Trim() == "")
            {
                this.Tips.AppendLine("所有者不能为空");
                return false;
            }
            //备注
            if (tSysDuty.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tSysDuty.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysDuty.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysDuty.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tSysDuty.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tSysDuty.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
