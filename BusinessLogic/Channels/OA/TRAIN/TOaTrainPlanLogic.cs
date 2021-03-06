using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.TRAIN;
using i3.DataAccess.Channels.OA.TRAIN;

namespace i3.BusinessLogic.Channels.OA.TRAIN
{
    /// <summary>
    /// 功能：培训计划
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaTrainPlanLogic : LogicBase
    {

	TOaTrainPlanVo tOaTrainPlan = new TOaTrainPlanVo();
    TOaTrainPlanAccess access;
		
	public TOaTrainPlanLogic()
	{
		  access = new TOaTrainPlanAccess();  
	}
		
	public TOaTrainPlanLogic(TOaTrainPlanVo _tOaTrainPlan)
	{
		tOaTrainPlan = _tOaTrainPlan;
		access = new TOaTrainPlanAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaTrainPlan">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaTrainPlanVo tOaTrainPlan)
        {
            return access.GetSelectResultCount(tOaTrainPlan);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaTrainPlanVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaTrainPlan">对象条件</param>
        /// <returns>对象</returns>
        public TOaTrainPlanVo Details(TOaTrainPlanVo tOaTrainPlan)
        {
            return access.Details(tOaTrainPlan);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaTrainPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaTrainPlanVo> SelectByObject(TOaTrainPlanVo tOaTrainPlan, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaTrainPlan, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaTrainPlan">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaTrainPlanVo tOaTrainPlan, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaTrainPlan, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaTrainPlan"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaTrainPlanVo tOaTrainPlan)
        {
            return access.SelectByTable(tOaTrainPlan);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaTrainPlan">对象</param>
        /// <returns></returns>
        public TOaTrainPlanVo SelectByObject(TOaTrainPlanVo tOaTrainPlan)
        {
            return access.SelectByObject(tOaTrainPlan);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaTrainPlanVo tOaTrainPlan)
        {
            return access.Create(tOaTrainPlan);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainPlan">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainPlanVo tOaTrainPlan)
        {
            return access.Edit(tOaTrainPlan);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainPlan_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaTrainPlan_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainPlanVo tOaTrainPlan_UpdateSet, TOaTrainPlanVo tOaTrainPlan_UpdateWhere)
        {
            return access.Edit(tOaTrainPlan_UpdateSet, tOaTrainPlan_UpdateWhere);
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
        public bool Delete(TOaTrainPlanVo tOaTrainPlan)
        {
            return access.Delete(tOaTrainPlan);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaTrainPlan.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //培训分类
	  if (tOaTrainPlan.TRAIN_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("培训分类不能为空");
                return false;
            }
	  //培训对象
	  if (tOaTrainPlan.TRAIN_TO.Trim() == "")
            {
                this.Tips.AppendLine("培训对象不能为空");
                return false;
            }
	  //培训内容
	  if (tOaTrainPlan.TRAIN_INFO.Trim() == "")
            {
                this.Tips.AppendLine("培训内容不能为空");
                return false;
            }
	  //培训目标
	  if (tOaTrainPlan.TRAIN_TARGET.Trim() == "")
            {
                this.Tips.AppendLine("培训目标不能为空");
                return false;
            }
	  //培训时间
	  if (tOaTrainPlan.TRAIN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("培训时间不能为空");
                return false;
            }
	  //负责部门
	  if (tOaTrainPlan.DEPT_ID.Trim() == "")
            {
                this.Tips.AppendLine("负责部门不能为空");
                return false;
            }
	  //考核办法
	  if (tOaTrainPlan.EXAMINE_METHOD.Trim() == "")
            {
                this.Tips.AppendLine("考核办法不能为空");
                return false;
            }
	  //计划年度
	  if (tOaTrainPlan.PLAN_YEAR.Trim() == "")
            {
                this.Tips.AppendLine("计划年度不能为空");
                return false;
            }
	  //编制人
	  if (tOaTrainPlan.DRAFT_ID.Trim() == "")
            {
                this.Tips.AppendLine("编制人不能为空");
                return false;
            }
	  //编制时间
	  if (tOaTrainPlan.DRAFT_DATE.Trim() == "")
            {
                this.Tips.AppendLine("编制时间不能为空");
                return false;
            }
	  //审批人
	  if (tOaTrainPlan.APP_ID.Trim() == "")
            {
                this.Tips.AppendLine("审批人不能为空");
                return false;
            }
	  //审批时间
	  if (tOaTrainPlan.APP_DATE.Trim() == "")
            {
                this.Tips.AppendLine("审批时间不能为空");
                return false;
            }
	  //审批意见
	  if (tOaTrainPlan.APP_INFO.Trim() == "")
            {
                this.Tips.AppendLine("审批意见不能为空");
                return false;
            }
	  //审批结果
	  if (tOaTrainPlan.APP_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("审批结果不能为空");
                return false;
            }
	  //备注1
	  if (tOaTrainPlan.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaTrainPlan.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaTrainPlan.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaTrainPlan.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaTrainPlan.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
