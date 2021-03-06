using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Base.Point;
using i3.DataAccess.Channels.Base.Point;

namespace i3.BusinessLogic.Channels.Base.Point
{
    /// <summary>
    /// 功能：监测点信息
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseCompanyPointLogic : LogicBase
    {

	TBaseCompanyPointVo tBaseCompanyPoint = new TBaseCompanyPointVo();
    TBaseCompanyPointAccess access;
		
	public TBaseCompanyPointLogic()
	{
		  access = new TBaseCompanyPointAccess();  
	}
		
	public TBaseCompanyPointLogic(TBaseCompanyPointVo _tBaseCompanyPoint)
	{
		tBaseCompanyPoint = _tBaseCompanyPoint;
		access = new TBaseCompanyPointAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseCompanyPoint">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            return access.GetSelectResultCount(tBaseCompanyPoint);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseCompanyPointVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseCompanyPoint">对象条件</param>
        /// <returns>对象</returns>
        public TBaseCompanyPointVo Details(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            return access.Details(tBaseCompanyPoint);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseCompanyPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseCompanyPointVo> SelectByObject(TBaseCompanyPointVo tBaseCompanyPoint, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseCompanyPoint, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseCompanyPoint">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseCompanyPointVo tBaseCompanyPoint, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseCompanyPoint, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseCompanyPoint"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            return access.SelectByTable(tBaseCompanyPoint);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseCompanyPoint">对象</param>
        /// <returns></returns>
        public TBaseCompanyPointVo SelectByObject(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            return access.SelectByObject(tBaseCompanyPoint);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            return access.Create(tBaseCompanyPoint);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyPoint">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            return access.Edit(tBaseCompanyPoint);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseCompanyPoint_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseCompanyPoint_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseCompanyPointVo tBaseCompanyPoint_UpdateSet, TBaseCompanyPointVo tBaseCompanyPoint_UpdateWhere)
        {
            return access.Edit(tBaseCompanyPoint_UpdateSet, tBaseCompanyPoint_UpdateWhere);
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
        public bool Delete(TBaseCompanyPointVo tBaseCompanyPoint)
        {
            return access.Delete(tBaseCompanyPoint);
        }

        /// <summary>
        ///  批量导入
        /// </summary>
        /// <param name="arrList"></param>
        /// <returns></returns>
        public bool SaveData(ArrayList arrList)
        {
            return access.SaveData(arrList);
        }

        public DataTable SelectByTableForPlan(TBaseCompanyPointVo tBaseCompanyPoint, string strPointIdList, int iIndex, int iCount)
        {
            return access.SelectByTableForPlan(tBaseCompanyPoint,strPointIdList,iIndex,iCount);
        }
        public int SelectByTableForPlanCount(TBaseCompanyPointVo tBaseCompanyPoint, string strPointIdList)
        {
            return access.SelectByTableForPlanCount(tBaseCompanyPoint, strPointIdList);
        }
        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseCompanyPoint.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //监测点名称
	  if (tBaseCompanyPoint.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("监测点名称不能为空");
                return false;
            }
	  //监测点类型ID(监督性监测、超常规监测、临时性监测、送样委托、验收监测、环评监测...)
	  if (tBaseCompanyPoint.POINT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("监测点类型ID(监督性监测、超常规监测、临时性监测、送样委托、验收监测、环评监测...)不能为空");
                return false;
            }
	  //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
	  if (tBaseCompanyPoint.MONITOR_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）不能为空");
                return false;
            }
	  //监测频次
	  if (tBaseCompanyPoint.FREQ.Trim() == "")
            {
                this.Tips.AppendLine("监测频次不能为空");
                return false;
            }
	  //所属企业ID
	  if (tBaseCompanyPoint.COMPANY_ID.Trim() == "")
            {
                this.Tips.AppendLine("所属企业ID不能为空");
                return false;
            }
	  //动态属性ID
	  if (tBaseCompanyPoint.DYNAMIC_ATTRIBUTE_ID.Trim() == "")
            {
                this.Tips.AppendLine("动态属性ID不能为空");
                return false;
            }
	  //建成时间
	  if (tBaseCompanyPoint.CREATE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("建成时间不能为空");
                return false;
            }
	  //监测点位置
	  if (tBaseCompanyPoint.ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("监测点位置不能为空");
                return false;
            }
	  //经度
	  if (tBaseCompanyPoint.LONGITUDE.Trim() == "")
            {
                this.Tips.AppendLine("经度不能为空");
                return false;
            }
	  //纬度
	  if (tBaseCompanyPoint.LATITUDE.Trim() == "")
            {
                this.Tips.AppendLine("纬度不能为空");
                return false;
            }
	  //点位描述
	  if (tBaseCompanyPoint.DESCRIPTION.Trim() == "")
            {
                this.Tips.AppendLine("点位描述不能为空");
                return false;
            }
	  //国标条件项
	  if (tBaseCompanyPoint.NATIONAL_ST_CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("国标条件项不能为空");
                return false;
            }
	  //行标条件项ID
	  if (tBaseCompanyPoint.INDUSTRY_ST_CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("行标条件项ID不能为空");
                return false;
            }
	  //地标条件项_ID
	  if (tBaseCompanyPoint.LOCAL_ST_CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("地标条件项_ID不能为空");
                return false;
            }
	  //使用状态(0为启用、1为停用)
      if (tBaseCompanyPoint.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
	  //序号
	  if (tBaseCompanyPoint.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
	  //备注1
	  if (tBaseCompanyPoint.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseCompanyPoint.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseCompanyPoint.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseCompanyPoint.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseCompanyPoint.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
