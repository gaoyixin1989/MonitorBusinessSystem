using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.PayFor;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.PayFor;

namespace i3.BusinessLogic.Channels.Env.Fill.PayFor
{
    /// <summary>
    /// 功能：生态补偿数据填报
    /// 创建日期：2013-06-24
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillPayforLogic : LogicBase
    {

        TEnvFillPayforVo tEnvFillPayfor = new TEnvFillPayforVo();
        TEnvFillPayforAccess access;

        public TEnvFillPayforLogic()
        {
            access = new TEnvFillPayforAccess();
        }

        public TEnvFillPayforLogic(TEnvFillPayforVo _tEnvFillPayfor)
        {
            tEnvFillPayfor = _tEnvFillPayfor;
            access = new TEnvFillPayforAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillPayfor">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillPayforVo tEnvFillPayfor)
        {
            return access.GetSelectResultCount(tEnvFillPayfor);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillPayforVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillPayfor">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillPayforVo Details(TEnvFillPayforVo tEnvFillPayfor)
        {
            return access.Details(tEnvFillPayfor);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillPayfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillPayforVo> SelectByObject(TEnvFillPayforVo tEnvFillPayfor, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillPayfor, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillPayfor">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillPayforVo tEnvFillPayfor, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillPayfor, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillPayfor"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillPayforVo tEnvFillPayfor)
        {
            return access.SelectByTable(tEnvFillPayfor);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillPayfor">对象</param>
        /// <returns></returns>
        public TEnvFillPayforVo SelectByObject(TEnvFillPayforVo tEnvFillPayfor)
        {
            return access.SelectByObject(tEnvFillPayfor);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillPayforVo tEnvFillPayfor)
        {
            return access.Create(tEnvFillPayfor);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPayfor">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPayforVo tEnvFillPayfor)
        {
            return access.Edit(tEnvFillPayfor);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillPayfor_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillPayfor_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillPayforVo tEnvFillPayfor_UpdateSet, TEnvFillPayforVo tEnvFillPayfor_UpdateWhere)
        {
            return access.Edit(tEnvFillPayfor_UpdateSet, tEnvFillPayfor_UpdateWhere);
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
        public bool Delete(TEnvFillPayforVo tEnvFillPayfor)
        {
            return access.Delete(tEnvFillPayfor);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillPayfor.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillPayfor.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillPayfor.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //扣缴金额
            if (tEnvFillPayfor.FEE.Trim() == "")
            {
                this.Tips.AppendLine("扣缴金额不能为空");
                return false;
            }
            //年度
            if (tEnvFillPayfor.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillPayfor.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillPayfor.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillPayfor.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillPayfor.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //上游断面水质结果COD浓度
            if (tEnvFillPayfor.UP_COD.Trim() == "")
            {
                this.Tips.AppendLine("上游断面水质结果COD浓度不能为空");
                return false;
            }
            //上游断面水质结果氨氮浓度
            if (tEnvFillPayfor.UP_NH.Trim() == "")
            {
                this.Tips.AppendLine("上游断面水质结果氨氮浓度不能为空");
                return false;
            }
            //备注1
            if (tEnvFillPayfor.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillPayfor.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillPayfor.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillPayfor.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillPayfor.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }
        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }
    }

}
