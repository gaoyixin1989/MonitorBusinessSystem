using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;

using i3.ValueObject.Channels.Env.Fill.Bottom;
using i3.DataAccess.Channels.Env.Fill.Bottom;

namespace i3.BusinessLogic.Channels.Env.Fill.Bottom
{
    /// <summary>
    /// 功能：河流底泥数据填报表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TEnvFillRiverBottomLogic : LogicBase
    {

        TEnvFillRiverBottomVo tEnvFillRiverBottom = new TEnvFillRiverBottomVo();
        TEnvFillRiverBottomAccess access;

        public TEnvFillRiverBottomLogic()
        {
            access = new TEnvFillRiverBottomAccess();
        }

        public TEnvFillRiverBottomLogic(TEnvFillRiverBottomVo _tEnvFillRiverBottom)
        {
            tEnvFillRiverBottom = _tEnvFillRiverBottom;
            access = new TEnvFillRiverBottomAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiverBottom">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            return access.GetSelectResultCount(tEnvFillRiverBottom);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverBottomVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiverBottom">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverBottomVo Details(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            return access.Details(tEnvFillRiverBottom);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiverBottom">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverBottomVo> SelectByObject(TEnvFillRiverBottomVo tEnvFillRiverBottom, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiverBottom, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiverBottom">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverBottomVo tEnvFillRiverBottom, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiverBottom, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiverBottom"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            return access.SelectByTable(tEnvFillRiverBottom);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiverBottom">对象</param>
        /// <returns></returns>
        public TEnvFillRiverBottomVo SelectByObject(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            return access.SelectByObject(tEnvFillRiverBottom);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            return access.Create(tEnvFillRiverBottom);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverBottom">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            return access.Edit(tEnvFillRiverBottom);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiverBottom_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiverBottom_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverBottomVo tEnvFillRiverBottom_UpdateSet, TEnvFillRiverBottomVo tEnvFillRiverBottom_UpdateWhere)
        {
            return access.Edit(tEnvFillRiverBottom_UpdateSet, tEnvFillRiverBottom_UpdateWhere);
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
        public bool Delete(TEnvFillRiverBottomVo tEnvFillRiverBottom)
        {
            return access.Delete(tEnvFillRiverBottom);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillRiverBottom.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //饮用水断面监测点ID
            if (tEnvFillRiverBottom.RIVER_SEDIMENT_POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("饮用水断面监测点ID不能为空");
                return false;
            }
            //月份
            if (tEnvFillRiverBottom.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //垂线ID，对应T_BAS_POINT_RIVER_VERTICAL主键
            if (tEnvFillRiverBottom.VERTICAL_ID.Trim() == "")
            {
                this.Tips.AppendLine("垂线ID，对应T_BAS_POINT_RIVER_VERTICAL主键不能为空");
                return false;
            }
            //枯水期、平水期、枯水期
            if (tEnvFillRiverBottom.KPF.Trim() == "")
            {
                this.Tips.AppendLine("枯水期、平水期、枯水期不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillRiverBottom.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRiverBottom.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRiverBottom.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRiverBottom.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRiverBottom.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRiverBottom.REMARK5.Trim() == "")
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
        public DataTable GetBottomFillData(string year, string month, string pointId)
        {
            DataTable dtMain = access.GetBottomFillData(year, month, pointId);

            if (dtMain.Rows.Count > 0)
            {
                //查询要监测的监测项
                string verticalId = "";
                foreach (DataRow drMain in dtMain.Rows)
                    verticalId += "'" + drMain["vertical_id"].ToString() + "',";
                DataTable dtAllItem = access.GetFillItem(verticalId.TrimEnd(','));

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
                    dtFill = access.SelectByTable(new TEnvFillRiverBottomVo() { RIVER_SEDIMENT_POINT_ID = pointId, MONTH = month });
                else
                    dtFill = access.SelectByTable(new TEnvFillRiverBottomVo() { MONTH = month });

                //查询所有监测项填报数据
                if (dtFill.Rows.Count > 0)
                {
                    string fillId = "";
                    foreach (DataRow drFill in dtFill.Rows)
                        fillId += "'" + drFill["ID"].ToString() + "',";
                    dtFillItem = new TEnvFillRiverBottomItemLogic().SelectByTable(" and RIVER_SEDIMENT_FILL_ID in(" + fillId.TrimEnd(',') + ")");
                }

                //遍历以构建表格数据
                foreach (DataRow drMain in dtMain.Rows)
                {
                    //填入监测项ID
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        drMain[drAllItem["item_name"].ToString() + "_id"] = drAllItem["id"].ToString(); //填入监测项ID
                    }

                    string curVerticalId = drMain["vertical_id"].ToString(); //当前垂线ID
                    var fillData = dtFill.AsEnumerable().Where(c => c["vertical_id"].ToString().Equals(curVerticalId)).ToList(); //获取当前垂线的填报数据

                    if (fillData.Count > 0)
                    {
                        drMain["sampling_day"] = fillData[0]["sampling_day"].ToString(); //填入采样日期
                        drMain["kpf"] = fillData[0]["kpf"].ToString(); //填入水期

                        string curFillId = fillData[0]["id"].ToString(); //当前的数据填报ID
                        var fillItemData = dtFillItem.AsEnumerable().Where(c => c["river_sediment_fill_id"].ToString().Equals(curFillId)).ToList(); //获取当前数据填报的各监测项数据

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
        public bool SaveBottomFillData(DataTable dtData)
        {
            return access.SaveBottomFillData(dtData);
        }
    }
}
