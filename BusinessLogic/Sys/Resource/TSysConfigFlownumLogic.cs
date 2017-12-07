using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.Resource;
using i3.DataAccess.Sys.Resource;

namespace i3.BusinessLogic.Sys.Resource
{
    /// <summary>
    /// 功能：采样分析监测流程排序配置
    /// 创建日期：2013-04-01
    /// 创建人：邵世卓
    /// </summary>
    public class TSysConfigFlownumLogic : LogicBase
    {

        TSysConfigFlownumVo tSysConfigFlownum = new TSysConfigFlownumVo();
        TSysConfigFlownumAccess access;

        public TSysConfigFlownumLogic()
        {
            access = new TSysConfigFlownumAccess();
        }

        public TSysConfigFlownumLogic(TSysConfigFlownumVo _tSysConfigFlownum)
        {
            tSysConfigFlownum = _tSysConfigFlownum;
            access = new TSysConfigFlownumAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysConfigFlownum">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysConfigFlownumVo tSysConfigFlownum)
        {
            return access.GetSelectResultCount(tSysConfigFlownum);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysConfigFlownumVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysConfigFlownum">对象条件</param>
        /// <returns>对象</returns>
        public TSysConfigFlownumVo Details(TSysConfigFlownumVo tSysConfigFlownum)
        {
            return access.Details(tSysConfigFlownum);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysConfigFlownum">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysConfigFlownumVo> SelectByObject(TSysConfigFlownumVo tSysConfigFlownum, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysConfigFlownum, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysConfigFlownum">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysConfigFlownumVo tSysConfigFlownum, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysConfigFlownum, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysConfigFlownum"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysConfigFlownumVo tSysConfigFlownum)
        {
            return access.SelectByTable(tSysConfigFlownum);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysConfigFlownum">对象</param>
        /// <returns></returns>
        public TSysConfigFlownumVo SelectByObject(TSysConfigFlownumVo tSysConfigFlownum)
        {
            return access.SelectByObject(tSysConfigFlownum);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysConfigFlownumVo tSysConfigFlownum)
        {
            return access.Create(tSysConfigFlownum);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysConfigFlownum">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysConfigFlownumVo tSysConfigFlownum)
        {
            return access.Edit(tSysConfigFlownum);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysConfigFlownum_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tSysConfigFlownum_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysConfigFlownumVo tSysConfigFlownum_UpdateSet, TSysConfigFlownumVo tSysConfigFlownum_UpdateWhere)
        {
            return access.Edit(tSysConfigFlownum_UpdateSet, tSysConfigFlownum_UpdateWhere);
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
        public bool Delete(TSysConfigFlownumVo tSysConfigFlownum)
        {
            return access.Delete(tSysConfigFlownum);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tSysConfigFlownum.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //环节名称
            if (tSysConfigFlownum.FLOW_NAME.Trim() == "")
            {
                this.Tips.AppendLine("环节名称不能为空");
                return false;
            }
            //上一环节编号
            if (tSysConfigFlownum.FIRST_FLOW_CODE.Trim() == "")
            {
                this.Tips.AppendLine("上一环节编号不能为空");
                return false;
            }
            //下一环节编号
            if (tSysConfigFlownum.SECOND_FLOW_CODE.Trim() == "")
            {
                this.Tips.AppendLine("下一环节编号不能为空");
                return false;
            }
            //环节序号
            if (tSysConfigFlownum.FLOW_NUM.Trim() == "")
            {
                this.Tips.AppendLine("环节序号不能为空");
                return false;
            }
            //是否并行
            if (tSysConfigFlownum.IS_COL.Trim() == "")
            {
                this.Tips.AppendLine("是否并行不能为空");
                return false;
            }
            //备注1
            if (tSysConfigFlownum.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysConfigFlownum.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysConfigFlownum.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
