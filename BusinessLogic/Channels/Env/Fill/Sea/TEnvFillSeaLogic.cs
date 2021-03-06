using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;

using i3.ValueObject.Channels.Env.Fill.Sea;
using i3.DataAccess.Channels.Env.Fill.Sea;

namespace i3.BusinessLogic.Channels.Env.Fill.Sea
{
    /// <summary>
    /// 功能：近海海域数据填报
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-25
    /// </summary>
    public class TEnvFillSeaLogic : LogicBase
    {

        TEnvFillSeaVo tEnvFillSea = new TEnvFillSeaVo();
        TEnvFillSeaAccess access;

        public TEnvFillSeaLogic()
        {
            access = new TEnvFillSeaAccess();
        }

        public TEnvFillSeaLogic(TEnvFillSeaVo _tEnvFillSea)
        {
            tEnvFillSea = _tEnvFillSea;
            access = new TEnvFillSeaAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillSea">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillSeaVo tEnvFillSea)
        {
            return access.GetSelectResultCount(tEnvFillSea);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillSeaVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillSea">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillSeaVo Details(TEnvFillSeaVo tEnvFillSea)
        {
            return access.Details(tEnvFillSea);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillSeaVo> SelectByObject(TEnvFillSeaVo tEnvFillSea, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillSea, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillSea">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillSeaVo tEnvFillSea, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillSea, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillSea"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillSeaVo tEnvFillSea)
        {
            return access.SelectByTable(tEnvFillSea);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillSea">对象</param>
        /// <returns></returns>
        public TEnvFillSeaVo SelectByObject(TEnvFillSeaVo tEnvFillSea)
        {
            return access.SelectByObject(tEnvFillSea);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillSeaVo tEnvFillSea)
        {
            return access.Create(tEnvFillSea);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSea">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeaVo tEnvFillSea)
        {
            return access.Edit(tEnvFillSea);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillSea_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillSea_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillSeaVo tEnvFillSea_UpdateSet, TEnvFillSeaVo tEnvFillSea_UpdateWhere)
        {
            return access.Edit(tEnvFillSea_UpdateSet, tEnvFillSea_UpdateWhere);
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
        public bool Delete(TEnvFillSeaVo tEnvFillSea)
        {
            return access.Delete(tEnvFillSea);
        }

        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillSea.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillSea.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillSea.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillSea.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillSea.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillSea.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillSea.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillSea.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //海区代码
            if (tEnvFillSea.SEA_AREA_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //重点海域代码
            if (tEnvFillSea.KEY_AREA_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //水深
            if (tEnvFillSea.DEPTH.Trim() == "")
            {
                this.Tips.AppendLine("水深不能为空");
                return false;
            }
            //枯水期、平水期、枯水期
            if (tEnvFillSea.KPF.Trim() == "")
            {
                this.Tips.AppendLine("枯水期、平水期、枯水期不能为空");
                return false;
            }
            //层次
            if (tEnvFillSea.LEVEL.Trim() == "")
            {
                this.Tips.AppendLine("层次不能为空");
                return false;
            }
            //评价
            if (tEnvFillSea.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染物
            if (tEnvFillSea.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillSea.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillSea.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillSea.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillSea.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillSea.REMARK5.Trim() == "")
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
        public DataTable GetSeaFillData(string year, string month, string pointId)
        {
            DataTable dtMain = access.GetSeaFillData(year, month, pointId);

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
                    dtFill = access.SelectByTable(new TEnvFillSeaVo() { POINT_ID = pointId, MONTH = month });
                else
                    dtFill = access.SelectByTable(new TEnvFillSeaVo() { MONTH = month });

                //查询所有监测项填报数据
                if (dtFill.Rows.Count > 0)
                {
                    string fillId = "";
                    foreach (DataRow drFill in dtFill.Rows)
                    {
                        fillId += "'" + drFill["id"].ToString() + "',";
                    }
                    fillId = fillId.TrimEnd(',');
                    dtFillItem = new TEnvFillSeaItemLogic().SelectByTable(" and SEA_FILL_ID in(" + fillId + ")");
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
                    var fillData = dtFill.AsEnumerable().Where(c => c["sea_point_id"].ToString().Equals(curPointId)).ToList(); //获取当前测点的填报数据

                    if (fillData.Count > 0)
                    {
                        drMain["sampling_day"] = fillData[0]["sampling_day"].ToString(); //填入采样日期
                        drMain["sample_num"] = fillData[0]["sample_num"].ToString(); //填入样品编号

                        string curFillId = fillData[0]["id"].ToString(); //当前的数据填报ID
                        var fillItemData = dtFillItem.AsEnumerable().Where(c => c["sea_fill_id"].ToString().Equals(curFillId)).ToList(); //获取当前数据填报的各监测项数据

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
        public bool SaveSeaFillData(DataTable dtData)
        {
            return access.SaveSeaFillData(dtData);
        }
    }
}
