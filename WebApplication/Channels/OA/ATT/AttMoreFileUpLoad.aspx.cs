using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using System.Data;
using i3.BusinessLogic.Channels.OA.ATT;
using System.IO;
using System.Web.Services;
namespace n16
{

    public partial class Channels_OA_ATT_AttMoreFileUpLoad : PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region//新增
            if (Request["action"] != null && Request["action"] == "add")
            {
                this.RtnInfo(this.Request["ID"].ToString(), this.Request["filetype"].ToString());
            }
            #endregion
            #region//一开始加载时,或者上传完，点关闭，查询数据
            if (Request["action"] != null && Request["action"] == "getInfo")
            {
                this.RtnInfo(this.Request["ID"].ToString(), this.Request["filetype"].ToString());
            }
            #endregion
        }

        private void RtnInfo(string ID, string strFileType)
        {
            string json = string.Empty;
            if (!string.IsNullOrEmpty(this.Request["ID"].ToString()))
            {
                ID = this.Request["ID"].ToString();//填报数据的第一行的ID
                DataTable dts = new TOaAttLogic().DetailZW(ID, strFileType);
                if (dts.Rows.Count > 0)
                {
                    string Json = CreateToJson(dts, dts.Rows.Count);
                    Response.ContentType = "application/json;charset=utf-8";
                    Response.Write(Json);
                    Response.End();
                }
            }
        }

        #region//下载按钮
        protected void btnExcelOut_Click(object sender, EventArgs e)
        {
            string strValue = this.hidSave.Value;
            string fill_ID = this.hidType.Value.ToString();
            DataTable dt = new TOaAttLogic().Detail_type(strValue);
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
            isSuccess = new TOaAttLogic().DeleteInfo(strValue);
            return isSuccess == true ? "1" : "0";
        }
    }
}