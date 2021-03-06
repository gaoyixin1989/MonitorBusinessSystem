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
    /// 功能：培训申请
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaTrainApplyLogic : LogicBase
    {

	TOaTrainApplyVo tOaTrainApply = new TOaTrainApplyVo();
    TOaTrainApplyAccess access;
		
	public TOaTrainApplyLogic()
	{
		  access = new TOaTrainApplyAccess();  
	}
		
	public TOaTrainApplyLogic(TOaTrainApplyVo _tOaTrainApply)
	{
		tOaTrainApply = _tOaTrainApply;
		access = new TOaTrainApplyAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaTrainApply">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaTrainApplyVo tOaTrainApply)
        {
            return access.GetSelectResultCount(tOaTrainApply);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaTrainApplyVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaTrainApply">对象条件</param>
        /// <returns>对象</returns>
        public TOaTrainApplyVo Details(TOaTrainApplyVo tOaTrainApply)
        {
            return access.Details(tOaTrainApply);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaTrainApply">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaTrainApplyVo> SelectByObject(TOaTrainApplyVo tOaTrainApply, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaTrainApply, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaTrainApply">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaTrainApplyVo tOaTrainApply, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaTrainApply, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaTrainApply"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaTrainApplyVo tOaTrainApply)
        {
            return access.SelectByTable(tOaTrainApply);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaTrainApply">对象</param>
        /// <returns></returns>
        public TOaTrainApplyVo SelectByObject(TOaTrainApplyVo tOaTrainApply)
        {
            return access.SelectByObject(tOaTrainApply);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaTrainApplyVo tOaTrainApply)
        {
            return access.Create(tOaTrainApply);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainApply">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainApplyVo tOaTrainApply)
        {
            return access.Edit(tOaTrainApply);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainApply_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaTrainApply_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainApplyVo tOaTrainApply_UpdateSet, TOaTrainApplyVo tOaTrainApply_UpdateWhere)
        {
            return access.Edit(tOaTrainApply_UpdateSet, tOaTrainApply_UpdateWhere);
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
        public bool Delete(TOaTrainApplyVo tOaTrainApply)
        {
            return access.Delete(tOaTrainApply);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaTrainApply.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //计划内或计划外
	  if (tOaTrainApply.IF_PALN.Trim() == "")
            {
                this.Tips.AppendLine("计划内或计划外不能为空");
                return false;
            }
	  //培训项目
	  if (tOaTrainApply.TRAIN_PROJECT.Trim() == "")
            {
                this.Tips.AppendLine("培训项目不能为空");
                return false;
            }
	  //培训内容
	  if (tOaTrainApply.TRAIN_CONTENT.Trim() == "")
            {
                this.Tips.AppendLine("培训内容不能为空");
                return false;
            }
	  //天时
	  if (tOaTrainApply.TRAIN_DAYS.Trim() == "")
            {
                this.Tips.AppendLine("天时不能为空");
                return false;
            }
	  //学时
	  if (tOaTrainApply.TRAIN_HOURS.Trim() == "")
            {
                this.Tips.AppendLine("学时不能为空");
                return false;
            }
	  //开始时间
	  if (tOaTrainApply.BEGIN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("开始时间不能为空");
                return false;
            }
	  //结束时间
	  if (tOaTrainApply.FINISH_DATE.Trim() == "")
            {
                this.Tips.AppendLine("结束时间不能为空");
                return false;
            }
	  //培训单位
	  if (tOaTrainApply.TRAIN_DEPT.Trim() == "")
            {
                this.Tips.AppendLine("培训单位不能为空");
                return false;
            }
	  //培训地点
	  if (tOaTrainApply.TRAIN_PLACE.Trim() == "")
            {
                this.Tips.AppendLine("培训地点不能为空");
                return false;
            }
	  //培训教师
	  if (tOaTrainApply.TRAIN_TEACHER.Trim() == "")
            {
                this.Tips.AppendLine("培训教师不能为空");
                return false;
            }
	  //联系人
	  if (tOaTrainApply.LINK_MAN.Trim() == "")
            {
                this.Tips.AppendLine("联系人不能为空");
                return false;
            }
	  //联系电话
	  if (tOaTrainApply.LINK_TEL.Trim() == "")
            {
                this.Tips.AppendLine("联系电话不能为空");
                return false;
            }
	  //发证单位
	  if (tOaTrainApply.CERTIFICATION_DEPT.Trim() == "")
            {
                this.Tips.AppendLine("发证单位不能为空");
                return false;
            }
	  //考核办法,1笔试、2口试、3实际操作，复选
	  if (tOaTrainApply.TEST_METHOD.Trim() == "")
            {
                this.Tips.AppendLine("考核办法,1笔试、2口试、3实际操作，复选不能为空");
                return false;
            }
	  //有效性评价
	  if (tOaTrainApply.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("有效性评价不能为空");
                return false;
            }
	  //部门审批人ID
	  if (tOaTrainApply.DEPT_APP_ID.Trim() == "")
            {
                this.Tips.AppendLine("部门审批人ID不能为空");
                return false;
            }
	  //部门审批时间
	  if (tOaTrainApply.DEPT_APP_DATE.Trim() == "")
            {
                this.Tips.AppendLine("部门审批时间不能为空");
                return false;
            }
	  //部门审批意见
	  if (tOaTrainApply.DEPT_APP_INFO.Trim() == "")
            {
                this.Tips.AppendLine("部门审批意见不能为空");
                return false;
            }
	  //站长审批人ID
	  if (tOaTrainApply.LEADER_APP_ID.Trim() == "")
            {
                this.Tips.AppendLine("站长审批人ID不能为空");
                return false;
            }
	  //站长审批时间
	  if (tOaTrainApply.LEADER_APP_DATE.Trim() == "")
            {
                this.Tips.AppendLine("站长审批时间不能为空");
                return false;
            }
	  //站长审批意见
	  if (tOaTrainApply.LEADER_APP_INFO.Trim() == "")
            {
                this.Tips.AppendLine("站长审批意见不能为空");
                return false;
            }
	  //备注1
	  if (tOaTrainApply.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaTrainApply.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaTrainApply.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaTrainApply.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaTrainApply.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
