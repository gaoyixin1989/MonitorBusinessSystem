using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.WF.XML
{
    /// <summary>
    /// ����һ��ʽ
    /// </summary>
    public class RptXml : XmlEnNoName
    {
        public new string Name
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }
        public new string URL
        {
            get
            {
                return this.GetValStringByKey("URL");
            }
        }
        public new string ICON
        {
            get
            {
                return this.GetValStringByKey("ICON");
            }
        }

        #region ����
        /// <summary>
        /// �ڵ���չ��Ϣ
        /// </summary>
        public RptXml()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new RptXmls();
            }
        }
        #endregion
    }
    /// <summary>
    /// ����һ��ʽs
    /// </summary>
    public class RptXmls : XmlEns
    {
        #region ����
        /// <summary>
        /// ����һ��ʽs
        /// </summary>
        public RptXmls() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new RptXml();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfData + "\\Xml\\WFAdmin.xml";
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public override string TableName
        {
            get
            {
                return "RptFlow";
            }
        }
        public override Entities RefEns
        {
            get
            {
                return null;
            }
        }
        #endregion
    }
}
