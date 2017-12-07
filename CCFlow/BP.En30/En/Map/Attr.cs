using System;
using System.Collections;
using BP.Web.Controls ;
using BP.DA;

namespace BP.En
{
    /// <summary>
    /// �༭����
    /// </summary>
    public enum EditType
    {
        /// <summary>
        /// �ɱ༭
        /// </summary>
        Edit,
        /// <summary>
        /// ����ɾ��
        /// </summary>
        UnDel,
        /// <summary>
        /// ֻ��,����ɾ����
        /// </summary>
        Readonly
    }
    /// <summary>
    /// �Զ���䷽ʽ
    /// </summary>
    public enum AutoFullWay
    {
        /// <summary>
        /// ������
        /// </summary>
        Way0,
        /// <summary>
        /// ��ʽ1
        /// </summary>
        Way1_JS,
        /// <summary>
        /// sql ��ʽ��
        /// </summary>
        Way2_SQL,
        /// <summary>
        /// ���
        /// </summary>
        Way3_FK,
        /// <summary>
        /// ��ϸ
        /// </summary>
        Way4_Dtl,
        /// <summary>
        /// �ű�
        /// </summary>
        Way5_JS
    }
	/// <summary>
	///  �ؼ�����
	/// </summary>
	public enum UIContralType
	{
		/// <summary>
		/// �ı���
		/// </summary>
		TB=0,
		/// <summary>
		/// ������
		/// </summary>
		DDL=1,
		/// <summary>
		/// CheckBok
		/// </summary>
		CheckBok=2,
		/// <summary>
		/// ��ѡ��ť
		/// </summary>
		RadioBtn=3
	}
    /// <summary>
    /// �߼�����
    /// </summary>
    public enum FieldTypeS
    {
        /// <summary>
        /// ��ͨ����
        /// </summary>
        Normal,
        /// <summary>
        /// ö������
        /// </summary>
        Enum,
        /// <summary>
        /// ���
        /// </summary>
        FK
    }
	/// <summary>
	/// �ֶ�����
	/// </summary>
	public enum FieldType
	{
		/// <summary>
		/// ������
		/// </summary>
		Normal,
		/// <summary>
		/// ����
		/// </summary>
		PK,
		/// <summary>
		/// ���
		/// </summary>
		FK,
		/// <summary>
		/// ö��
		/// </summary>
	    Enum,		 
		/// <summary>
		/// ���������������
		/// </summary>
		PKFK,
		/// <summary>
		/// ������������ö��
		/// </summary>
		PKEnum,
		/// <summary>
		/// �������ı�.
		/// </summary>
		RefText,
		/// <summary>
		/// �����
		/// </summary>
		NormalVirtual,
		/// <summary>
		/// ��ֵ��
		/// </summary>
		MultiValues
	}
	/// <summary>
	/// ����
	/// </summary>
	public class Attr
	{
        public BP.Sys.MapAttr ToMapAttr
        {
            get
            {

                BP.Sys.MapAttr attr = new BP.Sys.MapAttr();

                attr.KeyOfEn = this.Key;
                attr.Name = this.Desc;
                attr.DefVal  = this.DefaultVal.ToString();
                attr.KeyOfEn = this.Field;

                attr.MaxLen = this.MaxLength;
                attr.MinLen = this.MinLength;
                attr.UIBindKey = this.UIBindKey;
                attr.UIIsLine = this.UIIsLine;
                attr.UIHeight = 0;


                if (this.MaxLength > 3000)
                    attr.UIHeight = 10;

                attr.UIWidth = this.UIWidth;
                attr.MyDataType = this.MyDataType;

                attr.UIRefKey = this.UIRefKeyValue;

                attr.UIRefKeyText = this.UIRefKeyText;
                attr.UIVisible = this.UIVisible;
                //if (this.IsPK)
                //    attr.MyDataType =  = FieldType.PK;
                //    attr.MyFieldType = FieldType.PK;

                switch (this.MyFieldType)
                {
                    case FieldType.Enum:
                    case FieldType.PKEnum:
                        attr.UIContralType = this.UIContralType;
                        attr.LGType = FieldTypeS.Enum;
                        attr.UIIsEnable = this.UIIsReadonly;
                        break;
                    case FieldType.FK:
                    case FieldType.PKFK:
                        attr.UIContralType = this.UIContralType;
                        attr.LGType = FieldTypeS.FK;
                        //attr.MyDataType = (int)FieldType.FK;
                        attr.UIRefKey = "No";
                        attr.UIRefKeyText = "Name";
                        attr.UIIsEnable = this.UIIsReadonly;
                        break;
                    default:
                        attr.UIContralType = UIContralType.TB;
                        attr.LGType = FieldTypeS.Normal;

                        attr.UIIsEnable = !this.UIIsReadonly;
                        switch (this.MyDataType)
                        {
                            case DataType.AppBoolean:
                                attr.UIContralType = UIContralType.CheckBok;
                                attr.UIIsEnable = this.UIIsReadonly;
                                break;
                            case DataType.AppDate:
                                //if (this.Tag == "1")
                                //    attr.DefaultVal = DataType.CurrentData;
                                break;
                            case DataType.AppDateTime:
                                //if (this.Tag == "1")
                                //    attr.DefaultVal = DataType.CurrentData;
                                break;
                            default:
                                break;
                        }
                        break;
                }

                //attr.HisAutoFull = this.AutoFullWay;
                //attr.AutoFullDoc = this.AutoFullDoc;
                return attr;
            }
        }
        public BP.Web.Controls.TBType HisTBType
        {
            get
            {
                switch (this.MyDataType)
                {
                    case BP.DA.DataType.AppRate:
                    case BP.DA.DataType.AppMoney:
                        return BP.Web.Controls.TBType.Moneny;
                    case BP.DA.DataType.AppInt:
                    case BP.DA.DataType.AppFloat:
                    case BP.DA.DataType.AppDouble:
                        return BP.Web.Controls.TBType.Num;
                    default:
                        return BP.Web.Controls.TBType.TB;
                }
            }
        }
        public bool IsFK
        {
            get
            {
                if (this.MyFieldType == FieldType.FK || this.MyFieldType == FieldType.PKFK)
                    return true;
                else
                    return false;
            }
        }
        public bool IsFKorEnum
        {
            get
            {
                if (
                this.MyFieldType == FieldType.Enum
                            || this.MyFieldType == FieldType.PKEnum
                            || this.MyFieldType == FieldType.FK
                            || this.MyFieldType == FieldType.PKFK)
                    return true;
                else
                    return false;
            }
        }
		/// <summary>
		/// �ǲ�����ʹ��Ĭ��ֵ��
		/// </summary>
		public bool IsCanUseDefaultValues
		{
			get
			{
				if ( this.MyDataType==DataType.AppString && this.UIIsReadonly==false )
					return true;
				return false;
			}
		}
        public bool IsNum
        {
            get
            {
                if (MyDataType == DataType.AppBoolean || MyDataType == DataType.AppDouble
                    || MyDataType == DataType.AppFloat
                    || MyDataType == DataType.AppInt
                    || MyDataType == DataType.AppMoney
                    || MyDataType == DataType.AppRate
                    )
                    return true;
                else
                    return false;
            }
        }
        public bool IsEnum
        {
            get
            {
                if ( MyFieldType == FieldType.Enum || MyFieldType == FieldType.PKEnum)
                    return true;
                else
                    return false;
            }
        }
        public bool IsRefAttr
        {
            get
            {
                if (this.MyFieldType == FieldType.RefText)
                    return true;
                return false;
            }
        }
		/// <summary>
		/// ���������ǲ���PK
		/// </summary>
		public bool IsPK
		{
			get
			{
				if ( MyFieldType==FieldType.PK || MyFieldType==FieldType.PKFK || MyFieldType==FieldType.PKEnum  )
					return true;
				else
					return false;
			}
		}
        private int _IsKeyEqualField = -1;
        public bool IsKeyEqualField
        {
            get
            {
                if (_IsKeyEqualField == -1)
                {
                    if (this.Key == this.Field)
                        _IsKeyEqualField = 1;
                    else
                        _IsKeyEqualField = 0;
                }

                if (_IsKeyEqualField == 1)
                    return true;
                return false;
            }
        }
		/// <summary>
		/// ��������
		/// </summary>
		public string EnterDesc
		{
			get
			{
				if (this.UIContralType==UIContralType.TB)
				{
					if (this.UIIsReadonly || this.UIVisible==false)
					{
						return "���ֶ�ֻ��";
					}
					else
					{
						if (this.MyDataType==DataType.AppDate )
						{
							return "������������"+DataType.SysDataFormat;
						}
						else if (this.MyDataType==DataType.AppDateTime)
						{
							return "��������ʱ������"+DataType.SysDataTimeFormat;
						}
						else if (this.MyDataType==DataType.AppString)
						{ 
							return "����Ҫ����С����"+this.MinLength+"�ַ�����󳤶�"+this.MaxLength+"�ַ�";
						}
						else if (this.MyDataType==DataType.AppMoney)
						{
							return "������� 0.00";
						}
						else 
						{
							return "������ֵ����";
						}
					}

				}
				else if ( this.UIContralType==UIContralType.DDL || this.UIContralType==UIContralType.CheckBok )
				{
					if (this.UIIsReadonly )
					{
						return "���ֶ�ֻ��";
					}
					else
					{
						if (this.MyDataType==DataType.AppBoolean)
						{
							return "��/��";
						}
						else
						{
							return "�б�ѡ��";
						}
					}
				}
				 
				return "";
			}
		}

