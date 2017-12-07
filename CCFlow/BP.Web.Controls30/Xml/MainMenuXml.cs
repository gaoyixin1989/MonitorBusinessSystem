using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.XML;

namespace BP.GE
{
	/// <summary>
    /// ���˵�����
	/// </summary>
    public class MainMenuXmlAttr
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
	/// ���˵�
	/// </summary>
    public class MainMenuXml : XmlMenu
    {
        #region ����
        
        #endregion

        #region ����
        /// <summary>
        /// ���˵�
        /// </summary>
        public MainMenuXml()
        {
        }
        /// <summary>
        /// ���˵�
        /// </summary>
        /// <param name="no">���</param>
        public MainMenuXml(string no)
        {
            this.RetrieveByPK(MainMenuXmlAttr.No, no);
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new MainMenuXmls();
            }
        }
        #endregion
    }
	/// <summary>
    /// ���˵�s
	/// </summary>
    public class MainMenuXmls : XmlMenus
    {
        #region ����
        /// <summary>
        /// ���˵�s
        /// </summary>
        public MainMenuXmls() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new MainMenuXml();
            }
        }
        public override string File
        {
            get
            {
               return SystemConfig.PathOfXML + "\\MainMenu.xml";
               // return SystemConfig.PathOfXML + "\\Language\\";
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
