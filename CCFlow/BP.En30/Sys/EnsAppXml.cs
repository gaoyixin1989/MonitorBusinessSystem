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
    public class EnsAppXmlEnsName
    {
        /// <summary>
        /// ������Ϊ
        /// </summary>
        public const string EnsName = "EnsName";
        /// <summary>
        /// ���ʽ
        /// </summary>
        public const string Desc = "Desc";
        /// <summary>
        /// ��������
        /// </summary>
        public const string DBType = "DBType";
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public const string DefVal = "DefVal";
        /// <summary>
        /// ֵ
        /// </summary>
        public const string EnumKey = "EnumKey";
        /// <summary>
        /// ֵ
        /// </summary>
        public const string EnumVals = "EnumVals";
    }
    /// <summary>
    /// EnsAppXml ��ժҪ˵�������Ե����á�
    /// </summary>
    public class EnsAppXml : XmlEnNoName
    {
        #region ����
        /// <summary>
        /// ö��ֵ
        /// </summary>
        public string EnumKey
        {
            get
            {
                return this.GetValStringByKey(EnsAppXmlEnsName.EnumKey);
            }
        }
        public string EnumVals
        {
            get
            {
                return this.GetValStringByKey(EnsAppXmlEnsName.EnumVals);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string EnsName
        {
            get
            {
                return this.GetValStringByKey(EnsAppXmlEnsName.EnsName);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string DBType
        {
            get
            {
                return this.GetValStringByKey(EnsAppXmlEnsName.DBType);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Desc
        {
            get
            {
                return this.GetValStringByKey(EnsAppXmlEnsName.Desc);
            }
        }
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefVal
        {
            get
            {
                return this.GetValStringByKey(EnsAppXmlEnsName.DefVal);
            }
        }
        public bool DefValBoolen
        {
            get
            {
                return this.GetValBoolByKey(EnsAppXmlEnsName.DefVal);
            }
        }
        public int DefValInt
        {
            get
            {
                return this.GetValIntByKey(EnsAppXmlEnsName.DefVal);
            }
        }
        #endregion

        #region ����
        public EnsAppXml()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new EnsAppXmls();
            }
        }
        #endregion
    }
    /// <summary>
    /// ���Լ���
    /// </summary>
    public class EnsAppXmls : XmlEns
    {
        #region ����
        /// <summary>
        /// ���˹�����Ϊ������Ԫ��
        /// </summary>
        public EnsAppXmls()
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
                return new EnsAppXml();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfXML + "\\Ens\\EnsAppXml\\GE.xml";
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public override string TableName
        {
            get
            {
                return "Item";
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
