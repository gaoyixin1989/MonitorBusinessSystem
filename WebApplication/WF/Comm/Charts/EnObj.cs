using System;
using System.Collections;
using BP.DA;
using System.Data;
using BP.Sys;
using BP.En;

namespace BP.En
{
    /// <summary>
    /// ���ʿ���
    /// </summary>
    public class UAC
    {
        #region ���÷���
        public void Readonly()
        {
            this.IsUpdate = false;
            this.IsDelete = false;
            this.IsInsert = false;
            this.IsAdjunct = false;
            this.IsView = true;
        }
        /// <summary>
        /// ȫ������
        /// </summary>
        public void OpenAll()
        {
            this.IsUpdate = true;
            this.IsDelete = true;
            this.IsInsert = true;
            this.IsAdjunct = false;
            this.IsView = true;
        }
        /// <summary>
        /// Ϊһ����λ����ȫ����Ȩ��
        /// </summary>
        /// <param name="fk_station"></param>
        public void OpenAllForStation(string fk_station)
        {
            Paras ps = new Paras();
            ps.Add("user", Web.WebUser.No);
            ps.Add("st", fk_station);

            if (DBAccess.IsExits("SELECT FK_Emp FROM Port_EmpStation WHERE FK_Emp=" + SystemConfig.AppCenterDBVarStr + "user AND FK_Station=" + SystemConfig.AppCenterDBVarStr + "st", ps))
                this.OpenAll();
            else
                this.Readonly();
        }
        /// <summary>
        /// �����Թ���Ա
        /// </summary>
        public UAC OpenForSysAdmin()
        {
            if (SystemConfig.SysNo == "WebSite")
            {
                this.OpenAll();
                return this;
            }

            if (BP.Web.WebUser.No == "admin")
                this.OpenAll();
            return this;
        }
        public UAC OpenForAppAdmin()
        {
            if (BP.Web.WebUser.No != null && BP.Web.WebUser.No.Contains("admin") == true)
            {
                this.OpenAll();
            }
            return this;
        }
        #endregion

        #region ��������
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsInsert = false;
        /// <summary>
        /// �Ƿ�ɾ��
        /// </summary>
        public bool IsDelete = false;
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool IsUpdate = false;
        /// <summary>
        /// �Ƿ�鿴
        /// </summary>
        public bool IsView = true;
        /// <summary>
        /// ����
        /// </summary>
        public bool IsAdjunct = false;
        #endregion

        #region ����
        /// <summary>
        /// �û�����
        /// </summary>
        public UAC()
        {

        }
        #endregion
    }
    /// <summary>
    /// Entity ��ժҪ˵����
    /// </summary>	
    [Serializable]
    abstract public class EnObj
    {
        #region ���ʿ���.
        private string _DBVarStr = null;
        public string HisDBVarStr
        {
            get
            {
                if (_DBVarStr != null)
                    return _DBVarStr;

                _DBVarStr = this.EnMap.EnDBUrl.DBVarStr;
                return _DBVarStr;
            }
        }
        /// <summary>
        /// ���ķ��ʿ���.
        /// </summary>
        protected UAC _HisUAC = null;
        /// <summary>
        /// �õ� uac ����.
        /// </summary>
        /// <returns></returns>
        public virtual UAC HisUAC
        {
            get
            {
                if (_HisUAC == null)
                {
                    _HisUAC = new UAC();
                    if (BP.Web.WebUser.No == "admin")
                    {
                        _HisUAC.IsAdjunct = false;
                        _HisUAC.IsDelete = true;
                        _HisUAC.IsInsert = true;
                        _HisUAC.IsUpdate = true;
                        _HisUAC.IsView = true;
                    }
                }
                return _HisUAC;
            }
        }
        #endregion

        #region ȡ���ⲿ���õ�������Ϣ
        /// <summary>
        /// ȡ��Map ����չ���ԡ�
        /// ���ڵ�3������չ���Կ�����
        /// </summary>
        /// <param name="key">����Key</param>
        /// <returns>���õ�����</returns>
        public string GetMapExtAttrByKey(string key)
        {
            Paras ps = new Paras();
            ps.Add("enName", this.ToString());
            ps.Add("key", key);

            return (string)DBAccess.RunSQLReturnVal("select attrValue from Sys_ExtMap WHERE className=" + SystemConfig.AppCenterDBVarStr + "enName AND attrKey=" + SystemConfig.AppCenterDBVarStr + "key", ps);
        }
        #endregion

