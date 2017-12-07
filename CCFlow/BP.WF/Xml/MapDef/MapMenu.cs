using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.XML;

namespace BP.WF.XML
{
    /// <summary>
    /// ӳ��˵�
    /// </summary>
    public class MapMenu : XmlEn
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
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }
        public string JS
        {
            get
            {
                return this.GetValStringByKey("JS");
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
        /// <summary>
        /// ˵��
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey("Note");
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �ڵ���չ��Ϣ
        /// </summary>
        public MapMenu()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��s
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new MapMenus();
            }
        }
        #endregion
    }
    /// <summary>
    /// ӳ��˵�s
    /// </summary>
    public class MapMenus : XmlEns
    {
        #region ����
        /// <summary>
        /// ӳ��˵�s
        /// </summary>
        public MapMenus() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new MapMenu();
            }
        }
        public override string File
        {
            get
            {
               // return SystemConfig.PathOfWebApp + "\\WF\\MapDef\\Style\\XmlDB.xml";
                return SystemConfig.PathOfData + "\\XML\\XmlDB.xml";

            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public override string TableName
        {
            get
            {
                return "MapMenu";
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
