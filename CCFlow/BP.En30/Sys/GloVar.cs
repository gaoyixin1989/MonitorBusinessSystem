 using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// ȫ�ֱ���
    /// </summary>
    public class GloVarAttr : EntityNoNameAttr
    {
        /// <summary>
        /// Val
        /// </summary>
        public const string Val = "Val";
        /// <summary>
        /// Note
        /// </summary>
        public const string Note = "Note";
        /// <summary>
        /// GroupKey
        /// </summary>
        public const string GroupKey = "GroupKey";
    }
    /// <summary>
    /// ȫ�ֱ���
    /// </summary>
    public class GloVar : EntityNoName
    {
        #region ����
        public object ValOfObject
        {
            get
            {
                return this.GetValByKey(GloVarAttr.Val);
            }
            set
            {
                this.SetValByKey(GloVarAttr.Val, value);
            }
        }
        public string Val
        {
            get
            {
                return this.GetValStringByKey(GloVarAttr.Val);
            }
            set
            {
                this.SetValByKey(GloVarAttr.Val, value);
            }
        }
        public float ValOfFloat
        {
            get
            {
                return this.GetValFloatByKey(GloVarAttr.Val);
            }
            set
            {
                this.SetValByKey(GloVarAttr.Val, value);
            }
        }
        public int ValOfInt
        {
            get
            {
                return this.GetValIntByKey(GloVarAttr.Val);
            }
            set
            {
                this.SetValByKey(GloVarAttr.Val, value);
            }
        }
        public decimal ValOfDecimal
        {
            get
            {
                return this.GetValDecimalByKey(GloVarAttr.Val);
            }
            set
            {
                this.SetValByKey(GloVarAttr.Val, value);
            }
        }
        public bool ValOfBoolen
        {
            get
            {
                return this.GetValBooleanByKey(GloVarAttr.Val);
            }
            set
            {
                this.SetValByKey(GloVarAttr.Val, value);
            }
        }	
        /// <summary>
        /// note
        /// </summary>
        public string Note
        {
            get
            {
                return this.GetValStringByKey(GloVarAttr.Note);
            }
            set
            {
                this.SetValByKey(GloVarAttr.Note, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ȫ�ֱ���
        /// </summary>
        public GloVar()
        {
        }
        /// <summary>
        /// ȫ�ֱ���
        /// </summary>
        /// <param name="mypk"></param>
        public GloVar(string no)
        {
            this.No = no;
            this.Retrieve();
        }
         /// <summary>
		/// ��ֵ
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="isNullAsVal"></param> 
        public GloVar(string key, object isNullAsVal)
		{
			try
			{
				this.No=key;
				this.Retrieve(); 
			}
			catch
			{				
				if (this.RetrieveFromDBSources()==0)
				{
					this.Val = isNullAsVal.ToString();
					this.Insert();
				}
			}
		}
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_GloVar");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ȫ�ֱ���";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(GloVarAttr.No, null, "��", true, false, 1, 30, 20);
                map.AddTBString(GloVarAttr.Name, null, "����", true, false, 0, 120, 20);
                map.AddTBString(GloVarAttr.Val, null, "ֵ", true, false, 0, 4000, 20,true);
                map.AddTBString(GloVarAttr.GroupKey, null, "����ֵ", true, false, 0, 120, 20, true);
                map.AddTBStringDoc(GloVarAttr.Note, null, "˵��", true, false,true);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        #region ��������.
        private static string _Holidays = null;
        /// <summary>
        /// һ���·ݵļ���.
        /// </summary>
        public static string Holidays
        {
            get
            {
                if (_Holidays != null)
                    return _Holidays; 
                GloVar en = new GloVar();
                en.No ="Holiday";
                int i= en.RetrieveFromDBSources();
                if (i==0)
                    _Holidays="";
                else
                    _Holidays = en.Val;
                return _Holidays;
            }
            set
            {
                _Holidays = value;
            }
        }
        #endregion

    }
    /// <summary>
    /// ȫ�ֱ���s
    /// </summary>
    public class GloVars : EntitiesNoName
    {
        #region get value by key
        /// <summary>
        /// ���������ļ�
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="val">val</param>
        public static int SetValByKey(string key, object val)
        {
            GloVar en = new GloVar(key, val);
            en.ValOfObject = val;
            return en.Update();
        }
        /// <summary>
        /// ��ȡhtml����
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValByKeyHtml(string key)
        {
            return DataType.ParseText2Html(GloVars.GetValByKey(key));
        }
        public static string GetValByKey(string key)
        {
            foreach (GloVar cfg in GloVars.MyGloVars)
            {
                if (cfg.No == key)
                    return cfg.Val;
            }

            throw new Exception("error key=" + key);
        }
        /// <summary>
        /// �õ���һ��key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValByKey(string key, string isNullAs)
        {
            foreach (GloVar cfg in GloVars.MyGloVars)
            {
                if (cfg.No == key)
                    return cfg.Val;
            }

            GloVar en = new GloVar(key, isNullAs);
            //GloVar en = new GloVar(key);
            return en.Val;
        }
        public static int GetValByKeyInt(string key, int isNullAs)
        {
            foreach (GloVar cfg in GloVars.MyGloVars)
            {
                if (cfg.No == key)
                    return cfg.ValOfInt;
            }

            GloVar en = new GloVar(key, isNullAs);
            //GloVar en = new GloVar(key);
            return en.ValOfInt;
        }
        public static int GetValByKeyDecimal(string key, int isNullAs)
        {
            foreach (GloVar cfg in GloVars.MyGloVars)
            {
                if (cfg.No == key)
                    return cfg.ValOfInt;
            }

            GloVar en = new GloVar(key, isNullAs);
            //GloVar en = new GloVar(key);
            return en.ValOfInt;
        }
        public static bool GetValByKeyBoolen(string key, bool isNullAs)
        {

            foreach (GloVar cfg in GloVars.MyGloVars)
            {
                if (cfg.No == key)
                    return cfg.ValOfBoolen;
            }


            int val = 0;
            if (isNullAs)
                val = 1;

            GloVar en = new GloVar(key, val);

            return en.ValOfBoolen;
        }
        public static float GetValByKeyFloat(string key, float isNullAs)
        {
            foreach (GloVar cfg in GloVars.MyGloVars)
            {
                if (cfg.No == key)
                    return cfg.ValOfFloat;
            }

            GloVar en = new GloVar(key, isNullAs);
            return en.ValOfFloat;
        }
        private static GloVars _MyGloVars = null;
        public static GloVars MyGloVars
        {
            get
            {
                if (_MyGloVars == null)
                {
                    _MyGloVars = new GloVars();
                    _MyGloVars.RetrieveAll();
                }
                return _MyGloVars;
            }
        }
        public static void ReSetVal()
        {
            _MyGloVars = null;
        }
        #endregion

        #region ����
        /// <summary>
        /// ȫ�ֱ���s
        /// </summary>
        public GloVars()
        {
        }
        /// <summary>
        /// ȫ�ֱ���s
        /// </summary>
        /// <param name="fk_mapdata">s</param>
        public GloVars(string fk_mapdata)
        {
            if (SystemConfig.IsDebug)
                this.Retrieve(FrmLineAttr.FK_MapData, fk_mapdata);
            else
                this.RetrieveFromCash(FrmLineAttr.FK_MapData, (object)fk_mapdata);
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new GloVar();
            }
        }
        #endregion
    }
}
 