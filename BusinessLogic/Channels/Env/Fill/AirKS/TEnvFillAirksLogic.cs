using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.AirKS;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.AirKS;

namespace i3.BusinessLogic.Channels.Env.Fill.AirKS
{

    /// <summary>
    /// 功能：环境空气(科室)填报
    /// 创建日期：2013-07-03
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvFillAirksLogic : LogicBase
    {

        TEnvFillAirksVo tEnvFillAirks = new TEnvFillAirksVo();
        TEnvFillAirksAccess access;

        public TEnvFillAirksLogic()
        {
            access = new TEnvFillAirksAccess();
        }

        public TEnvFillAirksLogic(TEnvFillAirksVo _tEnvFillAirks)
        {
            tEnvFillAirks = _tEnvFillAirks;
            access = new TEnvFillAirksAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAirks">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAirksVo tEnvFillAirks)
        {
            return access.GetSelectResultCount(tEnvFillAirks);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAirksVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAirks">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAirksVo Details(TEnvFillAirksVo tEnvFillAirks)
        {
            return access.Details(tEnvFillAirks);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAirks">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAirksVo> SelectByObject(TEnvFillAirksVo tEnvFillAirks, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAirks, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAirks">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAirksVo tEnvFillAirks, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAirks, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAirks"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAirksVo tEnvFillAirks)
        {
            return access.SelectByTable(tEnvFillAirks);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAirks">对象</param>
        /// <returns></returns>
        public TEnvFillAirksVo SelectByObject(TEnvFillAirksVo tEnvFillAirks)
        {
            return access.SelectByObject(tEnvFillAirks);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAirksVo tEnvFillAirks)
        {
            return access.Create(tEnvFillAirks);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirks">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirksVo tEnvFillAirks)
        {
            return access.Edit(tEnvFillAirks);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAirks_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAirks_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAirksVo tEnvFillAirks_UpdateSet, TEnvFillAirksVo tEnvFillAirks_UpdateWhere)
        {
            return access.Edit(tEnvFillAirks_UpdateSet, tEnvFillAirks_UpdateWhere);
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
        public bool Delete(TEnvFillAirksVo tEnvFillAirks)
        {
            return access.Delete(tEnvFillAirks);
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
        /// 获取环境质量数据填报数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// /// <param name="dtShow">填报主表显示的列表信息 格式：有两列，第一列是字段名，第二列是中文名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <returns></returns>
        public DataTable GetFillData(string strWhere, DataTable dtShow, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial)
        {
            return access.GetFillData(strWhere, dtShow, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillAirks.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillAirks.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //年度
            if (tEnvFillAirks.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvFillAirks.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //监测起始月
            if (tEnvFillAirks.BEGIN_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("监测起始月不能为空");
                return false;
            }
            //监测起始日
            if (tEnvFillAirks.BEGIN_DAY.Trim() == "")
            {
                this.Tips.AppendLine("监测起始日不能为空");
                return false;
            }
            //监测起始时
            if (tEnvFillAirks.BEGIN_HOUR.Trim() == "")
            {
                this.Tips.AppendLine("监测起始时不能为空");
                return false;
            }
            //监测起始分
            if (tEnvFillAirks.BEGIN_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("监测起始分不能为空");
                return false;
            }
            //监测结束月
            if (tEnvFillAirks.END_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("监测结束月不能为空");
                return false;
            }
            //监测结束日
            if (tEnvFillAirks.END_DAY.Trim() == "")
            {
                this.Tips.AppendLine("监测结束日不能为空");
                return false;
            }
            //监测结束时
            if (tEnvFillAirks.END_HOUR.Trim() == "")
            {
                this.Tips.AppendLine("监测结束时不能为空");
                return false;
            }
            //监测结束分
            if (tEnvFillAirks.END_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("监测结束分不能为空");
                return false;
            }
            //备注1
            if (tEnvFillAirks.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillAirks.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillAirks.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillAirks.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillAirks.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
