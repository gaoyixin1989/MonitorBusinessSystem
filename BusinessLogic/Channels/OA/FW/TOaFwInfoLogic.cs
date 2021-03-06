using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.FW;
using i3.DataAccess.Channels.OA.FW;

namespace i3.BusinessLogic.Channels.OA.FW
{
    /// <summary>
    /// 功能：发文信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaFwInfoLogic : LogicBase
    {

	TOaFwInfoVo tOaFwInfo = new TOaFwInfoVo();
    TOaFwInfoAccess access;
		
	public TOaFwInfoLogic()
	{
		  access = new TOaFwInfoAccess();  
	}
		
	public TOaFwInfoLogic(TOaFwInfoVo _tOaFwInfo)
	{
		tOaFwInfo = _tOaFwInfo;
		access = new TOaFwInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaFwInfoVo tOaFwInfo)
        {
            return access.GetSelectResultCount(tOaFwInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaFwInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaFwInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaFwInfoVo Details(TOaFwInfoVo tOaFwInfo)
        {
            return access.Details(tOaFwInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaFwInfoVo> SelectByObject(TOaFwInfoVo tOaFwInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaFwInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable,指定用户的传阅发文
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForRead(TOaFwInfoVo tOaFwInfo, string strUserID, int iIndex, int iCount)
        {
            return access.SelectByTable_ForRead(tOaFwInfo, strUserID, iIndex, iCount);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaFwInfoVo tOaFwInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaFwInfo, iIndex, iCount);
        }
        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectTable(TOaFwInfoVo tOaFwInfo, int iIndex, int iCount)
        {
            return access.SelectTable(tOaFwInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaFwInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaFwInfoVo tOaFwInfo)
        {
            return access.SelectByTable(tOaFwInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <returns></returns>
        public TOaFwInfoVo SelectByObject(TOaFwInfoVo tOaFwInfo)
        {
            return access.SelectByObject(tOaFwInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaFwInfoVo tOaFwInfo)
        {
            return access.Create(tOaFwInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaFwInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaFwInfoVo tOaFwInfo)
        {
            return access.Edit(tOaFwInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaFwInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaFwInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaFwInfoVo tOaFwInfo_UpdateSet, TOaFwInfoVo tOaFwInfo_UpdateWhere)
        {
            return access.Edit(tOaFwInfo_UpdateSet, tOaFwInfo_UpdateWhere);
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
        public bool Delete(TOaFwInfoVo tOaFwInfo)
        {
            return access.Delete(tOaFwInfo);
        }
	
              /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaFwInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectCount(TOaFwInfoVo tOaFwInfo)
        {
            return access.GetSelectCount(tOaFwInfo);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaFwInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //原文编号
	  if (tOaFwInfo.YWNO.Trim() == "")
            {
                this.Tips.AppendLine("原文编号不能为空");
                return false;
            }
	  //发文编号
	  if (tOaFwInfo.FWNO.Trim() == "")
            {
                this.Tips.AppendLine("发文编号不能为空");
                return false;
            }
	  //发文标题
	  if (tOaFwInfo.FW_TITLE.Trim() == "")
            {
                this.Tips.AppendLine("发文标题不能为空");
                return false;
            }
	  //主办单位
	  if (tOaFwInfo.ZB_DEPT.Trim() == "")
            {
                this.Tips.AppendLine("主办单位不能为空");
                return false;
            }
	  //密级
	  if (tOaFwInfo.MJ.Trim() == "")
            {
                this.Tips.AppendLine("密级不能为空");
                return false;
            }
	  //发文日期
	  if (tOaFwInfo.FW_DATE.Trim() == "")
            {
                this.Tips.AppendLine("发文日期不能为空");
                return false;
            }
	  //拟稿人
	  if (tOaFwInfo.DRAFT_ID.Trim() == "")
            {
                this.Tips.AppendLine("拟稿人不能为空");
                return false;
            }
	  //拟稿日期
	  if (tOaFwInfo.DRAFT_DATE.Trim() == "")
            {
                this.Tips.AppendLine("拟稿日期不能为空");
                return false;
            }
	  //核稿人ID
	  if (tOaFwInfo.APP_ID.Trim() == "")
            {
                this.Tips.AppendLine("核稿人ID不能为空");
                return false;
            }
	  //核稿日期
	  if (tOaFwInfo.APP_DATE.Trim() == "")
            {
                this.Tips.AppendLine("核稿日期不能为空");
                return false;
            }
	  //核稿意见
	  if (tOaFwInfo.APP_INFO.Trim() == "")
            {
                this.Tips.AppendLine("核稿意见不能为空");
                return false;
            }
	  //签发人ID
	  if (tOaFwInfo.ISSUE_ID.Trim() == "")
            {
                this.Tips.AppendLine("签发人ID不能为空");
                return false;
            }
	  //签发日期
	  if (tOaFwInfo.ISSUE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("签发日期不能为空");
                return false;
            }
	  //签发意见
	  if (tOaFwInfo.ISSUE_INFO.Trim() == "")
            {
                this.Tips.AppendLine("签发意见不能为空");
                return false;
            }
	  //登记人ID
	  if (tOaFwInfo.REG_ID.Trim() == "")
            {
                this.Tips.AppendLine("登记人ID不能为空");
                return false;
            }
	  //登记日期
	  if (tOaFwInfo.REG_DATE.Trim() == "")
            {
                this.Tips.AppendLine("登记日期不能为空");
                return false;
            }
	  //校对人
	  if (tOaFwInfo.CHECK_ID.Trim() == "")
            {
                this.Tips.AppendLine("校对人不能为空");
                return false;
            }
	  //用印人ID
	  if (tOaFwInfo.SEAL_ID.Trim() == "")
            {
                this.Tips.AppendLine("用印人ID不能为空");
                return false;
            }
	  //缮印人ID
	  if (tOaFwInfo.PRINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("缮印人ID不能为空");
                return false;
            }
	  //归档人ID
	  if (tOaFwInfo.PIGONHOLE_ID.Trim() == "")
            {
                this.Tips.AppendLine("归档人ID不能为空");
                return false;
            }
	  //归档时间
	  if (tOaFwInfo.PIGONHOLE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("归档时间不能为空");
                return false;
            }
	  //备注1
	  if (tOaFwInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaFwInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaFwInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaFwInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaFwInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
