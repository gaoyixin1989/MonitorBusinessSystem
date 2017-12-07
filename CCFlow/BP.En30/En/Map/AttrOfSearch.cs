using System;
using System.Collections;
using BP.En;

namespace BP.En
{
	public enum OperatorSymbol
	{
		/// <summary>
		/// ����
		/// </summary>
		DaYu,
		/// <summary>
		/// ����
		/// </summary>
		DengYu,
		/// <summary>
		/// С��
		/// </summary>
		XiaoYu,
		/// <summary>
		/// ����
		/// </summary>
		Like,
	}
	/// <summary>
	/// �������Թ���
	/// </summary>
	public class AARef
	{
		/// <summary>
		/// Ŀ¼����
		/// </summary>
		public string CataAttr=null;
		/// <summary>
		/// ����key
		/// </summary>
		public string RefKey=null;
		/// <summary>
		/// ������
		/// </summary>
		public string SubAttr=null;
		/// <summary>
		/// �������Թ���
		/// </summary>
		/// <param name="CataAttr">����</param>
		/// <param name="RefKey"></param>
		/// <param name="SubAttr"></param>
		public AARef(string cataAttr,string subAttr,string refKey)
		{
			this.CataAttr=cataAttr;
			this.SubAttr=subAttr;
			this.RefKey=refKey;

		}
	}
	public class AARefs : System.Collections.CollectionBase
	{
		#region ����
		public AARefs()
		{
		}
		public AARefs this[int index]
		{
			get
			{
				return (AARefs)this.InnerList[index];
			}
		}
		#endregion
		 
		#region ����һ����ѯ���ԡ�
		/// <summary>
		/// ����һ����ѯ����
		/// </summary>
		/// <param name="lab">��ǩ</param>
		/// <param name="refKey">ʵ�������</param>
		/// <param name="defaultvalue">Ĭ��ֵ</param>
		public void Add(string lab,string key, string refKey,string defaultSymbol, string defaultvalue, int tbWidth)
		{
			AttrOfSearch aos= new AttrOfSearch(key,lab,refKey,defaultSymbol,defaultvalue,tbWidth,false);
			this.InnerList.Add(aos);
		}
		#endregion
	}

	/// <summary>
	/// SearchKey ��ժҪ˵����
	/// ��������һ����¼�Ĵ�ţ����⡣
	/// </summary>
    public class AttrOfSearch
    {
        #region ��������
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        private bool _IsHidden = false;
        /// <summary>
        /// �Ƿ�����
        /// </summary>
        public bool IsHidden
        {
            get
            {
                return _IsHidden;
            }
            set
            {
                _IsHidden = value;
            }
        }
        /// <summary>
        /// �����Ƿ����
        /// </summary>
        private bool _SymbolEnable = true;
        /// <summary>
        /// �����Ƿ����
        /// </summary>
        public bool SymbolEnable
        {
            get
            {
                return _SymbolEnable;
            }
            set
            {
                _SymbolEnable = value;
            }
        }

        /// <summary>
        /// ��ǩ
        /// </summary>
        private string _Lab = "";
        /// <summary>
        /// ��ǩ
        /// </summary>
        public string Lab
        {
            get
            {
                return _Lab;
            }
            set
            {
                _Lab = value;
            }
        }
        /// <summary>
        /// ��ѯĬ��ֵ
        /// </summary>
        private string _DefaultVal = "";
        /// <summary>
        /// OperatorKey
        /// </summary>
        public string DefaultVal
        {
            get
            {
                return _DefaultVal;
            }
            set
            {
                _DefaultVal = value;
            }
        }
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public string DefaultValRun
        {
            get
            {
                if (_DefaultVal == null)
                    return null;

                if (_DefaultVal.Contains("@"))
                {
                    if (_DefaultVal.Contains("@WebUser.No"))
                        return _DefaultVal.Replace("@WebUser.No", Web.WebUser.No);

                    if (_DefaultVal.Contains("@WebUser.Name"))
                        return _DefaultVal.Replace("@WebUser.Name", Web.WebUser.Name);

                    if (_DefaultVal.Contains("@WebUser.FK_Dept"))
                        return _DefaultVal.Replace("@WebUser.FK_Dept", Web.WebUser.FK_Dept);

                    if (_DefaultVal.Contains("@WebUser.FK_DeptName"))
                        return _DefaultVal.Replace("@WebUser.FK_DeptName", Web.WebUser.FK_DeptName);

                    if (_DefaultVal.Contains("@WebUser.FK_DeptNameOfFull"))
                        return _DefaultVal.Replace("@WebUser.FK_DeptNameOfFull", Web.WebUser.FK_DeptNameOfFull);

                    //if (_DefaultVal.Contains("@WebUser.FK_Unit"))
                    //    return _DefaultVal.Replace("@WebUser.FK_Unit", Web.WebUser.FK_Unit);

                }
                return _DefaultVal;
            }
        }
        /// <summary>
        /// Ĭ�ϵĲ�������.
        /// </summary>
        private string _defaultSymbol = "=";
        /// <summary>
        /// ��������
        /// </summary>
        public string DefaultSymbol
        {
            get
            {
                return _defaultSymbol;
            }
            set
            {
                _defaultSymbol = value;
            }
        }
        /// <summary>
        /// ��Ӧ������
        /// </summary>
        private string _RefAttr = "";
        /// <summary>
        /// ��Ӧ������
        /// </summary>
        public string RefAttrKey
        {
            get
            {
                return _RefAttr;
            }
            set
            {
                _RefAttr = value;
            }
        }
        /// <summary>
        /// Key
        /// </summary>
        private string _Key = "";
        /// <summary>
        /// Key
        /// </summary>
        public string Key
        {
            get
            {
                return _Key;
            }
            set
            {
                _Key = value;
            }
        }
        /// <summary>
        /// TB ���
        /// </summary>
        private int _TBWidth = 10;
        /// <summary>
        /// TBWidth 
        /// </summary>
        public int TBWidth
        {
            get
            {
                return _TBWidth;
            }
            set
            {
                _TBWidth = value;
            }
        }
        #endregion

