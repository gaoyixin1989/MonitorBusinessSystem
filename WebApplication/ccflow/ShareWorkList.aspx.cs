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

public partial class ccflow_ShareWorkList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        string opt = Request["opt"];
        if (!string.IsNullOrEmpty(opt))
        {
            switch (opt)
            {
                case "getShareWorkList":
                    Response.Write(getShareWorkList());
                    break;
                default: break;
            }

            Response.End();
            return;
        }
    }

    protected string getShareWorkList()
    {
         
        UserLogInfo userLoginInfo = this.LogInfo;
        TSysUserVo user = userLoginInfo.UserInfo;
        IList<string[]> queryParams = new List<string[]>();
        if (!string.IsNullOrEmpty(Request["flowName"]))
        {
            queryParams.Add(new string[3] { "FlowName", Request["flowName"], "like" });
        }
        if (!string.IsNullOrEmpty(Request["endRDTStart"]))
        {
            queryParams.Add(new string[3] { "RDT", Request["endRDTStart"], "ge" });
        }
        if (!string.IsNullOrEmpty(Request["endRDTEnd"]))
        {
            queryParams.Add(new string[3] { "RDT", Request["endRDTEnd"], "le" });
        }

        int? page = null;
        int? pageSize = null;
        if (!string.IsNullOrEmpty(Request["page"]))
        {
            page = Convert.ToInt32(Request["page"]);
        }
        if (!string.IsNullOrEmpty(Request["pagesize"]))
        {
            pageSize = Convert.ToInt32(Request["pagesize"]);
        }
        Boolean isObtain = Convert.ToBoolean(Request["isObtain"]);
        string info = CCFlowFacade.GetSharing(user.USER_NAME, isObtain, queryParams.ToArray(), page, pageSize);
        XmlDocument document = new XmlDocument();
        document.LoadXml(info);
        XmlNodeList rows = document.SelectNodes("/root/record/row");
        XmlNode nodeCount = document.SelectSingleNode("/root/count");

        string total = nodeCount.InnerText;

        IList<Dictionary<string, string>> list = new List<Dictionary<string, string>>();

        for (int i = 0; i < rows.Count; i++)
        {
            XmlNode node = rows.Item(i);
            XmlNodeList children = node.ChildNodes;
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int j = 0; j < children.Count; j++)
            {
                XmlNode element = children[j];
                dic.Add(element.Name, Server.UrlDecode(element.InnerText));
            }
            DateTime sdt = Convert.ToDateTime(dic["SDT"]);
            DateTime now = DateTime.Now;
            if (DateTime.Compare(now, sdt) <= 0)
            {
                dic["NodeState"] = "正常";
            }
            else
            {
                dic["NodeState"] = "逾期";
            }
            list.Add(dic);
        }
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        if (page == null || pageSize == null)
        {
            return serializer.Serialize(new { Rows = list });
        }
        else
        {
            return serializer.Serialize(new { Total = total, Rows = list });
        }


    }
}