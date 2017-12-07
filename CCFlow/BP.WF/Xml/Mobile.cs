using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.XML;

namespace BP.WF.XML
{
    /// <summary>
    /// �ƶ��˵�
    /// </summary>
    public class Mobile : XmlEn
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
                return this.GetValStringByKey("Name");
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �ڵ���չ��Ϣ
        /// </summary>
        public Mobile()
        {
        }
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new Mobiles();
            }
        }
        #endregion
    }
    /// <summary>
    /// 
    /// </summary>
    public class Mobiles : XmlEns
    {
        #region ����
        /// <summary>
        /// �����ʵ�����Ԫ��
        /// </summary>
        public Mobiles() { }
        #endregion

        #region ��д�������Ի򷽷���
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override XmlEn GetNewEntity
        {
            get
            {
                return new Mobile();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.CCFlowAppPath + "DataUser\\XML\\Mobiles.xml";
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
