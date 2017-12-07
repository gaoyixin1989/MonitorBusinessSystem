using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
namespace BP.WF.DTS
{
    /// <summary>
    /// ����ccflow6 Ҫִ�еĵ���
    /// </summary>
    public class UpTrack : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public UpTrack()
        {
            this.Title = "����ccflow6Ҫִ�еĵ���(����������wf_track����,���ɷ���ִ��.)";
            this.Help = "ִ�д˹��̰�ccflow4 ������ccflow6 �˹��̽����wf_track�������.";
        }
        /// <summary>
        /// ����ִ�б���
        /// </summary>
        /// <returns></returns>
        public override void Init()
        {
        }
        /// <summary>
        /// ��ǰ�Ĳ���Ա�Ƿ����ִ���������
        /// </summary>
        public override bool IsCanDo
        {
            get
            {
                if (BP.Web.WebUser.No == "admin")
                    return true;
                else
                    return false;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            Flows fls = new Flows();
            fls.RetrieveAllFromDBSource();

            string info = "";
            foreach (Flow fl in fls)
            {
                // ��鱨��.
                Track.CreateOrRepairTrackTable(fl.No);

                // ��ѯ.
                string sql = "SELECT * FROM WF_Track WHERE FK_Flow='" + fl.No + "'";
                DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Track tk = new Track();
                    tk.FK_Flow = fl.No;
                    tk.Row.LoadDataTable(dt, dt.Rows[0]);
                    tk.DoInsert(0); // ִ��insert.
                }
            }
            return  "ִ����ɣ���������ִ�����ˣ�����ͻ�����ظ��Ĺ켣���ݡ�";
        }
    }
}
