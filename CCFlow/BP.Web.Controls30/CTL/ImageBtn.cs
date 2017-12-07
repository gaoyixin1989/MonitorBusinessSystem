using System;
using System.Web.UI.WebControls;
using System.Drawing;
using System.ComponentModel;


namespace  BP.Web.Controls
{
	/// <summary>
	/// GenerButton ��ժҪ˵����
	/// </summary>
	[System.Drawing.ToolboxBitmap(typeof(System.Web.UI.WebControls.ImageButton))]
	public class ImageBtn : System.Web.UI.WebControls.ImageButton 
	{
		public enum ImageBtnType
		{
			Normal,			 
			Confirm,
			Save,
			Search,
		    Cancel,
			Delete,
			Update,
			Insert,
			Edit,
			New,
			View,
			Close,
			Export,
			Print,
			Add,
			Reomve
		}		
		private ImageBtnType _ShowType=ImageBtnType.Normal;
		public ImageBtnType ShowType
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
		private string _Hit=null;
		/// <summary>
		/// ��ʾ��Ϣ��
		/// </summary>
		public string  Hit
		{
			get
			{ 
				return _Hit;
			}
			set
			{
				this._Hit=value;
			}
		}
		public ImageBtn()
		{	
			this.CssClass="ImageBtn"+WebUser.Style;
			this.PreRender += new System.EventHandler(this.LinkBtnPreRender);
		}
		private void LinkBtnPreRender( object sender, System.EventArgs e )
		{
			if (this.Hit!=null)
				this.Attributes["onclick"] = "javascript: return confirm('�Ƿ������'); ";

			switch (this.ShowType )
			{
				case ImageBtnType.Edit :
					this.ImageUrl="�޸�";
					 
					if (this.AccessKey==null)
						this.AccessKey="e";
					break;
				case ImageBtnType.Close :
					this.ImageUrl="�ر�";
					 
					if (this.AccessKey==null) 	
						this.AccessKey="q";					 
					break;
				case ImageBtnType.Cancel :
					this.ImageUrl="ȡ��";
					 
					if (this.AccessKey==null) 	
						this.AccessKey="c";
					break;				��
				case ImageBtnType.Confirm :
					this.ImageUrl="ȷ��";
					 
					if (this.AccessKey==null)
						this.AccessKey="o";
				    break;
				case ImageBtnType.Search :
					this.ImageUrl="����";
					 
					if (this.AccessKey==null)
						this.AccessKey="f";
					break;
				case ImageBtnType.New :
					this.ImageUrl="�½�";
					 
					if (this.AccessKey==null) 
						this.AccessKey="n";
					break;
				case ImageBtnType.Delete :
					this.ImageUrl="ɾ��";
					 
					if (this.AccessKey==null)
						this.AccessKey="c";
					if (this.Hit==null)
					    this.Attributes["onclick"] = " return confirm('�˲���Ҫִ��ɾ�����Ƿ������');";
					else
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ��ɾ����["+this.Hit+"]���Ƿ������');";

					break;
				case ImageBtnType.Export :
					this.ImageUrl="����";
					 
					if (this.AccessKey==null) 	
						this.AccessKey="g";
					break;
				case ImageBtnType.Insert :
					 
					this.ImageUrl="����";
					 
					if (this.AccessKey==null) 	
						this.AccessKey="i";
					break ;
				case ImageBtnType.Print :
					this.ImageUrl="ssss";
					if (this.AccessKey==null) 	
						this.AccessKey="p";

					if (this.Hit==null)
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ�д�ӡ���Ƿ������');";
					else
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ�д�ӡ��["+this.Hit+"]���Ƿ������');";
					break ;
				case ImageBtnType.Save :
					this.ImageUrl="ssss";
					if (this.AccessKey==null)
						this.AccessKey="s";
					break;
				case ImageBtnType.View:
					this.ImageUrl="ssss";
					if (this.AccessKey==null) 	
						this.AccessKey="v";
					break;
				case ImageBtnType.Add:
					this.ImageUrl="ssss";
					if (this.AccessKey==null) 	
						this.AccessKey="a";
					break;
				case ImageBtnType.Reomve:
					this.ImageUrl="�Ƴ�";
					 

					if (this.Hit==null)
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ���Ƴ����Ƿ������');";
					else
						this.Attributes["onclick"] = " return confirm('�˲���Ҫִ���Ƴ���["+this.Hit+"]���Ƿ������');";
					break;
				default:
					 this.ImageUrl="ȷ��";
					if (this.AccessKey==null)
						this.AccessKey="o";
					break; 
			}		 
			
			 
		}	
	 
		
		 
	}
}
