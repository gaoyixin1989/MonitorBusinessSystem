using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;


namespace BP.Sys
{
    /// <summary>
    /// ��������
    /// </summary>
    public class FieldGroupXml : XmlEn
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
        public string Desc
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang+"Desc");
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �ڵ���չ��Ϣ
        /// </summary>
        public FieldGroupXml()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��s
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new FieldGroupXmls();
            }
        }
        #endregion
    }
    /// <summary>
    /// ��������s
    /// </summary>
    public class FieldGroupXmls : XmlEns
    {
        #region ����
        /// <summary>
        /// ��������s
        /// </summary>
        public FieldGroupXmls() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new FieldGroupXml();
            }
        }
        public override string File
        {
            get
            {
               // return SystemConfig.PathOfWebApp + "\\WF\\MapDef\\Style\\XmlDB.xml";
                                return SystemConfig.PathOfData + "\\XML\\XmlDB.xml";
                //\MapDef\\Style\
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public override string TableName
        {
            get
            {
                return "FieldGroup";
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
