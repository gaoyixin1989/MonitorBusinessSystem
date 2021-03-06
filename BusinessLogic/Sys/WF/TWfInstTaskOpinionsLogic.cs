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
    /// 功能：工作流实例环节附属评论表
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfInstTaskOpinionsLogic : LogicBase
    {

        TWfInstTaskOpinionsVo tWfInstTaskOpinions = new TWfInstTaskOpinionsVo();
        TWfInstTaskOpinionsAccess access;

        public TWfInstTaskOpinionsLogic()
        {
            access = new TWfInstTaskOpinionsAccess();
        }

        public TWfInstTaskOpinionsLogic(TWfInstTaskOpinionsVo _tWfInstTaskOpinions)
        {
            tWfInstTaskOpinions = _tWfInstTaskOpinions;
            access = new TWfInstTaskOpinionsAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfInstTaskOpinions">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            return access.GetSelectResultCount(tWfInstTaskOpinions);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfInstTaskOpinionsVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfInstTaskOpinions">对象条件</param>
        /// <returns>对象</returns>
        public TWfInstTaskOpinionsVo Details(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            return access.Details(tWfInstTaskOpinions);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfInstTaskOpinions">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfInstTaskOpinionsVo> SelectByObject(TWfInstTaskOpinionsVo tWfInstTaskOpinions, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfInstTaskOpinions, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfInstTaskOpinions">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfInstTaskOpinionsVo tWfInstTaskOpinions, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfInstTaskOpinions, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstTaskOpinions"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            return access.SelectByTable(tWfInstTaskOpinions);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载【和Detail左连接】
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfInstTaskOpinions"></param>
        /// <returns></returns>
        public DataTable SelectByTableJoinDetail(string strInstWFID)
        {
            return access.SelectByTableJoinDetail(strInstWFID);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfInstTaskOpinions">对象</param>
        /// <returns></returns>
        public TWfInstTaskOpinionsVo SelectByObject(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            return access.SelectByObject(tWfInstTaskOpinions);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            return access.Create(tWfInstTaskOpinions);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskOpinions">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            return access.Edit(tWfInstTaskOpinions);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfInstTaskOpinions_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfInstTaskOpinions_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfInstTaskOpinionsVo tWfInstTaskOpinions_UpdateSet, TWfInstTaskOpinionsVo tWfInstTaskOpinions_UpdateWhere)
        {
            return access.Edit(tWfInstTaskOpinions_UpdateSet, tWfInstTaskOpinions_UpdateWhere);
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
        public bool Delete(TWfInstTaskOpinionsVo tWfInstTaskOpinions)
        {
            return access.Delete(tWfInstTaskOpinions);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfInstTaskOpinions.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //环节实例编号
            if (tWfInstTaskOpinions.WF_INST_TASK_ID.Trim() == "")
            {
                this.Tips.AppendLine("环节实例编号不能为空");
                return false;
            }
            //流程实例编号
            if (tWfInstTaskOpinions.WF_INST_ID.Trim() == "")
            {
                this.Tips.AppendLine("流程实例编号不能为空");
                return false;
            }
            //评论内容
            if (tWfInstTaskOpinions.WF_IT_OPINION.Trim() == "")
            {
                this.Tips.AppendLine("评论内容不能为空");
                return false;
            }
            //评论类型
            if (tWfInstTaskOpinions.WF_IT_OPINION_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("评论类型不能为空");
                return false;
            }
            //评论人
            if (tWfInstTaskOpinions.WF_IT_OPINION_USER.Trim() == "")
            {
                this.Tips.AppendLine("评论人不能为空");
                return false;
            }
            //显示方式(只显示上一条,全显示)
            if (tWfInstTaskOpinions.WF_IT_SHOW_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("显示方式(只显示上一条,全显示)不能为空");
                return false;
            }
            //删除标记
            if (tWfInstTaskOpinions.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }

            return true;
        }

    }
}
