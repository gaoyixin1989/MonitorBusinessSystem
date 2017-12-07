using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb ;
using System.Data;
using System.Data.Common;

namespace CCPortal.DA
{
    public class ConnOfOLE : ConnBase
    {
        public new OleDbConnection Conn = null;
        public ConnOfOLE()
        {
        }
    }
    public class ConnOfOLEs : System.Collections.CollectionBase
    {
        public ConnOfOLEs()
        {
        }
        public void Add(ConnOfOLE conn)
        {
            this.InnerList.Add(conn);
        }
        /// <summary>
        ///  ��ʼ��
        /// </summary>
        public void Init()
        {
            for (int i = 0; i <= 1; i++)
            {
                ConnOfOLE conn = new ConnOfOLE();
                conn.IDX = i;
                this.Add(conn);
            }
        }
        public bool isLock = false;
        public ConnOfOLE GetOne()
        {
            //if (this.Count == 0)
            //    this.Init();
            while (isLock)
            {
                ;
            }

            isLock = true;
            foreach (ConnOfOLE conn in this)
            {
                if (conn.IsUsing == false)
                {
                    if (conn.Conn == null)
                        conn.Conn = new OleDbConnection(SystemConfig.AppCenterDSN);
                    conn.Times++;
                    conn.IsUsing = true;
                    isLock = false;
                    return conn;
                }
            }

            // ���û���µ����ӡ�
            ConnOfOLE nconn = new ConnOfOLE();
            nconn.IDX = this.Count;
            nconn.Conn = new OleDbConnection(SystemConfig.AppCenterDSN);
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
            conn.Close();
            conn.IsUsing = false;
            this.InnerList[conn.IDX] = conn;
        }
    }
}
