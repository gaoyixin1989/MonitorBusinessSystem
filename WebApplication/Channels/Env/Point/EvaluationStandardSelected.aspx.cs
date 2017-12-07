using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Data;
using i3.View;
using i3.ValueObject.Channels.Base.Evaluation;
using i3.BusinessLogic.Channels.Base.Evaluation;

/// <summary>
/// 功能描述：评价标准与条件项选择
/// 创建日期：2012-11-15
/// 创建人  ：熊卫华
/// </summary>
public partial class Channels_Env_Point_EvaluationStandardSelected : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (Request["strMonitorId"] != null)
                this.hiddenMonitorId.Value = Request["strMonitorId"].ToString();

            if (Request["strValue"] != null)
                this.hiddenSelectedValue.Value = Request["strValue"].ToString();

            string strAction = Request["type"];
            string strResult = "";
            switch (strAction)
            {
                //获取评价标准节点树信息
                case "getTreeInfo":
                    strResult = getTreeInfo(Request["strMonitorId"].ToString());
                    Response.Write(strResult);
                    Response.End();
                    break;
                default: break;
            }
        }
    }
    public string getTreeInfo(string strMonitorId)
    {
        //组建DataTable
        DataTable objTable = new DataTable();
        //ID
        objTable.Columns.Add("ID", System.Type.GetType("System.String"));
        //评价标准ID
        objTable.Columns.Add("STANDARD_ID", System.Type.GetType("System.String"));
        //评价标准代码
        objTable.Columns.Add("STANDARD_CODE", System.Type.GetType("System.String"));
        //条件项代码
        objTable.Columns.Add("CONDITION_CODE", System.Type.GetType("System.String"));
        //名称
        objTable.Columns.Add("NAME", System.Type.GetType("System.String"));
        //父节点ID
        objTable.Columns.Add("PARENT_ID", System.Type.GetType("System.String"));
        //类型,0为评价标准，1为条件项
        objTable.Columns.Add("TYPE", System.Type.GetType("System.String"));

        //获取评价标准信息
        TBaseEvaluationInfoVo TBaseEvaluationInfoVo = new TBaseEvaluationInfoVo();
        TBaseEvaluationInfoVo.MONITOR_ID = strMonitorId;
        TBaseEvaluationInfoVo.IS_DEL = "0";
        DataTable dtEvaluationInfo = new TBaseEvaluationInfoLogic().SelectByTable(TBaseEvaluationInfoVo);

        //获取评价标准条件项信息
        TBaseEvaluationConInfoVo TBaseEvaluationConInfoVo = new TBaseEvaluationConInfoVo();
        TBaseEvaluationConInfoVo.IS_DEL = "0";
        DataTable dtEvaluationConInfo = new TBaseEvaluationConInfoLogic().SelectByTable(TBaseEvaluationConInfoVo);

        //遍历评价标准信息
        foreach (DataRow row in dtEvaluationInfo.Rows)
        {
            DataRow objTableNewRow = objTable.NewRow();
            string strStandardId = row["ID"].ToString();
            objTableNewRow["ID"] = strStandardId;
            objTableNewRow["STANDARD_ID"] = strStandardId;
            objTableNewRow["STANDARD_CODE"] = row["STANDARD_CODE"].ToString();
            objTableNewRow["NAME"] = row["STANDARD_NAME"].ToString();
            objTableNewRow["PARENT_ID"] = "0";
            objTableNewRow["TYPE"] = "0";
            objTable.Rows.Add(objTableNewRow);
            /*//遍历条件项
            foreach (DataRow rowCon in dtEvaluationConInfo.Rows)
            {
                if (rowCon["STANDARD_ID"].ToString() == strStandardId && isExist(dtEvaluationConInfo, rowCon["PARENT_ID"].ToString()))
                {
                    DataRow objTableConNewRow = objTable.NewRow();
                    objTableConNewRow["ID"] = rowCon["ID"].ToString();
                    objTableConNewRow["STANDARD_ID"] = rowCon["STANDARD_ID"].ToString();
                    objTableConNewRow["STANDARD_CODE"] = row["STANDARD_CODE"].ToString();
                    objTableConNewRow["CONDITION_CODE"] = rowCon["CONDITION_CODE"].ToString();
                    objTableConNewRow["NAME"] = rowCon["CONDITION_NAME"].ToString();
                    if (rowCon["PARENT_ID"].ToString() == "0")
                        objTableConNewRow["PARENT_ID"] = strStandardId;
                    else
                        objTableConNewRow["PARENT_ID"] = rowCon["PARENT_ID"].ToString();
                    objTableConNewRow["TYPE"] = "1";
                    objTable.Rows.Add(objTableConNewRow);
                }
            }*/
        }
        //将DataTable序列化之后返回
        return DataTableToJson(objTable);
    }
    /// <summary>
    /// 判断父节点ID是否在数据集中
    /// </summary>
    /// <param name="strParentId"></param>
    /// <returns></returns>
    public bool isExist(DataTable dt, string strParentId)
    {
        bool isExist = false;
        foreach (DataRow row in dt.Rows)
        {
            if (strParentId != "0")
            {
                if (row["ID"].ToString() == strParentId)
                {
                    isExist = true; break;
                }
            }
            else
            {
                isExist = true;
            }
        }
        return isExist;
    }
}