		#region ���캯��
		public Attr()
		{}
		/// <summary>
		/// ���캯��
		/// </summary>
		/// <param name="key"></param>
		/// <param name="field"></param>
		/// <param name="defaultVal"></param>
		/// <param name="dataType"></param>
		/// <param name="isPK"></param>
		/// <param name="desc"></param>
		public Attr(string key , string field,  object defaultVal, int dataType, bool isPK, string desc, int minLength, int maxlength)
		{			
			this._key=key;
			this._field=field;
			this._desc=desc;
			if (isPK)
				this.MyFieldType = FieldType.PK ;			 
			this._dataType=dataType;
			this._defaultVal=defaultVal;
			this._minLength=minLength;
			this._maxLength=maxlength;
		}
		public Attr(string key , string field,  object defaultVal, int dataType, bool isPK, string desc)
		{			
			this._key=key;
			this._field=field;
			this._desc=desc;
			if (isPK)
				this.MyFieldType = FieldType.PK;
			this._dataType=dataType;
			this._defaultVal=defaultVal;			 
		}
		#endregion

		#region ����
        public string HelperUrl = null;
        public AutoFullWay AutoFullWay = AutoFullWay.Way0;
        public string  AutoFullDoc =null;
		/// <summary>
		/// ��������
		/// </summary>
		private string _key=null;
		/// <summary>
		/// ��������
		/// </summary>
		public string Key
		{
			get
			{
				return this._key;
			}
            set
            {
                if (value != null)
                    this._key = value.Trim();
            }
		}		
		/// <summary>
		/// ���Զ�Ӧ���ֶ�
		/// </summary>
		private string _field=null;
		/// <summary>
		/// ���Զ�Ӧ���ֶ�
		/// </summary>
		/// <returns></returns>
		public string Field
		{
			get
			{
				return this._field;
			}
			set
			{
                if (value != null)
                    this._field = value.Trim();
			}
		}		
		/// <summary>
		/// �ֶ�Ĭ��ֵ
		/// </summary>
		private object _defaultVal=null;
        public string DefaultValOfReal
        {
            get
            {
                if (_defaultVal == null)
                    return null;
                return _defaultVal.ToString();
            }
            set
            {
                _defaultVal = value;
            }
        }
		/// <summary>
		/// �ֶ�Ĭ��ֵ
		/// </summary>
		public object DefaultVal
		{
			get{
				switch (this.MyDataType)
				{
					case  DataType.AppString :
						if (this._defaultVal==null) 
							return "";
						break;
					case   DataType.AppInt :
						if (this._defaultVal==null )
							return 0;
						try
						{
							return int.Parse(this._defaultVal.ToString()) ;
						}
						catch
						{
							return 0;
							//throw new Exception("@����["+this.Key+"]Ĭ��ֵ���ִ���["+_defaultVal.ToString()+"]������ int ת����");
						}
					case   DataType.AppMoney :
						if (this._defaultVal==null)
							return 0;
						try
						{
							return float.Parse(this._defaultVal.ToString()) ;
						}
						catch
						{
                            return 0;
						//	throw new Exception("@����["+this.Key+"]Ĭ��ֵ���ִ���["+_defaultVal.ToString()+"]������ AppMoney ת����");
						}						
					case   DataType.AppFloat :
						if (this._defaultVal==null) 
							return 0;						 
						try
						{
							return float.Parse(this._defaultVal.ToString()) ;
						}
						catch
						{
                            return 0;
						//	throw new Exception("@����["+this.Key+"]Ĭ��ֵ���ִ���["+_defaultVal.ToString()+"]������ float ת����");
						}
						 
					case   DataType.AppBoolean :
						if (this._defaultVal==null || this._defaultVal.ToString()=="" ) 
							return 0;
						try
						{ 
							if ( DataType.StringToBoolean(this._defaultVal.ToString())  )
								return 1;
							else
								return 0;							 
						}
						catch
						{
							throw new Exception("@����["+this.Key+"]Ĭ��ֵ���ִ���["+this._defaultVal.ToString()+"]������ bool ת����������0/1��");
						}					 
						 
					case 5: 					 
						if (this._defaultVal==null) 
							return 0;
						try
						{
							return double.Parse(this._defaultVal.ToString()) ;
						}
						catch
						{
							throw new Exception("@����["+this.Key+"]Ĭ��ֵ���ִ���["+_defaultVal.ToString()+"]������ double ת����");
						}
						 					
					case   DataType.AppDate	 :					
						if (this._defaultVal==null) 
							return "";
						break;
					case   DataType.AppDateTime :
						if (this._defaultVal==null) 
							return "";
						break;
					default:
						throw new Exception("@bulider insert sql error: û������������ͣ��ֶ�����:"+this.Desc+" Ӣ��:"+this.Key);
				}
				return this._defaultVal;
			}
			 
