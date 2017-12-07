using System;
using System.Web.UI.WebControls;
using System.Drawing;
using System.ComponentModel;

namespace  BP.Web.Controls
{
	/// <summary>
	/// GenerButton ��ժҪ˵����
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(System.Web.UI.WebControls.Button))]
	public class Btn : System.Web.UI.WebControls.Button
	{
		private BtnType _ShowType=BtnType.Normal;
		[Category("�Զ���"),Description("��ť��ʾ���ͣ�Ϊ��ʵ��ȫ��ͳһ����")]
		public BtnType ShowType
		{
			get
			{
				return _ShowType;
			}
			set
			{
				this._ShowType=value;
			}
		}	 
		/// <summary>
		/// ��ʾ��Ϣ��
		/// </summary>
		public string  Hit
		{
			get
			{ 
				return ViewState["_Hit"].ToString();
			}
			set
			{
				 ViewState["_Hit"] =value;
			}
		}
		/// <summary>
		/// Btn
		/// </summary>
		/// <param name="btntype">btntype</param>
		public Btn(BtnType btntype)
		{
			this.ShowType =btntype; 
			//this.PreRender += new System.EventHandler(this.BtnPreRender);
		}
		/// <summary>
		/// Btn
		/// </summary>
        public Btn()
        {
            this.Attributes["class"] = "Btn";
           // this.PreRender += new System.EventHandler(this.BtnPreRender);
        }
		private void BtnPreRender( object sender, System.EventArgs e )
		{
			//this.Attributes["onclick"] +="javascript:showRuning();";
//			if (this.Hit!=null)
//				this.Attributes["onclick"] = "javascript: return confirm('�Ƿ������'); ";
			switch (this.ShowType )
			{
				case BtnType.ConfirmHit :
					if (this.Text==null || this.Text=="")
						this.Text="ȷ��(A)";
					if (this.AccessKey==null) 
						this.AccessKey="a";
					 
					this.Attributes["onclick"] = " return confirm('"+this.Hit+"');";				
					break;
			 
				case BtnType.Refurbish :
					if (this.Text==null || this.Text=="") 			 
						this.Text="ˢ��(R)";
					if (this.AccessKey==null) 	
						this.AccessKey="r";
					break;
				case BtnType.Back :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(B)";
					if (this.AccessKey==null) 	
						this.AccessKey="b";
					break;
				case BtnType.Edit :
					if (this.Text==null || this.Text=="") 			 
						this.Text="�޸�(E)";
					if (this.AccessKey==null) 	
						this.AccessKey="e";
					break;
				case BtnType.Close :
					if (this.Text==null || this.Text=="") 			 
						this.Text="�ر�(Q)";
					if (this.AccessKey==null) 	
						this.AccessKey="q";

					this.Attributes["onclick"] += " window.close(); return false";
					
					break;
				case BtnType.Cancel :
					if (this.Text==null || this.Text=="") 			 
						this.Text="ȡ��(C)";
					if (this.AccessKey==null) 	
						this.AccessKey="c";
					break;				��
				case BtnType.Confirm :
					if (this.Text==null || this.Text=="")
						this.Text="ȷ��(O)";
					if (this.AccessKey==null)
						this.AccessKey="o";
					break;
				case BtnType.Search :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(F)";
					if (this.AccessKey==null)
						this.AccessKey="f";
					break;
				case BtnType.New :
					if (this.Text==null || this.Text=="") 			 
						this.Text="�½�(N)";
					if (this.AccessKey==null) 
						this.AccessKey="n";
					break;
				case BtnType.SaveAndNew :
					if (this.Text==null || this.Text=="") 			 
						this.Text="���沢�½�(R)";
					if (this.AccessKey==null) 
						this.AccessKey="n";
					break;
				case BtnType.Delete :
					if (this.Text==null || this.Text=="") 			 
						this.Text= "ɾ��(D)";
					if (this.AccessKey==null)
						this.AccessKey="c";
					if (this.Hit==null)
						this.Attributes["onclick"] += " return confirm('�˲���Ҫִ��ɾ�����Ƿ������');";
					else
						this.Attributes["onclick"] += " return confirm('�˲���Ҫִ��ɾ����["+this.Hit+"]���Ƿ������');";
					break;
				case BtnType.Export :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(G)";
					if (this.AccessKey==null) 	
						this.AccessKey="g";
					break;
				case BtnType.Insert :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(I)";
					if (this.AccessKey==null) 	
						this.AccessKey="i";
					break ;
				case BtnType.Print :
					if (this.Text==null || this.Text=="") 			 
						this.Text="��ӡ(P)";
					if (this.AccessKey==null) 	
						this.AccessKey="p";

					if (this.Hit==null)
						this.Attributes["onclick"] += " return confirm('�˲���Ҫִ�д�ӡ���Ƿ������');";
					else
						this.Attributes["onclick"] += " return confirm('�˲���Ҫִ�д�ӡ��["+this.Hit+"]���Ƿ������');";
					break ;
				case BtnType.Save :
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(S)";
					if (this.AccessKey==null)
						this.AccessKey="s";
					break;
				case BtnType.View:
					if (this.Text==null || this.Text=="") 			 
						this.Text="���(V)";
					if (this.AccessKey==null) 	
						this.AccessKey="v";
					break;
				case BtnType.Add:
					if (this.Text==null || this.Text=="") 			 
						this.Text="����(A)";
					if (this.AccessKey==null) 	
						this.AccessKey="a";
					break;
				case BtnType.SelectAll:
					if (this.Text==null || this.Text=="") 			 
						this.Text="ȫѡ��(A)";
					if (this.AccessKey==null) 	
						this.AccessKey="a";
					break;
				case BtnType.SelectNone:
					if (this.Text==null || this.Text=="") 			 
						this.Text="ȫ��ѡ(N)";
					if (this.AccessKey==null) 	
						this.AccessKey="n";
					break;
				case BtnType.Reomve:
					if (this.Text==null || this.Text=="") 			 
						this.Text="�Ƴ�(M)";
					if (this.AccessKey==null) 	
						this.AccessKey="m";

					if (this.Hit==null)
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ���Ƴ����Ƿ������');";
					else
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ���Ƴ���["+this.Hit+"]���Ƿ������');";

					break;
				default:
					if (this.Text==null || this.Text=="")
						this.Text="ȷ��(O)";
					if (this.AccessKey==null)
						this.AccessKey="o";
					break; 
			} 
			 
		}

		 
	}
}
