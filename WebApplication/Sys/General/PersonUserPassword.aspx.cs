using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using i3.View;
using i3.BusinessLogic.Sys.General;
using i3.ValueObject.Sys.General;

/// <summary>
/// 功能描述：登陆用户个人密码管理
/// 创建日期：2011-04-15 10:10
/// 创建人  ：郑义
/// </summary>
public partial class Sys_General_PersonUserPassword : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string strUserID = LogInfo.UserInfo.ID;
            if (Request["strUserID"] != null)
            {
                strUserID = Request["strUserID"].ToString();
            }

            InitUserData(strUserID);
        }
    }
    #region 函数
    /// <summary>
    /// 初始化用户信息
    /// </summary>
    /// <param name="strID">用户ID</param>
    protected void InitUserData(string strID)
    {
        TSysUserVo objUserVo = new TSysUserLogic().Details(strID);
        BindObjectToControlsMode(objUserVo);

    }
    /// <summary>
    /// 设置用户密码
    /// </summary>
    protected void SetPassword()
    {
        string strUserID = LogInfo.UserInfo.ID;
        if (Request["strUserID"] != null)
        {
            strUserID = Request["strUserID"].ToString();
        }

        if (USER_PWD_ORG.Text == "" && LogInfo.UserInfo.ID != "000000001")
        {
            Alert("请输入原密码");
            lbMsg.Text = "请输入原密码";
            return;
        }
        //修改 潘德军，2013-8-5，修改原逻辑错误
        if (LogInfo.UserInfo.ID != "000000001")
        {
            TSysUserVo objUserVo_Temp = new TSysUserVo();
            objUserVo_Temp.USER_NAME = USER_NAME.Text;
            objUserVo_Temp.USER_PWD = ToMD5(USER_PWD_ORG.Text);
            TSysUserLogic objUserLogic_Temp = new TSysUserLogic();
            DataTable dtUserTemp = objUserLogic_Temp.SelectByTable(objUserVo_Temp);
            if (dtUserTemp.Rows.Count <= 0 && LogInfo.UserInfo.ID != "000000001")
            {
                Alert("请输入正确的原密码");
                lbMsg.Text = "请输入正确的原密码";
                return;
            }
        }
        if (USER_PWD.Text != USER_PWD_CONFIRM.Text)
        {
            Alert("两次输入的新密码不一致,请重新输入");
            lbMsg.Text = "两次输入的新密码不一致,请重新输入";
            return;
        }
        //修改密码 
        TSysUserVo objUserVo = new TSysUserVo();
        //objUserVo = (TSysUserVo)BindControlsToObjectMode(objUserVo);
        TSysUserLogic logicUser = new TSysUserLogic(objUserVo);
        objUserVo.USER_PWD = ToMD5(USER_PWD.Text);//密码要加密
        objUserVo.ID = strUserID;
        if (logicUser.Edit(objUserVo))
        {
            Alert("修改成功");
            lbMsg.Text = "修改成功";
            base.WriteLog(i3.ValueObject.ObjectBase.LogType.EditPassWord, objUserVo.ID, LogInfo.UserInfo.USER_NAME + "修改用户： " + objUserVo.USER_NAME + " 的密码成功!");
        }
        else
        {
            Alert("修改失败");
            lbMsg.Text = "修改失败";
        }

    }
    #endregion

    #region 事件
    /// <summary>
    /// 保存用户密码按钮点击事件
    /// </summary>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        SetPassword();
    }
    #endregion
}