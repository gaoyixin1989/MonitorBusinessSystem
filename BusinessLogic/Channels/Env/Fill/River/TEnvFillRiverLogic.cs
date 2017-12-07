using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using i3.ValueObject.Channels.Env.Fill.River;
using System.Data;
using i3.DataAccess.Channels.Env.Fill.River;

namespace i3.BusinessLogic.Channels.Env.Fill.River
{
    /// <summary>
    /// 功能：河流填报
    /// 创建日期：2013-06-18
    /// 创建人：魏林
    /// </summary>
    public class TEnvFillRiverLogic : LogicBase
    {

        TEnvFillRiverVo tEnvFillRiver = new TEnvFillRiverVo();
        TEnvFillRiverAccess access;

        public TEnvFillRiverLogic()
        {
            access = new TEnvFillRiverAccess();
        }

        public TEnvFillRiverLogic(TEnvFillRiverVo _tEnvFillRiver)
        {
            tEnvFillRiver = _tEnvFillRiver;
            access = new TEnvFillRiverAccess();
        }



        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tEnvFillRiver">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TEnvFillRiverVo tEnvFillRiver)
        {
            return access.GetSelectResultCount(tEnvFillRiver);
        }


        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TEnvFillRiverVo Details(string id)
        {
            return access.Details(id);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tEnvFillRiver">对象条件</param>
        /// <returns>对象</returns>
        public TEnvFillRiverVo Details(TEnvFillRiverVo tEnvFillRiver)
        {
            return access.Details(tEnvFillRiver);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tEnvFillRiver">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TEnvFillRiverVo> SelectByObject(TEnvFillRiverVo tEnvFillRiver, int iIndex, int iCount)
        {
            return access.SelectByObject(tEnvFillRiver, iIndex, iCount);

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tEnvFillRiver">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TEnvFillRiverVo tEnvFillRiver, int iIndex, int iCount)
        {
            return access.SelectByTable(tEnvFillRiver, iIndex, iCount);
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tEnvFillRiver"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TEnvFillRiverVo tEnvFillRiver)
        {
            return access.SelectByTable(tEnvFillRiver);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tEnvFillRiver">对象</param>
        /// <returns></returns>
        public TEnvFillRiverVo SelectByObject(TEnvFillRiverVo tEnvFillRiver)
        {
            return access.SelectByObject(tEnvFillRiver);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="sysRole">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TEnvFillRiverVo tEnvFillRiver)
        {
            return access.Create(tEnvFillRiver);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiver">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverVo tEnvFillRiver)
        {
            return access.Edit(tEnvFillRiver);
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tEnvFillRiver_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tEnvFillRiver_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TEnvFillRiverVo tEnvFillRiver_UpdateSet, TEnvFillRiverVo tEnvFillRiver_UpdateWhere)
        {
            return access.Edit(tEnvFillRiver_UpdateSet, tEnvFillRiver_UpdateWhere);
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
        public bool Delete(TEnvFillRiverVo tEnvFillRiver)
        {
            return access.Delete(tEnvFillRiver);
        }



        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            //主键ID
            if (tEnvFillRiver.ID.Trim() == "")
            {
                this.Tips.AppendLine("主键ID不能为空");
                return false;
            }
            //断面ID
            if (tEnvFillRiver.SECTION_ID.Trim() == "")
            {
                this.Tips.AppendLine("断面ID不能为空");
                return false;
            }
            //监测点ID
            if (tEnvFillRiver.POINT_ID.Trim() == "")
            {
                this.Tips.AppendLine("监测点ID不能为空");
                return false;
            }
            //采样日期
            if (tEnvFillRiver.SAMPLING_DAY.Trim() == "")
            {
                this.Tips.AppendLine("采样日期不能为空");
                return false;
            }
            //年度
            if (tEnvFillRiver.YEAR.Trim() == "")
            {
                this.Tips.AppendLine("年度不能为空");
                return false;
            }
            //月份
            if (tEnvFillRiver.MONTH.Trim() == "")
            {
                this.Tips.AppendLine("月份不能为空");
                return false;
            }
            //日
            if (tEnvFillRiver.DAY.Trim() == "")
            {
                this.Tips.AppendLine("日不能为空");
                return false;
            }
            //时
            if (tEnvFillRiver.HOUR.Trim() == "")
            {
                this.Tips.AppendLine("时不能为空");
                return false;
            }
            //分
            if (tEnvFillRiver.MINUTE.Trim() == "")
            {
                this.Tips.AppendLine("分不能为空");
                return false;
            }
            //枯水期、平水期、枯水期
            if (tEnvFillRiver.KPF.Trim() == "")
            {
                this.Tips.AppendLine("枯水期、平水期、枯水期不能为空");
                return false;
            }
            //评价
            if (tEnvFillRiver.JUDGE.Trim() == "")
            {
                this.Tips.AppendLine("评价不能为空");
                return false;
            }
            //超标污染类别污染物
            if (tEnvFillRiver.OVERPROOF.Trim() == "")
            {
                this.Tips.AppendLine("超标污染类别污染物不能为空");
                return false;
            }
            //备注1
            if (tEnvFillRiver.REMARK1.Trim() == "")
            {
                this.Tips.AppendLine("备注1不能为空");
                return false;
            }
            //备注2
            if (tEnvFillRiver.REMARK2.Trim() == "")
            {
                this.Tips.AppendLine("备注2不能为空");
                return false;
            }
            //备注3
            if (tEnvFillRiver.REMARK3.Trim() == "")
            {
                this.Tips.AppendLine("备注3不能为空");
                return false;
            }
            //备注4
            if (tEnvFillRiver.REMARK4.Trim() == "")
            {
                this.Tips.AppendLine("备注4不能为空");
                return false;
            }

            return true;
        }

        /// <summary>
        /// 获取河流数据填报数据
        /// </summary>
        /// <param name="year">年份</param>
        /// <param name="month">月份</param>
        /// <param name="pointId">断面ID</param>
        /// <returns></returns>
        public DataTable GetRiverFillData(string year, string month, string SectionID, ref DataTable dtAllItem, string unSureMark)
        {
            DataTable dtMain = access.GetRiverFillData(year, month, SectionID);
            if (dtMain.Rows.Count > 0)
            {
                //查询要监测的监测项
                string verticalId = "";
                foreach (DataRow drMain in dtMain.Rows)
                    verticalId += "'" + drMain["POINT_ID"].ToString() + "',";
                dtAllItem = access.GetFillItem(verticalId.TrimEnd(','));

                //把监测项拼接在表格中
                foreach (DataRow drAllItem in dtAllItem.Rows)
                {
                    dtMain.Columns.Add(drAllItem["ID"].ToString() + unSureMark, typeof(string));
                }

                DataTable dtFill = new DataTable(); //填报数据
                DataRow[] drFill;
                DataTable dtFillItem = new DataTable(); //监测项填报数据

                //根据条件查询所有填报数据
                dtFill = access.GetFillData(year, month, SectionID);

                foreach (DataRow drMain in dtMain.Rows)
                {
                    drFill = dtFill.Select("SECTION_ID='" + drMain["SECTION_ID"].ToString() + "' and POINT_ID='" + drMain["POINT_ID"].ToString() + "'");
                    if (drFill.Length > 0)
                    {
                        drMain["FillID"] = drFill[0]["ID"].ToString(); //填报表ID
                        drMain["KPF"] = drFill[0]["KPF"].ToString(); //填入水期
                        drMain["SAMPLING_DAY"] = drFill[0]["SAMPLING_DAY"].ToString(); //采样日期
                        drMain["OVERPROOF"] = drFill[0]["OVERPROOF"].ToString(); //超标污染物
                    }
                    //填入各监测项的值
                    foreach (DataRow drAllItem in dtAllItem.Rows)
                    {
                        string itemId = drAllItem["ID"].ToString(); //监测项ID
                        var itemValue = drFill.Where(c => c["ITEM_ID"].Equals(itemId)).ToList(); //监测项值

                        if (itemValue.Count > 0)
                            drMain[itemId + unSureMark] = itemValue[0]["ITEM_VALUE"].ToString(); //填入监测项值
                    }
                }
            }
            
            return dtMain;
        }
        /// <summary>
        /// 去除不必要更新的数据字段
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="UpdateName"></param>
        /// <returns></returns>
        public DataTable ChangeDataTable(DataTable dt, string UpdateName, string unSureMark)
        {
            return access.ChangeDataTable(dt, UpdateName, unSureMark);
        }

        /// <summary>
        /// 构造填报表需要显示的信息
        /// </summary>
        /// <returns></returns>
        public DataTable CreateShowDT()
        {
            return access.CreateShowDT();
        }
    }

}
