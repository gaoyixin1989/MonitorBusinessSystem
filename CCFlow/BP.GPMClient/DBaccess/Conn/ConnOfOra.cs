using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OracleClient;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;

namespace CCPortal.DA
{
    public class ConnOfOra : ConnBase
    {
        public new OracleConnection Conn = null;
        public ConnOfOra()
        {
        }
    }
    public class ConnOfOras : System.Collections.CollectionBase
    {
        public ConnOfOras()
        {
        }
        public void Add(ConnOfOra conn)
        {
            this.InnerList.Add(conn);
        }
        /// <summary>
        ///  ��ʼ��
        /// </summary>
        public void Init()
        {
            string str = SystemConfig.AppSettings["InitConnNum"];
            if (str == null)
                str = "3";
         

            int num = int.Parse(str);
            for (int i = 0; i <= num; i++)
            {
                ConnOfOra conn = new ConnOfOra();
                conn.Conn = new OracleConnection(SystemConfig.AppCenterDSN);
                conn.IDX = i;
                conn.IsUsing = false;
                this.Add(conn);
            }
        }
        public ConnOfOra GetByID(int id)
        {
            foreach(ConnOfOra conn in this)
                if (conn.IDX==id)
                    return conn;

            return null;
        }
        public bool isLock = false;
        public static bool isLocalConns = false;
     //   public static int  sleep = 1;
        /// <summary>
        /// �ڶ��汾��V2
        /// </summary>
        /// <returns></returns>
        public ConnOfOra GetOneV2()
        {
            while (isLocalConns)
            {
                ;
            }

            if (this.Count == 0)
            {
                this.Init();
                //throw new Exception("Error û�г��������ӡ�");
            }

            foreach (ConnOfOra conn in this)
            {
                if (conn.IsUsing == false)
                {
                    conn.IsUsing = true;
                    if (conn.Conn == null)
                        conn.Conn = new OracleConnection(SystemConfig.AppCenterDSN);
                    conn.Times++;
                    return conn;
                }
            }

            isLocalConns = true; // ��ס����
            string str = SystemConfig.AppSettings["InitConnMaxNum"];
            if (str == null || str == "")
                str = "3";

            int maxNum = int.Parse(str);
            if (this.Count > maxNum)
            {
                isLock = false;
             //   CCPortal.DA.Log.DebugWriteWarning("������������������" + maxNum + "���İ����������γ����������Ӧ�ã����Ƕ�����������������Լ���ϵͳ�ȴ���");
                return this.GetOneV2();
            }
            else
            {
               // CCPortal.DA.Log.DebugWriteWarning("��������[" + this.Count + "]���������ӡ�");
            }


            // ���û���µ����ӡ�
            ConnOfOra nconn = new ConnOfOra();
            nconn.IsUsing = true;
            nconn.IDX = this.Count;
            nconn.Conn = new OracleConnection(SystemConfig.AppCenterDSN);
            this.InnerList.Add(nconn);
            isLocalConns = false; //�ͷ�����
            return nconn;
        }
        public ConnOfOra GetOneV3_del()
        {
            ////if (this.Count == 0)
            ////    this.Init();
            int sltimes = 0;
            while (isLock || sltimes >= 500)
            {
                ;

                System.Threading.Thread.Sleep(1);

                sltimes++;
            }

            isLock = true;
            foreach (ConnOfOra conn in this)
            {
                if (conn.IsUsing == false)
                {
                    if (conn.Conn == null)
                        conn.Conn = new OracleConnection(SystemConfig.AppCenterDSN);

                    conn.Times++;
                    conn.IsUsing = true;

                    //conn.Close();

                    isLock = false;
                    return conn;
                }
            }

            // ���û���µ����ӡ�
            ConnOfOra nconn = new ConnOfOra();
            nconn.IDX = this.Count;
            nconn.Conn = new OracleConnection(SystemConfig.AppCenterDSN);
            nconn.IsUsing = true;
            this.InnerList.Add(nconn);
            isLock = false;
            return nconn;
            //throw new Exception("û�п��õ������ˣ��뱨�������Ա��");
        }
        /// <summary>
        /// PutPool
        /// </summary>
        /// <param name="conn"></param>
        public void PutPool(ConnBase conn)
        {
            conn.IsUsing = false;
            try
            {
                if (conn.Conn.State == ConnectionState.Open)
                    conn.Close();
            }
            catch
            { 
            }

            this.InnerList[conn.IDX] = conn;
        }
    }
}
