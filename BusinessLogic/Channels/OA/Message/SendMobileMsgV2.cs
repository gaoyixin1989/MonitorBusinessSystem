using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Szhto;

namespace i3.BusinessLogic.Channels.OA.Message
{
    public class SendMobileMsgV2
    {
        private Szhto.SzhtoSms axSzhtoSms1;
        
        private int nInitStat;

        public SendMobileMsgV2()
        {
            nInitStat = 0;

            axSzhtoSms1 = new Szhto.SzhtoSms();

            InitCOM();
        }

        ~SendMobileMsgV2()
        {
            axSzhtoSms1.YhCloseModem();
        }

        private void InitCOM()
        {
            if (axSzhtoSms1 == null)
            {
                return;
            }
            
            int port = 3;

            string str = "9600,N,8,1";      // 串口参数，固定
            string sN = "868995833001509";  // 短信猫序列号，随设备调整

            //打开端口
            string ret = axSzhtoSms1.YhOpenModem(port, str, sN);

            axSzhtoSms1.waittime = 10;  //设置多少秒后短信中心无任何反应视为失败
            // 打开失败
            if (ret.IndexOf("-1") >= 0)
            {
                nInitStat = 1;

            }
            // 打开成功
            else
            {
                nInitStat = 2;
            }
            i3.ValueObject.Channels.OA.Message.TOaMessageInfoVo obj = new ValueObject.Channels.OA.Message.TOaMessageInfoVo();
            obj.ACCEPT_TYPE = "2";
            i3.ValueObject.Channels.OA.Message.TOaMessageInfoVo objSet = new ValueObject.Channels.OA.Message.TOaMessageInfoVo();
            objSet.REMARK3 = nInitStat.ToString();
            new TOaMessageInfoLogic().Edit(objSet, obj);
        }

        public string SendMsg(string strSendByName, string strSendToName, string strNum, string strContent)
        {
            if (nInitStat != 2)
            {
                return "SendMsg error! init failed!";
            }
            
            string strRet = "failed";
            
            if (axSzhtoSms1.smsStatus != "") return strRet;
            
            int sBit = 0;  // 7为指定发7BIT短信，8指定发8Bit短信,0为自动判别,1为发至SP号,9发为送工业用16进制短信
            
            string sCen = "8613800763500";  // 短信中心号码 根据不同区域调整 具体请baidu

            string strNumTmp = ConvertNum(strNum);
            
            if (strNumTmp.IndexOf("error") >= 0)
            {
                strRet = strNumTmp;
            }
            else
            {
                string strTotalMsg = strSendToName + " 你好：" + strContent + " from " + strSendByName;
                strRet = axSzhtoSms1.YhSendSms(sCen, strNumTmp, strTotalMsg, sBit);
            }
            

            return strRet;
        }

        // 设备接口要求电话号码前必须加'86'
        private string ConvertNum(string strNum)
        {
            if (strNum.IndexOf("86") != 0)
            {
                strNum = "86" + strNum;
            }

            if (strNum.Length != 13)
            {
                return "error tel num!";
            }

            return strNum;
        }
    }
}
