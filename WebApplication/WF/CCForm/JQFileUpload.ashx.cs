using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BP.Sys;
using System.IO;
using BP.Web;
using System.Data;
using System.Drawing;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using BP.DA;
using BP.WF.Template;
using BP.WF;
using CCFlow.WF.CCForm;


namespace CCFlow.WF.CCForm
{
    /// <summary>
    /// JQFileUpload 的摘要说明
    /// </summary>
    public class JQFileUpload : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {


            string doType = context.Request["DoType"];
            string attachPk = context.Request["AttachPK"];
            string workid = context.Request["WorkID"];
            string fk_node = context.Request["FK_Node"];
            string ensName = context.Request["EnsName"];
            string fk_flow = context.Request["FK_Flow"];
            string pkVal = context.Request["PKVal"];
            string message = "true";
            if (context.Request.Files.Count > 0)
            {
                switch (doType)
                {
                    case "SingelAttach"://单附件上传
                        SingleAttach(context, attachPk, workid, fk_node, ensName);
                        break;
                    case "MoreAttach"://多附件上传
                        MoreAttach(context, attachPk, workid, fk_node, ensName, fk_flow, pkVal);
                        break;
                }
                //HttpContext.Current.Request.FilePath;
                //string strPath = System.Web.HttpContext.Current.Server.MapPath("~/upload/");
                //string strName = context.Request.Files[0].FileName;
                //context.Request.Files[0].SaveAs(System.IO.Path.Combine(strPath, strName));
            }


            context.Response.Charset = "UTF-8";
            context.Response.ContentEncoding = System.Text.Encoding.UTF8;
            context.Response.ContentType = "text/html";
            context.Response.Expires = 0;
            context.Response.Write(message);
            context.Response.End();
        }


        private void SingleAttach(HttpContext context, string attachPk, string workid, string fk_node, string ensName)
        {
            FrmAttachment frmAth = new FrmAttachment();
            frmAth.MyPK = attachPk;
            frmAth.RetrieveFromDBSources();

            string athDBPK = attachPk + "_" + workid;

            BP.WF.Node currND = new BP.WF.Node(fk_node);
            BP.WF.Work currWK = currND.HisWork;
            currWK.OID = long.Parse(workid);
            currWK.Retrieve();
            //处理保存路径.
            string saveTo = frmAth.SaveTo;

            if (saveTo.Contains("*") || saveTo.Contains("@"))
            {
                /*如果路径里有变量.*/
                saveTo = saveTo.Replace("*", "@");
                saveTo = BP.WF.Glo.DealExp(saveTo, currWK, null);
            }

            try
            {
                saveTo = context.Server.MapPath("~/" + saveTo);
            }
            catch (Exception)
            {
                saveTo = saveTo;
            }

            if (System.IO.Directory.Exists(saveTo) == false)
                System.IO.Directory.CreateDirectory(saveTo);


            saveTo = saveTo + "\\" + athDBPK + "." + context.Request.Files[0].FileName.Substring(context.Request.Files[0].FileName.LastIndexOf('.') + 1);
            context.Request.Files[0].SaveAs(saveTo);

            FileInfo info = new FileInfo(saveTo);
            FrmAttachmentDB dbUpload = new FrmAttachmentDB();
            dbUpload.MyPK = athDBPK;
            dbUpload.FK_FrmAttachment = attachPk;
            dbUpload.RefPKVal = workid;

            dbUpload.FK_MapData = ensName;

            dbUpload.FileExts = info.Extension;
            dbUpload.FileFullName = saveTo;
            dbUpload.FileName = context.Request.Files[0].FileName;
            dbUpload.FileSize = (float)info.Length;
            dbUpload.Rec = WebUser.No;
            dbUpload.RecName = WebUser.Name;
            dbUpload.RDT = BP.DA.DataType.CurrentDataTime;


            dbUpload.NodeID = fk_node;

            dbUpload.Save();

        }

