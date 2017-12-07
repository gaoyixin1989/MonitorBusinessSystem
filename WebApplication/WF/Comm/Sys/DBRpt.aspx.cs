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
using BP.En;
using BP.DA;
using BP.Web;
using BP.Web.Comm;
using BP.Web.Controls;

namespace CCFlow.Web.Comm
{
	/// <summary>
	/// DBRpt ��ժҪ˵����
	/// </summary>
    public partial class DBRpt : BP.Web.WebPageAdmin
	{
		public ToolbarDDL DDL_Level
		{
			get
			{
				return this.BPToolBar1.GetDDLByKey("DDL_Level");
			}
		}
		public ToolbarLab Lab_Msg
		{
			get
			{
				return this.BPToolBar1.GetLabByKey("Lab_Msg");
			}
		}
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (WebUser.No!="admin")
			{
				this.ToErrorPage("@��û��Ȩ�޲鿴������־��");
				return;
			}
			this.BPToolBar1.ButtonClick+=new EventHandler(BPToolBar1_ButtonClick);


			//this.BPToolBar1.CheckChange+=new EventHandler(BPToolBar1_CheckChange);
			if (this.IsPostBack==false)
			{
				
				this.BPToolBar1.AddLab("lb","���ݿ������Լ��");

				//this.BPToolBar1.AddDDL("DDL_Level", new System.EventHandler( this.BPToolBar1_CheckChange ),true);

				this.BPToolBar1.AddDDL("DDL_Level",true);


				this.DDL_Level.Items.Add(new ListItem("��ȫ�����","1"));
				this.DDL_Level.Items.Add(new ListItem("��ȫ������","2"));
				this.DDL_Level.Items.Add(new ListItem("��ȫ�����","3"));
				this.BPToolBar1.AddBtn(NamesOfBtn.Confirm);
				this.BPToolBar1.AddSpt("spt1");
				this.BPToolBar1.AddBtn(NamesOfBtn.Help);
				this.SetText();
			}
			this.DDL_Level.SelectedIndexChanged +=new EventHandler(BPToolBar1_CheckChange);			

		}
		public void SetText()
		{
			string str="";
			if (this.DDL_Level.SelectedItemStringVal=="1")
			{
				str="��ȫ����ͣ�";
				str+="<BR>�ڴ˰�ȫ�����²�����ó����½����";
				str+="<BR>1��һ��������ֵ����Ϊ�գ�����null���ó����ݱ��档";
				str+="<BR>2��һ��������ֵ����Ϊ�գ�����null���ó����ݱ��档";
				str+="<BR>��ʾ�������������õ����ݱ��棬�밴�մ˼���ִ�У����Ǻܰ�ȫ�ģ�����������ݣ�";
				this.Label1.ForeColor=Color.Green;

			}
			else if (this.DDL_Level.SelectedItemStringVal=="2")
			{
				str="��ȫ�����У�";
				str+="<BR>�ڴ˰�ȫ�����²�����ó����½����";
				str+="<BR>1��һ��������ֵ����Ϊ�գ�����null���ó����ݱ��档";
				str+="<BR>2��һ��������ֵ����Ϊ�գ�����null���ó����ݱ��档";
				str+="<BR> ��ʾ���������õ����ݱ��棬ȥ��������ҿո��밴�մ˼���ִ�С�";
				this.Label1.ForeColor=Color.Black;
			}
			else
			{
				str="��ȫ����ߣ�";
				str+="<BR>�ڴ˰�ȫ�����²�����ó����½����";
				str+="<BR>1��һ��������ֵ����Ϊ�գ�����null���ó����ݱ��档";
				str+="<BR>2��һ��������ֵ����Ϊ�գ�����null���ó����ݱ��档";
				str+="<BR>��ʾ���������õ����ݱ��棬ȥ��������ҿո񣬲���ɾ��ʵ������̶�Ӧ���ϵļ�¼�밴�մ˼���ִ�У�ע�⣬���п���ɾ��һЩ�����õ����ݡ�";
				this.Label1.ForeColor=Color.Red;
			}
			this.Label1.Text=str;

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

		private void BPToolBar1_ButtonClick(object sender, EventArgs e)
		{
			ToolbarBtn btn = (ToolbarBtn)sender;

			if (btn.ID==NamesOfBtn.Confirm)
			{
                BP.DA.DBCheckLevel level = (BP.DA.DBCheckLevel)this.DDL_Level.SelectedItemIntVal;
				string rpt= BP.Sys.PubClass.DBRpt(level);
				this.Label1.Text =rpt;
			}
			else
			{
				this.Helper("DBRpt.htm");
			}

		}
		private void BPToolBar1_CheckChange(object sender, EventArgs e)
		{
			this.SetText();
		}
	}
}
