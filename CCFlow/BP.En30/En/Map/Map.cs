using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.Sys;
using BP.Web.Controls;

namespace BP.En
{
    /// <summary>
    /// �༭������
    /// </summary>
    public enum EditerType
    {
        /// <summary>
        /// �ޱ༭��
        /// </summary>
        None,
        /// <summary>
        /// Sina�༭��
        /// </summary>
        Sina,
        /// <summary>
        /// FKEditer
        /// </summary>
        FKEditer,
        /// <summary>
        /// KindEditor
        /// </summary>
        KindEditor,
        /// <summary>
        /// �ٶȵ�UEditor
        /// </summary>
        UEditor
    }
    /// <summary>
    /// ��������
    /// </summary>
    public enum AdjunctType
    {
        /// <summary>
        /// ����Ҫ������
        /// </summary>
        None,
        /// <summary>
        /// ͼƬ
        /// </summary>
        PhotoOnly,
        /// <summary>
        /// word �ĵ���
        /// </summary>
        WordOnly,
        /// <summary>
        /// ���е�����
        /// </summary>
        ExcelOnly,
        /// <summary>
        /// ���е����͡�
        /// </summary>
        AllType
    }
    /// <summary>
    /// ʵ������
    /// </summary>
    public enum EnType
    {
        /// <summary>
        /// ϵͳʵ��
        /// </summary>
        Sys,
        /// <summary>
        /// ����Աά����ʵ��
        /// </summary>
        Admin,
        /// <summary>
        /// Ӧ�ó���ʵ��
        /// </summary>
        App,
        /// <summary>
        /// ��������ʵ�壨���Ը��£�
        /// </summary>
        ThirdPartApp,
        /// <summary>
        /// ��ͼ(������Ч)
        /// </summary>
        View,
        /// <summary>
        /// ��������Ȩ�޹���
        /// </summary>
        PowerAble,
        /// <summary>
        /// ����
        /// </summary>
        Etc,
        /// <summary>
        /// ��ϸ���ŵ�Ե㡣
        /// </summary>
        Dtl,
        /// <summary>
        /// ��Ե�
        /// </summary>
        Dot2Dot,
        /// <summary>
        /// XML������
        /// </summary>
        XML,
        /// <summary>
        /// ��չ���ͣ������ڲ�ѯ����Ҫ��
        /// </summary>
        Ext
    }
    /// <summary>
    /// �ƶ�����ʾ��ʽ
    /// </summary>
    public enum MoveToShowWay
    {
        /// <summary>
        /// ����ʾ
        /// </summary>
        None,
        /// <summary>
        /// �����б�
        /// </summary>
        DDL,
        /// <summary>
        /// ƽ��
        /// </summary>
        Panel
    }
    /// <summary>
    /// EnMap ��ժҪ˵����
    /// </summary>
    public class Map
    {
        #region ����.
        /// <summary>
        /// ���Ӱ���
        /// </summary>
        /// <param name="key">�ֶ�</param>
        /// <param name="url"></param>
        public void SetHelperUrl(string key, string url)
        {
            Attr attr = this.GetAttrByKey(key);
            attr.HelperUrl = url;
        }
        /// <summary>
        /// ���Ӱ���
        /// </summary>
        /// <param name="key">�ֶ�</param>
        public void SetHelperBaidu(string key)
        {
            Attr attr = this.GetAttrByKey(key);
            attr.HelperUrl = "http://www.baidu.com/s?word=ccflow " + attr.Desc;
        }
        /// <summary>
        /// ���Ӱ���
        /// </summary>
        /// <param name="key">�ֶ�</param>
        /// <param name="keyword">�ؼ���</param>
        public void SetHelperBaidu(string key, string keyword)
        {
            Attr attr = this.GetAttrByKey(key);
            attr.HelperUrl = "http://www.baidu.com/s?word=" + keyword;
        }
        /// <summary>
        /// ���Ӱ���
        /// </summary>
        /// <param name="key">�ֶ�</param>
        /// <param name="context">����</param>
        public void SetHelperAlert(string key, string context)
        {
            Attr attr = this.GetAttrByKey(key);
            attr.HelperUrl = "javascript:alert('"+context+"')";
        }
        #endregion ����.


        #region ��xml �ļ������й�ϵ
        /// <summary>
        /// xml �ļ���λ��
        /// </summary>
        public string XmlFile = null;
        #endregion ��xml �ļ������й�ϵ

        private Boolean _IsAllowRepeatNo;
        public Boolean IsAllowRepeatNo 
        {
            get { return _IsAllowRepeatNo; }
            set { _IsAllowRepeatNo = value; }
        }

        #region chuli
        /// <summary>
        /// ��ѯ���(Ϊ�˱���������Դ�˷�,һ�������ɶ��ʹ��)
        /// </summary>
        public string SelectSQL = null;
        /// <summary>
        /// �Ƿ��Ǽ򵥵����Լ���
        /// �����Ǵ�����������⣬��ϵͳ���������й�����̫�������ͻ�Ӱ��Ч�ʡ�
        /// </summary>
        public bool IsSimpleAttrs = false;
        /// <summary>
        /// ����Ϊ�򵥵�
        /// </summary>
        public Attrs SetToSimple()
        {
            Attrs attrs = new Attrs();
            foreach (Attr attr in this._attrs)
            {
                if (attr.MyFieldType == FieldType.PK ||
                    attr.MyFieldType == FieldType.PKEnum
                    ||
                    attr.MyFieldType == FieldType.PKFK)
                {
                    attrs.Add(new Attr(attr.Key, attr.Field, attr.DefaultVal, attr.MyDataType, true, attr.Desc));
                }
                else
                {
                    attrs.Add(new Attr(attr.Key, attr.Field, attr.DefaultVal, attr.MyDataType, false, attr.Desc));
                }
            }
            return attrs;
        }
        #endregion

        #region ���ڻ�������
        public string  _FK_MapData = null;
        public string FK_MapData
        {
            get
            {
                if (_FK_MapData == null)
                    return this.PhysicsTable ;
                return _FK_MapData;
            }
            set
            {
                _FK_MapData = value;
            }
        }
        /// <summary>
        /// ��ʾ��ʽ
        /// </summary>
        private FormShowType _FormShowType = FormShowType.NotSet;
        /// <summary>
        /// ���λ��OfEntity
        /// </summary>
        public FormShowType FormShowType
        {
            get
            {
                return _FormShowType;
            }
            set
            {
                _FormShowType = value;
            }
        }
        /// <summary>
        /// ���λ��
        /// </summary>
        private Depositary _DepositaryOfEntity = Depositary.None;
        /// <summary>
        /// ���λ��OfEntity
        /// </summary>
        public Depositary DepositaryOfEntity
        {
            get
            {
                return _DepositaryOfEntity;
            }
            set
            {
                _DepositaryOfEntity = value;
            }
        }
        /// <summary>
        /// 
        /// </summary>		
        private Depositary _DepositaryOfMap = Depositary.Application;
        /// <summary>
        /// ���λ��
        /// </summary>
        public Depositary DepositaryOfMap
        {
            get
            {
                return _DepositaryOfMap;
            }
            set
            {
                _DepositaryOfMap = value;
            }
        }
        #endregion

        #region ��ѯ���Դ���

        #region ��ö��ֵ�����������ѯ
        private AttrsOfSearch _attrsOfSearch = null;
        /// <summary>
        /// ��������
        /// </summary>
        public AttrsOfSearch AttrsOfSearch
        {
            get
            {
                if (this._attrsOfSearch == null)
                    this._attrsOfSearch = new AttrsOfSearch();
                return this._attrsOfSearch;
            }
        }
        /// <summary>
        /// �õ�ȫ����Attrs
        /// </summary>
        /// <returns></returns>
        public Attrs GetChoseAttrs(Entity en)
        {
            return BP.Sys.CField.GetMyAttrs(en.GetNewEntities,en.EnMap);
        }
        public Attrs GetChoseAttrs(Entities ens)
        {
            return BP.Sys.CField.GetMyAttrs(ens, this);
        }
        #endregion

        #region ����ö��ֵ�������������
        /// <summary>
        /// ���ҵ�attrs 
        /// </summary>
        private AttrSearchs _SearchAttrs = null;
        /// <summary>
        /// ���ҵ�attrs
        /// </summary>
        public AttrSearchs SearchAttrs
        {
            get
            {
                if (this._SearchAttrs == null)
                    this._SearchAttrs = new AttrSearchs();
                return this._SearchAttrs;
            }
        }
        public void AddHidden(string refKey, string symbol, string val)
        {
            AttrOfSearch aos = new AttrOfSearch("K" + this.AttrsOfSearch.Count, refKey, refKey, symbol, val, 0, true);
            this.AttrsOfSearch.Add(aos);
        }
        /// <summary>
        /// �����������.�����������/ö������/boolen.
        /// </summary>
        /// <param name="key">key</param>
        public void AddSearchAttr(string key)
        {
            Attr attr = this.GetAttrByKey(key);
            if (attr.Key == "FK_Dept")
                this.SearchAttrs.Add(attr, false, null);
            else
                this.SearchAttrs.Add(attr, true, null);
        }
        /// <summary>
        /// �����������.�����������/ö������/boolen.
        /// </summary>
        /// <param name="key">��ֵ</param>
        /// <param name="isShowSelectedAll">�Ƿ���ʾȫ��</param>
        /// <param name="relationalDtlKey">�����Ӳ˵��ֶ�</param>
        public void AddSearchAttr(string key, bool isShowSelectedAll, string relationalDtlKey)
        {
            Attr attr = this.GetAttrByKey(key);
            this.SearchAttrs.Add(attr, isShowSelectedAll, relationalDtlKey);
        }
        /// <summary>
        /// �����������.
        /// </summary>
        /// <param name="attr">����</param>
        public void AddSearchAttr_del(Attr attr)
        {
            //if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum
            //    || attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK
            //    || attr.MyDataType == DataType.AppBoolean
            //    || attr.MyDataType == DataType.AppDate
            //    || attr.MyDataType == DataType.AppDateTime)
            //{
            //    this.SearchAttrs.Add(attr, true, this.IsAddRefName);
            //}
            //else
            //{
            //    throw new Exception("@����[" + attr.Key + "," + attr.Desc + "]������ӵ����Ҽ�����.��Ϊ������ö�����������.");
            //}
        }
        #endregion

        #endregion

