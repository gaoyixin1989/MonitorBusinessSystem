using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.RPT;
using i3.DataAccess.Channels.RPT;

namespace i3.BusinessLogic.Channels.RPT
{
    /// <summary>
    /// 功能：报告文件印章表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptFileSignatureLogic : LogicBase
    {

        TRptFileSignatureVo tRptFileSignature = new TRptFileSignatureVo();
        TRptFileSignatureAccess access;

        public TRptFileSignatureLogic()
        {
            access = new TRptFileSignatureAccess();
        }

        public TRptFileSignatureLogic(TRptFileSignatureVo _tRptFileSignature)
        {
            tRptFileSignature = _tRptFileSignature;
            access = new TRptFileSignatureAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptFileSignature">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptFileSignatureVo tRptFileSignature)
        {
            return access.GetSelectResultCount(tRptFileSignature);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptFileSignatureVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptFileSignature">对象条件</param>
        /// <returns>对象</returns>
        public TRptFileSignatureVo Details(TRptFileSignatureVo tRptFileSignature)
        {
            return access.Details(tRptFileSignature);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptFileSignature">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptFileSignatureVo> SelectByObject(TRptFileSignatureVo tRptFileSignature, int iIndex, int iCount)
        {
            return access.SelectByObject(tRptFileSignature, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptFileSignature">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptFileSignatureVo tRptFileSignature, int iIndex, int iCount)
        {
            return access.SelectByTable(tRptFileSignature, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptFileSignature"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptFileSignatureVo tRptFileSignature)
        {
            return access.SelectByTable(tRptFileSignature);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptFileSignature">对象</param>
        /// <returns></returns>
        public TRptFileSignatureVo SelectByObject(TRptFileSignatureVo tRptFileSignature)
        {
            return access.SelectByObject(tRptFileSignature);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptFileSignatureVo tRptFileSignature)
        {
            return access.Create(tRptFileSignature);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptFileSignature">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptFileSignatureVo tRptFileSignature)
        {
            return access.Edit(tRptFileSignature);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptFileSignature_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tRptFileSignature_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptFileSignatureVo tRptFileSignature_UpdateSet, TRptFileSignatureVo tRptFileSignature_UpdateWhere)
        {
            return access.Edit(tRptFileSignature_UpdateSet, tRptFileSignature_UpdateWhere);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            return access.Delete(Id);
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TRptFileSignatureVo tRptFileSignature)
        {
            return access.Delete(tRptFileSignature);
        }

        #region WebOffice
        /// <summary>
        /// 保存印章
        /// </summary>
        /// <param name="signature">印章对象</param>
        /// <returns>是否成功</returns>
        public bool SaveSignature(TRptFileSignatureVo signature)
        {
            return access.SaveSignature(signature);
        }

        /// <summary>
        /// 调取指定文档的所有印章
        /// </summary>
        /// <param name="FileID">文件ID</param>
        /// <returns>印章对象</returns>
        public TRptFileSignatureVo LoadSignature(string FileID)
        {
            return access.LoadSignature(FileID);
        }
        #endregion

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tRptFileSignature.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //文件ID
            if (tRptFileSignature.FILE_ID.Trim() == "")
            {
                this.Tips.AppendLine("文件ID不能为空");
                return false;
            }
            //印章名称
            if (tRptFileSignature.MARK_NAME.Trim() == "")
            {
                this.Tips.AppendLine("印章名称不能为空");
                return false;
            }
            //添加用户
            if (tRptFileSignature.ADD_USER.Trim() == "")
            {
                this.Tips.AppendLine("添加用户不能为空");
                return false;
            }
            //添加日期
            if (tRptFileSignature.ADD_TIME.Trim() == "")
            {
                this.Tips.AppendLine("添加日期不能为空");
                return false;
            }
            //添加IP
            if (tRptFileSignature.ADD_IP.Trim() == "")
            {
                this.Tips.AppendLine("添加IP不能为空");
                return false;
            }
            //印章
            if (tRptFileSignature.MARK_GUID.Trim() == "")
            {
                this.Tips.AppendLine("印章不能为空");
                return false;
            }
            //备注1
            if (tRptFileSignature.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tRptFileSignature.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tRptFileSignature.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
