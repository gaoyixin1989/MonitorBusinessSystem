using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// ����ǩ������
    /// </summary>
    public enum SignType
    {
        /// <summary>
        /// ��
        /// </summary>
        None,
        /// <summary>
        /// ͼƬ
        /// </summary>
        Pic,
        /// <summary>
        /// CAǩ��.
        /// </summary>
        CA
    }

    public enum PicType
    {
        /// <summary>
        /// �Զ�ǩ��
        /// </summary>
        ZiDong,
        /// <summary>
        /// �ֶ�ǩ��
        /// </summary>
        ShouDong
    }
    /// <summary>
    /// ʵ������
    /// </summary>
    public class MapAttrAttr : EntityMyPKAttr
    {
        /// <summary>
        /// ʵ���ʶ
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// �����
        /// </summary>
        public const string KeyOfEn = "KeyOfEn";
        /// <summary>
        /// ʵ������
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public const string DefVal = "DefVal";
        /// <summary>
        /// �ֶ�
        /// </summary>
        public const string Field = "Field";
        /// <summary>
        /// ��󳤶�
        /// </summary>
        public const string MaxLen = "MaxLen";
        /// <summary>
        /// ��С����
        /// </summary>
        public const string MinLen = "MinLen";
        /// <summary>
        /// �󶨵�ֵ
        /// </summary>
        public const string UIBindKey = "UIBindKey";
        /// <summary>
        /// �ռ�����
        /// </summary>
        public const string UIContralType = "UIContralType";
        /// <summary>
        /// ���
        /// </summary>
        public const string UIWidth = "UIWidth";
        /// <summary>
        /// UIHeight
        /// </summary>
        public const string UIHeight = "UIHeight";
        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        public const string UIIsEnable = "UIIsEnable";
        /// <summary>
        /// �����ı��Key
        /// </summary>
        public const string UIRefKey = "UIRefKey";
        /// <summary>
        /// �����ı��Lab
        /// </summary>
        public const string UIRefKeyText = "UIRefKeyText";
        /// <summary>
        /// �Ƿ�ɼ���
        /// </summary>
        public const string UIVisible = "UIVisible";
        /// <summary>
        /// �Ƿ񵥶�����ʾ
        /// </summary>
        public const string UIIsLine = "UIIsLine";
        /// <summary>
        /// ���
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// ��ʶ�������ʱ���ݣ�
        /// </summary>
        public const string Tag = "Tag";
        /// <summary>
        /// MyDataType
        /// </summary>
        public const string MyDataType = "MyDataType";
        /// <summary>
        /// �߼�����
        /// </summary>
        public const string LGType = "LGType";
        /// <summary>
        /// �༭����
        /// </summary>
        public const string EditType = "EditType";
        /// <summary>
        /// �Զ���д����
        /// </summary>
        public const string AutoFullDoc = "AutoFullDoc";
        /// <summary>
        /// �Զ���д��ʽ
        /// </summary>
        public const string AutoFullWay = "AutoFullWay";
        /// <summary>
        /// GroupID
        /// </summary>
        public const string GroupID = "GroupID";
        /// <summary>
        /// �Ƿ���ǩ��
        /// </summary>
        public const string IsSigan = "IsSigan";
        /// <summary>
        /// �����С
        /// </summary>
        public const string FontSize = "FontSize";
        /// <summary>
        /// x
        /// </summary>
        public const string X = "X";
        /// <summary>
        /// y
        /// </summary>
        public const string Y = "Y";
        /// <summary>
        /// TabIdx
        /// </summary>
        public const string TabIdx = "TabIdx";
        /// <summary>
        /// GUID
        /// </summary>
        public const string GUID = "GUID";
        /// <summary>
        /// �ϲ���Ԫ����
        /// </summary>
        public const string ColSpan = "ColSpan";
        /// <summary>
        /// ǩ���ֶ�
        /// </summary>
        public const string SiganField = "SiganField";
        /// <summary>
        /// �Ƿ��Զ�ǩ��
        /// </summary>
        public const string PicType = "PicType";
    }
    /// <summary>
    /// ʵ������
    /// </summary>
    public class MapAttr : EntityMyPK
    {
        #region ����
        public EntitiesNoName HisEntitiesNoName
        {
            get
            {
                if (this.UIBindKey.Contains("."))
                {
                    EntitiesNoName ens = (EntitiesNoName)BP.En.ClassFactory.GetEns(this.UIBindKey);
                    ens.RetrieveAll();
                    return ens;
                }
                GENoNames myens = new GENoNames(this.UIBindKey, this.Name);
                myens.RetrieveAll();
                return myens;
            }
        }
        /// <summary>
        /// �Ƿ��ǵ���������ֶ�
        /// </summary>
        public bool IsTableAttr
        {
            get
            {
                return DataType.IsNumStr(this.KeyOfEn.Replace("F", ""));
            }
        }
        public Attr HisAttr
        {
            get
            {
                Attr attr = new Attr();
                attr.Key = this.KeyOfEn;
                attr.Desc = this.Name;

                string s = this.DefValReal;
                if (string.IsNullOrEmpty(s))
                    attr.DefaultValOfReal = null;
                else
                {
                    // attr.DefaultVal
                    attr.DefaultValOfReal = this.DefValReal;
                    //this.DefValReal;
                }


                attr.Field = this.Field;
                attr.MaxLength = this.MaxLen;
                attr.MinLength = this.MinLen;
                attr.UIBindKey = this.UIBindKey;
                attr.UIIsLine = this.UIIsLine;
                attr.UIHeight = 0; 
                if (this.UIHeight > 30)
                    attr.UIHeight = (int)this.UIHeight;

                attr.UIWidth = this.UIWidth;
                attr.MyDataType = this.MyDataType;
                attr.UIRefKeyValue = this.UIRefKey;
                attr.UIRefKeyText = this.UIRefKeyText;
                attr.UIVisible = this.UIVisible;
                if (this.IsPK)
                    attr.MyFieldType = FieldType.PK;

                switch (this.LGType)
                {
                    case FieldTypeS.Enum:
                        attr.UIContralType = this.UIContralType;
                        attr.MyFieldType = FieldType.Enum;
                        attr.UIDDLShowType = BP.Web.Controls.DDLShowType.SysEnum;
                        attr.UIIsReadonly = this.UIIsEnable;
                        break;
                    case FieldTypeS.FK:
                        attr.UIContralType = this.UIContralType;
                        attr.MyFieldType = FieldType.FK;
                        attr.UIDDLShowType = BP.Web.Controls.DDLShowType.Ens;
                        attr.UIRefKeyValue = "No";
                        attr.UIRefKeyText = "Name";
                        attr.UIIsReadonly = this.UIIsEnable;
                        break;
                    default:
                        attr.UIContralType = UIContralType.TB;
                        if (this.IsPK)
                            attr.MyFieldType = FieldType.PK;

                        attr.UIIsReadonly = !this.UIIsEnable;
                        switch (this.MyDataType)
                        {
                            case DataType.AppBoolean:
                                attr.UIContralType = UIContralType.CheckBok;
                                attr.UIIsReadonly = this.UIIsEnable;
                                break;
                            case DataType.AppDate:
                                if (this.Tag == "1")
                                    attr.DefaultVal = DataType.CurrentData;
                                break;
                            case DataType.AppDateTime:
                                if (this.Tag == "1")
                                    attr.DefaultVal = DataType.CurrentData;
                                break;
                            default:
                                break;
                        }
                        break;
                }

                //attr.AutoFullWay = this.HisAutoFull;
                //attr.AutoFullDoc = this.AutoFullDoc;
                //attr.MyFieldType = FieldType
                //attr.UIDDLShowType= BP.Web.Controls.DDLShowType.Self

                return attr;
            }
        }
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsPK
        {
            get
            {
                switch (this.KeyOfEn)
                {
                    case "OID":
                    case "No":
                    case "MyPK":
                        return true;
                    default:
                        return false;
                }
            }
        }
        public EditType HisEditType
        {
            get
            {
                return (EditType)this.GetValIntByKey(MapAttrAttr.EditType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.EditType, (int)value);
            }
        }
        public string FK_MapData
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// AutoFullWay
        /// </summary>
        public AutoFullWay HisAutoFull_del
        {
            get
            {
                return (AutoFullWay)this.GetValIntByKey(MapAttrAttr.AutoFullWay);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.AutoFullWay, (int)value);
            }
        }
        /// <summary>
        /// �Զ���д
        /// </summary>
        public string AutoFullDoc_Del
        {
            get
            {
                string doc = this.GetValStrByKey(MapAttrAttr.AutoFullDoc);
                doc = doc.Replace("~", "'");
                return doc;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.AutoFullDoc, value);
            }
        }
        public string AutoFullDocRun_Del
        {
            get
            {
                string doc = this.GetValStrByKey(MapAttrAttr.AutoFullDoc);
                doc = doc.Replace("~", "'");
                doc = doc.Replace("@WebUser.No", BP.Web.WebUser.No);
                doc = doc.Replace("@WebUser.Name", BP.Web.WebUser.Name);
                doc = doc.Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);
                return doc;
            }
        }
        public string KeyOfEn
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.KeyOfEn);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.KeyOfEn, value);
            }
        }
        public FieldTypeS LGType
        {
            get
            {
                return (FieldTypeS)this.GetValIntByKey(MapAttrAttr.LGType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.LGType, (int)value);
            }
        }
        public string LGTypeT
        {
            get
            {
                return this.GetValRefTextByKey(MapAttrAttr.LGType);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                string s = this.GetValStrByKey(MapAttrAttr.Name);
                if (s == "" || s == null)
                    return this.KeyOfEn;
                return s;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.Name, value);
            }
        }
        public bool IsNum
        {
            get
            {
                switch (this.MyDataType)
                {
                    case BP.DA.DataType.AppString:
                    case BP.DA.DataType.AppDate:
                    case BP.DA.DataType.AppDateTime:
                    case BP.DA.DataType.AppBoolean:
                        return false;
                    default:
                        return true;
                }
            }
        }
        public decimal DefValDecimal
        {
            get
            {
                return decimal.Parse(this.DefVal);
            }
        }
        public string DefValReal
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.DefVal);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.DefVal, value);
            }
        }
        /// <summary>
        /// �ϲ���Ԫ����
        /// </summary>
        public int ColSpan
        {
            get
            {
                int i= this.GetValIntByKey(MapAttrAttr.ColSpan);
                if (this.UIIsLine && i ==1)
                    return 3;
                if (i == 0)
                    return 1;
                return i;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.ColSpan, value);
            }
        }
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefVal
        {
            get
            {
                string s = this.GetValStrByKey(MapAttrAttr.DefVal);
                if (this.IsNum)
                {
                    if (s == "")
                        return "0";
                }

                switch (this.MyDataType)
                {
                    case BP.DA.DataType.AppDate:
                        if (this.Tag == "1" || s == "@RDT")
                            return DataType.CurrentData;
                        else
                            return "          ";
                        break;
                    case BP.DA.DataType.AppDateTime:
                        if (this.Tag == "1" || s == "@RDT")
                            return DataType.CurrentDataTime;
                        else
                            return "               ";
                        //return "    -  -    :  ";
                        break;
                    default:
                        break;
                }

                if (s.Contains("@") == false)
                    return s;

                switch (s.ToLower())
                {
                    case "@webuser.no":
                        return BP.Web.WebUser.No;
                    case "@webuser.name":
                        return BP.Web.WebUser.Name;
                    case "@webuser.fk_dept":
                        return BP.Web.WebUser.FK_Dept;
                    case "@webuser.fk_deptname":
                        return BP.Web.WebUser.FK_DeptName;
                    case "@webuser.fk_deptnameoffull":
                        return BP.Web.WebUser.FK_DeptNameOfFull;
                    case "@fk_ny":
                        return DataType.CurrentYearMonth;
                    case "@fk_nd":
                        return DataType.CurrentYear;
                    case "@fk_yf":
                        return DataType.CurrentMonth;
                    case "@rdt":
                        if (this.MyDataType == DataType.AppDate)
                            return DataType.CurrentData;
                        else
                            return DataType.CurrentDataTime;
                    case "@rd":
                        if (this.MyDataType == DataType.AppDate)
                            return DataType.CurrentData;
                        else
                            return DataType.CurrentDataTime;
                    case "@yyyy��mm��dd��":
                        return DataType.CurrentDataCNOfLong;
                    case "@yy��mm��dd��":
                        return DataType.CurrentDataCNOfShort;
                    default:
                        return s;
                    //throw new Exception("û��Լ���ı���Ĭ��ֵ����" + s);
                }
                return this.GetValStrByKey(MapAttrAttr.DefVal);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.DefVal, value);
            }
        }
        public bool DefValOfBool
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.DefVal, false);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.DefVal, value);
            }
        }
        /// <summary>
        /// �ֶ�
        /// </summary>
        public string Field
        {
            get
            {
                return this.KeyOfEn;
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
        public int MyDataType
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.MyDataType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.MyDataType, value);
            }
        }
        public string MyDataTypeS
        {
            get
            {
                switch (this.MyDataType)
                {
                    case DataType.AppString:
                        return "String";
                    case DataType.AppInt:
                        return "Int";
                    case DataType.AppFloat:
                        return "Float";
                    case DataType.AppMoney:
                        return "Money";
                    case DataType.AppDate:
                        return "Date";
                    case DataType.AppDateTime:
                        return "DateTime";
                    case DataType.AppBoolean:
                        return "Bool";
                    default:
                        throw new Exception("û���жϡ�");
                }
            }
            set
            {

                switch (value)
                {
                    case "String":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppString);
                        break;
                    case "Int":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppInt);
                        break;
                    case "Float":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppFloat);
                        break;
                    case "Money":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppMoney);
                        break;
                    case "Date":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppDate);
                        break;
                    case "DateTime":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppDateTime);
                        break;
                    case "Bool":
                        this.SetValByKey(MapAttrAttr.MyDataType, DataType.AppBoolean);
                        break;
                    default:
                        throw new Exception("sdsdsd");
                }

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
        /// ��󳤶�
        /// </summary>
        public int MaxLen
        {
            get
            {
                switch (this.MyDataType)
                {
                    case DataType.AppDate:
                        return 100;
                    case DataType.AppDateTime:
                        return 100;
                    default:
                        break;
                }

                int i = this.GetValIntByKey(MapAttrAttr.MaxLen);
                if (i > 4000)
                    i = 400;
                return i;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.MaxLen, value);
            }
        }
        /// <summary>
        /// ��С����
        /// </summary>
        public int MinLen
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.MinLen);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.MinLen, value);
            }
        }
        /// <summary>
        /// �Ƿ����Ϊ��, ����ֵ���͵�������Ч.
        /// </summary>
        public bool IsNull
        {
            get
            {
                if (this.MinLen == 0)
                    return false;
                else
                    return true;
            }
        }
        public int GroupID
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.GroupID);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.GroupID, value);
            }
        }
        public bool IsBigDoc
        {
            get
            {
                if (this.MaxLen > 3000)
                    return true;
                return false;
            }
        }
        public int UIRows
        {
            get
            {
                if (this.UIHeight < 40)
                    return 1;

                decimal d = decimal.Parse(this.UIHeight.ToString()) / 16;
                return (int)Math.Round(d, 0);
            }
        }
        /// <summary>
        /// �߶�
        /// </summary>
        public int UIHeightInt
        {
            get
            {
                return (int)this.UIHeight;
            }
        }
        /// <summary>
        /// �߶�
        /// </summary>
        public float UIHeight
        {
            get
            {
                return this.GetValFloatByKey(MapAttrAttr.UIHeight);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIHeight, value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public int UIWidthInt
        {
            get
            {
                return (int)this.UIWidth;
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public float UIWidth
        {
            get
            {
                //switch (this.MyDataType)
                //{
                //    case DataType.AppString:
                //        return this.GetValFloatByKey(MapAttrAttr.UIWidth);
                //    case DataType.AppFloat:
                //    case DataType.AppInt:
                //    case DataType.AppMoney:
                //    case DataType.AppRate:
                //    case DataType.AppDouble:
                //        return 80;
                //    case DataType.AppDate:
                //        return 75;
                //    case DataType.AppDateTime:
                //        return 112;
                //    default:
                //        return 70;
                return this.GetValFloatByKey(MapAttrAttr.UIWidth);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIWidth, value);
            }
        }
        public int UIWidthOfLab
        {
            get
            {
                return 0;

                //Graphics2D g2 = (Graphics2D)g;
                //g2.setRenderingHint(RenderingHints.KEY_ANTIALIASING,
                //                        RenderingHints.VALUE_ANTIALIAS_ON);

                //int textWidth = getFontMetrics(g2.getFont()).bytesWidth(str.getBytes(), 0, str.getBytes().length); 

            }
        }
        /// <summary>
        /// �Ƿ�ֻ��
        /// </summary>
        public bool UIVisible
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.UIVisible);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIVisible, value);
            }
        }
        /// <summary>
        /// �Ƿ����
        /// </summary>
        public bool UIIsEnable
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.UIIsEnable);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIIsEnable, value);
            }
        }
        /// <summary>
        /// �Ƿ񵥶�����ʾ
        /// </summary>
        public bool UIIsLine
        {
            get
            {
                return this.GetValBooleanByKey(MapAttrAttr.UIIsLine);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIIsLine, value);
            }
        }
        /// <summary>
        /// �Ƿ�����ǩ��
        /// </summary>
        public bool IsSigan
        {
            get
            {
                if (this.UIIsEnable)
                    return false;
                return this.GetValBooleanByKey(MapAttrAttr.IsSigan);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.IsSigan, value);
            }
        }
     /// <summary>
        /// ǩ������
        /// </summary>
        public SignType SignType
        {
            get
            {
                if (this.UIIsEnable)
                    return SignType.None;
                return (SignType)this.GetValIntByKey(MapAttrAttr.IsSigan);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.IsSigan, (int)value);
            }
        }
        public int Para_FontSize
        {
            get
            {
                return this.GetParaInt(MapAttrAttr.FontSize);
            }
            set
            {
                this.SetPara(MapAttrAttr.FontSize, value);
            }
        }
        
        /// <summary>
        /// �Ƿ�����ǩ��
        /// </summary>
        public string Para_SiganField
        {
            get
            {
                if (this.UIIsEnable)
                    return "";
                return this.GetParaString(MapAttrAttr.SiganField);
            }
            set
            {
                this.SetPara(MapAttrAttr.SiganField, value);
            }
        }

        /// <summary>
        /// ǩ������
        /// </summary>
        public PicType PicType
        {
            get
            {
                if (this.UIIsEnable)
                    return PicType.ZiDong;
                return (PicType)this.GetParaInt(MapAttrAttr.PicType);
            }
            set
            {
                this.SetPara(MapAttrAttr.PicType, (int)value);

            }
        }
        /// <summary>
        /// TextBox����
        /// </summary>
        public int TBModel
        {
            get
            {
                string s= this.GetValStrByKey(MapAttrAttr.UIBindKey);
                if (string.IsNullOrEmpty(s) || s.Length != 1)
                    return 0;
                else
                    return int.Parse(s);
            }
          
        }
        /// <summary>
        /// �󶨵�ֵ
        /// </summary>
        public string UIBindKey
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.UIBindKey);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIBindKey, value);
            }
        }
        /// <summary>
        /// �����ı��Key
        /// </summary>
        public string UIRefKey
        {
            get
            {
                string s = this.GetValStrByKey(MapAttrAttr.UIRefKey);
                if (s == "" || s == null)
                    s = "No";
                return s;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIRefKey, value);
            }
        }
        /// <summary>
        /// �����ı��Lab
        /// </summary>
        public string UIRefKeyText
        {
            get
            {
                string s = this.GetValStrByKey(MapAttrAttr.UIRefKeyText);
                if (s == "" || s == null)
                    s = "Name";
                return s;
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIRefKeyText, value);
            }
        }
        /// <summary>
        /// ��ʶ
        /// </summary>
        public string Tag
        {
            get
            {
                return this.GetValStrByKey(MapAttrAttr.Tag);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.Tag, value);
            }
        }
        /// <summary>
        /// �ؼ�����
        /// </summary>
        public UIContralType UIContralType
        {
            get
            {
                return (UIContralType)this.GetValIntByKey(MapAttrAttr.UIContralType);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.UIContralType, (int)value);
            }
        }
        public string F_Desc
        {
            get
            {
                switch (this.MyDataType)
                {
                    case DataType.AppString:
                        if (this.UIVisible == false)
                            return "����" + this.MinLen + "-" + this.MaxLen + "���ɼ�";
                        else
                            return "����" + this.MinLen + "-" + this.MaxLen;
                    case DataType.AppDate:
                    case DataType.AppDateTime:
                    case DataType.AppInt:
                    case DataType.AppFloat:
                    case DataType.AppMoney:
                        if (this.UIVisible == false)
                            return "���ɼ�";
                        else
                            return "";
                    default:
                        return "";
                }
            }
        }
        /// <summary>
        /// TabIdx
        /// </summary>
        public int TabIdx
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.TabIdx);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.TabIdx, value);
            }
        }
        /// <summary>
        /// ���
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(MapAttrAttr.Idx);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.Idx, value);
            }
        }

        public float X
        {
            get
            {
                return this.GetValFloatByKey(MapAttrAttr.X);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.X, value);
            }
        }
        public float Y
        {
            get
            {
                return this.GetValFloatByKey(MapAttrAttr.Y);
            }
            set
            {
                this.SetValByKey(MapAttrAttr.Y, value);
            }
        }
        #endregion

        #region ���췽��b
        /// <summary>
        /// ʵ������
        /// </summary>
        public MapAttr()
        {
        }
        public MapAttr(string mypk)
        {
            this.MyPK = mypk;
            this.Retrieve();
        }
        public MapAttr(string fk_mapdata, string key)
        {
            this.FK_MapData = fk_mapdata;
            this.KeyOfEn = key;
            this.Retrieve(MapAttrAttr.FK_MapData, this.FK_MapData, MapAttrAttr.KeyOfEn, this.KeyOfEn);
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

                Map map = new Map("Sys_MapAttr");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "ʵ������";
                map.EnType = EnType.Sys;

                map.AddMyPK();

                map.AddTBString(MapAttrAttr.FK_MapData, null, "ʵ���ʶ", true, true, 1, 200, 20);
                map.AddTBString(MapAttrAttr.KeyOfEn, null, "����", true, true, 1, 200, 20);

                map.AddTBString(MapAttrAttr.Name, null, "����", true, false, 0, 200, 20);
                map.AddTBString(MapAttrAttr.DefVal, null, "Ĭ��ֵ", false, false, 0, 4000, 20);

             //   map.AddDDLSysEnum(MapAttrAttr.UIContralType, 0, "�ռ�����", true, false, MapAttrAttr.UIContralType, "@0=�ı���@1=������");
             //   map.AddDDLSysEnum(MapAttrAttr.MyDataType, 0, "��������", true, false, MapAttrAttr.MyDataType,
               //     "@1=�ı�(String)@2=����(Int)@3=����(Float)@4=����@5=Double@6=AppDate@7=AppDateTime@8=AppMoney@9=AppRate");

                map.AddTBInt(MapAttrAttr.UIContralType, 0, "�ؼ�", true, false);
                map.AddTBInt(MapAttrAttr.MyDataType, 0, "��������", true, false);

                map.AddDDLSysEnum(MapAttrAttr.LGType, 0, "�߼�����", true, false, MapAttrAttr.LGType, 
                    "@0=��ͨ@1=ö��@2=���");

                map.AddTBFloat(MapAttrAttr.UIWidth, 100, "���", true, false);
                map.AddTBFloat(MapAttrAttr.UIHeight, 23, "�߶�", true, false);

                map.AddTBInt(MapAttrAttr.MinLen, 0, "��С����", true, false);
                map.AddTBInt(MapAttrAttr.MaxLen, 300, "��󳤶�", true, false);

                map.AddTBString(MapAttrAttr.UIBindKey, null, "�󶨵���Ϣ", true, false, 0, 100, 20);
                map.AddTBString(MapAttrAttr.UIRefKey, null, "�󶨵�Key", true, false, 0, 30, 20);
                map.AddTBString(MapAttrAttr.UIRefKeyText, null, "�󶨵�Text", true, false, 0, 30, 20);

                //map.AddTBInt(MapAttrAttr.UIVisible, 1, "�Ƿ�ɼ�", true, true);
                //map.AddTBInt(MapAttrAttr.UIIsEnable, 1, "�Ƿ�����", true, true);
                //map.AddTBInt(MapAttrAttr.UIIsLine, 0, "�Ƿ񵥶�����ʾ", true, true);

                map.AddBoolean(MapAttrAttr.UIVisible, true, "�Ƿ�ɼ�", true, true);
                map.AddBoolean(MapAttrAttr.UIIsEnable, true, "�Ƿ�����", true, true);
                map.AddBoolean(MapAttrAttr.UIIsLine, false, "�Ƿ񵥶�����ʾ", true, true);


               // map.AddTBString(MapAttrAttr.AutoFullDoc, null, "�Զ���д����", false, false, 0, 500, 20);
               //// map.AddDDLSysEnum(MapAttrAttr.AutoFullWay, 0, "�Զ���д��ʽ", true, false, MapAttrAttr.AutoFullWay,
               //  //   "@0=������@1=���������ݼ���@2=����SQL�Զ����@3=�����������@4=�Դӱ������ֵ");
               // map.AddTBInt(MapAttrAttr.AutoFullWay, 0, "�Զ���д��ʽ", true, false);


                map.AddTBInt(MapAttrAttr.Idx, 0, "���", true, false);
                map.AddTBInt(MapAttrAttr.GroupID, 0, "GroupID", true, false);

                //      map.AddTBInt(MapAttrAttr.TabIdx, 0, "Tab˳���", true, false);

                // �Ƿ���ǩ�֣�����Ա�ֶ���Ч��2010-09-23 ���ӡ� @0=��@1=ͼƬǩ��@2=CAǩ��.
                map.AddTBInt(MapAttrAttr.IsSigan, 0, "ǩ�֣�", true, false);
             
                map.AddTBFloat(MapAttrAttr.X, 5, "X", true, false);
                map.AddTBFloat(MapAttrAttr.Y, 5, "Y", false, false);

                map.AddTBString(FrmBtnAttr.GUID, null, "GUID", true, false, 0, 128, 20);

                map.AddTBString(MapAttrAttr.Tag, null, "��ʶ�������ʱ���ݣ�", true, false, 0, 100, 20);
                map.AddTBInt(MapAttrAttr.EditType, 0, "�༭����", true, false);

                //��Ԫ��������2013-07-24 ���ӡ�
                map.AddTBInt(MapAttrAttr.ColSpan, 1, "��Ԫ������", true, false);


                //��������.
                map.AddTBAtParas(4000); //


                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

      
        public void DoDownTabIdx()
        {
            this.DoOrderDown(MapAttrAttr.FK_MapData, this.FK_MapData, MapAttrAttr.Idx);
        }
        public void DoUpTabIdx()
        {
            this.DoOrderUp(MapAttrAttr.FK_MapData, this.FK_MapData, MapAttrAttr.Idx);
        }
        public void DoUp()
        {
            this.DoOrderUp(MapAttrAttr.GroupID, this.GroupID.ToString(), MapAttrAttr.UIVisible, "1", MapAttrAttr.Idx);
            MapAttr attr = new MapAttr();
            attr.MyPK = this.FK_MapData + "_Title";
            if (attr.RetrieveFromDBSources() == 1)
            {
                attr.Idx = -1;
                attr.Update();
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public void DoDown()
        {
            this.DoOrderDown(MapAttrAttr.GroupID, this.GroupID.ToString(), MapAttrAttr.UIVisible, "1", MapAttrAttr.Idx);

            MapAttr attr = new MapAttr();
            attr.MyPK = this.FK_MapData + "_Title";
            if (attr.RetrieveFromDBSources() == 1)
            {
                attr.Idx = -1;
                attr.Update();
            }
        }
        public void DoDtlDown()
        {
            try
            {
                string sql = "UPDATE Sys_MapAttr SET GroupID=( SELECT OID FROM Sys_GroupField WHERE EnName='" + this.FK_MapData + "') WHERE FK_MapData='" + this.FK_MapData + "'";
                DBAccess.RunSQL(sql);
            }
            catch
            {
            }

            this.DoDown();
        }
        public void DoDtlUp()
        {
            try
            {
                string sql = "UPDATE Sys_MapAttr SET GroupID=( SELECT OID FROM Sys_GroupField WHERE EnName='" + this.FK_MapData + "') WHERE FK_MapData='" + this.FK_MapData + "'";
                DBAccess.RunSQL(sql);
            }
            catch
            {
            }
            this.DoUp();
        }
        public void DoJump(MapAttr attrTo)
        {
            if (attrTo.Idx <= this.Idx)
                this.DoJumpUp(attrTo);
            else
                this.DoJumpDown(attrTo);
        }
        private string DoJumpUp(MapAttr attrTo)
        {
            string sql = "UPDATE Sys_MapAttr SET Idx=Idx+1 WHERE Idx <=" + attrTo.Idx + " AND FK_MapData='" + this.FK_MapData + "' AND GroupID=" + this.GroupID;
            DBAccess.RunSQL(sql);
            this.Idx = attrTo.Idx - 1;
            this.GroupID = attrTo.GroupID;
            this.Update();
            return null;
        }
        private string DoJumpDown(MapAttr attrTo)
        {
            string sql = "UPDATE Sys_MapAttr SET Idx=Idx-1 WHERE Idx <=" + attrTo.Idx + " AND FK_MapData='" + this.FK_MapData + "' AND GroupID=" + this.GroupID;
            DBAccess.RunSQL(sql);
            this.Idx = attrTo.Idx + 1;
            this.GroupID = attrTo.GroupID;
            this.Update();
            return null;
        }
        protected override bool beforeUpdateInsertAction()
        {
            if (this.LGType == FieldTypeS.Normal)
                if (this.UIIsEnable == true &&this.DefVal !=null &&  this.DefVal.Contains("@") == true)
                    throw new Exception("@�����ڷ�ֻ��(���ɱ༭)���ֶ����þ���@��Ĭ��ֵ. �����õ�Ĭ��ֵΪ:" + this.DefVal);
            
            return base.beforeUpdateInsertAction();
        }
        protected override bool beforeUpdate()
        {
            switch (this.MyDataType)
            {
                case DataType.AppDateTime:
                    this.MaxLen = 20;
                    break;
                case DataType.AppDate:
                    this.MaxLen = 10;
                    break;
                default:
                    break;
            }
            this.MyPK = this.FK_MapData + "_" + this.KeyOfEn;
            return base.beforeUpdate();
        }
        protected override bool beforeInsert()
        {
            if (string.IsNullOrEmpty(this.Name))
                throw new Exception("@�������ֶ����ơ�");
            
            if (this.KeyOfEn == null || this.KeyOfEn.Trim() == "")
            {
                try
                {
                    this.KeyOfEn = BP.DA.DataType.ParseStringToPinyin(this.Name);
                    if (this.KeyOfEn.Length > 20)
                        this.KeyOfEn = BP.DA.DataType.ParseStringToPinyinWordFirst(this.Name);

                    if (this.KeyOfEn == null || this.KeyOfEn.Trim() == "")
                        throw new Exception("@�������ֶ����������ֶ����ơ�");
                }
                catch (Exception ex)
                {
                    throw new Exception("@�������ֶ��������ֶ����ơ��쳣��Ϣ:" + ex.Message);
                }
            }
            else
            {
                this.KeyOfEn = PubClass.DealToFieldOrTableNames(this.KeyOfEn);
            }

            string keyofenC = this.KeyOfEn.Clone() as string;
            keyofenC = keyofenC.ToLower();

            if (PubClass.KeyFields.Contains("," + keyofenC + ",") == true)
                throw new Exception("@����:[" + this.KeyOfEn + "]���ֶιؼ��֣��������������ֶΡ�");

            if (this.IsExit(MapAttrAttr.KeyOfEn, this.KeyOfEn,
                MapAttrAttr.FK_MapData, this.FK_MapData))
            {
                return false;
                throw new Exception("@��["+this.MyPK+"]�Ѿ������ֶ�����[" + this.Name + "]�ֶ�[" + this.KeyOfEn + "]");
            }
             
            this.Idx = 999; // BP.DA.DBAccess.RunSQLReturnValInt("SELECT COUNT(*) FROM Sys_MapAttr WHERE FK_MapData='" + this.FK_MapData + "'") + 1;
            this.MyPK = this.FK_MapData + "_" + this.KeyOfEn;
            return base.beforeInsert();
        }
        /// <summary>
        /// ɾ��֮ǰ
        /// </summary>
        /// <returns></returns>
        protected override bool beforeDelete()
        {
            string sql = "DELETE FROM Sys_MapExt WHERE (AttrOfOper='" + this.KeyOfEn + "' OR AttrsOfActive='" + this.KeyOfEn + "' ) AND (FK_MapData='')";
            //ɾ��Ȩ�޹����ֶ�.
            sql += "@DELETE FROM Sys_FrmSln WHERE KeyOfEn='" + this.KeyOfEn + "' AND FK_MapData='"+this.FK_MapData+"'";
            BP.DA.DBAccess.RunSQLs(sql);
            return base.beforeDelete();
        }
    }
    /// <summary>
    /// ʵ������s
    /// </summary>
    public class MapAttrs : EntitiesMyPK
    {
        #region ����
        /// <summary>
        /// ʵ������s
        /// </summary>
        public MapAttrs()
        {
        }
        /// <summary>
        /// ʵ������s
        /// </summary>
        public MapAttrs(string fk_map)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MapAttrAttr.FK_MapData, fk_map);
            qo.addOrderBy(MapAttrAttr.Idx);
            qo.DoQuery();
        }
        public int SearchMapAttrsYesVisable(string fk_map)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(MapAttrAttr.FK_MapData, fk_map);
            qo.addAnd();
            qo.AddWhere(MapAttrAttr.UIVisible, 1);
            qo.addOrderBy(MapAttrAttr.GroupID, MapAttrAttr.Idx);
           // qo.addOrderBy(MapAttrAttr.Idx);
            return qo.DoQuery();
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new MapAttr();
            }
        }
        public int WithOfCtl
        {
            get
            {
                int i = 0;
                foreach (MapAttr item in this)
                {
                    if (item.UIVisible == false)
                        continue;

                    i += item.UIWidthInt;
                }
                return i;
            }
        }
        #endregion
    }
}