			set
			{
				this._defaultVal=value;
			}
		}
		/// <summary>
		/// �������͡�
		/// </summary>
		private int _dataType=0;
		/// <summary>
		/// �������͡�
		/// </summary>
		public int MyDataType 
		{
			get{
				return this._dataType;
			}
			set
			{
				this._dataType=value;
			}
		}
		public string MyDataTypeStr
		{
			get
			{
				return DataType.GetDataTypeDese(this.MyDataType);
			}
		}

		/// <summary>
		/// �ǲ���������
		/// </summary>
		private FieldType _FieldType=FieldType.Normal;
		/// <summary>
		/// �ǲ�������
		/// </summary>
		/// <returns> yes / no</returns>
		public FieldType MyFieldType
		{
			get
			{
				return this._FieldType;
			}
			set
			{
				this._FieldType=value;
			}
		}
		/// <summary>
		/// ������
		/// </summary>
		private string _desc=null;
		public string Desc
		{
			get
			{
				return this._desc;
			}
			set
			{
				this._desc=value;
			}
		}
        /// <summary>
        /// ���߰���
        /// </summary>
        public string DescHelper
        {
            get
            {
                if (this.HelperUrl == null)
                    return this._desc;

                if (this.HelperUrl.Contains("script"))
                    return "<a href=\"" + this.HelperUrl + "\"  ><img src='../../Img/Help.png'  height='20px' border=0/>" + this._desc + "</a>";
                else
                    return "<a href=\"" + this.HelperUrl + "\" target=_blank ><img src='../../Img/Help.png'  height='20px' border=0/>" + this._desc + "</a>";
            }
        }
        public string DescHelperIcon
        {
            get
            {
                if (this.HelperUrl == null)
                    return this._desc;
                return "<a href=\"" + this.HelperUrl + "\" ><img src='../../Img/Help.png' height='20px' border=0/></a>";
            }
        }
		/// <summary>
		/// ��󳤶ȡ�
		/// </summary>
		private int _maxLength=4000;
		/// <summary>
		/// ��󳤶ȡ�
		/// </summary>
        public int MaxLength
        {
            get
            {
                switch (this.MyDataType)
                {
                    case DataType.AppDate:
                        return 50;
                    case DataType.AppDateTime:
                        return 50;
                    default:
                        if (this.IsFK)
                            return 100;
                        else
                            return this._maxLength;
                }
            }
            set
            {
                this._maxLength = value;
            }
        }
		/// <summary>
		/// ��С���ȡ�
		/// </summary>
		private int _minLength=0;
		/// <summary>
		/// ��С���ȡ�
		/// </summary>
		public int MinLength
		{
			get
			{
				return this._minLength;
			}
			set
			{
				this._minLength=value;
			}
		}
        /// <summary>
        /// �Ƿ����Ϊ��, ����ֵ���͵�������Ч.
        /// </summary>
        public bool IsNull
        {
            get
            {
                if (this.MinLength == 0)
                    return false;
                else
                    return true;
            }
        }
		#endregion 

		#region UI ����չ����
        public int UIWidthInt
        {
            get
            {
                return (int)this.UIWidth;
            }
        }
        private float _UIWidth = 80;
		/// <summary>
		/// ���
		/// </summary>
		public float UIWidth
		{
            get
            {
                if (this._UIWidth <= 10)
                    return 15;
                else
                    return this._UIWidth;
            }
			set
			{
				this._UIWidth=value;
			}
		}

