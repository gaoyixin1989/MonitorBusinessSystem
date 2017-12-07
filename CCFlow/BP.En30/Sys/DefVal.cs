using System;
using System.Collections;
using BP.DA;
using BP.En;

namespace BP.Sys
{
    /// <summary>
    /// ����
    /// </summary>
    public class DefValAttr : EntityOIDAttr
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
        public const string AttrKey = "AttrKey";
        /// <summary>
        /// ��ʷ�ʻ�
        /// </summary>
        public const string LB = "LB";
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
    public class DefVal : EntityOID
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
        public string AttrKey
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.AttrKey);
            }
            set
            {
                this.SetValByKey(DefValAttr.AttrKey, value);
            }
        }
        /// <summary>
        /// �Ƿ���ʷ�ʻ�
        /// </summary>
        public string LB
        {
            get
            {
                return this.GetValStringByKey(DefValAttr.LB);
            }
            set
            {
                this.SetValByKey(DefValAttr.LB, value);
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
                map.EnDesc = "ѡ��ʻ�";
                map.CodeStruct = "2";
                map.AddTBIntPKOID();

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

                map.AddTBString(DefValAttr.FK_MapData, null, "ʵ��", false, false, 0, 50, 20);
                map.AddTBString(DefValAttr.AttrKey, null, "�ڵ��Ӧ�ֶ�", false, false, 0, 50, 20);
                //map.AddTBInt(DefValAttr.WordsSort, 0, "�ʻ�����", false, false);//1,2,3... �˻�-�ƽ�-��...(��ʱ)
                map.AddTBInt(DefValAttr.LB, 0, "���", false, false);//�ҵģ���ʷ,ϵͳ��
                map.AddTBString(DefValAttr.FK_Emp, null, "��Ա", false, true, 0, 100, 10);
                map.AddTBString(DefValAttr.CurValue, null, "�ı�", false, true, 0, 4000, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// Ĭ��ֵs
    /// </summary>
    public class DefVals : EntitiesOID
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
