using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Channels.OA.TRAIN;
using i3.DataAccess.Channels.OA.TRAIN;

namespace i3.BusinessLogic.Channels.OA.TRAIN
{
    /// <summary>
    /// 功能：培训文件附件信息
    /// 创建日期：2013-01-28
    /// 创建人：胡方扬
    /// </summary>
    public class TOaTrainFileLogic : LogicBase
    {

        TOaTrainFileVo tOaTrainFile = new TOaTrainFileVo();
        TOaTrainFileAccess access;

        public TOaTrainFileLogic()
        {
            access = new TOaTrainFileAccess();
        }

        public TOaTrainFileLogic(TOaTrainFileVo _tOaTrainFile)
        {
            tOaTrainFile = _tOaTrainFile;
            access = new TOaTrainFileAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tOaTrainFile">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TOaTrainFileVo tOaTrainFile)
        {
            return access.GetSelectResultCount(tOaTrainFile);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TOaTrainFileVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tOaTrainFile">对象条件</param>
        /// <returns>对象</returns>
        public TOaTrainFileVo Details(TOaTrainFileVo tOaTrainFile)
        {
            return access.Details(tOaTrainFile);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tOaTrainFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TOaTrainFileVo> SelectByObject(TOaTrainFileVo tOaTrainFile, int iIndex, int iCount)
        {
            return access.SelectByObject(tOaTrainFile, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tOaTrainFile">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TOaTrainFileVo tOaTrainFile, int iIndex, int iCount)
        {
            return access.SelectByTable(tOaTrainFile, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tOaTrainFile"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TOaTrainFileVo tOaTrainFile)
        {
            return access.SelectByTable(tOaTrainFile);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tOaTrainFile">对象</param>
        /// <returns></returns>
        public TOaTrainFileVo SelectByObject(TOaTrainFileVo tOaTrainFile)
        {
            return access.SelectByObject(tOaTrainFile);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TOaTrainFileVo tOaTrainFile)
        {
            return access.Create(tOaTrainFile);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainFile">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainFileVo tOaTrainFile)
        {
            return access.Edit(tOaTrainFile);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tOaTrainFile_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tOaTrainFile_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TOaTrainFileVo tOaTrainFile_UpdateSet, TOaTrainFileVo tOaTrainFile_UpdateWhere)
        {
            return access.Edit(tOaTrainFile_UpdateSet, tOaTrainFile_UpdateWhere);
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
        public bool Delete(TOaTrainFileVo tOaTrainFile)
        {
            return access.Delete(tOaTrainFile);
        }


        /// <summary>
        /// 获取指定培训计划的所有附件信息 Create By 胡方扬 2013-1-28
        /// </summary>
        /// <param name="tOaTrainFile"></param>
        /// <returns></returns>
        public DataTable TrainFileByTableList(TOaTrainFileVo tOaTrainFile, int iIndex, int iCount)
        {
            return access.TrainFileByTableList(tOaTrainFile, iIndex, iCount);
        }

                        /// <summary>
        /// 获取指定培训计划的所有附件信息记录 Create By 胡方扬 2013-1-28
        /// </summary>
        /// <param name="tOaTrainFile"></param>
        /// <returns></returns>
        public int SelectTrainFileByTableCount(TOaTrainFileVo tOaTrainFile)
        {
            return access.SelectTrainFileByTableCount(tOaTrainFile);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //
            if (tOaTrainFile.ID.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //培训计划ID
            if (tOaTrainFile.TRAIN_PLAN_ID.Trim() == "")
            {
                this.Tips.AppendLine("培训计划ID不能为空");
                return false;
            }

            return true;
        }

    }
}
