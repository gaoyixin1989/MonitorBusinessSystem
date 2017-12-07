using System;
using System.Data;
using System.Collections;
using BP.DA;
using BP.Web.Controls;
using System.Reflection;
using BP.Port;
using BP.En;
using BP.Sys;
namespace BP.WF.DTS
{
    /// <summary>
    /// ���ɿ�������
    /// </summary>
    public class GenerCH : Method
    {
        /// <summary>
        /// ���ɿ�������
        /// </summary>
        public GenerCH()
        {
            this.Title = "���ɿ������ݣ�Ϊ���е�����,�����������õĽڵ㿼����Ϣ�����ɿ������ݡ���";
            this.Help = "��Ҫ��ɾ������ʱ���ɵ����ݣ�Ȼ��Ϊÿ�����̵�ÿ���ڵ����������Զ����ɡ�";
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
                return false;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            string err = "";
            try
            {
                //ɾ�����е����ݡ�
                BP.DA.DBAccess.RunSQL("DELETE FROM WF_CH");

                //��ѯȫ��������.
                BP.WF.Nodes nds = new Nodes();
                nds.RetrieveAll();

                foreach (Node nd in nds)
                {
                    string sql = "SELECT * FROM ND" + int.Parse(nd.FK_Flow) + "TRACK WHERE NDFrom=" + nd.NodeID + " ORDER BY WorkID, RDT ";
                    System.Data.DataTable dt = BP.DA.DBAccess.RunSQLReturnTable(sql);
                    string priRDT = null;
                    string sdt = null;
                    foreach (DataRow dr in dt.Rows)
                    {
                        //���·���.
                        int atInt = (int)dr[BP.WF.TrackAttr.ActionType];
                        ActionType at = (ActionType)atInt;
                        switch (at)
                        {
                            case ActionType.Forward:
                            case ActionType.ForwardAskfor:
                            case ActionType.ForwardFL:
                            case ActionType.ForwardHL:
                                break;
                            default:
                                continue;
                        }

                        //��صı���.
                        Int64 workid = Int64.Parse(dr[TrackAttr.WorkID].ToString());
                        Int64 fid = Int64.Parse(dr[TrackAttr.FID].ToString());

                        //��ǰ����Ա��������Ǿ������¼.
                        string fk_emp = dr[BP.WF.TrackAttr.EmpFrom] as string;
                        if (BP.Web.WebUser.No != fk_emp)
                        {
                            try
                            {
                                BP.WF.Dev2Interface.Port_Login(fk_emp);
                            }
                            catch (Exception ex)
                            {
                                err += "@��Ա����:" + fk_emp + "���ܸ���Ա�Ѿ�ɾ��." + ex.Message;
                            }
                        }

                        //����.
                        string title = BP.DA.DBAccess.RunSQLReturnStringIsNull("select title from wf_generworkflow where workid=" + workid, "");
                        //������.
                        Glo.InitCH(nd.HisFlow, nd, workid, fid, title, priRDT, sdt,
                            DataType.ParseSysDate2DateTime(dr[TrackAttr.RDT].ToString()));

                        priRDT = dr[TrackAttr.RDT].ToString();
                        sdt = "��";
                    }
                }
            }
            catch (Exception ex)
            {
                return "���ɿ���ʧ��:" + ex.StackTrace;
            }

            //��¼.
            BP.WF.Dev2Interface.Port_Login("admin");
            return "ִ�гɹ�,��������Ϣ:" + err;
        }
    }
}
