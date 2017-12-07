using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// ���
	/// </summary>
	public class ND :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_ND";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
                return "���";// "���";
			}
		}
		#endregion 

		#region ���췽��
		public ND()
		{
		}
		public ND(string _No ):base(_No)
		{
		}
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class NDs :SimpleNoNameFixs
	{
        protected override void OnClear()
        {
            if (1 == 1)
            {

            }
            base.OnClear();
        }
		/// <summary>
		/// ��ȼ���
		/// </summary>
		public NDs()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new ND();
			}
		}
        public override int RetrieveAll()
        {
            return base.RetrieveAll();
        }
	}
}
