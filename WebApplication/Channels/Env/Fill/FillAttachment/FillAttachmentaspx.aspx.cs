using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using i3.View;
using i3.ValueObject.Channels.Env.Fill.Sea;
using i3.BusinessLogic.Channels.Env.Fill.Sea;
using i3.BusinessLogic.Channels.OA.ATT;
using System.Data;
using System.IO;
using i3.ValueObject.Channels.Env.Fill.DrinkSource;
using i3.BusinessLogic.Channels.Env.Fill.DrinkSource;
using i3.ValueObject.Channels.Env.Fill.PayFor;
using i3.BusinessLogic.Channels.Env.Fill.PayFor;

public partial class Channels_Env_Fill_FillAttachment_FillAttachmentaspx : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request["action"] != null)
        {
            string json = string.Empty;
            if (!string.IsNullOrEmpty(this.Request["IsInsert"].ToString()) && this.Request["IsInsert"].ToString() == "true")
            {
                #region//新增时调用
                if (!string.IsNullOrEmpty(this.Request["Save"].ToString()) && !string.IsNullOrEmpty(this.Request["filetype"].ToString()))
                {
                    string save = this.Request["Save"].ToString();
                    string fillType = this.Request["filetype"].ToString();
                    string strResult = this.SaveFWData(save, fillType, ID);
                    json = json = "{\"result\":\"success\",\"ID\":\"" + strResult + "\"}";
                    Response.ContentType = "application/json;charset=utf-8";
                    Response.Write(json);
                    Response.End();
                }
                #endregion
            }
            else
            {
                #region//一开始加载时,或者上传完，点关闭，查询数据
                if (!string.IsNullOrEmpty(this.Request["ID"].ToString()))
                {
                    string ID = this.Request["ID"].ToString();//填报数据的第一行的ID
                    if (ID.IndexOf("First") != -1)
                    {
               
                        DataTable dts = new TOaAttLogic().DetailFill_ID(ID);
                        if (dts.Rows.Count > 0)
                        {
                            string Json = CreateToJson(dts, dts.Rows.Count);
                            Response.ContentType = "application/json;charset=utf-8";
                            Response.Write(Json);
                            Response.End();
                        }
            
                    }
                }
                #endregion
            }
        }
    }

    #region//保存填报信息
    private string SaveFWData(string save, string filltype, string ID)
    {
        bool flag = false;
        string head = string.Empty;
        string strResult = string.Empty;
        if (!string.IsNullOrEmpty(filltype))
        {
            switch (filltype)
            {
                case "SeaFill":
                    TEnvFillSeaVo sea = new TEnvFillSeaVo();
                    sea.ID = GetSerialNumber(SerialType.T_ENV_FILL_SEA);
                    head = sea.ID;
                    flag = new TEnvFillSeaLogic().Create(sea);
                    break;
                case "DrinkSource":
                    TEnvFillDrinkSrcVo Drink = new TEnvFillDrinkSrcVo();
                    Drink.ID = GetSerialNumber(SerialType.T_ENV_FILL_DRINK_SRC);
                    head = Drink.ID;
                    flag = new TEnvFillDrinkSrcLogic().Create(Drink);
                    break;
                case "Payfor":
                    TEnvFillPayforVo payfor = new TEnvFillPayforVo();
                    payfor.ID = GetSerialNumber(SerialType.T_ENV_FILL_PAYFOR);
                    head = payfor.ID;
                    flag = new TEnvFillPayforLogic().Create(payfor);
                    break;
            }
            if (flag == true)
            {
                strResult = head;
            }
            else
            {
                strResult = "";
            }
        }
        return strResult;
    }
    #endregion


    #region//下载按钮
    protected void btnExcelOut_Click(object sender, EventArgs e)
    {
        string strValue = this.hidSave.Value.ToString();
        string fill_ID = this.hidType.Value.ToString();
        DataTable dt = new TOaAttLogic().Detail(strValue, fill_ID);
        if (dt.Rows.Count > 0)
        {
            string mastPath = System.Configuration.ConfigurationManager.AppSettings["AttPath"].ToString();
            string fileName = dt.Rows[0]["ATTACH_NAME"].ToString() + dt.Rows[0]["ATTACH_TYPE"].ToString();
            string filePath = mastPath + '\\' + dt.Rows[0]["UPLOAD_PATH"].ToString();
            if (File.Exists(filePath) == false)
            {
                LigerDialogAlert("附件不存在，下载失败", "error");
            }
            FileInfo fileInfo = new FileInfo(filePath);

            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + Server.UrlEncode(fileName));
            Response.AddHeader("Content-Length", fileInfo.Length.ToString());
            Response.AddHeader("Content-Transfer-Encoding", "binary");
            Response.ContentType = "application/octet-stream";
            Response.ContentEncoding = System.Text.Encoding.GetEncoding("gb2312");
            Response.WriteFile(fileInfo.FullName);
            Response.Flush();
            Response.End();
        }
    }
    #endregion

    /// <summary>
    /// 删除事件
    /// </summary>
    /// <param name="strValue">ID</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteDataInfo(string strValue)
    {
        bool isSuccess = false;
        isSuccess = new TOaAttLogic().DeleteDetail(strValue);
        return isSuccess == true ? "1" : "0";
    }
}