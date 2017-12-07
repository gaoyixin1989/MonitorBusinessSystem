using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP.WF.Template;

namespace BP.WF
{
	/// <summary>
	/// �ڵ㷽������	  
	/// </summary>
	public class DirectionAttr
	{
		/// <summary>
		/// �ڵ�
		/// </summary>
		public const string Node="Node";
		/// <summary>
		/// ת��Ľڵ�
		/// </summary>
		public const string ToNode="ToNode";
        /// <summary>
        /// ���̱��
        /// </summary>
        public const string FK_Flow = "FK_Flow";
        /// <summary>
        /// ��������
        /// </summary>
        public const string DirType = "DirType";
        /// <summary>
        /// �Ƿ����ԭ·����
        /// </summary>
        public const string IsCanBack = "IsCanBack";
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public const string Dots = "Dots";
	}
	/// <summary>
	/// �ڵ㷽��
	/// �ڵ�ķ��������������.
	/// 1, Node.
	/// 2, toNode.
	/// ��¼�˴�һ���ڵ㵽�����Ķ���ڵ�.
	/// Ҳ��¼�˵�����ڵ�������Ľڵ�.
	/// </summary>
	public class Direction :EntityMyPK
	{
		#region ��������
		/// <summary>
		///�ڵ�
		/// </summary>
        public int Node
        {
            get
            {
                return this.GetValIntByKey(DirectionAttr.Node);
            }
            set
            {
                this.SetValByKey(DirectionAttr.Node, value);
            }
        }
        public int DirType
        {
            get
            {
                return this.GetValIntByKey(DirectionAttr.DirType);
            }
            set
            {
                this.SetValByKey(DirectionAttr.DirType, value);
            }
        }
        public string FK_Flow
        {
            get
            {
                return this.GetValStringByKey(DirectionAttr.FK_Flow);
            }
            set
            {
                this.SetValByKey(DirectionAttr.FK_Flow, value);
            }
        }
		/// <summary>
		/// ת��Ľڵ�
		/// </summary>
		public int  ToNode
		{
			get
			{
				return this.GetValIntByKey(DirectionAttr.ToNode);
			}
			set
			{
				this.SetValByKey(DirectionAttr.ToNode,value);
			}
		}
        public bool IsCanBack
        {
            get
            {
                return this.GetValBooleanByKey(DirectionAttr.IsCanBack);
            }
            set
            {
                this.SetValByKey(DirectionAttr.IsCanBack, value);
            }
        }
        public string Dots
        {
            get
            {
                return this.GetValStringByKey(DirectionAttr.Dots);
            }
            set
            {
                this.SetValByKey(DirectionAttr.Dots, value);
            }
        }
		#endregion 

		#region ���췽��
		/// <summary>
		/// �ڵ㷽��
		/// </summary>
		public Direction(){}
		/// <summary>
		/// ��д���෽��
		/// </summary>
		public override Map EnMap
		{
			get
			{
				if (this._enMap!=null) 
					return this._enMap;
				
				Map map = new Map("WF_Direction");			 
				map.EnDesc="�ڵ㷽����Ϣ";

                /*
                 * MyPK ��һ���������� ���� Node+'_'+ToNode+'_'+DirType ��ϵ�. ����: 101_102_1
                 */
                map.AddMyPK();
                map.AddTBString(DirectionAttr.FK_Flow, null, "����", true, true, 0, 3, 0, false);
                map.AddTBInt(DirectionAttr.Node, 0, "�ӽڵ�", false, true);
				map.AddTBInt( DirectionAttr.ToNode,0,"���ڵ�",false,true);
                map.AddTBInt(DirectionAttr.DirType, 0, "����0ǰ��1����", false, true);
                map.AddTBInt(DirectionAttr.IsCanBack, 0, "�Ƿ����ԭ·����(�Ժ�������Ч)", false, true);
                /*
                 * Dots �洢��ʽΪ: @x1,y1@x2,y2
                 */
                map.AddTBString(NodeReturnAttr.Dots, null, "�켣��Ϣ", true, true, 0, 300, 0, false);

				this._enMap=map;
				return this._enMap;
			}
		}
		#endregion

        /// <summary>
        /// ����pk 
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            this.MyPK = this.FK_Flow+"_" +this.Node + "_" + this.ToNode + "_" + this.DirType;
            return base.beforeInsert();
        }
        protected override bool beforeDelete()
        {
            this.MyPK = this.FK_Flow + "_" + this.Node + "_" + this.ToNode + "_" + this.DirType;
            return base.beforeDelete();
        }
	}
	 /// <summary>
	 /// �ڵ㷽��
	 /// </summary>
	public class Directions :En.Entities
	{
		/// <summary>
		/// �ڵ㷽��
		/// </summary>
		public Directions(){}
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="flowNo"></param>
        public Directions(string flowNo)
        {
            this.Retrieve(DirectionAttr.FK_Flow, flowNo);
        }
        /// <summary>
        /// �ڵ㷽��
        /// </summary>
        /// <param name="NodeID">�ڵ�ID</param>
        /// <param name="dirType">����</param>
        public Directions(int NodeID, int dirType)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DirectionAttr.Node,NodeID);
            qo.addAnd();
            qo.AddWhere(DirectionAttr.DirType, dirType);
		    qo.DoQuery();			
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new Direction();
			}
		}
		/// <summary>
		/// �˽ڵ��ת���򼯺�
		/// </summary>
		/// <param name="nodeID">�˽ڵ��ID</param>
		/// <param name="isLifecyle">�ǲ����ж��ڽڵ����������</param>		 
		/// <returns>ת���򼯺�(ToNodes)</returns> 
		public Nodes GetHisToNodes(int nodeID, bool isLifecyle)
		{
			Nodes nds = new Nodes();
			QueryObject qo = new QueryObject(nds);
			qo.AddWhereInSQL(NodeAttr.NodeID,"SELECT ToNode FROM WF_Direction WHERE Node="+nodeID );
			qo.DoQuery();
			return nds;
		}
		/// <summary>
		/// ת��˽ڵ�ļ��ϵ�Nodes
		/// </summary>
		/// <param name="nodeID">�˽ڵ��ID</param>
		/// <returns>ת��˽ڵ�ļ��ϵ�Nodes (FromNodes)</returns> 
		public Nodes GetHisFromNodes(int nodeID)
		{
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(DirectionAttr.ToNode,nodeID);
			qo.DoQuery();
			Nodes ens = new Nodes();
			foreach(Direction en in this)
			{
				ens.AddEntity( new Node(en.Node) ) ;
			}
			return ens;
		}
		 
	}
}
