using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP;
namespace BP.Sys
{
	/// <summary>
	///  Ӧ������
	/// </summary>
    public class EnsAppCfgAttr : EntityNoAttr
    {
        /// <summary>
        /// �����ǩ
        /// </summary>
        public const string EnsName = "EnsName";
        /// <summary>
        /// ���ü�ֵ
        /// </summary>
        public const string CfgKey = "CfgKey";
        /// <summary>
        /// ֵ
        /// </summary>
        public const string CfgVal = "CfgVal";
    }
	/// <summary>
    /// Ӧ������
	/// </summary>
    public class EnsAppCfg : EntityMyPK
    {
        #region ��������
        /// <summary>
        /// ���ñ�ǩ
        /// </summary>
        public string CfgVal
        {
            get
            {
                string val= this.GetValStrByKey(EnsAppCfgAttr.CfgVal);
                if (val == null || val == "")
                    return null;
                return val;
            }
            set
            {
                this.SetValByKey(EnsAppCfgAttr.CfgVal, value);
            }
        }
        /// <summary>
        /// Int ֵ
        /// </summary>
        public int CfgValOfInt
        {
            get
            {
                try
                {
                    return this.GetValIntByKey(EnsAppCfgAttr.CfgVal);
                }
                catch
                {
                    return 0;
                }
            }
        }
        /// <summary>
        /// Boolen ֵ
        /// </summary>
        public bool CfgValOfBoolen
        {
            get
            {
                try
                {
                    return this.GetValBooleanByKey(EnsAppCfgAttr.CfgVal);
                }
                catch
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// ����Դ
        /// </summary>
        public string EnsName
        {
            get
            {
                return this.GetValStringByKey(EnsAppCfgAttr.EnsName);
            }
            set
            {
                this.SetValByKey(EnsAppCfgAttr.EnsName, value);
            }
        }
        /// <summary>
        /// ����·��
        /// </summary>
        public string CfgKey
        {
            get
            {
                return this.GetValStringByKey(EnsAppCfgAttr.CfgKey);
            }
            set
            {
                this.SetValByKey(EnsAppCfgAttr.CfgKey, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ϵͳʵ��
        /// </summary>
        public EnsAppCfg()
        {
        }
        /// <summary>
        /// ϵͳʵ��
        /// </summary>
        /// <param name="no"></param>
        public EnsAppCfg(string pk)
        {
            this.MyPK = pk;
            int i = this.RetrieveFromDBSources();
            if (i == 0)
            {
                //BP.Sys.Xml.EnsAppXml xml = new BP.Sys.Xml.EnsAppXml();
            }
        }
        public EnsAppCfg(string ensName, string cfgkey)
        {
            this.MyPK = ensName + "@" + cfgkey;
            try
            {
                this.Retrieve();
            }
            catch
            {
                BP.Sys.Xml.EnsAppXmls xmls = new BP.Sys.Xml.EnsAppXmls();
                int i = xmls.Retrieve(BP.Sys.Xml.EnsAppXmlEnsName.EnsName, ensName,
                      "No", cfgkey);
                if (i == 0)
                {
                    Attrs attrs = this.EnMap.HisCfgAttrs;
                    foreach (Attr attr in attrs)
                    {
                        if (attr.Key == cfgkey)
                        {
                            this.EnsName = ensName;
                            this.CfgKey = cfgkey;
                            if (attr.Key == "FocusField")
                            {
                                Entity en = BP.En.ClassFactory.GetEns(ensName).GetNewEntity;
                                if (en.EnMap.Attrs.Contains("Name"))
                                    this.CfgVal = "Name";
                                if (en.EnMap.Attrs.Contains("Title"))
                                    this.CfgVal = "Title";
                            }
                            else
                            {
                                this.CfgVal = attr.DefaultVal.ToString();
                            }
                            this.Insert();
                            return;
                        }
                    }
                }
                BP.Sys.Xml.EnsAppXml xml = null;
                if (xmls.Count == 0)
                    xml = new Xml.EnsAppXml();
                else
                    xml = xmls[0] as BP.Sys.Xml.EnsAppXml;

                this.EnsName = ensName;
                this.CfgKey = cfgkey;
                this.CfgVal = xml.DefVal;
                this.Insert();
            }
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_EnsAppCfg");
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ʵ�弯������";
                map.EnType = EnType.Sys;

                map.AddMyPK();
                map.AddTBString(EnsAppCfgAttr.EnsName, null, "ʵ�弯��", true, false, 0, 100, 60);
                map.AddTBString(EnsAppCfgAttr.CfgKey, null, "��", true, false, 0, 100, 60);
                map.AddTBString(EnsAppCfgAttr.CfgVal, null, "ֵ", true, false, 0, 200, 60);


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
	/// <summary>
    /// Ӧ������
	/// </summary>
    public class EnsAppCfgs : Entities
    {

        #region ��ȡ����
        public static string GetValString(string ensName, string cfgKey)
        {
            EnsAppCfg cfg = new EnsAppCfg(ensName,cfgKey);
            return  cfg.CfgVal;
        }
        public static int GetValInt(string ensName, string cfgKey)
        {
            try
            {
                EnsAppCfg cfg = new EnsAppCfg(ensName, cfgKey);
                return cfg.CfgValOfInt;
            }
            catch
            {
                return 400;
            }
        }
        public static bool GetValBoolen(string ensName, string cfgKey)
        {
            EnsAppCfg cfg = new EnsAppCfg(ensName, cfgKey);
            return cfg.CfgValOfBoolen;
        }
        #endregion ��ȡ����

        #region ����
        /// <summary>
        /// Ӧ������
        /// </summary>
        public EnsAppCfgs()
        {

        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new EnsAppCfg();
            }
        }
        #endregion
    }
}
