using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Sys;
using BP.XML;

namespace BP.Web.Controls
{
	/// <summary>
    /// ȡֵ����
	/// </summary>
    public class FrmPopValAttr
    {
        /// <summary>
        /// ���  
        /// </summary>    
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ��ǩ1
        /// </summary>
        public const string Tag1 = "Tag1";
        /// <summary>
        /// ��ǩ2
        /// </summary>
        public const string Tag2 = "Tag2";
        /// <summary>
        /// ����
        /// </summary>
        public const string AtPara = "AtPara";
        public const string H = "H";
        public const string W = "W";
    }
	/// <summary>
	/// ȡֵ
	/// </summary>
    public class FrmPopVal : XmlEnNoName
    {
        #region ����
        public string AtPara
        {
            get
            {
                return this.GetValStringByKey(FrmPopValAttr.AtPara);
            }
        }
        public string Tag1
        {
            get
            {
                return this.GetValStringByKey(FrmPopValAttr.Tag1);
            }
        }
        public string Tag2
        {
            get
            {
                return this.GetValStringByKey(FrmPopValAttr.Tag2);
            }
        }
        public string H
        {
            get
            {
                return this.GetValStringByKey(FrmPopValAttr.H);
            }
        }
        public string W
        {
            get
            {
                return this.GetValStringByKey(FrmPopValAttr.W);
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ȡֵ
        /// </summary>
        public FrmPopVal()
        {

        }
        /// <summary>
        /// ȡֵ
        /// </summary>
        /// <param name="no">���</param>
        public FrmPopVal(string no)
        {
            this.RetrieveByPK(FrmPopValAttr.No, no);
        }
        
        /// <summary>
        /// ��ȡһ��ʵ��
        /// </summary>
        public override XmlEns GetNewEntities
        {
            get
            {
                return new FrmPopVals();
            }
        }
        #endregion
    }
	/// <summary>
    /// ȡֵs
	/// </summary>
    public class FrmPopVals : XmlMenus
    {
        #region ����
        /// <summary>
        /// ȡֵs
        /// </summary>
        public FrmPopVals()
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
                return new FrmPopVal();
            }
        }
        public override string File
        {
            get
            {
                return SystemConfig.PathOfDataUser + "\\Xml\\FrmPopVal.xml";
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
