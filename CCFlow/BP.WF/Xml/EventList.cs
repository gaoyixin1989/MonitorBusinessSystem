using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.WF.XML
{
    /// <summary>
    /// �¼�
    /// </summary>
    public class EventList : XmlEn
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
        /// ��չ����
        /// </summary>
        public string NameHtml
        {
            get
            {
                if (this.IsHaveMsg)
                    return "<img src='../Img/Message24.png' border=0 width='17px'/>" + this.GetValStringByKey(BP.Web.WebUser.SysLang);
                else
                    return  this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string EventDesc
        {
            get
            {
                return this.GetValStringByKey("EventDesc");
            }
        }
        /// <summary>
        /// �¼�����
        /// </summary>
        public string EventType
        {
            get
            {
                return this.GetValStringByKey("EventType");
            }
        }
        /// <summary>
        /// �Ƿ�����Ϣ
        /// </summary>
        public bool IsHaveMsg
        {
            get
            {
                return this.GetValBoolByKey("IsHaveMsg");
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �¼�
        /// </summary>
        public EventList()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new EventLists();
            }
        }
        #endregion
    }
    /// <summary>
    /// �¼�s
    /// </summary>
    public class EventLists : XmlEns
    {
        #region ����
        /// <summary>
        /// �¼�s
        /// </summary>
        public EventLists() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new EventList();
            }
        }
        /// <summary>
        /// ���·��
        /// </summary>
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
