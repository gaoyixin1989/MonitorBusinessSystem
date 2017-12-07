using System;
//using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Text; 
using BP.En;

namespace BP.Web.Pub
{
	public class PubClass
	{
		
		#region ����һ��attr �õ�һ��contral
		/// <summary>
		///  ����һ��attr �õ�һ��contral
		/// </summary>
		/// <param name="attr">����</param>
		public void GetContralByAttr(Attr attr)
		{

			 
		}
		#endregion 


		#region SetInputFocus
		/// <summary>
		/// ��Ҫ�� body onload="setFocus()" ; 
		/// </summary>
		/// <param name="ControlName"></param>
		/// <param name="pg"></param>
		public static void SetInputFocus(string ControlName, Page pg)
		{
			StringBuilder sb = new StringBuilder("");
			sb.Append("<script language=javascript>");
			sb.Append("function setFocus() {");
			sb.Append("  if (document.forms[0]['"+ControlName+"'] != null)");
			sb.Append("  { document.forms[0]['"+ControlName+"'].focus(); }");
			sb.Append("}</script>");
            if (!pg.ClientScript.IsStartupScriptRegistered("InputFocusHandler"))
                pg.ClientScript.IsStartupScriptRegistered("InputFocusHandler");
			string clent= "<script language=javascript>document.onload=setFocus()</script>";
			pg.Response.Write(clent);
		} 		
		#endregion		
		

	    #region �ͻ��˵���ز�����

		/// <summary>
		/// 0�� ��ɫ���仯��
		/// 1�� Black. 
		/// 2�� Gree(ͨ��).
		/// 3�� Yellow�����棩.
		/// 4�� Red�����ش���. 
		/// </summary>
		/// <param name="Lab"></param>
		/// <param name="Mess"></param>
		/// <param name="Style"></param>
		public static void ShowMessage(System.Web.UI.WebControls.Label Lab, string Mess , int Style)
		{            
		}
		public static void ShowMessage(string mess , System.Web.UI.Page pg)
		{			
			//ShowMessage(mess,pg,false);
			return ;
		}
////		public static void CloseWindow(System.Web.UI.Page pg)
////		{
////			pg.Response.Write("<script language=javascript> window.close() </script>") ;
////		}
//		public static void ShowMessage(Exception ex, System.Web.UI.Page pg)
//		{			
//			ShowMessage(ex.Message,pg);
//		}
		/// <summary>
		/// ����page ������show message
		/// </summary>
		/// <param name="mess"></param>
		public static void ShowMessage(string mess )
		{
			string script= "<script language=JavaScript>alert('"+mess+"');</script>";
			System.Web.HttpContext.Current.Response.Write( script );
		}		
		public static void ToErrorPage(System.Exception ex)
		{
			ToErrorPage(ex.Message) ;
		}
		public static void ToErrorPage(string mess)
		{		
			System.Web.HttpContext.Current.Session["info"]=mess;
			System.Web.HttpContext.Current.Response.Redirect(System.Web.HttpContext.Current.Request.ApplicationPath+"/Portal/ErrPage.aspx");
		}				
		#endregion		 

		#region mess
		/// <summary>
		/// ��Ч����������, ���ʵ��
		/// </summary>
		public static void ShowMessageMSG_InvalidIntFloatString()
		{
			 PubClass.ShowMessage("��Ч����ֵ����������, ���ʵ��");
		}
		public static void ShowMessageMSG_OutDGMaxLength()
		{
			PubClass.ShowMessage("��¼�Ѿ������������÷�Χ��");
		}
		public static void ShowMessageMSG_DeleteOK()
		{
			PubClass.ShowMessage("ɾ���ɹ�����");
		}
		public static void ShowMessageMSG_SaveOK()
		{
			PubClass.ShowMessage("����ɹ�����");
		}
		public static void ShowMessageMSG_UpdateOK()
		{
			PubClass.ShowMessage("���³ɹ�����");
		}

		#endregion 

		
	}	
}