        #region ��������
        /// <summary>
        /// ȡ���ֶ�
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>field name </returns>
        public string GetFieldByKey(string key)
        {
            return GetAttrByKey(key).Field;
        }
        /// <summary>
        /// ȡ������
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>val</returns>
        public string GetDescByKey(String key)
        {
            return GetAttrByKey(key).Desc;
        }
        /// <summary>
        /// ͨ��һ��key �õ���������ֵ��
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>attr</returns>
        public Attr GetAttrByKey(string key)
        {
            foreach (Attr attr in this.Attrs)
            {
                if (attr.Key.ToUpper() == key.ToUpper())
                {
                    return attr;
                }
            }

            if (key == null)
                throw new Exception("@[" + this.EnDesc + "] ��ȡ����key ֵ����Ϊ��.");


            if (this.ToString().Contains("."))
                throw new Exception("@[" + this.EnDesc + "," + this.PhysicsTable + "] û���ҵ� key=[" + key + "]�����ԣ�����Map�ļ�������������ԭ��֮һ�ǣ�������ϵͳ�е�һ��ʵ������Թ������ʵ�壬���ڸ�ʵ��������Ϣʱû�а��չ�����дreftext, refvalue�����ʵ��");
            else
            {
                throw new Exception("@[" + this.EnDesc + "," + this.PhysicsTable + "] û���ҵ� key=[" + key + "]�����ԣ�����Sys_MapAttr���Ƿ��и�����,��SQLִ��: SELECT * FROM Sys_MapAttr WHERE FK_MapData='" + this.ToString() + "' AND KeyOfEn='" + key + "' �Ƿ���Բ�ѯ�����ݣ����û�п��ܸ��ֶ����Զ�ʧ��");
            }
        }
        /// <summary>
        /// �������.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Attr GetAttrByBindKey(string key)
        {
            foreach (Attr attr in this.Attrs)
            {
                if (attr.UIBindKey == key)
                {
                    return attr;
                }
            }
            if (key == null)
                throw new Exception("@[" + this.EnDesc + "] ��ȡ����key ֵ����Ϊ��.");

            if (this.ToString().Contains("."))
                throw new Exception("@[" + this.EnDesc + "," + this.ToString() + "] û���ҵ� key=[" + key + "]�����ԣ�����Map�ļ�������������ԭ��֮һ�ǣ�������ϵͳ�е�һ��ʵ������Թ������ʵ�壬���ڸ�ʵ��������Ϣʱû�а��չ�����дreftext, refvalue�����ʵ��");
            else
                throw new Exception("@[" + this.EnDesc + "," + this.ToString() + "] û���ҵ� key=[" + key + "]�����ԣ�����Sys_MapAttr���Ƿ��и�����,��SQLִ��: SELECT * FROM Sys_MapAttr WHERE FK_MapData='"+this.ToString()+"' AND KeyOfEn='"+key+"' �Ƿ���Բ�ѯ�����ݣ����û�п��ܸ��ֶ����Զ�ʧ��");
        }
        /// <summary>
        /// ͨ��һ��key �õ���������ֵ��
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>attr</returns>
        public Attr GetAttrByDesc(string desc)
        {
            foreach (Attr attr in this.Attrs)
            {
                if (attr.Desc == desc)
                {
                    return attr;
                }
            }
            if (desc == null)
                throw new Exception("@[" + this.EnDesc + "] ��ȡ���� desc  ֵ����Ϊ��.");

            throw new Exception("@[" + this.EnDesc + "] û���ҵ� desc=[" + desc + "]�����ԣ�����Map�ļ�������������ԭ��֮һ�ǣ�������ϵͳ�е�һ��ʵ������Թ������ʵ�壬���ڸ�ʵ��������Ϣʱû�а��չ�����дreftext, refvalue�����ʵ��");
        }
        #endregion

        #region ��������
        /// <summary>
        /// ȡ������TB��ȡ�
        /// </summary>
        private int _MaxTBLength = 0;
        /// <summary>
        /// ����TB��ȡ�
        /// </summary>
        public float MaxTBLength
        {
            get
            {
                if (_MaxTBLength == 0)
                {
                    foreach (Attr attr in this.Attrs)
                    {
                        if (attr.UIWidth > _MaxTBLength)
                        {
                            _MaxTBLength = (int)attr.UIWidth;
                        }
                    }
                }
                return _MaxTBLength;
            }
        }
        /// <summary>
        /// ������̼���
        /// </summary>
        private Attrs _HisPhysicsAttrs = null;
        /// <summary>
        /// ������̼���
        /// </summary>
        public Attrs HisPhysicsAttrs
        {
            get
            {
                if (_HisPhysicsAttrs == null)
                {
                    _HisPhysicsAttrs = new Attrs();
                    foreach (Attr attr in this.Attrs)
                    {
                        if (attr.MyFieldType == FieldType.NormalVirtual || attr.MyFieldType == FieldType.RefText)
                            continue;
                        _HisPhysicsAttrs.Add(attr, false, this.IsAddRefName);
                    }
                }
                return _HisPhysicsAttrs;
            }
        }
        /// <summary>
        /// �����������
        /// </summary>
        private Attrs _HisFKAttrs = null;
        /// <summary>
        /// �����������
        /// </summary>
        public Attrs HisFKAttrs
        {
            get
            {
                if (_HisFKAttrs == null)
                {
                    _HisFKAttrs = new Attrs();
                    foreach (Attr attr in this.Attrs)
                    {
                        if (attr.MyFieldType == FieldType.FK
                            || attr.MyFieldType == FieldType.PKFK)
                        {
                            _HisFKAttrs.Add(attr, false, false);
                        }
                    }
                }
                return _HisFKAttrs;
            }
        }
        private int _isFull = -1;
        /// <summary>
        /// �Ƿ����Զ�����
        /// </summary>
        public bool IsHaveAutoFull
        {
            get
            {
                if (_isFull == -1)
                {
                    foreach (Attr attr in _attrs)
                    {
                        if (attr.AutoFullDoc != null)
                            _isFull = 1;
                    }
                    if (_isFull == -1)
                        _isFull = 0;
                }
                if (_isFull == 0)
                    return false;
                return true;
            }
        }
        public bool IsHaveFJ=false;
        /// <summary>
        /// �ƶ�����ʾ��ʽ
        /// </summary>
        public string TitleExt = null;
        private int _isJs = -1;
        public bool IsHaveJS
        {
            get
            {
                if (_isJs == -1)
                {
                    foreach (Attr attr in _attrs)
                    {
                        if (attr.AutoFullDoc == null)
                            continue;
                        if (attr.AutoFullWay == AutoFullWay.Way1_JS)
                        {
                            _isJs = 1;
                            break;
                        }
                    }

                    if (_isJs == -1)
                        _isJs = 0;
                }

                if (_isJs == 0)
                    return false;
                return true;
            }
        }
        /// <summary>
        /// �Ƿ���������������
        /// AttrKey -  AttrKeyName 
        /// </summary>
        public bool IsAddRefName = false;
        /// <summary>
        /// �������Enum����
        /// </summary>
        private Attrs _HisEnumAttrs = null;
        /// <summary>
        /// �������Enum����
        /// </summary>
        public Attrs HisEnumAttrs
        {
            get
            {
                if (_HisEnumAttrs == null)
                {
                    _HisEnumAttrs = new Attrs();
                    foreach (Attr attr in this.Attrs)
                    {
                        if (attr.MyFieldType == FieldType.Enum || attr.MyFieldType == FieldType.PKEnum)
                        {
                            _HisEnumAttrs.Add(attr, true, false);
                        }
                    }
                }
                return _HisEnumAttrs;
            }
        }
        /// <summary>
        /// �������EnumandPk����
        /// </summary>
        private Attrs _HisFKEnumAttrs = null;
        /// <summary>
        /// �������EnumandPk����
        /// </summary>
        public Attrs HisFKEnumAttrs
        {
            get
            {
                if (_HisFKEnumAttrs == null)
                {
                    _HisFKEnumAttrs = new Attrs();
                    foreach (Attr attr in this.Attrs)
                    {
                        if (attr.MyFieldType == FieldType.Enum
                            || attr.MyFieldType == FieldType.PKEnum
                            || attr.MyFieldType == FieldType.FK
                            || attr.MyFieldType == FieldType.PKFK)
                        {
                            _HisFKEnumAttrs.Add(attr);
                        }
                    }
                }
                return _HisFKEnumAttrs;
            }
        }
        #endregion

        #region ����ʵ��������Ϣ
        private Attrs _HisCfgAttrs = null;
        public Attrs HisCfgAttrs
        {
            get
            {
                if (this._HisCfgAttrs == null)
                {
                    this._HisCfgAttrs = new Attrs();
                    if (Web.WebUser.No == "admin")
                    {

                        this._HisCfgAttrs.AddDDLSysEnum("UIRowStyleGlo", 2,"��������з��(Ӧ��ȫ��)", true, false, "UIRowStyleGlo", 
                            "@0=�޷��@1=������@2=����ƶ�@3=���沢����ƶ�");

                        this._HisCfgAttrs.AddBoolen("IsEnableDouclickGlo", true,
                             "�Ƿ�����˫����(Ӧ��ȫ��)");

                        this._HisCfgAttrs.AddBoolen("IsEnableFocusField", true, "�Ƿ����ý����ֶ�");
                        this._HisCfgAttrs.AddTBString("FocusField", null, "�����ֶ�(������ʾ����򿪵���",
                            true, false, 0, 20, 20);
                        this._HisCfgAttrs.AddBoolen("IsEnableRefFunc", true, "�Ƿ�������ع�����");
                        this._HisCfgAttrs.AddBoolen("IsEnableOpenICON", true, "�Ƿ����ô�ͼ��");
                        this._HisCfgAttrs.AddDDLSysEnum("MoveToShowWay", 0,"�ƶ�����ʾ��ʽ", true, false,
                            "MoveToShowWay", "@0=����ʾ@1=�����б�@2=ƽ��");
                        this._HisCfgAttrs.AddTBString("MoveTo", null, "�ƶ����ֶ�", true, false, 0, 20, 20);
                        this._HisCfgAttrs.AddTBInt("WinCardW", 820, "�������ڿ��", true, false);
                        this._HisCfgAttrs.AddTBInt("WinCardH", 480, "�������ڸ߶�", true, false);
                        this._HisCfgAttrs.AddDDLSysEnum("EditerType", 0,  "����ı��༭��", 
                            true, false, "EditerType", "@0=��@1=sina�༭��@2=FKCEditer@3=KindEditor@4=UEditor");
                        this._HisCfgAttrs.AddTBString("ShowColumns", "", "ѡ����", false, false, 0, 1000, 100);    //added by liuxc,2015-8-7,����ѡ���д洢�ֶ�
                      //  this._HisCfgAttrs.AddDDLSysEnum("UIRowStyleGlo", 2, "��������з��(Ӧ��ȫ��)", true, false, "UIRowStyleGlo", "@0=�޷��@1=������@2=����ƶ�@3=���沢����ƶ�");
                    }
                }
                return _HisCfgAttrs;
            }
        }
        #endregion

