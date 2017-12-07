using System; 
using System.Collections;
using BP.DA; 
using BP.Web.Controls;

namespace BP.En
{
	/// <summary>
	/// EnDtl ��ժҪ˵����
	/// </summary>
	public class EnDtl
	{

	
		/// <summary>
		/// ��ϸ
		/// </summary>
		public EnDtl()
		{			  
		}
		/// <summary>
		/// ��ϸ
		/// </summary>
		/// <param name="className">������</param>
		public EnDtl(string className)
		{
			this.Ens=ClassFactory.GetEns(className);
		}
		/// <summary>
		/// ������
		/// </summary>
		public string EnsName
		{
			get
			{
				return this.Ens.ToString();
			}
		}
		/// <summary>
		/// ��ϸ
		/// </summary>
		public Entities _Ens=null;
		/// <summary>
		/// ��ȡ������ ���ļ���
		/// </summary>
		public Entities Ens
		{
			get
			{
				return _Ens;
			}
			set			
			{
				_Ens=value;
			}
		}
		/// <summary>
		/// ��������key
		/// </summary>
		private string _refKey=null;
		/// <summary>
		/// �������� key
		/// </summary>
		public string RefKey
		{
			get
			{
				return _refKey;
			}
			set
			{
				this._refKey =value; 
			}
		}
		/// <summary>
		/// ����
		/// </summary>
		private string _Desc=null;
		/// <summary>
		/// ����
		/// </summary>
		public string Desc
		{
			get
			{
				if (this._Desc==null)
					this._Desc=this.Ens.GetNewEntity.EnDesc;
				return _Desc;
			}
			set
			{
				_Desc=value;
			}
		}
	}
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	public class EnDtls: CollectionBase
	{
		/// <summary>
		/// �ǲ��ǰ���className
		/// </summary>
		/// <param name="className"></param>
		/// <returns></returns>
		public bool IsContainKey(string className)		
		{
			foreach(EnDtl ed in this)
			{
				if (ed.EnsName==className)
					return true;
			}
			return false;
		}	 
		/// <summary>
		/// ����
		/// </summary>
		/// <param name="attr">attr</param>
		public void Add(EnDtl en)
		{
			 if (this.IsExits(en))
				 return ;
			this.InnerList.Add(en);
		}
		/// <summary>
		/// �ǲ��Ǵ��ڼ�������
		/// </summary>
		/// <param name="en">Ҫ����EnDtl</param>
		/// <returns>true/false</returns>
		public bool IsExits(EnDtl en)
		{
			foreach (EnDtl dtl in this )
			{
				if (dtl.Ens== en.Ens )
				{
					return true;
				}
			}
			return false;
		}
	 
		/// <summary>
		/// ͨ��һ��key �õ���������ֵ��
		/// </summary>
		/// <param name="key">key</param>
		/// <returns>EnDtl</returns>
		public EnDtl GetEnDtlByKey(string key)
		{		
			foreach (EnDtl dtl in this )
			{
				if (dtl.RefKey.Equals(key))
				{
					return dtl;
				}
			}
			throw new Exception("@û���ҵ� key=["+key+"]�����ԣ�����map�ļ���");
		}
		/// <summary>
		/// �����������ʼ����ڵ�Ԫ��Attr��
		/// </summary>
		public EnDtl this[int index]
		{			
			get
			{	
				return (EnDtl)this.InnerList[index];
			}
		}
		/// <summary>
		/// className
		/// </summary>
		/// <param name="className">������</param>
		/// <returns></returns>
		public EnDtl GetEnDtlByEnsName(string className)
		{
			foreach( EnDtl en in this)
			{
				if (en.EnsName==className)
					return en;
			}
			throw new Exception("@û���ҵ�������ϸ:"+className);
		}
		
	}
}
