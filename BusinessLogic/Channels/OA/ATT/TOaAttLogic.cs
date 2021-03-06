using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.ATT;
using i3.DataAccess.Channels.OA.ATT;

namespace i3.BusinessLogic.Channels.OA.ATT
{
    /// <summary>
    /// 功能：附件信息
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaAttLogic : LogicBase
    {

        TOaAttVo tOaAtt = new TOaAttVo();
        TOaAttAccess access;

        public TOaAttLogic()
        {
            access = new TOaAttAccess();
        }

        public TOaAttLogic(TOaAttVo _tOaAtt)
        {
            tOaAtt = _tOaAtt;
            access = new TOaAttAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaAtt">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaAttVo tOaAtt)
        {
            return access.GetSelectResultCount(tOaAtt);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaAttVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaAtt">对象条件</param>
        /// <returns>对象</returns>
        public TOaAttVo Details(TOaAttVo tOaAtt)
        {
            return access.Details(tOaAtt);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaAttVo> SelectByObject(TOaAttVo tOaAtt, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaAtt, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaAtt">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaAttVo tOaAtt, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaAtt, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaAtt"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaAttVo tOaAtt)
        {
            return access.SelectByTable(tOaAtt);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaAtt">对象</param>
        /// <returns></returns>
        public TOaAttVo SelectByObject(TOaAttVo tOaAtt)
        {
            return access.SelectByObject(tOaAtt);
        }

                /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaAtt">对象条件</param>
        /// <returns>对象</returns>
        public DataTable Detail(string Business_ID, string fill_ID)
        {
            return access.Detail(Business_ID, fill_ID); 
        }
        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaAttVo tOaAtt)
        {
            return access.Create(tOaAtt);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaAtt">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaAttVo tOaAtt)
        {
            return access.Edit(tOaAtt);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaAtt_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaAtt_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaAttVo tOaAtt_UpdateSet, TOaAttVo tOaAtt_UpdateWhere)
        {
            return access.Edit(tOaAtt_UpdateSet, tOaAtt_UpdateWhere);
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
        public bool Delete(TOaAttVo tOaAtt)
        {
            return access.Delete(tOaAtt);
        }
        /// <summary>
        /// 查询站务管理---人事档案信息的考核历史记录，可以多个附件
        /// </summary>
        /// <param name="tOaAtt"></param>
        /// <returns></returns>
        public DataTable SelectUnionEmployeTable(TOaAttVo tOaAtt)
        {
            return access.SelectUnionEmployeTable(tOaAtt);
        }
        public DataTable DetailFill(string id)
        {
            return access.DetailFill(id);
        }
                /// <returns>对象</returns>
        public DataTable DetailFill_ID(string ID)
        {
            return access.DetailFill_ID(ID);
        }
                /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public bool DeleteDetail(string id)
        {
            return access.DeleteDetail(id);
        }
                /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool DeleteInfo(string BusinessID)
        {
            return access.DeleteInfo(BusinessID);
        }
        /// <summary>
        /// 功能描述：获取监测子任务中第一条附件信息
        /// 创建时间：2013-1-18
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strTaskID">监测任务</param>
        /// <param name="strBusinessType">附件类型</param>
        /// <returns></returns>
        public TOaAttVo getImgByTask(string strTaskID, string strBusinessType)
        {
            List<TOaAttVo> listAtt = access.getImgByTask(strTaskID, strBusinessType);
            if (listAtt.Count > 0)
            {
                return listAtt[0] as TOaAttVo;
            }
            return new TOaAttVo();
        }

        public DataTable Detail_type(string Business_ID)
        {
            return access.Detail_type(Business_ID);
        }
                /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public DataTable DetailZW(string id, string strFileType)
        {
            return access.DetailZW(id, strFileType);
        }

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tOaAtt.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //业务ID
            if (tOaAtt.BUSINESS_ID.Trim() == "")
            {
                this.Tips.AppendLine("业务ID不能为空");
                return false;
            }
            //业务类型
            if (tOaAtt.BUSINESS_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("业务类型不能为空");
                return false;
            }
            //附件名称
            if (tOaAtt.ATTACH_NAME.Trim() == "")
            {
                this.Tips.AppendLine("附件名称不能为空");
                return false;
            }
            //附件类型
            if (tOaAtt.ATTACH_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("附件类型不能为空");
                return false;
            }
            //附件路径
            if (tOaAtt.UPLOAD_PATH.Trim() == "")
            {
                this.Tips.AppendLine("附件路径不能为空");
                return false;
            }
            //上传日期
            if (tOaAtt.UPLOAD_DATE.Trim() == "")
            {
                this.Tips.AppendLine("上传日期不能为空");
                return false;
            }
            //上传人ID
            if (tOaAtt.UPLOAD_PERSON.Trim() == "")
            {
                this.Tips.AppendLine("上传人ID不能为空");
                return false;
            }
            //附件说明
            if (tOaAtt.DESCRIPTION.Trim() == "")
            {
                this.Tips.AppendLine("附件说明不能为空");
                return false;
            }
            //备注
            if (tOaAtt.REMARKS.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }
            //
            if (tOaAtt.FILL_ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            return true;
        }

    }

}
