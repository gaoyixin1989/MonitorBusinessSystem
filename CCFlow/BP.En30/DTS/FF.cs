using System;
using System.Collections;
using BP.DA;
using BP.Web.Controls ; 

namespace BP.DTS
{ 
	/// <summary>
	/// ����
	/// </summary>
	public class FF
	{
		/// <summary>
		/// ���ֶ�
		/// </summary>
		public string FromField=null;
		/// <summary>
		/// ���ֶ�
		/// </summary>
		public string ToField=null;
		/// <summary>
		/// ����Դ����
		/// </summary>
		public int DataType=1;//DataType.AppString;		
		/// <summary>
		/// �Ƿ�������
		/// </summary>
		public bool IsPK=false;
		public FF()
		{
		}
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="from">��</param>
		/// <param name="to">��</param>
		/// <param name="datatype">��������</param>
		/// <param name="isPk">�Ƿ�PK</param>
		public FF(string from,string to,int datatype, bool isPk)
		{
			this.FromField=from;
			this.ToField=to;
			this.DataType=datatype;
			this.IsPK=isPk;
		}
	}
	/// <summary>
	/// ���Լ���
	/// </summary>
	[Serializable]
	public class FFs: CollectionBase
	{
        public int PKCount
        {
            get
            {
                int i = 0;
                foreach (FF ff in this)
                {
                    if (ff.IsPK)
                        i++;
                }
                if (i == 0)
                    throw new Exception("û������PK. ����map ����.");
                return i;
            }
        }
		/// <summary>
		/// ���Լ���
		/// </summary>
		public FFs(){}
		/// <summary>
		/// ����һ������
		/// </summary>
		/// <param name="attr"></param>
		public void Add(FF ff)
		{
			this.InnerList.Add(ff);
		}
		/// <summary>
		/// ����һ������Ӱ��
		/// </summary>
		/// <param name="fromF"></param>
		/// <param name="toF"></param>
		/// <param name="dataType"></param>
		/// <param name="isPk"></param>
		public void Add(string fromF,string toF, int dataType, bool isPk)
		{
			this.Add( new FF( fromF , toF , dataType ,isPk ) );
		}
		 

		/// <summary>
		/// �����������ʼ����ڵ�Ԫ��Attr��
		/// </summary>
		public FF this[int index]
		{			
			get
			{	
				return (FF)this.InnerList[index];
			}
		}
	}	
}
