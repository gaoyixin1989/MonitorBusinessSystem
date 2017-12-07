using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Air30;
using System.Data;
using i3.DataAccess.Channels.Env.Point.Air30;

namespace i3.BusinessLogic.Channels.Env.Point.Air30
{
    /// <summary>
    /// 功能：双三十废气
    /// 创建日期：2013-06-17
    /// 创建人：魏林
    /// </summary>
    public class TEnvPAir30Logic : LogicBase
    {

        TEnvPAir30Vo tEnvPAir30 = new TEnvPAir30Vo();
        TEnvPAir30Access access;

        public TEnvPAir30Logic()
        {
            access = new TEnvPAir30Access();
        }

        public TEnvPAir30Logic(TEnvPAir30Vo _tEnvPAir30)
        {
            tEnvPAir30 = _tEnvPAir30;
            access = new TEnvPAir30Access();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPAir30">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPAir30Vo tEnvPAir30)
        {
            return access.GetSelectResultCount(tEnvPAir30);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPAir30Vo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPAir30">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPAir30Vo Details(TEnvPAir30Vo tEnvPAir30)
        {
            return access.Details(tEnvPAir30);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPAir30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPAir30Vo> SelectByObject(TEnvPAir30Vo tEnvPAir30, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPAir30, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPAir30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPAir30Vo tEnvPAir30, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPAir30, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPAir30"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPAir30Vo tEnvPAir30)
        {
            return access.SelectByTable(tEnvPAir30);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPAir30">对象</param>
        /// <returns></returns>
        public TEnvPAir30Vo SelectByObject(TEnvPAir30Vo tEnvPAir30)
        {
            return access.SelectByObject(tEnvPAir30);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAir30Vo tEnvPAir30)
        {
            return access.Create(tEnvPAir30);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir30">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAir30Vo tEnvPAir30)
        {
            return access.Edit(tEnvPAir30);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAir30_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPAir30_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAir30Vo tEnvPAir30_UpdateSet, TEnvPAir30Vo tEnvPAir30_UpdateWhere)
        {
            return access.Edit(tEnvPAir30_UpdateSet, tEnvPAir30_UpdateWhere);
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
        public bool Delete(TEnvPAir30Vo tEnvPAir30)
        {
            return access.Delete(tEnvPAir30);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPAir30.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPAir30.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPAir30.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPAir30.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //测点编号
            if (tEnvPAir30.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("测点编号不能为空");
                return false;
            }
            //测点名称
            if (tEnvPAir30.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("测点名称不能为空");
                return false;
            }
            //行政区ID（字典项）
            if (tEnvPAir30.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("行政区ID（字典项）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPAir30.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPAir30.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPAir30.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPAir30.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPAir30.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPAir30.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //测点位置
            if (tEnvPAir30.LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("测点位置不能为空");
                return false;
            }
            //删除标记
            if (tEnvPAir30.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPAir30.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPAir30.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPAir30.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPAir30.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPAir30.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPAir30.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAir30Vo TEnvPAir30, string strSerial)
        {
            return access.Create(TEnvPAir30, strSerial);
        }

        /// <summary>
        /// 河流垂线监测项目的复制逻辑
        /// </summary>
        /// <param name="strFID"></param>
        /// <param name="strTID"></param>
        /// <param name="strSerial"></param>
        /// <returns></returns>
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            return access.PasteItem(strFID, strTID, strSerial);
        }
    }

}
