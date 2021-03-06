using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.EXAMINE;
using i3.DataAccess.Channels.OA.EXAMINE;

namespace i3.BusinessLogic.Channels.OA.EXAMINE
{
    /// <summary>
    /// 功能：人员考核
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaExamineInfoLogic : LogicBase
    {

	TOaExamineInfoVo tOaExamineInfo = new TOaExamineInfoVo();
    TOaExamineInfoAccess access;
		
	public TOaExamineInfoLogic()
	{
		  access = new TOaExamineInfoAccess();  
	}
		
	public TOaExamineInfoLogic(TOaExamineInfoVo _tOaExamineInfo)
	{
		tOaExamineInfo = _tOaExamineInfo;
		access = new TOaExamineInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaExamineInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaExamineInfoVo tOaExamineInfo)
        {
            return access.GetSelectResultCount(tOaExamineInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaExamineInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaExamineInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaExamineInfoVo Details(TOaExamineInfoVo tOaExamineInfo)
        {
            return access.Details(tOaExamineInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaExamineInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaExamineInfoVo> SelectByObject(TOaExamineInfoVo tOaExamineInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaExamineInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaExamineInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaExamineInfoVo tOaExamineInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaExamineInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaExamineInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaExamineInfoVo tOaExamineInfo)
        {
            return access.SelectByTable(tOaExamineInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaExamineInfo">对象</param>
        /// <returns></returns>
        public TOaExamineInfoVo SelectByObject(TOaExamineInfoVo tOaExamineInfo)
        {
            return access.SelectByObject(tOaExamineInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaExamineInfoVo tOaExamineInfo)
        {
            return access.Create(tOaExamineInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaExamineInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaExamineInfoVo tOaExamineInfo)
        {
            return access.Edit(tOaExamineInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaExamineInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaExamineInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaExamineInfoVo tOaExamineInfo_UpdateSet, TOaExamineInfoVo tOaExamineInfo_UpdateWhere)
        {
            return access.Edit(tOaExamineInfo_UpdateSet, tOaExamineInfo_UpdateWhere);
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
        public bool Delete(TOaExamineInfoVo tOaExamineInfo)
        {
            return access.Delete(tOaExamineInfo);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaExamineInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //用户ID
	  if (tOaExamineInfo.USERID.Trim() == "")
            {
                this.Tips.AppendLine("用户ID不能为空");
                return false;
            }
	  //考核类型，1事业单位工作人员年度考核，2专业技术人员年度考核
	  if (tOaExamineInfo.EXAMINE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("考核类型，1事业单位工作人员年度考核，2专业技术人员年度考核不能为空");
                return false;
            }
	  //考核时间
	  if (tOaExamineInfo.EXAMINE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("考核时间不能为空");
                return false;
            }
	  //考核年度
	  if (tOaExamineInfo.EXAMINE_YEAR.Trim() == "")
            {
                this.Tips.AppendLine("考核年度不能为空");
                return false;
            }
	  //考核状态,1未发送，2待审批，3已审批
	  if (tOaExamineInfo.EXAMINE_STATUS.Trim() == "")
            {
                this.Tips.AppendLine("考核状态,1未发送，2待审批，3已审批不能为空");
                return false;
            }
	  //部门考核评语
	  if (tOaExamineInfo.DEPT_APP.Trim() == "")
            {
                this.Tips.AppendLine("部门考核评语不能为空");
                return false;
            }
	  //单位考核评语
	  if (tOaExamineInfo.LEADER_APP.Trim() == "")
            {
                this.Tips.AppendLine("单位考核评语不能为空");
                return false;
            }
	  //主管单位意见
	  if (tOaExamineInfo.SUPERIOR_APP.Trim() == "")
            {
                this.Tips.AppendLine("主管单位意见不能为空");
                return false;
            }
	  //个人意见
	  if (tOaExamineInfo.OPINION.Trim() == "")
            {
                this.Tips.AppendLine("个人意见不能为空");
                return false;
            }
	  //复核或申诉情况说明
	  if (tOaExamineInfo.APPEAL.Trim() == "")
            {
                this.Tips.AppendLine("复核或申诉情况说明不能为空");
                return false;
            }
	  //备注1
	  if (tOaExamineInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaExamineInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaExamineInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaExamineInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaExamineInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
