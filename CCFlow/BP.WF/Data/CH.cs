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
    /// ���״̬
    /// </summary>
    public enum CHSta
    {
        /// <summary>
        /// ��ʱ���
        /// </summary>
        JiShi,
        /// <summary>
        /// �������
        /// </summary>
        AnQi,
        /// <summary>
        /// Ԥ�����
        /// </summary>
        YuQi,
        /// <summary>
        /// �������
        /// </summary>
        ChaoQi
    }
	/// <summary>
	/// ʱЧ��������
	/// </summary>
    public class CHAttr
    {
        #region ����
        public const string MyPK = "MyPK";
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ���̱��
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ���̱��
        /// </summary>
        public const string FK_FlowT = "FK_FlowT";

        /// <summary>
        /// �ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// �ڵ���
        /// </summary>
        public const string FK_NodeT = "FK_NodeT";

        /// <summary>
        /// ���ű��
        /// </summary>
        public const string FK_Dept = "FK_Dept";
        /// <summary>
        /// ���ű��
        /// </summary>
        public const string FK_DeptT = "FK_DeptT";
        /// <summary>
        /// �ʹ��
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        public const string FK_EmpT = "FK_EmpT";
        /// <summary>
        /// ����
        /// </summary>
        public const string TSpan = "TSpan";
        /// <summary>
        /// ʵ������
        /// </summary>
        public const string UseMinutes = "UseMinutes";
        /// <summary>
        /// ʹ��ʱ��
        /// </summary>
        public const string UseTime = "UseTime";
        /// <summary>
        /// ����
        /// </summary>
        public const string OverMinutes = "OverMinutes";
        /// <summary>
        /// Ԥ��
        /// </summary>
        public const string OverTime = "OverTime";
        /// <summary>
        /// ״̬
        /// </summary>
        public const string CHSta = "CHSta";
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_NY = "FK_NY";
        /// <summary>
        /// ��
        /// </summary>
        public const string Week = "Week";
        /// <summary>
        /// FID
        /// </summary>
        public const string FID = "FID";
        /// <summary>
        /// ����
        /// </summary>
        public const string Title = "Title";
        /// <summary>
        /// ʱ���
        /// </summary>
        public const string DTFrom = "DTFrom";
        /// <summary>
        /// ʱ�䵽
        /// </summary>
        public const string DTTo = "DTTo";
        /// <summary>
        /// Ӧ�������
        /// </summary>
        public const string SDT = "SDT";
        #endregion
    }
	/// <summary>
	/// ʱЧ����
	/// </summary> 
    public class CH : EntityMyPK
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
        public string TSpan
        {
            get
            {
                return this.GetValStringByKey(CHAttr.TSpan);
            }
            set
            {
                this.SetValByKey(CHAttr.TSpan, value);
            }
        }
        /// <summary>
        /// ʵ�������ʱ.
        /// </summary>
        public int UseMinutes
        {
            get
            {
                return this.GetValIntByKey(CHAttr.UseMinutes);
            }
            set
            {
                this.SetValByKey(CHAttr.UseMinutes, value);
            }
        }
        public string UseTime
        {
            get
            {
                return this.GetValStringByKey(CHAttr.UseTime);
            }
            set
            {
                this.SetValByKey(CHAttr.UseTime, value);
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public int OverMinutes
        {
            get
            {
                return this.GetValIntByKey(CHAttr.OverMinutes);
            }
            set
            {
                this.SetValByKey(CHAttr.OverMinutes, value);
            }
        }
        /// <summary>
        /// Ԥ��
        /// </summary>
        public string OverTime
        {
            get
            {
                return this.GetValStringByKey(CHAttr.OverTime);
            }
            set
            {
                this.SetValByKey(CHAttr.OverTime, value);
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
        /// ��
        /// </summary>
        public int Week
        {
            get
            {
                return this.GetValIntByKey(CHAttr.Week);
            }
            set
            {
                this.SetValByKey(CHAttr.Week, value);
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
        public CH() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pk"></param>
        public CH(string pk)
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

                map.AddMyPK();

                map.AddTBInt(CHAttr.WorkID, 0, "����ID", false, true);
                map.AddTBInt(CHAttr.FID, 0, "FID", false, true);
                
                map.AddTBString(CHAttr.Title, null, "����", false, false, 0, 900, 5);

                map.AddTBString(CHAttr.FK_Flow, null, "����", false, false, 3, 3, 3);
                map.AddTBString(CHAttr.FK_FlowT, null, "��������", true, true, 0, 50, 5);

                map.AddTBInt(CHAttr.FK_Node, 0, "�ڵ�", false, false);
                map.AddTBString(CHAttr.FK_NodeT, null, "�ڵ�����", true, true, 0, 50, 5);

                map.AddTBString(CHAttr.DTFrom, null, "ʱ���", true, true, 0, 50, 5);
                map.AddTBString(CHAttr.DTTo, null, "��", true, true, 0, 50, 5);
                map.AddTBString(CHAttr.SDT, null, "Ӧ�������", true, true, 0, 50, 5);

                map.AddTBString(CHAttr.TSpan, null, "�涨����", true, true, 0, 50, 5);

                map.AddTBInt(CHAttr.UseMinutes, 0, "ʵ��ʹ�÷���", false, true);
                map.AddTBString(CHAttr.UseTime, null, "ʵ��ʹ��ʱ��", true, true, 0, 50, 5);

                map.AddTBInt(CHAttr.OverMinutes, 0, "���ڷ���", false, true);
                map.AddTBString(CHAttr.OverTime, null, "����", true, true, 0, 50, 5);

                map.AddTBString(CHAttr.FK_Dept, null, "��������", true, true, 0, 50, 5);
                map.AddTBString(CHAttr.FK_DeptT, null, "��������", true, true, 0, 50, 5);

                map.AddTBString(CHAttr.FK_Emp, null, "������", true, true, 0, 30, 3);
                map.AddTBString(CHAttr.FK_EmpT, null, "����������", true, true, 0, 50, 5);

                map.AddTBString(CHAttr.FK_NY, null, "�����·�", true, true, 0, 10, 10);
                map.AddTBInt(CHAttr.Week, 0, "�ڼ���", false, true);

                map.AddTBInt(CHAttr.FID, 0, "FID", false, true);
                map.AddTBInt(CHAttr.CHSta, 0, "״̬", true, true);
                map.AddTBIntMyNum();

                //map.AddSearchAttr(CHAttr.FK_Dept);
                //map.AddSearchAttr(CHAttr.FK_NY);
                //map.AddSearchAttr(CHAttr.FK_Emp);

                //RefMethod rm = new RefMethod();
                //rm.Title = "��";
                //rm.ClassMethodName = this.ToString() + ".DoOpen";
                //rm.Icon = "/WF/Img/FileType/doc.gif";
                //map.AddRefMethod(rm);

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

        protected override bool beforeUpdateInsertAction()
        {
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
	/// ʱЧ����s
	/// </summary>
	public class CHs :Entities
	{
		#region ���췽������
		/// <summary>
        /// ʱЧ����s
		/// </summary>
		public CHs(){}
		#endregion 

		#region ����
		/// <summary>
        /// ʱЧ����
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new CH();
			}
		}
		#endregion
	}
}
