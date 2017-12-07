using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// �����б�
	/// </summary>
    public class EntitySimpleTreeAttr
    {
        /// <summary>
        /// ���ṹ���
        /// </summary>
        public const string No = "No";
        /// <summary>
        /// ����
        /// </summary>
        public const string Name = "Name";
        /// <summary>
        /// ���ڱ��
        /// </summary>
        public const string ParentNo = "ParentNo";
    }
	/// <summary>
	/// ��ʵ��
	/// </summary>
    abstract public class EntitySimpleTree : Entity
    {
        #region ����
        public bool IsRoot
        {
            get
            {
                if (this.ParentNo == "-1" || this.ParentNo == "0")
                    return true;

                if (this.ParentNo == this.No)
                    return true;

                return false;
            }
        }
        /// <summary>
        /// Ψһ��ʾ
        /// </summary>
        public string No
        {
            get
            {
                return this.GetValStringByKey(EntitySimpleTreeAttr.No);
            }
            set
            {
                this.SetValByKey(EntitySimpleTreeAttr.No, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntitySimpleTreeAttr.Name);
            }
            set
            {
                this.SetValByKey(EntitySimpleTreeAttr.Name, value);
            }
        }
        /// <summary>
        /// ���ڵ���
        /// </summary>
        public string ParentNo
        {
            get
            {
                return this.GetValStringByKey(EntitySimpleTreeAttr.ParentNo);
            }
            set
            {
                this.SetValByKey(EntitySimpleTreeAttr.ParentNo, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        public override string PK
        {
            get
            {
                return EntitySimpleTreeAttr.No;
            }
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        public EntitySimpleTree()
        {
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        /// <param name="no">���</param>
        public EntitySimpleTree(string no)
        {
            if (string.IsNullOrEmpty(no))
                throw new Exception(this.EnDesc + "@�Ա�[" + this.EnDesc + "]���в�ѯǰ����ָ����š�");

            this.No = no;
            if (this.Retrieve() == 0)
                throw new Exception("@û��" + this._enMap.PhysicsTable + ", No = " + this.No + "�ļ�¼��");
        }
        #endregion

    }
	/// <summary>
    /// ��ʵ��s
	/// </summary>
    abstract public class EntitiesSimpleTree : Entities
    {
        /// <summary>
        /// ��ѯ�����ӽڵ�
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int RetrieveHisChinren(EntitySimpleTree en)
        {
            int i=this.Retrieve(EntitySimpleTreeAttr.ParentNo, en.No);
            this.AddEntity(en);
            return i + 1;
        }

        /// <summary>
        /// ��ȡ�����ӽڵ�
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public EntitiesTree GenerHisChinren(EntitySimpleTree en)
        {
            EntitiesTree ens = this.CreateInstance() as EntitiesTree;
            foreach (EntitySimpleTree item in ens)
            {
                if (en.ParentNo == en.No)
                    ens.AddEntity(item);
            }
            return ens;
        }
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public new EntitySimpleTree this[int index]
        {
            get
            {
                return (EntitySimpleTree)this.InnerList[index];
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public EntitiesSimpleTree()
        {
        }
        /// <summary>
        /// ��ѯȫ��
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            return base.RetrieveAll("No");
        }
    }
}
