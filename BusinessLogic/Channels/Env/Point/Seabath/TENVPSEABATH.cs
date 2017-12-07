using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Point.Seabath;
using i3.DataAccess.Channels.Env.Point.Seabath;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.Seabath
{
    /// <summary>
    /// 功能：海水浴场
    /// 创建日期：2013-06-18
    /// 创建人：刘静楠
    /// </summary>
    public class TEnvPSeabathLogic : LogicBase
    {

        TEnvPSeabathVo tEnvPSeabath = new TEnvPSeabathVo();
        TEnvPSeabathAccess access;

        public TEnvPSeabathLogic()
        {
            access = new TEnvPSeabathAccess();
        }

        public TEnvPSeabathLogic(TEnvPSeabathVo _tEnvPSeabath)
        {
            tEnvPSeabath = _tEnvPSeabath;
            access = new TEnvPSeabathAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvPSeabath">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvPSeabathVo tEnvPSeabath)
        {
            return access.GetSelectResultCount(tEnvPSeabath);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvPSeabathVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvPSeabath">对象条件</param>
        /// <returns>对象</returns>
        public TEnvPSeabathVo Details(TEnvPSeabathVo tEnvPSeabath)
        {
            return access.Details(tEnvPSeabath);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvPSeabath">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvPSeabathVo> SelectByObject(TEnvPSeabathVo tEnvPSeabath, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvPSeabath, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvPSeabath">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvPSeabathVo tEnvPSeabath, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvPSeabath, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvPSeabath"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvPSeabathVo tEnvPSeabath)
        {
            return access.SelectByTable(tEnvPSeabath);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvPSeabath">对象</param>
        /// <returns></returns>
        public TEnvPSeabathVo SelectByObject(TEnvPSeabathVo tEnvPSeabath)
        {
            return access.SelectByObject(tEnvPSeabath);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSeabathVo tEnvPSeabath)
        {
            return access.Create(tEnvPSeabath);
        }

        /// <summary>
        /// 对象添加(ljn.2013/6/14)
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvPSeabathVo tEnvPSeabath, string Number)
        {
            return access.Create(tEnvPSeabath, Number);
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSeabath">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSeabathVo tEnvPSeabath)
        {
            return access.Edit(tEnvPSeabath);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvPSeabath_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvPSeabath_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvPSeabathVo tEnvPSeabath_UpdateSet, TEnvPSeabathVo tEnvPSeabath_UpdateWhere)
        {
            return access.Edit(tEnvPSeabath_UpdateSet, tEnvPSeabath_UpdateWhere);
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
        public bool Delete(TEnvPSeabathVo tEnvPSeabath)
        {
            return access.Delete(tEnvPSeabath);
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
            //ID
            if (tEnvPSeabath.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //年份
            if (tEnvPSeabath.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年份不能为空");
                return false;
            }
            //月度
            if (tEnvPSeabath.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false;
            }
            //监测点名称
            if (tEnvPSeabath.POINT_NAME.Trim() == "") 
            {
                this.Tips.AppendLine("监测点名称不能为空");
                return false;
            }
            //监测点编码
            if (tEnvPSeabath.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //功能区代码
            if (tEnvPSeabath.FUNCTION_CODE.Trim() == "")
            {
                this.Tips.AppendLine("功能区代码不能为空");
                return false;
            }
            //海区ID(字典项)
            if (tEnvPSeabath.SEA_AREA_ID.Trim() == "")
            {
                this.Tips.AppendLine("海区ID(字典项)不能为空");
                return false;
            }
            //重点海域ID(字典项)
            if (tEnvPSeabath.KEY_SEA_ID.Trim() == "")
            {
                this.Tips.AppendLine("重点海域ID(字典项)不能为空");
                return false;
            }
            //国家编号
            if (tEnvPSeabath.COUNTRY_CODE.Trim() == "")
            {
                this.Tips.AppendLine("国家编号不能为空");
                return false;
            }
            //省份编号
            if (tEnvPSeabath.PROVINCE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("省份编号不能为空");
                return false;
            }
            //点位类别
            if (tEnvPSeabath.POINT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("点位类别不能为空");
                return false;
            }
            //经度（度）
            if (tEnvPSeabath.LONGITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("经度（度）不能为空");
                return false;
            }
            //经度（分）
            if (tEnvPSeabath.LONGITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("经度（分）不能为空");
                return false;
            }
            //经度（秒）
            if (tEnvPSeabath.LONGITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("经度（秒）不能为空");
                return false;
            }
            //纬度（度）
            if (tEnvPSeabath.LATITUDE_DEGREE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（度）不能为空");
                return false;
            }
            //纬度（分）
            if (tEnvPSeabath.LATITUDE_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("纬度（分）不能为空");
                return false;
            }
            //纬度（秒）
            if (tEnvPSeabath.LATITUDE_SECOND.Trim() == "")
            {
                this.Tips.AppendLine("纬度（秒）不能为空");
                return false;
            }
            //具体位置
            if (tEnvPSeabath.LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("具体位置不能为空");
                return false;
            }
            //条件项
            if (tEnvPSeabath.CONDITION_ID.Trim() == "")
            {
                this.Tips.AppendLine("条件项不能为空");
                return false;
            }
            //删除标记
            if (tEnvPSeabath.IS_DEL.Trim() == "")
            {
                this.Tips.AppendLine("删除标记不能为空");
                return false;
            }
            //序号
            if (tEnvPSeabath.NUM.Trim() == "")
            {
                this.Tips.AppendLine("序号不能为空");
                return false;
            }
            //备注1
            if (tEnvPSeabath.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvPSeabath.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvPSeabath.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvPSeabath.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvPSeabath.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }

}