        #region ���Ĺ�����Ϣ.
        private Attrs _HisRefAttrs = null;
        public Attrs HisRefAttrs
        {
            get
            {
                if (this._HisRefAttrs == null)
                {
                    this._HisRefAttrs = new Attrs();

                    foreach (Attr attr in this.Attrs)
                    {
                        if (attr.MyFieldType == FieldType.FK || attr.MyFieldType == FieldType.PKFK)
                        {
                            _HisRefAttrs.Add(attr);
                        }
                    }
                }
                return _HisRefAttrs;
            }
        }
        #endregion

        #region ������ع���
        /// <summary>
        /// ����һ����ع���
        /// </summary>
        /// <param name="title">����</param>
        /// <param name="classMethodName">����</param>
        /// <param name="icon">ͼ��</param>
        /// <param name="tooltip">��ʾ��Ϣ</param>
        /// <param name="target">���ӵ�</param>
        /// <param name="width">���</param>
        /// <param name="height">�߶�</param>
        public void AddRefMethod(string title, string classMethodName, Attrs attrs, string warning, string icon, string tooltip, string target, int width, int height)
        {
            RefMethod func = new RefMethod();
            func.Title = title;
            func.Warning = warning;
            func.ClassMethodName = classMethodName;
            func.Icon = icon;
            func.ToolTip = tooltip;
            func.Width = width;
            func.Height = height;
            func.HisAttrs = attrs;
            this.HisRefMethods.Add(func);
        }
        public void AddRefMethodOpen()
        {
            RefMethod func = new RefMethod();
            func.Title = "��";
            func.ClassMethodName = this.ToString() + ".DoOpenCard";
            func.Icon = "/WF/Img/Btn/Edit.gif";
            this.HisRefMethods.Add(func);
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="func"></param>
        public void AddRefMethod(RefMethod rm)
        {
            this.HisRefMethods.Add(rm);
        }
        #endregion

        #region ����������ϸ��Ϣ
        /// <summary>
        /// ������ϸ
        /// </summary>
        /// <param name="ens">������Ϣ</param>
        /// <param name="refKey">����</param>
        public void AddDtl(Entities ens, string refKey)
        {
            EnDtl dtl = new EnDtl();
            dtl.Ens = ens;
            dtl.RefKey = refKey;
            this.Dtls.Add(dtl);
        }
        /// <summary>
        /// ��ع���s
        /// </summary> 
        private RefMethods _RefMethods = null;
        /// <summary>
        /// ��ع���
        /// </summary>
        public RefMethods HisRefMethods
        {
            get
            {
                if (this._RefMethods == null)
                    _RefMethods = new RefMethods();

                return _RefMethods;
            }
        }
        /// <summary>
        /// ��ϸs
        /// </summary> 
        private EnDtls _Dtls = null;
        /// <summary>
        /// ������ϸ
        /// </summary>
        public EnDtls Dtls
        {
            get
            {
                if (this._Dtls == null)
                    _Dtls = new EnDtls();

                return _Dtls;
            }
        }
        /// <summary>
        /// ���е���ϸ
        /// </summary> 
        private EnDtls _DtlsAll = null;
        /// <summary>
        /// ���е���ϸ
        /// </summary>
        public EnDtls DtlsAll
        {
            get
            {
                if (this._DtlsAll == null)
                {
                    _DtlsAll = this.Dtls;

                    // �������Ķ�ѡ��
                    foreach (AttrOfOneVSM en in this.AttrsOfOneVSM)
                    {
                        EnDtl dtl = new EnDtl();
                        dtl.Ens = en.EnsOfMM;
                        dtl.RefKey = en.AttrOfOneInMM;
                        //dtl.Desc =en.Desc;
                        //dtl.Desc = en.Desc ;
                        _DtlsAll.Add(dtl);
                    }

                }
                return _DtlsAll;
            }
        }
        #endregion

        #region ���캭��
        /// <summary>
        /// ���캭�� 
        /// </summary>
        /// <param name="dburl">���ݿ�����</param>
        /// <param name="physicsTable">����table.</param>
        public Map(DBUrl dburl, string physicsTable)
        {
            this.EnDBUrl = dburl;
            this.PhysicsTable = physicsTable;
        }
        /// <summary>
        /// ���캭��
        /// </summary>
        /// <param name="physicsTable">����table</param>
        public Map(string physicsTable)
        {
            this.PhysicsTable = physicsTable;
        }
        /// <summary>
        /// ���캭��
        /// </summary>
        /// <param name="DBUrlKeyList">���ӵ�Key �������  DBUrlKeyList �õ�</param>
        /// <param name="physicsTable">�����</param>
        public Map(DBUrlType dburltype, string physicsTable)
        {
            this.EnDBUrl = new DBUrl(dburltype);
            this.PhysicsTable = physicsTable;
        }
        /// <summary>
        /// ���캭��
        /// </summary>
        public Map() { }
        #endregion

        #region ����
        /// <summary>
        /// ��Զ�Ĺ���
        /// </summary>
        private AttrsOfOneVSM _AttrsOfOneVSM = new AttrsOfOneVSM();
        /// <summary>
        /// ��Զ�Ĺ���
        /// </summary>
        public AttrsOfOneVSM AttrsOfOneVSM
        {
            get
            {
                if (this._AttrsOfOneVSM == null)
                    this._AttrsOfOneVSM = new AttrsOfOneVSM();
                return this._AttrsOfOneVSM;
            }
            set
            {
                this._AttrsOfOneVSM = value;
            }
        }
        /// <summary>
        /// ͨ����ʵ���������ȡ������OneVSM����.
        /// </summary>
        /// <param name="ensOfMMclassName"></param>
        /// <returns></returns>
        public AttrOfOneVSM GetAttrOfOneVSM(string ensOfMMclassName)
        {
            foreach (AttrOfOneVSM attr in this.AttrsOfOneVSM)
            {
                if (attr.EnsOfMM.ToString() == ensOfMMclassName)
                {
                    return attr;
                }
            }
            throw new Exception("error param:  " + ensOfMMclassName);
        }
        /// <summary>
        /// �ļ�����
        /// </summary>
        private AdjunctType _AdjunctType = AdjunctType.None;
        /// <summary>
        /// �ļ�����
        /// </summary>
        public AdjunctType AdjunctType
        {
            get
            {
                return this._AdjunctType;
            }
            set
            {
                this._AdjunctType = value;
            }
        }
        public string MoveTo = null;
        /// <summary>
        /// ʵ������
        /// </summary>
        string _EnDesc = "";
        public string EnDesc
        {
            get
            {
                return this._EnDesc;
            }
            set
            {
                this._EnDesc = value;
            }
        }
        public bool IsShowSearchKey = true;
        public BP.Sys.DTSearchWay DTSearchWay= BP.Sys.DTSearchWay.None;
        public string  DTSearchKey = null;
        /// <summary>
        /// ͼƬDefaultImageUrl
        /// </summary>
        public string Icon = "../Images/En/Default.gif";
        /// <summary>
        /// ʵ������
        /// </summary>
        EnType _EnType = EnType.App;
        /// <summary>
        /// ʵ������ Ĭ��Ϊ0(�û�Ӧ��).
        /// </summary>
        public EnType EnType
        {
            get
            {
                return this._EnType;
            }
            set
            {
                this._EnType = value;
            }
        }
        #region  �������Ը���xml.
        private string PKs = "";
        public void GenerMap(string xml)
        {
            DataSet ds = new DataSet("");
            ds.ReadXml(xml);
            foreach (DataTable dt in ds.Tables)
            {
                switch (dt.TableName)
                {
                    case "Base":
                        this.DealDT_Base(dt);
                        break;
                    case "Attr":
                        this.DealDT_Attr(dt);
                        break;
                    case "SearchAttr":
                        this.DealDT_SearchAttr(dt);
                        break;
                    case "Dtl":
                        this.DealDT_SearchAttr(dt);
                        break;
                    case "Dot2Dot":
                        this.DealDT_Dot2Dot(dt);
                        break;
                    default:
                        throw new Exception("XML ������Ϣ����û��Լ���ı��:" + dt.TableName);
                }
            }
            // ������õ������ԡ�

        }

        private void DealDT_Base(DataTable dt)
        {
            if (dt.Rows.Count != 1)
                throw new Exception("������Ϣ���ô��󣬲��ܶ��ڻ�������1�м�¼��");
            foreach (DataColumn dc in dt.Columns)
            {
                string val = dt.Rows[0][dc.ColumnName].ToString();
                if (val == null)
                    continue;
                if (dt.Rows[0][dc.ColumnName] == DBNull.Value)
                    continue;

                switch (dc.ColumnName)
                {
                    case "EnDesc":
                        this.EnDesc = val;
                        break;
                    case "Table":
                        this.PhysicsTable = val;
                        break;
                    case "DBUrl":
                        this.EnDBUrl = new DBUrl(DataType.GetDBUrlByString(val));
                        break;
                    case "ICON":
                        this.Icon = val;
                        break;
                    case "CodeStruct":
                        this.CodeStruct = val;
                        break;
                    case "AdjunctType":
                        //this.PhysicsTable=val;
                        break;
                    case "EnType":
                        switch (val)
                        {
                            case "Admin":
                                this.EnType = BP.En.EnType.Admin;
                                break;
                            case "App":
                                this.EnType = BP.En.EnType.App;
                                break;
                            case "Dot2Dot":
                                this.EnType = BP.En.EnType.Dot2Dot;
                                break;
                            case "Dtl":
                                this.EnType = BP.En.EnType.Dtl;
                                break;
                            case "Etc":
                                this.EnType = BP.En.EnType.Etc;
                                break;
                            case "PowerAble":
                                this.EnType = BP.En.EnType.PowerAble;
                                break;
                            case "Sys":
                                this.EnType = BP.En.EnType.Sys;
                                break;
                            case "View":
                                this.EnType = BP.En.EnType.View;
                                break;
                            case "XML":
                                this.EnType = BP.En.EnType.XML;
                                break;
                            default:
                                throw new Exception("û��Լ���ı��:EnType =  " + val);
                        }
                        break;
                    case "DepositaryOfEntity":
                        switch (val)
                        {
                            case "Application":
                                this.DepositaryOfEntity = Depositary.Application;
                                break;
                            case "None":
                                this.DepositaryOfEntity = Depositary.None;
                                break;
                            case "Session":
                                this.DepositaryOfEntity = Depositary.Application;
                                break;
                            default:
                                throw new Exception("û��Լ���ı��:DepositaryOfEntity=[" + val + "] Ӧ��ѡ��Ϊ,Application, None, Session ");
                        }
                        break;
                    case "DepositaryOfMap":
                        switch (val)
                        {
                            case "Application":
                            case "Session":
                                this.DepositaryOfMap = Depositary.Application;
                                break;
                            case "None":
                                this.DepositaryOfMap = Depositary.None;
                                break;
                            default:
                                throw new Exception("û��Լ���ı��:DepositaryOfMap=[" + val + "] Ӧ��ѡ��Ϊ,Application, None, Session ");
                        }
                        break;
                    case "PKs":
                        this.PKs = val;
                        break;
                    default:
                        throw new Exception("������Ϣ��û��Լ���ı��:" + val);
                }
            }
        }
        private void DealDT_Attr(DataTable dt)
        {
            foreach (DataRow dr in dt.Rows)
            {
                Attr attr = new Attr();
                foreach (DataColumn dc in dt.Columns)
                {
                    string val = dr[dc.ColumnName].ToString();
                    switch (dc.ColumnName)
                    {
                        case "Key":
                            attr.Key = val;
                            break;
                        case "Field":
                            attr.Field = val;
                            break;
                        case "DefVal":
                            attr.DefaultVal = val;
                            break;
                        case "DT":
                            attr.MyDataType = DataType.GetDataTypeByString(val);
                            break;
                        case "UIBindKey":
                            attr.UIBindKey = val;
                            break;
                        case "UIIsReadonly":
                            if (val == "1" || val.ToUpper() == "TRUE")
                                attr.UIIsReadonly = true;
                            else
                                attr.UIIsReadonly = false;
                            break;
                        case "MinLen":
                            attr.MinLength = int.Parse(val);
                            break;
                        case "MaxLen":
                            attr.MaxLength = int.Parse(val);
                            break;
                        case "TBLen":
                            attr.UIWidth = int.Parse(val);
                            break;
                        default:
                            throw new Exception("û��Լ���ı��:" + val);
                    }
                }

                // �ж�����.
                if (attr.UIBindKey == null)
                {
                    /* ˵��û�������������ö�����͡�*/
                    //if (attr.MyDataType
                }
                else
                {
                    if (attr.UIBindKey.IndexOf(".") != -1)
                    {
                        /*˵������һ���ࡣ*/
                        Entities ens = attr.HisFKEns;
                        EntitiesNoName ensNoName = ens as EntitiesNoName;
                        if (ensNoName == null)
                        {
                            /*û��ת���ɹ��������*/
                        }
                        else
                        {
                            /*�Ѿ�ת���ɹ�, ˵������EntityNoName ���͡� */
                            if (this.PKs.IndexOf(attr.Key) != -1)
                            {
                                /* �����һ������  */
                                if (attr.Field == "")
                                    attr.Field = attr.Key;
                                this.AddDDLEntitiesPK(attr.Key, attr.Field, attr.DefaultVal.ToString(), attr.Desc, ensNoName, attr.UIIsReadonly);
                            }
                            else
                            {
                                this.AddDDLEntities(attr.Key, attr.Field, attr.DefaultVal.ToString(), attr.Desc, ensNoName, attr.UIIsReadonly);
                            }
                        }

                    }
                    else
                    {
                    }

                }


            }
        }
        private void DealDT_SearchAttr(DataTable dt)
        {
        }
        private void DealDT_Dtl(DataTable dt)
        {
        }
        private void DealDT_Dot2Dot(DataTable dt)
        {
        }
        #endregion

        #region ������No�ִ��й�
        /// <summary>
        /// �����ִ����ֶεĳ��ȡ�
        /// </summary>
        int _GenerNoLength = 0;
        public int GenerNoLength
        {
            get
            {
                if (this._GenerNoLength == 0)
                    throw new Exception("@û��ָ�������ִ����ֶγ��ȡ�");
                return this._GenerNoLength;
            }
            set
            {
                this._GenerNoLength = value;
            }
        }
        /// <summary>
        /// ����ṹ
        /// ���磺 0�� 2322;
        /// </summary>
        string _CodeStruct = "2";
        /// <summary>
        /// ����Ľṹ
        /// </summary>
        public string CodeStruct
        {
            get
            {
                return this._CodeStruct;
            }
            set
            {
                this._CodeStruct = value;
                this.IsAutoGenerNo = true;
            }
        }
        /// <summary>
        /// ��ŵ��ܳ��ȡ�
        /// </summary>
        public int CodeLength
        {
            get
            {
                int i = 0;
                if (CodeStruct.Length == 0)
                {
                    i = int.Parse(this.CodeStruct);
                }
                else
                {
                    char[] s = this.CodeStruct.ToCharArray();
                    foreach (char c in s)
                    {
                        i = i + int.Parse(c.ToString());
                    }
                }

                return i;
            }
        }
        /// <summary>
        /// �Ƿ������ظ�������(Ĭ�ϲ������ظ���)
        /// </summary>
        private bool _IsAllowRepeatName = true;
        /// <summary>
        /// �Ƿ������ظ�������.
        /// ��insert��update ǰ��顣
        /// </summary>
        public bool IsAllowRepeatName
        {
            get
            {
                return _IsAllowRepeatName;
            }
            set
            {
                _IsAllowRepeatName = value;
            }
        }
        /// <summary>
        /// �Ƿ��Զ����
        /// </summary>
        private bool _IsAutoGenerNo = false;
        /// <summary>
        /// �Ƿ��Զ����.		 
        /// </summary>
        public bool IsAutoGenerNo
        {
            get
            {
                return _IsAutoGenerNo;
            }
            set
            {
                _IsAutoGenerNo = value;
            }
        }
        /// <summary>
        /// �Ƿ����ų��ȡ���Ĭ�ϵ�false��
        /// </summary>
        private bool _IsCheckNoLength = false;
        /// <summary>
        /// �Ƿ����ų���.
        /// ��insert ǰ��顣
        /// </summary>
        public bool IsCheckNoLength
        {
            get
            {
                return _IsCheckNoLength;
            }
            set
            {
                _IsCheckNoLength = value;
            }
        }
        #endregion

        #region �������й�ϵ��

        DBUrl _EnDBUrl = null;
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public DBUrl EnDBUrl
        {
            get
            {
                if (this._EnDBUrl == null)
                {
                    _EnDBUrl = new DBUrl();
                }
                return this._EnDBUrl;
            }
            set
            {
                this._EnDBUrl = value;
            }
        }
        private string _PhysicsTable = null;

        public bool IsView
        {
            get
            {
                string sql = "";
                switch (this.EnDBUrl.DBType)
                {
                    case DBType.Oracle:
                        sql = "SELECT TABTYPE  FROM TAB WHERE UPPER(TNAME)=:v";
                        DataTable oradt = DBAccess.RunSQLReturnTable(sql, "v", this.PhysicsTableExt.ToUpper());
                        if (oradt.Rows.Count == 0)
                            throw new Exception("@������[" + this.PhysicsTableExt + "]");
                        if (oradt.Rows[0][0].ToString().ToUpper().Trim() == "V".ToString())
                            return true;
                        else
                            return false;
                        break;
                    case DBType.Access:
                        sql = "select   Type   from   msysobjects   WHERE   UCASE(name)='" + this.PhysicsTableExt.ToUpper() + "'";
                        DataTable dtw = DBAccess.RunSQLReturnTable(sql);
                        if (dtw.Rows.Count == 0)
                            throw new Exception("@������[" + this.PhysicsTableExt + "]");
                        if (dtw.Rows[0][0].ToString().Trim() == "5")
                            return true;
                        else
                            return false;
                    case DBType.MSSQL:
                        sql = "select xtype from sysobjects WHERE name ="+SystemConfig.AppCenterDBVarStr+"v";
                        DataTable dt1 = DBAccess.RunSQLReturnTable(sql, "v", this.PhysicsTableExt);
                        if (dt1.Rows.Count == 0)
                            throw new Exception("@������[" + this.PhysicsTableExt + "]");

                        if (dt1.Rows[0][0].ToString().ToUpper().Trim() == "V".ToString() )
                            return true;
                        else
                            return false;
                    case DBType.Informix:
                        sql = "select tabtype from systables where tabname = '"+this.PhysicsTableExt.ToLower()+"'";
                        DataTable dtaa = DBAccess.RunSQLReturnTable(sql);
                        if (dtaa.Rows.Count == 0)
                            throw new Exception("@������[" + this.PhysicsTableExt + "]");

                        if (dtaa.Rows[0][0].ToString().ToUpper().Trim() == "V")
                            return true;
                        else
                            return false;
                    case DBType.MySQL:
                        sql = "SELECT Table_Type FROM information_schema.TABLES WHERE table_name='" + this.PhysicsTableExt + "' and table_schema='" + SystemConfig.AppCenterDBDatabase + "'";
                        DataTable dt2 = DBAccess.RunSQLReturnTable(sql);
                        if (dt2.Rows.Count == 0)
                            throw new Exception("@������[" + this.PhysicsTableExt + "]");

                        if (dt2.Rows[0][0].ToString().ToUpper().Trim() == "VIEW")
                            return true;
                        else
                            return false;
                    default:
                        throw new Exception("@û�������жϡ�");
                }

                DataTable dt = DBAccess.RunSQLReturnTable(sql, "v", this.PhysicsTableExt.ToUpper());
                if (dt.Rows.Count == 0)
                    throw new Exception("@������[" + this.PhysicsTableExt + "]");

                if (dt.Rows[0][0].ToString() == "VIEW")
                    return true;
                else
                    return false;
            }
        }

        public string PhysicsTableExt
        {
            get
            {
                if (this.PhysicsTable.IndexOf(".") != -1)
                {
                    string[] str = this.PhysicsTable.Split('.');
                    return str[1];
                }
                else
                    return this.PhysicsTable;
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        /// <returns>Table name</returns>
        public string PhysicsTable
        {
            get
            {
                return this._PhysicsTable;
                /*
                if (DBAccess.AppCenterDBType==DBType.Oracle)
                {
                    return ""+this._PhysicsTable+"";
                }
                else
                {
                    return this._PhysicsTable;
                }
                */
            }
            set
            {
                // ��Ϊ��ɵ�select ���������ڴ�,�޸�����ʱ��ҲҪ�޸��ڴ�����ݡ�
                //DA.Cash.AddObj(this.ToString()+"SQL",Depositary.Application,null);

                DA.Cash.RemoveObj(this.ToString() + "SQL", Depositary.Application);
                Cash.RemoveObj("MapOf" + this.ToString(), this.DepositaryOfMap); // RemoveObj

                //DA.Cash.setObj(en.ToString()+"SQL",en.EnMap.DepositaryOfMap) as string;
                this._PhysicsTable = value;
            }
        }
        #endregion

        private Attrs _attrs = null;
        public Attrs Attrs
        {
            get
            {
                if (this._attrs == null)
                    this._attrs = new Attrs();
                return this._attrs;
            }
            set
            {
                if (this._attrs == null)
                    this._attrs = new En.Attrs();

                Attrs myattrs = value;
                foreach (Attr item in myattrs)
                    this._attrs.Add(item);
            }
        }
        #endregion

        #region ��������صĲ���

        #region DDL

        #region �ڰﶨ �̶� ö�������й�ϵ�Ĳ�����
        private void AddDDLFixEnum(string key, string field, int defaultVal, bool IsPK, string desc, DDLShowType showtype, bool isReadonly)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppInt;

            if (IsPK)
                attr.MyFieldType = FieldType.PK;
            else
                attr.MyFieldType = FieldType.Normal;

            attr.Desc = desc;
            attr.UIContralType = UIContralType.DDL;
            attr.UIDDLShowType = showtype;
            attr.UIIsReadonly = isReadonly;
            this.Attrs.Add(attr);
        }
        private void AddDDLFixEnumPK(string key, int defaultVal, string desc, DDLShowType showtype, bool isReadonly)
        {
            this.AddDDLFixEnum(key, key, defaultVal, true, desc, showtype, isReadonly);
        }
        private void AddDDLFixEnumPK(string key, string field, int defaultVal, string desc, DDLShowType showtype, bool isReadonly)
        {
            this.AddDDLFixEnumPK(key, field, defaultVal, desc, showtype, isReadonly);
        }
        private void AddDDLFixEnum(string key, int defaultVal, string desc, DDLShowType showtype, bool isReadonly)
        {
            this.AddDDLFixEnum(key, key, defaultVal, false, desc, showtype, isReadonly);
        }
        private void AddBoolean_del(string key, int defaultVal, string desc, bool isReadonly)
        {
            this.AddDDLFixEnum(key, key, defaultVal, false, desc, DDLShowType.Boolean, isReadonly);
        }
        private void AddBoolean_del(string key, string field, int defaultVal, string desc, bool isReadonly)
        {
            this.AddDDLFixEnum(key, field, defaultVal, false, desc, DDLShowType.Boolean, isReadonly);
        }
        #endregion

        #region  ��boolen �й�ϵ�Ĳ���.
        public void AddBoolean(string key,  bool defaultVal, string desc, bool isUIVisable, bool isUIEnable, bool isLine, string helpUrl)
        {
            AddBoolean(key, key, defaultVal, desc, isUIVisable, isUIEnable, isLine, null);
        }
        public void AddBoolean(string key, string field, bool defaultVal, string desc, bool isUIVisable, bool isUIEnable, bool isLine)
        {
            AddBoolean(key, field, defaultVal, desc, isUIVisable, isUIEnable, isLine, null);
        }
        /// <summary>
        /// ������boolen �й�ϵ�Ĳ���.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">field</param>
        /// <param name="defaultVal">defaultVal</param>
        /// <param name="desc">desc</param>
        /// <param name="isUIEnable">isUIEnable</param>
        /// <param name="isUIVisable">isUIVisable</param>
        public void AddBoolean(string key, string field, bool defaultVal, string desc, bool isUIVisable, bool isUIEnable, bool isLine, string helpUrl)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.HelperUrl = helpUrl;

            if (defaultVal)
                attr.DefaultVal = 1;
            else
                attr.DefaultVal = 0;

            attr.MyDataType = DataType.AppBoolean;
            attr.Desc = desc;
            attr.UIContralType = UIContralType.CheckBok;
            attr.UIIsReadonly = isUIEnable;
            attr.UIVisible = isUIVisable;
            attr.UIIsLine = isLine;
            this.Attrs.Add(attr);
        }
        /// <summary>
        /// ������boolen �й�ϵ�Ĳ���.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">field</param>
        /// <param name="defaultVal">defaultVal</param>
        /// <param name="desc">desc</param>
        /// <param name="isUIEnable">isUIEnable</param>
        /// <param name="isUIVisable">isUIVisable</param>
        public void AddBoolean(string key, bool defaultVal, string desc, bool isUIVisable, bool isUIEnable)
        {
            AddBoolean(key, key, defaultVal, desc, isUIVisable, isUIEnable,false);
        }

        /// <summary>
        /// ������boolen �й�ϵ�Ĳ���.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="field">field</param>
        /// <param name="defaultVal">defaultVal</param>
        /// <param name="desc">desc</param>
        /// <param name="isUIEnable">isUIEnable</param>
        /// <param name="isUIVisable">isUIVisable</param>
        public void AddBoolean(string key, bool defaultVal, string desc, bool isUIVisable, bool isUIEnable, bool isLine)
        {
            AddBoolean(key, key, defaultVal, desc, isUIVisable, isUIEnable, isLine);
        }


        #endregion

        #region �ڰﶨ�Զ���,ö�������й�ϵ�Ĳ�����
        public void AddDDLSysEnumPK(string key, string field, int defaultVal, string desc, bool isUIVisable, bool isUIEnable, string sysEnumKey)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppInt;
            attr.MyFieldType = FieldType.PKEnum;
            attr.Desc = desc;
            attr.UIContralType = UIContralType.DDL;
            attr.UIDDLShowType = DDLShowType.SysEnum;
            attr.UIBindKey = sysEnumKey;
            attr.UIVisible = isUIVisable;
            attr.UIIsReadonly = isUIEnable;
            this.Attrs.Add(attr);
        }
        public void AddDDLSysEnum(string key, string field, int defaultVal, string desc, bool isUIVisable, bool isUIEnable, string sysEnumKey, string cfgVal, bool isLine)
        {
            AddDDLSysEnum(  key,   field,   defaultVal,   desc,   isUIVisable,   isUIEnable,   sysEnumKey,   cfgVal,   isLine, null);
        }
        /// <summary>
        /// �Զ���ö������
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="field">�ֶ�</param>
        /// <param name="defaultVal">Ĭ��</param>
        /// <param name="desc">����</param>
        /// <param name="sysEnumKey">Key</param>
        public void AddDDLSysEnum(string key, string field, int defaultVal, string desc, bool isUIVisable, bool isUIEnable, string sysEnumKey, string cfgVal, bool isLine, string helpUrl)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.HelperUrl = helpUrl;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppInt;
            attr.MyFieldType = FieldType.Enum;
            attr.Desc = desc;
            attr.UIContralType = UIContralType.DDL;
            attr.UIDDLShowType = DDLShowType.SysEnum;
            attr.UIBindKey = sysEnumKey;
            attr.UITag = cfgVal;
            attr.UIVisible = isUIVisable;
            attr.UIIsReadonly = isUIEnable;
            attr.UIIsLine = isLine;
            this.Attrs.Add(attr);
        }
        /// <summary>
        /// �Զ���ö������
        /// </summary>
        /// <param name="key">��</param>		
        /// <param name="defaultVal">Ĭ��</param>
        /// <param name="desc">����</param>
        /// <param name="sysEnumKey">Key</param>
        public void AddDDLSysEnum(string key, int defaultVal, string desc, bool isUIVisable, bool isUIEnable, string sysEnumKey)
        {
            AddDDLSysEnum(key, key, defaultVal, desc, isUIVisable, isUIEnable, sysEnumKey, null,false);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="defaultVal"></param>
        /// <param name="desc"></param>
        /// <param name="isUIVisable"></param>
        /// <param name="isUIEnable"></param>
        /// <param name="sysEnumKey"></param>
        /// <param name="cfgVal"></param>
        public void AddDDLSysEnum(string key, int defaultVal, string desc, bool isUIVisable, bool isUIEnable, string sysEnumKey, string cfgVal,bool isLine)
        {
            AddDDLSysEnum(key, key, defaultVal, desc, isUIVisable, isUIEnable, sysEnumKey, cfgVal, isLine);
        }
        public void AddDDLSysEnum(string key, int defaultVal, string desc, bool isUIVisable, bool isUIEnable, string sysEnumKey, string cfgVal)
        {
            AddDDLSysEnum(key, key, defaultVal, desc, isUIVisable, isUIEnable, sysEnumKey, cfgVal,false);
        }
        public void AddDDLSysEnum(string key, int defaultVal, string desc, bool isUIVisable, bool isUIEnable)
        {
            AddDDLSysEnum(key, key, defaultVal, desc, isUIVisable, isUIEnable, key, null, false);
        }
        #endregion


        #region �ڰﶨ�Զ���,ö�������й�ϵ�Ĳ�����
        /// <summary>
        /// �Զ���ö������
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="field">�ֶ�</param>
        /// <param name="defaultVal">Ĭ��</param>
        /// <param name="desc">����</param>
        /// <param name="sysEnumKey">Key</param>
        public void AddRadioBtnSysEnum(string key, string field, int defaultVal, string desc, bool isUIVisable, bool isUIEnable, string sysEnumKey)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppInt;
            attr.MyFieldType = FieldType.Enum;
            attr.Desc = desc;
            attr.UIContralType = UIContralType.RadioBtn;
            attr.UIDDLShowType = DDLShowType.Self;
            attr.UIBindKey = sysEnumKey;
            attr.UIVisible = isUIVisable;
            attr.UIIsReadonly = isUIEnable;
            this.Attrs.Add(attr);
        }
        /// <summary>
        /// �Զ���ö������
        /// </summary>
        /// <param name="key">��</param>		
        /// <param name="defaultVal">Ĭ��</param>
        /// <param name="desc">����</param>
        /// <param name="sysEnumKey">Key</param>
        public void AddRadioBtnSysEnum(string key, int defaultVal, string desc, bool isUIVisable, bool isUIEnable, string sysEnumKey)
        {
            AddDDLSysEnum(key, key, defaultVal, desc, isUIVisable, isUIEnable, sysEnumKey, null,false);
        }
        #endregion



