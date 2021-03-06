using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.RPT;
using i3.DataAccess.Channels.RPT;

namespace i3.BusinessLogic.Channels.RPT
{
    /// <summary>
    /// 功能：标签表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptMarkLogic : LogicBase
    {

        TRptMarkVo tRptMark = new TRptMarkVo();
        TRptMarkAccess access;

        public TRptMarkLogic()
        {
            access = new TRptMarkAccess();
        }

        public TRptMarkLogic(TRptMarkVo _tRptMark)
        {
            tRptMark = _tRptMark;
            access = new TRptMarkAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptMark">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptMarkVo tRptMark)
        {
            return access.GetSelectResultCount(tRptMark);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptMarkVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptMark">对象条件</param>
        /// <returns>对象</returns>
        public TRptMarkVo Details(TRptMarkVo tRptMark)
        {
            return access.Details(tRptMark);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptMark">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptMarkVo> SelectByObject(TRptMarkVo tRptMark, int iIndex, int iCount)
        {
            return access.SelectByObject(tRptMark, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptMark">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptMarkVo tRptMark, int iIndex, int iCount)
        {
            return access.SelectByTable(tRptMark, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptMark"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptMarkVo tRptMark)
        {
            return access.SelectByTable(tRptMark);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptMark">对象</param>
        /// <returns></returns>
        public TRptMarkVo SelectByObject(TRptMarkVo tRptMark)
        {
            return access.SelectByObject(tRptMark);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptMarkVo tRptMark)
        {
            return access.Create(tRptMark);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptMark">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptMarkVo tRptMark)
        {
            return access.Edit(tRptMark);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptMark_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tRptMark_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptMarkVo tRptMark_UpdateSet, TRptMarkVo tRptMark_UpdateWhere)
        {
            return access.Edit(tRptMark_UpdateSet, tRptMark_UpdateWhere);
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
        public bool Delete(TRptMarkVo tRptMark)
        {
            return access.Delete(tRptMark);
        }

        #region WebOffice
        /// <summary>
        /// 列出所有的标签
        /// </summary>
        /// <param name="MarkName">标签名称</param>
        /// <param name="MarkDesc">标签描述</param>
        /// <returns></returns>
        public void ListBookMarks(out string MarkName, out string MarkDesc)
        {
            access.ListBookMarks(out MarkName, out MarkDesc);
        }

        /// <summary>
        /// 调取模板的标签
        /// </summary>
        /// <param name="TemplateID">模板ID</param>
        /// <returns>标签对象列表</returns>
        public DataTable LoadBookMarks(string TemplateID)
        {
            return access.LoadBookMarks(TemplateID);
        }

        /// <summary>
        /// 保存模板标签
        /// </summary>
        /// <param name="MarkList">标签列表</param>
        /// <returns>是否成功</returns>
        public bool SaveBookMarks(string MarkNameList, string TemplateID)
        {
            return access.SaveBookMarks(MarkNameList, TemplateID);
        }

        /// <summary>
        /// 通过标签属性获得标签名称
        /// </summary>
        /// <param name="AttributeName">标签属性</param>
        /// <returns>标签名称</returns>
        public string GetBookMarkNameByAttribute(string AttributeName)
        {
            TRptMarkVo mark = new TRptMarkVo();
            mark.ATTRIBUTE_NAME = AttributeName;

            List<TRptMarkVo> ListTemp = access.SelectByObject(mark, 0, 0);

            //判断是否非空
            if (null != ListTemp && ListTemp.Count > 0)
            {
                return ListTemp[0].MARK_NAME;
            }
            else
            {
                return "";
            }
        }
        #endregion


        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tRptMark.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //标签名称
            if (tRptMark.MARK_NAME.Trim() == "")
            {
                this.Tips.AppendLine("标签名称不能为空");
                return false;
            }
            //标签描述
            if (tRptMark.MARK_DESC.Trim() == "")
            {
                this.Tips.AppendLine("标签描述不能为空");
                return false;
            }
            //标签说明
            if (tRptMark.MARK_REMARK.Trim() == "")
            {
                this.Tips.AppendLine("标签说明不能为空");
                return false;
            }
            //属性名称
            if (tRptMark.ATTRIBUTE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("属性名称不能为空");
                return false;
            }
            //备注
            if (tRptMark.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("备注不能为空");
                return false;
            }

            return true;
        }

    }
}
