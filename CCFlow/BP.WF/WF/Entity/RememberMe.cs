
using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;
using BP.En;

namespace BP.WF
{
	/// <summary>
	/// ������ ����
	/// </summary>
    public class RememberMeAttr
    {
        #region ��������
        /// <summary>
        /// �����ڵ�
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// ��ǰ�ڵ�
        /// </summary>
        public const string FK_Node = "FK_Node";
        /// <summary>
        /// ��ִ����Ա
        /// </summary>
        public const string Objs = "Objs";
        /// <summary>
        /// ��ִ����Ա
        /// </summary>
        public const string ObjsExt = "ObjsExt";
        /// <summary>
        /// ��ִ����Ա������
        /// </summary>
        public const string NumOfObjs = "NumOfObjs";
        /// <summary>
        /// ������Ա����ѡ)
        /// </summary>
        public const string Emps = "Emps";
        /// <summary>
        /// ������Ա��������ѡ)
        /// </summary>
        public const string NumOfEmps = "NumOfEmps";
        /// <summary>
        /// ������Ա����ѡ)
        /// </summary>
        public const string EmpsExt = "EmpsExt";
        #endregion
    }
	/// <summary>
	/// ������
	/// </summary>
    public class RememberMe : EntityMyPK
    {
        #region ����
        /// <summary>
        /// ����Ա
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(RememberMeAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(RememberMeAttr.FK_Emp, value);
                this.MyPK = this.FK_Node + "_" + BP.Web.WebUser.No;
            }
        }
        /// <summary>
        /// ��ǰ�ڵ�
        /// </summary>
        public int FK_Node
        {
            get
            {
                return this.GetValIntByKey(RememberMeAttr.FK_Node);
            }
            set
            {
                this.SetValByKey(RememberMeAttr.FK_Node, value);
                this.MyPK = this.FK_Node + "_" + BP.Web.WebUser.No;
            }
        }
        /// <summary>
        /// ��Ч�Ĺ�����Ա
        /// </summary>
        public string Objs
        {
            get
            {
                return this.GetValStringByKey(RememberMeAttr.Objs);
            }
            set
            {
                this.SetValByKey(RememberMeAttr.Objs, value);
            }
        }
        /// <summary>
        /// ��Ч�Ĳ�����Աext
        /// </summary>
        public string ObjsExt
        {
            get
            {
                return this.GetValStringByKey(RememberMeAttr.ObjsExt);
            }
            set
            {
                this.SetValByKey(RememberMeAttr.ObjsExt, value);
            }
        }
        /// <summary>
        /// ���е���Ա����.
        /// </summary>
        public int NumOfEmps
        {
            get
            {
                return this.Emps.Split('@').Length - 2;
            }
        }
        /// <summary>
        /// ���Դ������Ա����
        /// </summary>
        public int NumOfObjs
        {
            get
            {
                return this.Objs.Split('@').Length - 2;
            }
        }
        /// <summary>
        /// ���еĹ�����Ա
        /// </summary>
        public string Emps
        {
            get
            {
                return this.GetValStringByKey(RememberMeAttr.Emps);
            }
            set
            {
                this.SetValByKey(RememberMeAttr.Emps, value);
            }
        }
        /// <summary>
        /// ���еĹ�����Աext
        /// </summary>
        public string EmpsExt
        {
            get
            {
                string str = this.GetValStringByKey(RememberMeAttr.EmpsExt).Trim();
                if (str.Length == 0)
                    return str;

                if (str.Substring(str.Length - 1) == "��")
                    return str.Substring(0, str.Length - 1);
                else
                    return str;
            }
            set
            {
                this.SetValByKey(RememberMeAttr.EmpsExt, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// RememberMe
        /// </summary>
        public RememberMe()
        {
        }
        /// <summary>
        /// ��д���෽��
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;
                Map map = new Map("WF_RememberMe");
                map.EnDesc = "������";
                map.EnType = EnType.Admin;

                map.AddMyPK();

                map.AddTBInt(RememberMeAttr.FK_Node, 0, "�ڵ�", false, false);
                map.AddTBString(RememberMeAttr.FK_Emp, "", "��ǰ������Ա", true, false, 1, 30, 10);

                map.AddTBString(RememberMeAttr.Objs, "", "������Ա", true, false, 0, 4000, 10);
                map.AddTBString(RememberMeAttr.ObjsExt, "", "������ԱExt", true, false, 0, 4000, 10);

                map.AddTBString(RememberMeAttr.Emps, "", "���еĹ�����Ա", true, false, 0, 4000, 10);
                map.AddTBString(RememberMeAttr.EmpsExt, "", "������ԱExt", true, false, 0, 4000, 10);
                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion

        protected override bool beforeUpdateInsertAction()
        {
            this.FK_Emp = BP.Web.WebUser.No;
            this.MyPK = this.FK_Node + "_" + this.FK_Emp;
            return base.beforeUpdateInsertAction();
        }
    }
	/// <summary>
	/// ������
	/// </summary>
	public class RememberMes: Entities
	{
		#region ����
		/// <summary>
		/// �õ����� Entity 
		/// </summary>
		public override Entity GetNewEntity
		{
			get
			{
				return new RememberMe();
			}
		}
		/// <summary>
		/// RememberMe
		/// </summary>
		public RememberMes(){} 		 
		#endregion
	}
	
}
