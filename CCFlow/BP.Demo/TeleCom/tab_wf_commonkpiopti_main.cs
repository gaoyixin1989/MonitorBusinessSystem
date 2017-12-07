using System;
using System.Data;
using BP.DA;
using BP.En;
using BP.WF;
using BP.Port;

namespace BP.Demo
{
    /// <summary>
    /// ʡ�� ����
    /// </summary>
    public class tab_commonkpiopti_mainAttr
    {
        #region ��������
        /// <summary>
        /// ���̱��
        /// </summary>
        public const string fk_flow = "fk_flow";
        /// <summary>
        /// ����ID
        /// </summary>
        public const string WorkID = "WorkID";
        /// <summary>
        /// ����
        /// </summary>
        public const string wf_title = "wf_title";
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
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public const string techology = "techology";
        /// <summary>
        /// KIPID
        /// </summary>
        public const string kpi_id = "kpi_id";
        /// <summary>
        /// 
        /// </summary>
        public const string threshold = "threshold";
        #endregion
    }
    /// <summary>
    /// ʡ��
    /// </summary>
    public class tab_commonkpiopti_main : EntityOID
    {
        #region ����
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public Int64 WorkID
        {
            get
            {
                return this.GetValInt64ByKey(tab_commonkpiopti_mainAttr.WorkID);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.WorkID, value);
            }
        }
        /// <summary>
        /// ���̱��
        /// </summary>
        public string fk_flow
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.fk_flow);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.fk_flow, value);
            }
        }
        /// <summary>
        /// ������
        /// </summary>
        public string wf_send_user
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_send_user);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_send_user, value);
            }
        }
        /// <summary>
        /// �������
        /// </summary>
        public string wf_category
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_category);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_category, value);
            }
        }
        /// <summary>
        /// �����̶�
        /// </summary>
        public int wf_priority
        {
            get
            {
                return this.GetValIntByKey(tab_commonkpiopti_mainAttr.wf_priority);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_priority, value);
            }
        }
        /// <summary>
        /// ����
        /// </summary>
        public string wf_title
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_title);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_title, value);
            }
        }
        /// <summary>
        /// ���ݱ��
        /// </summary>
        public string wf_no
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_no);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_no, value);
            }
        }
        /// <summary>
        /// �����˲���
        /// </summary>
        public string wf_send_department
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_send_department);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_send_department, value);
            }
        }
        /// <summary>
        /// ����ʱ��
        /// </summary>
        public string wf_send_time
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_send_time);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_send_time, value);
            }
        }
        /// <summary>
        /// �����˵绰
        /// </summary>
        public string wf_send_phone
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.wf_send_phone);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.wf_send_phone, value);
            }
        }
        /// <summary>
        /// ������Ϣ
        /// </summary>
        public string techology
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.techology);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.techology, value);
            }
        }
        /// <summary>
        /// kpi_id
        /// </summary>
        public string kpi_id
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.kpi_id);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.kpi_id, value);
            }
        }
        /// <summary>
        /// threshold
        /// </summary>
        public string threshold
        {
            get
            {
                return this.GetValStringByKey(tab_commonkpiopti_mainAttr.threshold);
            }
            set
            {
                this.SetValByKey(tab_commonkpiopti_mainAttr.threshold, value);
            }
        }
        #endregion

        #region ���캯��
        /// <summary>
        /// ʡ��
        /// </summary>
        public tab_commonkpiopti_main()
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
                Map map = new Map("Demo_commonkpiopti_main");
                map.EnDesc = "ʡ��";

                map.AddTBIntPKOID();

                map.AddTBInt(tab_commonkpiopti_mainAttr.WorkID, 0, "����ID", true, false);

                map.AddTBString(tab_commonkpiopti_mainAttr.fk_flow, null, "���̱��", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_title, null, "����", true, false, 0, 200, 10);

                map.AddTBString(tab_commonkpiopti_mainAttr.wf_no, null, "���ݱ��", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_category, null, "�������", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_priority, null, "�����̶�", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_send_user, null, "������", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_send_department, null, "�����˲���", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_send_time, null, "����ʱ��", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.wf_send_phone, null, "�����˵绰", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.techology, null, "������Ϣ", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.kpi_id, null, "kpi_id", true, false, 0, 200, 10);
                map.AddTBString(tab_commonkpiopti_mainAttr.threshold, null, "threshold", true, false, 0, 200, 10);

                this._enMap = map;
                return this._enMap;
            }
        }
        #endregion
    }
    /// <summary>
    /// ʡ��s
    /// </summary>
    public class tab_commonkpiopti_mains : EntitiesOID
    {
        #region ����
        /// <summary>
        /// �õ����� Entity 
        /// </summary>
        public override Entity GetNewEntity
        {
            get
            {
                return new tab_commonkpiopti_main();
            }
        }
        /// <summary>
        /// ʡ��s
        /// </summary>
        public tab_commonkpiopti_mains() { }
        #endregion
    }
}
