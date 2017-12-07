using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port; 

namespace BP.WF
{
	/// <summary>
	/// �˻ع켣
	/// </summary>
	public class ReturnWorkAttr 
	{
		#region ��������
		/// <summary>
		/// ����ID
		/// </summary>
		public const  string WorkID="WorkID";
		/// <summary>
		/// ������Ա
		/// </summary>
		public const  string Worker="Worker";
		/// <summary>
		/// �˻�ԭ��
		/// </summary>
		public const  string Note="Note";
        /// <summary>
        /// �˻�����
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// �˻���
        /// </summary>
        public const string Returner = "Returner";
        /// <summary>
        /// �˻�������
        /// </summary>
        public const string ReturnerName = "ReturnerName";
        /// <summary>
        /// �˻ص��ڵ�
        /// </summary>
        public const string ReturnToNode = "ReturnToNode";
        /// <summary>
        /// �˻ؽڵ�
        /// </summary>
        public const string ReturnNode = "ReturnNode";
        /// <summary>
        /// �˻ؽڵ�����
        /// </summary>
        public const string ReturnNodeName = "ReturnNodeName";
        /// <summary>
        /// �˻ظ�
        /// </summary>
        public const string ReturnToEmp = "ReturnToEmp";
        /// <summary>
        /// �˻غ��Ƿ���Ҫԭ·���أ�
        /// </summary>
        public const string IsBackTracking = "IsBackTracking";
		#endregion
	}
	/// <summary>
	/// �˻ع켣
	/// </summary>
    public class ReturnWork : EntityMyPK
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(ReturnWorkAttr.WorkID);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.WorkID, value);
            }
        }
        /// <summary>
        /// �˻ص��ڵ�
        /// </summary>
        public int ReturnToNode
        {
            get
            {
                return this.GetValIntByKey(ReturnWorkAttr.ReturnToNode);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.ReturnToNode, value);
            }
        }
        /// <summary>
        /// �˻ؽڵ�
        /// </summary>
        public int ReturnNode
        {
            get
            {
                return this.GetValIntByKey(ReturnWorkAttr.ReturnNode);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.ReturnNode, value);
            }
        }
        public string ReturnNodeName
        {
            get
            {
                return this.GetValStrByKey(ReturnWorkAttr.ReturnNodeName);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.ReturnNodeName, value);
            }
        }
        /// <summary>
        /// �˻���
        /// </summary>
        public string Returner
        {
            get
            {
                return this.GetValStringByKey(ReturnWorkAttr.Returner);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.Returner, value);
            }
        }
        public string ReturnerName
        {
            get
            {
                return this.GetValStringByKey(ReturnWorkAttr.ReturnerName);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.ReturnerName, value);
            }
        }
        /// <summary>
        /// �˻ظ�
        /// </summary>
        public string ReturnToEmp
        {
            get
            {
                return this.GetValStringByKey(ReturnWorkAttr.ReturnToEmp);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.ReturnToEmp, value);
            }
        }
        public string Note
        {
            get
            {
                return this.GetValStringByKey(ReturnWorkAttr.Note);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.Note, value);
            }
        }
        public string NoteHtml
        {
            get
            {
                return this.GetValHtmlStringByKey(ReturnWorkAttr.Note);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(ReturnWorkAttr.RDT);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.RDT, value);
            }
        }
        /// <summary>
        /// �Ƿ�Ҫԭ·���أ�
        /// </summary>
        public bool IsBackTracking
        {
            get
            {
                return this.GetValBooleanByKey(ReturnWorkAttr.IsBackTracking);
            }
            set
            {
                SetValByKey(ReturnWorkAttr.IsBackTracking, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �˻ع켣
        /// </summary>
        public ReturnWork() { }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("WF_ReturnWork");
                map.EnDesc =  "�˻ع켣";
                map.EnType = EnType.App;

                map.AddMyPK();

                map.AddTBInt(ReturnWorkAttr.WorkID, 0, "WorkID", true, true);

                map.AddTBInt(ReturnWorkAttr.ReturnNode, 0, "�˻ؽڵ�", true, true);
                map.AddTBString(ReturnWorkAttr.ReturnNodeName, null, "�˻ؽڵ�����", true, true, 0, 200, 10);

                map.AddTBString(ReturnWorkAttr.Returner, null, "�˻���", true, true, 0, 20, 10);
                map.AddTBString(ReturnWorkAttr.ReturnerName, null, "�˻�������", true, true, 0, 200, 10);

                map.AddTBInt(ReturnWorkAttr.ReturnToNode, 0, "ReturnToNode", true, true);
                map.AddTBString(ReturnWorkAttr.ReturnToEmp, null, "�˻ظ�", true, true, 0, 4000, 10);

                map.AddTBString(ReturnWorkAttr.Note, "", "�˻�ԭ��", true, true, 0, 4000, 10);
                map.AddTBDateTime(ReturnWorkAttr.RDT, null, "�˻�����", true, true);

                map.AddTBInt(ReturnWorkAttr.IsBackTracking, 0, "�Ƿ�Ҫԭ·����?", true, true);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion


        protected override bool beforeInsert()
        {
            this.Returner = BP.Web.WebUser.No;
            this.ReturnerName = BP.Web.WebUser.Name;

            this.RDT =DataType.CurrentDataTime;
            return base.beforeInsert();
        }
    }
	/// <summary>
	/// �˻ع켣s 
	/// </summary>
	public class ReturnWorks : Entities
	{	 
		#region ����
		/// <summary>
		/// �˻ع켣s
		/// </summary>
		public ReturnWorks()
		{
		}
		/// <summary>
		/// �õ����� Entity
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ReturnWork();
			}
		}
		#endregion
	}
	
}
