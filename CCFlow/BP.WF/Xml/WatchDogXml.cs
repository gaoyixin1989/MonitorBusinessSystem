using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.XML;
using BP.Sys;

namespace BP.WF.XML
{
    /// <summary>
    /// ���̼�ز˵�
    /// </summary>
    public class WatchDogXml : XmlEnNoName
    {
        public new string Name
        {
            get
            {
                return this.GetValStringByKey(BP.Web.WebUser.SysLang);
            }
        }

        #region ����
        /// <summary>
        /// ���̼�ز˵�
        /// </summary>
        public WatchDogXml()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new WatchDogXmls();
            }
        }
        #endregion
    }
    /// <summary>
    /// ���̼�ز˵�s
    /// </summary>
    public class WatchDogXmls : XmlEns
    {
        #region ����
        /// <summary>
        /// ���̼�ز˵�s
        /// </summary>
        public WatchDogXmls() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new WatchDogXml();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfWebApp + "\\WF\\Admin\\Sys\\Sys.xml";
            }
        }
        /// <summary>
        /// ��
        /// </summary>
        public override string TableName
        {
            get
            {
                return "WatchDog";
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
