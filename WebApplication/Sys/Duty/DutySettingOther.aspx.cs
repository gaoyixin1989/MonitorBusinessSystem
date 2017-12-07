using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.ValueObject.Sys.Duty;
using i3.BusinessLogic.Sys.Duty;
using i3.BusinessLogic.Channels.Base.MonitorType;
using i3.ValueObject.Channels.Base.MonitorType;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Item;
using i3.ValueObject.Sys.Resource;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.General;
using i3.BusinessLogic.Sys.General;
public partial class Sys_Duty_DutySettingOther : System.Web.UI.Page
{
    protected string MenuNodesJson="";
    private string nodes="", varNodes="";
    public List<string> treenodes = new List<string>();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strAction = Request["action"];
            if (!String.IsNullOrEmpty(strAction))
            {
                switch (strAction)
                {
                    case "GetMonitorsDutys":
                        GetMonitorsDutys();
                        MenuNodesJson = varNodes;
                        Response.Write(MenuNodesJson);
                        Response.End();
                        break;
                }
            }
        }
        
    }

    /// <summary>
    /// 获取监测类别
    /// </summary>
    /// <param name="iIndex"></param>
    /// <param name="iCount"></param>
    /// <returns></returns>
    public void  GetMonitorsDutys( )
    {
        DataTable dtSt = new DataTable();
        DataTable dtDict = new DataTable();
        TBaseMonitorTypeInfoVo objtd = new TBaseMonitorTypeInfoVo();
        objtd.IS_DEL = "0";
        dtSt = new TBaseMonitorTypeInfoLogic().SelectByTable(objtd);

        TSysDictVo objDit = new TSysDictVo();
        objDit.DICT_TYPE = "duty_other";
        dtDict = new TSysDictLogic().SelectByTable(objDit);

        if (dtSt.Rows.Count > 0)
        {
            int Sum = 0;
            foreach (DataRow drr in dtSt.Rows)
            {
                Sum++;
                nodes = "{ Id:'" + drr["ID"].ToString() + "',pId:'0',Code:'" + drr["ID"] + "',Name:'" + drr["MONITOR_TYPE_NAME"] + "'";
                if (Sum == 1)
                {
                    nodes += " ,open:true},\r\n";
                }
                else
                {
                    nodes += " },\r\n";
                }
                for (int i = 0; i < dtDict.Rows.Count; i++)
                {
                    nodes += "{ Id:'" + dtDict.Rows[i]["ID"] + "',pId:'" + drr["ID"] + "',Code:'" + dtDict.Rows[i]["DICT_CODE"].ToString() + "',Name:'" + dtDict.Rows[i]["DICT_TEXT"].ToString() + "'";
                    if (i == dtDict.Rows.Count - 1)
                    {
                        nodes += "}\r\n";
                    }
                    else
                    {
                        nodes += "},\r\n";
                    }
                }
                treenodes.Add(nodes);
            }

        }
        string rootNode = "{ Id:'0',pId:'',Code:'0',Name:'监测类别岗位职责集合',open:true},";
        varNodes = "["+rootNode +""+ String.Join(",\r\n", treenodes.ToArray()) + "]";
        
    }
}