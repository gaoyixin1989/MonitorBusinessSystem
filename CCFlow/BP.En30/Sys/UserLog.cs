using System;
using System.Collections;
using BP.DA;
using BP.En;
using BP;
namespace BP.Sys
{
	/// <summary>
	/// �û���־
	/// </summary>
    public class UserLogAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// ��־���
        /// </summary>
        public const string LogFlag = "LogFlag";
        /// <summary>
        /// ��������
        /// </summary>
        public const string Docs = "Docs";
        /// <summary>
        /// ��¼����
        /// </summary>
        public const string RDT = "RDT";
        public const string IP = "IP";

    }
	/// <summary>
	/// �û���־
	/// </summary>
	public class UserLog: EntityMyPK
	{
        public override UAC HisUAC
        {
            get
            {
                var uac = new UAC();
                uac.Readonly();

                return uac;
            }
        }

		#region �û���־��Ϣ��ֵ�б�
		#endregion

		#region ��������
        public string IP
        {
            get
            {
                return this.GetValStringByKey(UserLogAttr.IP);
            }
            set
            {
                this.SetValByKey(UserLogAttr.IP, value);
            }
        }
        /// <summary>
        /// ��־��Ǽ�
        /// </summary>
        public string LogFlag
        {
            get
            {
                return this.GetValStringByKey(UserLogAttr.LogFlag);
            }
            set
            {
                this.SetValByKey(UserLogAttr.LogFlag, value);
            }
        }
		/// <summary>
		/// FK_Emp
		/// </summary>
		public string FK_Emp
		{
			get
			{
				return this.GetValStringByKey(UserLogAttr.FK_Emp) ; 
			}
			set
			{
				this.SetValByKey(UserLogAttr.FK_Emp,value) ; 
			}
		}
        public string RDT
        {
            get
            {
                return this.GetValStringByKey(UserLogAttr.RDT);
            }
            set
            {
                this.SetValByKey(UserLogAttr.RDT, value);
            }
        }
      
        public string Docs
        {
            get
            {
                return this.GetValStringByKey(UserLogAttr.Docs);
            }
            set
            {
                this.SetValByKey(UserLogAttr.Docs, value);
            }
        }
      
		#endregion

		#region ���췽��
		/// <summary>
		/// �û���־
		/// </summary>
		public UserLog()
		{
		}
	 
		/// <summary>
		/// EnMap
		/// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("Sys_UserLogT");
                map.DepositaryOfEntity = Depositary.None;
                map.DepositaryOfMap = Depositary.Application;

                map.EnDesc = "�û���־";
                map.EnType = EnType.Sys;
                map.AddMyPK();
                map.AddTBString(UserLogAttr.FK_Emp, null, "�û�", true, false, 0, 30, 20);
                map.AddTBString(UserLogAttr.IP, null, "IP", true, false, 0, 200, 20);
                map.AddTBString(UserLogAttr.LogFlag, null, "��ʶ", true, false, 0, 300, 20);
                map.AddTBString(UserLogAttr.Docs, null, "˵��", true, false, 0, 300, 20);
                map.AddTBString(UserLogAttr.RDT, null, "��¼����", true, false, 0, 20, 20);

                map.GetAttrByKey(this.PK).UIVisible = false;

                map.DTSearchKey = UserLogAttr.RDT;
                map.DTSearchWay = DTSearchWay.ByDate;

                this._enMap = map;
                return this._enMap;
            }
        }
		#endregion 

        #region ��д
        public override Entities GetNewEntities
        {
            get { return new UserLogs(); }
        }
        #endregion ��д
    }
	/// <summary>
	/// �û���־s
	/// </summary>
    public class UserLogs : EntitiesMyPK
    {
        #region ����
        public UserLogs()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="emp"></param>
        public UserLogs(string emp)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere(UserLogAttr.FK_Emp, emp);
            qo.DoQuery();
        }
        #endregion

        #region ��д
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new UserLog();
            }
        }
        #endregion

    }
}