        public void MoreAttach(HttpContext context, string attachPk, string workid, string fk_node, string ensNamestring, string fk_flow, string pkVal)
        {
            BP.Sys.FrmAttachment athDesc = new BP.Sys.FrmAttachment(attachPk);


            string savePath = athDesc.SaveTo;

            if (savePath.Contains("@") == true || savePath.Contains("*") == true)
            {
                /*如果有变量*/
                savePath = savePath.Replace("*", "@");
                GEEntity en = new GEEntity(athDesc.FK_MapData);
                en.PKVal = pkVal;
                en.Retrieve();
                savePath = BP.WF.Glo.DealExp(savePath, en, null);

                if (savePath.Contains("@") && fk_node != null)
                {
                    /*如果包含 @ */
                    BP.WF.Flow flow = new BP.WF.Flow(fk_flow);
                    BP.WF.Data.GERpt myen = flow.HisGERpt;
                    myen.OID = long.Parse(workid);
                    myen.RetrieveFromDBSources();
                    savePath = BP.WF.Glo.DealExp(savePath, myen, null);
                }
                if (savePath.Contains("@") == true)
                    throw new Exception("@路径配置错误,变量没有被正确的替换下来." + savePath);
            }
            else
            {
                savePath = athDesc.SaveTo + "\\" + pkVal;
            }

            //替换关键的字串.
            savePath = savePath.Replace("\\\\", "\\");
            try
            {

                savePath = context.Server.MapPath("~/" + savePath);

            }
            catch (Exception)
            {
                savePath = savePath;

            }
            try
            {

                if (System.IO.Directory.Exists(savePath) == false)
                {
                    System.IO.Directory.CreateDirectory(savePath);
                    //System.IO.Directory.CreateDirectory(athDesc.SaveTo);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("@创建路径出现错误，可能是没有权限或者路径配置有问题:" + context.Server.MapPath("~/" + savePath) + "===" + savePath + "@技术问题:" + ex.Message);
            }


            string exts = System.IO.Path.GetExtension(context.Request.Files[0].FileName).ToLower().Replace(".", "");




            //int oid = BP.DA.DBAccess.GenerOID();
            string guid = BP.DA.DBAccess.GenerGUID();

            string fileName = context.Request.Files[0].FileName.Substring(0, context.Request.Files[0].FileName.LastIndexOf('.'));
            //string ext = fu.FileName.Substring(fu.FileName.LastIndexOf('.') + 1);
            string ext = System.IO.Path.GetExtension(context.Request.Files[0].FileName);

            //string realSaveTo = Server.MapPath("~/" + savePath) + "/" + guid + "." + fileName + "." + ext;

            //string realSaveTo = Server.MapPath("~/" + savePath) + "\\" + guid + "." + fu.FileName.Substring(fu.FileName.LastIndexOf('.') + 1);
            //string saveTo = savePath + "/" + guid + "." + fileName + "." + ext;




            string realSaveTo = savePath + "/" + guid + "." + fileName + ext;

            string saveTo = realSaveTo;

            try
            {
                context.Request.Files[0].SaveAs(realSaveTo);
            }
            catch (Exception ex)
            {
                throw;

            }





            FileInfo info = new FileInfo(realSaveTo);
            FrmAttachmentDB dbUpload = new FrmAttachmentDB();

            dbUpload.MyPK = guid; // athDesc.FK_MapData + oid.ToString();
            dbUpload.NodeID = fk_node.ToString();
            dbUpload.FK_FrmAttachment = attachPk;



            dbUpload.FK_MapData = athDesc.FK_MapData;
            dbUpload.FK_FrmAttachment = attachPk;

            dbUpload.FileExts = info.Extension;
            dbUpload.FileFullName = saveTo;
            dbUpload.FileName = context.Request.Files[0].FileName;
            dbUpload.FileSize = (float)info.Length;

            dbUpload.RDT = DataType.CurrentDataTimess;
            dbUpload.Rec = BP.Web.WebUser.No;
            dbUpload.RecName = BP.Web.WebUser.Name;
            dbUpload.RefPKVal = pkVal;
            //if (athDesc.IsNote)
            //    dbUpload.MyNote = this.Pub1.GetTextBoxByID("TB_Note").Text;

            //if (athDesc.Sort.Contains(","))
            //    dbUpload.Sort = this.Pub1.GetDDLByID("ddl").SelectedItemStringVal;

            dbUpload.UploadGUID = guid;
            dbUpload.Insert();


        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

    }
}