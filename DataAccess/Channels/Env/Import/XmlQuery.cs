using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace i3.DataAccess.Channels.Env.Import
{
    /// <summary>
    /// XML处理类 Created by 熊卫华 2013年6月20日
    /// </summary>
    public class XmlQuery
    {
        /// <summary>
        /// 根据Xml文档加载
        /// </summary>
        /// <param name="strXmlUrl">xml模板路径</param>
        /// <returns></returns>
        public XmlDocument createXmlDocument(string strXmlUrl)
        {
            XmlDocument document = new XmlDocument();
            document.Load(strXmlUrl);
            return document;
        }
        /// <summary>
        /// 根据节点名称获取首节点元素信息
        /// </summary>
        /// <param name="document">xml文档对象</param>
        /// <param name="strRootNodeName">xml节点名称</param>
        /// <returns></returns>
        public XmlElement getRootElementInfo(XmlDocument document, string strRootNodeName)
        {
            XmlElement xe = null;
            XmlNodeList nodeList = document.SelectSingleNode("GlobalSettings").ChildNodes;
            if (nodeList.Count == 0)
                return xe;

            foreach (XmlNode xn in nodeList)
            {
                if (xn.NodeType == XmlNodeType.Element)
                {
                    xe = (XmlElement)xn;
                    if (xe.Name == strRootNodeName)
                        break;
                }
            }
            return xe;
        }
        /// <summary>
        /// 根据元素名称搜素子元素信息
        /// </summary>
        /// <param name="xmlElement">父元素对象</param>
        /// <param name="strElementName">子元素名称</param>
        /// <returns></returns>
        public List<XmlElement> xmlElementSearch(XmlElement xmlElement, string strElementName)
        {
            List<XmlElement> list = new List<XmlElement>();
            if (xmlElement.HasChildNodes == false)
                return list;

            XmlNodeList nodeList = xmlElement.ChildNodes;
            foreach (XmlNode xn in nodeList)
            {
                if (xn.NodeType == XmlNodeType.Element)
                {
                    if (xn.Name == strElementName)
                    {
                        list.Add(xn as XmlElement);
                    }
                    else
                    {
                        list.AddRange(xmlElementSearch(xn as XmlElement, strElementName));
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取xml配置文件元素属性
        /// </summary>
        /// <param name="xmlElement">元素信息</param>
        /// <param name="strAttributeName">属性名称</param>
        /// <returns></returns>
        public string getElementAttribute(XmlElement xmlElement, string strAttributeName)
        {
            return xmlElement.GetAttribute(strAttributeName);
        }
    }
}