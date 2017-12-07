using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.NoiseFun;
using System.Data;
using i3.DataAccess.Channels.Env.Point.NoiseFun;

namespace i3.BusinessLogic.Channels.Env.Point.NoiseFun
{
    /// <summary>
    /// 功能：功能区噪声
    /// 创建日期：2013-06-15
    /// 创建人：魏林
    /// </summary>
    public class TEnvPNoiseFunctionLogic : LogicBase
    {

        TEnvPNoiseFunctionVo tEnvPNoiseFunction = new TEnvPNoiseFunctionVo();
        TEnvPNoiseFunctionAccess access;

        public TEnvPNoiseFunctionLogic()
        {
            access = new TEnvPNoiseFunctionAccess();
        }

        public TEnvPNoiseFunctionLogic(TEnvPNoiseFunctionVo _tEnvPNoiseFunction)
        {
            tEnvPNoiseFunction = _tEnvPNoiseFunction;
            access = new TEnvPNoiseFunctionAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPNoiseFunction">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            return access.GetSelectResultCount(tEnvPNoiseFunction);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPNoiseFunctionVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPNoiseFunction">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPNoiseFunctionVo Details(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            return access.Details(tEnvPNoiseFunction);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPNoiseFunction">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPNoiseFunctionVo> SelectByObject(TEnvPNoiseFunctionVo tEnvPNoiseFunction, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPNoiseFunction, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPNoiseFunction">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPNoiseFunctionVo tEnvPNoiseFunction, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPNoiseFunction, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPNoiseFunction"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            return access.SelectByTable(tEnvPNoiseFunction);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPNoiseFunction">对象</param>
        /// <returns></returns>
        public TEnvPNoiseFunctionVo SelectByObject(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            return access.SelectByObject(tEnvPNoiseFunction);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            return access.Create(tEnvPNoiseFunction);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseFunction">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            return access.Edit(tEnvPNoiseFunction);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPNoiseFunction_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPNoiseFunction_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPNoiseFunctionVo tEnvPNoiseFunction_UpdateSet, TEnvPNoiseFunctionVo tEnvPNoiseFunction_UpdateWhere)
        {
            return access.Edit(tEnvPNoiseFunction_UpdateSet, tEnvPNoiseFunction_UpdateWhere);
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
        public bool Delete(TEnvPNoiseFunctionVo tEnvPNoiseFunction)
        {
            return access.Delete(tEnvPNoiseFunction);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvPNoiseFunction.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //年度
            if (tEnvPNoiseFunction.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvPNoiseFunction.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //测站ID（字典项）
            if (tEnvPNoiseFunction.SATAIONS_ID.Trim() == "")
            {
                this.Tips.AppendLine("测站ID（字典项）不能为空");
                return false;
            }
            //功能区ID（字典项）
            if (tEnvPNoiseFunction.FUNCTION_AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("功能区ID（字典项）不能为空");
                return false;
            }
            //测点编号
            if (tEnvPNoiseFunction.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("测点编号不能为空");
                return false;
            }
            //测点名称
            if (tEnvPNoiseFunction.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("测点名称不能为空");
                return false;
            }
            //标准昼间
            if (tEnvPNoiseFunction.STANDARD_LIGHT.Trim() == "")
            {
                this.Tips.AppendLine("标准昼间不能为空");
                return false;
            }
            //标准夜间
            if (tEnvPNoiseFunction.STANDARD_NIGHT.Trim() == "")
            {
                this.Tips.AppendLine("标准夜间不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPNoiseFunction.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPNoiseFunction.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPNoiseFunction.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPNoiseFunction.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPNoiseFunction.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPNoiseFunction.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //测点位置
            if (tEnvPNoiseFunction.LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("测点位置不能为空");
                return false;
            }
            //删除标记
            if (tEnvPNoiseFunction.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPNoiseFunction.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPNoiseFunction.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPNoiseFunction.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPNoiseFunction.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPNoiseFunction.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPNoiseFunction.REMARK5.Trim() == "")
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
        public bool Create(TEnvPNoiseFunctionVo TEnvPNoiseFunction, string strSerial)
        {
            return access.Create(TEnvPNoiseFunction, strSerial);
        }

        
        /// <summary>
        /// 监测项目的复制逻辑
        /// </summary>
        /// <param name="strFID"></param>
        /// <param name="strTID"></param>
        /// <param name="strSerial"></param>
        /// <returns></returns>
        public string PasteItem(string strFID, string strTID, string strSerial)
        {
            return access.PasteItem(strFID, strTID, strSerial);
        }
        /// <summary>
        /// 根据年份和月份、功能区获取监测点信息
        /// </summary>
        /// <returns></returns>
        public DataTable PointByTable(string strYear, string strMonth, string AreaCode)
        {
            return access.PointByTable(strYear, strMonth, AreaCode);
        }

    }

}
