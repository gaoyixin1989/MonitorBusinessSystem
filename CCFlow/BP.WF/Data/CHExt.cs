using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.En;
using BP.Port;
using BP.Web;
using BP.Sys;

namespace BP.WF.Data
{
	 
	/// <summary>
	/// ʱЧ����
	/// </summary> 
    public class CHExt : EntityMyPK
    {
        #region ��������
        /// <summary>
        /// ����״̬
        /// </summary>
        public CHSta CHSta
        {
            get
            {
                return (CHSta)this.GetValIntByKey(CHAttr.CHSta);
            }
            set
            {
                this.SetValByKey(CHAttr.CHSta, (int)value);
            }
        }
        /// <summary>
        /// ʱ�䵽
        /// </summary>
        public string DTTo
        {
            get
            {
                return this.GetValStringByKey(CHAttr.DTTo);
            }
            set
            {
                this.SetValByKey(CHAttr.DTTo, value);
            }
        }
        /// <summary>
        /// ʱ���
        /// </summary>
        public string DTFrom
        {
            get
            {
                return this.GetValStringByKey(CHAttr.DTFrom);
            }
            set
            {
                this.SetValByKey(CHAttr.DTFrom, value);
            }
        }
        /// <summary>
        /// Ӧ�������
        /// </summary>
        public string SDT
        {
            get
            {
                return this.GetValStringByKey(CHAttr.SDT);
            }
            set
            {
                this.SetValByKey(CHAttr.SDT, value);
            }
        }
        /// <summary>
        /// ���̱���
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(CHAttr.Title);
            }
            set
            {
                this.SetValByKey(CHAttr.Title, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(CHAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(CHAttr.FK_Flow, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_FlowT
        {
            get
            {
                return this.GetValStringByKey(CHAttr.FK_FlowT);
            }
            set
            {
                this.SetValByKey(CHAttr.FK_FlowT, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public float TSpan
        {
            get
            {
                return this.GetValFloatByKey(CHAttr.TSpan);
            }
            set
            {
                this.SetValByKey(CHAttr.TSpan, value);
            }
        }
        /// <summary>
        /// ������Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(CHAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(CHAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// ��Ա
        /// </summary>
        public string FK_EmpT
        {
            get
            {
                return this.GetValStringByKey(CHAttr.FK_EmpT);
            }
            set
            {
                this.SetValByKey(CHAttr.FK_EmpT, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_Dept
        {
            get
            {
                return this.GetValStrByKey(CHAttr.FK_Dept);
            }
            set
            {
                this.SetValByKey(CHAttr.FK_Dept, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string FK_DeptT
        {
            get
            {
                return this.GetValStrByKey(CHAttr.FK_DeptT);
            }
            set
            {
                this.SetValByKey(CHAttr.FK_DeptT, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string FK_NY
        {
            get
            {
                return this.GetValStrByKey(CHAttr.FK_NY);
            }
            set
            {
                this.SetValByKey(CHAttr.FK_NY, value);
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(CHAttr.WorkID);
            }
            set
            {
                this.SetValByKey(CHAttr.WorkID, value);
            }
        }
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 FID
        {
            get
            {
                return this.GetValInt64ByKey(CHAttr.FID);
            }
            set
            {
                this.SetValByKey(CHAttr.FID, value);
            }
        }
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(CHAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(CHAttr.FK_Node, value);
            }
        }
        /// <summary>
        /// �ڵ�����
        /// </summary>
        public string FK_NodeT
        {
            get
            {
                return this.GetValStrByKey(CHAttr.FK_NodeT);
            }
            set
            {
                this.SetValByKey(CHAttr.FK_NodeT, value);
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.IsDelete = false;
                uac.IsInsert = false;
                uac.IsUpdate = false;
                uac.IsView = true;
                return uac;
            }
        }
        /// <summary>
        /// ʱЧ����
        /// </summary>
        public CHExt() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk"></param>
        public CHExt(string pk)
            : base(pk)
        {
        }
        #endregion

        #region Map
        /// <summary>
        /// EnMap
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_CH");
                map.DepositaryOfMap = Depositary.None;
                map.EnDesc = "ʱЧ����";


                map.AddTBString(CHAttr.Title, null, "����", true, true, 0, 900, 5,true);

                map.AddDDLEntities(CHAttr.FK_Flow, null, "����", new Flows(), false);

                map.AddTBString(CHAttr.FK_NodeT, null, "�ڵ�����", true, true, 0, 50, 5);

                map.AddTBString(CHAttr.DTFrom, null, "ʱ���", true, true, 0, 50, 5);
                map.AddTBString(CHAttr.DTTo, null, "��", true, true, 0, 50, 5);
                map.AddTBString(CHAttr.SDT, null, "Ӧ�������", true, true, 0, 50, 5);


                map.AddTBString(CHAttr.TSpan, null, "����", true, true, 0, 50, 5);
                map.AddTBString(CHAttr.UseTime, null, "��ʱ", true, true, 0, 50, 5);
                map.AddTBString(CHAttr.OverTime, null, "����", true, true, 0, 50, 5);
                 
                map.AddDDLSysEnum(CHAttr.CHSta, 0, "״̬", true, true, CHAttr.CHSta,
                    "@0=��ʱ���@1=�������@2=�������@3=�������");

                map.AddDDLEntities(CHAttr.FK_Dept, null, "��������", new BP.Port.Depts(), false);

                map.AddDDLEntities(CHAttr.FK_Emp, null, "������", new BP.Port.Emps(), false);

                map.AddDDLEntities(CHAttr.FK_NY, null, "�·�", new BP.Pub.NYs(), false);

                map.AddTBIntMyNum();
                map.AddTBInt(CHAttr.WorkID, 0, "����ID", false, true);
                map.AddTBInt(CHAttr.FID, 0, "FID", false, false);

                map.AddTBStringPK(CHAttr.MyPK, null, "MyPK", false, false, 0, 50, 5);
                //map.AddMyPK();

                map.AddSearchAttr(CHAttr.FK_Dept);
                map.AddSearchAttr(CHAttr.FK_NY);
                map.AddSearchAttr(CHAttr.CHSta);
                map.AddSearchAttr(CHAttr.FK_Flow);

                RefMethod rm = new RefMethod();
                rm.Title = "�����̹켣";
                rm.ClassMethodName = this.ToString() + ".DoOpen";
                rm.RefMethodType = En.RefMethodType.RightFrameOpen;
                rm.Icon = "/WF/Img/FileType/doc.gif";
                rm.IsForEns = false;
                map.AddRefMethod(rm);

                //rm = new RefMethod();
                //rm.Title = "��";
                //rm.ClassMethodName = this.ToString() + ".DoOpenPDF";
                //rm.Icon = "/WF/Img/FileType/pdf.gif";
                //map.AddRefMethod(rm);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        public string DoOpen()
        {
            return "/WF/WFRpt.aspx?FK_Flow"+this.FK_Flow+"&WorkID="+this.WorkID+"&OID="+this.WorkID;
        }
    }
	/// <summary>
	/// ʱЧ����s
	/// </summary>
	public class CHExts :Entities
	{
		#region ���췽������
		/// <summary>
        /// ʱЧ����s
		/// </summary>
		public CHExts(){}
		#endregion 

		#region ����
		/// <summary>
        /// ʱЧ����
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CHExt();
			}
		}
		#endregion
	}
}
