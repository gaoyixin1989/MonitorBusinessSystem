using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.OA.PART.SAMPLE;
using System.Data;
using i3.DataAccess.Channels.OA.PART.SAMPLE;

namespace i3.BusinessLogic.Channels.OA.PART.SAMPLE
{
    /// <summary>
    /// 功能：标准样品领用信息
    /// 创建日期：2013-09-13
    /// 创建人：魏林
    /// </summary>
    public class TOaPartstandCollarLogic : LogicBase
    {

        TOaPartstandCollarVo tOaPartstandCollar = new TOaPartstandCollarVo();
        TOaPartstandCollarAccess access;

        public TOaPartstandCollarLogic()
        {
            access = new TOaPartstandCollarAccess();
        }

        public TOaPartstandCollarLogic(TOaPartstandCollarVo _tOaPartstandCollar)
        {
            tOaPartstandCollar = _tOaPartstandCollar;
            access = new TOaPartstandCollarAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaPartstandCollar">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaPartstandCollarVo tOaPartstandCollar)
        {
            return access.GetSelectResultCount(tOaPartstandCollar);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaPartstandCollarVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaPartstandCollar">对象条件</param>
        /// <returns>对象</returns>
        public TOaPartstandCollarVo Details(TOaPartstandCollarVo tOaPartstandCollar)
        {
            return access.Details(tOaPartstandCollar);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaPartstandCollar">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaPartstandCollarVo> SelectByObject(TOaPartstandCollarVo tOaPartstandCollar, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaPartstandCollar, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaPartstandCollar">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaPartstandCollarVo tOaPartstandCollar, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaPartstandCollar, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaPartstandCollar"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaPartstandCollarVo tOaPartstandCollar)
        {
            return access.SelectByTable(tOaPartstandCollar);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaPartstandCollar">对象</param>
        /// <returns></returns>
        public TOaPartstandCollarVo SelectByObject(TOaPartstandCollarVo tOaPartstandCollar)
        {
            return access.SelectByObject(tOaPartstandCollar);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartstandCollarVo tOaPartstandCollar)
        {
            return access.Create(tOaPartstandCollar);
        }

        /// <summary>
        /// 对象领用明细和扣除库存数量
        /// </summary>
        /// <param name="tOaPartstandCollar">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaPartstandCollarVo tOaPartstandCollar, bool b)
        {
            return access.Create(tOaPartstandCollar, b);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartstandCollar">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartstandCollarVo tOaPartstandCollar)
        {
            return access.Edit(tOaPartstandCollar);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaPartstandCollar_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaPartstandCollar_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaPartstandCollarVo tOaPartstandCollar_UpdateSet, TOaPartstandCollarVo tOaPartstandCollar_UpdateWhere)
        {
            return access.Edit(tOaPartstandCollar_UpdateSet, tOaPartstandCollar_UpdateWhere);
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
        public bool Delete(TOaPartstandCollarVo tOaPartstandCollar)
        {
            return access.Delete(tOaPartstandCollar);
        }

        /// <summary>
        /// 获取领用样品明细
        /// </summary>
        /// <param name="tOaPartCollar"></param>
        /// <param name="tOaPartInfor"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public DataTable SelectUnionPartByTable(TOaPartstandCollarVo tOaPartstandCollar, TOaPartstandInfoVo tOaPartstandInfor, int iIndex, int iCount)
        {
            return access.SelectUnionPartByTable(tOaPartstandCollar, tOaPartstandInfor, iIndex, iCount);
        }

        /// <summary>
        /// 获取领用样品明细总记录数
        /// </summary>
        /// <param name="tOaPartCollar"></param>
        /// <param name="tOaPartInfor"></param>
        /// <param name="iIndex"></param>
        /// <param name="iCount"></param>
        /// <returns></returns>
        public int GetUnionPartByTableCount(TOaPartstandCollarVo tOaPartstandCollar, TOaPartstandInfoVo tOaPartstandInfor)
        {
            return access.GetUnionPartByTableCount(tOaPartstandCollar, tOaPartstandInfor);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tOaPartstandCollar.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //物料ID
            if (tOaPartstandCollar.SAMPLE_ID.Trim() == "")
            {
                this.Tips.AppendLine("物料ID不能为空");
                return false;
            }
            //领用数量
            if (tOaPartstandCollar.USED_QUANTITY.Trim() == "")
            {
                this.Tips.AppendLine("领用数量不能为空");
                return false;
            }
            //领用人ID
            if (tOaPartstandCollar.USER_ID.Trim() == "")
            {
                this.Tips.AppendLine("领用人ID不能为空");
                return false;
            }
            //领用日期
            if (tOaPartstandCollar.LASTIN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("领用日期不能为空");
                return false;
            }
            //领用理由
            if (tOaPartstandCollar.REASON.Trim() == "")
            {
                this.Tips.AppendLine("领用理由不能为空");
                return false;
            }
            //备注1
            if (tOaPartstandCollar.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tOaPartstandCollar.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaPartstandCollar.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tOaPartstandCollar.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tOaPartstandCollar.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }


    }

}
