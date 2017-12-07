using System;
using System.Collections;
using BP.DA;

namespace BP.En
{

	/// <summary>
	/// �����б�
	/// </summary>
	public class EntityMIDAttr
	{
		public static string MID="MID";
	}
	/// <summary>
	/// MIDʵ��,ֻ��һ��ʵ�����ʵ��ֻ��һ���������ԡ�
	/// </summary>
	abstract public class EntityMID : Entity
	{

		public override int Save()
		{
			
			if ( this.Update()==0 )
			{
				this.MID=BP.DA.DBAccess.GenerOID();
				this.Insert();
				this.Retrieve();
			}
			return this.MID;
		}


//		public override int Save()
//		{
//
//			if (this.IsExits)
//				return this.Update();
//			else
//			{
//				this.Insert();
//				return 1;
//			}
//
//			//			if (this.Update()==0)
//			//				this.Insert();
//			//
//			//			return base.Save ();
//		}


		/// <summary>
		/// �Ƿ����
		/// </summary>
		/// <returns></returns>
		public bool IsExitCheckByPKs()
		{
			return false;
		}


		#region ����
		/// <summary>
		/// MID, ����ǿյľͷ��� 0 . 
		/// </summary>
		public int MID
		{
			get
			{
				try
				{
					return this.GetValIntByKey(EntityMIDAttr.MID);
				}
				catch
				{
					return 0; 
				}
			}

			set
			{
				this.SetValByKey(EntityMIDAttr.MID,value);
			}
		}
		#endregion

		#region ���캯��
		/// <summary>
		/// ����һ����ʵ��
		/// </summary>
		protected EntityMID()
		{
		}
		/// <summary>
		/// ����MID����ʵ��
		/// </summary>
		/// <param name="MID">MID</param>
		protected EntityMID(int mid)  
		{
			this.SetValByKey(EntityMIDAttr.MID,MID);
			QueryObject qo = new QueryObject(this);
			qo.AddWhere(EntityMIDAttr.MID,mid);
			if (qo.DoQuery()==0)
				throw new Exception("û�в�ѯ��MID="+mid+"��ʵ����");
			//this.Retrieve();
		}
		#endregion
	 
		#region override ����
	
		public override int Retrieve()
		{
			if (this.MID==0)
			{
				return base.Retrieve();
			}
			else
			{
				QueryObject qo = new QueryObject(this);
				qo.AddWhere("MID", this.MID);
				if (qo.DoQuery()==0)
					throw new Exception("û�д˼�¼:MID="+this.MID);
				else
					return 1;
			}
		}

		/// <summary>
		/// ɾ��֮ǰ�Ĳ�����
		/// </summary>
		/// <returns></returns>
		protected override bool beforeDelete() 
		{
			if (base.beforeDelete()==false)
				return false;

			try 
			{				
				if (this.MID < 0 )
					throw new Exception("@ʵ��["+this.EnDesc+"]û�б�ʵ����������Delete().");
				return true;
				 
			} 
			catch (Exception ex) 
			{
				throw new Exception("@["+this.EnDesc+"].beforeDelete err:"+ex.Message);
			}
		}
		public override int DirectInsert()
		{
			this.MID=DBAccess.GenerOID();
			//EnDA.Insert(this);
			return this.RunSQL( SqlBuilder.Insert(this) );

		}
		#endregion

		#region public ����
		 
		#endregion

	}
	/// <summary>
	/// MIDʵ�弯��
	/// </summary>
	abstract public class EntitiesMID : Entities
	{
		public EntitiesMID()
		{
		}		 
	}
}
