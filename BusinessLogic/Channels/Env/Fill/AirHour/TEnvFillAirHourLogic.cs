using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.AirHour;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.AirHour;

namespace i3.BusinessLogic.Channels.Env.Fill.AirHour
{
    /// <summary>
    /// 功能：环境空气填报（小时）
    /// 创建日期：2013-06-27
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillAirhourLogic : LogicBase
    {

        TEnvFillAirhourVo tEnvFillAirhour = new TEnvFillAirhourVo();
        TEnvFillAirhourAccess access;

        public TEnvFillAirhourLogic()
        {
            access = new TEnvFillAirhourAccess();
        }

        public TEnvFillAirhourLogic(TEnvFillAirhourVo _tEnvFillAirhour)
        {
            tEnvFillAirhour = _tEnvFillAirhour;
            access = new TEnvFillAirhourAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAirhour">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirhourVo tEnvFillAirhour)
        {
            return access.GetSelectResultCount(tEnvFillAirhour);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirhourVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAirhour">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirhourVo Details(TEnvFillAirhourVo tEnvFillAirhour)
        {
            return access.Details(tEnvFillAirhour);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAirhour">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirhourVo> SelectByObject(TEnvFillAirhourVo tEnvFillAirhour, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAirhour, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAirhour">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirhourVo tEnvFillAirhour, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAirhour, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAirhour"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirhourVo tEnvFillAirhour)
        {
            return access.SelectByTable(tEnvFillAirhour);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAirhour">对象</param>
        /// <returns></returns>
        public TEnvFillAirhourVo SelectByObject(TEnvFillAirhourVo tEnvFillAirhour)
        {
            return access.SelectByObject(tEnvFillAirhour);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirhourVo tEnvFillAirhour)
        {
            return access.Create(tEnvFillAirhour);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirhour">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirhourVo tEnvFillAirhour)
        {
            return access.Edit(tEnvFillAirhour);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirhour_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAirhour_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirhourVo tEnvFillAirhour_UpdateSet, TEnvFillAirhourVo tEnvFillAirhour_UpdateWhere)
        {
            return access.Edit(tEnvFillAirhour_UpdateSet, tEnvFillAirhour_UpdateWhere);
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
        public bool Delete(TEnvFillAirhourVo tEnvFillAirhour)
        {
            return access.Delete(tEnvFillAirhour);
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
            if (tEnvFillAirhour.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillAirhour.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //年度
            if (tEnvFillAirhour.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillAirhour.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillAirhour.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillAirhour.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //气温
            if (tEnvFillAirhour.TEMPERATRUE.Trim() == "")
            {
                this.Tips.AppendLine("气温不能为空");
                return false;
            }
            //气压
            if (tEnvFillAirhour.PRESSURE.Trim() == "")
            {
                this.Tips.AppendLine("气压不能为空");
                return false;
            }
            //风速
            if (tEnvFillAirhour.WIND_SPEED.Trim() == "")
            {
                this.Tips.AppendLine("风速不能为空");
                return false;
            }
            //风向
            if (tEnvFillAirhour.WIND_DIRECTION.Trim() == "")
            {
                this.Tips.AppendLine("风向不能为空");
                return false;
            }
            //API指数
            if (tEnvFillAirhour.VISIBLITY.Trim() == "")
            {
                this.Tips.AppendLine("API指数不能为空");
                return false;
            }
            //空气质量指数
            if (tEnvFillAirhour.HUMIDITY.Trim() == "")
            {
                this.Tips.AppendLine("空气质量指数不能为空");
                return false;
            }
            //备注1
            if (tEnvFillAirhour.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillAirhour.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillAirhour.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillAirhour.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillAirhour.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
