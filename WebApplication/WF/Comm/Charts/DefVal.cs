using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// ����
    /// </summary>
    public class DefValAttr : EntityTreeAttr
    {
        /// <summary>
        /// ���ڵ���
        /// </summary>
        public const string ParentNo = "ParentNo";
        /// <summary>
        /// �Ƿ񸸽ڵ�
        /// </summary>
        public const string IsParent = "IsParent";
        /// <summary>
        /// ���
        /// </summary>
        public const string WordsSort = "WordsSort";
        /// <summary>
        /// �ڵ����
        /// </summary>
        public const string FK_MapData = "FK_MapData";
        /// <summary>
        /// �ڵ��Ӧ�ֶ�
        /// </summary>
        public const string NodeAttrKey = "NodeAttrKey";
        /// <summary>
        /// ��ʷ�ʻ�
        /// </summary>
        public const string IsHisWords = "IsHisWords";
        /// <summary>
        /// ��Ա���
        /// </summary>
        public const string FK_Emp = "FK_Emp";
        /// <summary>
        /// �ڵ��ı�
        /// </summary>
        public const string CurValue = "CurValue";
    }
    /// <summary>
    /// Ĭ��ֵ
    /// </summary>
    public class DefVal : EntityTree
    {
        #region ��������
        /// <summary>
        /// ���ڵ���
        /// </summary>
        public string ParentNo
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.ParentNo);
            }
            set
            {
                this.SetValByKey(DefValAttr.ParentNo, value);
            }
        }
        /// <summary>
        /// �Ƿ񸸽ڵ�
        /// </summary>
        public string IsParent
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.IsParent);
            }
            set
            {
                this.SetValByKey(DefValAttr.IsParent, value);
            }
        }
        /// <summary>
        /// �ʻ����
        /// </summary>
        public string WordsSort
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.WordsSort);
            }
            set
            {
                this.SetValByKey(DefValAttr.WordsSort, value);
            }
        }
        /// <summary>
        /// �ڵ���
        /// </summary>
        public string FK_MapData
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.FK_MapData);
            }
            set
            {
                this.SetValByKey(DefValAttr.FK_MapData, value);
            }
        }
        /// <summary>
        /// �ڵ��Ӧ�ֶ�
        /// </summary>
        public string NodeAttrKey
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.NodeAttrKey);
            }
            set
            {
                this.SetValByKey(DefValAttr.NodeAttrKey, value);
            }
        }
        /// <summary>
        /// �Ƿ���ʷ�ʻ�
        /// </summary>
        public string IsHisWords
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.IsHisWords);
            }
            set
            {
                this.SetValByKey(DefValAttr.IsHisWords, value);
            }
        }
        /// <summary>
        /// ��Ա���
        /// </summary>
        public string FK_Emp
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.FK_Emp);
            }
            set
            {
                this.SetValByKey(DefValAttr.FK_Emp, value);
            }
        }
        /// <summary>
        /// �ڵ��ı�
        /// </summary>
        public string CurValue
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.CurValue);
            }
            set
            {
                this.SetValByKey(DefValAttr.CurValue, value);
            }
        }
        #endregion

        #region ���췽��

        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public DefVal()
        {
        }
        /// <summary>
        /// map
        /// </summary>
        public override Map EnMap
        {
            get
            {
                if (this._enMap != null) return this._enMap;
                Map map = new Map("Sys_DefVal");
                map.EnType = EnType.Sys;
                map.EnDesc = "Ĭ��ֵ";
                map.DepositaryOfEntity = Depositary.Application;
                map.DepositaryOfMap = Depositary.Application;
                //��2015-1-10   ���ݹ�˾����Ķ�   ������Դ��
                //map.AddTBStringPK(DefValAttr.No, null, "���", true, true, 1, 50, 20);
                //map.AddTBString(DefValAttr.EnsName, null, "������", false, true, 0, 100, 10);
                //map.AddTBString(DefValAttr.EnsDesc, null, "������", false, true, 0, 100, 10);
                //map.AddTBString(DefValAttr.AttrKey, null, "����", false, true, 0, 100, 10);
                //map.AddTBString(DefValAttr.AttrDesc, null, "��������", false, false, 0, 100, 10);
                //map.AddTBString(DefValAttr.FK_Emp, null, "��Ա", false, true, 0, 100, 10);
                //map.AddTBString(DefValAttr.Val, null, "ֵ", true, false, 0, 1000, 10);
                //map.AddTBString(DefValAttr.ParentNo, null, "���ڵ���", false, false, 0, 50, 20);
                //map.AddTBInt(DefValAttr.IsParent, 0, "�Ƿ񸸽ڵ�", false, false);
                //map.AddTBString(DefValAttr.HistoryWords, null, "��ʷ�ʻ�", false, false, 0, 2000, 20);

                map.AddTBStringPK(DefValAttr.No, null, "���", true, true, 1, 50, 20);
                map.AddTBString(DefValAttr.ParentNo, null, "���ڵ���", false, false, 0, 50, 20);
                map.AddTBInt(DefValAttr.IsParent, 0, "�Ƿ񸸽ڵ�", false, false);//1,0  Y/N
                map.AddTBInt(DefValAttr.WordsSort, 0, "�ʻ����", false, false);//0,1,2...   ��-�˻�-�ƽ�...(��ʱ)
                map.AddTBString(DefValAttr.FK_MapData, null, "�ڵ���", false, false, 0, 50, 20);
                map.AddTBString(DefValAttr.NodeAttrKey, null, "�ڵ��Ӧ�ֶ�", false, false, 0, 50, 20);
                map.AddTBInt(DefValAttr.IsHisWords, 0, "�Ƿ���ʷ�ʻ�", false, false);//1,0 Y/N
                map.AddTBString(DefValAttr.FK_Emp, null, "��Ա", false, true, 0, 100, 10);
                map.AddTBString(DefValAttr.CurValue, null, "�ڵ��ı�", false, true, 0, 8000, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// Ĭ��ֵs
    /// </summary>
    public class DefVals : EntitiesNoName
    {
        /// <summary>
        /// Ĭ��ֵ
        /// </summary>
        public DefVals()
        {
        }
        /// <summary>
        /// �õ����� Entity
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new DefVal();
            }
        }
    }
}
