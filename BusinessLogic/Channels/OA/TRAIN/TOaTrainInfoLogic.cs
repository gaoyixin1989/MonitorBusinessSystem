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
    /// 功能：员工培训记录
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaTrainInfoLogic : LogicBase
    {

	TOaTrainInfoVo tOaTrainInfo = new TOaTrainInfoVo();
    TOaTrainInfoAccess access;
		
	public TOaTrainInfoLogic()
	{
		  access = new TOaTrainInfoAccess();  
	}
		
	public TOaTrainInfoLogic(TOaTrainInfoVo _tOaTrainInfo)
	{
		tOaTrainInfo = _tOaTrainInfo;
		access = new TOaTrainInfoAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaTrainInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaTrainInfoVo tOaTrainInfo)
        {
            return access.GetSelectResultCount(tOaTrainInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaTrainInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaTrainInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaTrainInfoVo Details(TOaTrainInfoVo tOaTrainInfo)
        {
            return access.Details(tOaTrainInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaTrainInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaTrainInfoVo> SelectByObject(TOaTrainInfoVo tOaTrainInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaTrainInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaTrainInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaTrainInfoVo tOaTrainInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaTrainInfo, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaTrainInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaTrainInfoVo tOaTrainInfo)
        {
            return access.SelectByTable(tOaTrainInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaTrainInfo">对象</param>
        /// <returns></returns>
        public TOaTrainInfoVo SelectByObject(TOaTrainInfoVo tOaTrainInfo)
        {
            return access.SelectByObject(tOaTrainInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaTrainInfoVo tOaTrainInfo)
        {
            return access.Create(tOaTrainInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainInfoVo tOaTrainInfo)
        {
            return access.Edit(tOaTrainInfo);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainInfo_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaTrainInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainInfoVo tOaTrainInfo_UpdateSet, TOaTrainInfoVo tOaTrainInfo_UpdateWhere)
        {
            return access.Edit(tOaTrainInfo_UpdateSet, tOaTrainInfo_UpdateWhere);
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
        public bool Delete(TOaTrainInfoVo tOaTrainInfo)
        {
            return access.Delete(tOaTrainInfo);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaTrainInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //培训ID
	  if (tOaTrainInfo.TRAIN_ID.Trim() == "")
            {
                this.Tips.AppendLine("培训ID不能为空");
                return false;
            }
	  //员工ID
	  if (tOaTrainInfo.EMPLOYE_ID.Trim() == "")
            {
                this.Tips.AppendLine("员工ID不能为空");
                return false;
            }
	  //培训教材评估
	  if (tOaTrainInfo.ENTRYDATE.Trim() == "")
            {
                this.Tips.AppendLine("培训教材评估不能为空");
                return false;
            }
	  //培训教师评估
	  if (tOaTrainInfo.TRAINDATE.Trim() == "")
            {
                this.Tips.AppendLine("培训教师评估不能为空");
                return false;
            }
	  //培训成绩
	  if (tOaTrainInfo.TRAINPROJECT.Trim() == "")
            {
                this.Tips.AppendLine("培训成绩不能为空");
                return false;
            }
	  //培训结果
	  if (tOaTrainInfo.TRAINRESULT.Trim() == "")
            {
                this.Tips.AppendLine("培训结果不能为空");
                return false;
            }
	  //证书编号
	  if (tOaTrainInfo.CERTIFICATECODE.Trim() == "")
            {
                this.Tips.AppendLine("证书编号不能为空");
                return false;
            }
	  //自我总结
	  if (tOaTrainInfo.TRAINCONTENT.Trim() == "")
            {
                this.Tips.AppendLine("自我总结不能为空");
                return false;
            }
	  //总结时间
	  if (tOaTrainInfo.REMARKS.Trim() == "")
            {
                this.Tips.AppendLine("总结时间不能为空");
                return false;
            }
	  //理论掌握能力评估
	  if (tOaTrainInfo.THEORY_SKILL.Trim() == "")
            {
                this.Tips.AppendLine("理论掌握能力评估不能为空");
                return false;
            }
	  //实际操作能力评估
	  if (tOaTrainInfo.OPER_SKILL.Trim() == "")
            {
                this.Tips.AppendLine("实际操作能力评估不能为空");
                return false;
            }
	  //样品和质控样分析能力评估
	  if (tOaTrainInfo.TEST_SKILL.Trim() == "")
            {
                this.Tips.AppendLine("样品和质控样分析能力评估不能为空");
                return false;
            }
	  //评价结论
	  if (tOaTrainInfo.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价结论不能为空");
                return false;
            }
	  //评价人ID
	  if (tOaTrainInfo.JUDGE_ID.Trim() == "")
            {
                this.Tips.AppendLine("评价人ID不能为空");
                return false;
            }
	  //评价日期
	  if (tOaTrainInfo.JUDGE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("评价日期不能为空");
                return false;
            }
	  //备注1
	  if (tOaTrainInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaTrainInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaTrainInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaTrainInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaTrainInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
