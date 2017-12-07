using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Web.Services;

public partial class Channels_Base_Search_TotalSearchForSample : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    /// <summary>
    /// 获取质控类型名称
    /// </summary>
    /// <param name="strValue">质控类型</param>
    /// <returns></returns>
    [WebMethod]
    public static string getQcType(string strValue)
    {
        string strResult = "";
        switch (strValue)
        {
            case "0":
                strResult = "原始样";
                break;
            case "1":
                strResult = "现场空白";
                break;
            case "2":
                strResult = "现场加标";
                break;
            case "3":
                strResult = "现场平行";
                break;
            case "4":
                strResult = "实验室密码平行";
                break;
            case "5":
                strResult = "实验室空白";
                break;
            case "6":
                strResult = "实验室加标";
                break;
            case "7":
                strResult = "实验室明码平行";
                break;
            case "8":
                strResult = "标准样";
                break;
        }
        return strResult;
    }
}