using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web;
using BP.Sys;
using BP.WF.Port;
using System.Net.Mail;

namespace BP.WF
{
    /// <summary>
    /// ��Ϣ����
    /// </summary>
    public class SMSMsgType
    {
        /// <summary>
        /// �Զ�����Ϣ
        /// </summary>
        public const string Self = "Self";
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public const string CC = "CC";
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public const string ToDo = "ToDo";
        /// <summary>
        /// ����
        /// </summary>
        public const string Etc = "Etc";
    }
	/// <summary>
	/// ��Ϣ״̬
	/// </summary>
    public enum MsgSta
    {
        /// <summary>
        /// δ��ʼ
        /// </summary>
        UnRun,
        /// <summary>
        /// �ɹ�
        /// </summary>
        RunOK,
        /// <summary>
        /// ʧ��
        /// </summary>
        RunError,
        /// <summary>
        /// ��ֹ����
        /// </summary>
        Disable
    }
	/// <summary>
	/// ��Ϣ����
	/// </summary>
	public class SMSAttr:EntityMyPKAttr
	{
        /// <summary>
        /// ��Ϣ��ǣ��д˱�ǵĲ��ڷ��ͣ�
        /// </summary>
        public const string MsgFlag = "MsgFlag";
		/// <summary>
		/// ״̬ 0 δ���ͣ� 1 ���ͳɹ���2����ʧ��.
		/// </summary>
        public const string EmailSta = "EmailSta";
        /// <summary>
        /// �ʼ�
        /// </summary>
        public const string Email = "Email";
        /// <summary>
        /// �ʼ�����
        /// </summary>
        public const string EmailTitle = "EmailTitle";
        /// <summary>
        /// �ʼ�����
        /// </summary>
        public const string EmailDoc = "EmailDoc";
        /// <summary>
        /// ������
        /// </summary>
        public const string Sender = "Sender";
        /// <summary>
        /// ���͸�
        /// </summary>
        public const string SendTo = "SendTo";
        /// <summary>
        /// ��������
        /// </summary>
        public const string RDT = "RDT";
        /// <summary>
        /// ��������
        /// </summary>
        public const string SendDT = "SendDT";
        /// <summary>
        /// �Ƿ��ȡ
        /// </summary>
        public const string IsRead = "IsRead";
        /// <summary>
        /// ״̬ 0 δ���ͣ� 1 ���ͳɹ���2����ʧ��.
        /// </summary>
        public const string MobileSta = "MobileSta";
        /// <summary>
        /// �ֻ�
        /// </summary>
        public const string Mobile = "Mobile";
        /// <summary>
        /// �ֻ���Ϣ
        /// </summary>
        public const string MobileInfo = "MobileInfo";
        /// <summary>
        /// �Ƿ���ʾ����
        /// </summary>
        public const string IsAlert = "IsAlert";
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public const string MsgType = "MsgType";
	}
	/// <summary>
	/// ��Ϣ
	/// </summary> 
    public class SMS : EntityMyPK
    {
        #region �·��� 2013 
        /// <summary>
        /// �����ֻ���Ϣ
        /// </summary>
        /// <param name="Mobile">�ֻ���</param>
        /// <param name="doc">�ֻ�����</param>
        public static void SendSMS_del(string Mobile, string doc)
        {
            // �����������Ϣ����.
            if (BP.WF.Glo.IsEnableSysMessage == false)
                return;

            SMS sms = new SMS();
            sms.MyPK = DBAccess.GenerGUID();
            sms.HisEmailSta = MsgSta.UnRun;
            sms.Email = Mobile;
            sms.Title = doc;
            sms.Sender = BP.Web.WebUser.No;
            sms.RDT = BP.DA.DataType.CurrentDataTime;
            try
            {
                sms.Insert();
            }
            catch
            {
                sms.CheckPhysicsTable();
                sms.Insert();
            }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="userNo">������</param>
        /// <param name="msgTitle">����</param>
        /// <param name="msgDoc">����</param>
        /// <param name="msgFlag">���</param>
        /// <param name="msgType">����</param>
        public static void SendMsg(string userNo, string msgTitle, string msgDoc, string msgFlag, string msgType)
        {
           
            SMS sms = new SMS();
            sms.MyPK = DBAccess.GenerGUID();
            sms.HisEmailSta = MsgSta.UnRun;

            sms.Sender=WebUser.No;
            sms.SendTo = userNo;

            sms.Title = msgTitle;
            sms.DocOfEmail = msgDoc;

            sms.Sender = BP.Web.WebUser.No;
            sms.RDT = BP.DA.DataType.CurrentDataTime;
            
            sms.MsgFlag = msgFlag; // ��Ϣ��־.
            sms.MsgType = msgType; // ��Ϣ����.
            sms.Insert();
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="mobileNum">�ֻ�����</param>
        /// <param name="mobileInfo">������Ϣ</param>
        /// <param name="email">�ʼ�</param>
        /// <param name="title">����</param>
        /// <param name="infoBody">�ʼ�����</param>
        /// <param name="msgFlag">��Ϣ��ǣ�����Ϊ�ա�</param>
        /// <param name="guestNo">�û����</param>
        public static void SendMsg(string mobileNum, string mobileInfo, string email, string title, string
            infoBody, string msgFlag,string msgType,string guestNo)
        {

            SMS sms = new SMS();
            sms.Sender = WebUser.No;
            sms.RDT = BP.DA.DataType.CurrentDataTimess;
            sms.SendTo = guestNo;

            // �ʼ���Ϣ
            sms.HisEmailSta = MsgSta.UnRun;
            sms.Title = title;
            sms.DocOfEmail = infoBody;

            //�ֻ���Ϣ.
            sms.Mobile = mobileNum;
            sms.HisMobileSta = MsgSta.UnRun;
            sms.MobileInfo = mobileInfo;
            sms.MsgFlag = msgFlag; // ��Ϣ��־.

            if (string.IsNullOrEmpty(msgFlag))
            {
                sms.MyPK = DBAccess.GenerGUID();
                sms.Insert();
            }
            else
            {
                // ����Ѿ��и�PK,�Ͳ��ò�����.
                try
                {
                    sms.MyPK = msgFlag;
                    sms.Insert();
                }
                catch
                {
                }
            }
        }
        #endregion �·���

        #region �ֻ���������
        /// <summary>
        /// �ֻ�����
        /// </summary>
        public string Mobile
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Mobile);
            }
            set
            {
                SetValByKey(SMSAttr.Mobile, value);
            }
        }
        /// <summary>
        /// �ֻ�״̬
        /// </summary>
        public MsgSta HisMobileSta
        {
            get
            {
                return (MsgSta)this.GetValIntByKey(SMSAttr.MobileSta);
            } 
            set
            {
                SetValByKey(SMSAttr.MobileSta, (int)value);
            }
        }
        /// <summary>
        /// �ֻ���Ϣ
        /// </summary>
        public string MobileInfo
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.MobileInfo);
            }
            set
            {
                SetValByKey(SMSAttr.MobileInfo, value);
            }
        }
        #endregion

        #region  �ʼ�����
        /// <summary>
        /// �ʼ�״̬
        /// </summary>
        public MsgSta HisEmailSta
        {
            get
            {
                return (MsgSta)this.GetValIntByKey(SMSAttr.EmailSta);
            }
            set
            {
                this.SetValByKey(SMSAttr.EmailSta, (int)value);
            }
        }
        /// <summary>
        /// Email
        /// </summary>
        public string Email
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Email);
            }
            set
            {
                SetValByKey(SMSAttr.Email, value);
            }
        }
        /// <summary>
        /// ���͸�
        /// </summary>
        public string SendToEmpNo
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.SendTo);
            }
            set
            {
                SetValByKey(SMSAttr.SendTo, value);
            }
        }
        /// <summary>
        /// ���͸� 
        /// </summary>
        public string SendTo
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.SendTo);
            }
            set
            {
                SetValByKey(SMSAttr.SendTo, value);
            }
        }
        /// <summary>
        /// ��Ϣ���(�������������ⷢ���ظ�)
        /// </summary>
        public string MsgFlag
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.MsgFlag);
            }
            set
            {
                SetValByKey(SMSAttr.MsgFlag, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string MsgType
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.MsgType);
            }
            set
            {
                SetValByKey(SMSAttr.MsgType, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string Sender
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.Sender);
            }
            set
            {
                SetValByKey(SMSAttr.Sender, value);
            }
        }
        /// <summary>
        /// ��¼����
        /// </summary>
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.RDT);
            }
            set
            {
                SetValByKey(SMSAttr.RDT, value);
            }
        }
        /// <summary>
        /// ��������
        /// </summary>
        public string SendDT
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.SendDT);
            }
            set
            {
                SetValByKey(SMSAttr.SendDT, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get
            {
                return this.GetValStringByKey(SMSAttr.EmailTitle);
            }
            set
            {
                SetValByKey(SMSAttr.EmailTitle, value);
            }
        }
        /// <summary>
        /// �ʼ�����
        /// </summary>
        public string DocOfEmail
        {
            get
            {
                string doc = this.GetValStringByKey(SMSAttr.EmailDoc);
                if (string.IsNullOrEmpty(doc))
                    return this.Title;
                return doc.Replace('~', '\'');
            }
            set
            {
                SetValByKey(SMSAttr.EmailDoc, value);
            }
        }
        public string Doc
        {
            get
            {
                return this.DocOfEmail;
            }
            set
            {
                SetValByKey(SMSAttr.EmailDoc, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// UI�����ϵķ��ʿ���
        /// </summary>
        public override UAC HisUAC
        {
            get
            {
                UAC uac = new UAC();
                uac.OpenAll();
                return uac;
            }
        }
        /// <summary>
        /// ��Ϣ
        /// </summary>
        public SMS()
        {
        }
        /// <summary>
        /// Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_SMS");
                map.EnDesc = "��Ϣ";

                map.AddMyPK();

                map.AddTBString(SMSAttr.Sender, null, "������(����Ϊ��)", false, true, 0, 200, 20);
                map.AddTBString(SMSAttr.SendTo, null, "���͸�(����Ϊ��)", false, true, 0, 200, 20);
                map.AddTBDateTime(SMSAttr.RDT, "д��ʱ��", true, false);

                map.AddTBString(SMSAttr.Mobile, null, "�ֻ���(����Ϊ��)", false, true, 0, 30, 20);
                map.AddTBInt(SMSAttr.MobileSta, (int)MsgSta.UnRun, "��Ϣ״̬", true, true);
                map.AddTBString(SMSAttr.MobileInfo, null, "������Ϣ", false, true, 0, 1000, 20);

                map.AddTBString(SMSAttr.Email, null, "Email(����Ϊ��)", false, true, 0, 200, 20);
                map.AddTBInt(SMSAttr.EmailSta, (int)MsgSta.UnRun, "EmaiSta��Ϣ״̬", true, true);
                map.AddTBString(SMSAttr.EmailTitle, null, "����", false, true, 0, 3000, 20);
                map.AddTBStringDoc(SMSAttr.EmailDoc, null, "����", false, true);
                map.AddTBDateTime(SMSAttr.SendDT,null, "����ʱ��", false, false);

                map.AddTBInt(SMSAttr.IsRead, 0, "�Ƿ��ȡ?", true, true);
                map.AddTBInt(SMSAttr.IsAlert, 0, "�Ƿ���ʾ?", true, true);

                map.AddTBString(SMSAttr.MsgFlag, null, "��Ϣ���(���ڷ�ֹ�����ظ�)", false, true, 0, 200, 20);
                map.AddTBString(SMSAttr.MsgType, null, "��Ϣ����(CC����,ToDo����)", false, true, 0, 200, 20);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
        /// <summary>
        /// �����ʼ�
        /// </summary>
        /// <param name="mail"></param>
        /// <param name="mailTitle"></param>
        /// <param name="mailDoc"></param>
        /// <returns></returns>
        public static bool SendEmailNow(string mail, string mailTitle, string mailDoc)
        {
            System.Net.Mail.MailMessage myEmail = new System.Net.Mail.MailMessage();
            myEmail.From = new System.Net.Mail.MailAddress("ccflow.cn@gmail.com", "ccflow", System.Text.Encoding.UTF8);

            myEmail.To.Add(mail);
            myEmail.Subject = mailTitle;
            myEmail.SubjectEncoding = System.Text.Encoding.UTF8;//�ʼ��������

            myEmail.Body = mailDoc;
            myEmail.BodyEncoding = System.Text.Encoding.UTF8;//�ʼ����ݱ���
            myEmail.IsBodyHtml = true;//�Ƿ���HTML�ʼ�

            myEmail.Priority = MailPriority.High;//�ʼ����ȼ�

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential(SystemConfig.GetValByKey("SendEmailAddress", "ccflow.cn@gmail.com"),
                SystemConfig.GetValByKey("SendEmailPass", "ccflow123"));
            //����д������������
            client.Port = SystemConfig.GetValByKeyInt("SendEmailPort", 587); //ʹ�õĶ˿�
            client.Host = SystemConfig.GetValByKey("SendEmailHost", "smtp.gmail.com");

            // ����ssl����. 
            if (SystemConfig.GetValByKeyInt("SendEmailEnableSsl", 1) == 1)
                client.EnableSsl = true;  //����ssl����.
            else
                client.EnableSsl = false; //����ssl����.


            try
            {
                object userState = myEmail;
                client.SendAsync(myEmail, userState);
                return true;
            }
            catch
            {
                return false;
            }
        }
         
    }
	/// <summary>
	/// ��Ϣs
	/// </summary> 
    public class SMSs : Entities
    {
        public override Entity GetNewEntity
        {
            get
            {
                return new SMS();
            }
        }
        public SMSs()
        {
        }
    }
}
 