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
    /// 功能：仪器信息
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseApparatusInfoLogic : LogicBase
    {

	TBaseApparatusInfoVo tBaseApparatusInfo = new TBaseApparatusInfoVo();
    TBaseApparatusInfoAccess access;
		
	public TBaseApparatusInfoLogic()
	{
		  access = new TBaseApparatusInfoAccess();  
	}
		
	public TBaseApparatusInfoLogic(TBaseApparatusInfoVo _tBaseApparatusInfo)
	{
		tBaseApparatusInfo = _tBaseApparatusInfo;
		access = new TBaseApparatusInfoAccess();
	}

        
        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="strApparatusName">仪器名</param>
        /// <param name="strApparatusCode">仪器编号</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForSelectApparatus_inItem(string strApparatusName, string strApparatusCode, int iIndex, int iCount)
        {
            return access.SelectByTable_ForSelectApparatus_inItem(strApparatusName, strApparatusCode, iIndex, iCount);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="strApparatusName">仪器名</param>
        /// <param name="strApparatusCode">仪器编号</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ForSelectApparatus_inItem(string strApparatusName, string strApparatusCode)
        {
            return access.GetSelectResultCount_ForSelectApparatus_inItem(strApparatusName, strApparatusCode);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            return access.GetSelectResultCount(tBaseApparatusInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseApparatusInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseApparatusInfoVo Details(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            return access.Details(tBaseApparatusInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseApparatusInfoVo> SelectByObject(TBaseApparatusInfoVo tBaseApparatusInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tBaseApparatusInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusInfoVo tBaseApparatusInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tBaseApparatusInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseApparatusInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            return access.SelectByTable(tBaseApparatusInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <returns></returns>
        public TBaseApparatusInfoVo SelectByObject(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            return access.SelectByObject(tBaseApparatusInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            return access.Create(tBaseApparatusInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            return access.Edit(tBaseApparatusInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseApparatusInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tBaseApparatusInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseApparatusInfoVo tBaseApparatusInfo_UpdateSet, TBaseApparatusInfoVo tBaseApparatusInfo_UpdateWhere)
        {
            return access.Edit(tBaseApparatusInfo_UpdateSet, tBaseApparatusInfo_UpdateWhere);
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
        public bool Delete(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            return access.Delete(tBaseApparatusInfo);
        }


        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TBaseApparatusInfoVo tBaseApparatusInfo, int iIndex, int iCount)
        {
            return access.SelectDefinedTadble(tBaseApparatusInfo, iIndex, iCount);
        }

        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TBaseApparatusInfoVo tBaseApparatusInfo)
        {
            return access.GetSelecDefinedtResultCount(tBaseApparatusInfo);
        }

        /// <summary>
        ///  导入
        /// </summary>
        /// <param name="arrList"></param>
        /// <returns></returns>
        public bool SaveData(ArrayList arrList)
        {
            return access.SaveData(arrList);
        }


        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <param name="intIsMonth">1，按月，本月要检定；2，按季度，本季度要检定；3，提前一个月要检定；</param>
        /// <param name="intSrcType">1，全部；2，仅检定/校准时间；3，仅期间核查时间；</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusInfoVo tBaseApparatusInfo, int iIndex, int iCount, int intIsMonth, int intSrcType)
        {

            return access.SelectByTable(tBaseApparatusInfo, iIndex, iCount, intIsMonth, intSrcType);
        }

        /// <summary>
        /// 获取对象DataTable Create By weilin 2013-07-24
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <param name="intIsMonth">1，按月，本月要检定；2，按季度，本季度要检定；3，提前一个月要检定；</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseApparatusInfoVo tBaseApparatusInfo, int iIndex, int iCount, int intIsMonth)
        {
            return access.SelectByTable(tBaseApparatusInfo, iIndex, iCount, intIsMonth);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseApparatusInfo">对象</param>
        /// <param name="intIsMonth">1，按月，本月要检定；2，按季度，本季度要检定；3，提前一个月要检定；</param>
        /// <param name="intSrcType">1，全部；2，仅检定/校准时间；3，仅期间核查时间；</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseApparatusInfoVo tBaseApparatusInfo, int intIsMonth, int intSrcType)
        {
            return access.GetSelectResultCount(tBaseApparatusInfo,  intIsMonth,  intSrcType);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //ID
	  if (tBaseApparatusInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
	  //仪器编号
	  if (tBaseApparatusInfo.APPARATUS_CODE.Trim() == "")
            {
                this.Tips.AppendLine("仪器编号不能为空");
                return false;
            }
	  //档案类别
	  if (tBaseApparatusInfo.ARCHIVES_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("档案类别不能为空");
                return false;
            }
	  //类别1(辅助，非辅助)
	  if (tBaseApparatusInfo.SORT1.Trim() == "")
            {
                this.Tips.AppendLine("类别1(辅助，非辅助)不能为空");
                return false;
            }
	  //类别2(l流量计，离子计,汽车尾气.......)
	  if (tBaseApparatusInfo.SORT2.Trim() == "")
            {
                this.Tips.AppendLine("类别2(l流量计，离子计,汽车尾气.......)不能为空");
                return false;
            }
	  //所属仪器或者项目
	  if (tBaseApparatusInfo.BELONG_TO.Trim() == "")
            {
                this.Tips.AppendLine("所属仪器或者项目不能为空");
                return false;
            }
	  //仪器名称
	  if (tBaseApparatusInfo.NAME.Trim() == "")
            {
                this.Tips.AppendLine("仪器名称不能为空");
                return false;
            }
	  //规格型号
	  if (tBaseApparatusInfo.MODEL.Trim() == "")
            {
                this.Tips.AppendLine("规格型号不能为空");
                return false;
            }
	  //出厂编号
	  if (tBaseApparatusInfo.SERIAL_NO.Trim() == "")
            {
                this.Tips.AppendLine("出厂编号不能为空");
                return false;
            }
	  //仪器供应商
	  if (tBaseApparatusInfo.APPARATUS_PROVIDER.Trim() == "")
            {
                this.Tips.AppendLine("仪器供应商不能为空");
                return false;
            }
	  //配件供应商
	  if (tBaseApparatusInfo.FITTINGS_PROVIDER.Trim() == "")
            {
                this.Tips.AppendLine("配件供应商不能为空");
                return false;
            }
	  //仪器供应商网址
	  if (tBaseApparatusInfo.WEB_SITE.Trim() == "")
            {
                this.Tips.AppendLine("仪器供应商网址不能为空");
                return false;
            }
	  //仪器性能(合格,一级合格,正常)
	  if (tBaseApparatusInfo.CAPABILITY.Trim() == "")
            {
                this.Tips.AppendLine("仪器性能(合格,一级合格,正常)不能为空");
                return false;
            }
	  //联系人
	  if (tBaseApparatusInfo.LINK_MAN.Trim() == "")
            {
                this.Tips.AppendLine("联系人不能为空");
                return false;
            }
	  //联系电话
	  if (tBaseApparatusInfo.LINK_PHONE.Trim() == "")
            {
                this.Tips.AppendLine("联系电话不能为空");
                return false;
            }
	  //邮编
	  if (tBaseApparatusInfo.POST.Trim() == "")
            {
                this.Tips.AppendLine("邮编不能为空");
                return false;
            }
	  //联系地址
	  if (tBaseApparatusInfo.ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("联系地址不能为空");
                return false;
            }
	  //量值溯源方式(校准、自校、检定)
	  if (tBaseApparatusInfo.CERTIFICATE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("量值溯源方式(校准、自校、检定)不能为空");
                return false;
            }
	  //溯源结果(合格，不合格)
	  if (tBaseApparatusInfo.TRACE_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("溯源结果(合格，不合格)不能为空");
                return false;
            }
	  //检定方式(不检，来检,送检,暂不能检，不详)
	  if (tBaseApparatusInfo.TEST_MODE.Trim() == "")
            {
                this.Tips.AppendLine("检定方式(不检，来检,送检,暂不能检，不详)不能为空");
                return false;
            }
	  //校正周期
	  if (tBaseApparatusInfo.VERIFY_CYCLE.Trim() == "")
            {
                this.Tips.AppendLine("校正周期不能为空");
                return false;
            }
	  //使用科室
	  if (tBaseApparatusInfo.DEPT.Trim() == "")
            {
                this.Tips.AppendLine("使用科室不能为空");
                return false;
            }
	  //保管人
	  if (tBaseApparatusInfo.KEEPER.Trim() == "")
            {
                this.Tips.AppendLine("保管人不能为空");
                return false;
            }
	  //放置地点
	  if (tBaseApparatusInfo.POSITION.Trim() == "")
            {
                this.Tips.AppendLine("放置地点不能为空");
                return false;
            }
	  //使用状况(在用，未用)
	  if (tBaseApparatusInfo.STATUS.Trim() == "")
            {
                this.Tips.AppendLine("使用状况(在用，未用)不能为空");
                return false;
            }
	  //档案上传地址
	  if (tBaseApparatusInfo.ARCHIVES_ADDRESS.Trim() == "")
            {
                this.Tips.AppendLine("档案上传地址不能为空");
                return false;
            }
	  //最近检定/校准时间
	  if (tBaseApparatusInfo.BEGIN_TIME.Trim() == "")
            {
                this.Tips.AppendLine("最近检定/校准时间不能为空");
                return false;
            }
	  //到期检定/校准时间
	  if (tBaseApparatusInfo.END_TIME.Trim() == "")
            {
                this.Tips.AppendLine("到期检定/校准时间不能为空");
                return false;
            }
	  //扩展不确定度
	  if (tBaseApparatusInfo.EXPANDED_UNCETAINTY.Trim() == "")
            {
                this.Tips.AppendLine("扩展不确定度不能为空");
                return false;
            }
	  //测量范围
	  if (tBaseApparatusInfo.MEASURING_RANGE.Trim() == "")
            {
                this.Tips.AppendLine("测量范围不能为空");
                return false;
            }
	  //检定单位
	  if (tBaseApparatusInfo.EXAMINE_DEPARTMENT.Trim() == "")
            {
                this.Tips.AppendLine("检定单位不能为空");
                return false;
            }
	  //检定单位电话
	  if (tBaseApparatusInfo.DEPARTMENT_PHONE.Trim() == "")
            {
                this.Tips.AppendLine("检定单位电话不能为空");
                return false;
            }
	  //检定单位联系人
	  if (tBaseApparatusInfo.DEPARTMENT_LINKMAN.Trim() == "")
            {
                this.Tips.AppendLine("检定单位联系人不能为空");
                return false;
            }
	  //期间核查方式
	  if (tBaseApparatusInfo.VERIFICATION_WAY.Trim() == "")
            {
                this.Tips.AppendLine("期间核查方式不能为空");
                return false;
            }
	  //期间核查结果
	  if (tBaseApparatusInfo.VERIFICATION_RESULT.Trim() == "")
            {
                this.Tips.AppendLine("期间核查结果不能为空");
                return false;
            }
	  //最近期间核查时间
	  if (tBaseApparatusInfo.VERIFICATION_BEGIN_TIME.Trim() == "")
            {
                this.Tips.AppendLine("最近期间核查时间不能为空");
                return false;
            }
	  //最近期间核查时间
	  if (tBaseApparatusInfo.VERIFICATION_END_TIME.Trim() == "")
            {
                this.Tips.AppendLine("最近期间核查时间不能为空");
                return false;
            }
	  //备注1
	  if (tBaseApparatusInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tBaseApparatusInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tBaseApparatusInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tBaseApparatusInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tBaseApparatusInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