		private int _UIHeight=0;
		/// <summary>
		/// �߶�
		/// </summary>
		public int UIHeight
		{
			get
			{
				return this._UIHeight*10;
			}
			set
			{
				this._UIHeight=value;
			}
		}

		private bool _UIVisible=true;
		/// <summary>
		/// �ǲ��ǿɼ�
		/// </summary>
        public bool UIVisible
        {
            get
            {
                return this._UIVisible;
            }
            set
            {
                this._UIVisible = value;
            }
        }
        /// <summary>
        /// �Ƿ�����ʾ
        /// </summary>
        public bool UIIsLine = false;
		private bool _UIIsReadonly=false;
		/// <summary>
		/// �ǲ���ֻ��
		/// </summary>
		public bool UIIsReadonly
		{
			get
			{
				return this._UIIsReadonly;
			}
			set
			{
				this._UIIsReadonly=value;
			}
		}
		private UIContralType _UIContralType=UIContralType.TB;
		/// <summary>
		/// �ؼ����͡�
		/// </summary>
		public UIContralType UIContralType
		{
			get
			{
				return this._UIContralType;
			}
			set
			{
				this._UIContralType=value;
			}
		}
		private string _UIBindKey=null;
		/// <summary>
		/// ҪBind ��Key.
		/// ��TB ������� DataHelpKey
		/// ��DDL ������  SelfBindKey.
		/// </summary>
		public string UIBindKey
		{
			get
			{
				return this._UIBindKey ;
			}
			set
			{
				this._UIBindKey=value;
			}
		}
        private string _UIBindKeyOfEn = null;
		public bool UIIsDoc
		{
			get
			{
				if (this.UIHeight!=0 && this.UIContralType==UIContralType.TB)
					return true;
				else
					return false;
			}
		}
        private Entity _HisFKEn = null;
        public Entity HisFKEn
        {
            get
            {
                #warning new a entity.
               return this.HisFKEns.GetNewEntity;

                if (_HisFKEn == null)
                    _HisFKEn = this.HisFKEns.GetNewEntity;

                return _HisFKEn;
            }
        }
		private Entities _HisFKEns=null;
		/// <summary>
		/// ��������ens.���ֻ����,���������fk, ʱ��Ч��
		/// </summary>
		public Entities HisFKEns
		{
			get
			{
				if (_HisFKEns==null)
				{

                    if (this.MyFieldType == FieldType.Enum || this.MyFieldType == FieldType.PKEnum)
                    {
                        return null;
                    }
                    else if (this.MyFieldType == FieldType.FK || this.MyFieldType == FieldType.PKFK)
                    {
                        if (this.UIBindKey.Contains("."))
                            _HisFKEns = ClassFactory.GetEns(this.UIBindKey);
                        else
                            _HisFKEns = new GENoNames(this.UIBindKey, this.Desc);  // ClassFactory.GetEns(this.UIBindKey);
                    }
                    else
                    {
                        return null;
                    }
				}
				return _HisFKEns;
			}
			set
			{
				_HisFKEns=value;
			}
		}
		private  TBType _TBShowType =TBType.TB;
		/// <summary>
		/// Ҫ��ʵ�Ŀؼ����͡�
		/// </summary>
		public TBType UITBShowType
		{
			get
			{
				if (this.MyDataType==DataType.AppDate)
					return TBType.Date ;
				else if (this.MyDataType==DataType.AppFloat)
					return TBType.Float ;
				else if (this.MyDataType==DataType.AppBoolean)
					return TBType.Date ; //throw new Exception("@�������ô���");
				else if (this.MyDataType==DataType.AppDouble)
					return TBType.Decimal ;
				else if (this.MyDataType==DataType.AppInt)
					return TBType.Num ;
				else if (this.MyDataType==DataType.AppMoney)
					return TBType.Moneny;
				else
					return _TBShowType; 
			}
			set
			{
				this._TBShowType=value;
			}
		}
		private  DDLShowType _UIDDLShowType =DDLShowType.None;
		/// <summary>
		/// Ҫ��ʵ�Ŀؼ����͡�
		/// </summary>
		public DDLShowType UIDDLShowType
		{
			get
			{
				if (this.MyDataType == DataType.AppBoolean)
					return DDLShowType.Boolean ;
				else 
				    return this._UIDDLShowType ;
			}
			set
			{
				this._UIDDLShowType=value;
			}
		}

		private string _UIRefKey=null;
		/// <summary>
		/// ҪBind ��Key. ��TB ������� DataHelpKey 
		/// ��DDL ������SelfBindKey.
		/// </summary>
		public string UIRefKeyValue
		{
			get
			{
				return this._UIRefKey ;
			}
			set
			{
				this._UIRefKey=value;
			}
		}
		private string _UIRefText=null;
		/// <summary>
		/// ������ʵ��valkey	 
		/// </summary>
		public string UIRefKeyText
		{
			get
			{
				return this._UIRefText ;
			}
			set
			{
				this._UIRefText=value;
			}
		}
        public string UITag = null;
		#endregion		 
	}
	/// <summary>
	/// ���Լ���
	/// </summary>
	[Serializable]
	public class Attrs: CollectionBase
	{
		#region �������Ե����� String
		protected void AddTBString(string key , string field,  object defaultVal, 
            FieldType _FieldType, TBType tbType, string desc, bool uiVisable, bool isReadonly ,int minLength, int maxLength, int tbWith )
		{
			Attr attr = new Attr();
			attr.Key=key;
			attr.Field=field;
			attr.DefaultVal = defaultVal;
			attr.MyDataType=DataType.AppString;
			attr.Desc=desc;
			attr.UITBShowType=tbType;
			attr.UIVisible = uiVisable;
			attr.UIWidth=tbWith;
			attr.UIIsReadonly = isReadonly;
			attr.MaxLength =maxLength;
			attr.MinLength =minLength;
			attr.MyFieldType=_FieldType;
			this.Add(attr);
		}
		public void AddTBString(string key, string defaultVal,  string desc, bool uiVisable, bool isReadonly ,int minLength, int maxLength, int tbWith )
		{
			AddTBString(  key ,   key,    defaultVal,   FieldType.Normal,  TBType.TB,  desc,   uiVisable,   isReadonly ,  minLength,   maxLength,   tbWith );
		}
		public void AddTBString(string key, string field , object defaultVal,   string desc, bool uiVisable, bool isReadonly ,int minLength, int maxLength, int tbWith )
		{
			AddTBString(  key ,   field,    defaultVal,   FieldType.Normal,  TBType.TB,  desc,   uiVisable,   isReadonly ,  minLength,   maxLength,   tbWith );
		}

