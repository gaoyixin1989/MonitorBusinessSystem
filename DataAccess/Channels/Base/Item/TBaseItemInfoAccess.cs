using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Collections;
using System.Data;

using i3.ValueObject.Channels.Base.Item;
using i3.ValueObject;
using System.IO;

namespace i3.DataAccess.Channels.Base.Item
{
    /// <summary>
    /// 功能：监测项目管理
    /// 创建日期：2012-10-22
    /// 创建人：熊卫华
    /// </summary>
    public class TBaseItemInfoAccess : SqlHelper
    {

        #region 处理函数
        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="strSrhMONITOR_ID">监测类别</param>
        /// <param name="strSrhItemName">监测项目</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ForSelectItem_inBag(string strSrhMONITOR_ID, string strSrhItemName, int iIndex, int iCount)
        {

            string strSQL = " select ID,ITEM_NAME from T_BASE_ITEM_INFO ";
            strSQL += " where IS_DEL='0' and IS_SUB='1' and HAS_SUB_ITEM='0'";
            if (strSrhMONITOR_ID.Length > 0)
                strSQL += " and MONITOR_ID = '" + strSrhMONITOR_ID + "'";
            if (strSrhItemName.Length > 0)
                strSQL += " and ITEM_NAME like '%" + strSrhItemName + "%'";
            strSQL += " order by ORDER_NUM";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="strSrhMONITOR_ID">监测类别</param>
        /// <param name="strSrhItemName">监测项目</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ForSelectItem_inBag(string strSrhMONITOR_ID, string strSrhItemName)
        {
            string strSQL = "select Count(*) from T_BASE_ITEM_INFO where IS_DEL='0' and IS_SUB='1' and HAS_SUB_ITEM='0'";
            if (strSrhMONITOR_ID.Length > 0)
                strSQL += " and MONITOR_ID = '" + strSrhMONITOR_ID + "'";
            if (strSrhItemName.Length > 0)
                strSQL += " and ITEM_NAME like '%" + strSrhItemName + "%'";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount(TBaseItemInfoVo tBaseItemInfo)
        {
            string strSQL = "select Count(*) from T_BASE_ITEM_INFO " + this.BuildWhereStatement(tBaseItemInfo);
            strSQL += " and  monitor_id in (select id from T_BASE_MONITOR_TYPE_INFO where is_del='0')";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 获得查询结果总行数，用于分页
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <returns>返回行数</returns>
        public int GetSelectResultCount_ItemOfBag(string strItemBagID)
        {
            string strSQL = " select * from T_BASE_ITEM_INFO i ";
            strSQL += " join T_BASE_ITEM_SUB_ITEM s on s.ITEM_ID=i.ID";
            strSQL += " join T_BASE_ITEM_INFO ip on ip.ID=s.PARENT_ITEM_ID";
            strSQL += " where i.IS_SUB='1' and i.HAS_SUB_ITEM='0' and  i.IS_DEL='0' and s.IS_DEL='0' and ip.IS_DEL='0' and ip.id='" + strItemBagID + "'";
            strSQL = "select Count(*) from  (" + strSQL + ")";
            return Convert.ToInt32(SqlHelper.ExecuteScalar(strSQL));
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>对象</returns>
        public TBaseItemInfoVo Details(string id)
        {
            string strSQL = String.Format("select * from  T_BASE_ITEM_INFO  where id='{0}'", id);

            return SqlHelper.ExecuteObject(new TBaseItemInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象明细
        /// </summary>
        /// <param name="tBaseItemInfo">对象条件</param>
        /// <returns>对象</returns>
        public TBaseItemInfoVo Details(TBaseItemInfoVo tBaseItemInfo)
        {
            string strSQL = String.Format("select * from  T_BASE_ITEM_INFO " + this.BuildWhereStatement(tBaseItemInfo));
            return SqlHelper.ExecuteObject(new TBaseItemInfoVo(), strSQL);
        }

        /// <summary>
        /// 获取对象List
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public List<TBaseItemInfoVo> SelectByObject(TBaseItemInfoVo tBaseItemInfo, int iIndex, int iCount)
        {

            string strSQL = String.Format("select * from  T_BASE_ITEM_INFO " + this.BuildWhereStatement(tBaseItemInfo));
            return SqlHelper.ExecuteObjectList(tBaseItemInfo, BuildPagerExpress(strSQL, iIndex, iCount));

        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable(TBaseItemInfoVo tBaseItemInfo, int iIndex, int iCount)
        {

            string strSQL = " select * from T_BASE_ITEM_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseItemInfo));
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ByJoinMonitorType(TBaseItemInfoVo tBaseItemInfo, int iIndex, int iCount)
        {
           // InsertData();
            string strSQL = " select * from T_BASE_ITEM_INFO {0} ";
            strSQL = String.Format(strSQL, BuildWhereStatement(tBaseItemInfo));
            strSQL = "select m.MONITOR_TYPE_NAME as MONITOR_NAME,i.* from (" + strSQL + ")i join T_BASE_MONITOR_TYPE_INFO m on m.id=i.monitor_id where m.is_del='0'";
            strSQL += " order by i.MONITOR_ID,i.ORDER_NUM";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        #region//把水和废水的监测项目变成空气类
        public void InsertData()
        {
            string strsql = "select  ID,Item_Name,Monitor_ID,IS_SAMPLEDEPT from T_BASE_ITEM_INFO Where 1=1  AND MONITOR_ID = '000000002' AND HAS_SUB_ITEM = '0' AND IS_SUB = '1' AND IS_DEL = '0' and ID like '%FQ%'";
            DataTable DT = SqlHelper.ExecuteDataTable(strsql);
            if (DT.Rows.Count > 0)
            {
                foreach (DataRow dr in DT.Rows)
                {
                    TBaseItemInfoVo objItem = new TBaseItemInfoVo();
                    objItem.ID = dr["ID"].ToString().Replace('F', 'K');
                    objItem.IS_DEL = "0";
                    objItem.HAS_SUB_ITEM = "0";//监测项目管理默认直接子项目。说明，父项目，在用户界面以项目包形式存在，不属于项目范畴；子项目、项目包，在数据库存一个表，对用户应该是两个概念。
                    objItem.IS_SUB = "1";
                    objItem.MONITOR_ID = "001000019";
                    objItem.ITEM_NAME = dr["Item_Name"].ToString();
                    objItem.IS_SAMPLEDEPT = dr["IS_SAMPLEDEPT"].ToString();
                    objItem.LAB_CERTIFICATE = "是";
                    objItem.MEASURE_CERTIFICATE = "是";
                    objItem.IS_ANYSCENE_ITEM = "0";
                    objItem.REMARK1 = "空气类";
                    string strLog = SqlHelper.BuildInsertExpress(objItem, TBaseItemInfoVo.T_BASE_ITEM_INFO_TABLE);
                    WriteLog(strLog);
                    string sql = "select ID,Item_id,ANALYSIS_METHOD_ID from T_BASE_ITEM_ANALYSIS where item_id='" + dr["ID"].ToString() + "'";
                    DataTable dts = SqlHelper.ExecuteDataTable(sql);
                    if (dts.Rows.Count > 0)
                    {
                        int count = 1;
                        foreach (DataRow drs in dts.Rows)
                        {
                            TBaseItemAnalysisVo objItemAnalysisSet = new TBaseItemAnalysisVo();
                            if (drs["ID"].ToString().Contains("FQ"))
                            {
                                if (dts.Rows.Count >= 2)
                                {
                                    if (count == 1)
                                    {
                                        objItemAnalysisSet.ID = objItem.ID;
                                        objItemAnalysisSet.IS_DEL = "0";
                                        objItemAnalysisSet.ITEM_ID = objItem.ID;
                                        objItemAnalysisSet.ANALYSIS_METHOD_ID = drs["ANALYSIS_METHOD_ID"].ToString();
                                        objItemAnalysisSet.IS_DEFAULT = "否";
                                        objItemAnalysisSet.PRECISION = "0";
                                        objItemAnalysisSet.REMARK1 = "空气类";
                                        count++;
                                    }
                                    else
                                    {
                                        string joinid = string.Empty;
                                        string ID = objItem.ID.Substring(2, 7);
                                        int newID = Convert.ToInt32(ID) + (count-1);
                                        if (newID <= 9)
                                        {
                                            joinid = "KQ000000" + newID.ToString();
                                        }
                                        else
                                        {
                                            joinid = "KQ00000" + newID.ToString();
                                        }
                                        objItemAnalysisSet.ID = joinid;
                                        objItemAnalysisSet.IS_DEL = "0";
                                        objItemAnalysisSet.ITEM_ID = joinid;
                                        objItemAnalysisSet.ANALYSIS_METHOD_ID = drs["ANALYSIS_METHOD_ID"].ToString();
                                        objItemAnalysisSet.IS_DEFAULT = "否";
                                        objItemAnalysisSet.PRECISION = "0";
                                        objItemAnalysisSet.REMARK1 = "空气类";
                                        count++;
                                    }
                                }
                                else
                                {
                                    objItemAnalysisSet.ID = objItem.ID;
                                    objItemAnalysisSet.IS_DEL = "0";
                                    objItemAnalysisSet.ITEM_ID = objItem.ID;
                                    objItemAnalysisSet.ANALYSIS_METHOD_ID = drs["ANALYSIS_METHOD_ID"].ToString();
                                    objItemAnalysisSet.IS_DEFAULT = "否";
                                    objItemAnalysisSet.PRECISION = "0";
                                    objItemAnalysisSet.REMARK1 = "空气类";
                                }
                            }
                            else
                            {
                                objItemAnalysisSet.ID = GetSerialNumber("t_base_item_analysis_id");
                                objItemAnalysisSet.IS_DEL = "0";
                                objItemAnalysisSet.ITEM_ID = objItem.ID;
                                objItemAnalysisSet.ANALYSIS_METHOD_ID = drs["ANALYSIS_METHOD_ID"].ToString();
                                objItemAnalysisSet.IS_DEFAULT = "否";
                                objItemAnalysisSet.PRECISION = "0";
                                objItemAnalysisSet.REMARK1 = "空气类";
                            }
                            string strSQL = SqlHelper.BuildInsertExpress(objItemAnalysisSet, TBaseItemAnalysisVo.T_BASE_ITEM_ANALYSIS_TABLE);
                            WriteLog(strSQL);
                        }
                        if (dts.Rows.Count < count)
                        {
                            count = 1;
                        }
                    }

                }
            }
            string strsqls = "select  ID,Item_Name,Monitor_ID,IS_SAMPLEDEPT from T_BASE_ITEM_INFO Where 1=1  AND MONITOR_ID = '000000002' AND HAS_SUB_ITEM = '0' AND IS_SUB = '1' AND IS_DEL = '0' and ID NOT  like '%FQ%'";
            DataTable DTs = SqlHelper.ExecuteDataTable(strsqls);
            if (DTs.Rows.Count > 0)
            {
                foreach (DataRow dr in DTs.Rows)
                {
                    TBaseItemInfoVo objItem = new TBaseItemInfoVo();
                    objItem.ID = GetSerialNumber("t_base_item_info_id");
                    objItem.IS_DEL = "0";
                    objItem.HAS_SUB_ITEM = "0";//监测项目管理默认直接子项目。说明，父项目，在用户界面以项目包形式存在，不属于项目范畴；子项目、项目包，在数据库存一个表，对用户应该是两个概念。
                    objItem.IS_SUB = "1";
                    objItem.MONITOR_ID = "001000019";
                    objItem.ITEM_NAME = dr["Item_Name"].ToString();
                    objItem.IS_SAMPLEDEPT = dr["IS_SAMPLEDEPT"].ToString();
                    objItem.LAB_CERTIFICATE = "是";
                    objItem.MEASURE_CERTIFICATE = "是";
                    objItem.IS_ANYSCENE_ITEM = "0";
                    objItem.REMARK1 = "空气类";
                    string strLog = SqlHelper.BuildInsertExpress(objItem, TBaseItemInfoVo.T_BASE_ITEM_INFO_TABLE);
                    WriteLog(strLog);
                    string sql = "select ID,Item_id,ANALYSIS_METHOD_ID from T_BASE_ITEM_ANALYSIS where item_id='" + dr["ID"].ToString() + "'";
                    DataTable dts = SqlHelper.ExecuteDataTable(sql);
                    if (dts.Rows.Count > 0)
                    {
                        foreach (DataRow drs in dts.Rows)
                        {
                            TBaseItemAnalysisVo objItemAnalysisSet = new TBaseItemAnalysisVo();
                            objItemAnalysisSet.ID = GetSerialNumber("t_base_item_analysis_id");
                            objItemAnalysisSet.IS_DEL = "0";
                            objItemAnalysisSet.ITEM_ID = objItem.ID;
                            objItemAnalysisSet.ANALYSIS_METHOD_ID = drs["ANALYSIS_METHOD_ID"].ToString();
                            objItemAnalysisSet.IS_DEFAULT = "否";
                            objItemAnalysisSet.PRECISION = "0";
                            objItemAnalysisSet.REMARK1 = "空气类";
                            string strSQL = SqlHelper.BuildInsertExpress(objItemAnalysisSet, TBaseItemAnalysisVo.T_BASE_ITEM_ANALYSIS_TABLE);
                            WriteLog(strSQL);
                        }
                    }
                }
            }
        }

        #region//写入日志
        /// <summary>
        /// 写入日志
        /// </summary>
        /// <param name="strLog">写入日志详细信息</param>
        public static void WriteLog(string strLog)
        {
            //定义日志文件名称
            string strLogFileName = DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
            //获取当前安装目录名称
            string strAppInstallationPath = System.AppDomain.CurrentDomain.BaseDirectory;
            //文件路径
            string strTextFilePath = strAppInstallationPath + "\\" + strLogFileName;
            //日志信息
            string strMessage = strLog + Environment.NewLine;
            //将日志写入文本当中
            File.AppendAllText(strTextFilePath, strMessage);
        }
        #endregion
        #endregion

        /// <summary>
        /// 获取对象DataTable
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <param name="iIndex">起始页码</param>
        /// <param name="iCount">每页数目</param>
        /// <returns>返回结果</returns>
        public DataTable SelectByTable_ItemOfBag(string strItemBagID, int iIndex, int iCount)
        {
            string strSQL = " select s.ID,i.ID as ItemID,ip.ITEM_NAME as ItemBagName,i.Item_Name as ItemName from T_BASE_ITEM_INFO i ";
            strSQL += " join T_BASE_ITEM_SUB_ITEM s on s.ITEM_ID=i.ID";
            strSQL += " join T_BASE_ITEM_INFO ip on ip.ID=s.PARENT_ITEM_ID";
            strSQL += " where i.IS_SUB='1' and i.HAS_SUB_ITEM='0' and  i.IS_DEL='0' and s.IS_DEL='0' and ip.IS_DEL='0' and ip.id='" + strItemBagID + "'";
            strSQL += " order by i.ORDER_NUM";
            return SqlHelper.ExecuteDataTable(BuildPagerExpress(strSQL, iIndex, iCount));
        }

        /// <summary>
        /// 根据对象获取全部数据，用Table承载
        ///  数据量较小时使用【不推荐】
        /// </summary>
        /// <param name="tBaseItemInfo"></param>
        /// <returns></returns>
        public DataTable SelectByTable(TBaseItemInfoVo tBaseItemInfo)
        {
            string strSQL = "select * from T_BASE_ITEM_INFO " + this.BuildWhereStatement(tBaseItemInfo);
            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 根据对象特征获取单一对象
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <returns></returns>
        public TBaseItemInfoVo SelectByObject(TBaseItemInfoVo tBaseItemInfo)
        {
            string strSQL = "select * from T_BASE_ITEM_INFO " + this.BuildWhereStatement(tBaseItemInfo);
            return SqlHelper.ExecuteObject(new TBaseItemInfoVo(), strSQL);
        }

        /// <summary>
        /// 对象添加
        /// </summary>
        /// <param name="tBaseItemInfo">对象</param>
        /// <returns>是否成功</returns>
        public bool Create(TBaseItemInfoVo tBaseItemInfo)
        {
            string strSQL = SqlHelper.BuildInsertExpress(tBaseItemInfo, TBaseItemInfoVo.T_BASE_ITEM_INFO_TABLE);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemInfo">用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemInfoVo tBaseItemInfo)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseItemInfo, TBaseItemInfoVo.T_BASE_ITEM_INFO_TABLE);
            strSQL += string.Format(" where ID='{0}' ", tBaseItemInfo.ID);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// ph值、溶解氧、电导率，现场项目转变成实验室项目
        /// </summary>
        /// huangjinjun add 2016.1.25
        /// <returns>是否成功</returns>
        public bool EditItemTypeFX()
        {
            string strSQL = "update t_base_item_info set is_sampledept='否' where id in('000001989','000001991','000002143')";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// ph值、溶解氧、电导率，实验室项目转变成现场项目
        /// </summary>
        /// huangjinjun add 2016.1.25
        /// <returns>是否成功</returns>
        public bool EditItemTypeXC()
        {
            string strSQL = "update t_base_item_info set is_sampledept='是' where id in('000001989','000001991','000002143')";
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }


        /// <summary>
        /// 对象编辑
        /// </summary>
        /// <param name="tBaseItemInfo_UpdateSet">UpdateSet用户对象</param>
        /// <param name="tBaseItemInfo_UpdateWhere">UpdateWhere用户对象</param>
        /// <returns>是否成功</returns>
        public bool Edit(TBaseItemInfoVo tBaseItemInfo_UpdateSet, TBaseItemInfoVo tBaseItemInfo_UpdateWhere)
        {
            string strSQL = SqlHelper.BuildUpdateExpress(tBaseItemInfo_UpdateSet, TBaseItemInfoVo.T_BASE_ITEM_INFO_TABLE);
            strSQL += this.BuildWhereStatement(tBaseItemInfo_UpdateWhere);
            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(string Id)
        {
            string strSQL = String.Format("delete from T_BASE_ITEM_INFO where ID='{0}'", Id);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 对象删除
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public bool Delete(TBaseItemInfoVo tBaseItemInfo)
        {
            string strSQL = "delete from T_BASE_ITEM_INFO ";
            strSQL += this.BuildWhereStatement(tBaseItemInfo);

            return SqlHelper.ExecuteNonQuery(CommandType.Text, strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 根据样品ID(质控类型)查找项目-非现场项目
        /// </summary>
        /// <param name="Id">ID</param>
        /// <returns>是否成功</returns>
        public DataTable SelectItemForQC(string strSampleID, string strQcType)
        {
//            string strSQL = @"select * from T_BASE_ITEM_INFO where IS_SAMPLEDEPT='否' and ID in 
//                                (select ITEM_ID from T_MIS_MONITOR_RESULT where 1=1 {0} {1} )";
            string strSQL = @"select * from T_BASE_ITEM_INFO where  ID in 
                                (select ITEM_ID from T_MIS_MONITOR_RESULT where 1=1 {0} {1} )";
            if (strQcType == "0")
                strSampleID = " and SAMPLE_ID in ('" + strSampleID + "' )";
            else
                strSampleID = " and SAMPLE_ID in (select ID from T_MIS_MONITOR_SAMPLE_INFO where QC_SOURCE_ID = '" + strSampleID + "'  and QC_TYPE = '" + strQcType + "' )";
            if (strQcType.Length > 0)
                strQcType = " and QC_TYPE = '" + strQcType + "'";
            strSQL = string.Format(strSQL, strSampleID, strQcType);

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 获取样品的现场加标信息
        /// </summary>
        /// <param name="strSampleID"></param>
        /// <param name="strQcType"></param>
        /// <returns></returns>
        public DataTable SelectQcAddItem(string strSampleID, string strQcType)
        {
            string strSQL = @"select b.ID,c.ITEM_NAME,b.QC_ADD 
                            from T_MIS_MONITOR_RESULT a 
                            inner join T_MIS_MONITOR_QC_ADD b on(a.ID=b.RESULT_ID_ADD)
                            left join T_BASE_ITEM_INFO c on(a.ITEM_ID=c.ID)
                            where a.SAMPLE_ID='" + strSampleID + "' and a.QC_TYPE='" + strQcType + "'";

            return SqlHelper.ExecuteDataTable(strSQL);
        }
        /// <summary>
        /// 获取样品的密码盲样信息
        /// </summary>
        /// <param name="strSampleID"></param>
        /// <param name="strQcType"></param>
        /// <returns></returns>
        public DataTable SelectQcBlindItem(string strSampleID, string strQcType)
        {
            string strSQL = @"select b.ID,c.ITEM_NAME,b.STANDARD_VALUE,b.UNCETAINTY 
                            from T_MIS_MONITOR_RESULT a 
                            inner join T_MIS_MONITOR_QC_BLIND_ZZ b on(a.ID=b.RESULT_ID)
                            left join T_BASE_ITEM_INFO c on(a.ITEM_ID=c.ID)
                            where a.SAMPLE_ID='" + strSampleID + "' and a.QC_TYPE='" + strQcType + "'";

            return SqlHelper.ExecuteDataTable(strSQL);
        }

        /// <summary>
        /// 采样时，修改现场加标和盲样的方法
        /// </summary>
        /// <param name="strTable"></param>
        /// <param name="strUpdateCell"></param>
        /// <param name="strUpdateCellValue"></param>
        /// <param name="strID"></param>
        /// <returns></returns>
        public bool UpdateQcInfo(string strTable, string strUpdateCell, string strUpdateCellValue, string strID)
        {
            string strSQL = "";
            strSQL = "UPDATE {0} SET {1}='{2}' WHERE ID='{3}'";
            strSQL = string.Format(strSQL, strTable, strUpdateCell, strUpdateCellValue, strID);

            return SqlHelper.ExecuteNonQuery(strSQL) > 0 ? true : false;
        }

        /// <summary>
        /// 为监测类别复制监测项目
        /// </summary>
        /// <param name="tBaseItemInfo"></param>
        /// <returns></returns>
        public bool CopySameMonitorItemInfor(TBaseItemInfoVo tBaseItemInfo, string strToId)
        {
            string strSQL = "";
            ArrayList objArr = new ArrayList();
            DataTable dt = SelectByTable(tBaseItemInfo);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string strItemId = GetSerialNumber("t_base_item_info_id");
                    strSQL = string.Format(" INSERT INTO T_BASE_ITEM_INFO (ID,ITEM_NAME,MONITOR_ID,IS_SAMPLEDEPT,HAS_SUB_ITEM,IS_SUB,ITEM_NUM,ANALYSE_NUM,TEST_POINT_NUM,PRETREATMENT_FEE,TEST_ANSY_FEE,CHARGE,TEST_POWER_FEE,LAB_CERTIFICATE,MEASURE_CERTIFICATE,TWIN_VALUE,ADD_MIN,ADD_MAX,ORDER_NUM,IS_DEL)  SELECT '{0}',ITEM_NAME,'{1}',IS_SAMPLEDEPT,HAS_SUB_ITEM,IS_SUB,ITEM_NUM,ANALYSE_NUM,TEST_POINT_NUM,PRETREATMENT_FEE,TEST_ANSY_FEE,CHARGE,TEST_POWER_FEE,LAB_CERTIFICATE,MEASURE_CERTIFICATE,TWIN_VALUE,ADD_MIN,ADD_MAX,ORDER_NUM,IS_DEL FROM T_BASE_ITEM_INFO WHERE ID='{2}'", strItemId, strToId, dr["ID"].ToString());
                    objArr.Add(strSQL);

                    TBaseItemAnalysisVo objAnsy = new TBaseItemAnalysisVo();
                    objAnsy.ITEM_ID = dr["ID"].ToString();

                    DataTable dtAnsy = new TBaseItemAnalysisAccess().SelectByTable(objAnsy);
                    if (dtAnsy.Rows.Count > 0)
                    {
                        foreach (DataRow drr in dtAnsy.Rows)
                        {
                            string strAnsyId = GetSerialNumber("t_base_item_analysis_id");
                            strSQL = string.Format(" INSERT INTO T_BASE_ITEM_ANALYSIS (ID,ITEM_ID,ANALYSIS_METHOD_ID,INSTRUMENT_ID,UNIT,PRECISION,UPPER_LIMIT,LOWER_LIMIT,LOWER_CHECKOUT,IS_DEFAULT,IS_DEL)  SELECT '{0}','{1}',ANALYSIS_METHOD_ID,INSTRUMENT_ID,UNIT,PRECISION,UPPER_LIMIT,LOWER_LIMIT,LOWER_CHECKOUT,IS_DEFAULT,IS_DEL FROM T_BASE_ITEM_ANALYSIS WHERE ID='{2}'", strAnsyId, strItemId, drr["ID"].ToString());
                            objArr.Add(strSQL);
                        }
                    }
                }
            }

            return SqlHelper.ExecuteSQLByTransaction(objArr);
        }
        /// <summary>
        /// 设置默认现场采样仪器
        /// </summary>
        /// <param name="strItemId">监测项目ID</param>
        /// <param name="strSamplingInstrumentId">现场采样仪器ID</param>
        /// <returns></returns>
        public bool setItemSamplingInstrumentDefault(string strItemId, string strSamplingInstrumentId)
        {
            ArrayList arr = new ArrayList();
            string strSql = "update T_BASE_ITEM_SAMPLING_INSTRUMENT set IS_DEFAULT='0' where IS_DEL='0' and ITEM_ID='" + strItemId + "'";
            arr.Add(strSql);
            strSql = "update T_BASE_ITEM_SAMPLING_INSTRUMENT set IS_DEFAULT='1' where ID='" + strSamplingInstrumentId + "'";
            arr.Add(strSql);
            return SqlHelper.ExecuteSQLByTransaction(arr);
        }

        /// <summary>
        /// 获取监测项目要用到的原始记录表的表名称和表ID
        /// </summary>
        /// <param name="strLikeWhere"></param>
        /// <returns></returns>
        public DataTable GetSysDutyCataLogTableInfor(string strLikeWhere) {
            string strSQL = @"SELECT object_id as table_id,name as table_name FROM sys.objects WHERE 1=1";
            if (!String.IsNullOrEmpty(strLikeWhere)) {
                strSQL += @" AND name like '{0}'";
            }
            strSQL = String.Format(strSQL,strLikeWhere);

            return SqlHelper.ExecuteDataTable(strSQL);
        }
        #endregion

        #region 构造条件

        /// <summary>
        /// 根据对象构造条件语句
        /// </summary>
        /// <param name="tBaseItemInfo"></param>
        /// <returns></returns>
        public string BuildWhereStatement(TBaseItemInfoVo tBaseItemInfo)
        {
            StringBuilder strWhereStatement = new StringBuilder(" Where 1=1 ");
            if (null != tBaseItemInfo)
            {

                //ID
                if (!String.IsNullOrEmpty(tBaseItemInfo.ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ID = '{0}'", tBaseItemInfo.ID.ToString()));
                }
                //监测项目名称
                if (!String.IsNullOrEmpty(tBaseItemInfo.ITEM_NAME.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_NAME like '%{0}%'", tBaseItemInfo.ITEM_NAME.ToString()));
                }
                //监测类别（废水和废气、环境空气和废气、土壤和固体废弃物、噪声和震动、电磁辐射及放射性、其它）
                if (!String.IsNullOrEmpty(tBaseItemInfo.MONITOR_ID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MONITOR_ID = '{0}'", tBaseItemInfo.MONITOR_ID.ToString()));
                }
                //废水类，现场室填写项目
                if (!String.IsNullOrEmpty(tBaseItemInfo.IS_SAMPLEDEPT.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_SAMPLEDEPT = '{0}'", tBaseItemInfo.IS_SAMPLEDEPT.ToString()));
                }
                //是否包含监测子项
                if (!String.IsNullOrEmpty(tBaseItemInfo.HAS_SUB_ITEM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND HAS_SUB_ITEM = '{0}'", tBaseItemInfo.HAS_SUB_ITEM.ToString()));
                }
                //是否是监测子项
                if (!String.IsNullOrEmpty(tBaseItemInfo.IS_SUB.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_SUB = '{0}'", tBaseItemInfo.IS_SUB.ToString()));
                }
                //项目代码，用于秦皇岛交接单打印
                if (!String.IsNullOrEmpty(tBaseItemInfo.ITEM_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ITEM_NUM = '{0}'", tBaseItemInfo.ITEM_NUM.ToString()));
                }
                //样品数，用于清远监测项目费用计算
                if (!String.IsNullOrEmpty(tBaseItemInfo.TEST_POINT_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND SAMPLE_NUM = '{0}'", tBaseItemInfo.TEST_POINT_NUM.ToString()));
                }
                //分析批次数，用于清远监测项目费用计算
                if (!String.IsNullOrEmpty(tBaseItemInfo.ANALYSE_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ANALYSE_NUM = '{0}'", tBaseItemInfo.ANALYSE_NUM.ToString()));
                }
                //前处理费用，用于清远监测项目费用计算
                if (!String.IsNullOrEmpty(tBaseItemInfo.PRETREATMENT_FEE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND PRETREATMENT_FEE = '{0}'", tBaseItemInfo.PRETREATMENT_FEE.ToString()));
                }
                //前处理费用，用于清远监测项目费用计算
                if (!String.IsNullOrEmpty(tBaseItemInfo.TEST_ANSY_FEE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEST_ANSY_FEE = '{0}'", tBaseItemInfo.TEST_ANSY_FEE.ToString()));
                }
                //监测单价
                if (!String.IsNullOrEmpty(tBaseItemInfo.CHARGE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND CHARGE = '{0}'", tBaseItemInfo.CHARGE.ToString()));
                }
                //开机费用
                if (!String.IsNullOrEmpty(tBaseItemInfo.TEST_POWER_FEE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TEST_POWER_FEE = '{0}'", tBaseItemInfo.TEST_POWER_FEE.ToString()));
                }
                //实验室认可
                if (!String.IsNullOrEmpty(tBaseItemInfo.LAB_CERTIFICATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND LAB_CERTIFICATE = '{0}'", tBaseItemInfo.LAB_CERTIFICATE.ToString()));
                }
                //计量认可
                if (!String.IsNullOrEmpty(tBaseItemInfo.MEASURE_CERTIFICATE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND MEASURE_CERTIFICATE = '{0}'", tBaseItemInfo.MEASURE_CERTIFICATE.ToString()));
                }
                //平行上限
                if (!String.IsNullOrEmpty(tBaseItemInfo.TWIN_VALUE.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND TWIN_VALUE = '{0}'", tBaseItemInfo.TWIN_VALUE.ToString()));
                }
                //加标下限
                if (!String.IsNullOrEmpty(tBaseItemInfo.ADD_MIN.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_MIN = '{0}'", tBaseItemInfo.ADD_MIN.ToString()));
                }
                //加标上限
                if (!String.IsNullOrEmpty(tBaseItemInfo.ADD_MAX.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ADD_MAX = '{0}'", tBaseItemInfo.ADD_MAX.ToString()));
                }
                //序号
                if (!String.IsNullOrEmpty(tBaseItemInfo.ORDER_NUM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ORDER_NUM = '{0}'", tBaseItemInfo.ORDER_NUM.ToString()));
                }
                //0为在使用、1为停用
                if (!String.IsNullOrEmpty(tBaseItemInfo.IS_DEL.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_DEL = '{0}'", tBaseItemInfo.IS_DEL.ToString()));
                }
                //0为否、1为是 标识为是否为分析类现场监测项目
                if (!String.IsNullOrEmpty(tBaseItemInfo.IS_ANYSCENE_ITEM.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND IS_ANYSCENE_ITEM = '{0}'", tBaseItemInfo.IS_ANYSCENE_ITEM.ToString()));
                }
                //原始记录单使用表ID
                if (!String.IsNullOrEmpty(tBaseItemInfo.ORI_CATALOG_TABLEID.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND ORI_CATALOG_TABLEID = '{0}'", tBaseItemInfo.ORI_CATALOG_TABLEID.ToString()));
                }
                //备注1
                if (!String.IsNullOrEmpty(tBaseItemInfo.REMARK1.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK1 = '{0}'", tBaseItemInfo.REMARK1.ToString()));
                }
                //备注2
                if (!String.IsNullOrEmpty(tBaseItemInfo.REMARK2.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK2 = '{0}'", tBaseItemInfo.REMARK2.ToString()));
                }
                //备注3
                if (!String.IsNullOrEmpty(tBaseItemInfo.REMARK3.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK3 = '{0}'", tBaseItemInfo.REMARK3.ToString()));
                }
                //备注4
                if (!String.IsNullOrEmpty(tBaseItemInfo.REMARK4.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK4 = '{0}'", tBaseItemInfo.REMARK4.ToString()));
                }
                //备注5
                if (!String.IsNullOrEmpty(tBaseItemInfo.REMARK5.ToString().Trim()))
                {
                    strWhereStatement.Append(string.Format(" AND REMARK5 = '{0}'", tBaseItemInfo.REMARK5.ToString()));
                }
            }
            return strWhereStatement.ToString();
        }

        #endregion
    }
}
