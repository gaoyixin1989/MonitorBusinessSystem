using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.WF
{
	/// <summary>
	/// �Զ�������·�� ����
	/// </summary>
    public class TransferCustomAttr : EntityMyPKAttr
    {
        #region ��������
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ������
        /// </summary>
        public const string Worker = "Worker";
        /// <summary>
        /// ˳��
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string StartDT = "StartDT";
        /// <summary>
        /// ��������
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// Ҫ���õ������̱��
        /// </summary>
        public const string SubFlowNo = "SubFlowNo";
        #endregion
    }
	/// <summary>
	/// �Զ�������·��
	/// </summary>
    public class TransferCustom : EntityMyPK
    {
        #region ����
        /// <summary>
        /// �ڵ�ID
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(TransferCustomAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(TransferCustomAttr.FK_Node, value);
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(TransferCustomAttr.WorkID);
            }
            set
            {
                this.SetValByKey(TransferCustomAttr.WorkID, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Worker
        {
            get
            {
                return this.GetValStringByKey(TransferCustomAttr.Worker);
            }
            set
            {
                this.SetValByKey(TransferCustomAttr.Worker, value);
            }
        }
        /// <summary>
        /// Ҫ���õ������̱��
        /// </summary>
        public string SubFlowNo
        {
            get
            {
                return this.GetValStringByKey(TransferCustomAttr.SubFlowNo);
            }
            set
            {
                this.SetValByKey(TransferCustomAttr.SubFlowNo, value);
            }
        }
        /// <summary>
        /// ˳��
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(TransferCustomAttr.Idx);
            }
            set
            {
                this.SetValByKey(TransferCustomAttr.Idx, value);
            }
        }
        /// <summary>
        /// ����ʱ�䣨����Ϊ�գ�
        /// </summary>
        public string StartDT
        {
            get
            {
                return this.GetValStringByKey(TransferCustomAttr.StartDT);
            }
            set
            {
                this.SetValByKey(TransferCustomAttr.StartDT, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// TransferCustom
        /// </summary>
        public TransferCustom()
        {
        }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_TransferCustom");
                map.EnDesc = "�Զ�������·��"; //������liuxianchen.
                map.EnType = EnType.Admin;

                map.AddMyPK(); //Ψһ������.

                //����.
                map.AddTBInt(TransferCustomAttr.WorkID, 0, "WorkID", true, false);
                map.AddTBInt(TransferCustomAttr.FK_Node, 0, "����ID", true, false);
                map.AddTBString(TransferCustomAttr.Worker, null, "������", true, false, 0, 200, 10);
                map.AddTBString(TransferCustomAttr.SubFlowNo, null, "Ҫ�����������̱��", true, false, 0, 3, 10);
                map.AddTBDateTime(TransferCustomAttr.RDT, null, "����ʱ��", true, false);
                map.AddTBInt(TransferCustomAttr.Idx, 0, "˳���", true, false);
              
                //map.AddTBString(TransferCustomAttr.StartDT, null, "����ʱ��", true, false, 0, 20, 10);
                
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        /// <summary>
        /// ��ȡ��һ��Ҫ����Ķ���·��.
        /// Ҫ�������¼������:
        /// 1, ��ǰ�ڵ㲻���ڶ������棬�ͷ��ص�һ��.
        /// 2, �����ǰ����Ϊ��,����Ϊ��Ҫ������, ����null.
        /// 3, �����ǰ�ڵ������һ��,�ͷ���null,��ʾҪ��������.
        /// </summary>
        /// <param name="workid">��ǰ����ID</param>
        /// <param name="currNodeID">��ǰ�ڵ�ID</param>
        /// <returns>��ȡ��һ��Ҫ����Ķ���·��,���û�оͷ��ؿ�.</returns>
        public static TransferCustom GetNextTransferCustom(Int64 workid, int currNodeID)
        {
            TransferCustoms ens = new TransferCustoms();
            ens.Retrieve(TransferCustomAttr.WorkID, workid, TransferCustomAttr.Idx);
            if (ens.Count == 0)
                return null;

                /*��ȡ���һ��*/
                TransferCustom tEnd = ens[ens.Count-1] as TransferCustom;
                if (tEnd.FK_Node == currNodeID)
                    return null; //��ʾҪ��������Ϊ�������һ������.

            // ��ʼ��, �ҵ���ǰ�ڵ����һ��.
            bool isRec = false;
            foreach (TransferCustom en in ens)
            {
                if (en.FK_Node == currNodeID)
                {
                    isRec = true;
                    continue;
                }
                if (isRec)
                    return en;
            }

            //���û���ҵ����ͷ������һ��.
            return (TransferCustom)ens[0];
        }
    }
	/// <summary>
	/// �Զ�������·��
	/// </summary>
	public class TransferCustoms: EntitiesMyPK
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new TransferCustom();
			}
		}
		/// <summary>
        /// �Զ�������·��
		/// </summary>
		public TransferCustoms(){} 		 
		#endregion
	}
}