		public void AddTBStringDoc(string key, string defaultVal,  string desc, bool uiVisable, bool isReadonly)
		{
			AddTBStringDoc(  key ,   key,    defaultVal,    desc,   uiVisable,   isReadonly ,  0,   2000,   300, 300 );
		}
		public void AddTBStringDoc(string key, string defaultVal,  string desc, bool uiVisable, bool isReadonly ,int minLength, int maxLength, int tbWith , int rows )
		{
			AddTBStringDoc(  key ,   key,    defaultVal,    desc,   uiVisable,   isReadonly ,  minLength,   maxLength,   tbWith, rows );
		}
		public void AddTBStringDoc(string key,string field, string defaultVal,  string desc, bool uiVisable, bool isReadonly ,int minLength, int maxLength, int tbWith, int rows )
		{
			Attr attr = new Attr();
			attr.Key=key;
			attr.Field=field;
			attr.DefaultVal = defaultVal;
			attr.MyDataType=DataType.AppString;
			attr.Desc=desc;
			attr.UITBShowType=TBType.TB;
			attr.UIVisible = uiVisable;
			attr.UIWidth=300;
			attr.UIIsReadonly = isReadonly;
			attr.MaxLength =maxLength;
			attr.MinLength =minLength;
			attr.MyFieldType=FieldType.Normal;
			attr.UIHeight = rows;
			this.Add(attr);
		}
        /// <summary>
        /// ���Ӹ���
        /// </summary>
        /// <param name="fileDesc"></param>
        public void AddMyFile(string fileDesc)
        {
            this.AddTBString(EntityNoMyFileAttr.MyFileName, null, fileDesc, false, false, 0, 100, 200);
            this.AddTBString(EntityNoMyFileAttr.MyFilePath, null, "MyFilePath", false, false, 0, 100, 200);
            this.AddTBString(EntityNoMyFileAttr.MyFileExt, null, "MyFileExt", false, false, 0, 10, 10);
            //  this.AddTBInt(EntityNoMyFileAttr.MyFileNum, 0, "MyFileNum", false, false);
            this.AddTBInt(EntityNoMyFileAttr.MyFileH, 0, "MyFileH", false, false);
            this.AddTBInt(EntityNoMyFileAttr.MyFileW, 0, "MyFileW", false, false);
            this.AddTBInt("MyFileSize", 0, "MyFileSize", false, false);

            //this.IsHaveFJ = true;
        }
		#endregion  �������Ե����� String


		#region �������Ե����� Int

		/// <summary>
		/// ����һ����ͨ�����͡�
		/// </summary>
		/// <param name="key">��</param>
		/// <param name="_Field">�ֶ�</param>
		/// <param name="defaultVal">Ĭ��ֵ</param>
		/// <param name="desc">����</param>
		/// <param name="uiVisable">�ǲ��ǿɼ�</param>
		/// <param name="isReadonly">�ǲ���ֻ��</param>
		public void AddTBInt(string key, string _Field, int defaultVal, string desc, bool uiVisable, bool isReadonly )
		{
			Attr attr = new Attr();
			attr.Key=key;
			attr.Field=_Field;
			attr.DefaultVal = defaultVal;
			attr.MyDataType=DataType.AppInt;
			attr.MyFieldType = FieldType.Normal;
			attr.Desc=desc;
			attr.UITBShowType=TBType.Int;
			attr.UIVisible = uiVisable;
			attr.UIIsReadonly = isReadonly;
			this.Add(attr);
		}
		/// <summary>
		/// ����һ����ͨ�����͡��ֶ�ֵ��������ͬ��
		/// </summary>
		/// <param name="key">��</param>		 
		/// <param name="defaultVal">Ĭ��ֵ</param>
		/// <param name="desc">����</param>
		/// <param name="uiVisable">�ǲ��ǿɼ�</param>
		/// <param name="isReadonly">�ǲ���ֻ��</param>
		public void AddTBInt(string key,  int defaultVal, string desc, bool uiVisable, bool isReadonly )
		{
			this.AddTBInt(key,key,defaultVal,desc,uiVisable,isReadonly) ;
		}
        public void AddBoolen(string key, bool defaultVal, string desc)
        {
            Attr attr = new Attr();
            attr.Key = key;
            attr.Field = key;

            if (defaultVal)
                attr.DefaultVal = 1;
            else
                attr.DefaultVal = 0;

            attr.MyDataType = DataType.AppBoolean;
            attr.Desc = desc;
            attr.UIContralType = UIContralType.CheckBok;
            attr.UIIsReadonly = true;
            attr.UIVisible = true;
            this.Add(attr);
        }
		#endregion  �������Ե����� Int


