using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;

using i3.ValueObject.Channels.Env.Fill.Offshore;
using i3.DataAccess.Channels.Env.Fill.Offshore;

namespace i3.BusinessLogic.Channels.Env.Fill.Offshore
{
    /// <summary>
    /// 功能：近岸直排数据填报
    /// 创建日期：2012-10-22
    /// /// 修改人：刘静楠
    /// 修改时间：2013-6-25
    public class TEnvFillOffshoreLogic : LogicBase
    {

        TEnvFillOffshoreVo tEnvFillOffshore = new TEnvFillOffshoreVo();
        TEnvFillOffshoreAccess access;

        public TEnvFillOffshoreLogic()
        {
            access = new TEnvFillOffshoreAccess();
        }

        public TEnvFillOffshoreLogic(TEnvFillOffshoreVo _tEnvFillOffshore)
        {
            tEnvFillOffshore = _tEnvFillOffshore;
            access = new TEnvFillOffshoreAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillOffshore">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            return access.GetSelectResultCount(tEnvFillOffshore);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillOffshoreVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillOffshore">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillOffshoreVo Details(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            return access.Details(tEnvFillOffshore);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillOffshore">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillOffshoreVo> SelectByObject(TEnvFillOffshoreVo tEnvFillOffshore, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillOffshore, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillOffshore">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillOffshoreVo tEnvFillOffshore, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillOffshore, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillOffshore"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            return access.SelectByTable(tEnvFillOffshore);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillOffshore">对象</param>
        /// <returns></returns>
        public TEnvFillOffshoreVo SelectByObject(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            return access.SelectByObject(tEnvFillOffshore);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            return access.Create(tEnvFillOffshore);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillOffshore">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            return access.Edit(tEnvFillOffshore);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillOffshore_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillOffshore_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillOffshoreVo tEnvFillOffshore_UpdateSet, TEnvFillOffshoreVo tEnvFillOffshore_UpdateWhere)
        {
            return access.Edit(tEnvFillOffshore_UpdateSet, tEnvFillOffshore_UpdateWhere);
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
        public bool Delete(TEnvFillOffshoreVo tEnvFillOffshore)
        {
            return access.Delete(tEnvFillOffshore);
        }

        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }




        #region//合法性验证
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillOffshore.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillOffshore.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillOffshore.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillOffshore.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillOffshore.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillOffshore.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillOffshore.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillOffshore.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //企业名称
            if (tEnvFillOffshore.COMPANY_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //排污口名称
            if (tEnvFillOffshore.POINT_CODE.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //排污口代码
            if (tEnvFillOffshore.POINT_NAME.Trim() == "")
            {
                this.Tips.AppendLine("不能为空");
                return false;
            }
            //是否达标
            if (tEnvFillOffshore.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("是否达标不能为空");
                return false;
            }
            //不达标项目
            if (tEnvFillOffshore.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("不达标项目不能为空");
                return false;
            }
            //污水流量
            if (tEnvFillOffshore.FLUX.Trim() == "")
            {
                this.Tips.AppendLine("污水流量不能为空");
                return false;
            }
            //污水排放时间
            if (tEnvFillOffshore.LET_TIME.Trim() == "")
            {
                this.Tips.AppendLine("污水排放时间不能为空");
                return false;
            }
            //污水量
            if (tEnvFillOffshore.WATER_TOTAL.Trim() == "")
            {
                this.Tips.AppendLine("污水量不能为空");
                return false;
            }
            //备注1
            if (tEnvFillOffshore.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillOffshore.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillOffshore.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillOffshore.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillOffshore.REMARK5.Trim() == "")
            {
                this.Tips.AppendLine("备注5不能为空");
                return false;
            }

            return true;
        }
        #endregion

        /// <summary>
        /// 获取填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="pointId">监测点</param>
        /// <returns></returns>
        public DataTable GetOffShoreFillData(string year, string month, string pointId)
        {
            DataTable dtMain = access.GetOffShoreFillData(year, month, pointId);

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
                    dtFill = access.SelectByTable(new TEnvFillOffshoreVo() { POINT_ID = pointId, MONTH = month });
                else
                    dtFill = access.SelectByTable(new TEnvFillOffshoreVo() { MONTH = month });

                //查询所有监测项填报数据
                if (dtFill.Rows.Count > 0)
                {
                    string fillId = "";
                    foreach (DataRow drFill in dtFill.Rows)
                    {
                        fillId += "'" + drFill["id"].ToString() + "',";
                    }
                    fillId = fillId.TrimEnd(',');
                    dtFillItem = new TEnvFillOffshoreItemLogic().SelectByTable(" and OFFSHORE_FILL_ID in(" + fillId + ")");
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
                    var fillData = dtFill.AsEnumerable().Where(c => c["offshore_point_id"].ToString().Equals(curPointId)).ToList(); //获取当前测点的填报数据

                    if (fillData.Count > 0)
                    {
                        drMain["sampling_day"] = fillData[0]["sampling_day"].ToString(); //填入采样日期

                        string curFillId = fillData[0]["id"].ToString(); //当前的数据填报ID
                        var fillItemData = dtFillItem.AsEnumerable().Where(c => c["offshore_fill_id"].ToString().Equals(curFillId)).ToList(); //获取当前数据填报的各监测项数据

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
        public bool SaveOffShoreFillData(DataTable dtData)
        {
            return access.SaveOffShoreFillData(dtData);
        }
    }
}