        #region ���췽��
        /// <summary>
        /// ����һ����ͨ�Ĳ�ѯ����
        /// </summary>
        public AttrOfSearch(string key, string lab, string refAttr, string DefaultSymbol, string defaultValue, int tbwidth, bool isHidden)
        {
            this.Key = key;
            this.Lab = lab;
            this.RefAttrKey = refAttr;
            this.DefaultSymbol = DefaultSymbol;
            this.DefaultVal = defaultValue;
            this.TBWidth = tbwidth;
            this.IsHidden = isHidden;
        }
        #endregion
    }
	/// <summary>
	/// SearchKey ����
	/// </summary>
	public class AttrsOfSearch : System.Collections.CollectionBase
	{
		#region ����
		public AttrsOfSearch()
		{
		}
		public AttrsOfSearch this[int index]
		{
			get
			{
				return (AttrsOfSearch)this.InnerList[index];
			}
		}
		#endregion
		 
		#region ����һ����ѯ���ԡ�
		/// <summary>
		/// ����һ�����صĲ�ѯ����
		/// </summary>
		/// <param name="refKey">����key</param>
		/// <param name="symbol">��������</param>
		/// <param name="val">������val.</param>
		public void AddHidden(string refKey,string symbol, string val)
		{
			AttrOfSearch aos= new AttrOfSearch( "K"+this.InnerList.Count,refKey,refKey,symbol,val,0,true);
			this.InnerList.Add(aos);
		}
		/// <summary>
		/// ����һ����ѯ����
		/// </summary>
		/// <param name="lab">��ǩ</param>
		/// <param name="refKey">ʵ�������</param>
		/// <param name="defaultvalue">Ĭ��ֵ</param>
		public void Add(string lab, string refKey,string defaultSymbol, string defaultvalue, int tbWidth)
		{
			AttrOfSearch aos= new AttrOfSearch( "K"+this.InnerList.Count,lab,refKey,defaultSymbol,defaultvalue,tbWidth,false);
			this.InnerList.Add(aos);
		}
		public void Add( AttrOfSearch en)
		{
			this.InnerList.Add(en);
		}

		/// <summary>
		/// ����2�����ԡ�
		/// </summary>
		/// <param name="lab">����</param>
		/// <param name="refKey">������Key</param>
		/// <param name="defaultvalueOfFrom">Ĭ��ֵ��</param>
		/// <param name="defaultvalueOfTo">Ĭ��ֵ��</param>
		/// <param name="tbWidth">���</param>
		public void AddFromTo(string lab,string refKey,string defaultvalueOfFrom, string defaultvalueOfTo, int tbWidth)
		{
			AttrOfSearch aos= new AttrOfSearch( "Form_"+refKey,lab+"��",refKey,">=", defaultvalueOfFrom,tbWidth,false);
			aos.SymbolEnable=false;
			this.InnerList.Add(aos);

			AttrOfSearch aos1= new AttrOfSearch( "To_"+refKey,"��",refKey,  "<=" , defaultvalueOfTo,tbWidth,false);
			aos1.SymbolEnable=false;
			this.InnerList.Add(aos1);

		}
		#endregion
	}
}
