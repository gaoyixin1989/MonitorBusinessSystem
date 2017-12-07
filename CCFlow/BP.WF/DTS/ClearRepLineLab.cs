using System;
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
    /// �������ɱ��� ��ժҪ˵��
    /// </summary>
    public class ClearRepLineLab : Method
    {
        /// <summary>
        /// �����в����ķ���
        /// </summary>
        public ClearRepLineLab()
        {
            this.Title = "����ظ��ı��е�Line Lab ����";
            this.Help = "���ڱ�ģ����ǰ��Bug���µı�ǩ�����ظ����ݡ�";
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
                return true;
            }
        }
        /// <summary>
        /// ִ��
        /// </summary>
        /// <returns>����ִ�н��</returns>
        public override object Do()
        {
            FrmLines ens = new FrmLines();
            ens.RetrieveAllFromDBSource();
            string sql = "";
            foreach (FrmLine item in ens)
            {
                sql = "DELETE FROM " + item.EnMap.PhysicsTable + " WHERE FK_MapData='" + item.FK_MapData + "' AND X=" + item.X + " AND Y=" + item.Y + " and x1=" + item.X1 + " and x2=" + item.X2 + " and y1=" + item.Y1 + " and y2=" + item.Y2;
                DBAccess.RunSQL(sql);
                item.MyPK = BP.DA.DBAccess.GenerOIDByGUID().ToString();
                item.Insert();
            }


            FrmLabs labs = new FrmLabs();
            labs.RetrieveAllFromDBSource();
            foreach (FrmLab item in labs)
            {
                sql = "DELETE FROM " + item.EnMap.PhysicsTable + " WHERE FK_MapData='" + item.FK_MapData + "' and x=" + item.X + " and y=" + item.Y + " and Text='" + item.Text + "'";
                DBAccess.RunSQL(sql);
                item.MyPK = BP.DA.DBAccess.GenerOIDByGUID().ToString();
                item.Insert();
            }
            return "ɾ���ɹ�";
        }
    }
}
