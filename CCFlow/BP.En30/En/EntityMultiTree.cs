using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// �����б�
	/// </summary>
    public class EntityMultiTreeAttr
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
        /// <summary>
        /// �����
        /// </summary>
        public const string TreeNo = "TreeNo";
        /// <summary>
        /// ˳���
        /// </summary>
        public const string Idx = "Idx";
        /// <summary>
        /// �Ƿ���Ŀ¼
        /// </summary>
        public const string IsDir = "IsDir";
    }
	/// <summary>
	/// �����ʵ��
	/// </summary>
    abstract public class EntityMultiTree : Entity
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
                return this.GetValStringByKey(EntityMultiTreeAttr.No);
            }
            set
            {
                this.SetValByKey(EntityMultiTreeAttr.No, value);
            }
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        public string TreeNo
        {
            get
            {
                return this.GetValStringByKey(EntityMultiTreeAttr.TreeNo);
            }
            set
            {
                this.SetValByKey(EntityMultiTreeAttr.TreeNo, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityMultiTreeAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityMultiTreeAttr.Name, value);
            }
        }
        /// <summary>
        /// ���ڵ���
        /// </summary>
        public string ParentNo
        {
            get
            {
                return this.GetValStringByKey(EntityMultiTreeAttr.ParentNo);
            }
            set
            {
                this.SetValByKey(EntityMultiTreeAttr.ParentNo, value);
            }
        }
        /// <summary>
        /// �Ƿ���Ŀ¼
        /// </summary>
        public bool IsDir
        {
            get
            {
                return this.GetValBooleanByKey(EntityMultiTreeAttr.IsDir);
            }
            set
            {
                this.SetValByKey(EntityMultiTreeAttr.IsDir, value);
            }
        }
        /// <summary>
        /// ˳���
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(EntityMultiTreeAttr.Idx);
            }
            set
            {
                this.SetValByKey(EntityMultiTreeAttr.Idx, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public int Grade
        {
            get
            {
                return this.TreeNo.Length / 2;
            }
        }
        #endregion

        #region ��Ҫ��д.
        /// <summary>
        /// �����������ֶ�.
        /// ���������̱�����, �������̱���ֶΣ���Ҫʵ������д.
        /// </summary>
        public abstract string RefObjField
        {
            get;
        }
        #endregion ��Ҫ��д.

        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        public override string PK
        {
            get
            {
                return EntityMultiTreeAttr.No;
            }
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        public EntityMultiTree()
        {
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        /// <param name="no">���</param>
        public EntityMultiTree(string no)
        {
            if (string.IsNullOrEmpty(no))
                throw new Exception(this.EnDesc + "@�Ա�[" + this.EnDesc + "]���в�ѯǰ����ָ����š�");

            this.No = no;
            if (this.Retrieve() == 0)
                throw new Exception("@û��" + this._enMap.PhysicsTable + ", No = " + this.No + "�ļ�¼��");
        }
        #endregion

        #region ҵ���߼�����
        /// <summary>
        /// ��������treeNo
        /// </summary>
        public void ResetTreeNo()
        {
        }
        /// <summary>
        /// ������Ƶ�����.
        /// </summary>
        /// <returns></returns>
        protected override bool beforeInsert()
        {
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

        #region ����������õķ���
        /// <summary>
        /// �½�ͬ���ڵ�
        /// </summary>
        /// <returns></returns>
        public EntityMultiTree DoCreateSameLevelNode()
        {
            EntityMultiTree en = this.CreateInstance() as EntityMultiTree;
            en.No = BP.DA.DBAccess.GenerOID(this.ToString()).ToString(); // en.GenerNewNoByKey(EntityMultiTreeAttr.No);
            en.Name = "�½��ڵ�" + en.No;
            en.ParentNo = this.ParentNo;
            en.IsDir = false;
            en.TreeNo = this.GenerNewNoByKey(EntityMultiTreeAttr.TreeNo, EntityMultiTreeAttr.ParentNo, this.ParentNo);

             //��ʵ���ำֵ.
            en.SetValByKey(this.RefObjField, this.GetValStringByKey(this.RefObjField) ); 

            en.Insert();
            return en;
        }
        /// <summary>
        /// �½��ӽڵ�
        /// </summary>
        /// <returns></returns>
        public EntityMultiTree DoCreateSubNode()
        {
            EntityMultiTree en = this.CreateInstance() as EntityMultiTree;
            en.No = BP.DA.DBAccess.GenerOID(this.ToString()).ToString(); // en.GenerNewNoByKey(EntityMultiTreeAttr.No);
            en.Name = "�½��ڵ�" + en.No;
            en.ParentNo = this.No;
            en.IsDir = false;

            //��ʵ���ำֵ.
            en.SetValByKey(this.RefObjField, this.GetValStringByKey(this.RefObjField) ); 

            en.TreeNo = this.GenerNewNoByKey(EntityMultiTreeAttr.TreeNo, EntityMultiTreeAttr.ParentNo, this.No);
            if (en.TreeNo.Substring(en.TreeNo.Length - 2) == "01")
                en.TreeNo = this.TreeNo + "01";
            en.Insert();

            // ���ô˽ڵ���Ŀ¼
            if (this.IsDir == false)
            {
                this.IsDir = true;
                this.Update(EntityMultiTreeAttr.IsDir, true);
            }
            return en;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string DoUp()
        {
            this.DoOrderUp(EntityMultiTreeAttr.ParentNo, this.ParentNo,
                this.RefObjField, this.GetValStringByKey(RefObjField), EntityMultiTreeAttr.Idx);
            return null;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string DoDown()
        {
            this.DoOrderDown(EntityMultiTreeAttr.ParentNo, this.ParentNo,
                this.RefObjField, this.GetValStringByKey(RefObjField), EntityMultiTreeAttr.Idx);
            return null;
        }
        #endregion
    }
	/// <summary>
    /// �����ʵ��s
	/// </summary>
    abstract public class EntitiesMultiTree : Entities
    {
        /// <summary>
        /// ��ѯ�����ӽڵ�
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int RetrieveHisChinren(EntityMultiTree en)
        {
            int i=this.Retrieve(EntityMultiTreeAttr.ParentNo, en.No);
            this.AddEntity(en);
            return i + 1;
        }
        /// <summary>
        /// ��ȡ�����ӽڵ�
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public EntitiesTree GenerHisChinren(EntityMultiTree en)
        {
            EntitiesTree ens = this.CreateInstance() as EntitiesTree;
            foreach (EntityMultiTree item in ens)
            {
                if (en.ParentNo == en.No)
                    ens.AddEntity(item);
            }
            return ens;
        }
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public new EntityMultiTree this[int index]
        {
            get
            {
                return (EntityMultiTree)this.InnerList[index];
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public EntitiesMultiTree()
        {
        }
    }
}
