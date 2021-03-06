using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;

using i3.ValueObject.Sys.WF;
using i3.DataAccess.Sys.WF;

namespace i3.BusinessLogic.Sys.WF
{
    /// <summary>
    /// 功能：流程分类
    /// 创建日期：2012-11-06
    /// 创建人：石磊
    /// </summary>
    public class TWfSettingBelongsLogic : LogicBase
    {

        TWfSettingBelongsVo tWfSettingBelongs = new TWfSettingBelongsVo();
        TWfSettingBelongsAccess access;

        public TWfSettingBelongsLogic()
        {
            access = new TWfSettingBelongsAccess();
        }

        public TWfSettingBelongsLogic(TWfSettingBelongsVo _tWfSettingBelongs)
        {
            tWfSettingBelongs = _tWfSettingBelongs;
            access = new TWfSettingBelongsAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tWfSettingBelongs">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TWfSettingBelongsVo tWfSettingBelongs)
        {
            return access.GetSelectResultCount(tWfSettingBelongs);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TWfSettingBelongsVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tWfSettingBelongs">对象条件</param>
        /// <returns>对象</returns>
        public TWfSettingBelongsVo Details(TWfSettingBelongsVo tWfSettingBelongs)
        {
            return access.Details(tWfSettingBelongs);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tWfSettingBelongs">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TWfSettingBelongsVo> SelectByObject(TWfSettingBelongsVo tWfSettingBelongs, int iIndex, int iCount)
        {
            return access.SelectByObject(tWfSettingBelongs, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tWfSettingBelongs">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TWfSettingBelongsVo tWfSettingBelongs, int iIndex, int iCount)
        {
            return access.SelectByTable(tWfSettingBelongs, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tWfSettingBelongs"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TWfSettingBelongsVo tWfSettingBelongs)
        {
            return access.SelectByTable(tWfSettingBelongs);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tWfSettingBelongs">对象</param>
        /// <returns></returns>
        public TWfSettingBelongsVo SelectByObject(TWfSettingBelongsVo tWfSettingBelongs)
        {
            return access.SelectByObject(tWfSettingBelongs);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TWfSettingBelongsVo tWfSettingBelongs)
        {
            return access.Create(tWfSettingBelongs);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingBelongs">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingBelongsVo tWfSettingBelongs)
        {
            return access.Edit(tWfSettingBelongs);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tWfSettingBelongs_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tWfSettingBelongs_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TWfSettingBelongsVo tWfSettingBelongs_UpdateSet, TWfSettingBelongsVo tWfSettingBelongs_UpdateWhere)
        {
            return access.Edit(tWfSettingBelongs_UpdateSet, tWfSettingBelongs_UpdateWhere);
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
        public bool Delete(TWfSettingBelongsVo tWfSettingBelongs)
        {
            return access.Delete(tWfSettingBelongs);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //编号
            if (tWfSettingBelongs.ID.Trim() == "")
            {
                this.Tips.AppendLine("编号不能为空");
                return false;
            }
            //分类编号
            if (tWfSettingBelongs.WF_CLASS_ID.Trim() == "")
            {
                this.Tips.AppendLine("分类编号不能为空");
                return false;
            }
            //分类父编号
            if (tWfSettingBelongs.WF_CLASS_PID.Trim() == "")
            {
                this.Tips.AppendLine("分类父编号不能为空");
                return false;
            }
            //分类名称
            if (tWfSettingBelongs.WF_CLASS_NAME.Trim() == "")
            {
                this.Tips.AppendLine("分类名称不能为空");
                return false;
            }
            //分类备注
            if (tWfSettingBelongs.WF_CLASS_NOTE.Trim() == "")
            {
                this.Tips.AppendLine("分类备注不能为空");
                return false;
            }
            //分类等级
            if (tWfSettingBelongs.WF_CLASS_LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("分类等级不能为空");
                return false;
            }
            //简述
            if (tWfSettingBelongs.REMARK.Trim() == "")
            {
                this.Tips.AppendLine("简述不能为空");
                return false;
            }

            return true;
        }

    }
}
