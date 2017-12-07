using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// EntityOIDNameAttr
	/// </summary>
	public class EntityOIDNameAttr:EntityOIDAttr
	{
		/// <summary>
		/// ����
		/// </summary>
		public const string Name="Name";
	}
	/// <summary>
	/// ���� OID Name ���Ե�ʵ��̳С�	
	/// </summary>
    abstract public class EntityOIDName : EntityOID
    {
        #region ����
        /// <summary>
        /// ����ֵ
        /// </summary>
        public override string PK
        {
            get
            {
                return "OID";
            }
        }
        public override string PKField
        {
            get
            {
                return "OID";
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        protected EntityOIDName() { }
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="oid">OID</param>
        protected EntityOIDName(int oid) : base(oid) { }
        #endregion

        #region ���Է���
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityOIDNameAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityOIDNameAttr.Name, value);
            }
        }
        /// <summary>
        /// �������Ʋ�ѯ��
        /// </summary>
        /// <returns>���ز�ѯ�����ĸ���</returns>
        public int RetrieveByName()
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere("Name", this.Name);
            return qo.DoQuery();
        }
        protected int LoadDir(string dir)
        {


            return 1;
        }
        
        protected override bool beforeUpdate()
        {
            if (this.EnMap.IsAllowRepeatName == false)
            {
                if (this.PKCount == 1)
                {
                    if (this.ExitsValueNum("Name", this.Name) >= 2)
                        throw new Exception("@����ʧ��[" + this.EnMap.EnDesc + "] OID=[" + this.OID + "]����[" + Name + "]�ظ�.");
                }
            }
            return base.beforeUpdate();
        }
        #endregion
    }
	/// <summary>
	/// ����OID Name ���Ե�ʵ��̳�
	/// </summary>
	abstract public class EntitiesOIDName : EntitiesOID
	{
		#region ����
		/// <summary>
		/// ����
		/// </summary>
		public EntitiesOIDName()
		{
			//
			// TODO: �ڴ˴���ӹ��캯���߼�
			//
		}		
		#endregion
	}
}
