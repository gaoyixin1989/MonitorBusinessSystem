using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// �����б�
	/// </summary>
	public class EntityNoNameAttr : EntityNoAttr
	{	
		/// <summary>
		/// ����
		/// </summary>
		public const string Name="Name";
        /// <summary>
        /// ���Ƽ��
        /// </summary>
        public const string NameOfS = "NameOfS";

	}
    public class EntityNoNameMyFileAttr : EntityNoMyFileAttr
    {
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
    }
	/// <summary>
	/// ���б�����ƵĻ���ʵ��
	/// </summary>
    abstract public class EntityNoName : EntityNo
    {
        #region ����

        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityNoNameAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityNoNameAttr.Name, value);
            }
        }
        //public string NameE
        //{
        //    get
        //    {
        //        return this.GetValStringByKey("NameE");
        //    }
        //    set
        //    {
        //        this.SetValByKey("NameE", value);
        //    }
        //}
        #endregion

        #region ���캯��
        /// <summary>
        /// 
        /// </summary>
        public EntityNoName()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="_No"></param>
        protected EntityNoName(string _No) : base(_No) { }
        #endregion

        #region ҵ���߼�����
        /// <summary>
        /// ������Ƶ�����.
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
            if (this.No.Trim().Length == 0)
            {
                if (this.EnMap.IsAutoGenerNo)
                    this.No = this.GenerNewNo;
                else
                    throw new Exception("@û�и�[" + this.EnDesc + " , " + this.Name + "]��������.");
            }

            if (this.EnMap.IsAllowRepeatName == false)
            {
                if (this.PKCount == 1)
                {
                    if (this.ExitsValueNum("Name", this.Name) >= 1)
                        throw new Exception("@����ʧ��[" + this.EnMap.EnDesc + "] ���[" + this.No + "]����[" + Name + "]�ظ�.");
                }
            }
            return base.beforeInsert();
        }
        protected override bool beforeUpdate()
        {
            if (this.EnMap.IsAllowRepeatName == false)
            {
                if (this.PKCount == 1)
                {
                    if (this.ExitsValueNum("Name", this.Name) >= 2)
                        throw new Exception("@����ʧ��[" + this.EnMap.EnDesc + "] ���[" + this.No + "]����[" + Name + "]�ظ�.");
                }
            }
            return base.beforeUpdate();
        }
        #endregion
    }
	/// <summary>
    /// ���б�����ƵĻ���ʵ��s
	/// </summary>
    abstract public class EntitiesNoName : EntitiesNo
    {
        /// <summary>
        /// ��������ӵ�����β������������Ѿ����ڣ������.
        /// </summary>
        /// <param name="entity">Ҫ��ӵĶ���</param>
        /// <returns>������ӵ��ĵط�</returns>
        public virtual int AddEntity(EntityNoName entity)
        {
            foreach (EntityNoName en in this)
            {
                if (en.No == entity.No)
                    return 0;
            }
            return this.InnerList.Add(entity);
        }
        /// <summary>
        /// ��������ӵ�����β������������Ѿ����ڣ������
        /// </summary>
        /// <param name="entity">Ҫ��ӵĶ���</param>
        /// <returns>������ӵ��ĵط�</returns>
        public virtual void Insert(int index, EntityNoName entity)
        {
            foreach (EntityNoName en in this)
            {
                if (en.No == entity.No)
                    return;
            }

            this.InnerList.Insert(index, entity);
        }
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public new EntityNoName this[int index]
        {
            get
            {
                return (EntityNoName)this.InnerList[index];
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public EntitiesNoName()
        {
        }
        /// <summary> 
        /// ��������ģ����ѯ
        /// </summary>
        /// <param name="likeName">likeName</param>
        /// <returns>���ز�ѯ��Num</returns>
        public int RetrieveByLikeName(string likeName)
        {
            QueryObject qo = new QueryObject(this);
            qo.AddWhere("Name", "like", " %" + likeName + "% ");
            return qo.DoQuery();
        }
        public override int RetrieveAll()
        {
            return base.RetrieveAll("No");
        }
    }
}
