using System;
using System.Collections;
using System.Data;
using BP.DA;
using BP.En;
using BP.Web.Controls;
using BP.Web;
using BP.Sys;

namespace BP.Rpt
{
    public enum DBAChartType
    {
        Table,
        Column,
        Pie,
        Line
    }
    /// <summary>
    /// ��״ͼ��ʾ����
    /// </summary>
    public enum ColumnChartShowType
    {
        /// <summary>
        /// ����ʾ
        /// </summary>
        None,
        /// <summary>
        /// ����
        /// </summary>
        HengXiang,
        /// <summary>
        /// ����
        /// </summary>
        ShuXiang
    }
    /// <summary>
    /// ����ͼͼ��ʾ����
    /// </summary>
    public enum LineChartShowType
    {
        /// <summary>
        /// ����ʾ
        /// </summary>
        None,
        /// <summary>
        /// ����
        /// </summary>
        HengXiang,
        /// <summary>
        /// ����
        /// </summary>
        ShuXiang
    }
    /// <summary>
    /// ���ݱ���
    /// </summary>
    public class Rpt2Attr
    {
        /// <summary>
        /// ���ݱ���
        /// </summary>
        public Rpt2Attr()
        {
        }
        private string _Title="";
        /// <summary>
        /// ����
        /// </summary>
        public string Title
        {
            get
            {
                if (string.IsNullOrEmpty(_Title))
                    return "";

                if (_Title.Contains("@")==false)
                    return _Title;

                string title = _Title.Clone() as string ;

                title = title.Replace("@WebUser.No", BP.Web.WebUser.No);
                title = title.Replace("@WebUser.Name", BP.Web.WebUser.Name);
                title = title.Replace("@WebUser.FK_Dept", BP.Web.WebUser.FK_Dept);
                title = title.Replace("@WebUser.FK_DeptName", BP.Web.WebUser.FK_DeptName);
                if (title.Contains("@") == false)
                    return title;

                foreach (string key in Glo.Request.QueryString)
                {
                    title = title.Replace("@" + key, Glo.Request.QueryString[key]);
                }

                if (title.Contains("@") == false)
                    return title;

                if (title.Contains("@") == false)
                    return title;

                BP.DA.AtPara ap = new AtPara(this.DefaultParas);
                foreach (string key in ap.HisHT.Keys)
                    title = title.Replace("@" + key, ap.GetValStrByKey(key) );

                return title;
            }
            set
            {
                _Title = value;
            }
        }
        /// <summary>
        /// ��������Դ
        /// </summary>
        public string DBSrc = "";
        /// <summary>
        /// ��ϸ��Ϣ(����Ϊ��)
        /// </summary>
        public string DBSrcOfDtl = "";
        /// <summary>
        /// ��ߵĲ˵�.
        /// </summary>
        public string LeftMenu = "";
      
        /// <summary>
        /// �߶�.
        /// </summary>
        public int H = 400;
        /// <summary>
        /// Ĭ�Ͽ���
        /// </summary>
        public int W = 900;
        /// <summary>
        /// �ײ�����.
        /// </summary>
        public string xAxisName = "";
        /// <summary>
        /// �ұ�����
        /// </summary>
        public string yAxisName = "";
        /// <summary>
        /// ��ֵ�е�ǰ׺
        /// </summary>
        public string numberPrefix = "";
        /// <summary>
        /// ͼ��ĺ���Ĳ����ߵ�����.
        /// </summary>
        public int numDivLines = 8;

        /// <summary>
        /// Ĭ����ʾ������ͼ��.
        /// </summary>
        public DBAChartType DefaultShowChartType = DBAChartType.Column;
        /// <summary>
        /// �Ƿ�����table.
        /// </summary>
        public bool IsEnableTable = true;

        /// <summary>
        /// ��ͼ��ʾ����.
        /// </summary>
        private ColumnChartShowType _ColumnChartShowType = ColumnChartShowType.ShuXiang;
        public ColumnChartShowType ColumnChartShowType
        {
            get
            {
                return _ColumnChartShowType;
            }
            set
            {
                _ColumnChartShowType = value;
            }
        }
        /// <summary>
        /// �Ƿ���ʾ��ͼ
        /// </summary>
        public bool IsEnablePie = false;
        /// <summary>
        /// ����ͼ��ʾ����.
        /// </summary>
        public LineChartShowType LineChartShowType = LineChartShowType.HengXiang;
        /// <summary>
        /// Ĭ�ϲ���.
        /// </summary>
        public string DefaultParas = "";
        /// <summary>
        /// y���ֵ.
        /// </summary>
        public double MaxValue = 0;
        /// <summary>
        /// y��Сֵ.
        /// </summary>
        public double MinValue = 0;
        /// <summary>
        /// ��ײ�����Ϣ�Ʊ�����Ϣ.
        /// </summary>
        public string ChartInfo = null;
        /// <summary>
        /// �ֶι�ϵ����ʽ
        /// </summary>
        public string ColExp = "";
        /// <summary>
        /// ˵��
        /// </summary>
        public string DESC = "";
    }
    public class Rpt2Attrs : System.Collections.CollectionBase
    {
        public void Add(Rpt2Attr en)
        {
            this.InnerList.Add(en);
        }
        public Rpt2Attr GetD2(int idx)
        {
            return (Rpt2Attr)this.InnerList[idx];
        }
    }
    /// <summary>
    /// ��������
    /// </summary>
    abstract public class Rpt2Base
    {
        #region ���췽��
        /// <summary>
        /// ��������
        /// </summary>
        public Rpt2Base()
        {
        }
        #endregion ���췽��

        #region Ҫ������ǿ����д������.
        /// <summary>
        /// ��ʾ�ı���.
        /// </summary>
        abstract public string Title
        {
            get;
        }
        /// <summary>
        /// Ĭ��ѡ�������.
        /// </summary>
        abstract public int AttrDefSelected
        {
            get;
        }
        /// <summary>
        /// ������ʾ����, ���������@���Ÿ���.
        /// </summary>
        abstract public Rpt2Attrs AttrsOfGroup
        {
            get;
        }
        #endregion Ҫ��������д������.

        #region �ṩ�������ߵķ���.
        #endregion �ṩ�������ߵķ���
    }
}