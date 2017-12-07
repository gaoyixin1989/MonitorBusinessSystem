using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.Demo
{
    /// <summary>
    /// �о� ����
    /// </summary>
    public class tab_commonkpioptiAttr
    {
        #region ��������
        /// <summary>
        /// ����������
        /// </summary>
        public const string tab_commonkpiopti_main = "tab_commonkpiopti_main";
        /// <summary>
        /// ���̱��
        /// </summary>
        public const string fk_flow = "fk_flow";
        /// <summary>
        /// WorkID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ���ڵ�ID
        /// </summary>
        public const string ParentWorkID = "ParentWorkID";
        /// <summary>
        /// regionid
        /// </summary>
        public const string region_id = "region_id";
        /// <summary>
        /// ���ݱ��
        /// </summary>
        public const string wf_no = "wf_no";
        /// <summary>
        /// �������
        /// </summary>
        public const string wf_category = "wf_category";
        /// <summary>
        /// �����̶�
        /// </summary>
        public const string wf_priority = "wf_priority";
        /// <summary>
        /// ������
        /// </summary>
        public const string wf_send_user = "wf_send_user";
        /// <summary>
        /// �����˲���
        /// </summary>
        public const string wf_send_department = "wf_send_department";
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public const string wf_send_time = "wf_send_time";
        /// <summary>
        /// �����˵绰
        /// </summary>
        public const string wf_send_phone = "wf_send_phone";


        #endregion
    }
    /// <summary>
    /// �о�
    /// </summary>
    public class tab_commonkpiopti : EntityOID
    {
        #region ����
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public int tab_commonkpiopti_main
        {
            get
            {
                return this.GetValIntByKey(tab_commonkpioptiAttr.tab_commonkpiopti_main);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.tab_commonkpiopti_main, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string fk_flow
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.fk_flow);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.fk_flow, value);
            }
        }
        /// <summary>
        /// ���ݱ��
        /// </summary>
        public string region_id
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.region_id);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.region_id, value);
            }
        }
        /// <summary>
        /// ָ��
        /// </summary>
        public string wf_no
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_no);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_no, value);
            }
        }
        /// <summary>
        /// �оָ�����
        /// </summary>
        public string wf_category
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_category);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_category, value);
            }
        }
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(tab_commonkpioptiAttr.WorkID);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.WorkID, value);
            }
        }
        public Int64 ParentWorkID
        {
            get
            {
                return this.GetValInt64ByKey(tab_commonkpioptiAttr.ParentWorkID);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.ParentWorkID, value);
            }
        }
        /// <summary>
        /// �����̶�
        /// </summary>
        public int wf_priority
        {
            get
            {
                return this.GetValIntByKey(tab_commonkpioptiAttr.wf_priority);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_priority, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string wf_send_user
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_send_user);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_send_user, value);
            }
        }
        /// <summary>
        /// �����˲���
        /// </summary>
        public string wf_send_department
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_send_department);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_send_department, value);
            }
        }
        /// <summary>
        /// ������ʱ��
        /// </summary>
        public string wf_send_time
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_send_time);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_send_time, value);
            }
        }
        /// <summary>
        /// �����˵绰
        /// </summary>
        public string wf_send_phone
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpioptiAttr.wf_send_phone);
            }
            set
            {
                this.SetValByKey(tab_commonkpioptiAttr.wf_send_phone, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// �о�
        /// </summary>
        public tab_commonkpiopti()
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
                Map map = new Map("Demo_commonkpiopti");
                map.EnDesc = "�о�";

                map.AddTBIntPKOID();

                map.AddTBInt(tab_commonkpioptiAttr.WorkID, 0, "WorkID", true, false);
                map.AddTBInt(tab_commonkpioptiAttr.ParentWorkID, 0, "������ID", true, false);

                map.AddTBInt(tab_commonkpioptiAttr.wf_priority, 0, "�����̶�", true, false);

                map.AddTBString(tab_commonkpioptiAttr.tab_commonkpiopti_main, null, "����ʡ��ID", true, false, 0, 200, 10);

                map.AddTBString(tab_commonkpioptiAttr.region_id, null, "���ݱ��", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_category, null, "���", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_no, null, "���ݱ��", true, false, 0, 200, 10);

                map.AddTBString(tab_commonkpioptiAttr.wf_priority, null, "�����̶�", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_send_user, null, "������", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_send_department, null, "�����˲���", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_send_time, null, "����ʱ��", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpioptiAttr.wf_send_phone, null, "�����˵绰", true, false, 0, 200, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// �о�s
    /// </summary>
    public class tab_commonkpioptis : EntitiesOID
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new tab_commonkpiopti();
            }
        }
        /// <summary>
        /// �о�s
        /// </summary>
        public tab_commonkpioptis() { }
        #endregion
    }
}