		#region �������Ե����� Float����
		public void AddTBFloat(string key, string _Field, float defaultVal, string desc, bool uiVisable, bool isReadonly )
		{
			Attr attr = new Attr();
			attr.Key=key;
			attr.Field=_Field;
			attr.DefaultVal = defaultVal;
			attr.MyDataType=DataType.AppFloat;
			attr.Desc=desc;
			attr.UITBShowType=TBType.Num;
			attr.UIVisible = uiVisable;
			attr.UIIsReadonly = isReadonly;
			this.Add(attr);
		}
		public void AddTBFloat(string key, float defaultVal,  string desc, bool uiVisable, bool isReadonly )
		{
			this.AddTBFloat(key,key, defaultVal,desc,uiVisable,isReadonly) ; 
		}
		#endregion  �������Ե����� Float


		#region Decimal����
		public void AddTBDecimal(string key, string _Field, decimal defaultVal, string desc, bool uiVisable, bool isReadonly )
		{
			Attr attr = new Attr();
			attr.Key=key;
			attr.Field=_Field;
			attr.DefaultVal = defaultVal;
			attr.MyDataType=DataType.AppDouble;
			attr.Desc=desc;
			attr.UITBShowType=TBType.Decimal;
			attr.UIVisible = uiVisable;
			attr.UIIsReadonly = isReadonly;
			this.Add(attr);
		}
		public void AddTBDecimal(string key, decimal defaultVal,  string desc, bool uiVisable, bool isReadonly )
		{
			this.AddTBDecimal(key,key, defaultVal,desc,uiVisable,isReadonly) ; 
		}
		#endregion


		#region ����
		public void AddTBDate(string key, string field, string defaultVal,  string desc, bool uiVisable, bool isReadonly )
		{
			Attr attr = new Attr();
			attr.Key=key;
			attr.Field=field;
			attr.DefaultVal = defaultVal;
			attr.MyDataType=DataType.AppDate;
			attr.Desc=desc;
			attr.UITBShowType=TBType.Date;
			attr.UIVisible = uiVisable;
			attr.UIIsReadonly = isReadonly;
			attr.MaxLength=20;
			this.Add(attr);
		}
		 
