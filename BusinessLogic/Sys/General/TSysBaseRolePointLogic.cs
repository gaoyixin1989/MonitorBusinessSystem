using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.General;
using i3.DataAccess.Sys.General;

namespace i3.BusinessLogic.Sys.General
{
    /// <summary>
    /// 功能：角色监测点权限
    /// 创建日期：2011-04-13
    /// 创建人：郑义
    /// </summary>
    public class TSysBaseRolePointLogic : LogicBase
    {

	TSysBaseRolePointVo tSysBaseRolePoint = new TSysBaseRolePointVo();
        TSysBaseRolePointAccess access;
		
	public TSysBaseRolePointLogic()
	{
		  access = new TSysBaseRolePointAccess();  
	}
		
	public TSysBaseRolePointLogic(TSysBaseRolePointVo _tSysBaseRolePoint)
	{
		tSysBaseRolePoint = _tSysBaseRolePoint;
		access = new TSysBaseRolePointAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysBaseRolePoint">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            return access.GetSelectResultCount(tSysBaseRolePoint);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysBaseRolePointVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysBaseRolePoint">对象条件</param>
        /// <returns>对象</returns>
        public TSysBaseRolePointVo Details(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            return access.Details(tSysBaseRolePoint);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysBaseRolePoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysBaseRolePointVo> SelectByObject(TSysBaseRolePointVo tSysBaseRolePoint, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysBaseRolePoint, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysBaseRolePoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysBaseRolePointVo tSysBaseRolePoint, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysBaseRolePoint, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysBaseRolePoint"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            return access.SelectByTable(tSysBaseRolePoint);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysBaseRolePoint">对象</param>
        /// <returns></returns>
        public TSysBaseRolePointVo SelectByObject(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            return access.SelectByObject(tSysBaseRolePoint);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            return access.Create(tSysBaseRolePoint);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysBaseRolePoint">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysBaseRolePointVo tSysBaseRolePoint)
        {
            return access.Edit(tSysBaseRolePoint);
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
        public bool DeleteByRoleID(string strRoleID)
        {
            return access.DeleteByRoleID(strRoleID);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            	  //备注3
	  if (tSysBaseRolePoint.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //编号
	  if (tSysBaseRolePoint.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //角色编号
	  if (tSysBaseRolePoint.ROLE_ID.Trim() == "")
            {
                this.Tips.AppendLine("角色编号不能为空");
                return false;
            }
	  //监测点编号
	  if (tSysBaseRolePoint.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点编号不能为空");
                return false;
            }
	  //备注
	  if (tSysBaseRolePoint.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
	  //备注1
	  if (tSysBaseRolePoint.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tSysBaseRolePoint.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }

            return true;
        }

    }
}
