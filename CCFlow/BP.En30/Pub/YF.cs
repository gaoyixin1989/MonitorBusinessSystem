using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Pub
{
	/// <summary>
	/// �·�
	/// </summary>
	public class YF :SimpleNoNameFix
	{
		#region ʵ�ֻ����ķ�����
		 
		/// <summary>
		/// �����
		/// </summary>
		public override string  PhysicsTable
		{
			get
			{
				return "Pub_YF";
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		public override string  Desc
		{
			get
			{
                return  "�·�";  // "�·�";
			}
		}
		#endregion 

		#region ���췽��
		public YF()
        {
        }
        /// <summary>
        /// _No
        /// </summary>
        /// <param name="_No"></param>
		public YF(string _No ): base(_No)
        {
        }
		#endregion 
	}
	/// <summary>
	/// NDs
	/// </summary>
	public class YFs :SimpleNoNameFixs
	{
		/// <summary>
		/// �·ݼ���
		/// </summary>
		public YFs()
		{
		}
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new YF();
			}
		}
        public override int RetrieveAll()
        {
            int num= base.RetrieveAll();

            if (num != 12)
            {
                BP.DA.DBAccess.RunSQL("DELETE FROM Pub_YF ");

                for (int i = 1; i <= 12; i++)
                {
                    BP.Pub.YF yf = new YF();
                    yf.No = i.ToString().PadLeft( 2,'0');
                    yf.Name = i.ToString().PadLeft(2, '0');
                    yf.Insert();
                }

               return base.RetrieveAll();
            }
            return 12;
        }
	}
}
