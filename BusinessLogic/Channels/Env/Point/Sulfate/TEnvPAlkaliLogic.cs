using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using i3.ValueObject.Channels.Env.Point.Sulfate;
using i3.DataAccess.Channels.Env.Point.Sulfate;

namespace i3.BusinessLogic.Channels.Env.Point.Sulfate
{
    /// <summary>
    /// 功能：硫酸盐化速率监测点表
    /// 创建日期：2013-06-15
    /// 创建人：ljn
    /// </summary>
    public class TEnvPAlkaliLogic : LogicBase
    {
        TEnvPAlkaliVo tEnvPAlkali = new TEnvPAlkaliVo();
        TEnvPAlkaliAccess access;

        public TEnvPAlkaliLogic()
        {
            access = new TEnvPAlkaliAccess();
        }

        public TEnvPAlkaliLogic(TEnvPAlkaliVo _tEnvPAlkali)
        {
            tEnvPAlkali = _tEnvPAlkali;
            access = new TEnvPAlkaliAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPAlkali">对象</param>
        /// <returns>返回行数</returns>s
        public int GetSelectResultCount(TEnvPAlkaliVo tEnvPAlkali)
        {
            return access.GetSelectResultCount(tEnvPAlkali);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPAlkaliVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPAlkali">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPAlkaliVo Details(TEnvPAlkaliVo tEnvPAlkali)
        {
            return access.Details(tEnvPAlkali);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPAlkali">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPAlkaliVo> SelectByObject(TEnvPAlkaliVo tEnvPAlkali, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPAlkali, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPAlkali">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPAlkaliVo tEnvPAlkali, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPAlkali, iIndex, iCount);
        }


        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPAlkali">对象</param>
        /// <returns></returns>
        public TEnvPAlkaliVo SelectByObject(TEnvPAlkaliVo tEnvPAlkali)
        {
            return access.SelectByObject(tEnvPAlkali);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAlkaliVo tEnvPAlkali)
        {
            return access.Create(tEnvPAlkali);
        }
        /// <summary>
        /// 对象添加(ljn.2013/6/15)
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPAlkaliVo tEnvPAir, string Number)
        {
            return access.Create(tEnvPAir, Number);
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAlkali">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAlkaliVo tEnvPAlkali)
        {
            return access.Edit(tEnvPAlkali);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPAlkali_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPAlkali_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPAlkaliVo tEnvPAlkali_UpdateSet, TEnvPAlkaliVo tEnvPAlkali_UpdateWhere)
        {
            return access.Edit(tEnvPAlkali_UpdateSet, tEnvPAlkali_UpdateWhere);
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
        public bool Delete(TEnvPAlkaliVo tEnvPAlkali)
        {
            return access.Delete(tEnvPAlkali);
        }

        //监测项目复制
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            return access.PasteItem(strFID, strTID, strSerial);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSeabath"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPAlkaliVo tEnvPAir)
        {
            return access.SelectByTable(tEnvPAir);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        #region//字段
        public override bool Validate()
        {
            //主键ID
            if (tEnvPAlkali.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPAlkali.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPAlkali.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPAlkali.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //测点编号
            if (tEnvPAlkali.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("测点编号不能为空");
                return false;
            }
            //测点名称
            if (tEnvPAlkali.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("测点名称不能为空");
                return false;
            }
            //行政区ID（字典项）
            if (tEnvPAlkali.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("行政区ID（字典项）不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPAlkali.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPAlkali.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPAlkali.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPAlkali.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPAlkali.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPAlkali.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //测点位置
            if (tEnvPAlkali.LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("测点位置不能为空");
                return false;
            }
            //删除标记
            if (tEnvPAlkali.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPAlkali.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPAlkali.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPAlkali.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPAlkali.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPAlkali.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPAlkali.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }
            //
            if (tEnvPAlkali.COLUMN_21.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }

            return true;
        }
        #endregion 
    }
}
