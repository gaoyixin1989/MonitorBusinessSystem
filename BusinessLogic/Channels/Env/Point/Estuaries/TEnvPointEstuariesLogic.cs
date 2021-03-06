using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.Env.Point.Estuaries;
using i3.DataAccess.Channels.Env.Point.Estuaries;

namespace i3.BusinessLogic.Channels.Env.Point.Estuaries
{
    /// <summary>
    /// 功能：入海河口监测点表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠 
    /// time:2013-06-20
    /// </summary>
    public class TEnvPointEstuariesLogic : LogicBase
    {

        TEnvPointEstuariesVo tEnvPointEstuaries = new TEnvPointEstuariesVo();
        TEnvPointEstuariesAccess access;

        public TEnvPointEstuariesLogic()
        {
            access = new TEnvPointEstuariesAccess();
        }

        public TEnvPointEstuariesLogic(TEnvPointEstuariesVo _tEnvPointEstuaries)
        {
            tEnvPointEstuaries = _tEnvPointEstuaries;
            access = new TEnvPointEstuariesAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPointEstuaries">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            return access.GetSelectResultCount(tEnvPointEstuaries);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPointEstuaries">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPointEstuariesVo Details(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            return access.Details(tEnvPointEstuaries);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPointEstuaries">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPointEstuariesVo> SelectByObject(TEnvPointEstuariesVo tEnvPointEstuaries, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPointEstuaries, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPointEstuaries">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPointEstuariesVo tEnvPointEstuaries, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPointEstuaries, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPointEstuaries"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            return access.SelectByTable(tEnvPointEstuaries);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPointEstuaries">对象</param>
        /// <returns></returns>
        public TEnvPointEstuariesVo SelectByObject(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            return access.SelectByObject(tEnvPointEstuaries);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            return access.Create(tEnvPointEstuaries);
        }
        /// <summary>
        /// 对象添加(ljn,2012/6/17)
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPointEstuariesVo tEnvPointEstuaries, string strSerial)
        {
            return access.Create(tEnvPointEstuaries, strSerial);
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuaries">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            return access.Edit(tEnvPointEstuaries);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPointEstuaries_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPointEstuaries_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPointEstuariesVo tEnvPointEstuaries_UpdateSet, TEnvPointEstuariesVo tEnvPointEstuaries_UpdateWhere)
        {
            return access.Edit(tEnvPointEstuaries_UpdateSet, tEnvPointEstuaries_UpdateWhere);
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
        public bool Delete(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            return access.Delete(tEnvPointEstuaries);
        }

        /// <summary>
        /// 自定义查询  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <param name="iIndex">起始页</param>
        /// <param name="iCount">条数</param>
        /// <returns></returns>
        public DataTable SelectDefinedTadble(TEnvPointEstuariesVo tEnvPointEstuaries, int iIndex, int iCount)
        {
            return access.SelectDefinedTadble(tEnvPointEstuaries, iIndex, iCount);
        }

        /// <summary>
        /// 获取自定义查询结果总数  Create By Castle(胡方扬)  2012-11-19
        /// </summary>
        /// <param name="tBaseEvaluationInfo">对象</param>
        /// <returns></returns>
        public int GetSelecDefinedtResultCount(TEnvPointEstuariesVo tEnvPointEstuaries)
        {
            return access.GetSelecDefinedtResultCount(tEnvPointEstuaries);
        }
        //监测项目复制
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            return access.PasteItem(strFID, strTID, strSerial);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键
            if (tEnvPointEstuaries.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键不能为空");
                return false;
            }
            //年度
            if (tEnvPointEstuaries.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //测站ID
            if (tEnvPointEstuaries.STATION_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID不能为空");
                return false;
            }
            //河流ID
            if (tEnvPointEstuaries.RIVER_ID.Trim() == "")
            {
                this.Tips.AppendLine("河流ID不能为空");
                return false;
            }
            //流域ID
            if (tEnvPointEstuaries.VALLEY_ID.Trim() == "")
            {
                this.Tips.AppendLine("流域ID不能为空");
                return false;
            }
            //
            if (tEnvPointEstuaries.SECTION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //断面名称
            if (tEnvPointEstuaries.SECTION_NAME.Trim() == "")
            {
                this.Tips.AppendLine("断面名称不能为空");
                return false;
            }
            //省份ID
            if (tEnvPointEstuaries.PROVINCE_ID.Trim() == "")
            {
                this.Tips.AppendLine("省份ID不能为空");
                return false;
            }
            //所在地区
            if (tEnvPointEstuaries.AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("所在地区不能为空");
                return false;
            }
            //控制级别
            if (tEnvPointEstuaries.CONTRAL_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("控制级别不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPointEstuaries.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPointEstuaries.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPointEstuaries.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPointEstuaries.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPointEstuaries.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPointEstuaries.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //条件项
            if (tEnvPointEstuaries.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //使用状态(0为启用、1为停用)
            if (tEnvPointEstuaries.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("使用状态(0为启用、1为停用)不能为空");
                return false;
            }
            //序号
            if (tEnvPointEstuaries.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPointEstuaries.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPointEstuaries.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPointEstuaries.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPointEstuaries.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPointEstuaries.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
