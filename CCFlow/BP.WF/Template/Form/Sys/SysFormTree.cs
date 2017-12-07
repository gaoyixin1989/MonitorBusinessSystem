using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.En;
using BP.Port;

namespace BP.Sys
{
    /// <summary>
    /// ����
    /// </summary>
    public class SysFormTreeAttr : EntityTreeAttr
    {
    }
    /// <summary>
    ///  ���̱���
    /// </summary>
    public class SysFormTree : EntityTree
    {
        #region ���췽��
        /// <summary>
        /// ���̱���
        /// </summary>
        public SysFormTree()
        {
        }
        /// <summary>
        /// ���̱���
        /// </summary>
        /// <param name="_No"></param>
        public SysFormTree(string _No) : base(_No) { }
        #endregion

        #region ϵͳ����.
        /// <summary>
        /// ���̱���Map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null)
                    return this._enMap;

                Map map = new Map("Sys_FormTree");
                map.EnDesc = "����";
                map.CodeStruct = "2";

                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;

                map.AddTBStringPK(SysFormTreeAttr.No, null, "���", true, true, 1, 10, 20);
                map.AddTBString(SysFormTreeAttr.Name, null, "����", true, false, 0, 100, 30);
                map.AddTBString(SysFormTreeAttr.ParentNo, null, "���ڵ�No", false, false, 0, 100, 30);
                map.AddTBString(SysFormTreeAttr.TreeNo, null, "TreeNo", false, false, 0, 100, 30);

                map.AddTBInt(SysFormTreeAttr.IsDir, 0, "�Ƿ���Ŀ¼?", false, false);
                map.AddTBInt(SysFormTreeAttr.Idx, 0, "Idx", false, false);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion ϵͳ����.

        protected override bool beforeDelete()
        {
            if (!string.IsNullOrEmpty(this.No))
                DeleteChild(this.No);
            return base.beforeDelete();
        }
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="parentNo"></param>
        private void DeleteChild(string parentNo)
        {
            SysFormTrees formTrees = new SysFormTrees();
            formTrees.RetrieveByAttr(SysFormTreeAttr.ParentNo, parentNo);
            foreach (SysFormTree item in formTrees)
            {
                MapData md = new MapData();
                md.FK_FormTree = item.No;
                md.Delete();
                DeleteChild(item.No);
            }
        }
    }
    /// <summary>
    /// ���̱���
    /// </summary>
    public class SysFormTrees : EntitiesTree
    {
        /// <summary>
        /// ���̱���s
        /// </summary>
        public SysFormTrees() { }
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new SysFormTree();
            }

        }
        public override int RetrieveAll()
        {
            int i = base.RetrieveAll();
            if (i == 0)
            {
                SysFormTree fs = new SysFormTree();
                fs.Name = "������";
                fs.No = "01";
                fs.Insert();

                fs = new SysFormTree();
                fs.Name = "�칫��";
                fs.No = "02";
                fs.Insert();
                i = base.RetrieveAll();
            }
            return i;
        }
    }
}
