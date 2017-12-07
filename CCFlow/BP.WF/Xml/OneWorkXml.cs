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
    public class OneWorkXml : XmlEnNoName
    {
        #region ����.
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
                return this.GetValStringByKey("No");
            }
        }
        #endregion ����.

        #region ����
        /// <summary>
        /// �ڵ���չ��Ϣ
        /// </summary>
        public OneWorkXml()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new OneWorkXmls();
            }
        }
        #endregion
    }
    /// <summary>
    /// ����һ��ʽs
    /// </summary>
    public class OneWorkXmls : XmlEns
    {
        #region ����
        /// <summary>
        /// ����һ��ʽs
        /// </summary>
        public OneWorkXmls() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new OneWorkXml();
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
                return "OneWork";
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
