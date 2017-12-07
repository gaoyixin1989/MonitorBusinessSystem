using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Services;

using i3.View;
using i3.ValueObject.Channels.OA.PART;
using i3.BusinessLogic.Channels.OA.PART;
using i3.ValueObject.Sys.Resource;

/// <summary>
/// 功能描述：物料信息管理
/// 创建日期：2012-11-07
/// 创建人  ：潘德军
/// </summary>
public partial class Channels_OA_Part_PartList : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Params["Action"] == "GetData")
        {
            GetData();
            Response.End();
        }
        else if (Request.Params["Action"] == "getDict")
        {
            string strResult = getDict(Request.Params["dictType"].ToString());
            Response.Write(strResult);
            Response.End();
        }
        
    }

    /// <summary>
    /// 获取下拉字典项
    /// </summary>
    /// <returns></returns>
    private string getDict(string strDictType)
    {
        //return getDictJsonString(strDictType);

        TSysDictVo TSysDictVo = new TSysDictVo();
        TSysDictVo.DICT_TYPE = strDictType;
        if (Request.Params["type"] != null)
        {
            if (Request.Params["type"].ToString() == "other")
            {
                TSysDictVo.DICT_CODE = "03,04,05,07";
            }
            else if (Request.Params["type"].ToString() == "drugs")
            {
                TSysDictVo.DICT_CODE = "01,02";
            }
            else if (Request.Params["type"].ToString() == "office")
            {
                TSysDictVo.DICT_CODE = "06";
            }
        }
        DataTable dt = new i3.BusinessLogic.Sys.Resource.TSysDictLogic().SelectByTable(TSysDictVo);
        return DataTableToJson(dt);
    }

    //获取数据
    private void GetData()
    {
        string strSortname = Request.Params["sortname"];
        string strSortorder = Request.Params["sortorder"];
        int intPageIdx = Convert.ToInt32(Request.Params["page"]);
        int intPagesize = Convert.ToInt32(Request.Params["pagesize"]);

        string strSrhPART_CODE = (Request.Params["SrhPART_CODE"] != null) ? Request.Params["SrhPART_CODE"] : "";
        string strSrhPART_NAME = (Request.Params["SrhPART_NAME"] != null) ? Request.Params["SrhPART_NAME"] : "";
        string strSrhPART_TYPE = (Request.Params["SrhPART_TYPE"] != null) ? Request.Params["SrhPART_TYPE"] : "";
        if (strSrhPART_TYPE == "" && Request.Params["type"] != null)
        {
            if (Request.Params["type"].ToString() == "other")
            {
                strSrhPART_TYPE = "03,04,05,07";
            }
            else if (Request.Params["type"].ToString() == "drugs")
            {
                strSrhPART_TYPE = "01,02";
            }
            else if (Request.Params["type"].ToString() == "office")
            {
                strSrhPART_TYPE = "06";
            }
        }

        TOaPartInfoVo objPart = new TOaPartInfoVo();
        objPart.IS_DEL = "0";
        objPart.PART_CODE = strSrhPART_CODE;
        objPart.PART_NAME = strSrhPART_NAME;
        objPart.PART_TYPE = strSrhPART_TYPE;
        objPart.SORT_FIELD = "PART_NAME asc,MODELS";
        objPart.SORT_TYPE = "asc";

        objPart.REMARK1 = "query";//huangjinjun add

        TOaPartInfoLogic logicPart = new TOaPartInfoLogic();

        int intTotalCount = logicPart.GetSelectResultCount(objPart); ;//总计的数据条数
        DataTable dt = logicPart.SelectByTable_ByJoin(objPart, intPageIdx, intPagesize);

        string strJson = CreateToJson(dt, intTotalCount);

        Response.Write(strJson);
    }

    /// <summary>
    /// 删除数据
    /// </summary>
    /// <param name="strValue">需要删除的数据</param>
    /// <returns></returns>
    [WebMethod]
    public static string deleteData(string strDelIDs)
    {
        if (strDelIDs.Length == 0)
            return "0";

        string[] arrDelIDs = strDelIDs.Split(',');

        bool isSuccess = true;
        for (int i = 0; i < arrDelIDs.Length; i++)
        {
            TOaPartInfoVo objPart = new TOaPartInfoVo();
            objPart.ID = arrDelIDs[i];
            objPart.IS_DEL = "1";

            isSuccess = new TOaPartInfoLogic().Edit(objPart);
        }

        if (isSuccess)
        {
            return "1";
        }
        else
        {
            return "0";
        }
    }

    /// <summary>
    /// 添加数据
    /// </summary>
    /// <param name="strMONITOR_TYPE_NAME">类型名称</param>
    /// <param name="strDESCRIPTION">描述</param>
    /// <returns></returns>
    [WebMethod]
    public static string AddData(string strPART_CODE, string strPART_NAME,string strPART_TYPE,string strUNIT,string strMODELS,string strINVENTORY,
        string strMEDIUM, string strPURE, string strALARM, string strUSEING, string strREQUEST, string strNARURE)
    {
        bool isSuccess = true;
        string Msg = "0";

        TOaPartInfoVo obj = new TOaPartInfoVo();
        obj.PART_CODE = strPART_CODE;
        obj.PART_NAME = strPART_NAME;
        obj.MODELS = strMODELS;
        obj.IS_DEL = "0";
        obj = new TOaPartInfoLogic().SelectByObject(obj);
        if (obj.ID == "")
        {
            TOaPartInfoVo objPart = new TOaPartInfoVo();
            objPart.ID = GetSerialNumber("t_oa_part_info_id");
            objPart.IS_DEL = "0";
            objPart.PART_CODE = strPART_CODE;
            objPart.PART_NAME = strPART_NAME;
            objPart.MODELS = strMODELS;
            objPart.PART_TYPE = strPART_TYPE;
            objPart.UNIT = strUNIT;
            objPart.INVENTORY = strINVENTORY;
            objPart.MEDIUM = strMEDIUM;
            objPart.PURE = strPURE;
            objPart.ALARM = strALARM;
            objPart.USEING = strUSEING;
            objPart.REQUEST = strREQUEST;
            objPart.NARURE = strNARURE;

            isSuccess = new TOaPartInfoLogic().Create(objPart);

            if (isSuccess)
            {
                Msg = "1";
            }
            else
            {
                Msg = "0";
            }
        }
        else
            Msg = "存在相同的物料信息，请检查";

        return Msg;
    }

    /// <summary>
    /// 编辑数据
    /// </summary>
    /// <param name="strID">id</param>
    /// <param name="strMONITOR_TYPE_NAME">类型名称</param>
    /// <param name="strDESCRIPTION">描述</param>
    /// <returns></returns>
    [WebMethod]
    public static string EditData(string strID, string strPART_CODE, string strPART_NAME, string strPART_TYPE, string strUNIT, string strMODELS, string strINVENTORY,
        string strMEDIUM, string strPURE, string strALARM, string strUSEING, string strREQUEST, string strNARURE, string isCheck)
    {
        bool isSuccess = true;
        string Msg = "0";

        if (isCheck == "true")
        {
            TOaPartInfoVo obj = new TOaPartInfoVo();
            obj.PART_CODE = strPART_CODE;
            obj.PART_NAME = strPART_NAME;
            obj.MODELS = strMODELS;
            obj.IS_DEL = "0";
            obj = new TOaPartInfoLogic().SelectByObject(obj);
            if (obj.ID != "")
                Msg = "存在相同的物料信息，请检查";
        }
        if (Msg == "0")
        {
            TOaPartInfoVo objPart = new TOaPartInfoVo();
            objPart.ID = strID;
            objPart.IS_DEL = "0";
            objPart.PART_CODE = strPART_CODE;
            objPart.PART_NAME = strPART_NAME;
            objPart.PART_TYPE = strPART_TYPE;
            objPart.UNIT = strUNIT;
            objPart.MODELS = strMODELS;
            objPart.INVENTORY = strINVENTORY;
            objPart.MEDIUM = strMEDIUM;
            objPart.PURE = strPURE;
            objPart.ALARM = strALARM;
            objPart.USEING = strUSEING;
            objPart.REQUEST = strREQUEST;
            objPart.NARURE = strNARURE;

            isSuccess = new TOaPartInfoLogic().Edit(objPart);

            if (isSuccess)
            {
                Msg = "1";
            }
            else
            {
                Msg = "0";
            }
        }

        return Msg;
    }
}