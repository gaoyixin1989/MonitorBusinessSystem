using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.PART;
using i3.DataAccess.Channels.OA.PART;

namespace i3.BusinessLogic.Channels.OA.PART
{
    /// <summary>
    /// 功能：物料领用明细
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaPartCollarLogic : LogicBase
    {

	TOaPartCollarVo tOaPartCollar = new TOaPartCollarVo();
    TOaPartCollarAccess access;
		
	public TOaPartCollarLogic()
	{
		  access = new TOaPartCollarAccess();  
	}
		
	public TOaPartCollarLogic(TOaPartCollarVo _tOaPartCollar)
	{
		tOaPartCollar = _tOaPartCollar;
		access = new TOaPartCollarAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartCollar">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartCollarVo tOaPartCollar)
        {
            return access.GetSelectResultCount(tOaPartCollar);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartCollarVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartCollar">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartCollarVo Details(TOaPartCollarVo tOaPartCollar)
        {
            return access.Details(tOaPartCollar);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartCollar">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartCollarVo> SelectByObject(TOaPartCollarVo tOaPartCollar, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaPartCollar, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartCollar">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartCollarVo tOaPartCollar, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaPartCollar, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartCollar"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartCollarVo tOaPartCollar)
        {
            return access.SelectByTable(tOaPartCollar);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartCollar">对象</param>
        /// <returns></returns>
        public TOaPartCollarVo SelectByObject(TOaPartCollarVo tOaPartCollar)
        {
            return access.SelectByObject(tOaPartCollar);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartCollarVo tOaPartCollar)
        {
            return access.Create(tOaPartCollar);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartCollar">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartCollarVo tOaPartCollar)
        {
            return access.Edit(tOaPartCollar);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartCollar_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaPartCollar_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartCollarVo tOaPartCollar_UpdateSet, TOaPartCollarVo tOaPartCollar_UpdateWhere)
        {
            return access.Edit(tOaPartCollar_UpdateSet, tOaPartCollar_UpdateWhere);
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
        public bool Delete(TOaPartCollarVo tOaPartCollar)
        {
            return access.Delete(tOaPartCollar);
        }


        public DataTable SelectUnionPartByTable(TOaPartCollarVo tOaPartCollar, TOaPartInfoVo tOaPartInfor,int iIndex,int iCount)
        {
            return access.SelectUnionPartByTable(tOaPartCollar,tOaPartInfor,iIndex,iCount);
        }

        public int GetUnionPartByTableCount(TOaPartCollarVo tOaPartCollar, TOaPartInfoVo tOaPartInfor)
        {
            return access.GetUnionPartByTableCount(tOaPartCollar, tOaPartInfor);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaPartCollar.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //物料ID
	  if (tOaPartCollar.PART_ID.Trim() == "")
            {
                this.Tips.AppendLine("物料ID不能为空");
                return false;
            }
	  //领用数量
	  if (tOaPartCollar.USED_QUANTITY.Trim() == "")
            {
                this.Tips.AppendLine("领用数量不能为空");
                return false;
            }
	  //领用人ID
	  if (tOaPartCollar.USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("领用人ID不能为空");
                return false;
            }
	  //领用日期
	  if (tOaPartCollar.LASTIN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("领用日期不能为空");
                return false;
            }
	  //领用理由
	  if (tOaPartCollar.REASON.Trim() == "")
            {
                this.Tips.AppendLine("领用理由不能为空");
                return false;
            }
	  //备注1
	  if (tOaPartCollar.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaPartCollar.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaPartCollar.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaPartCollar.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaPartCollar.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
