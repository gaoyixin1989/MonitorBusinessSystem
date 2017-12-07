using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.Air;
using i3.DataAccess.Channels.Env.Fill.Air;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Fill.Air
{
    /// <summary>
    /// 功能：环境空气 填报监测项目 
    /// 创建日期：2013-05-21
    /// 创建人：钟杰华
    /// 修改时间：2013-06-24
    /// 修改人：刘静楠
    /// </summary>
    public class TEnvFillAirHourLogic : LogicBase
    {

        TEnvFillAirHourVo tEnvFillAirHour = new TEnvFillAirHourVo();
        TEnvFillAirHourAccess access;

        public TEnvFillAirHourLogic()
        {
            access = new TEnvFillAirHourAccess();
        }

        public TEnvFillAirHourLogic(TEnvFillAirHourVo _tEnvFillAirHour)
        {
            tEnvFillAirHour = _tEnvFillAirHour;
            access = new TEnvFillAirHourAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAirHour">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirHourVo tEnvFillAirHour)
        {
            return access.GetSelectResultCount(tEnvFillAirHour);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirHourVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAirHour">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirHourVo Details(TEnvFillAirHourVo tEnvFillAirHour)
        {
            return access.Details(tEnvFillAirHour);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAirHour">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirHourVo> SelectByObject(TEnvFillAirHourVo tEnvFillAirHour, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAirHour, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAirHour">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirHourVo tEnvFillAirHour, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAirHour, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAirHour"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirHourVo tEnvFillAirHour)
        {
            return access.SelectByTable(tEnvFillAirHour);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAirHour">对象</param>
        /// <returns></returns>
        public TEnvFillAirHourVo SelectByObject(TEnvFillAirHourVo tEnvFillAirHour)
        {
            return access.SelectByObject(tEnvFillAirHour);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirHourVo tEnvFillAirHour)
        {
            return access.Create(tEnvFillAirHour);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirHour">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirHourVo tEnvFillAirHour)
        {
            return access.Edit(tEnvFillAirHour);
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
        /// 根据条件删除数据
        /// </summary>
        /// <param name="where">条件，每个条件间要用and/or来连接
        /// <returns></returns>
        public void DeleteByWhere(string where)
        {
            access.DeleteByWhere(where);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillAirHour.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //数据填报ID
            if (tEnvFillAirHour.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("数据填报ID不能为空");
                return false;
            }
            //监测项ID
            if (tEnvFillAirHour.ITEM_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测项ID不能为空");
                return false;
            }
            //监测值
            if (tEnvFillAirHour.ITEM_VALUE.Trim() == "")
            {
                this.Tips.AppendLine("监测值不能为空");
                return false;
            }
            //空气质量指数
            if (tEnvFillAirHour.IAQI.Trim() == "")
            {
                this.Tips.AppendLine("空气质量指数不能为空");
                return false;
            }
            //评价
            if (tEnvFillAirHour.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标倍数
            if (tEnvFillAirHour.UP_DOUBLE.Trim() == "")
            {
                this.Tips.AppendLine("超标倍数不能为空");
                return false;
            }
            //备注1
            if (tEnvFillAirHour.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillAirHour.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillAirHour.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillAirHour.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillAirHour.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }


        /// <summary>
        /// 根据条件返回集合
        /// </summary>
        /// <param name="where">自己拼接的条件，第个条件要用and/or连接</param>
        /// <returns></returns>
        public List<TEnvFillAirHourVo> SelectByObject(string where)
        {
            return access.SelectByObject(where);
        }

        /// <summary>
        /// 根据条件返回DataTable
        /// </summary>
        /// <param name="where">自己拼接的条件，第个条件要用and/or连接</param>
        /// <returns></returns>
        public DataTable SelectByTable(string where)
        {
            return access.SelectByTable(where);
        }

        /// <summary>
        /// 获取集合
        /// </summary>
        /// <param name="model">条件实体</param>
        /// <returns></returns>
        public List<TEnvFillAirHourVo> SelectByList(TEnvFillAirHourVo model)
        {
            return access.SelectByList(model);
        }

    }
}