		public void AddTBDate(string key, string defaultVal,  string desc, bool uiVisable, bool isReadonly )
		{
			this.AddTBDate(key,key,defaultVal,desc,uiVisable,isReadonly) ; 
		}
		/// <summary>
		/// �����������͵Ŀؽ�(Ĭ�������ǵ�ǰ����)
		/// </summary>
		/// <param name="key">key</param>
		/// <param name="desc">desc</param>
		/// <param name="uiVisable">uiVisable</param>
		/// <param name="isReadonly">isReadonly</param>
		public void AddTBDate(string key, string desc, bool uiVisable, bool isReadonly )
		{
			this.AddTBDate(key,key,DateTime.Now.ToString(DataType.SysDataFormat),desc,uiVisable,isReadonly) ; 
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
		public void AddTBDateTime(string key, string field, string defaultVal, 
			string desc, bool uiVisable, bool isReadonly )
		{
			Attr attr = new Attr();
			attr.Key=key;
			attr.Field=field;
			attr.DefaultVal = defaultVal;
			attr.MyDataType=DataType.AppDateTime;
			attr.Desc=desc;
			attr.UITBShowType=TBType.DateTime ;
			attr.UIVisible = uiVisable;			 
			attr.UIIsReadonly = isReadonly;
			attr.MaxLength=30;
			attr.MinLength=0;
			attr.UIWidth=100;
			this.Add(attr);
		}
		public void AddTBDateTime(string key, string defaultVal,  string desc, bool uiVisable, bool isReadonly )
		{
			this.AddTBDateTime(key,key,defaultVal,desc,uiVisable,isReadonly);
		}
		public void AddTBDateTime(string key,string desc, bool uiVisable, bool isReadonly )
		{
			this.AddTBDateTime(key,key,DateTime.Now.ToString(DataType.SysDataTimeFormat),desc,uiVisable,isReadonly);
		}
		#endregion 


		#region �ڰﶨ�Զ���,ö�������й�ϵ�Ĳ�����
        public void AddDDLSysEnum(string key, int defaultVal, string desc, bool isUIVisable, bool isUIEnable, string sysEnumKey)
        {
            this.AddDDLSysEnum(key, key, defaultVal, desc, isUIVisable, isUIEnable, sysEnumKey, null);
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="key"></param>
        /// <param name="field"></param>
        /// <param name="defaultVal"></param>
        /// <param name="desc"></param>
        /// <param name="isUIVisable"></param>
        /// <param name="isUIEnable"></param>
        /// <param name="sysEnumKey"></param>
        public void AddDDLSysEnum(string key,string field, int defaultVal, string desc, bool isUIVisable, bool isUIEnable, string sysEnumKey)
        {
            this.AddDDLSysEnum(key, field, defaultVal, desc, isUIVisable, isUIEnable, sysEnumKey, null);
        }
		/// <summary>
		/// �Զ���ö������
		/// </summary>
		/// <param name="key">��</param>
		/// <param name="field">�ֶ�</param>
		/// <param name="defaultVal">Ĭ��</param>
		/// <param name="desc">����</param>
		/// <param name="sysEnumKey">Key</param>
        public void AddDDLSysEnum(string key, string field, int defaultVal, string desc, bool isUIVisable, bool isUIEnable, string sysEnumKey, string cfgVal)
        {
            Attr attr = new Attr();
            attr.Key = key;
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
            this.Add(attr);
        }
		/// <summary>
		/// �Զ���ö������
		/// </summary>
		/// <param name="key">��</param>		
		/// <param name="defaultVal">Ĭ��</param>
		/// <param name="desc">����</param>
		/// <param name="sysEnumKey">Key</param>
		public void AddDDLSysEnum(string key , int defaultVal, string desc,  bool isUIVisable, bool isUIEnable, string sysEnumKey,string cfgVals)
		{
            AddDDLSysEnum(key, key, defaultVal, desc, isUIVisable, isUIEnable, sysEnumKey, cfgVals);
		}
        public void AddDDLSysEnum(string key, int defaultVal, string desc, bool isUIVisable, bool isUIEnable)
        {
            AddDDLSysEnum(key, key, defaultVal, desc, isUIVisable, isUIEnable, key);
        }
		#endregion 

		#region entities
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
		public void AddDDLEntities(string key , string field,  object defaultVal, int dataType, FieldType _fildType, string desc, Entities ens, string refKey, string refText, bool uiIsEnable )
		{
			Attr attr = new Attr();
			attr.Key=key;  
			attr.Field=field;
			attr.DefaultVal = defaultVal;
			attr.MyDataType= dataType ;
			attr.MyFieldType=_fildType;
			 
			attr.Desc=desc;
			attr.UIContralType=UIContralType.DDL ;
			attr.UIDDLShowType=DDLShowType.Ens;
			attr.UIBindKey=ens.ToString();
            //attr.UIBindKeyOfEn = ens.GetNewEntity.ToString();
            attr.HisFKEns = ens;

			attr.HisFKEns=ens;
			attr.UIRefKeyText=refText;
			attr.UIRefKeyValue=refKey;
			attr.UIIsReadonly=uiIsEnable;
			this.Add(attr,true,false);
		}
		public void AddDDLEntities(string key , string field,  object defaultVal, int dataType, string desc, Entities ens, string refKey, string refText, bool uiIsEnable )
		{
			AddDDLEntities(key,field,defaultVal, dataType , FieldType.FK , desc, ens,refKey,refText,uiIsEnable);
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
		public void AddDDLEntities(string key ,  object defaultVal, int dataType, string desc, Entities ens, string refKey, string refText , bool uiIsEnable)
		{
			AddDDLEntities(key,key,defaultVal,  dataType ,desc, ens,refKey,refText,uiIsEnable);
		}
        #endregion


		#region entityNoName
		public void AddDDLEntities(string key ,object defaultVal,   string desc, EntitiesNoName ens, bool uiIsEnable )
		{
			this.AddDDLEntities(key,key,defaultVal,DataType.AppString, desc,ens,"No","Name",uiIsEnable);
		}
		public void AddDDLEntities(string key , string field,  object defaultVal, string desc, EntitiesNoName ens, bool uiIsEnable )
		{
			this.AddDDLEntities(key,field,defaultVal,DataType.AppString,desc,ens,"No","Name",uiIsEnable);
		}
		#endregion

        #region EntitiesSimpleTree
        public void AddDDLEntities(string key, object defaultVal, string desc, EntitiesSimpleTree ens, bool uiIsEnable)
        {
            this.AddDDLEntities(key, key, defaultVal, DataType.AppString, desc, ens, "No", "Name", uiIsEnable);
        }
        public void AddDDLEntities(string key, object defaultVal, string desc, EntitiesTree ens, bool uiIsEnable)
        {
            this.AddDDLEntities(key, key, defaultVal, DataType.AppString, desc, ens, "No", "Name", uiIsEnable);
        }
        #endregion


        #region EntitiesOIDName
        public void AddDDLEntities(string key, object defaultVal, string desc, EntitiesOIDName ens, bool uiIsEnable)
        {
            this.AddDDLEntities(key, key, defaultVal, DataType.AppInt, desc, ens, "OID", "Name", uiIsEnable);
        }
        public void AddDDLEntities(string key, string field, object defaultVal, string desc, EntitiesOIDName ens, bool uiIsEnable)
        {
            this.AddDDLEntities(key, field, defaultVal, DataType.AppInt, desc, ens, "OID", "Name", uiIsEnable);
        }
		#endregion

		public Attrs Clone()
		{
			Attrs attrs = new Attrs();
			foreach(Attr attr in this)
			{
				attrs.Add(attr);
			}
			return attrs;
		}
		/// <summary>
		/// ��һ��Attr �Ƿ��� Doc ����.
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public Attr NextAttr(string CurrentKey)
		{
			int i =this.GetIndexByKey( CurrentKey) ;

			if (this.Count > i )
				return null;

			return  this[i+1] as Attr;
		}
		public Attr PrvAttr(string CurrentKey)
		{
			int i =this.GetIndexByKey( CurrentKey ) ;

			if (this.Count < i )
				return null;

			return  this[i-1] as Attr;
		}
		/// <summary>
		/// �Ƿ��������key��
		/// </summary>
		/// <param name="key"></param>
		/// <returns></returns>
		public bool Contains(string key)
		{
            foreach (Attr attr in this)
            {
                if (attr.Key  == key )
                    return true;
            }
			return false;
		}
        public bool ContainsUpper(string key)
        {
            foreach (Attr attr in this)
            {
                if (attr.Key.ToUpper() == key.ToUpper() )
                    return true;
            }
            return false;
        }
		/// <summary>
		/// �����ֶ�Num
		/// </summary>
		public int ConutOfPhysicsFields
		{
			get
			{
				int i = 0 ;
				foreach(Attr attr in this)
				{
					if (attr.MyFieldType!=FieldType.RefText)
						i++;
				}
				return i ;
			}
		}
		
		protected override void OnInsertComplete(int index, object value)
		{
			base.OnInsertComplete (index, value);
		}
		
		/// <summary>
		/// ͨ��Key �� ȡ������Index.
		/// </summary>
		/// <param name="key">Key</param>
		/// <returns>index</returns>
		public int GetIndexByKey(string key)
		{
			for(int i=0 ; i < this.Count ; i++)
			{
				if (this[i].Key == key)
					return i ;
			}
			return -1;
		}
        public Attr GetAttrByKey(string key)
		{
            foreach (Attr  item in this)
            {
                if (item.Key == key)
                    return item;
            }
            return null;
        }

      
        /// <summary>
        /// ���Լ���
        /// </summary>
        /// <param name="cfgKeys">Լ�����ֶθ�ʽ</param>
        public Attrs(string cfgKeys) 
        {

            /**
             *  ����Լ���ĸ�ʽ���ַ������ɼ���.
             *  
 @Key=MyStringField;Name=�ҵ������ֶ�;Type=String;DefVal=Ĭ��ֵ;AppType=Normal;IsLine=1             
 @Key=MyIntField;Name=�ҵ������ֶ�;Type=Int;DefVal=12;AppType=Normal;IsLine=false
 @Key=MyFloatField;Name=�ҵĸ����ֶ�;Type=Float;DefVal=12.0;AppType=Normal;IsLine=false
 @Key=MyEnumField;Name=�ҵ�ö���ֶ�;Type=Int;DefVal=0;AppType=Enum;IsLine=false;BindKey=[0=Yes,1=No,2=Unhnow]
 @Key=MyFKField;Name=�ҵ�����ֶ�;Type=String;DefVal=01;AppType=FK;IsLine=false;BindKey=BP.Port.Depts
             * */

            string[] strs = cfgKeys.Split('@');
            foreach (string str in strs)
            {
                AtPara ap = new AtPara(str.Replace(";", "@"));
                FieldTypeS ft = (FieldTypeS)ap.GetValIntByKey("AppType");
                switch (ft)
                {
                    case FieldTypeS.Enum:
                        this.AddDDLSysEnum(ap.GetValStrByKey("Key"), ap.GetValStrByKey("Key"),
                            ap.GetValIntByKey("DefVal"), ap.GetValStrByKey("Name"), true, true, ap.GetValStrByKey("Key"), "@"+ap.GetValStrByKey("BindKey").Replace(",","@"));
                        break;
                    case FieldTypeS.FK:
                        EntitiesNoName ens = (EntitiesNoName)BP.En.ClassFactory.GetEns(ap.GetValStrByKey("BindKey"));
                        this.AddDDLEntities(ap.GetValStrByKey("Key"), ap.GetValStrByKey("DefVal"), ap.GetValStrByKey("Name"), ens,true);
                        break;
                    default:
                        switch (ap.GetValStrByKey("Type"))
                        {
                            case "String":
                                this.AddTBString(ap.GetValStrByKey("Key"), ap.GetValStrByKey("DefVal"), ap.GetValStrByKey("Name"), true, false, 0, 1000, 500);
                                break;
                            case "Int":
                                this.AddTBInt(ap.GetValStrByKey("Key"), ap.GetValIntByKey("DefVal"), ap.GetValStrByKey("Name"), true, false);
                                break;
                            case "Float":
                                this.AddTBFloat(ap.GetValStrByKey("Key"), ap.GetValFloatByKey("DefVal"), ap.GetValStrByKey("Name"), true, false);
                                break;
                            default:
                                break;
                        }
                        break;
                }
            }
        }
		/// <summary>
		/// ���Լ���
		/// </summary>
		public Attrs(){}
	 
        public void Add(Attr attr)
        {
            if (attr.Field == null || attr.Field == "")
                throw new Exception("�������ô������������� key='" + attr.Key + "',���ֶ�ֵΪ��");

            bool k = attr.IsKeyEqualField;
            this.Add(attr, true, false);
        }
		/// <summary>
		/// ����һ�����ԡ�
		/// </summary>
		/// <param name="attr">attr</param>
		/// <param name="isAddHisRefText">isAddHisRefText</param>
		public void Add(Attr attr, bool isAddHisRefText, bool isAddHisRefName )
		{
            foreach (Attr myattr in this)
            {
                if (myattr.Key == attr.Key)
                    return;
            }

			this.InnerList.Add(attr);

			if (isAddHisRefText)
				this.AddRefAttrText(attr);

            if (isAddHisRefName)
                this.AddRefAttrName(attr);
		}
		private void AddRefAttrText(Attr attr)
		{
			if ( attr.MyFieldType==FieldType.FK 
				||  attr.MyFieldType==FieldType.Enum 
				||  attr.MyFieldType==FieldType.PKEnum 
				||  attr.MyFieldType==FieldType.PKFK )
			{

				Attr myattr= new Attr();
				myattr.MyFieldType=FieldType.RefText ;
				myattr.MyDataType=DataType.AppString ;
				myattr.UIContralType=UIContralType.TB;
				myattr.UIWidth=attr.UIWidth*2;
				myattr.Key=    attr.Key+"Text";


				myattr.UIIsReadonly=true;
				myattr.UIBindKey = attr.UIBindKey ;
               // myattr.UIBindKeyOfEn = attr.UIBindKeyOfEn;
                myattr.HisFKEns = attr.HisFKEns;
                             

              
				//myattr.Desc=attr.Desc+"����";
				 
				string desc=myattr.Desc="����";
				if (desc.IndexOf("���") >=0 )
					myattr.Desc=attr.Desc.Replace("���","����");
				else
					myattr.Desc=attr.Desc+"����";

				if (attr.UIContralType==UIContralType.DDL)
					myattr.UIVisible=false;

				this.InnerList.Add(myattr);


				//this.Add(myattr,true);
			}
		}
        private void AddRefAttrName(Attr attr)
        {
            if (attr.MyFieldType == FieldType.FK
                || attr.MyFieldType == FieldType.Enum
                || attr.MyFieldType == FieldType.PKEnum
                || attr.MyFieldType == FieldType.PKFK)
            {

                Attr myattr = new Attr();
                myattr.MyFieldType = FieldType.Normal;
                myattr.MyDataType = DataType.AppString;
                myattr.UIContralType = UIContralType.TB;
                myattr.UIWidth = attr.UIWidth * 2;

                myattr.Key = attr.Key + "Name";
                myattr.Field = attr.Key + "Name";

                myattr.MaxLength = 200;
                myattr.MinLength = 0;

                myattr.UIVisible = false;
                myattr.UIIsReadonly = true;

                myattr.Desc = myattr.Desc = "Name";
                this.InnerList.Add(myattr);
            }
        }

		/// <summary>
		/// �����������ʼ����ڵ�Ԫ��Attr��
		/// </summary>
		public Attr this[int index]
		{			
			get
			{	
				return (Attr)this.InnerList[index];
			}
		}
	}	
}
