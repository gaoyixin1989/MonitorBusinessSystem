using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP;
namespace BP.Sys
{
    /// <summary>
    /// UI������.Search. Card, Group��Ϣ.
    /// </summary>
    public class UIConfig
    {
        public Entity HisEn;
        public AtPara HisAP;
        public UIConfig()
        {
        }
        /// <summary>
        /// UI������.Search. Card, Group��Ϣ.
        /// </summary>
        /// <param name="enName"></param>
        public UIConfig(Entity en)
        {
            this.HisEn = en;
            EnCfg cfg = new EnCfg(en.ToString());
            string paraStr = cfg.UI;
            if (string.IsNullOrEmpty(paraStr) == true)
                paraStr = "@UIRowStyleGlo=0@IsEnableDouclickGlo=1@IsEnableRefFunc=1@IsEnableFocusField=1@IsEnableOpenICON=1@FocusField=''@WinCardH=600@@WinCardW=800@ShowColumns=";
            HisAP = new AtPara(paraStr);
        }

        /// <summary>
        /// ��ȡ��ʾ�����飬�м���,����
        /// </summary>
        public string[] ShowColumns
        {
            get
            {
                var colstr = this.HisAP.GetValStrByKey("ShowColumns");

                if(string.IsNullOrWhiteSpace(colstr))
                    return new string[0];

                return colstr.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            }
        }

        #region �ƶ�.
        /// <summary>
        /// �ƶ�����ʽ.
        /// </summary>
        public MoveToShowWay MoveToShowWay
        {
            get
            {
                return (MoveToShowWay)this.HisAP.GetValIntByKey("MoveToShowWay");
            }
        }
        public EditerType EditerType
        {
            get
            {
                return (EditerType)this.HisAP.GetValIntByKey("EditerType");
            }
        }

        /// <summary>
        /// �ƶ����ֶ�
        /// </summary>
        public string MoveTo
        {
            get
            {
                string s = this.HisAP.GetValStrByKey("MoveTo");
                return s;
            }
        }
        #endregion �ƶ�.

        /// <summary>
        /// �������
        /// </summary>
        public int UIRowStyleGlo
        {
            get
            {
                return this.HisAP.GetValIntByKey("UIRowStyleGlo");
            }
        }

        /// <summary>
        /// �Ƿ�����˫���򿪣�
        /// </summary>
        public bool IsEnableDouclickGlo
        {
            get
            {
                return this.HisAP.GetValBoolenByKey("IsEnableDouclickGlo");
            }
        }
        /// <summary>
        /// �Ƿ���ʾ��ع���?
        /// </summary>
        public bool IsEnableRefFunc
        {
            get
            {
                return this.HisAP.GetValBoolenByKey("IsEnableRefFunc");
            }
        }
        /// <summary>
        /// �Ƿ����ý����ֶ�
        /// </summary>
        public bool IsEnableFocusField
        {
            get
            {
                return this.HisAP.GetValBoolenByKey("IsEnableFocusField");
            }
        }

