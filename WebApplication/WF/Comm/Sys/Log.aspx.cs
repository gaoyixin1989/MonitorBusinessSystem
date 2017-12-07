using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;


namespace CCFlow.Web.Comm.Sys
{
	/// <summary>
	/// Log ��ժҪ˵����
	/// </summary>
    public partial class UILog : BP.Web.WebPageAdmin
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{

            this.Label1.Text = this.GenerCaption("��־����");

			//this.GenerLabel(this.Label1,"��־����");

			System.IO.DirectoryInfo di= new System.IO.DirectoryInfo(BP.Sys.SystemConfig.PathOfLog  );
			if (di.Exists==false)
			{
				di.Create();
			}
			this.UCSys1.AddTable();
			this.UCSys1.Add("<TR>");
			this.UCSys1.AddTDTitle("ID");
			this.UCSys1.AddTDTitle("�ļ�����");
			this.UCSys1.AddTDTitle("��С");
			//this.UCSys1.AddTDTitle("��������");
			this.UCSys1.Add("</TR>");

			FileInfo[] fis =  di.GetFiles("*.*");
			int idx=0;
			foreach(FileInfo fi in fis)
			{
				idx++;
				this.UCSys1.Add("<TR onmouseover='TROver(this)' onmouseout='TROut(this)'>");
				this.UCSys1.Add("<TD class='Idx' >"+idx.ToString()+"</TD>");
				this.UCSys1.AddTD("<a href='../../Data/Log/"+fi.Name+"'>"+fi.Name+"</a>");
				this.UCSys1.AddTDNum( fi.Length.ToString("#")  );
				this.UCSys1.Add("</TR>");
			}
			this.UCSys1.Add("</Table>");

			//onmouseover='TROver(this)' onmouseout='TROut(this)'
		}

		#region Web ������������ɵĴ���
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: �õ����� ASP.NET Web ���������������ġ�
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
		/// �˷��������ݡ�
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
