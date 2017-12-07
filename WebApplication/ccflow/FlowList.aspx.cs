using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.ValueObject.Sys.General;
using System.Xml;
using System.Web.Script.Serialization;
using WebApplication;
public partial class ccflow_FlowList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string opt = Request["opt"];
        if (!string.IsNullOrEmpty(opt))
        {   
            switch (opt)
            {
                case "getFlowList":
                    Response.Write(getFlowList());
                    break;
                default: break;
            }

            Response.End();
            return;
        }
    }

    protected string getFlowList()
    {
        
        UserLogInfo userLoginInfo = this.LogInfo;
        TSysUserVo user = userLoginInfo.UserInfo;
        string info = CCFlowFacade.GetEmpStart(user.USER_NAME);
        XmlDocument document = new XmlDocument();
        document.LoadXml(info);
        XmlNodeList rows = document.SelectNodes("/root/record/row");
        IList<Dictionary<string, string>> list = new List<Dictionary<string, string>>();
        IList<Dictionary<string, string>> sort = new List<Dictionary<string, string>>();
        for (int i = 0; i < rows.Count; i++)
        {
            XmlNode node = rows.Item(i);
            XmlNodeList children = node.ChildNodes;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            
            for (int j = 0; j < children.Count; j++)
            {
                XmlNode element = children[j];
                dic.Add(element.Name, Server.UrlDecode(element.InnerText));
                if(element.Name=="FlowPic"){
                    string FlowPicUrl = dic["FlowPic"];
                    string[] arrayUrl = FlowPicUrl.Split('/');
                    arrayUrl[3] = Server.UrlEncode(arrayUrl[3]);
                    string temp = string.Concat(FlowPicUrl, "/");
                    dic["FlowPic"] = temp.Substring(0, temp.LastIndexOf("/"));
                }
            }
            
            list.Add(dic);
        }
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(new { Rows = list});
    }
}