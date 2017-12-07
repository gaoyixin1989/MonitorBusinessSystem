using System; 
using System.Collections;
using BP.DA; 
using BP.Web.Controls;
using System.Reflection;

namespace BP.En
{
    /// <summary>
    /// ��ع�������
    /// </summary>
    public enum RefMethodType
    {
        /// <summary>
        /// ����
        /// </summary>
        Func,
        /// <summary>
        /// ģ̬���ڴ�
        /// </summary>
        LinkModel,
        /// <summary>
        /// �´��ڴ�
        /// </summary>
        LinkeWinOpen,
        /// <summary>
        /// �Ҳര�ڴ�
        /// </summary>
        RightFrameOpen
    }
    /// <summary>
    /// RefMethod ��ժҪ˵����
    /// </summary>
    public class RefMethod
    {
        #region �봰���йصķ���.
        /// <summary>
        /// �߶�
        /// </summary>
        public int Height = 600;
        /// <summary>
        /// ���
        /// </summary>
        public int Width = 800;
        public string Target = "_B123";
        #endregion

        /// <summary>
        /// ����
        /// </summary>
        public RefMethodType RefMethodType = RefMethodType.Func;
        /// <summary>
        /// ����ֶ�
        /// </summary>
        public string RefAttrKey = null;
        /// <summary>
        /// ���ӱ�ǩ
        /// </summary>
        public string RefAttrLinkLabel = null;
        /// <summary>
        /// �Ƿ���ʾ��Ens��?
        /// </summary>
        public bool IsForEns = true;
        /// <summary>
        /// ��ع���
        /// </summary>
        public RefMethod()
        {
        }
        /// <summary>
        /// ����
        /// </summary>
        private Attrs _HisAttrs = null;
        /// <summary>
        /// ����
        /// </summary>
        public Attrs HisAttrs
        {
            get
            {
                if (_HisAttrs == null)
                    _HisAttrs = new Attrs();
                return _HisAttrs;
            }
            set
            {
                _HisAttrs = value;
            }
        }
        /// <summary>
        /// ����λ�ã���������ʵ��.
        /// </summary>
        public int Index = 0;
        /// <summary>
        /// �Ƿ���ʾ
        /// </summary>
        public bool Visable = true;
        /// <summary>
        /// �Ƿ����������
        /// </summary>
        public bool IsCanBatch = false;
        /// <summary>
        /// ����
        /// </summary>
        public string Title = null;
        /// <summary>
        /// ����ǰ��ʾ��Ϣ
        /// </summary>
        public string Warning = null;
        /// <summary>
        /// ����
        /// </summary>
        public string ClassMethodName = null;
        /// <summary>
        /// ͼ��
        /// </summary>
        public string Icon = null;
        public string GetIcon(string path)
        {
            if (this.Icon == null)
            {
                return null;
                return "<img src='/WF/Img/Btn/Do.gif'  border=0 />";
            }
            else
            {
                string url = path + Icon;
                url = url.Replace("//", "/");
                return "<img src='" + url + "'  border=0 />";
            }
        }
        /// <summary>
        /// ��ʾ��Ϣ
        /// </summary>
        public string ToolTip = null;
       
        /// <summary>
        /// PKVal
        /// </summary>
        public object PKVal = "PKVal";
        /// <summary>
        /// 
        /// </summary>
        public Entity HisEn = null;
        /// <summary>
        /// ʵ��PK
        /// </summary>
        public string[] PKs = "".Split('.');
        /// <summary>
        /// ִ��
        /// </summary>
        /// <param name="paras"></param>
        /// <returns></returns>
        public object Do(object[] paras)
        {
            string str = this.ClassMethodName.Trim(' ', ';', '.');
            int pos = str.LastIndexOf(".");
            string clas = str.Substring(0, pos);
            string meth = str.Substring(pos, str.Length - pos).Trim('.', ' ', '(', ')');
            if (this.HisEn == null)
            {
                this.HisEn = BP.En.ClassFactory.GetEn(clas);
                Attrs attrs = this.HisEn.EnMap.Attrs;

                //if (SystemConfig.IsBSsystem)
                //{
                //    //string val = BP.Sys.Glo.Request.QueryString["No"];
                //    //if (val == null)
                //    //{
                //    //    val = BP.Sys.Glo.Request.QueryString["PK"];
                //    //}
                //    this.HisEn.PKVal = BP.Sys.Glo.Request.QueryString[this.HisEn.PK];
                //}
                //else
                //    this.HisEn.PKVal = this.PKVal;
                //this.HisEn.Retrieve();
            }

            Type tp = this.HisEn.GetType();
            MethodInfo mp = tp.GetMethod(meth);
            if (mp == null)
                throw new Exception("@����ʵ��[" + tp.FullName + "]��û���ҵ�����[" + meth + "]��");

            try
            {
                return mp.Invoke(this.HisEn, paras); //�����ɴ� MethodInfo ʵ������ķ������캯����
            }
            catch (System.Reflection.TargetException ex)
            {
                string strs = "";
                if (paras == null)
                {
                    throw new Exception(ex.Message);
                }
                else
                {
                    foreach (object obj in paras)
                    {
                        strs += "para= " + obj.ToString() + " type=" + obj.GetType().ToString() + "\n<br>";
                    }
                }
                throw new Exception(ex.Message + "  more info:" + strs);
            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class RefMethods : CollectionBase
    {
        /// <summary>
        /// ����
        /// </summary>
        /// <param name="attr">attr</param>
        public void Add(RefMethod en)
        {
            if (this.IsExits(en))
                return;
            en.Index = this.InnerList.Count;
            this.InnerList.Add(en);
        }
        /// <summary>
        /// �ǲ��Ǵ��ڼ�������
        /// </summary>
        /// <param name="en">Ҫ����RefMethod</param>
        /// <returns>true/false</returns>
        public bool IsExits(RefMethod en)
        {
            foreach (RefMethod dtl in this)
            {
                if (dtl.ClassMethodName == en.ClassMethodName)
                    return true;
            }
            return false;
        }
        /// <summary>
        /// �ܹ�����������
        /// </summary>
        public int CountOfVisable
        {
            get
            {
                int i = 0;
                foreach (RefMethod rm in this)
                {
                    if (rm.Visable)
                        i++;
                }
                return i;
            }
        }
        /// <summary>
        /// �����������ʼ����ڵ�Ԫ��Attr��
        /// </summary>
        public RefMethod this[int index]
        {
            get
            {
                return (RefMethod)this.InnerList[index];
            }
        }
    }
}
