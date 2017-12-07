using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Seabath;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.Seabath;

namespace i3.BusinessLogic.Channels.Env.Fill.Seabath
{
    /// <summary>
    /// 功能：海水浴场填报
    /// 创建日期：2013-06-25
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillSeabathLogic : LogicBase
    {

        TEnvFillSeabathVo tEnvFillSeabath = new TEnvFillSeabathVo();
        TEnvFillSeabathAccess access;

        public TEnvFillSeabathLogic()
        {
            access = new TEnvFillSeabathAccess();
        }

        public TEnvFillSeabathLogic(TEnvFillSeabathVo _tEnvFillSeabath)
        {
            tEnvFillSeabath = _tEnvFillSeabath;
            access = new TEnvFillSeabathAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSeabath">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSeabathVo tEnvFillSeabath)
        {
            return access.GetSelectResultCount(tEnvFillSeabath);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSeabathVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSeabath">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSeabathVo Details(TEnvFillSeabathVo tEnvFillSeabath)
        {
            return access.Details(tEnvFillSeabath);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSeabath">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSeabathVo> SelectByObject(TEnvFillSeabathVo tEnvFillSeabath, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillSeabath, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSeabath">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSeabathVo tEnvFillSeabath, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillSeabath, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSeabath"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSeabathVo tEnvFillSeabath)
        {
            return access.SelectByTable(tEnvFillSeabath);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSeabath">对象</param>
        /// <returns></returns>
        public TEnvFillSeabathVo SelectByObject(TEnvFillSeabathVo tEnvFillSeabath)
        {
            return access.SelectByObject(tEnvFillSeabath);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSeabathVo tEnvFillSeabath)
        {
            return access.Create(tEnvFillSeabath);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeabath">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeabathVo tEnvFillSeabath)
        {
            return access.Edit(tEnvFillSeabath);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSeabath_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSeabath_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeabathVo tEnvFillSeabath_UpdateSet, TEnvFillSeabathVo tEnvFillSeabath_UpdateWhere)
        {
            return access.Edit(tEnvFillSeabath_UpdateSet, tEnvFillSeabath_UpdateWhere);
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
        public bool Delete(TEnvFillSeabathVo tEnvFillSeabath)
        {
            return access.Delete(tEnvFillSeabath);
        }

        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillSeabath.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillSeabath.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillSeabath.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillSeabath.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillSeabath.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillSeabath.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillSeabath.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillSeabath.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //评价
            if (tEnvFillSeabath.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillSeabath.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillSeabath.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillSeabath.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillSeabath.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillSeabath.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillSeabath.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
