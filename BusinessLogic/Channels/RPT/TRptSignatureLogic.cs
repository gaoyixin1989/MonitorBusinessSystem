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
    /// 功能：印章表
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TRptSignatureLogic : LogicBase
    {

        TRptSignatureVo tRptSignature = new TRptSignatureVo();
        TRptSignatureAccess access;

        public TRptSignatureLogic()
        {
            access = new TRptSignatureAccess();
        }

        public TRptSignatureLogic(TRptSignatureVo _tRptSignature)
        {
            tRptSignature = _tRptSignature;
            access = new TRptSignatureAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TRptSignatureVo tRptSignature)
        {
            return access.GetSelectResultCount(tRptSignature);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TRptSignatureVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tRptSignature">对象条件</param>
        /// <returns>对象</returns>
        public TRptSignatureVo Details(TRptSignatureVo tRptSignature)
        {
            return access.Details(tRptSignature);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TRptSignatureVo> SelectByObject(TRptSignatureVo tRptSignature, int iIndex, int iCount)
        {
            return access.SelectByObject(tRptSignature, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TRptSignatureVo tRptSignature, int iIndex, int iCount)
        {
            return access.SelectByTable(tRptSignature, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tRptSignature"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TRptSignatureVo tRptSignature)
        {
            return access.SelectByTable(tRptSignature);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <returns></returns>
        public TRptSignatureVo SelectByObject(TRptSignatureVo tRptSignature)
        {
            return access.SelectByObject(tRptSignature);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TRptSignatureVo tRptSignature)
        {
            return access.Create(tRptSignature);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptSignature">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptSignatureVo tRptSignature)
        {
            return access.Edit(tRptSignature);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tRptSignature_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tRptSignature_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TRptSignatureVo tRptSignature_UpdateSet, TRptSignatureVo tRptSignature_UpdateWhere)
        {
            return access.Edit(tRptSignature_UpdateSet, tRptSignature_UpdateWhere);
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
        public bool Delete(TRptSignatureVo tRptSignature)
        {
            return access.Delete(tRptSignature);
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <param name="strDept">部门</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCountByDept(TRptSignatureVo tRptSignature, string strDept)
        {
            return access.GetSelectResultCountByDept(tRptSignature, strDept);
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tRptSignature">对象</param>
        /// <param name="strDept">部门</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTableByDept(TRptSignatureVo tRptSignature, string strDept, int iIndex, int iCount)
        {
            return access.SelectByTableByDept(tRptSignature, strDept, iIndex, iCount);
        }

        #region WebOffice
        /// <summary>
        /// 调取所有的印章
        /// </summary>
        /// <param name="MarkList">印章列表</param>
        /// <returns>是否成功</returns>
        public bool LoadMarkList(out string MarkList)
        {
            return access.LoadMarkList(out MarkList);
        }

        /// <summary>
        /// 根据用户名和密码调取印章
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <param name="Mark_Body">文件流</param>
        /// <param name="Mark_Type">文件类型</param>
        /// <returns>是否成功</returns>
        public bool LoadMarkImage(string UserName, string PassWord, out byte[] Mark_Body, out string Mark_Type)
        {
            return access.LoadMarkImage(UserName, PassWord, out Mark_Body, out Mark_Type);
        }
        #endregion


        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //ID
            if (tRptSignature.ID.Trim() == "")
            {
                this.Tips.AppendLine("ID不能为空");
                return false;
            }
            //印章名称
            if (tRptSignature.MARK_NAME.Trim() == "")
            {
                this.Tips.AppendLine("印章名称不能为空");
                return false;
            }
            //印章文件
            if (tRptSignature.MARK_BODY == null)
            {
                this.Tips.AppendLine("印章文件不能为空");
                return false;
            }
            //文件类型
            if (tRptSignature.MARK_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("文件类型不能为空");
                return false;
            }
            //文件路径
            if (tRptSignature.MARK_PATH.Trim() == "")
            {
                this.Tips.AppendLine("文件路径不能为空");
                return false;
            }
            //文件大小
            if (tRptSignature.MARK_SIZE.Trim() == "")
            {
                this.Tips.AppendLine("文件大小不能为空");
                return false;
            }
            //用户名
            if (tRptSignature.USER_NAME.Trim() == "")
            {
                this.Tips.AppendLine("用户名不能为空");
                return false;
            }
            //用户密码
            if (tRptSignature.PASS_WORD.Trim() == "")
            {
                this.Tips.AppendLine("用户密码不能为空");
                return false;
            }
            //添加日期
            if (tRptSignature.ADD_TIME.Trim() == "")
            {
                this.Tips.AppendLine("添加日期不能为空");
                return false;
            }
            //添加IP
            if (tRptSignature.ADD_USER.Trim() == "")
            {
                this.Tips.AppendLine("添加IP不能为空");
                return false;
            }
            //备注1
            if (tRptSignature.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tRptSignature.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tRptSignature.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }

            return true;
        }

    }
}
