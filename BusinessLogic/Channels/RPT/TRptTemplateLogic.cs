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
    /// 功能：模板表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptTemplateLogic : LogicBase
    {

        TRptTemplateVo tRptTemplate = new TRptTemplateVo();
        TRptTemplateAccess access;

        public TRptTemplateLogic()
        {
            access = new TRptTemplateAccess();
        }

        public TRptTemplateLogic(TRptTemplateVo _tRptTemplate)
        {
            tRptTemplate = _tRptTemplate;
            access = new TRptTemplateAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptTemplate">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptTemplateVo tRptTemplate)
        {
            return access.GetSelectResultCount(tRptTemplate);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptTemplateVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptTemplate">对象条件</param>
        /// <returns>对象</returns>
        public TRptTemplateVo Details(TRptTemplateVo tRptTemplate)
        {
            return access.Details(tRptTemplate);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptTemplate">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptTemplateVo> SelectByObject(TRptTemplateVo tRptTemplate, int iIndex, int iCount)
        {
            return access.SelectByObject(tRptTemplate, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptTemplate">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptTemplateVo tRptTemplate, int iIndex, int iCount)
        {
            return access.SelectByTable(tRptTemplate, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptTemplate"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptTemplateVo tRptTemplate)
        {
            return access.SelectByTable(tRptTemplate);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptTemplate">对象</param>
        /// <returns></returns>
        public TRptTemplateVo SelectByObject(TRptTemplateVo tRptTemplate)
        {
            return access.SelectByObject(tRptTemplate);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptTemplateVo tRptTemplate)
        {
            return access.Create(tRptTemplate);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptTemplate">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptTemplateVo tRptTemplate)
        {
            return access.Edit(tRptTemplate);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptTemplate_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tRptTemplate_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptTemplateVo tRptTemplate_UpdateSet, TRptTemplateVo tRptTemplate_UpdateWhere)
        {
            return access.Edit(tRptTemplate_UpdateSet, tRptTemplate_UpdateWhere);
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
        public bool Delete(TRptTemplateVo tRptTemplate)
        {
            return access.Delete(tRptTemplate);
        }

        #region WebOffice
        /// <summary>
        /// 调取模板文件
        /// </summary>
        /// <param name="TemplateID">模板ID</param>
        /// <returns>模板文件流</returns>
        public byte[] LoadTemplate(string TemplateID)
        {
            return access.LoadTemplate(TemplateID);
        }

        /// <summary>
        /// 保存模板文件
        /// 1、如果存在，覆盖原模板；
        /// 2、如果不存在，插入该模板；
        /// </summary>
        /// <param name="template">模板对象</param>
        /// <returns>是否成功</returns>
        public bool SaveTemplate(TRptTemplateVo template)
        {
            return access.SaveTemplate(template);
        }
        #endregion

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tRptTemplate.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //文件名
            if (tRptTemplate.FILE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("文件名不能为空");
                return false;
            }
            //文件类型
            if (tRptTemplate.FILE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("文件类型不能为空");
                return false;
            }
            //文件大小
            if (tRptTemplate.FILE_SIZE.Trim() == "")
            {
                this.Tips.AppendLine("文件大小不能为空");
                return false;
            }
            //文件内容
            if (tRptTemplate.FILE_BODY == null)
            {
                this.Tips.AppendLine("文件内容不能为空");
                return false;
            }
            //文件路径
            if (tRptTemplate.FILE_PATH.Trim() == "")
            {
                this.Tips.AppendLine("文件路径不能为空");
                return false;
            }
            //文件描述
            if (tRptTemplate.FILE_DESC.Trim() == "")
            {
                this.Tips.AppendLine("文件描述不能为空");
                return false;
            }
            //添加日期
            if (tRptTemplate.ADD_TIME.Trim() == "")
            {
                this.Tips.AppendLine("添加日期不能为空");
                return false;
            }
            //添加人
            if (tRptTemplate.ADD_USER.Trim() == "")
            {
                this.Tips.AppendLine("添加人不能为空");
                return false;
            }
            //备注1
            if (tRptTemplate.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tRptTemplate.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tRptTemplate.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
