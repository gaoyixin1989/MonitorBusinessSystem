using System;
using System.Collections;
using BP.En;

namespace BP.En
{
	/// <summary>
	/// SearchKey ��ժҪ˵����
	/// ��������һ����¼�Ĵ�ţ����⡣
	/// </summary>
	public class AttrOfOneVSM 
	{
		#region ��������
		/// <summary>
		/// ��Զ��ʵ��.
		/// </summary>
		private Entities _ensOfMM=null;
		/// <summary>
		/// ��Զ��ʵ�弯��
		/// </summary>
		public Entities EnsOfMM
		{
			get
			{
				return _ensOfMM;
			}
			set
			{
				_ensOfMM=value;
			}
		}
		/// <summary>
		/// ��Զ��ʵ��
		/// </summary>
		private Entities _ensOfM=null;
		/// <summary>
		/// ��Զ��ʵ�弯��
		/// </summary>
		public Entities EnsOfM
		{
			get
			{
				return _ensOfM;
			}
			set
			{
				_ensOfM=value;
			}
		}
		/// <summary>
		/// M��ʵ�������ڶ�Զ��ʵ����
		/// </summary>
		private string _Desc=null;
		/// <summary>
		/// ��ʵ�������ڶ�Զ��ʵ����
		/// </summary>
		public string Desc
		{
			get
			{
			    return _Desc;//edited by liuxc,2014-10-18 "<font color=blue ><u>" + _Desc + "</u></font>";
			}
			set
			{
				_Desc=value;
			}
		}
		/// <summary>
		/// һ��ʵ�������ڶ�Զ��ʵ����.
		/// </summary>
		private string _AttrOfOneInMM=null;
		/// <summary>
		/// һ��ʵ�������ڶ�Զ��ʵ����
		/// </summary>
		public string AttrOfOneInMM
		{
			get
			{
				return _AttrOfOneInMM;
			}
			set
			{
				_AttrOfOneInMM=value;
			}
		}

		/// <summary>
		/// M��ʵ�������ڶ�Զ��ʵ����
		/// </summary>
		private string _AttrOfMInMM=null;
		/// <summary>
		/// ��ʵ�������ڶ�Զ��ʵ����
		/// </summary>
		public string AttrOfMInMM
		{
			get
			{
				return _AttrOfMInMM;
			}
			set
			{
				_AttrOfMInMM=value;
			}
		}
		/// <summary>
		/// ��ǩ
		/// </summary>
		private string _AttrOfMText=null;
		/// <summary>
		/// ��ǩ
		/// </summary>
		public string AttrOfMText
		{
			get
			{
				return _AttrOfMText;
			}
			set
			{
				_AttrOfMText=value;
			}
		}
		/// <summary>
		/// Value
		/// </summary>
		private string _AttrOfMValue=null;
		/// <summary>
		/// Value
		/// </summary>
		public string AttrOfMValue
		{
			get
			{
				return _AttrOfMValue;
			}
			set
			{
				_AttrOfMValue=value;
			}
		}
		#endregion

		#region ���췽��
		/// <summary>
		/// AttrOfOneVSM
		/// </summary>
		public AttrOfOneVSM()
		{}
		/// <summary>
		/// AttrOfOneVSM
		/// </summary>
		/// <param name="_ensOfMM"></param>
		/// <param name="_ensOfM"></param>
		/// <param name="AttrOfOneInMM"></param>
		/// <param name="AttrOfMInMM"></param>
		/// <param name="AttrOfMText"></param>
		/// <param name="AttrOfMValue"></param>
		public AttrOfOneVSM(Entities _ensOfMM, Entities _ensOfM, string AttrOfOneInMM, string AttrOfMInMM , string AttrOfMText, string AttrOfMValue, string desc)
		{
			this.EnsOfM=_ensOfM;
			this.EnsOfMM=_ensOfMM;
			this.AttrOfOneInMM=AttrOfOneInMM;
			this.AttrOfMInMM=AttrOfMInMM;
			this.AttrOfMText=AttrOfMText;
			this.AttrOfMValue=AttrOfMValue;
			this.Desc=desc;
		}
		#endregion

	}
	/// <summary>
	/// AttrsOfOneVSM ����
	/// </summary>
	public class AttrsOfOneVSM : System.Collections.CollectionBase
	{
		public AttrsOfOneVSM()
		{
		}
		public AttrOfOneVSM this[int index]
		{
			get
			{
				return (AttrOfOneVSM)this.InnerList[index];
			}
		}
		/// <summary>
		/// ����һ��SearchKey .
		/// </summary>
		/// <param name="r">SearchKey</param>
		public void Add(AttrOfOneVSM attr)
		{
			if (this.IsExits(attr))
				return ;
			this.InnerList.Add(attr);
		}

		/// <summary>
		/// �ǲ��Ǵ��ڼ�������
		/// </summary>
		/// <param name="en">Ҫ����EnDtl</param>
		/// <returns>true/false</returns>
		public bool IsExits(AttrOfOneVSM en)
		{
			foreach (AttrOfOneVSM attr in this )
			{
				if (attr.EnsOfMM == en.EnsOfMM  )
				{
					return true;
				}
			}
			return false;
		}

		/// <summary>
		/// ����һ������
		/// </summary>
		/// <param name="_ensOfMM">��Զ��ʵ��</param>
		/// <param name="_ensOfM">��ʵ��</param>
		/// <param name="AttrOfOneInMM">��ʵ��,��MM�е�����</param>
		/// <param name="AttrOfMInMM">��ʵ��������MM�е�����</param>
		/// <param name="AttrOfMText"></param>
		/// <param name="AttrOfMValue"></param>
		/// <param name="desc">����</param>
		public void Add(Entities _ensOfMM, Entities _ensOfM, string AttrOfOneInMM, string AttrOfMInMM , string AttrOfMText, string AttrOfMValue, string desc)
		{
			AttrOfOneVSM en = new AttrOfOneVSM(_ensOfMM,_ensOfM,AttrOfOneInMM,AttrOfMInMM,AttrOfMText,AttrOfMValue,desc) ;
			 
			this.Add(en);				
		}
		 
	}
}
