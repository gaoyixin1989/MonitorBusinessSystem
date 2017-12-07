using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// ����
	/// </summary>
	public class NY :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_NY";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
                return  "����";// "����";
			}
		}
		#endregion 

		#region ���췽��
		 
		public NY(){}
		 
		public NY(string _No ): base(_No){}
		
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class NYs :SimpleNoNameFixs
	{
		/// <summary>
		/// ���¼���
		/// </summary>
		public NYs()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new NY();
			}
		}
	}
}
