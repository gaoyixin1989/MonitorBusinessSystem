using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using i3.View;
using i3.BusinessLogic.Sys.Resource;
using i3.ValueObject.Sys.Resource;


/// <summary>
/// 功能描述：序列号编辑（添加和编辑）
/// 创建日期：2011-04-08 14:20
/// 创建人  ：郑义
/// 修改时间：2011-04-14 18:30
/// 修改人  ：郑义
/// 修改内容：更改所有符合开发规范
/// </summary>
public partial class Sys_Resource_SerialEdit : PageBase
{
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            if (!String.IsNullOrEmpty(Request.QueryString["ID"]))
            {
                this.id = Request.QueryString["ID"].ToString();
                InitSerialData();
            }
        }
    }
    /// <summary>
    /// 序列号ID
    /// </summary>
    public string id
    {
        get
        {
            object o = ViewState["id"];
            if (o == null) return "";
            else return o.ToString();
        }
        set
        {
            ViewState["id"] = value;
        }
    }
    /// <summary>
    /// 加载序列号数据
    /// </summary>
    private void InitSerialData()
    {
        SERIAL_CODE.Enabled = false;
        SERIAL_NUMBER.Enabled = false;
        LENGTH.Enabled = false;
        GRANULARITY.Enabled = false;
        MIN.Enabled = false;
        MAX.Enabled = false;
        //绑定数据
        BindObjectToControlsMode(new TSysSerialLogic().Details(this.id));
    }
    /// <summary>
    /// 输入验证
    /// </summary>
    protected bool InputValidation()
    {
        bool blResult = true;
        int iSERIAL_NUMBER = Convert.ToInt32(SERIAL_NUMBER.Text);//序列号
        int iLENGTH = Convert.ToInt32(LENGTH.Text);//长度
        int iGRANULARITY = Convert.ToInt32(GRANULARITY.Text);//粒度
        int iMIN = Convert.ToInt32(MIN.Text);//最小值
        int iMAX = Convert.ToInt32(MAX.Text);//最大值
        if (iLENGTH < 2 || iLENGTH > 9)
        {
            base.Alert("长度的值应在2－9之间!");
            blResult = false;
        }
        else if (iGRANULARITY < 1 || iGRANULARITY > 9)
        {
            base.Alert("粒度的值应在1－9之间!");
            blResult = false;
        }
        else if (iMIN > iSERIAL_NUMBER || iMAX < iSERIAL_NUMBER || iMIN >= iMAX)
        {
            base.Alert("请正确输入序列号、最小值和最大值!");
            blResult = false;
        }
        else if ((iMAX - iMIN) / iGRANULARITY<2)
        {
            base.Alert("请正确最小值、最大值和粒度!");
            blResult = false;
        }
        else if (iLENGTH != iMAX.ToString().Length)
        {
            base.Alert("请正确输入长度值或最大值!");
            blResult = false;
        }
        return blResult;
    }
    /// <summary>
    /// 保存按钮事件
    /// </summary>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //序列号对象
        TSysSerialVo voSerial = new TSysSerialVo();
        if (!String.IsNullOrEmpty(this.id))//如果ID不为空，则状态为更新，否则为新增
        {
            //获取页面数据对象
            //BindControlsToObjectMode();
            voSerial.SERIAL_CODE = SERIAL_CODE.Text;
            voSerial.SERIAL_NAME = SERIAL_NAME.Text;
            //设置ID
            voSerial.ID = this.id;


            TSysSerialVo tempSerialVo = new TSysSerialVo();
            tempSerialVo.SERIAL_CODE = voSerial.SERIAL_CODE;
            tempSerialVo.ID = new TSysSerialLogic().Details(tempSerialVo).ID;
            if (string.IsNullOrEmpty(tempSerialVo.ID) || tempSerialVo.ID == voSerial.ID)
            {
                if (new TSysSerialLogic().Edit(voSerial))
                {
                    base.WriteLog(i3.ValueObject.ObjectBase.LogType.EditSerial, voSerial.ID, LogInfo.UserInfo.USER_NAME + "修改序列号" + voSerial.SERIAL_CODE + "成功!");
                    base.Alert("修改成功!");
                }
                else
                {
                    base.Alert("修改失败!");
                }
            }
            else
            {
                base.Alert("该序列号已经存在!");
            }
        }
        else
        {
            if (!InputValidation())
                return;
            //获取页面数据对象
            BindControlsToObjectMode(voSerial);
            //判断该序列号是否存在
            TSysSerialVo tempSerialVo = new TSysSerialVo();
            tempSerialVo.SERIAL_CODE = voSerial.SERIAL_CODE;
            tempSerialVo.ID = new TSysSerialLogic().Details(tempSerialVo).ID;
            if (string.IsNullOrEmpty(tempSerialVo.ID))
            {
                voSerial.CREATE_TIME = DateTime.Now.ToString();
                if (new TSysSerialLogic().Create(voSerial))
                {
                    base.WriteLog(i3.ValueObject.ObjectBase.LogType.AddSerial, voSerial.ID, LogInfo.UserInfo.USER_NAME + "添加序列号" + voSerial.SERIAL_CODE + "成功!");
                    base.Alert("添加成功!");
                }
                else
                {
                    base.Alert("添加失败!");
                }
            }
            else
            {
                base.Alert("该序列号已经存在!");
            }
        }
    }
    
}