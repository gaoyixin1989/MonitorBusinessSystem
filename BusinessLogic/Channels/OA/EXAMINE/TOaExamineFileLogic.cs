using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.EXAMINE;
using i3.DataAccess.Channels.OA.EXAMINE;

namespace i3.BusinessLogic.Channels.OA.EXAMINE
{
    /// <summary>
    /// 功能：人员考核总结文件
    /// 创建日期：2012-10-22
    /// 创建人：潘德军
    /// </summary>
    public class TOaExamineFileLogic : LogicBase
    {

	TOaExamineFileVo tOaExamineFile = new TOaExamineFileVo();
    TOaExamineFileAccess access;
		
	public TOaExamineFileLogic()
	{
		  access = new TOaExamineFileAccess();  
	}
		
	public TOaExamineFileLogic(TOaExamineFileVo _tOaExamineFile)
	{
		tOaExamineFile = _tOaExamineFile;
		access = new TOaExamineFileAccess();
	}

        

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaExamineFile">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaExamineFileVo tOaExamineFile)
        {
            return access.GetSelectResultCount(tOaExamineFile);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaExamineFileVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaExamineFile">对象条件</param>
        /// <returns>对象</returns>
        public TOaExamineFileVo Details(TOaExamineFileVo tOaExamineFile)
        {
            return access.Details(tOaExamineFile);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaExamineFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaExamineFileVo> SelectByObject(TOaExamineFileVo tOaExamineFile, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaExamineFile, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaExamineFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaExamineFileVo tOaExamineFile, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaExamineFile, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaExamineFile"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaExamineFileVo tOaExamineFile)
        {
            return access.SelectByTable(tOaExamineFile);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaExamineFile">对象</param>
        /// <returns></returns>
        public TOaExamineFileVo SelectByObject(TOaExamineFileVo tOaExamineFile)
        {
            return access.SelectByObject(tOaExamineFile);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaExamineFileVo tOaExamineFile)
        {
            return access.Create(tOaExamineFile);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaExamineFile">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaExamineFileVo tOaExamineFile)
        {
            return access.Edit(tOaExamineFile);
        }

	/// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaExamineFile_UpdateSet">UpdateSet用户对象</param>
	/// <param name="tOaExamineFile_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaExamineFileVo tOaExamineFile_UpdateSet, TOaExamineFileVo tOaExamineFile_UpdateWhere)
        {
            return access.Edit(tOaExamineFile_UpdateSet, tOaExamineFile_UpdateWhere);
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
        public bool Delete(TOaExamineFileVo tOaExamineFile)
        {
            return access.Delete(tOaExamineFile);
        }
	

        
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
				  //编号
	  if (tOaExamineFile.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
	  //业务编码
	  if (tOaExamineFile.EXAMINE_ID.Trim() == "")
            {
                this.Tips.AppendLine("业务编码不能为空");
                return false;
            }
	  //文件类型
	  if (tOaExamineFile.FILE_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("文件类型不能为空");
                return false;
            }
	  //文件大小
	  if (tOaExamineFile.FILE_SIZE.Trim() == "")
            {
                this.Tips.AppendLine("文件大小不能为空");
                return false;
            }
	  //文件内容
	  if (tOaExamineFile.FILE_BODY.Trim() == "")
            {
                this.Tips.AppendLine("文件内容不能为空");
                return false;
            }
	  //文件路径
	  if (tOaExamineFile.FILE_PATH.Trim() == "")
            {
                this.Tips.AppendLine("文件路径不能为空");
                return false;
            }
	  //文件描述
	  if (tOaExamineFile.FILE_DESC.Trim() == "")
            {
                this.Tips.AppendLine("文件描述不能为空");
                return false;
            }
	  //备注1
	  if (tOaExamineFile.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
	  //备注2
	  if (tOaExamineFile.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
	  //备注3
	  if (tOaExamineFile.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
	  //备注4
	  if (tOaExamineFile.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
	  //备注5
	  if (tOaExamineFile.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
