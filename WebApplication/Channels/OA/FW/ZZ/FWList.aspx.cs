using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
using System.Data;

using i3.View;
using i3.ValueObject.Channels.OA.FW;
using i3.BusinessLogic.Channels.OA.FW;
using i3.BusinessLogic.Channels.Mis.Monitor.Sample;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Text;
using NPOI.SS.UserModel;

namespace n9
{
    /// <summary>
    /// 功能描述：发文列表
    /// 创建日期：2013-6-28
    /// 创建人  ：李焕明
    /// </summary>
    public partial class Channels_OA_FW_QHD_FWList : ZZFWBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //定义结果
            string strResult = "";

            if (!Page.IsPostBack)
            {
                //获取列表信息
                if (Request["type"] != null && Request["type"].ToString() == "GetFWViewList")
                {
                    strResult = getFWList(Request.QueryString["strFWStatus"].ToString());
                    Response.Write(strResult);
                    Response.End();
                }
                if (Request["type"] != null && Request["type"].ToString() == "GetData")
                {
                    //查询信息
                    strResult = GetData(Request["Date"].ToString(), Request["Status"].ToString(), Request["Number"].ToString());
                    Response.Write(strResult);
                    Response.End();
                }
            }
        }

        //获取发文信息
        private string getFWList(string strFWStatus)
        {
            string strSortname = Request.Params["sortname"];
            string strSortorder = Request.Params["sortorder"];
            //当前页面
            int intPageIndex = Convert.ToInt32(Request.Params["page"]);
            //每页记录数
            int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

            TOaFwInfoVo objFWInfo = new TOaFwInfoVo();
            objFWInfo.FW_STATUS = strFWStatus;
            objFWInfo.DRAFT_ID = LogInfo.UserInfo.ID;
            int intTotalCount = new TOaFwInfoLogic().GetSelectResultCount(objFWInfo);
            DataTable dt = new TOaFwInfoLogic().SelectByTable(objFWInfo, intPageIndex, intPageSize);
            string strJson = LigerGridDataToJson(dt, intTotalCount);

            return strJson;
        }

        // 删除发文信息
        [WebMethod]
        public static string deleteFWInfo(string strValue)
        {
            bool isSuccess = new TOaFwInfoLogic().Delete(strValue);

            return isSuccess == true ? "1" : "0";
        }

        /// <summary>
        /// 导出、打印发文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnImport_Click(object sender, EventArgs e)
        {
            string fwID = this.hidFwId.Value;

            FWExport(fwID);
        }

        //查询信息
        private string GetData(string Date, string Status, string Number)
        {
            string strSortname = Request.Params["sortname"];
            string strSortorder = Request.Params["sortorder"];
            //当前页面
            int intPageIndex = Convert.ToInt32(Request.Params["page"]);
            //每页记录数
            int intPageSize = Convert.ToInt32(Request.Params["pagesize"]);

            TOaFwInfoVo objFWInfo = new TOaFwInfoVo();
            objFWInfo.FW_STATUS = Status;//状态
            objFWInfo.FWNO = Number;//发文编码
            objFWInfo.FW_DATE = Date;//发文日期
            //objFWInfo.DRAFT_ID = LogInfo.UserInfo.ID;
            int intTotalCount = new TOaFwInfoLogic().GetSelectCount(objFWInfo);
            DataTable dt = new TOaFwInfoLogic().SelectTable(objFWInfo, intPageIndex, intPageSize);
            string strJson = LigerGridDataToJson(dt, intTotalCount);
            return strJson;
        }
    }
}