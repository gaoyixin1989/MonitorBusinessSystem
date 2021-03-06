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
    /// 功能：员工基本信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaEmployeInfoLogic : LogicBase
    {

	TOaEmployeInfoVo tOaEmployeInfo = new TOaEmployeInfoVo();
    TOaEmployeInfoAccess access;
		
	public TOaEmployeInfoLogic()
	{
		  access = new TOaEmployeInfoAccess();  
	}
		
	public TOaEmployeInfoLogic(TOaEmployeInfoVo _tOaEmployeInfo)
	{
		tOaEmployeInfo = _tOaEmployeInfo;
		access = new TOaEmployeInfoAccess();
	}


    /// <summary>
    /// 员工信息表:导出已出报告的符合检索条件的报表（EXCEL格式）
    /// </summary>
    /// <param name="TMisMonitorResultVo"></param>
    /// <returns></returns>
    public DataTable SelectTestDataLst(string[] strWhereArr, int iIndex, int iCount)
    {
        return access.SelectTestDataLst(strWhereArr, iIndex, iCount);
    }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaEmployeInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaEmployeInfoVo tOaEmployeInfo)
        {
            return access.GetSelectResultCount(tOaEmployeInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaEmployeInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaEmployeInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaEmployeInfoVo Details(TOaEmployeInfoVo tOaEmployeInfo)
        {
            return access.Details(tOaEmployeInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaEmployeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaEmployeInfoVo> SelectByObject(TOaEmployeInfoVo tOaEmployeInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaEmployeInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaEmployeInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaEmployeInfoVo tOaEmployeInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaEmployeInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaEmployeInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaEmployeInfoVo tOaEmployeInfo)
        {
            return access.SelectByTable(tOaEmployeInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaEmployeInfo">对象</param>
        /// <returns></returns>
        public TOaEmployeInfoVo SelectByObject(TOaEmployeInfoVo tOaEmployeInfo)
        {
            return access.SelectByObject(tOaEmployeInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaEmployeInfoVo tOaEmployeInfo)
        {
            return access.Create(tOaEmployeInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeInfoVo tOaEmployeInfo)
        {
            return access.Edit(tOaEmployeInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaEmployeInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaEmployeInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaEmployeInfoVo tOaEmployeInfo_UpdateSet, TOaEmployeInfoVo tOaEmployeInfo_UpdateWhere)
        {
            return access.Edit(tOaEmployeInfo_UpdateSet, tOaEmployeInfo_UpdateWhere);
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
        public bool Delete(TOaEmployeInfoVo tOaEmployeInfo)
        {
            return access.Delete(tOaEmployeInfo);
        }

        /// <summary>
        /// 对象是否存在验证
        /// </summary>
        /// <param name="tOaEmployeInfo">对象</param>
        /// <returns>是否存在</returns>
        public bool IsExist(TOaEmployeInfoVo tOaEmployeInfo)
        {
            return access.IsExist(tOaEmployeInfo);
        }

        /// <summary>
        /// 自定义模糊查询 Create By Castle (胡方扬)  2013-01-10
        /// </summary>
        /// <param name="tOaEmployeInfo"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectDefineByTable(TOaEmployeInfoVo tOaEmployeInfo,int iIndex,int iCount) { 
        return access.SelectDefineByTable(tOaEmployeInfo,iIndex,iCount);
        }
        
                /// <summary>
        /// 自定义模糊查询返回记录数 Create By Castle (胡方扬)  2013-01-10
        /// </summary>
        /// <param name="tOaEmployeInfo"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int SelectDefineBytResult(TOaEmployeInfoVo tOaEmployeInfo)
        {
            return access.SelectDefineBytResult(tOaEmployeInfo);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaEmployeInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //USER_ID
	  if (tOaEmployeInfo.USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("USER_ID不能为空");
                return false;
            }
	  //员工编号
	  if (tOaEmployeInfo.EMPLOYE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("员工编号不能为空");
                return false;
            }
	  //员工姓名
	  if (tOaEmployeInfo.EMPLOYE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("员工姓名不能为空");
                return false;
            }
	  //身份证号
	  if (tOaEmployeInfo.ID_CARD.Trim() == "")
            {
                this.Tips.AppendLine("身份证号不能为空");
                return false;
            }
	  //性别
	  if (tOaEmployeInfo.SEX.Trim() == "")
            {
                this.Tips.AppendLine("性别不能为空");
                return false;
            }
	  //出生日期
	  if (tOaEmployeInfo.BIRTHDAY.Trim() == "")
            {
                this.Tips.AppendLine("出生日期不能为空");
                return false;
            }
	  //
	  if (tOaEmployeInfo.AGE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
	  //民族
	  if (tOaEmployeInfo.NATION.Trim() == "")
            {
                this.Tips.AppendLine("民族不能为空");
                return false;
            }
	  //政治面貌
	  if (tOaEmployeInfo.POLITICALSTATUS.Trim() == "")
            {
                this.Tips.AppendLine("政治面貌不能为空");
                return false;
            }
	  //文化程度
	  if (tOaEmployeInfo.EDUCATIONLEVEL.Trim() == "")
            {
                this.Tips.AppendLine("文化程度不能为空");
                return false;
            }
	  //所在部门
	  if (tOaEmployeInfo.DEPART.Trim() == "")
            {
                this.Tips.AppendLine("所在部门不能为空");
                return false;
            }
	  //
	  if (tOaEmployeInfo.POST.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
	  //岗位
	  if (tOaEmployeInfo.POSITION.Trim() == "")
            {
                this.Tips.AppendLine("岗位不能为空");
                return false;
            }
	  //级别
	  if (tOaEmployeInfo.POST_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("级别不能为空");
                return false;
            }
	  //人员分类
	  if (tOaEmployeInfo.EMPLOYE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("人员分类不能为空");
                return false;
            }
	  //现任职时间
	  if (tOaEmployeInfo.POST_DATE.Trim() == "")
            {
                this.Tips.AppendLine("现任职时间不能为空");
                return false;
            }
	  //编制类别
	  if (tOaEmployeInfo.ORGANIZATION_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("编制类别不能为空");
                return false;
            }
	  //入编时间
	  if (tOaEmployeInfo.ORGANIZATION_DATE.Trim() == "")
            {
                this.Tips.AppendLine("入编时间不能为空");
                return false;
            }
	  //工作时间
	  if (tOaEmployeInfo.ENTRYDATE.Trim() == "")
            {
                this.Tips.AppendLine("工作时间不能为空");
                return false;
            }
	  //聘任专业技术职务
	  if (tOaEmployeInfo.TECHNOLOGY_POST.Trim() == "")
            {
                this.Tips.AppendLine("聘任专业技术职务不能为空");
                return false;
            }
	  //入本单位时间
	  if (tOaEmployeInfo.APPLY_DATE.Trim() == "")
            {
                this.Tips.AppendLine("入本单位时间不能为空");
                return false;
            }
	  //毕业院校
	  if (tOaEmployeInfo.GRADUATE.Trim() == "")
            {
                this.Tips.AppendLine("毕业院校不能为空");
                return false;
            }
	  //所学专业
	  if (tOaEmployeInfo.SPECIALITY.Trim() == "")
            {
                this.Tips.AppendLine("所学专业不能为空");
                return false;
            }
	  //毕业时间
	  if (tOaEmployeInfo.GRADUATE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("毕业时间不能为空");
                return false;
            }
	  //专业技术等级
	  if (tOaEmployeInfo.TECHNOLOGY_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("专业技术等级不能为空");
                return false;
            }
	  //工勤技能等级
	  if (tOaEmployeInfo.SKILL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("工勤技能等级不能为空");
                return false;
            }
	  //工作状态,1在职、2离职、3退休
	  if (tOaEmployeInfo.POST_STATUS.Trim() == "")
            {
                this.Tips.AppendLine("工作状态,1在职、2离职、3退休不能为空");
                return false;
            }
	  //备注
	  if (tOaEmployeInfo.INFO.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
	  //备注1
	  if (tOaEmployeInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaEmployeInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaEmployeInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaEmployeInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaEmployeInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
