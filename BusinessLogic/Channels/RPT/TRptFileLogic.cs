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
    /// 功能：报告表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptFileLogic : LogicBase
    {

        TRptFileVo tRptFile = new TRptFileVo();
        TRptFileAccess access;

        public TRptFileLogic()
        {
            access = new TRptFileAccess();
        }

        public TRptFileLogic(TRptFileVo _tRptFile)
        {
            tRptFile = _tRptFile;
            access = new TRptFileAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptFile">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptFileVo tRptFile)
        {
            return access.GetSelectResultCount(tRptFile);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptFileVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptFile">对象条件</param>
        /// <returns>对象</returns>
        public TRptFileVo Details(TRptFileVo tRptFile)
        {
            return access.Details(tRptFile);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptFileVo> SelectByObject(TRptFileVo tRptFile, int iIndex, int iCount)
        {
            return access.SelectByObject(tRptFile, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptFileVo tRptFile, int iIndex, int iCount)
        {
            return access.SelectByTable(tRptFile, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptFile"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptFileVo tRptFile)
        {
            return access.SelectByTable(tRptFile);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptFile">对象</param>
        /// <returns></returns>
        public TRptFileVo SelectByObject(TRptFileVo tRptFile)
        {
            return access.SelectByObject(tRptFile);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptFileVo tRptFile)
        {
            return access.Create(tRptFile);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptFile">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptFileVo tRptFile)
        {
            return access.Edit(tRptFile);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptFile_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tRptFile_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptFileVo tRptFile_UpdateSet, TRptFileVo tRptFile_UpdateWhere)
        {
            return access.Edit(tRptFile_UpdateSet, tRptFile_UpdateWhere);
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
        public bool Delete(TRptFileVo tRptFile)
        {
            return access.Delete(tRptFile);
        }

        #region WebOffice
        /// <summary>
        /// 调取文件
        /// </summary>
        /// <param name="FileID">文件ID</param>
        /// <returns>文件文件流</returns>
        public byte[] LoadFile(string FileID)
        {
            return access.LoadFile(FileID);
        }

        /// <summary>
        /// 保存文件
        /// 1、如果存在，覆盖原文件；
        /// 2、如果不存在，插入该文件；
        /// </summary>
        /// <param name="template">文件对象</param>
        /// <returns>是否成功</returns>
        public bool SaveFile(TRptFileVo file)
        {
            return access.SaveFile(file);
        }

        /// <summary>
        /// 功能描述：根据业务ID获取最新报告文件信息
        /// 创建时间：2012-12-12
        /// 创建人：邵世卓
        /// </summary>
        /// <param name="strContractId">监测任务ID</param>
        /// <returns></returns>
        public TRptFileVo getNewReportByContractID(string strContractId)
        {
            return access.getNewReportByContractID(strContractId);
        }
        #endregion

        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tRptFile.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //委托书ID
            if (tRptFile.CONTRACT_ID.Trim() == "")
            {
                this.Tips.AppendLine("委托书ID不能为空");
                return false;
            }
            //文件名
            if (tRptFile.FILE_NAME.Trim() == "")
            {
                this.Tips.AppendLine("文件名不能为空");
                return false;
            }
            //文件类型
            if (tRptFile.FILE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("文件类型不能为空");
                return false;
            }
            //文件大小
            if (tRptFile.FILE_SIZE.Trim() == "")
            {
                this.Tips.AppendLine("文件大小不能为空");
                return false;
            }
            //文件内容
            if (tRptFile.FILE_BODY == null)
            {
                this.Tips.AppendLine("文件内容不能为空");
                return false;
            }
            //文件路径
            if (tRptFile.FILE_PATH.Trim() == "")
            {
                this.Tips.AppendLine("文件路径不能为空");
                return false;
            }
            //文件描述
            if (tRptFile.FILE_DESC.Trim() == "")
            {
                this.Tips.AppendLine("文件描述不能为空");
                return false;
            }
            //添加日期
            if (tRptFile.ADD_TIME.Trim() == "")
            {
                this.Tips.AppendLine("添加日期不能为空");
                return false;
            }
            //添加人
            if (tRptFile.ADD_USER.Trim() == "")
            {
                this.Tips.AppendLine("添加人不能为空");
                return false;
            }
            //备注1
            if (tRptFile.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tRptFile.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tRptFile.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
