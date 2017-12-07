using System;
using System.Data;
using System.Collections;
using BP;
using BP.DA;
using BP.En;

namespace BP.DTS
{
    public class DataToCash : DataIOEn
    {
        public DataToCash()
        {
            this.HisDoType = DoType.Especial;
            this.Title = "���ȵ����ݵ� cash ��ȥ";
          //  this.HisUserType = Web.UserType.SysAdmin;

            this.DefaultEveryMin = "00";
            this.DefaultEveryHH = "00";
            this.DefaultEveryDay = "00";
            this.DefaultEveryMonth = "00";
            this.Note = "";
        }
        public override void Do()
        {
            Log.DebugWriteInfo("��ʼִ�� DataToCahs ");
            string sql = "";
            string str = "";

            #region ö�����ͷ���cash.
            sql = "  SELECT DISTINCT ENUMKEY FROM SYS_ENUM ";
            DataTable dt = DBAccess.RunSQLReturnTable(sql);
            foreach (DataRow dr in dt.Rows)
            {
                str = dr[0].ToString();
                BP.Sys.SysEnums ses = new BP.Sys.SysEnums(str);
            }
            #endregion
          
            #region ���ȵ���
            //if (SystemConfig.SysNo == SysNoList.WF)
            //{
            //    Log.DefaultLogWriteLineInfo("����ģ��");
            //    sql = "SELECT URL FROM WF_NODEREFFUNC  ";
            //    dt = DBAccess.RunSQLReturnTable(sql);
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        try
            //        {
            //            str = Cash.GetBillStr(dr[0].ToString(),false);
            //        }
            //        catch (Exception ex)
            //        {
            //            Log.DefaultLogWriteLineInfo("@���뵥��cash ���ִ���" + ex.Message);
            //        }
            //    }
            //}
            #endregion

            #region ��������ݷŽ�cash.
            // entity ���ݷŽ�cash.
            ArrayList al = ClassFactory.GetObjects("BP.En.Entities");
            foreach (Entities ens in al)
            {
                Depositary where;
                try
                {
                    where = ens.GetNewEntity.EnMap.DepositaryOfEntity;
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLine(LogType.Info, "@�ڰ����ݷ����ڴ�ʱ���ִ���:" + ex.Message + " cls=" + ens.ToString());
                    /* �����û���½��Ϣ��map ����ȡ���� */
                    continue;
                }

                if (where == Depositary.None)
                    continue;

                //try
                //{
                //    ens.FlodInCash();
                //}
                //catch (Exception ex)
                //{
                //    Log.DefaultLogWriteLine(LogType.Info, "@�����ݷŽ� cash �г��ִ���@" + ex.Message);
                //}
            }
            #endregion

            #region  ��xml ���ݷŽ�cash.
            al = ClassFactory.GetObjects("BP.XML.XmlEns");
            foreach (BP.XML.XmlEns ens in al)
            {
                try
                {
                    dt = ens.GetTable();
                    ens.RetrieveAll();
                }
                catch (Exception ex)
                {
                    Log.DefaultLogWriteLineError("@���� " + ens.ToString() + "���ִ���:" + ex.Message);
                }
            }
            #endregion

            Log.DefaultLogWriteLine(LogType.Info, "����ִ��DataToCahs ");
        }
    }
}
