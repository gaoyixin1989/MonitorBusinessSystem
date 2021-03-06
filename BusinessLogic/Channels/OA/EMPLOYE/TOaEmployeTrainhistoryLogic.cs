using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.EMPLOYE;
using i3.DataAccess.Channels.OA.EMPLOYE;

namespace i3.BusinessLogic.Channels.OA.EMPLOYE
{
    /// <summary>
    /// 功能：员工培训履历
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeTrainhistoryLogic : LogicBase
    {

	TOaEmployeTrainhistoryVo tOaEmployeTrainhistory = new TOaEmployeTrainhistoryVo();
    TOaEmployeTrainhistoryAccess access;
		
	public TOaEmployeTrainhistoryLogic()
	{
		  access = new TOaEmployeTrainhistoryAccess();  
	}
		
	public TOaEmployeTrainhistoryLogic(TOaEmployeTrainhistoryVo _tOaEmployeTrainhistory)
	{
		tOaEmployeTrainhistory = _tOaEmployeTrainhistory;
		access = new TOaEmployeTrainhistoryAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            return access.GetSelectResultCount(tOaEmployeTrainhistory);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeTrainhistoryVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeTrainhistoryVo Details(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            return access.Details(tOaEmployeTrainhistory);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeTrainhistoryVo> SelectByObject(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaEmployeTrainhistory, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaEmployeTrainhistory, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeTrainhistory"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            return access.SelectByTable(tOaEmployeTrainhistory);
        }

        /// <summary>
        /// 获取培训结果附件
        /// </summary>
        /// <param name="tOaEmployeTrainhistory"></param>
        /// <param name="strFileType"></param>
        /// <returns></returns>
        public DataTable SelectByTrainAttTable(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory,string strFileType)
        {
            return access.SelectByTrainAttTable(tOaEmployeTrainhistory, strFileType);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">对象</param>
        /// <returns></returns>
        public TOaEmployeTrainhistoryVo SelectByObject(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            return access.SelectByObject(tOaEmployeTrainhistory);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            return access.Create(tOaEmployeTrainhistory);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeTrainhistory">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            return access.Edit(tOaEmployeTrainhistory);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeTrainhistory_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeTrainhistory_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory_UpdateSet, TOaEmployeTrainhistoryVo tOaEmployeTrainhistory_UpdateWhere)
        {
            return access.Edit(tOaEmployeTrainhistory_UpdateSet, tOaEmployeTrainhistory_UpdateWhere);
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
        public bool Delete(TOaEmployeTrainhistoryVo tOaEmployeTrainhistory)
        {
            return access.Delete(tOaEmployeTrainhistory);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaEmployeTrainhistory.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //员工编号
	  if (tOaEmployeTrainhistory.EMPLOYEID.Trim() == "")
            {
                this.Tips.AppendLine("员工编号不能为空");
                return false;
            }
	  //所在单位
	  if (tOaEmployeTrainhistory.ATT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("所在单位不能为空");
                return false;
            }
	  //附件路径
	  if (tOaEmployeTrainhistory.ATT_URL.Trim() == "")
            {
                this.Tips.AppendLine("附件路径不能为空");
                return false;
            }
	  //附件说明
	  if (tOaEmployeTrainhistory.ATT_INFO.Trim() == "")
            {
                this.Tips.AppendLine("附件说明不能为空");
                return false;
            }
	  //培训结果
      if (tOaEmployeTrainhistory.TRAIN_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("培训结果不能为空");
                return false;
            }
	  //证书号
      if (tOaEmployeTrainhistory.BOOK_NUM.Trim() == "")
            {
                this.Tips.AppendLine("证书号不能为空");
                return false;
            }
	  //备注1
	  if (tOaEmployeTrainhistory.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaEmployeTrainhistory.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaEmployeTrainhistory.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaEmployeTrainhistory.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaEmployeTrainhistory.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
