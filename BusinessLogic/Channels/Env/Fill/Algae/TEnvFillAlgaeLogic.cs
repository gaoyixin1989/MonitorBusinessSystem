using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;

using i3.ValueObject.Channels.Env.Fill.Algae;
using i3.DataAccess.Channels.Env.Fill.Algae;

namespace i3.BusinessLogic.Channels.Env.Fill.Algae
{
    /// <summary>
    /// 功能：蓝藻水华数据填报
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillAlgaeLogic : LogicBase
    {

        TEnvFillAlgaeVo tEnvFillAlgae = new TEnvFillAlgaeVo();
        TEnvFillAlgaeAccess access;

        public TEnvFillAlgaeLogic()
        {
            access = new TEnvFillAlgaeAccess();
        }

        public TEnvFillAlgaeLogic(TEnvFillAlgaeVo _tEnvFillAlgae)
        {
            tEnvFillAlgae = _tEnvFillAlgae;
            access = new TEnvFillAlgaeAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillAlgae">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            return access.GetSelectResultCount(tEnvFillAlgae);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillAlgaeVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillAlgae">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillAlgaeVo Details(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            return access.Details(tEnvFillAlgae);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillAlgae">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillAlgaeVo> SelectByObject(TEnvFillAlgaeVo tEnvFillAlgae, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillAlgae, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillAlgae">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillAlgaeVo tEnvFillAlgae, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillAlgae, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillAlgae"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            return access.SelectByTable(tEnvFillAlgae);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillAlgae">对象</param>
        /// <returns></returns>
        public TEnvFillAlgaeVo SelectByObject(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            return access.SelectByObject(tEnvFillAlgae);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            return access.Create(tEnvFillAlgae);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlgae">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            return access.Edit(tEnvFillAlgae);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillAlgae_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillAlgae_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillAlgaeVo tEnvFillAlgae_UpdateSet, TEnvFillAlgaeVo tEnvFillAlgae_UpdateWhere)
        {
            return access.Edit(tEnvFillAlgae_UpdateSet, tEnvFillAlgae_UpdateWhere);
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
        public bool Delete(TEnvFillAlgaeVo tEnvFillAlgae)
        {
            return access.Delete(tEnvFillAlgae);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillAlgae.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //饮用水断面监测点ID
            if (tEnvFillAlgae.ALGAE_POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("饮用水断面监测点ID不能为空");
                return false;
            }
            //月份
            if (tEnvFillAlgae.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillAlgae.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //备注1
            if (tEnvFillAlgae.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillAlgae.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillAlgae.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillAlgae.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillAlgae.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点</param>
        /// <returns></returns>
        public DataTable GetAlgaeFillData(string year, string month, string pointId)
        {
            DataTable dtMain = access.GetAlgaeFillData(year, month, pointId);

            if (dtMain.Rows.Count > 0)
            {
                //查询要监测的监测项
                DataTable dtAllItem = access.GetFillItem("'" + pointId + "'");

                //把监测项拼接在表格中
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns.Add(drAllItem["ITEM_NAME"].ToString() + "_id", typeof(string));
                    dtMain.Columns.Add(drAllItem["ITEM_NAME"].ToString(), typeof(string));
                }

                DataTable dtFill = new DataTable(); //填报数据
                DataTable dtFillItem = new DataTable(); //监测项填报数据

                //查询所有填报数据
                if (!string.IsNullOrEmpty(pointId))
                    dtFill = access.SelectByTable(new TEnvFillAlgaeVo() { ALGAE_POINT_ID = pointId, MONTH = month });
                else
                    dtFill = access.SelectByTable(new TEnvFillAlgaeVo() { MONTH = month });

                //查询所有监测项填报数据
                if (dtFill.Rows.Count > 0)
                {
                    string fillId = "";
                    foreach (DataRow drFill in dtFill.Rows)
                    {
                        fillId += "'" + drFill["id"].ToString() + "',";
                    }
                    fillId = fillId.TrimEnd(',');
                    dtFillItem = new TEnvFillAlgaeItemLogic().SelectByTable(" and ALGAE_FILL_ID in(" + fillId + ")");
                }

                //遍历以构建表格数据
                foreach (DataRow drMain in dtMain.Rows)
                {
                    //填入监测项ID
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        drMain[drAllItem["item_name"].ToString() + "_id"] = drAllItem["id"].ToString(); //填入监测项ID
                    }

                    string curPointId = drMain["point_id"].ToString(); //当前测点ID
                    var fillData = dtFill.AsEnumerable().Where(c => c["algae_point_id"].ToString().Equals(curPointId)).ToList(); //获取当前测点的填报数据

                    if (fillData.Count > 0)
                    {
                        drMain["sampling_day"] = fillData[0]["sampling_day"].ToString(); //填入采样日期

                        string curFillId = fillData[0]["id"].ToString(); //当前的数据填报ID
                        var fillItemData = dtFillItem.AsEnumerable().Where(c => c["algae_fill_id"].ToString().Equals(curFillId)).ToList(); //获取当前数据填报的各监测项数据

                        //填入各监测项的值
                        foreach (DataRow drAllItem in dtAllItem.Rows)
                        {
                            string itemName = drAllItem["item_name"].ToString(); //监测项名称
                            string itemId = drAllItem["id"].ToString(); //监测项ID
                            var itemValue = fillItemData.Where(c => c["item_id"].Equals(itemId)).ToList(); //监测项值

                            if (itemValue.Count > 0)
                                drMain[itemName] = itemValue[0]["item_value"].ToString(); //填入监测项值
                        }
                    }
                }

                //声明不确定的列
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns[drAllItem["item_name"].ToString() + "_id"].ColumnName += "_unSure";
                    dtMain.Columns[drAllItem["item_name"].ToString()].ColumnName += "_unSure";
                }
            }

            return dtMain;
        }

        /// <summary>
        /// 保存填报数据
        /// </summary>
        /// <param name="dtData">填报数据</param>
        /// <returns></returns>
        public bool SaveAlgaeFillData(DataTable dtData)
        {
            return access.SaveAlgaeFillData(dtData);
        }
    }
}
