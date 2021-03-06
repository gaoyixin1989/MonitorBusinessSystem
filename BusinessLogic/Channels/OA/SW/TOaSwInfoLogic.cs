using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.SW;
using i3.DataAccess.Channels.OA.SW;

namespace i3.BusinessLogic.Channels.OA.SW
{
    /// <summary>
    /// 功能：收文管理
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaSwInfoLogic : LogicBase
    {

        TOaSwInfoVo tOaSwInfo = new TOaSwInfoVo();
        TOaSwInfoAccess access;

        public TOaSwInfoLogic()
        {
            access = new TOaSwInfoAccess();
        }

        public TOaSwInfoLogic(TOaSwInfoVo _tOaSwInfo)
        {
            tOaSwInfo = _tOaSwInfo;
            access = new TOaSwInfoAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaSwInfoVo tOaSwInfo)
        {
            return access.GetSelectResultCount(tOaSwInfo);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaSwInfoVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaSwInfo">对象条件</param>
        /// <returns>对象</returns>
        public TOaSwInfoVo Details(TOaSwInfoVo tOaSwInfo)
        {
            return access.Details(tOaSwInfo);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaSwInfoVo> SelectByObject(TOaSwInfoVo tOaSwInfo, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaSwInfo, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaSwInfoVo tOaSwInfo, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaSwInfo, iIndex, iCount);
        }

         /// <summary>
        /// 获取对象DataTable,指定用户的传阅收文
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForRead(TOaSwInfoVo tOaSwInfo,string strUserID, int iIndex, int iCount)
        {

            return access.SelectByTable_ForRead(tOaSwInfo,strUserID, iIndex, iCount);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ForRead(TOaSwInfoVo tOaSwInfo, string strUserID)
        {
            return access.GetSelectResultCount_ForRead(tOaSwInfo, strUserID);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaSwInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaSwInfoVo tOaSwInfo)
        {
            return access.SelectByTable(tOaSwInfo);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaSwInfo">对象</param>
        /// <returns></returns>
        public TOaSwInfoVo SelectByObject(TOaSwInfoVo tOaSwInfo)
        {
            return access.SelectByObject(tOaSwInfo);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaSwInfoVo tOaSwInfo)
        {
            return access.Create(tOaSwInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwInfoVo tOaSwInfo)
        {
            return access.Edit(tOaSwInfo);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaSwInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaSwInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaSwInfoVo tOaSwInfo_UpdateSet, TOaSwInfoVo tOaSwInfo_UpdateWhere)
        {
            return access.Edit(tOaSwInfo_UpdateSet, tOaSwInfo_UpdateWhere);
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
        public bool Delete(TOaSwInfoVo tOaSwInfo)
        {
            return access.Delete(tOaSwInfo);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tOaSwInfo.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //原文编号
            if (tOaSwInfo.FROM_CODE.Trim() == "")
            {
                this.Tips.AppendLine("原文编号不能为空");
                return false;
            }
            //收文编号
            if (tOaSwInfo.SW_CODE.Trim() == "")
            {
                this.Tips.AppendLine("收文编号不能为空");
                return false;
            }
            //来文单位
            if (tOaSwInfo.SW_FROM.Trim() == "")
            {
                this.Tips.AppendLine("来文单位不能为空");
                return false;
            }
            //收文份数
            if (tOaSwInfo.SW_COUNT.Trim() == "")
            {
                this.Tips.AppendLine("收文份数不能为空");
                return false;
            }
            //收文日期
            if (tOaSwInfo.SW_DATE.Trim() == "")
            {
                this.Tips.AppendLine("收文日期不能为空");
                return false;
            }
            //密级
            if (tOaSwInfo.SW_MJ.Trim() == "")
            {
                this.Tips.AppendLine("密级不能为空");
                return false;
            }
            //收文标题
            if (tOaSwInfo.SW_TITLE.Trim() == "")
            {
                this.Tips.AppendLine("收文标题不能为空");
                return false;
            }
            //收文类别
            if (tOaSwInfo.SW_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("收文类别不能为空");
                return false;
            }
            //签收人ID
            if (tOaSwInfo.SW_SIGN_ID.Trim() == "")
            {
                this.Tips.AppendLine("签收人ID不能为空");
                return false;
            }
            //签收日期
            if (tOaSwInfo.SW_SIGN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("签收日期不能为空");
                return false;
            }
            //登记人ID
            if (tOaSwInfo.SW_REG_ID.Trim() == "")
            {
                this.Tips.AppendLine("登记人ID不能为空");
                return false;
            }
            //登记日期
            if (tOaSwInfo.SW_REG_DATE.Trim() == "")
            {
                this.Tips.AppendLine("登记日期不能为空");
                return false;
            }
            //拟办人ID
            if (tOaSwInfo.SW_PLAN_ID.Trim() == "")
            {
                this.Tips.AppendLine("拟办人ID不能为空");
                return false;
            }
            //拟办日期
            if (tOaSwInfo.SW_PLAN_DATE.Trim() == "")
            {
                this.Tips.AppendLine("拟办日期不能为空");
                return false;
            }
            //批办人ID
            if (tOaSwInfo.SW_PLAN_APP_ID.Trim() == "")
            {
                this.Tips.AppendLine("批办人ID不能为空");
                return false;
            }
            //批办日期
            if (tOaSwInfo.SW_PLAN_APP_DATE.Trim() == "")
            {
                this.Tips.AppendLine("批办日期不能为空");
                return false;
            }
            //批办意见
            if (tOaSwInfo.SW_PLAN_APP_INFO.Trim() == "")
            {
                this.Tips.AppendLine("批办意见不能为空");
                return false;
            }
            //主办人ID
            if (tOaSwInfo.SW_APP_ID.Trim() == "")
            {
                this.Tips.AppendLine("主办人ID不能为空");
                return false;
            }
            //主办日期
            if (tOaSwInfo.SW_APP_DATE.Trim() == "")
            {
                this.Tips.AppendLine("主办日期不能为空");
                return false;
            }
            //主办意见
            if (tOaSwInfo.SW_APP_INFO.Trim() == "")
            {
                this.Tips.AppendLine("主办意见不能为空");
                return false;
            }
            //归档人ID
            if (tOaSwInfo.PIGONHOLE_ID.Trim() == "")
            {
                this.Tips.AppendLine("归档人ID不能为空");
                return false;
            }
            //归档时间
            if (tOaSwInfo.PIGONHOLE_DATE.Trim() == "")
            {
                this.Tips.AppendLine("归档时间不能为空");
                return false;
            }
            //办理状态
            if (tOaSwInfo.SW_STATUS.Trim() == "")
            {
                this.Tips.AppendLine("办理状态不能为空");
                return false;
            }
            //备注1
            if (tOaSwInfo.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tOaSwInfo.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaSwInfo.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tOaSwInfo.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tOaSwInfo.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }
            //主题词
            if (tOaSwInfo.SUBJECT_WORD.Trim() == "")
            {
                this.Tips.AppendLine("主题词不能为空");
                return false;
            }
            return true;
        }

        /// <summary>
        /// 获取当前用户需要办理的收文DataTable
        /// </summary>
        /// <param name="userID">当前用户ID</param>
        /// <param name="where">额外条件</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectHandleTable(string userID, string where, int iIndex, int iCount)
        {
            return access.SelectHandleTable(userID, where, iIndex, iCount);
        }
        /// <summary>
        /// 获取当前用户需要办理的收文个数（ljn,2013/11/28,郑州需求）
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public string GetSwResultCount(string UserID)
        {
            return access.GetSwResultCount(UserID);
        }

        /// <summary>
        /// 获取当前用户需要办理的收文DataTable
        /// </summary>
        /// <param name="userID">当前用户ID</param>
        /// <param name="where">额外条件</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectHandleTable_Mobile(string userID, string strConfirm, string where, int iIndex, int iCount)
        {
            return access.SelectHandleTable_Mobile(userID, strConfirm, where, iIndex, iCount);
        }

        /// <summary>
        /// 获取当前用户需要办理的收文DataTable的 Count
        /// </summary>
        /// <param name="userID">当前用户ID</param>
        /// <param name="where">额外条件</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public int SelectHandleTable_Count(string userID, string strConfirm, string where)
        {
            return access.SelectHandleTable_Count_Mobile(userID, strConfirm, where);
        }

        /// <summary>
        /// 获取收文单的详细信息
        /// </summary>
        /// <param name="strID"></param>
        /// <returns></returns>
        public DataTable GetSWDetails(string strID)
        {
            return access.GetSWDetails(strID);
        }

        /// <summary>
        /// 发送收文逻辑
        /// </summary>
        /// <param name="SWID">收文ID</param>
        /// <param name="SWStatus">收文新状态</param>
        /// <param name="SWPreStatus">收文前状态</param>
        /// <param name="LoginUser">当前登录人</param>
        /// <param name="Suggestion">批办意见</param>
        /// <param name="Handler">操作人</param>
        /// <param name="Reader">阅办人</param>
        /// <param name="Maker">办结人</param>
        /// <param name="Serial"></param>
        /// <returns></returns>
        public bool SendSW(string SWID, string SWStatus, string SWPreStatus, string LoginUser, string Suggestion, string Handler, string Reader, string Maker, string Serial)
        {
            return access.SendSW(SWID, SWStatus, SWPreStatus, LoginUser, Suggestion, Handler, Reader, Maker, Serial);
        }

        /// <summary>
        /// 完成（归档）收文
        /// </summary>
        /// <param name="SWID">收文ID</param>
        /// <param name="SWStatus">收文新状态</param>
        /// <param name="SWPreStatus">收文前状态</param>
        /// <param name="LoginUser">登录人ID</param>
        /// <returns></returns>
        public bool FinishSW(string SWID, string SWStatus, string SWPreStatus, string LoginUser)
        {
            return access.FinishSW(SWID, SWStatus, SWPreStatus, LoginUser);
        }
    }
}