        #region ��ʵ���ɹ�ϵ�Ĳ�����

        #region entityNoName
        public void AddDDLEntities(string key, string defaultVal, string desc, EntitiesSimpleTree ens, bool uiIsEnable)
        {
            this.AddDDLEntities(key, key, defaultVal, DataType.AppString, desc, ens, "No", "Name", uiIsEnable);
        }
        public void AddDDLEntities(string key, string defaultVal, string desc, EntitiesTree ens, bool uiIsEnable)
        {
            this.AddDDLEntities(key, key, defaultVal, DataType.AppString, desc, ens, "No", "Name", uiIsEnable);
        }
        public void AddDDLEntities(string key, string defaultVal, string desc, EntitiesNoName ens, bool uiIsEnable)
        {
            this.AddDDLEntities(key, key, defaultVal, DataType.AppString, desc, ens, "No", "Name", uiIsEnable);
        }
        public void AddDDLEntities(string key, string field, string defaultVal, string desc, EntitiesNoName ens, bool uiIsEnable)
        {
            this.AddDDLEntities(key, field, defaultVal, DataType.AppString, desc, ens, "No", "Name", uiIsEnable);
        }
        #endregion

        #region EntitiesOIDName
        public void AddDDLEntities(string key, int defaultVal, string desc, EntitiesOIDName ens, bool uiIsEnable)
        {
            this.AddDDLEntities(key, key, defaultVal, DataType.AppInt, desc, ens, "OID", "Name", uiIsEnable);
        }
        public void AddDDLEntities(string key, string field, object defaultVal, string desc, EntitiesOIDName ens, bool uiIsEnable)
        {
            this.AddDDLEntities(key, field, defaultVal, DataType.AppInt, desc, ens, "OID", "Name", uiIsEnable);
        }
        #endregion

