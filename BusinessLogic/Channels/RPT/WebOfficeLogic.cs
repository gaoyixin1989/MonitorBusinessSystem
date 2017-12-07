using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.RPT;
using i3.BusinessLogic.Channels.RPT;
using System.Data;

namespace i3.BusinessLogic.Channels.RPT
{
    /// <summary>
    /// 功能描述：WebOffice业务逻辑层
    /// 创建日期：2008.10.6
    /// 创建人：  陈国迎
    /// </summary>
    public class WebOfficeLogic
    {
        #region 文件
        /// <summary>
        /// 调取文件
        /// </summary>
        /// <param name="FileID">文件ID</param>
        /// <returns>文件文件流</returns>
        public byte[] LoadFile(string FileID)
        {
            return new TRptFileLogic().LoadFile(FileID);
        }

        /// <summary>
        /// 保存文件
        /// 1、如果存在，覆盖原文件；
        /// 2、如果不存在，插入该文件；
        /// </summary>
        /// <param name="template">文件对象</param>
        /// <returns>是否成功</returns>
        public bool SaveFile(TRptFileVo file)
        {
            return new TRptFileLogic().SaveFile(file);
        }
        #endregion

        #region 模板
        /// <summary>
        /// 调取模板文件
        /// </summary>
        /// <param name="TemplateID">模板ID</param>
        /// <returns>模板文件流</returns>
        public byte[] LoadTemplate(string TemplateID)
        {
            return new TRptTemplateLogic().LoadTemplate(TemplateID);
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
            return new TRptTemplateLogic().SaveTemplate(template);
        }
        #endregion

        #region 标签
        /// <summary>
        /// 列出所有的标签
        /// </summary>
        /// <param name="MarkName">标签名称</param>
        /// <param name="MarkDesc">标签描述</param>
        /// <returns></returns>
        public void ListBookMarks(out string MarkName, out string MarkDesc)
        {
            new TRptMarkLogic().ListBookMarks(out MarkName, out MarkDesc);
        }

        /// <summary>
        /// 调取模板的标签
        /// </summary>
        /// <param name="TemplateID">模板ID</param>
        /// <returns>标签对象列表</returns>
        public DataTable LoadBookMarks(string TemplateID)
        {
            return new TRptMarkLogic().LoadBookMarks(TemplateID);
        }

        /// <summary>
        /// 保存模板标签
        /// </summary>
        /// <param name="MarkList">标签列表</param>
        /// <returns>是否成功</returns>
        public bool SaveBookMarks(string MarkNameList, string TemplateID)
        {
            return new TRptMarkLogic().SaveBookMarks(MarkNameList, TemplateID);
        }

        /// <summary>
        /// 通过标签属性获得标签名称
        /// </summary>
        /// <param name="AttributeName">标签属性</param>
        /// <returns>标签名称</returns>
        public string GetBookMarkNameByAttribute(string AttributeName)
        {
            return new TRptMarkLogic().GetBookMarkNameByAttribute(AttributeName);
        }
        #endregion

        #region 印章
        /// <summary>
        /// 调取所有的印章
        /// </summary>
        /// <param name="MarkList">印章列表</param>
        /// <returns>是否成功</returns>
        public bool LoadMarkList(out string MarkList)
        {
            return new TRptSignatureLogic().LoadMarkList(out MarkList);
        }

        /// <summary>
        /// 根据用户名和密码调取印章
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="Mark_Body">文件流</param>
        /// <param name="Mark_Type">文件类型</param>
        /// <returns>是否成功</returns>
        public bool LoadMarkImage(string UserName, string PassWord, out byte[] Mark_Body, out string Mark_Type)
        {
            return new TRptSignatureLogic().LoadMarkImage(UserName, PassWord, out Mark_Body, out Mark_Type);
        }
        /// <summary>
        /// 保存印章
        /// </summary>
        /// <param name="signature">印章对象</param>
        /// <returns>是否成功</returns>
        public bool SaveSignature(TRptFileSignatureVo signature)
        {
            return new TRptFileSignatureLogic().SaveSignature(signature);
        }

        /// <summary>
        /// 调取指定文档的所有印章
        /// </summary>
        /// <param name="FileID">文件ID</param>
        /// <returns>印章对象</returns>
        public TRptFileSignatureVo LoadSignature(string FileID)
        {
            return new TRptFileSignatureLogic().LoadSignature(FileID);
        }

        /// <summary>
        /// 创建表格
        /// </summary>
        /// <returns>表格</returns>
        //public DataTable CreateTable()
        //{
        //    return (new ReportBuildSystem()).GetTestResult("000000022");
        //}
        #endregion
    }
}
