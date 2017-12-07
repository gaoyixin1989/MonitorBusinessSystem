using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.Sys;
using BP.En;
using BP.XML;

namespace BP.Web
{
	/// <summary>
    /// ����˵�����
	/// </summary>
    public class GroupXmlAttr
    {
        /// <summary>
        /// ���  
        /// </summary>    
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
    }
	/// <summary>
	/// ����˵�
	/// </summary>
    public class GroupXml : XmlEnNoName
    {
        #region ����
        #endregion

        #region ����
        /// <summary>
        /// ����˵�
        /// </summary>
        public GroupXml()
        {

        }
        /// <summary>
        /// ����˵�
        /// </summary>
        /// <param name="no">���</param>
        public GroupXml(string no)
        {
            this.RetrieveByPK(GroupXmlAttr.No, no);
        }
        
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new GroupXmls();
            }
        }
        #endregion
    }
	/// <summary>
    /// ����˵�s
	/// </summary>
    public class GroupXmls : XmlMenus
    {
        #region ����
        /// <summary>
        /// ����˵�s
        /// </summary>
        public GroupXmls()
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
                return new GroupXml();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfXML + "\\Ens\\Group.xml";
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