        /// <summary>
        /// ��ʵ���й�ϵ�Ĳ�����
        /// </summary>
        /// <param name="key">��ֵ</param>
        /// <param name="field">�ֶ�</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="dataType">DataType����</param>
        /// <param name="desc">����</param>
        /// <param name="ens">ʵ�弯��</param>
        /// <param name="refKey">�����Ľ�</param>
        /// <param name="refText">������Text</param>
        private void AddDDLEntities(string key, string field, object defaultVal, int dataType, FieldType _fildType, string desc, Entities ens, string refKey, string refText, bool uiIsEnable)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = dataType;
            attr.MyFieldType = _fildType;
            attr.MaxLength = 50;

            attr.Desc = desc;
            attr.UIContralType = UIContralType.DDL;
            attr.UIDDLShowType = DDLShowType.Ens;
            attr.UIBindKey = ens.ToString();
            // attr.UIBindKeyOfEn = ens.GetNewEntity.ToString();

            attr.HisFKEns = ens;


            attr.HisFKEns = ens;
            attr.UIRefKeyText = refText;
            attr.UIRefKeyValue = refKey;
            attr.UIIsReadonly = uiIsEnable;

            this.Attrs.Add(attr, true, this.IsAddRefName);
        }
        public void AddDDLEntities(string key, string field, object defaultVal, int dataType, string desc, Entities ens, string refKey, string refText, bool uiIsEnable)
        {
            AddDDLEntities(key, field, defaultVal, dataType, FieldType.FK, desc, ens, refKey, refText, uiIsEnable);
        }
        /// <summary>
        /// ��ʵ���й�ϵ�Ĳ������ֶ�������������ͬ��
        /// </summary>
        /// <param name="key">��ֵ</param>
        /// <param name="field">�ֶ�</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="dataType">DataType����</param>
        /// <param name="desc">����</param>
        /// <param name="ens">ʵ�弯��</param>
        /// <param name="refKey">�����Ľ�</param>
        /// <param name="refText">������Text</param>
        public void AddDDLEntities(string key, object defaultVal, int dataType, string desc, Entities ens, string refKey, string refText, bool uiIsEnable)
        {
            AddDDLEntities(key, key, defaultVal, dataType, desc, ens, refKey, refText, uiIsEnable);
        }
        public void AddDDLEntitiesPK(string key, object defaultVal, int dataType, string desc, EntitiesTree ens, bool uiIsEnable)
        {
            AddDDLEntities(key, key, defaultVal, dataType, FieldType.PKFK, desc, ens, "No", "Name", uiIsEnable);
        }
        public void AddDDLEntitiesPK(string key, object defaultVal, int dataType, string desc, Entities ens, string refKey, string refText, bool uiIsEnable)
        {
            AddDDLEntities(key, key, defaultVal, dataType, FieldType.PKFK, desc, ens, refKey, refText, uiIsEnable);
        }
        public void AddDDLEntitiesPK(string key, string field, object defaultVal, int dataType, string desc, Entities ens, string refKey, string refText, bool uiIsEnable)
        {
            AddDDLEntities(key, field, defaultVal, dataType, FieldType.PKFK, desc, ens, refKey, refText, uiIsEnable);
        }

        #region ����EntitiesNoName �й�ϵ�Ĳ�����
        /// <summary>
        /// ����EntitiesNoName �й�ϵ�Ĳ���
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="defaultVal"></param>
        /// <param name="desc"></param>
        /// <param name="ens"></param>
        /// <param name="uiIsEnable"></param>
        public void AddDDLEntitiesPK(string key, string field, string defaultVal, string desc, EntitiesTree ens, bool uiIsEnable)
        {
            AddDDLEntities(key, field, (object)defaultVal, DataType.AppString, FieldType.PKFK, desc, ens, "No", "Name", uiIsEnable);
        }
        public void AddDDLEntitiesPK(string key, string field, string defaultVal, string desc, EntitiesNoName ens, bool uiIsEnable)
        {
            AddDDLEntities(key, field, (object)defaultVal, DataType.AppString, FieldType.PKFK, desc, ens, "No", "Name", uiIsEnable);
        }
        public void AddDDLEntitiesPK(string key, string defaultVal, string desc, EntitiesNoName ens, bool uiIsEnable)
        {
            AddDDLEntitiesPK(key, key, defaultVal, desc, ens, uiIsEnable);
        }
        public void AddDDLEntitiesPK(string key, string defaultVal, string desc, EntitiesTree ens, bool uiIsEnable)
        {
            AddDDLEntitiesPK(key, key, defaultVal, desc, ens, uiIsEnable);
        }
        public void AddDDLEntitiesPK(string key, string defaultVal, string desc, EntitiesSimpleTree ens, bool uiIsEnable)
        {
            AddDDLEntitiesPK(key, key, defaultVal, DataType.AppString,  desc, ens, "No", "Name", uiIsEnable);
        }
        #endregion

        #endregion






        #endregion

        #region TB

        #region string �й�ϵ�Ĳ�����

        #region ����
        protected void AddTBString(string key, string field, object defaultVal, FieldType _FieldType, TBType tbType, string desc, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith, bool isUILine)
        {
            AddTBString(key, field, defaultVal, _FieldType, tbType, desc, uiVisable, isReadonly, minLength, maxLength, tbWith, isUILine, null);
        }
        protected void AddTBString(string key, string field, object defaultVal, FieldType _FieldType, TBType tbType, string desc, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith, bool isUILine,string helpUrl)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.HelperUrl = helpUrl;

            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppString;
            attr.Desc = desc;
            attr.UITBShowType = tbType;
            attr.UIVisible = uiVisable;
            attr.UIWidth = tbWith;
            attr.UIIsReadonly = isReadonly;
            attr.MaxLength = maxLength;
            attr.MinLength = minLength;
            attr.MyFieldType = _FieldType;
            attr.UIIsLine = isUILine;
            this.Attrs.Add(attr);
        }
        #endregion

        #region �����ġ�
        /// <summary>
        /// ͬ������ʵ������.
        /// </summary>
        public void AddAttrsFromMapData()
        {
            if (string.IsNullOrEmpty(this.FK_MapData))
                throw new Exception("@��û��Ϊmap�� FK_MapData ��ֵ.");

            MapData md = null;
            md = new MapData();
            md.No = this.FK_MapData;
            if (md.RetrieveFromDBSources() == 0)
            {
                md.Name = this.FK_MapData;
                md.PTable = this.PhysicsTable;
                md.EnPK = this.PKs;
                md.Insert();
                md.RepairMap();
            }
            md.Retrieve();
            BP.Sys.MapAttrs attrs = new BP.Sys.MapAttrs(this.FK_MapData);

            /*�� �ֹ���д��attr ���� mapattrs����ȥ. */
            foreach (Attr attr in this.Attrs)
            {
                if (attrs.Contains(BP.Sys.MapAttrAttr.KeyOfEn, attr.Key))
                    continue;

                if (attr.IsRefAttr)
                    continue;

                //���ļ�ʵ��������Է����ϵʵ������ȥ��
                BP.Sys.MapAttr mapattrN = attr.ToMapAttr;
                mapattrN.FK_MapData = this.FK_MapData;
                if (mapattrN.UIHeight == 0)
                    mapattrN.UIHeight = 23;
                mapattrN.Insert();
                attrs.AddEntity(mapattrN);
            }

            //�ѹ�ϵʵ��������Է����ļ�ʵ������ȥ��
            foreach (BP.Sys.MapAttr attr in attrs)
            {
                if (this.Attrs.Contains(attr.KeyOfEn) == true)
                    continue;
                this.AddAttr(attr.HisAttr);
            }
        }
        public void AddAttrs(Attrs attrs)
        {
            foreach (Attr attr in attrs)
            {
                if (attr.IsRefAttr)
                    continue;
                this.Attrs.Add(attr);
            }
        }
        public void AddAttr(Attr attr)
        {
            this.Attrs.Add(attr);
        }
        public void AddAttr(string key, object defaultVal, int dbtype, bool isPk, string desc)
        {
            if (isPk)
                AddTBStringPK(key, key, desc, true, false, 0, 1000, 100);
            else
                AddTBString(key, key, defaultVal.ToString(), FieldType.Normal, TBType.TB, desc, true, false, 0, 1000, 100,false);
        }
        /// <summary>
        /// ����һ��textbox ���͵����ԡ�
        /// </summary>
        /// <param name="key">��ֵ</param>
        /// <param name="field">�ֶ�ֵ</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="_FieldType">�ֶ�����</param>
        /// <param name="desc">����</param>
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="uiVisable">�ǲ���ֻ��</param>
        /// <param name="minLength">��С����</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <param name="tbWith">���</param> 
        public void AddTBString(string key, string defaultVal, string desc, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            AddTBString(key, key, defaultVal, FieldType.Normal, TBType.TB, desc, uiVisable, isReadonly, minLength, maxLength, tbWith,false);
        }
        public void AddTBString(string key, string field, object defaultVal, string desc, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            AddTBString(key, field, defaultVal, FieldType.Normal, TBType.TB, desc, uiVisable, isReadonly, minLength, maxLength, tbWith,false);
        }
        public void AddTBString(string key, string defaultVal, string desc, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith,bool isUILine)
        {
            AddTBString(key, key, defaultVal, FieldType.Normal, TBType.TB, desc, uiVisable, isReadonly, minLength, maxLength, tbWith, isUILine);
        }
        public void AddTBString(string key, string defaultVal, string desc, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith, bool isUILine,string helpUrl)
        {
            AddTBString(key, key, defaultVal, FieldType.Normal, TBType.TB, desc, uiVisable, isReadonly, minLength, maxLength, tbWith, isUILine, helpUrl);
        }
        /// <summary>
        /// ��������
        /// </summary>
        public void AddMyFileS()
        {
            this.AddTBInt(EntityNoMyFileAttr.MyFileNum, 0, "����", false, false);
            this.IsHaveFJ = true;
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="desc"></param>
        public void AddMyFileS(string desc)
        {
            this.AddTBInt(EntityNoMyFileAttr.MyFileNum, 0, desc, false, false);
            this.IsHaveFJ = true;
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        public void AddMyFile()
        {
            this.AddTBString(EntityNoMyFileAttr.MyFileName, null, "������ͼƬ", false, false, 0, 100, 200);
            this.AddTBString(EntityNoMyFileAttr.MyFilePath, null, "MyFilePath", false, false, 0, 100, 200);
            this.AddTBString(EntityNoMyFileAttr.MyFileExt, null, "MyFileExt", false, false, 0, 10, 10);
            this.AddTBString(EntityNoMyFileAttr.WebPath, null, "WebPath", false, false, 0, 200, 10);

            this.AddTBInt(EntityNoMyFileAttr.MyFileH, 0, "MyFileH", false, false);
            this.AddTBInt(EntityNoMyFileAttr.MyFileW, 0, "MyFileW", false, false);
            this.AddTBFloat("MyFileSize", 0, "MyFileSize", false, false);
            this.IsHaveFJ = true;
        }
        /// <summary>
        /// ����һ������
        /// </summary>
        /// <param name="fileDesc">����</param>
        public void AddMyFile(string fileDesc)
        {
            this.AddTBString(EntityNoMyFileAttr.MyFileName, null, fileDesc, false, false, 0, 100, 200);
            this.AddTBString(EntityNoMyFileAttr.MyFilePath, null, "MyFilePath", false, false, 0, 100, 200);
            this.AddTBString(EntityNoMyFileAttr.MyFileExt, null, "MyFileExt", false, false, 0, 10, 10);
            this.AddTBString(EntityNoMyFileAttr.WebPath, null, "WebPath", false, false, 0, 200, 10);
            this.AddTBInt(EntityNoMyFileAttr.MyFileH, 0, "MyFileH", false, false);
            this.AddTBInt(EntityNoMyFileAttr.MyFileW, 0, "MyFileW", false, false);
            this.AddTBFloat("MyFileSize", 0, "MyFileSize", false, false);
            this.IsHaveFJ = true;
        }
        private AttrFiles _HisAttrFiles = null;
        public AttrFiles HisAttrFiles
        {
            get
            {
                if (_HisAttrFiles == null)
                    _HisAttrFiles = new AttrFiles();
                return _HisAttrFiles;
            }
        }
        /// <summary>
        /// ����һ���ض��ĸ���,�������������Ӷ����
        /// ���磺���Ӽ������������ġ�
        /// </summary>
        /// <param name="fileDesc"></param>
        /// <param name="fExt"></param>
        public void AddMyFile(string fileDesc, string fExt)
        {
            HisAttrFiles.Add(fExt, fileDesc);
            this.IsHaveFJ = true;
        }

        #region ���Ӵ���ı�����
        public void AddTBStringDoc()
        {
            AddTBStringDoc("Doc", "Doc", null, "����", true, false, 0, 4000, 300, 300,true);
        }
        public void AddTBStringDoc(string key, string defaultVal, string desc, bool uiVisable, bool isReadonly,bool isUILine)
        {
            AddTBStringDoc(key, key, defaultVal, desc, uiVisable, isReadonly, 0, 4000, 300, 300, isUILine);
        }
        public void AddTBStringDoc(string key, string defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            AddTBStringDoc(key, key, defaultVal, desc, uiVisable, isReadonly, 0, 4000, 300, 300, false);
        }
        public void AddTBStringDoc(string key, string defaultVal, string desc, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith, int rows)
        {
            AddTBStringDoc(key, key, defaultVal, desc, uiVisable, isReadonly, minLength, maxLength, tbWith, rows,false);
        }
        public void AddTBStringDoc(string key, string field, string defaultVal, string desc, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith, int rows, bool isUILine)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppString;
            attr.Desc = desc;
            attr.UITBShowType = TBType.TB;
            attr.UIVisible = uiVisable;
            attr.UIWidth = 300;
            attr.UIIsReadonly = isReadonly;
            attr.MaxLength = 4000;
            attr.MinLength = minLength;
            attr.MyFieldType = FieldType.Normal;
            attr.UIHeight = rows;
            attr.UIIsLine = isUILine;
            this.Attrs.Add(attr);
        }
        #endregion

        #region  PK
        public void AddTBStringPK(string key, string defaultVal, string desc, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            this.PKs = key;
            AddTBString(key, key, defaultVal, FieldType.PK, TBType.TB, desc, uiVisable, isReadonly, minLength, maxLength, tbWith,false);
        }
        public void AddTBStringPK(string key, string field, object defaultVal, string desc, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            this.PKs = key;
            AddTBString(key, field, defaultVal, FieldType.PK, TBType.TB, desc, uiVisable, isReadonly, minLength, maxLength, tbWith,false);
        }
        #endregion

        #region PKNo

        #endregion

        #region  ����� Ens �й�ϵ�Ĳ�����
        /// <summary>
        /// ����� Ens �й�ϵ�Ĳ�����
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="field">�ֶ�</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="ens">ʵ��</param>		 
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        /// <param name="minLength">��С����</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <param name="tbWith">���</param>
        public void AddTBStringFKEns(string key, string field, string defaultVal, string desc, Entities ens, string refKey, string refText, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            Attr attr = new Attr();
            attr.Key = key;

            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppString;
            attr.UIBindKey = ens.ToString();
            attr.HisFKEns = ens;
            // attr.UIBindKeyOfEn = ens.GetNewEntity.ToString();

            attr.Desc = desc;
            attr.UITBShowType = TBType.Ens;
            attr.UIVisible = uiVisable;
            attr.UIWidth = tbWith;
            attr.UIIsReadonly = isReadonly;
            attr.MaxLength = maxLength;
            attr.MinLength = minLength;
            attr.UIRefKeyValue = refKey;
            attr.UIRefKeyText = refText;
            attr.MyFieldType = FieldType.FK;
            this.Attrs.Add(attr);
        }
        /// <summary>
        /// ����� Ens �й�ϵ�Ĳ�����
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="ens">ʵ��</param>		 
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        /// <param name="minLength">��С����</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <param name="tbWith">���</param>
        public void AddTBStringFKEns(string key, string defaultVal, string desc, Entities ens, string refKey, string refText, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            this.AddTBStringFKEns(key, key, defaultVal, desc, ens, refKey, refText, uiVisable, isReadonly, minLength, maxLength, tbWith);
        }
        #endregion

        #region �ڶ�ֵ�й�ϵ�Ĳ���
        /// <summary>
        /// �ڶ�ֵ�й�ϵ�Ĳ���
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="defaultVal"></param>
        /// <param name="desc"></param>
        /// <param name="ens"></param>
        /// <param name="uiVisable"></param>
        /// <param name="isReadonly"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="tbWith"></param>
        public void AddTBMultiValues(string key, string field, object defaultVal, string desc, Entities ens, string refValue, string refText, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppString;
            attr.UIBindKey = ens.ToString();
            attr.HisFKEns = ens;

            // attr.UIBindKeyOfEn = ens.GetNewEntity.ToString();

            attr.Desc = desc;
            attr.UITBShowType = TBType.Ens;
            attr.UIVisible = uiVisable;
            attr.UIWidth = tbWith;
            attr.UIIsReadonly = isReadonly;
            attr.UIRefKeyText = refText;
            attr.UIRefKeyValue = refValue;
            attr.MaxLength = maxLength;
            attr.MinLength = minLength;
            attr.MyFieldType = FieldType.MultiValues;

            this.Attrs.Add(attr);
        }
        #endregion

        #region  ������ Ens �й�ϵ�Ĳ�����
        /// <summary>
        /// ����� Ens �й�ϵ�Ĳ�����
        /// ����
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="field">�ֶ�</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="ens">ʵ��</param>		 
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        /// <param name="minLength">��С����</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <param name="tbWith">���</param>
        public void AddTBStringPKEns(string key, string field, object defaultVal, string desc, Entities ens, string refVal, string refText, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppString;
            attr.UIBindKey = ens.ToString();
            attr.HisFKEns = attr.HisFKEns;
            //attr.UIBindKeyOfEn = ens.GetNewEntity.ToString();
            attr.Desc = desc;
            attr.UITBShowType = TBType.Ens;
            attr.UIVisible = uiVisable;
            attr.UIWidth = tbWith;
            attr.UIIsReadonly = isReadonly;

            attr.UIRefKeyText = refText;
            attr.UIRefKeyValue = refVal;

            attr.MaxLength = maxLength;
            attr.MinLength = minLength;
            attr.MyFieldType = FieldType.PKFK;
            this.Attrs.Add(attr);
        }
        /// <summary>
        /// ����� Ens �й�ϵ�Ĳ�����
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="ens">ʵ��</param>		 
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        /// <param name="minLength">��С����</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <param name="tbWith">���</param>
        public void AddTBStringPKEns(string key, string defaultVal, string desc, Entities ens, string refKey, string refText, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            this.AddTBStringPKEns(key, key, defaultVal, desc, ens, refKey, refText, uiVisable, isReadonly, minLength, maxLength, tbWith);
        }
        #endregion

        #region  ������ DataHelpKey �й�ϵ�Ĳ�����
        /// <summary>
        /// ����� DataHelpKey �й�ϵ�Ĳ���, �����Զ�����Ҽ�����ϵͳ.
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="field">�ֶ�</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="DataHelpKey"> ��TB �ﶨ����ҽ�����Key </param></param>		 
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        /// <param name="minLength">��С����</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <param name="tbWith">���</param>
        public void AddTBStringPKSelf(string key, string field, object defaultVal, string desc, string DataHelpKey, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppString;
            attr.UIBindKey = DataHelpKey;
            attr.Desc = desc;
            attr.UITBShowType = TBType.Self;
            attr.UIVisible = uiVisable;
            attr.UIWidth = tbWith;
            attr.UIIsReadonly = isReadonly;
            attr.MaxLength = maxLength;
            attr.MinLength = minLength;
            attr.MyFieldType = FieldType.PK;
            this.Attrs.Add(attr);
        }
        /// <summary>
        /// ����� Ens �й�ϵ�Ĳ����������Զ�����Ҽ�����ϵͳ.
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="DataHelpKey"> ��TB �ﶨ����ҽ�����Key </param></param>
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        /// <param name="minLength">��С����</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <param name="tbWith">���</param>
        public void AddTBStringPKSelf(string key, object defaultVal, string desc, string DataHelpKey, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            this.AddTBStringPKSelf(key, key, defaultVal, desc, DataHelpKey, uiVisable, isReadonly, minLength, maxLength, tbWith);
        }
        #endregion

        #region  ����� DataHelpKey �й�ϵ�Ĳ�����
        /// <summary>
        /// ����� DataHelpKey �й�ϵ�Ĳ����������Զ�����Ҽ�����ϵͳ.
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="field">�ֶ�</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="DataHelpKey"> ��TB �ﶨ����ҽ�����Key </param></param>		 
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        /// <param name="minLength">��С����</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <param name="tbWith">���</param>
        public void AddTBStringFKSelf(string key, string field, object defaultVal, string desc, string DataHelpKey, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppString;
            attr.UIBindKey = DataHelpKey;
            attr.Desc = desc;
            attr.UITBShowType = TBType.Self;
            attr.UIVisible = uiVisable;
            attr.UIWidth = tbWith;
            attr.UIIsReadonly = isReadonly;
            attr.MaxLength = maxLength;
            attr.MinLength = minLength;
            attr.MyFieldType = FieldType.Normal;
            this.Attrs.Add(attr);
        }
        /// <summary>
        /// ����� Ens �й�ϵ�Ĳ��������� Ens �Ҽ�����ϵͳ.
        /// </summary>
        /// <param name="key">����</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="DataHelpKey"> ��TB �ﶨ����ҽ�����Key </param></param>
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        /// <param name="minLength">��С����</param>
        /// <param name="maxLength">��󳤶�</param>
        /// <param name="tbWith">���</param>
        public void AddTBStringFKSelf(string key, object defaultVal, string desc, string DataHelpKey, bool uiVisable, bool isReadonly, int minLength, int maxLength, int tbWith)
        {
            this.AddTBStringFKSelf(key, key, defaultVal, desc, DataHelpKey, uiVisable, isReadonly, minLength, maxLength, tbWith);
        }
        #endregion

        #region  �������ֲ
        public void AddTBStringFKValue(string refKey, string key, string desc, bool IsVisable, int with)
        {

        }

        #endregion

        #endregion

        #endregion

        #region ��������
        public void AddTBDate(string key)
        {
            switch (key)
            {
                case "RDT":
                    AddTBDate("RDT", "��¼����", true, true);
                    break;
                case "UDT":
                    AddTBDate("UDT", "��������", true, true);
                    break;
                default:
                    AddTBDate(key, key, true, true);
                    break;
            }
        }
        /// <summary>
        /// �����������͵Ŀؽ�
        /// </summary>
        /// <param name="key">��ֵ</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        public void AddTBDate(string key, string field, string defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppDate;
            attr.Desc = desc;
            attr.UITBShowType = TBType.Date;
            attr.UIVisible = uiVisable;
            attr.UIIsReadonly = isReadonly;
            attr.MaxLength = 50;
            this.Attrs.Add(attr);
        }
        /// <summary>
        /// �����������͵Ŀؽ�
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="defaultVal">defaultVal/��������õ������Ϣ,��ѡ�����ķ�������</param>
        /// <param name="desc">desc</param>
        /// <param name="uiVisable">uiVisable</param>
        /// <param name="isReadonly">isReadonly</param>
        public void AddTBDate(string key, string defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            AddTBDate(key, key, defaultVal, desc, uiVisable, isReadonly);
        }
        /// <summary>
        /// �����������͵Ŀؽ�(Ĭ�������ǵ�ǰ����)
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="desc">desc</param>
        /// <param name="uiVisable">uiVisable</param>
        /// <param name="isReadonly">isReadonly</param>
        public void AddTBDate(string key, string desc, bool uiVisable, bool isReadonly)
        {
            AddTBDate(key, key, DateTime.Now.ToString(DataType.SysDataFormat), desc, uiVisable, isReadonly);
        }
        #endregion

        #region ����ʱ�����͡�
        /// <summary>
        /// �����������͵Ŀؽ�
        /// </summary>
        /// <param name="key">��ֵ</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        public void AddTBDateTime(string key, string field, string defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppDateTime;
            attr.Desc = desc;
            attr.UITBShowType = TBType.DateTime;
            attr.UIVisible = uiVisable;
            attr.UIIsReadonly = isReadonly;
            attr.MaxLength = 50;
            attr.UIWidth = 100;
            this.Attrs.Add(attr);
        }
        public void AddTBDateTime(string key, string defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            this.AddTBDateTime(key, key, defaultVal, desc, uiVisable, isReadonly);
        }
        public void AddTBDateTime(string key, string desc, bool uiVisable, bool isReadonly)
        {
            this.AddTBDateTime(key, key, DateTime.Now.ToString(DataType.SysDataTimeFormat), desc, uiVisable, isReadonly);
        }
        #endregion

        #region �ʽ�����
        public void AddTBMoney(string key, string field, float defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppMoney;
            attr.Desc = desc;
            attr.UITBShowType = TBType.Moneny;
            attr.UIVisible = uiVisable;
            attr.UIIsReadonly = isReadonly;
            this.Attrs.Add(attr);
        }
        public void AddTBMoney(string key, float defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            this.AddTBMoney(key, key, defaultVal, desc, uiVisable, isReadonly);
        }
        #endregion

        #region Int����
        /// <summary>
        /// ����һ����ͨ�����͡�
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="_Field">�ֶ�</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        public void AddTBInt(string key, string _Field, int defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = _Field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppInt;
            attr.MyFieldType = FieldType.Normal;
            attr.Desc = desc;
            attr.UITBShowType = TBType.Int;
            attr.UIVisible = uiVisable;
            attr.UIIsReadonly = isReadonly;
            this.Attrs.Add(attr);
        }
        /// <summary>
        /// ����һ����ͨ�����͡��ֶ�ֵ��������ͬ��
        /// </summary>
        /// <param name="key">��</param>		 
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        public void AddTBInt(string key, int defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            this.AddTBInt(key, key, defaultVal, desc, uiVisable, isReadonly);
        }
        /// <summary>
        /// ����һ��PK�����͡�
        /// </summary>
        /// <param name="key">��</param>
        /// <param name="_Field">�ֶ�</param>
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        public void AddTBIntPK(string key, string _Field, int defaultVal, string desc, bool uiVisable, bool isReadonly, bool identityKey)
        {
            this.PKs = key;
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = _Field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppInt;
            attr.MyFieldType = FieldType.PK;
            attr.Desc = desc;
            attr.UITBShowType = TBType.Int;
            attr.UIVisible = uiVisable;
            attr.UIIsReadonly = isReadonly;
            if (identityKey)
                attr.UIBindKey = "1"; //�����Ǵ�ֵ�����������Զ���������������.
            this.Attrs.Add(attr);
        }
        /// <summary>
        /// ����һ��PK�����͡��ֶ�ֵ��������ͬ��
        /// </summary>
        /// <param name="key">��</param>		 
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        public void AddTBIntPKOID(string _field, string desc)
        {
            this.AddTBIntPK("OID", _field, 0, "OID", false, true,false);
        }
        /// <summary>
        /// ����һ��MID
        /// </summary>
        public void AddTBMID()
        {
            Attr attr = new Attr();
            attr.Key = "MID";
            attr.Field = "MID";
            attr.DefaultVal = 0;
            attr.MyDataType = DataType.AppInt;
            attr.MyFieldType = FieldType.Normal;
            attr.Desc = "MID";
            attr.UITBShowType = TBType.Int;
            attr.UIVisible = false;
            attr.UIIsReadonly = true;
            this.Attrs.Add(attr);
        }
        public void AddTBIntPKOID()
        {
            this.AddTBIntPKOID("OID", "OID");
        }
        public void AddTBMyNum(string desc)
        {
            this.AddTBInt("MyNum", 1, desc, true, true);
        }
        public void AddTBMyNum()
        {
            this.AddTBInt("MyNum", 1, "����", true, true);
        }
        /// <summary>
        /// ����  AtParas�ֶ�.
        /// </summary>
        /// <param name="fieldLength"></param>
        public void AddTBAtParas(int fieldLength)
        {
            this.AddTBString("AtPara", null, "AtPara", false, true, 0, fieldLength, 10);
        }
        /// <summary>
        /// ����
        /// </summary>
        public void AddMyPK()
        {
            this.PKs = "MyPK";
            this.AddTBStringPK("MyPK", null, "MyPK", true, true, 1, 100, 10);

            //Attr attr = new Attr();
            //attr.Key = "MyPK";
            //attr.Field = "MyPK";
            //attr.DefaultVal = null;
            //attr.MyDataType = DataType.AppString;
            //attr.MyFieldType = FieldType.PK;
            //attr.Desc = "MyPK";
            //attr.UITBShowType = TBType.TB;
            //attr.UIVisible = false;
            //attr.UIIsReadonly = true;
            //attr.MinLength = 1;
            //attr.MaxLength = 100;
            //this.Attrs.Add(attr);
        }
        public void AddMyPKNoVisable()
        {
            this.AddTBStringPK("MyPK", null, "MyPK", false, false, 1, 100, 10);
        }
        /// <summary>
        /// �����Զ�������
        /// </summary>
        public void AddAID()
        {
            Attr attr = new Attr();
            attr.Key = "AID";
            attr.Field = "AID";
            attr.DefaultVal = null;
            attr.MyDataType = DataType.AppInt;
            attr.MyFieldType = FieldType.PK;
            attr.Desc = "AID";
            attr.UITBShowType = TBType.TB;
            attr.UIVisible = false;
            attr.UIIsReadonly = true;
            this.Attrs.Add(attr);
        }
        /// <summary>
        /// ����һ��PK�����͡��ֶ�ֵ��������ͬ��
        /// </summary>
        /// <param name="key">��</param>		 
        /// <param name="defaultVal">Ĭ��ֵ</param>
        /// <param name="desc">����</param>
        /// <param name="uiVisable">�ǲ��ǿɼ�</param>
        /// <param name="isReadonly">�ǲ���ֻ��</param>
        public void AddTBIntPK(string key, int defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            this.AddTBIntPK(key, key, defaultVal, desc, uiVisable, isReadonly,false);
        }

        public void AddTBIntPK(string key, int defaultVal, string desc, bool uiVisable, bool isReadonly, bool identityKey)
        {
            this.AddTBIntPK(key, key, defaultVal, desc, uiVisable, isReadonly, identityKey);
        }
        public void AddTBIntMyNum()
        {
            this.AddTBInt("MyNum", "MyNum", 1, "����", true, true);
        }
        #endregion

        #region Float����
        public void AddTBFloat(string key, string _Field, float defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = _Field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppFloat;
            attr.Desc = desc;
            attr.UITBShowType = TBType.Num;
            attr.UIVisible = uiVisable;
            attr.UIIsReadonly = isReadonly;
            this.Attrs.Add(attr);
        }
        public void AddTBFloat(string key, float defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            this.AddTBFloat(key, key, defaultVal, desc, uiVisable, isReadonly);
        }
        #endregion

        #region Decimal����
        public void AddTBDecimal(string key, string _Field, decimal defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = _Field;
            attr.DefaultVal = defaultVal;
            attr.MyDataType = DataType.AppDouble;
            attr.Desc = desc;
            attr.UITBShowType = TBType.Decimal;
            attr.UIVisible = uiVisable;
            attr.UIIsReadonly = isReadonly;
            this.Attrs.Add(attr);
        }
        public void AddTBDecimal(string key, decimal defaultVal, string desc, bool uiVisable, bool isReadonly)
        {
            this.AddTBDecimal(key, key, defaultVal, desc, uiVisable, isReadonly);
        }
        #endregion
        #endregion

        #endregion
    }
}