        #region CreateInstance
        /// <summary>
        /// ����һ��ʵ��
        /// </summary>
        /// <returns>�����ʵ��</returns>
        public Entity CreateInstance()
        {
            return this.GetType().Assembly.CreateInstance(this.ToString()) as Entity;
            //return ClassFactory.GetEn(this.ToString());
        }
        private Entities _GetNewEntities = null;
        #endregion

        #region ����
        /// <summary>
        /// ��������Ĭ��Ϣ.
        /// </summary>
        public void ResetDefaultVal()
        {
            Attrs attrs = this.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                string v = attr.DefaultValOfReal as string;
                if (v == null)
                    continue;

                if (attr.DefaultValOfReal.Contains("@") == false)
                    continue;

                string myval = this.GetValStrByKey(attr.Key);

                // ����Ĭ��ֵ.
                switch (v)
                {
                    case "@WebUser.No":
                        if (attr.UIIsReadonly == true)
                        {
                            this.SetValByKey(attr.Key, Web.WebUser.No);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(myval) || myval == v)
                                this.SetValByKey(attr.Key, Web.WebUser.No);
                        }
                        continue;
                    case "@WebUser.Name":
                        if (attr.UIIsReadonly == true)
                        {
                            this.SetValByKey(attr.Key, Web.WebUser.Name);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(myval) || myval == v)
                                this.SetValByKey(attr.Key, Web.WebUser.Name);
                        }
                        continue;
                    case "@WebUser.FK_Dept":
                        if (attr.UIIsReadonly == true)
                        {
                            this.SetValByKey(attr.Key, Web.WebUser.FK_Dept);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(myval) || myval == v)
                                this.SetValByKey(attr.Key, Web.WebUser.FK_Dept);
                        }
                        continue;
                    case "@WebUser.FK_DeptName":
                        if (attr.UIIsReadonly == true)
                        {
                            this.SetValByKey(attr.Key, Web.WebUser.FK_DeptName);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(myval) || myval == v)
                                this.SetValByKey(attr.Key, Web.WebUser.FK_DeptName);
                        }
                        continue;
                    case "@WebUser.FK_DeptNameOfFull":
                        if (attr.UIIsReadonly == true)
                        {
                            this.SetValByKey(attr.Key, Web.WebUser.FK_DeptNameOfFull);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(myval) || myval == v)
                                this.SetValByKey(attr.Key, Web.WebUser.FK_DeptNameOfFull);
                        }
                        continue;
                    case "@RDT":
                        if (attr.UIIsReadonly == true)
                        {
                            if (attr.MyDataType == DataType.AppDate || myval == v)
                                this.SetValByKey(attr.Key, DataType.CurrentData);

                            if (attr.MyDataType == DataType.AppDateTime || myval == v)
                                this.SetValByKey(attr.Key, DataType.CurrentDataTime);
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(myval) || myval == v)
                            {
                                if (attr.MyDataType == DataType.AppDate)
                                    this.SetValByKey(attr.Key, DataType.CurrentData);
                                else
                                    this.SetValByKey(attr.Key, DataType.CurrentDataTime);
                            }
                        }
                        continue;
                    default:
                        continue;
                }
            }
        }
        /// <summary>
        /// �����е�ֵ�����ó�Ĭ��ֵ�������������⡣
        /// </summary>
        public void ResetDefaultValAllAttr()
        {
            Attrs attrs = this.EnMap.Attrs;
            foreach (Attr attr in attrs)
            {
                if (attr.UIIsReadonly == false && attr.DefaultValOfReal != null)
                    continue;

                if (attr.IsPK)
                    continue;

                string v = attr.DefaultValOfReal as string;
                if (v == null)
                {
                    this.SetValByKey(attr.Key, "");
                    continue;
                }

                if (v.Contains("@") == false && v!=null )
                {
                    this.SetValByKey(attr.Key, v);
                    continue;
                }
 

                // ����Ĭ��ֵ.
                switch (v)
                {
                    case "@WebUser.No":
                        this.SetValByKey(attr.Key, Web.WebUser.No);
                        continue;
                    case "@WebUser.Name":
                        this.SetValByKey(attr.Key, Web.WebUser.Name);
                        continue;
                    case "@WebUser.FK_Dept":
                        this.SetValByKey(attr.Key, Web.WebUser.FK_Dept);
                        continue;
                    case "@WebUser.FK_DeptName":
                        this.SetValByKey(attr.Key, Web.WebUser.FK_DeptName);
                        continue;
                    case "@WebUser.FK_DeptNameOfFull":
                        this.SetValByKey(attr.Key, Web.WebUser.FK_DeptNameOfFull);
                        continue;
                    case "@RDT":
                        if (attr.MyDataType == DataType.AppDate)
                            this.SetValByKey(attr.Key, DataType.CurrentData);
                        else
                            this.SetValByKey(attr.Key, DataType.CurrentDataTime);
                        continue;
                    default:
                        continue;
                }
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// ʵ��
        /// </summary>
        public EnObj()
        {
        }
        private Map _tmpEnMap = null;
        /// <summary>
        /// Map
        /// </summary>
        protected Map _enMap
        {
            get
            {
                if (_tmpEnMap != null)
                    return _tmpEnMap;

                Map obj = Cash.GetMap(this.ToString());
                if (obj == null)
                {
                    if (_tmpEnMap == null)
                        return null;
                    else
                        return _tmpEnMap;
                }
                else
                {
                    _tmpEnMap = obj;
                }
                return _tmpEnMap;
            }
            set
            {
                if (value == null)
                {
                    _tmpEnMap = null;
                    return;
                }

                Map mp = (Map)value;
                if (SystemConfig.IsDebug)
                {
                    //#region ���map �Ƿ����
                    //if (mp != null)
                    //{
                    //    int i = 0;
                    //    foreach (Attr attr in this.EnMap.Attrs)
                    //    {
                    //        if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.PKFK)
                    //            i++;
                    //    }
                    //    if (i == 0)
                    //        throw new Exception("@û�и���" + this.EnDesc + "������������");

                    //    if (this.IsNoEntity)
                    //    {
                    //        if (mp.Attrs.Contains("No") == false)
                    //            throw new Exception("@EntityNo ��map��û�� No ���ԡ�@��" + mp.EnDesc + " , " + this.ToString());

                    //        if (i != 1)
                    //            throw new Exception("@��������� EntityNo �����ǲ�����ġ� @��" + mp.EnDesc + " , " + this.ToString());
                    //    }
                    //    else if (this.IsOIDEntity)
                    //    {
                    //        if (mp.Attrs.Contains("OID") == false)
                    //            throw new Exception("@EntityOID ��map��û�� OID ���ԡ�@��" + mp.EnDesc + " , " + this.ToString());
                    //        if (i != 1)
                    //            throw new Exception("@��������� EntityOID �����ǲ�����ġ� @��" + mp.EnDesc + " , " + this.ToString());
                    //    }
                    //    else
                    //    {
                    //        if (mp.Attrs.Contains("MyPK"))
                    //            if (i != 1)
                    //                throw new Exception("@��������� EntityMyPK �����ǲ�����ġ� @��" + mp.EnDesc + " , " + this.ToString());
                    //    }
                    //}
                    //#endregion ���map �Ƿ����
                }

                if (mp == null || mp.DepositaryOfMap == Depositary.None)
                {
                    _tmpEnMap = mp;
                    return;
                }

                Cash.SetMap(this.ToString(), mp);
                _tmpEnMap = mp;
            }
        }
        /// <summary>
        /// ������Ҫ�̳�
        /// </summary>
        public abstract Map EnMap
        {
            get;
        }
        /// <summary>
        /// ��̬�Ļ�ȡmap
        /// </summary>
        public Map EnMapInTime
        {
            get
            {
                _tmpEnMap = null;
                Cash.SetMap(this.ToString(), null);
                return this.EnMap;
            }
        }

        #endregion

        #region row ���ʵ�����ݵ�
        /// <summary>
        /// ʵ��� map ��Ϣ��	
        /// </summary>		
        //public abstract void EnMap();		
        private Row _row = null;
        public Row Row
        {
            get
            {
                if (this._row == null)
                {
                    this._row = new Row();
                    this._row.LoadAttrs(this.EnMap.Attrs);
                }
                return this._row;
            }
            set
            {
                this._row = value;
            }
        }
        #endregion

        #region �������ԵĲ�����

        #region ����ֵ����
        public void SetValByKeySuperLink(string attrKey, string val)
        {
            this.SetValByKey(attrKey, DataType.DealSuperLink(val));
        }

        /// <summary>
        /// ����object���͵�ֵ
        /// </summary>
        /// <param name="attrKey">attrKey</param>
        /// <param name="val">val</param>
        public void SetValByKey(string attrKey, string val)
        {
            switch (val)
            {
                case null:
                case "&nbsp;":
                    val = "";
                    break;
                case "RDT":
                    if (val.Length > 4)
                    {
                        this.SetValByKey("FK_NY", val.Substring(0, 7));
                        this.SetValByKey("FK_ND", val.Substring(0, 4));
                    }
                    break;
                default:
                    break;
            }
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, int val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, Int64 val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, float val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, decimal val)
        {
            this.Row.SetValByKey(attrKey, val);
        }
        public void SetValByKey(string attrKey, object val)
        {
            this.Row.SetValByKey(attrKey, val);
        }

        public void SetValByDesc(string attrDesc, object val)
        {
            if (val == null)
                throw new Exception("@������������[" + attrDesc + "]null ֵ��");
            this.Row.SetValByKey(this.EnMap.GetAttrByDesc(attrDesc).Key, val);
        }

        /// <summary>
        /// ���ù������͵�ֵ
        /// </summary>
        /// <param name="attrKey">attrKey</param>
        /// <param name="val">val</param>
        public void SetValRefTextByKey(string attrKey, object val)
        {
            this.SetValByKey(attrKey + "Text", val);
        }
        /// <summary>
        /// ����bool���͵�ֵ
        /// </summary>
        /// <param name="attrKey">attrKey</param>
        /// <param name="val">val</param>
        public void SetValByKey(string attrKey, bool val)
        {
            if (val)
                this.SetValByKey(attrKey, 1);
            else
                this.SetValByKey(attrKey, 0);
        }
        /// <summary>
        /// ����Ĭ��ֵ
        /// </summary>
        public void SetDefaultVals()
        {
            foreach (Attr attr in this.EnMap.Attrs)
            {
                this.SetValByKey(attr.Key, attr.DefaultVal);
            }
        }
        /// <summary>
        /// �����������͵�ֵ
        /// </summary>
        /// <param name="attrKey">attrKey</param>
        /// <param name="val">val</param>
        public void SetDateValByKey(string attrKey, string val)
        {
            try
            {
                this.SetValByKey(attrKey, DataType.StringToDateStr(val));
            }
            catch (System.Exception ex)
            {
                throw new Exception("@���Ϸ����������ݸ�ʽ:key=[" + attrKey + "],value=" + val + " " + ex.Message);
            }
        }
        #endregion

        #region ȡֵ����
        /// <summary>
        /// ȡ��Object
        /// </summary>
        /// <param name="attrKey"></param>
        /// <returns></returns>
        public Object GetValByKey(string attrKey)
        {
            return this.Row.GetValByKey(attrKey);

            //try
            //{
            //    return this.Row.GetValByKey(attrKey);				
            //}
            //catch(Exception ex)
            //{
            //    throw new Exception(ex.Message+"  "+attrKey+" EnsName="+this.ToString() );
            //}
        }
        /// <summary>
        /// GetValDateTime
        /// </summary>
        /// <param name="attrKey"></param>
        /// <returns></returns>
        public DateTime GetValDateTime(string attrKey)
        {
            return DataType.ParseSysDateTime2DateTime(this.GetValStringByKey(attrKey));
        }
        /// <summary>
        /// ��ȷ��  attrKey ���� map ������²���ʹ����
        /// </summary>
        /// <param name="attrKey"></param>
        /// <returns></returns>
        public string GetValStrByKey(string key)
        {
            return this.Row.GetValByKey(key).ToString();
        }
        public string GetValStrByKey(string key, string isNullAs)
        {
            try
            {
                return this.Row.GetValByKey(key).ToString();
            }
            catch
            {
                return isNullAs;
            }
        }
        /// <summary>
        /// ȡ��String
        /// </summary>
        /// <param name="attrKey"></param>
        /// <returns></returns>
        public string GetValStringByKey(string attrKey)
        {
            switch (attrKey)
            {
                case "Doc":
                    string s = this.Row.GetValByKey(attrKey).ToString();
                    if (s == "")
                        s = this.GetValDocText();
                    return s;
                default:
                    try
                    {
                        if (this.Row == null)
                            throw new Exception("@û�г�ʼ��Row.");
                        return this.Row.GetValByKey(attrKey).ToString();
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("@��ȡֵ�ڼ���������쳣��" + ex.Message + "  " + attrKey + " ��û����������������ԣ�EnsName=" + this.ToString());
                    }
                    break;
            }
        }
        public string GetValStringByKey(string attrKey, string defVal)
        {
            string val = this.GetValStringByKey(attrKey);
            if (val == null || val == "")
                return defVal;
            else
                return val;
        }
        /// <summary>
        ///  ȡ������ı�
        /// </summary>
        /// <returns></returns>
        public string GetValDocText()
        {
            string s = this.GetValStrByKey("Doc");
            if (s.Trim().Length != 0)
                return s;

            s = SysDocFile.GetValTextV2(this.ToString(), this.PKVal.ToString());
            this.SetValByKey("Doc", s);
            return s;
        }
        public string GetValDocHtml()
        {
            string s = this.GetValHtmlStringByKey("Doc");
            if (s.Trim().Length != 0)
                return s;

            s = SysDocFile.GetValHtmlV2(this.ToString(), this.PKVal.ToString());
            this.SetValByKey("Doc", s);
            return s;
        }
        /// <summary>
        /// ȡ��Html ��Ϣ��
        /// </summary>
        /// <param name="attrKey">attr</param>
        /// <returns>html.</returns>
        public string GetValHtmlStringByKey(string attrKey)
        {
            return DataType.ParseText2Html(this.GetValStringByKey(attrKey));
        }
        public string GetValHtmlStringByKey(string attrKey, string defval)
        {
            return DataType.ParseText2Html(this.GetValStringByKey(attrKey, defval));
        }
        /// <summary>
        /// ȡ��ö�ٻ�������ı�ǩ
        /// �����ö�پͻ�ȡö�ٱ�ǩ.
        /// ���������ͻ�ȡΪ���������.
        /// </summary>
        /// <param name="attrKey"></param>
        /// <returns></returns>
        public string GetValRefTextByKey(string attrKey)
        {
            string str = "";
            try
            {
                str = this.Row.GetValByKey(attrKey + "Text") as string;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + attrKey);
            }
            return str;
        }
        public Int64 GetValInt64ByKey(string key)
        {
            return Int64.Parse(this.GetValStringByKey(key));
        }
        public int GetValIntByKey(string key, int IsZeroAs)
        {
            int i = this.GetValIntByKey(key);
            if (i == 0)
                i = IsZeroAs;
            return i;
        }
        public int GetValIntByKey11(string key)
        {
            return int.Parse(this.GetValStrByKey(key));
        }
        /// <summary>
        /// ����key �õ�int val
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int GetValIntByKey(string key)
        {
            try
            {
                return int.Parse(this.GetValStrByKey(key));
            }
            catch (Exception ex)
            {
                //if (SystemConfig.IsDebug == false)
                //    throw new Exception("@[" + this.EnMap.GetAttrByKey(key).Desc + "]���������֣����������[" + this.GetValStrByKey(key) + "]��");
                //else
                //    throw new Exception("@��[" + this.EnDesc + "]�ڻ�ȡ����[" + key + "]ֵ,���ִ��󣬲��ܽ�[" + this.GetValStringByKey(key) + "]ת��Ϊint����.������Ϣ��" + ex.Message + "@�����Ƿ��ڴ洢ö������ʱ������SetValbyKey��û��ת������ȷ������:this.SetValByKey( Key ,(int)value)  ");

                string v = this.GetValStrByKey(key).ToLower();
                if (v == "true")
                {
                    this.SetValByKey(key, 1);
                    return 1;
                }
                if (v == "false")
                {
                    this.SetValByKey(key, 0);
                    return 0;
                }

                if (key == "OID")
                {
                    this.SetValByKey("OID", 0);
                    return 0;
                }

                if (this.GetValStrByKey(key) == "")
                {
                    Attr attr = this.EnMap.GetAttrByKey(key);
                    if (attr.IsNull)
                        return 567567567;
                    else
                        return int.Parse(attr.DefaultVal.ToString());
                }

                //else
                //{
                //    return int.Parse(this.EnMap.GetAttrByKey(key).DefaultVal.ToString());
                //}

                if (SystemConfig.IsDebug == false)
                    throw new Exception("@[" + this.EnMap.GetAttrByKey(key).Desc + "]���������֣����������[" + this.GetValStrByKey(key) + "]��");
                else
                    throw new Exception("@��[" + this.EnDesc + "]�ڻ�ȡ����[" + key + "]ֵ,���ִ��󣬲��ܽ�[" + this.GetValStringByKey(key) + "]ת��Ϊint����.������Ϣ��" + ex.Message + "@�����Ƿ��ڴ洢ö������ʱ������SetValbyKey��û��ת������ȷ������:this.SetValByKey( Key ,(int)value)  ");
            }
        }
        /// <summary>
        /// ����key �õ� bool val
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool GetValBooleanByKey(string key)
        {
            string s = this.GetValStrByKey(key);
            if (string.IsNullOrEmpty(s))
                s = this.EnMap.GetAttrByKey(key).DefaultVal.ToString();

            if (s.ToUpper() == "FALSE")
                return false;
            if (s.ToUpper() == "TRUE")
                return true;

            if (int.Parse(s) == 0)
                return false;
            else
                return true;
        }

        public bool GetValBooleanByKey(string key, bool defval)
        {
            try
            {

                if (int.Parse(this.GetValStringByKey(key)) == 0)
                    return false;
                else
                    return true;
            }
            catch
            {
                return defval;
            }
        }
        public string GetValBoolStrByKey(string key)
        {
            if (int.Parse(this.GetValStringByKey(key)) == 0)
                return "��";
            else
                return "��";
        }
        /// <summary>
        /// ����key �õ�flaot val
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public float GetValFloatByKey(string key, int blNum)
        {
            return float.Parse(float.Parse(this.Row.GetValByKey(key).ToString()).ToString("0.00"));
        }
        /// <summary>
        /// ����key �õ�flaot val
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public float GetValFloatByKey(string key)
        {
            try
            {
                return float.Parse(float.Parse(this.Row.GetValByKey(key).ToString()).ToString("0.00"));
            }
            catch
            {
                if (this.GetValStringByKey(key) == "")
                {
                    Attr attr = this.EnMap.GetAttrByKey(key);
                    if (attr.IsNull)
                        return 567567567;
                    else
                        return float.Parse(attr.DefaultVal.ToString());
                }
                return 0;
            }
        }
        public decimal GetValMoneyByKey(string key)
        {
            try
            {
                return decimal.Parse(this.GetValDecimalByKey(key).ToString("0.00"));
            }
            catch
            {
                if (this.GetValStringByKey(key) == "")
                {
                    Attr attr = this.EnMap.GetAttrByKey(key);
                    if (attr.IsNull)
                        return 567567567;
                    else
                        return decimal.Parse(attr.DefaultVal.ToString());
                }
                return 0;
            }
        }
        /// <summary>
        /// ����key �õ�flaot val
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public decimal GetValDecimalByKey(string key)
        {
            return decimal.Round(decimal.Parse(this.GetValStrByKey(key)), 4);
        }
        public decimal GetValDecimalByKey(string key, string items)
        {
            if (items == "" || items == null)
                return 0;

            if (items.IndexOf("@" + key) == -1)
                return 0;

            string str = items.Substring(items.IndexOf("@" + key));

            return decimal.Round(decimal.Parse(this.GetValStringByKey(key)), 4);
        }
        public double GetValDoubleByKey(string key)
        {
            try
            {
                return double.Parse(this.GetValStrByKey(key));
            }
            catch (Exception ex)
            {
                throw new Exception("@��[" + this.EnDesc + "]�ڻ�ȡ����[" + key + "]ֵ,���ִ��󣬲��ܽ�[" + this.GetValStringByKey(key) + "]ת��Ϊdouble����.������Ϣ��" + ex.Message);
            }
        }
        public string GetValAppDateByKey(string key)
        {
            try
            {
                string str = this.GetValStringByKey(key);
                if (str == null || str == "")
                    return str;
                return DataType.StringToDateStr(str);
            }
            catch (System.Exception ex)
            {
                throw new Exception("@ʵ����[" + this.EnMap.EnDesc + "]  ����[" + key + "]ֵ[" + this.GetValStringByKey(key).ToString() + "]���ڸ�ʽת�����ִ���" + ex.Message);
            }
            //return "2003-08-01";
        }
        #endregion

        #endregion

        #region ��ȡ������Ϣ
        public string GetCfgValStr(string key)
        {
            return BP.Sys.EnsAppCfgs.GetValString(this.ToString() + "s", key);
        }

        public int GetCfgValInt(string key)
        {
            return BP.Sys.EnsAppCfgs.GetValInt(this.ToString() + "s", key);
        }

        public bool GetCfgValBoolen(string key)
        {
            return BP.Sys.EnsAppCfgs.GetValBoolen(this.ToString() + "s", key);
        }
        public void SetCfgVal(string key, object val)
        {
            BP.Sys.EnsAppCfg cfg = new EnsAppCfg();
            cfg.MyPK = this.ToString() + "s@" + key;
            cfg.CfgKey = key;
            cfg.CfgVal = val.ToString();
            cfg.EnsName = this.ToString() + "s";
            cfg.Save();
        }
        #endregion

        #region ����
        /// <summary>
        /// �ļ�������
        /// </summary>
        public SysFileManagers HisSysFileManagers
        {
            get
            {
                return new SysFileManagers(this.ToString(), this.PKVal.ToString());
            }
        }
        public bool IsBlank
        {
            get
            {
                if (this._row == null)
                    return true;

                Attrs attrs = this.EnMap.Attrs;
                foreach (Attr attr in attrs)
                {

                    if (attr.UIIsReadonly && attr.IsFKorEnum == false)
                        continue;

                    //if (attr.IsFK && string.IsNullOrEmpty(attr.DefaultVal.ToString()) ==true)
                    //    continue; /*��������,���������Ĭ��ֵΪnull.*/

                    string str = this.GetValStrByKey(attr.Key);
                    if (str == "" || str == attr.DefaultVal.ToString() || str == null)
                        continue;

                    if (attr.MyDataType == DataType.AppDate && attr.DefaultVal == null)
                    {
                        if (str == DataType.CurrentData)
                            continue;
                        else
                            return true;
                    }

                    if (str == attr.DefaultVal.ToString() && attr.IsFK == false)
                        continue;

                    if (attr.IsEnum)
                    {
                        if (attr.DefaultVal.ToString() == str)
                            continue;
                        else
                            return false;
                        continue;
                    }

                    if (attr.IsNum)
                    {
                        if (decimal.Parse(str) != decimal.Parse(attr.DefaultVal.ToString()))
                            return false;
                        else
                            continue;
                    }

                    if (attr.IsFKorEnum)
                    {
                        //if (attr.DefaultVal == null || attr.DefaultVal == "")
                        //    continue;

                        if (attr.DefaultVal.ToString() != str)
                            return false;
                        else
                            continue;
                    }

                    if (str != attr.DefaultVal.ToString())
                        return false;
                }
                return true;
            }
        }
        /// <summary>
        /// ��ȡ��������
        /// �ǲ��ǿյ�ʵ��.
        /// </summary>
        public bool IsEmpty
        {
            get
            {
                if (this._row == null)
                {
                    return true;
                }
                else
                {
                    if (this.PKVal == null || this.PKVal.ToString() == "0" || this.PKVal.ToString() == "")
                        return true;
                    return false;
                }
            }
            set
            {
                this._row = null;
            }
        }
        /// <summary>
        /// �����ʵ�������
        /// </summary>
        public String EnDesc
        {
            get
            {
                return this.EnMap.EnDesc;
            }
        }
        /// <summary>
        /// ȡ������ֵ���������������Ψһ���ͷ��ص�һ��ֵ��
        /// ��ȡ������
        /// </summary>
        public Object PKVal
        {
            get
            {
                return this.GetValByKey(this.PK);
            }
            set
            {
                this.SetValByKey(this.PK, value);
            }
        }
        /// <summary>
        /// ���ֻ��һ������,�ͷ���PK,����ж���ͷ��ص�һ��.PK
        /// </summary>
        public int PKCount
        {
            get
            {
                switch (this.PK)
                {
                    case "OID":
                    case "No":
                    case "MyPK":
                        return 1;
                    default:
                        break;
                }

                int i = 0;
                foreach (Attr attr in this.EnMap.Attrs)
                {
                    if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.PKFK)
                        i++;
                }
                if (i == 0)
                    throw new Exception("@û�и���" + this.EnDesc + "��" + this.EnMap.PhysicsTable + "������������");
                else
                    return i;
            }
        }
        /// <summary>
        /// �ǲ���OIDEntity
        /// </summary>
        public bool IsOIDEntity
        {
            get
            {
                if (this.PK == "OID")
                    return true;
                return false;
            }
        }
        /// <summary>
        /// �ǲ���OIDEntity
        /// </summary>
        public bool IsNoEntity
        {
            get
            {
                if (this.PK == "No")
                    return true;
                return false;
            }
        }
        /// <summary>
        /// �Ƿ���TreeEntity
        /// </summary>
        public bool IsTreeEntity
        {
            get
            {
                if (this.PK == "ID")
                    return true;
                return false;
            }
        }
        /// <summary>
        /// �ǲ���IsMIDEntity
        /// </summary>
        public bool IsMIDEntity
        {
            get
            {
                if (this.PK == "MID")
                    return true;
                return false;
            }
        }
        /// <summary>
        /// ���ֻ��һ������,�ͷ���PK,����ж���ͷ��ص�һ��.PK
        /// </summary>
        public virtual string PK
        {
            get
            {
                string pks = "";
                foreach (Attr attr in this.EnMap.Attrs)
                {
                    if (attr.MyFieldType == FieldType.PK
                        || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.PKFK)
                        pks += attr.Key + ",";
                }
                if (pks == "")
                    throw new Exception("@û�и���" + this.EnDesc + "��" + this.EnMap.PhysicsTable + "������������");
                pks = pks.Substring(0, pks.Length - 1);
                return pks;
            }
        }
        public virtual string PKField
        {
            get
            {
                foreach (Attr attr in this.EnMap.Attrs)
                {
                    if (attr.MyFieldType == FieldType.PK
                        || attr.MyFieldType == FieldType.PKEnum
                        || attr.MyFieldType == FieldType.PKFK)
                        return attr.Field;
                }
                throw new Exception("@û�и���" + this.EnDesc + "������������");
            }
        }
        /// <summary>
        /// ���ֻ��һ������,�ͷ���PK,����ж���ͷ��ص�һ��.PK
        /// </summary>
        public string[] PKs
        {
            get
            {
                string[] strs1 = new string[this.PKCount];
                int i = 0;
                foreach (Attr attr in this.EnMap.Attrs)
                {
                    if (attr.MyFieldType == FieldType.PK || attr.MyFieldType == FieldType.PKEnum || attr.MyFieldType == FieldType.PKFK)
                    {
                        strs1[i] = attr.Key;
                        i++;
                    }
                }
                return strs1;
            }
        }
        /// <summary>
        /// ȡ������ֵ��
        /// </summary>
        public Hashtable PKVals
        {
            get
            {
                Hashtable ht = new Hashtable();
                string[] strs = this.PKs;
                foreach (string str in strs)
                {
                    ht.Add(str, this.GetValStringByKey(str));
                }
                return ht;
            }
        }
        #endregion

        public void domens()
        {
        }

    }

}
