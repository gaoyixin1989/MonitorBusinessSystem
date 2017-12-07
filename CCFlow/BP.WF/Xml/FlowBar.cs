using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.WF.XML
{
    /// <summary>
    /// ���̰�ť
    /// </summary>
    public class FlowBar : XmlEn
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
        #endregion

        #region ����
        /// <summary>
        /// �ڵ���չ��Ϣ
        /// </summary>
        public FlowBar()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new FlowBars();
            }
        }
        #endregion
    }
    /// <summary>
    /// ���̰�ťs
    /// </summary>
    public class FlowBars : XmlEns
    {
        #region ����
        /// <summary>
        /// �����ʵ�����Ԫ��
        /// </summary>
        public FlowBars() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new FlowBar();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfWebApp + "\\DataUser\\XML\\BarOfTop.xml";
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public override string TableName
        {
            get
            {
                return "FlowBar";
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
