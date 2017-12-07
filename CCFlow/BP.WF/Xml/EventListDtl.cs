using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.XML;

namespace BP.WF.XML
{
    /// <summary>
    /// �¼��б�
    /// </summary>
    public class EventListDtlList
    {
        /// <summary>
        /// ����ǰ
        /// </summary>
        public const string DtlSaveBefore = "DtlSaveBefore";
        /// <summary>
        /// �����
        /// </summary>
        public const string DtlSaveEnd = "DtlSaveEnd";
        /// <summary>
        /// ��¼����ǰ
        /// </summary>
        public const string DtlItemSaveBefore = "DtlItemSaveBefore";
        /// <summary>
        /// ��¼�����
        /// </summary>
        public const string DtlItemSaveAfter = "DtlItemSaveAfter";
        /// <summary>
        /// ��¼ɾ��ǰ
        /// </summary>
        public const string DtlItemDelBefore = "DtlItemDelBefore";
        /// <summary>
        /// ��¼ɾ����
        /// </summary>
        public const string DtlItemDelAfter = "DtlItemDelAfter";
    }
    /// <summary>
    /// �ӱ��¼�
    /// </summary>
    public class EventListDtl : XmlEn
    {
        #region ����
        /// <summary>
        /// ���
        /// </summary>
        public string No
        {
            get
            {
                return this.GetValStringByKey("No");
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string EventDesc
        {
            get
            {
                return this.GetValStringByKey("EventDesc");
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �ӱ��¼�
        /// </summary>
        public EventListDtl()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new EventListDtls();
            }
        }
        #endregion
    }
    /// <summary>
    /// �ӱ��¼�s
    /// </summary>
    public class EventListDtls : XmlEns
    {
        #region ����
        /// <summary>
        /// �ӱ��¼�s
        /// </summary>
        public EventListDtls() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new EventListDtl();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfXML + "\\EventList.xml";
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public override string TableName
        {
            get
            {
                return "ItemDtl";
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
