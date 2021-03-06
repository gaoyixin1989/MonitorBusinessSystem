using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.Notice;
using i3.DataAccess.Channels.OA.Notice;

namespace i3.BusinessLogic.Channels.OA.Notice
{
    /// <summary>
    /// 功能：公告管理
    /// 创建日期：2013-02-23
    /// 创建人：熊卫华
    /// </summary>
    public class TOaNoticeLogic : LogicBase
    {

        TOaNoticeVo tOaNotice = new TOaNoticeVo();
        TOaNoticeAccess access;

        public TOaNoticeLogic()
        {
            access = new TOaNoticeAccess();
        }

        public TOaNoticeLogic(TOaNoticeVo _tOaNotice)
        {
            tOaNotice = _tOaNotice;
            access = new TOaNoticeAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaNotice">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaNoticeVo tOaNotice)
        {
            return access.GetSelectResultCount(tOaNotice);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaNoticeVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaNotice">对象条件</param>
        /// <returns>对象</returns>
        public TOaNoticeVo Details(TOaNoticeVo tOaNotice)
        {
            return access.Details(tOaNotice);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaNotice">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaNoticeVo> SelectByObject(TOaNoticeVo tOaNotice, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaNotice, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaNotice">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaNoticeVo tOaNotice, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaNotice, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaNotice"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaNoticeVo tOaNotice)
        {
            return access.SelectByTable(tOaNotice);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaNotice">对象</param>
        /// <returns></returns>
        public TOaNoticeVo SelectByObject(TOaNoticeVo tOaNotice)
        {
            return access.SelectByObject(tOaNotice);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaNoticeVo tOaNotice)
        {
            return access.Create(tOaNotice);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaNotice">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaNoticeVo tOaNotice)
        {
            return access.Edit(tOaNotice);
        }
        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaNotice_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaNotice_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaNoticeVo tOaNotice_UpdateSet, TOaNoticeVo tOaNotice_UpdateWhere)
        {
            return access.Edit(tOaNotice_UpdateSet, tOaNotice_UpdateWhere);
        }

        /// <summary>
        /// 置顶前修改所有非置顶状态 黄进军20141112
        /// </summary>
        /// <param name="tOaNotice">用户对象</param>
        /// <returns>是否成功</returns>
        public bool EditAll()
        {
            return access.EditAll();
        }

        /// <summary>
        /// 置顶修改置顶状态 黄进军20141112
        /// </summary>
        /// <param name="tOaNotice">用户对象</param>
        /// <returns>是否成功</returns>
        public bool EditSetTopOne(string id)
        {
            return access.EditSetTopOne(id);
        }

        /// <summary>
        /// 按显示序号获取最早十条数据
        /// </summary>
        /// <returns></returns>
        public DataTable getTopTenData()
        {
            return access.getTopTenData();
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
        public bool Delete(TOaNoticeVo tOaNotice)
        {
            return access.Delete(tOaNotice);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tOaNotice.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //公告标题
            if (tOaNotice.TITLE.Trim() == "")
            {
                this.Tips.AppendLine("公告标题不能为空");
                return false;
            }
            //公告内容
            if (tOaNotice.CONTENT.Trim() == "")
            {
                this.Tips.AppendLine("公告内容不能为空");
                return false;
            }
            //公告类别
            if (tOaNotice.NOTICE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("公告类别不能为空");
                return false;
            }
            //发布时间
            if (tOaNotice.RELEASE_TIME.Trim() == "")
            {
                this.Tips.AppendLine("发布时间不能为空");
                return false;
            }
            //发布人
            if (tOaNotice.RELIEASER.Trim() == "")
            {
                this.Tips.AppendLine("发布人不能为空");
                return false;
            }
            //发布方式
            if (tOaNotice.RELIEASER_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("发布方式不能为空");
                return false;
            }
            //显示顺序
            if (tOaNotice.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("显示序号不能为空");
                return false;
            }
            //备注2
            if (tOaNotice.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaNotice.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tOaNotice.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tOaNotice.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
