using System;
using System.Collections;
using BP.DA;
using BP.Sys;
using BP.En;
using BP.XML;

namespace BP.WF.XML
{
    /// <summary>
    /// �¼�Դ
    /// </summary>
	public class EventSource:XmlEn
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
		#endregion

		#region ����
		/// <summary>
        /// �¼�Դ
		/// </summary>
		public EventSource()
		{
		}
		/// <summary>
		/// ��ȡһ��ʵ��
		/// </summary>
		public override XmlEns GetNewEntities
		{
			get
			{
				return new EventSources();
			}
		}
		#endregion
	}
	/// <summary>
    /// �¼�Դs
	/// </summary>
    public class EventSources : XmlEns
    {
        #region ����
        /// <summary>
        /// �¼�Դs
        /// </summary>
        public EventSources() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new EventSource();
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
                return "Source";
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
