using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Apparatus;
using i3.DataAccess.Channels.Base.Apparatus;

namespace i3.BusinessLogic.Channels.Base.Apparatus
{
    /// <summary>
    /// 功能：仪器资料备份
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusFilebakLogic : LogicBase
    {

	TBaseApparatusFilebakVo tBaseApparatusFilebak = new TBaseApparatusFilebakVo();
    TBaseApparatusFilebakAccess access;
		
	public TBaseApparatusFilebakLogic()
	{
		  access = new TBaseApparatusFilebakAccess();  
	}
		
	public TBaseApparatusFilebakLogic(TBaseApparatusFilebakVo _tBaseApparatusFilebak)
	{
		tBaseApparatusFilebak = _tBaseApparatusFilebak;
		access = new TBaseApparatusFilebakAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusFilebak">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            return access.GetSelectResultCount(tBaseApparatusFilebak);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseApparatusFilebakVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseApparatusFilebak">对象条件</param>
        /// <returns>对象</returns>
        public TBaseApparatusFilebakVo Details(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            return access.Details(tBaseApparatusFilebak);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseApparatusFilebak">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseApparatusFilebakVo> SelectByObject(TBaseApparatusFilebakVo tBaseApparatusFilebak, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseApparatusFilebak, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusFilebak">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusFilebakVo tBaseApparatusFilebak, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseApparatusFilebak, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseApparatusFilebak"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            return access.SelectByTable(tBaseApparatusFilebak);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseApparatusFilebak">对象</param>
        /// <returns></returns>
        public TBaseApparatusFilebakVo SelectByObject(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            return access.SelectByObject(tBaseApparatusFilebak);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            return access.Create(tBaseApparatusFilebak);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusFilebak">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            return access.Edit(tBaseApparatusFilebak);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusFilebak_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseApparatusFilebak_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusFilebakVo tBaseApparatusFilebak_UpdateSet, TBaseApparatusFilebakVo tBaseApparatusFilebak_UpdateWhere)
        {
            return access.Edit(tBaseApparatusFilebak_UpdateSet, tBaseApparatusFilebak_UpdateWhere);
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
        public bool Delete(TBaseApparatusFilebakVo tBaseApparatusFilebak)
        {
            return access.Delete(tBaseApparatusFilebak);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseApparatusFilebak.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //仪器ID
	  if (tBaseApparatusFilebak.APPARATUS_CODE.Trim() == "")
            {
                this.Tips.AppendLine("仪器ID不能为空");
                return false;
            }
	  //资料编号
	  if (tBaseApparatusFilebak.APPARATUS_FILE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("资料编号不能为空");
                return false;
            }
	  //资料名称
	  if (tBaseApparatusFilebak.APPARATUS_ATT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("资料名称不能为空");
                return false;
            }
	  //资料保存目录ID
	  if (tBaseApparatusFilebak.APPARATUS_ATT_FOLDER_ID.Trim() == "")
            {
                this.Tips.AppendLine("资料保存目录ID不能为空");
                return false;
            }
	  //资料保存文件ID
	  if (tBaseApparatusFilebak.APPARATUS_ATT_FILE_ID.Trim() == "")
            {
                this.Tips.AppendLine("资料保存文件ID不能为空");
                return false;
            }
	  //备注1
	  if (tBaseApparatusFilebak.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseApparatusFilebak.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseApparatusFilebak.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseApparatusFilebak.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseApparatusFilebak.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
