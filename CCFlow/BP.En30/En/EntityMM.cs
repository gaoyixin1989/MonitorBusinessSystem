using System;

namespace BP.En
{
	/// <summary>
	/// ��Զ�ļ��ϡ�
	/// </summary>
	abstract public class EntityMM:Entity
	{
		/// <summary>
		/// ��Զ�ļ���
		/// </summary>
		public EntityMM()
		{
		}
	}
	/// <summary>
	/// ��Զ�ļ���
	/// </summary>
	abstract public class EntitiesMM : Entities
	{
		/// <summary>
		/// ��Զ�ļ���
		/// </summary>
		protected EntitiesMM() 
		{
		}
		/// <summary>
		/// �ṩͨ��һ��ʵ��� val ��ѯ�����ʵ�弯�ϡ�
		/// </summary>
		/// <param name="attr">����</param>
		/// <param name="val">ֲ</param>
		/// <param name="refEns">�����ļ���</param>
		/// <returns>�����ļ���</returns>
		protected Entities throwOneKeyValGetRefEntities(string attr , int val, Entities refEns )
		{																									
			QueryObject qo = new QueryObject(refEns);
			qo.AddWhere(attr, val);
			return refEns;
		}
		/// <summary>
		/// �ṩͨ��һ��ʵ��� val ��ѯ�����ʵ�弯�ϡ�
		/// </summary>
		/// <param name="attr">����</param>
		/// <param name="val">ֲ</param>
		/// <param name="refEns">�����ļ���</param>
		/// <returns>�����ļ���</returns>
		protected Entities throwOneKeyValGetRefEntities(string attr, string val, Entities  refEns) 	 
		{
			QueryObject qo = new QueryObject(refEns);
			qo.AddWhere(attr, val);
			return refEns;
		}
	}
}
