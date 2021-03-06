using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.ARCHIVES;
using i3.DataAccess.Channels.OA.ARCHIVES;

namespace i3.BusinessLogic.Channels.OA.ARCHIVES
{
    /// <summary>
    /// 功能：目录文件管理
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaArchivesDocumentLogic : LogicBase
    {

        TOaArchivesDocumentVo tOaArchivesDocument = new TOaArchivesDocumentVo();
        TOaArchivesDocumentAccess access;

        public TOaArchivesDocumentLogic()
        {
            access = new TOaArchivesDocumentAccess();
        }

        public TOaArchivesDocumentLogic(TOaArchivesDocumentVo _tOaArchivesDocument)
        {
            tOaArchivesDocument = _tOaArchivesDocument;
            access = new TOaArchivesDocumentAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaArchivesDocument">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            return access.GetSelectResultCount(tOaArchivesDocument);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaArchivesDocumentVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaArchivesDocument">对象条件</param>
        /// <returns>对象</returns>
        public TOaArchivesDocumentVo Details(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            return access.Details(tOaArchivesDocument);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaArchivesDocument">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaArchivesDocumentVo> SelectByObject(TOaArchivesDocumentVo tOaArchivesDocument, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaArchivesDocument, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaArchivesDocument">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaArchivesDocumentVo tOaArchivesDocument, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaArchivesDocument, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaArchivesDocument"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            return access.SelectByTable(tOaArchivesDocument);
        }

        /// <summary>
        /// 表单查询
        /// </summary>
        /// <param name="tOaArchivesDocument">用户对象</param>
        /// <returns></returns>
        public int SelectByTableForSearchCount(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            return access.SelectByTableForSearchCount(tOaArchivesDocument);
        }

        public int SelectTableForSearchCount(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            return access.SelectTableForSearchCount(tOaArchivesDocument);
        }
        

        /// <summary>
        /// 表单查询
        /// </summary>
        /// <param name="tOaArchivesDocument">用户对象</param>
        /// <returns></returns>
        public DataTable SelectByTableForSearch(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            return access.SelectByTableForSearch(tOaArchivesDocument);
        }

        /// <summary>
        /// 表单查询
        /// </summary>
        /// <param name="tOaArchivesDocument">用户对象</param>
        /// <returns></returns>
        public DataTable SelectByTableForSearch(TOaArchivesDocumentVo tOaArchivesDocument, int intPageIndex, int intPageSize)
        {
            return access.SelectByTableForSearch(tOaArchivesDocument, intPageIndex, intPageSize);
        }

        public DataTable SelectTableForSearch(TOaArchivesDocumentVo tOaArchivesDocument, int intPageIndex, int intPageSize)
        {
            return access.SelectTableForSearch(tOaArchivesDocument, intPageIndex, intPageSize);
        }
        

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaArchivesDocument">对象</param>
        /// <returns></returns>
        public TOaArchivesDocumentVo SelectByObject(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            return access.SelectByObject(tOaArchivesDocument);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            return access.Create(tOaArchivesDocument);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesDocument">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            return access.Edit(tOaArchivesDocument);
        }

        public bool update(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            return access.update(tOaArchivesDocument);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaArchivesDocument_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaArchivesDocument_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaArchivesDocumentVo tOaArchivesDocument_UpdateSet, TOaArchivesDocumentVo tOaArchivesDocument_UpdateWhere)
        {
            return access.Edit(tOaArchivesDocument_UpdateSet, tOaArchivesDocument_UpdateWhere);
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
        public bool Delete(TOaArchivesDocumentVo tOaArchivesDocument)
        {
            return access.Delete(tOaArchivesDocument);
        }

        #region 废止&销毁
        /// <summary>
        /// 废止文档历史查询
        /// </summary>
        /// <param name="tOaArchivesDocument">对象</param>
        /// <param name="intPageIndex">页码</param>
        /// <param name="intPageSize">页数</param>
        /// <returns></returns>
        public DataTable SelectByTableForDelete(TOaArchivesDocumentVo tOaArchivesDocument, int intPageIndex, int intPageSize)
        {
            return access.SelectByTableForDelete(tOaArchivesDocument, intPageIndex, intPageSize);
        }
        #endregion

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键
            if (tOaArchivesDocument.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键不能为空");
                return false;
            }
            //目录ID
            if (tOaArchivesDocument.DIRECTORY_ID.Trim() == "")
            {
                this.Tips.AppendLine("目录ID不能为空");
                return false;
            }
            //文件名称
            if (tOaArchivesDocument.DOCUMENT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("文件名称不能为空");
                return false;
            }
            //文件类型
            if (tOaArchivesDocument.DOCUMENT_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("文件类型不能为空");
                return false;
            }
            //条形码
            if (tOaArchivesDocument.BAR_CODE.Trim() == "")
            {
                this.Tips.AppendLine("条形码不能为空");
                return false;
            }
            //文件大小
            if (tOaArchivesDocument.DOCUMENT_SIZE.Trim() == "")
            {
                this.Tips.AppendLine("文件大小不能为空");
                return false;
            }
            //上传日期
            if (tOaArchivesDocument.UPLOADING_DATE.Trim() == "")
            {
                this.Tips.AppendLine("上传日期不能为空");
                return false;
            }
            //存放位置
            if (tOaArchivesDocument.DOCUMENT_LOCATION.Trim() == "")
            {
                this.Tips.AppendLine("存放位置不能为空");
                return false;
            }
            //文件路径
            if (tOaArchivesDocument.DOCUMENT_PATH.Trim() == "")
            {
                this.Tips.AppendLine("文件路径不能为空");
                return false;
            }
            //文件描述
            if (tOaArchivesDocument.DOCUMENT_DESCRIPTION.Trim() == "")
            {
                this.Tips.AppendLine("文件描述不能为空");
                return false;
            }
            //备注1
            if (tOaArchivesDocument.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tOaArchivesDocument.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tOaArchivesDocument.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tOaArchivesDocument.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tOaArchivesDocument.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
