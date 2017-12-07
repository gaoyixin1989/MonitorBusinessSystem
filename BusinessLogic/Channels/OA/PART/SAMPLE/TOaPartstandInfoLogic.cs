using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.OA.PART.SAMPLE;
using System.Data;
using i3.DataAccess.Channels.OA.PART.SAMPLE;

namespace i3.BusinessLogic.Channels.OA.PART.SAMPLE
{
    /// <summary>
    /// 功能：标准样品信息
    /// 创建日期：2013-09-12
    /// 创建人：魏林
    /// </summary>
    public class TOaPartstandInfoLogic : LogicBase
    {

        TOaPartstandInfoVo tOaPartstandInfo = new TOaPartstandInfoVo();
        TOaPartstandInfoAccess access;

        public TOaPartstandInfoLogic()
        {
            access = new TOaPartstandInfoAccess();
        }

        public TOaPartstandInfoLogic(TOaPartstandInfoVo _tOaPartstandInfo)
        {
            tOaPartstandInfo = _tOaPartstandInfo;
            access = new TOaPartstandInfoAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartstandInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartstandInfoVo tOaPartstandInfo)
        {
            return access.GetSelectResultCount(tOaPartstandInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartstandInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartstandInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartstandInfoVo Details(TOaPartstandInfoVo tOaPartstandInfo)
        {
            return access.Details(tOaPartstandInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartstandInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartstandInfoVo> SelectByObject(TOaPartstandInfoVo tOaPartstandInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaPartstandInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartstandInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartstandInfoVo tOaPartstandInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaPartstandInfo, iIndex, iCount);
        }

        public DataTable SelectByTableNew(TOaPartstandInfoVo tOaPartstandInfo, int iIndex, int iCount, bool isZore)
        {
            return access.SelectByTableNew(tOaPartstandInfo, iIndex, iCount, isZore);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartstandInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartstandInfoVo tOaPartstandInfo)
        {
            return access.SelectByTable(tOaPartstandInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartstandInfo">对象</param>
        /// <returns></returns>
        public TOaPartstandInfoVo SelectByObject(TOaPartstandInfoVo tOaPartstandInfo)
        {
            return access.SelectByObject(tOaPartstandInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartstandInfoVo tOaPartstandInfo)
        {
            return access.Create(tOaPartstandInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartstandInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartstandInfoVo tOaPartstandInfo)
        {
            return access.Edit(tOaPartstandInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartstandInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaPartstandInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartstandInfoVo tOaPartstandInfo_UpdateSet, TOaPartstandInfoVo tOaPartstandInfo_UpdateWhere)
        {
            return access.Edit(tOaPartstandInfo_UpdateSet, tOaPartstandInfo_UpdateWhere);
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
        public bool Delete(TOaPartstandInfoVo tOaPartstandInfo)
        {
            return access.Delete(tOaPartstandInfo);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tOaPartstandInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //物料编码
            if (tOaPartstandInfo.SAMPLE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("物料编码不能为空");
                return false;
            }
            //物料类别
            if (tOaPartstandInfo.SAMPLE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("物料类别不能为空");
                return false;
            }
            //物料名称
            if (tOaPartstandInfo.SAMPLE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("物料名称不能为空");
                return false;
            }
            //单位
            if (tOaPartstandInfo.UNIT.Trim() == "")
            {
                this.Tips.AppendLine("单位不能为空");
                return false;
            }
            //规格型号
            if (tOaPartstandInfo.CLASS_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("规格型号不能为空");
                return false;
            }
            //库存量
            if (tOaPartstandInfo.INVENTORY.Trim() == "")
            {
                this.Tips.AppendLine("库存量不能为空");
                return false;
            }
            //介质/基体
            if (tOaPartstandInfo.TOTAL_INVENTORY.Trim() == "")
            {
                this.Tips.AppendLine("介质/基体不能为空");
                return false;
            }
            //
            if (tOaPartstandInfo.SAMPLE_SOURCE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //
            if (tOaPartstandInfo.POTENCY.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //分析纯/化学纯
            if (tOaPartstandInfo.BUY_DATE.Trim() == "")
            {
                this.Tips.AppendLine("分析纯/化学纯不能为空");
                return false;
            }
            //报警值
            if (tOaPartstandInfo.EFF_DATE.Trim() == "")
            {
                this.Tips.AppendLine("报警值不能为空");
                return false;
            }
            //用途
            if (tOaPartstandInfo.LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("用途不能为空");
                return false;
            }
            //技术要求
            if (tOaPartstandInfo.CARER.Trim() == "")
            {
                this.Tips.AppendLine("技术要求不能为空");
                return false;
            }
            //备注1
            if (tOaPartstandInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tOaPartstandInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaPartstandInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tOaPartstandInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tOaPartstandInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
