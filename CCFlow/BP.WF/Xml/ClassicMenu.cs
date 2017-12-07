using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.WF.XML
{
    /// <summary>
    /// ����ģʽ���˵�
    /// </summary>
    public class ClassicMenu : XmlEn
    {
        #region ����
        public string No
        {
            get
            {
                return this.GetValStringByKey("No");
            }
        }
        public string Name
        {
            get
            {
                switch (this.No)
                {
                    case "EmpWorks":
                        return this.GetValStringByKey(BP.Web.WebUser.SysLang)+"("+BP.WF.Dev2Interface.Todolist_EmpWorks+")";
                    case "Sharing":
                        return this.GetValStringByKey(BP.Web.WebUser.SysLang) + "(" + BP.WF.Dev2Interface.Todolist_Sharing + ")";
                    case "CC":
                        return this.GetValStringByKey(BP.Web.WebUser.SysLang) + "(" + BP.WF.Dev2Interface.Todolist_CCWorks + ")";
                    default:
                        return this.GetValStringByKey(BP.Web.WebUser.SysLang);
                }
            }
        }
        /// <summary>
        /// ͼƬ
        /// </summary>
        public string Img
        {
            get
            {
                return this.GetValStringByKey("Img");
            }
        }
        public string Title
        {
            get
            {
                return this.GetValStringByKey("Title");
            }
        }
        public string Url
        {
            get
            {
                return this.GetValStringByKey("Url");
            }
        }
        public bool Enable
        {
            get
            {
                return this.GetValBoolByKey("Enable");
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �ڵ���չ��Ϣ
        /// </summary>
        public ClassicMenu()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new ClassicMenus();
            }
        }
        #endregion
    }
    /// <summary>
    /// ����ģʽ���˵�s
    /// </summary>
    public class ClassicMenus : XmlEns
    {
        #region ����
        /// <summary>
        /// �����ʵ�����Ԫ��
        /// </summary>
        public ClassicMenus() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new ClassicMenu();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfWebApp + "\\DataUser\\XML\\LeftEnum.xml";
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public override string TableName
        {
            get
            {
                return "ClassicMenu";
            }
        }
        public override Entities RefEns
        {
            get
            {
                return null; //new BP.ZF1.AdminTools();
            }
        }
        #endregion
    }
}
