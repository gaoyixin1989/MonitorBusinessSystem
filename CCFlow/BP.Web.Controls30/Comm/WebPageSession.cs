using System;
using System.IO;
using System.Data;
using System.Web.UI.WebControls;
using BP.Web;
//using BP.Rpt ; 
using BP.DA;
using BP.En;
using System.Reflection;
using System.Text.RegularExpressions;


namespace BP.Web
{
    /// <summary>
    /// PortalPage ��ժҪ˵����
    /// </summary>
    public class WebPageSession : WebPage
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (this.Request.Browser.Cookies == false)
                throw new Exception("�����������֧��cookies���ܣ��޷�ʹ��ccflow��");

            if (WebUser.No==null)
                throw new Exception("����½��ʱ��̫���������µ�½��");
        }
    }
}

