using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;

using i3.ValueObject.Channels.Env.Fill.Rain;
using i3.DataAccess.Channels.Env.Fill.Rain;

namespace i3.BusinessLogic.Channels.Env.Fill.Rain
{
    /// <summary>
    /// 功能：降水数据填报表
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// 修改人：刘静楠
    /// 修改时间：2013-6-24
    /// </summary>
    public class TEnvFillRainLogic : LogicBase
    {

        TEnvFillRainVo tEnvFillRain = new TEnvFillRainVo();
        TEnvFillRainAccess access;

        public TEnvFillRainLogic()
        {
            access = new TEnvFillRainAccess();
        }

        public TEnvFillRainLogic(TEnvFillRainVo _tEnvFillRain)
        {
            tEnvFillRain = _tEnvFillRain;
            access = new TEnvFillRainAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRain">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRainVo tEnvFillRain)
        {
            return access.GetSelectResultCount(tEnvFillRain);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRainVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRain">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRainVo Details(TEnvFillRainVo tEnvFillRain)
        {
            return access.Details(tEnvFillRain);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRainVo> SelectByObject(TEnvFillRainVo tEnvFillRain, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRain, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRain">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRainVo tEnvFillRain, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRain, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRain"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRainVo tEnvFillRain)
        {
            return access.SelectByTable(tEnvFillRain);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRain">对象</param>
        /// <returns></returns>
        public TEnvFillRainVo SelectByObject(TEnvFillRainVo tEnvFillRain)
        {
            return access.SelectByObject(tEnvFillRain);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRainVo tEnvFillRain)
        {
            return access.Create(tEnvFillRain);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRain">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRainVo tEnvFillRain)
        {
            return access.Edit(tEnvFillRain);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRain_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRain_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRainVo tEnvFillRain_UpdateSet, TEnvFillRainVo tEnvFillRain_UpdateWhere)
        {
            return access.Edit(tEnvFillRain_UpdateSet, tEnvFillRain_UpdateWhere);
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
        public bool Delete(TEnvFillRainVo tEnvFillRain)
        {
            return access.Delete(tEnvFillRain);
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
            if (tEnvFillRain.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillRain.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //年度
            if (tEnvFillRain.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月度
            if (tEnvFillRain.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月度不能为空");
                return false; 
            }
            //开始月
            if (tEnvFillRain.BEGIN_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("开始月不能为空");
                return false;
            }
            //开始日
            if (tEnvFillRain.BEGIN_DAY.Trim() == "")
            {
                this.Tips.AppendLine("开始日不能为空");
                return false;
            }
            //开始时
            if (tEnvFillRain.BEGIN_HOUR.Trim() == "")
            {
                this.Tips.AppendLine("开始时不能为空");
                return false;
            }
            //开始分
            if (tEnvFillRain.BEGIN_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("开始分不能为空");
                return false;
            }
            //结束月
            if (tEnvFillRain.END_MONTH.Trim() == "")
            {
                this.Tips.AppendLine("结束月不能为空");
                return false;
            }
            //结束日
            if (tEnvFillRain.END_DAY.Trim() == "")
            {
                this.Tips.AppendLine("结束日不能为空");
                return false;
            }
            //结束时
            if (tEnvFillRain.END_HOUR.Trim() == "")
            {
                this.Tips.AppendLine("结束时不能为空");
                return false;
            }
            //结束分
            if (tEnvFillRain.END_MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("结束分不能为空");
                return false;
            }
            //降水类型
            if (tEnvFillRain.RAIN_TYPE.Trim() == "")
            {
                this.Tips.AppendLine("降水类型不能为空");
                return false;
            }
            //降水量
            if (tEnvFillRain.PRECIPITATION.Trim() == "")
            {
                this.Tips.AppendLine("降水量不能为空");
                return false;
            }
            //评价
            if (tEnvFillRain.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillRain.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRain.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRain.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRain.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRain.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }
            //备注5
            if (tEnvFillRain.REMARK5.Trim() == "")
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
        /// <param name="year">年度</param>
        /// <param name="month">月份</param>
        /// <param name="day">日期</param>
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public DataTable GetRainFillData(string year, string month, string day, string pointId)
        {
            DataTable dtMain = access.GetRainFillData(pointId, month, day);

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
                DataTable dtFillItem = new DataTable(); //监测项填报数据

                //查询所有监测项填报数据
                if (dtMain.Rows.Count > 0)
                {
                    string fillId = "";
                    foreach (DataRow drMain in dtMain.Rows)
                    {
                        fillId += "'" + drMain["fill_id"].ToString() + "',";
                    }
                    fillId = fillId.TrimEnd(',');
                    dtFillItem = new TEnvFillRainItemLogic().SelectByTable(" and RAIN_FILL_ID in(" + fillId + ")");
                }

                //遍历以构建表格数据
                foreach (DataRow drMain in dtMain.Rows)
                {
                    //填入监测项ID
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        drMain[drAllItem["item_name"].ToString() + "_id"] = drAllItem["id"].ToString(); //填入监测项ID
                    }

                    string curFillId = drMain["fill_id"].ToString(); //当前的数据填报ID
                    var fillItemData = dtFillItem.AsEnumerable().Where(c => c["rain_fill_id"].ToString().Equals(curFillId)).ToList(); //获取当前数据填报的各监测项数据

                    //填入各监测项的值
                    foreach (var FID in fillItemData)
                    {
                        //获取监测项名称
                        string itemName = dtAllItem.AsEnumerable().Where(c => c["id"].ToString().Equals(FID["item_id"].ToString())).ToList()[0]["item_name"].ToString();

                        drMain[itemName] = FID["item_value"].ToString(); //填入监测项值
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
        /// <param name="pointId">监测点ID</param>
        /// <returns></returns>
        public bool SaveRainFillData(DataTable dtData, string pointId)
        {
            return access.SaveRainFillData(dtData, pointId);
        }
    }
}
