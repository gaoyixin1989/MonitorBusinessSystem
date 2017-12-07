using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.XML;


namespace BP.Sys.Xml
{
    /// <summary>
    /// ����
    /// </summary>
    public class EnsAppGroupXmlEnsName
    {
        /// <summary>
        /// ������Ϊ
        /// </summary>
        public const string EnsName = "EnsName";
        /// <summary>
        /// ���ʽ
        /// </summary>
        public const string GroupKey = "GroupKey";
        /// <summary>
        /// ��������
        /// </summary>
        public const string GroupName = "GroupName";
    }
    /// <summary>
    /// EnsAppGroupXml ��ժҪ˵�������Ե����á�
    /// </summary>
    public class EnsAppGroupXml : XmlEnNoName
    {
        #region ����
        /// <summary>
        /// ����
        /// </summary>
        public string EnsName
        {
            get
            {
                return this.GetValStringByKey(EnsAppGroupXmlEnsName.EnsName);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string GroupName
        {
            get
            {
                return this.GetValStringByKey(EnsAppGroupXmlEnsName.GroupName);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string GroupKey
        {
            get
            {
                return this.GetValStringByKey(EnsAppGroupXmlEnsName.GroupKey);
            }
        }
        #endregion

        #region ����
        public EnsAppGroupXml()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new EnsAppGroupXmls();
            }
        }
        #endregion
    }
    /// <summary>
    /// ���Լ���
    /// </summary>
    public class EnsAppGroupXmls : XmlEns
    {
        #region ����
        /// <summary>
        /// ���˹�����Ϊ������Ԫ��
        /// </summary>
        public EnsAppGroupXmls()
        {
        }
      
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new EnsAppGroupXml();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfXML + "\\Ens\\EnsAppXml\\";
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public override string TableName
        {
            get
            {
                return "Group";
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
