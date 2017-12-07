using System;
using System.Collections;
using BP.DA;

namespace BP.En
{
	/// <summary>
	/// �����б�
	/// </summary>
    public class EntityTreeAttr
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
        /// <summary>
        /// ���Ʋ���
        /// </summary>
        public const string CtrlWayPara = "CtrlWayPara";
        /// <summary>
        /// ͼ��
        /// </summary>
        public const string ICON = "ICON";
    }
	/// <summary>
	/// ��ʵ��
	/// </summary>
    abstract public class EntityTree : Entity
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
                return this.GetValStringByKey(EntityTreeAttr.No);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.No, value);
            }
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        public string TreeNo
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.TreeNo);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.TreeNo, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string Name
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.Name);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.Name, value);
            }
        }
        /// <summary>
        /// ���ڵ���
        /// </summary>
        public string ParentNo
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.ParentNo);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.ParentNo, value);
            }
        }
        /// <summary>
        /// ͼ��
        /// </summary>
        public string ICON
        {
            get
            {
                return this.GetValStringByKey(EntityTreeAttr.ICON);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.ICON, value);
            }
        }
        /// <summary>
        /// �Ƿ���Ŀ¼
        /// </summary>
        public bool IsDir
        {
            get
            {
                return this.GetValBooleanByKey(EntityTreeAttr.IsDir);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.IsDir, value);
            }
        }
        /// <summary>
        /// ˳���
        /// </summary>
        public int Idx
        {
            get
            {
                return this.GetValIntByKey(EntityTreeAttr.Idx);
            }
            set
            {
                this.SetValByKey(EntityTreeAttr.Idx, value);
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

        #region ���캯��
        /// <summary>
        /// ����
        /// </summary>
        public override string PK
        {
            get
            {
                return EntityTreeAttr.No;
            }
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        public EntityTree()
        {
        }
        /// <summary>
        /// ���ṹ���
        /// </summary>
        /// <param name="no">���</param>
        public EntityTree(string no)
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

            if (string.IsNullOrEmpty(this.No))
                this.No = this.GenerNewNoByKey("No");
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
        public EntityTree DoCreateSameLevelNode()
        {
            EntityTree en = this.CreateInstance() as EntityTree;
            en.No = BP.DA.DBAccess.GenerOID(this.ToString()).ToString(); // en.GenerNewNoByKey(EntityTreeAttr.No);
            en.Name = "�½��ڵ�" + en.No;
            en.ParentNo = this.ParentNo;
            en.IsDir = false;
            en.TreeNo = this.GenerNewNoByKey(EntityTreeAttr.TreeNo, EntityTreeAttr.ParentNo, this.ParentNo);
            en.Insert();
            return en;
        }
        /// <summary>
        /// �½��ӽڵ�
        /// </summary>
        /// <returns></returns>
        public EntityTree DoCreateSubNode()
        {
            EntityTree en = this.CreateInstance() as EntityTree;
            en.No = BP.DA.DBAccess.GenerOID(this.ToString()).ToString(); // en.GenerNewNoByKey(EntityTreeAttr.No);
            en.Name = "�½��ڵ�" + en.No;
            en.ParentNo = this.No;
            en.IsDir = false;
            en.TreeNo = this.GenerNewNoByKey(EntityTreeAttr.TreeNo, EntityTreeAttr.ParentNo, this.No);
            if (en.TreeNo.Substring(en.TreeNo.Length - 2) == "01")
                en.TreeNo = this.TreeNo + "10";
            en.Insert();

            // ���ô˽ڵ���Ŀ¼
            if (this.IsDir == false)
            {
                this.Retrieve();
                this.IsDir = true;
                this.Update();
            }
            return en;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string DoUp()
        {
            this.DoOrderUp(EntityTreeAttr.ParentNo, this.ParentNo, EntityTreeAttr.Idx);
            return null;
        }
        /// <summary>
        /// ����
        /// </summary>
        /// <returns></returns>
        public string DoDown()
        {
            this.DoOrderDown(EntityTreeAttr.ParentNo, this.ParentNo, EntityTreeAttr.Idx);
            return null;
        }
        #endregion
    }
	/// <summary>
    /// ��ʵ��s
	/// </summary>
    abstract public class EntitiesTree : Entities
    {
        /// <summary>
        /// ��ѯ�����ӽڵ�
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public int RetrieveHisChinren(EntityTree en)
        {
            int i=this.Retrieve(EntityTreeAttr.ParentNo, en.No);
            this.AddEntity(en);
            return i + 1;
        }

        /// <summary>
        /// ��ȡ�����ӽڵ�
        /// </summary>
        /// <param name="en"></param>
        /// <returns></returns>
        public EntitiesTree GenerHisChinren(EntityTree en)
        {
            EntitiesTree ens = this.CreateInstance() as EntitiesTree;
            foreach (EntityTree item in ens)
            {
                if (en.ParentNo == en.No)
                    ens.AddEntity(item);
            }
            return ens;
        }
        /// <summary>
        /// ����λ��ȡ������
        /// </summary>
        public new EntityTree this[int index]
        {
            get
            {
                return (EntityTree)this.InnerList[index];
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public EntitiesTree()
        {
        }
        /// <summary>
        /// ��ѯȫ��
        /// </summary>
        /// <returns></returns>
        public override int RetrieveAll()
        {
            return base.RetrieveAll("TreeNo");
        }
    }
}
