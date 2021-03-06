using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.Resource;
using i3.DataAccess.Sys.Resource;

namespace i3.BusinessLogic.Sys.Resource
{
    /// <summary>
    /// 功能：序列号管理
    /// 创建日期：2011-04-07
    /// 创建人：郑义
    /// </summary>
    public class TSysSerialLogic : LogicBase
    {

        TSysSerialVo tSysSerial = new TSysSerialVo();
        TSysSerialAccess access;

        public TSysSerialLogic()
        {
            access = new TSysSerialAccess();
        }

        public TSysSerialLogic(TSysSerialVo _tSysSerial)
        {
            tSysSerial = _tSysSerial;
            access = new TSysSerialAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tSysSerial">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TSysSerialVo tSysSerial)
        {
            return access.GetSelectResultCount(tSysSerial);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TSysSerialVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tSysSerial">对象条件</param>
        /// <returns>对象</returns>
        public TSysSerialVo Details(TSysSerialVo tSysSerial)
        {
            return access.Details(tSysSerial);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tSysSerial">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TSysSerialVo> SelectByObject(TSysSerialVo tSysSerial, int iIndex, int iCount)
        {
            return access.SelectByObject(tSysSerial, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tSysSerial">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TSysSerialVo tSysSerial, int iIndex, int iCount)
        {
            return access.SelectByTable(tSysSerial, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tSysSerial"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TSysSerialVo tSysSerial)
        {
            return access.SelectByTable(tSysSerial);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tSysSerial">对象</param>
        /// <returns></returns>
        public TSysSerialVo SelectByObject(TSysSerialVo tSysSerial)
        {
            return access.SelectByObject(tSysSerial);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TSysSerialVo tSysSerial)
        {
            return access.Create(tSysSerial);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tSysSerial">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TSysSerialVo tSysSerial)
        {
            return access.Edit(tSysSerial);
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
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tSysSerial.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //编码
            if (tSysSerial.SERIAL_CODE.Trim() == "")
            {
                this.Tips.AppendLine("编码不能为空");
                return false;
            }
            //名称
            if (tSysSerial.SERIAL_NAME.Trim() == "")
            {
                this.Tips.AppendLine("名称不能为空");
                return false;
            }
            //分组
            if (tSysSerial.SERIAL_GROUP.Trim() == "")
            {
                this.Tips.AppendLine("分组不能为空");
                return false;
            }
            //序列号
            if (tSysSerial.SERIAL_NUMBER.Trim() == "")
            {
                this.Tips.AppendLine("序列号不能为空");
                return false;
            }
            //位数
            if (tSysSerial.LENGTH.Trim() == "")
            {
                this.Tips.AppendLine("位数不能为空");
                return false;
            }
            //前缀
            if (tSysSerial.PREFIX.Trim() == "")
            {
                this.Tips.AppendLine("前缀不能为空");
                return false;
            }
            //粒度
            if (tSysSerial.GRANULARITY.Trim() == "")
            {
                this.Tips.AppendLine("粒度不能为空");
                return false;
            }
            //最小值
            if (tSysSerial.MIN.Trim() == "")
            {
                this.Tips.AppendLine("最小值不能为空");
                return false;
            }
            //最大值
            if (tSysSerial.MAX.Trim() == "")
            {
                this.Tips.AppendLine("最大值不能为空");
                return false;
            }
            //创建人
            if (tSysSerial.CREATE_ID.Trim() == "")
            {
                this.Tips.AppendLine("创建人不能为空");
                return false;
            }
            //创建日期
            if (tSysSerial.CREATE_TIME.Trim() == "")
            {
                this.Tips.AppendLine("创建日期不能为空");
                return false;
            }
            //备注
            if (tSysSerial.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //备注1
            if (tSysSerial.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tSysSerial.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tSysSerial.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }
        /// <summary>
        /// 功能描述：获取序号
        /// 创建人：　熊卫华
        /// 创建日期：2007-1-22
        /// </summary>
        /// <param name="strSerialType">类型</param>
        /// <returns>序号</returns>
        public string GetSerialNumber(string strSerialType)
        {
            return access.GetSerialNumber(strSerialType);
        }

        /// <summary>
        /// 功能描述：获取序号串
        /// 创建人：　潘德军
        /// 创建日期：2012-12-20
        /// </summary>
        /// <param name="strSerialType">类型</param>
        /// <param name="iSerialCount">序列号个数</param>
        /// <returns>序号</returns>
        public string GetSerialNumberList(string strSerialType, int iSerialCount)
        {
            return access.GetSerialNumberList(strSerialType, iSerialCount);
        }
    }
}
