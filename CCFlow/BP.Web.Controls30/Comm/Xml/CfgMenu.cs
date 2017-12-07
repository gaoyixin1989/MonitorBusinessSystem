using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;


namespace BP.Web.Comm
{
    public class CfgMenuAttr
    {
        /// <summary>
        /// ���
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        public const string HelpFile = "HelpFile";
        public const string Img = "Img";
    }
    /// <summary>
    /// ���ò˵�
    /// </summary>
    public class CfgMenu : XmlMenu
    {
        #region ����
        public CfgMenu()
        {
        }
        /// <summary>
        /// ���
        /// </summary>
        /// <param name="no"></param>
        public CfgMenu(string no)
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new CfgMenus();
            }
        }
        #endregion
    }
    /// <summary>
    /// ���ò˵�s
    /// </summary>
    public class CfgMenus : XmlMenus
    {
        #region ����
        /// <summary>
        /// ���ò˵�
        /// </summary>
        public CfgMenus()
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
                return new CfgMenu();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfWebApp + "\\WF\\Comm\\Sys\\CfgMenu.xml";
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
                return null; //new BP.ZF1.Helps();
            }
        }
        #endregion
    }
}