        /// <summary>
        /// �Ƿ��ICON
        /// </summary>
        public bool IsEnableOpenICON
        {
            get
            {
                return this.HisAP.GetValBoolenByKey("IsEnableOpenICON");
            }
        }
        /// <summary>
        /// �����ֶ�
        /// </summary>
        public string FocusField
        {
            get
            {
                string s = this.HisAP.GetValStrByKey("FocusField");
                if (string.IsNullOrEmpty(s))
                {
                    if (this.HisEn.EnMap.Attrs.Contains("Name"))
                        return "Name";
                    if (this.HisEn.EnMap.Attrs.Contains("Title"))
                        return "Title";
                }
                return s;
            }
        }
        public int WinCardW
        {
            get
            {
                return this.HisAP.GetValIntByKey("WinCardW");
            }
        }
        public int WinCardH
        {
            get
            {
                return this.HisAP.GetValIntByKey("WinCardH");
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public int Save()
        {
            EnCfg cfg = new EnCfg(this.HisEn.ToString());
            cfg.UI = this.HisAP.GenerAtParaStrs();
            return cfg.Save();
        }


    }
    /// <summary>
    ///  ������Ϣ
    /// </summary>
    public class EnCfgAttr : EntityNoAttr
    {
        /// <summary>
        /// �����ǩ
        /// </summary>
        public const string GroupTitle = "GroupTitle";
        /// <summary>
        /// ����·��
        /// </summary>
        public const string FJSavePath = "FJSavePath";
        /// <summary>
        /// ����·��
        /// </summary>
        public const string FJWebPath = "FJWebPath";
        /// <summary>
        /// ���ݷ�����ʽ
        /// </summary>
        public const string Datan = "Datan";
        /// <summary>
        /// Search,Group,����.
        /// </summary>
        public const string UI = "UI";
    }
    /// <summary>
    /// EnCfgs
    /// </summary>
    public class EnCfg : EntityNo
    {
        #region UI����.
        public string UI
        {
            get
            {
                return this.GetValStringByKey(EnCfgAttr.UI);
            }
            set
            {
                this.SetValByKey(EnCfgAttr.UI, value);
            }
        }
        #endregion UI����.


        #region ��������
        /// <summary>
        /// ���ݷ�����ʽ
        /// </summary>
        public string Datan
        {
            get
            {
                return this.GetValStringByKey(EnCfgAttr.Datan);
            }
            set
            {
                this.SetValByKey(EnCfgAttr.Datan, value);
            }
        }
        /// <summary>
        /// ����Դ
        /// </summary>
        public string GroupTitle
        {
            get
            {
                return this.GetValStringByKey(EnCfgAttr.GroupTitle);
            }
            set
            {
                this.SetValByKey(EnCfgAttr.GroupTitle, value);
            }
        }
        /// <summary>
        /// ����·��
        /// </summary>
        public string FJSavePath
        {
            get
            {
                string str = this.GetValStringByKey(EnCfgAttr.FJSavePath);
                if (str == "" || str == null || str == string.Empty)
                    return BP.Sys.SystemConfig.PathOfDataUser + this.No + "\\";
                return str;
            }
            set
            {
                this.SetValByKey(EnCfgAttr.FJSavePath, value);
            }
        }
        public string FJWebPath
        {
            get
            {
                string str = this.GetValStringByKey(EnCfgAttr.FJWebPath);
                if (str == "" || str == null)
                    return BP.Sys.Glo.Request.ApplicationPath + "/DataUser/" + this.No + "/";
                return str;
            }
            set
            {
                this.SetValByKey(EnCfgAttr.FJWebPath, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ϵͳʵ��
        /// </summary>
        public EnCfg()
        {
        }
        /// <summary>
        /// ϵͳʵ��
        /// </summary>
        /// <param name="no"></param>
        public EnCfg(string enName)
        {
            this.No = enName;
            try
            {
                this.Retrieve();
            }
            catch (Exception ex)
            {
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
                Map map = new Map("Sys_EnCfg");
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ʵ������";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(EnCfgAttr.No, null, "ʵ������", true, false, 1, 100, 60);
                map.AddTBString(EnCfgAttr.GroupTitle, null, "�����ǩ", true, false, 0, 2000, 60);
                map.AddTBString(EnCfgAttr.FJSavePath, null, "����·��", true, false, 0, 100, 60);
                map.AddTBString(EnCfgAttr.FJWebPath, null, "����Web·��", true, false, 0, 100, 60);
                map.AddTBString(EnCfgAttr.Datan, null, "�ֶ����ݷ�����ʽ", true, false, 0, 200, 60);
                map.AddTBString(EnCfgAttr.UI, null, "UI����", true, false, 0, 2000, 60);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ʵ�弯��
    /// </summary>
    public class EnCfgs : EntitiesNoName
    {
        #region ����
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public EnCfgs()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new EnCfg();
            }
        }
        #endregion
    }
}
