using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;

using i3.ValueObject.Channels.Env.Fill.River30;
using i3.DataAccess.Channels.Env.Fill.River30;
using i3.BusinessLogic.Channels.Base.Item;
using i3.ValueObject.Channels.Base.Item;

namespace i3.BusinessLogic.Channels.Env.Fill.River30
{
    /// <summary>
    /// 功能：双三十水断面数据填报表
    /// 创建日期：2013-05-08
    /// 创建人：潘德军
    /// modify :刘静楠
    /// time:2013-6-25
    /// </summary>
    public class TEnvFillRiver30Logic : LogicBase
    {

        TEnvFillRiver30Vo tEnvFillRiver30 = new TEnvFillRiver30Vo();
        TEnvFillRiver30Access access;

        public TEnvFillRiver30Logic()
        {
            access = new TEnvFillRiver30Access();
        }

        public TEnvFillRiver30Logic(TEnvFillRiver30Vo _tEnvFillRiver30)
        {
            tEnvFillRiver30 = _tEnvFillRiver30;
            access = new TEnvFillRiver30Access();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiver30">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiver30Vo tEnvFillRiver30)
        {
            return access.GetSelectResultCount(tEnvFillRiver30);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiver30Vo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiver30">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiver30Vo Details(TEnvFillRiver30Vo tEnvFillRiver30)
        {
            return access.Details(tEnvFillRiver30);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiver30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiver30Vo> SelectByObject(TEnvFillRiver30Vo tEnvFillRiver30, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiver30, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiver30">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiver30Vo tEnvFillRiver30, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiver30, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiver30"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiver30Vo tEnvFillRiver30)
        {
            return access.SelectByTable(tEnvFillRiver30);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiver30">对象</param>
        /// <returns></returns>
        public TEnvFillRiver30Vo SelectByObject(TEnvFillRiver30Vo tEnvFillRiver30)
        {
            return access.SelectByObject(tEnvFillRiver30);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiver30Vo tEnvFillRiver30)
        {
            return access.Create(tEnvFillRiver30);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiver30">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiver30Vo tEnvFillRiver30)
        {
            return access.Edit(tEnvFillRiver30);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiver30_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiver30_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiver30Vo tEnvFillRiver30_UpdateSet, TEnvFillRiver30Vo tEnvFillRiver30_UpdateWhere)
        {
            return access.Edit(tEnvFillRiver30_UpdateSet, tEnvFillRiver30_UpdateWhere);
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
        public bool Delete(TEnvFillRiver30Vo tEnvFillRiver30)
        {
            return access.Delete(tEnvFillRiver30);
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
        /// 获取河流数据填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="pointId">所在地区ID</param>
        /// <returns></returns>
        public DataTable GetRiver30FillData(string year, string month, string pointId)
        {
            DataTable dtMain = access.GetRiver30FillData(year, month, pointId);

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
                    dtFill = access.SelectByTableEx(new TEnvFillRiver30Vo() { REMARK1 = pointId, MONTH = month });
                else
                    dtFill = access.SelectByTable(new TEnvFillRiver30Vo() { MONTH = month });

                //查询所有监测项填报数据
                if (dtFill.Rows.Count > 0)
                {
                    string riverFillId = "";
                    foreach (DataRow drFill in dtFill.Rows)
                        riverFillId += "'" + drFill["ID"].ToString() + "',";
                    dtFillItem = new TEnvFillRiver30ItemLogic().SelectByTable(" and RIVER_FILL_ID in(" + riverFillId.TrimEnd(',') + ")");
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
                        var fillItemData = dtFillItem.AsEnumerable().Where(c => c["river_fill_id"].ToString().Equals(curFillId)).ToList(); //获取当前数据填报的各监测项数据

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
        /// 保存数据
        /// </summary>
        /// <param name="dtData">填报数据</param>
        /// <returns></returns>
        public bool SaveRiverFillData(DataTable dtData)
        {
            return access.SaveRiverFillData(dtData);
        }


        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillRiver30.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvFillRiver30.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillRiver30.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillRiver30.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillRiver30.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillRiver30.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillRiver30.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillRiver30.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillRiver30.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //枯水期、平水期、枯水期
            if (tEnvFillRiver30.KPF.Trim() == "")
            {
                this.Tips.AppendLine("枯水期、平水期、枯水期不能为空");
                return false;
            }
            //水质类别Code（字典项）
            if (tEnvFillRiver30.WATER_TYPE_CODE.Trim() == "")
            {
                this.Tips.AppendLine("水质类别Code（字典项）不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillRiver30.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRiver30.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRiver30.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRiver30.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRiver30.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRiver30.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }

    }
}
