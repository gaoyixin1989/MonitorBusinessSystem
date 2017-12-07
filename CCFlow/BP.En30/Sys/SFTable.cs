using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
	/// <summary>
	/// �û��Զ����
	/// </summary>
    public class SFTableAttr : EntityNoNameAttr
    {
        /// <summary>
        /// �Ƿ����ɾ��
        /// </summary>
        public const string IsDel = "IsDel";
        /// <summary>
        /// �ֶ�
        /// </summary>
        public const string FK_Val = "FK_Val";
        /// <summary>
        /// ���ݱ�����
        /// </summary>
        public const string TableDesc = "TableDesc";
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public const string DefVal = "DefVal";
        /// <summary>
        /// ����Դ
        /// </summary>
        public const string DBSrc = "DBSrc";
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public const string IsTree = "IsTree";

        /// <summary>
        /// �ֵ������
        /// </summary>
	    public const string SFTableType = "SFTableType";

        #region ���ӵ�����ϵͳ��ȡ���ݵ����ԡ�
        /// <summary>
        /// ����Դ
        /// </summary>
        public const string FK_SFDBSrc = "FK_SFDBSrc";
        /// <summary>
        /// ����Դ��
        /// </summary>
        public const string SrcTable = "SrcTable";
        /// <summary>
        /// ��ʾ��ֵ
        /// </summary>
        public const string ColumnValue = "ColumnValue";
        /// <summary>
        /// ��ʾ������
        /// </summary>
        public const string ColumnText = "ColumnText";
        /// <summary>
        /// �����ֵ
        /// </summary>
        public const string ParentValue = "ParentValue";
        /// <summary>
        /// ��ѯ���
        /// </summary>
	    public const string SelectStatement = "SelectStatement";
        #endregion ���ӵ�����ϵͳ��ȡ���ݵ����ԡ�

    }

	/// <summary>
	/// �û��Զ����
	/// </summary>
    public class SFTable : EntityNoName
    {
        #region ���ӵ�����ϵͳ��ȡ���ݵ�����
        /// <summary>
        /// ����Դ
        /// </summary>
        public string FK_SFDBSrc
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.FK_SFDBSrc);
            }
            set
            {
                this.SetValByKey(SFTableAttr.FK_SFDBSrc, value);
            }
        }
        /// <summary>
        /// ���������
        /// </summary>
        public string SrcTable
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.SrcTable);
            }
            set
            {
                this.SetValByKey(SFTableAttr.SrcTable, value);
            }
        }
        /// <summary>
        /// ֵ/�����ֶ���
        /// </summary>
        public string ColumnValue
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.ColumnValue);
            }
            set
            {
                this.SetValByKey(SFTableAttr.ColumnValue, value);
            }
        }
        /// <summary>
        /// ��ʾ�ֶ�/��ʾ�ֶ���
        /// </summary>
        public string ColumnText
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.ColumnText);
            }
            set
            {
                this.SetValByKey(SFTableAttr.ColumnText, value);
            }
        }
        /// <summary>
        /// ������ֶ���
        /// </summary>
        public string ParentValue
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.ParentValue);
            }
            set
            {
                this.SetValByKey(SFTableAttr.ParentValue, value);
            }
        }

        /// <summary>
        /// ��ѯ���
        /// </summary>
        public string SelectStatement
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.SelectStatement);
            }
            set
            {
                this.SetValByKey(SFTableAttr.SelectStatement, value);
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsClass
        {
            get
            {
                if (this.No.Contains("."))
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// �Ƿ�������ʵ��?
        /// </summary>
        public bool IsTree
        {
            get
            {
                return this.GetValBooleanByKey(SFTableAttr.IsTree);
            }
            set
            {
                this.SetValByKey(SFTableAttr.IsTree, value);
            }
        }

        /// <summary>
        /// �ֵ������
        /// <remarks>0��NoName����</remarks>
        /// <remarks>1��NoNameTree����</remarks>
        /// <remarks>2��NoName������������</remarks>
        /// </summary>
	    public int SFTableType
	    {
	        get
	        {
	            return this.GetValIntByKey(SFTableAttr.SFTableType);
	        }
            set
            {
                this.SetValByKey(SFTableAttr.SFTableType, value);
            }
	    }

        /// <summary>
        /// ֵ
        /// </summary>
        public string FK_Val
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.FK_Val);
            }
            set
            {
                this.SetValByKey(SFTableAttr.FK_Val, value);
            }
        }
        public string TableDesc
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.TableDesc);
            }
            set
            {
                this.SetValByKey(SFTableAttr.TableDesc, value);
            }
        }
        public string DefVal
        {
            get
            {
                return this.GetValStringByKey(SFTableAttr.DefVal);
            }
            set
            {
                this.SetValByKey(SFTableAttr.DefVal, value);
            }
        }
        public EntitiesNoName HisEns
        {
            get
            {
                if (this.IsClass)
                {
                    EntitiesNoName ens = (EntitiesNoName)BP.En.ClassFactory.GetEns(this.No);
                    ens.RetrieveAll();
                    return ens;
                }

                BP.En.GENoNames ges = new GENoNames(this.No, this.Name);
                ges.RetrieveAll();
                return ges;
            }
        }
        #endregion

        #region ���췽��
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenForSysAdmin();
                uac.IsInsert = false;
                return uac;
            }
        }
        /// <summary>
        /// �û��Զ����
        /// </summary>
        public SFTable()
        {
        }
        public SFTable(string mypk)
        {
            this.No = mypk;
            try
            {
                this.Retrieve();
            }
            catch (Exception ex)
            {
                switch (this.No)
                {
                    case "BP.Pub.NYs":
                        this.Name = "����";
                      //  this.HisSFTableType = SFTableType.ClsLab;
                        this.FK_Val = "FK_NY";
                     //   this.IsEdit = true;
                        this.Insert();
                        break;
                    case "BP.Pub.YFs":
                        this.Name = "��";
                      //  this.HisSFTableType = SFTableType.ClsLab;
                        this.FK_Val = "FK_YF";
                       // this.IsEdit = true;
                        this.Insert();
                        break;
                    case "BP.Pub.Days":
                        this.Name = "��";
                     //   this.HisSFTableType = SFTableType.ClsLab;
                        this.FK_Val = "FK_Day";
                        //this.IsEdit = true;
                        this.Insert();
                        break;
                    case "BP.Pub.NDs":
                        this.Name = "��";
                     //   this.HisSFTableType = SFTableType.ClsLab;
                        this.FK_Val = "FK_ND";
                       // this.IsEdit = true;
                        this.Insert();
                        break;
                    default:
                        throw new Exception(ex.Message);
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
                Map map = new Map("Sys_SFTable");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;
                map.EnDesc = "�ֵ��";
                map.EnType = EnType.Sys;

                map.AddTBStringPK(SFTableAttr.No, null, "��Ӣ������", true, false, 1, 20, 20);
                map.AddTBString(SFTableAttr.Name, null, "����������", true, false, 0, 30, 20);
                map.AddTBString(SFTableAttr.FK_Val, null, "Ĭ�ϴ������ֶ���", true, false, 0, 50, 20);
                map.AddTBString(SFTableAttr.TableDesc, null, "������", true, false, 0, 50, 20);
                map.AddTBString(SFTableAttr.DefVal, null, "Ĭ��ֵ", true, false, 0, 200, 20);
                map.AddBoolean(SFTableAttr.IsTree, false, "�Ƿ�����ʵ��", true, true);
                map.AddDDLSysEnum(SFTableAttr.SFTableType, 0, "�ֵ������", true, false, SFTableAttr.SFTableType, "@0=NoName����@1=NoNameTree����@2=NoName������������");

                //����Դ.
                map.AddDDLEntities(SFTableAttr.FK_SFDBSrc, "local", "����Դ", new BP.Sys.SFDBSrcs(), true);
                map.AddTBString(SFTableAttr.SrcTable, null, "����Դ��", true, false, 0, 50, 20);
                map.AddTBString(SFTableAttr.ColumnValue, null, "��ʾ��ֵ(�����)", true, false, 0, 50, 20);
                map.AddTBString(SFTableAttr.ColumnText, null, "��ʾ������(������)", true, false, 0, 50, 20);
                map.AddTBString(SFTableAttr.ParentValue, null, "����ֵ(������)", true, false, 0, 50, 20);
                map.AddTBString(SFTableAttr.SelectStatement, null, "��ѯ���", true, false, 0, 1000, 600, true);

                //����.
                map.AddSearchAttr(SFTableAttr.FK_SFDBSrc);

                RefMethod rm = new RefMethod();
                rm.Title = "�༭����"; 
                rm.ClassMethodName = this.ToString() + ".DoEdit";
                rm.RefMethodType = RefMethodType.RightFrameOpen;
                rm.IsForEns = false;
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "����Table��";
                rm.ClassMethodName = this.ToString() + ".DoGuide";
                rm.RefMethodType = RefMethodType.RightFrameOpen;
                rm.IsForEns = false;
                map.AddRefMethod(rm);

                rm = new RefMethod();
                rm.Title = "����Դ����";
                rm.ClassMethodName = this.ToString() + ".DoMangDBSrc";
                rm.RefMethodType = RefMethodType.RightFrameOpen;
                rm.IsForEns = false;
                map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        /// <summary>
        /// ����Դ����
        /// </summary>
        /// <returns></returns>
        public string DoMangDBSrc()
        {
            return "/WF/Comm/Search.aspx?EnsName=BP.Sys.SFDBSrcs";
        }
        /// <summary>
        /// ��������
        /// </summary>
        /// <returns></returns>
        public string DoGuide()
        {
            return "/WF/Comm/Sys/SFGuide.aspx";
        }
        /// <summary>
        /// �༭����
        /// </summary>
        /// <returns></returns>
        public string DoEdit()
        {
            if (this.IsClass)
                return "/WF/Comm/Ens.aspx?EnsName=" + this.No;
            else
                return "/WF/MapDef/SFTableEditData.aspx?RefNo=" + this.No;
        }
        protected override bool beforeDelete()
        {
            MapAttrs attrs = new MapAttrs();
            attrs.Retrieve(MapAttrAttr.UIBindKey, this.No);
            if (attrs.Count != 0)
            {
                string err = "";
                foreach (MapAttr item in attrs)
                    err += " @ " + item.MyPK + " " + item.Name ;
                throw new Exception("@����ʵ���ֶ�������:"+err);
            }
            return base.beforeDelete();
        }
    }
	/// <summary>
	/// �û��Զ����s
	/// </summary>
    public class SFTables : EntitiesNoName
	{		
		#region ����
        /// <summary>
        /// �û��Զ����s
        /// </summary>
		public SFTables()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity 
		{
			get
			{
				return new SFTable();
			}
		}
		#endregion
	}
}
