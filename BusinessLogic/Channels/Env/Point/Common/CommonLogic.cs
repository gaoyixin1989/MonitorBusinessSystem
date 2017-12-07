using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using i3.DataAccess.Channels.Env.Point.Common;
using System.Data;

namespace i3.BusinessLogic.Channels.Env.Point.Common
{
    /// <summary>
    /// 功能：公共方法
    /// 创建日期：2013-06-08
    /// 创建人：魏林
    /// </summary>
    public class CommonLogic
    {
        /// <summary>
        /// 批量保存点位监测项目数据
        /// </summary>
        /// <param name="strTableName">点位监测项目表名</param>
        ///<param name="strPoint_ID">点位ID值</param>
        ///<param name="strItemValue">监测项目ID值</param>
        /// <returns></returns>
        public bool SaveItemInfo(string strTableName, string strPoint_ID, string strItemValue, string strSerialId)
        {
            CommonAccess com = new CommonAccess();
            return com.SaveItemInfo(strTableName, strPoint_ID, strItemValue, strSerialId);
        }

        /// <summary>
        /// 获取监测项目信息
        /// </summary>
        /// <param name="strID">监测项目ID</param>
        /// <returns></returns>
        public DataTable getItemInfo(string strID)
        {
            CommonAccess com = new CommonAccess();

            return com.getItemInfo(strID);
        }

        /// <summary>
        /// 通用方法，获取垂线监测项目资料
        /// </summary>
        /// <param name="strTableName">数据表名</param>
        /// <param name="strWhereColumnName">条件字段名称</param>
        /// <param name="strColumnValue">条件字段数据</param>
        /// <returns></returns>
        public DataTable getVerticalItem(string strTableName, string strWhereColumnName, string strColumnValue)
        {
            CommonAccess com = new CommonAccess();
            return com.getVerticalItem(strTableName, strWhereColumnName, strColumnValue);
        }
        /// <summary>
        /// 保存填报数据的通用方法
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="FillTable"></param>
        /// <param name="FillItemTable"></param>
        /// <param name="unSureMark"></param>
        /// <param name="strFillID"></param>
        /// <param name="strSerial"></param>
        /// <param name="strSerialItem"></param>
        /// <returns></returns>
        public bool SaveFillData(DataTable dt, string FillTable, string FillItemTable, string unSureMark, ref string strFillID, string strSerial, string strSerialItem)
        {
            CommonAccess com = new CommonAccess();
            return com.SaveFillData(dt, FillTable, FillItemTable, unSureMark, ref strFillID, strSerial, strSerialItem);
        }

        /// <summary>
        /// 获取环境质量数据填报数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// /// <param name="dtShow">填报主表显示的列表信息 格式：有两列，第一列是字段名，第二列是中文名</param>
        /// <param name="SectionTable">断面表名</param>
        /// <param name="PointTable">测点表名</param>
        /// <param name="ItemTable">测点监测项目表名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="FillISerial">填报监测项表序列类型</param>
        /// <param name="FillSerial">填报表序列类型</param>
        /// <param name="mark">区分点位是两级还是三级结构（"0":两级  "1":三级）</param>
        /// <returns></returns>
        public DataTable GetFillData(string strWhere, DataTable dtShow, string SectionTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string mark)
        {
            CommonAccess com = new CommonAccess();
            return com.GetFillData(strWhere, dtShow, SectionTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial, mark);
        }
        /// <summary>
        /// 获取环境质量补测的数据填报数据
        /// </summary>
        /// <param name="strWhere">条件</param>
        /// <param name="dtShow">填报主表显示的列表信息 格式：有两列，第一列是字段名，第二列是中文名</param>
        /// <param name="FillTable">填报表名</param>
        /// <param name="FillITable">填报监测项表名</param>
        /// <param name="mark">区分点位是两级还是三级结构（"0":两级  "1":三级）</param>
        /// <returns></returns>
        public DataTable GetFillBcData(string strWhere, DataTable dtShow, string FillTable, string FillITable, string mark)
        {
            CommonAccess com = new CommonAccess();
            return com.GetFillBcData(strWhere, dtShow, FillTable, FillITable, mark);
        }
        public DataTable GetFillData_ZZ(string strWhere, DataTable dtShow, string SectionTable, string PointTable, string ItemTable, string FillTable, string FillITable, string FillSerial, string FillISerial, string mark)
        {
            CommonAccess com = new CommonAccess();
            return com.GetFillData_ZZ(strWhere, dtShow, SectionTable, PointTable, ItemTable, FillTable, FillITable, FillSerial, FillISerial, mark);
        }

        /// <summary>
        /// 根据某字段值返回某字段的值
        /// </summary>
        /// <param name="TabelName"></param>
        /// <param name="ColName"></param>
        /// <param name="IDName"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public string getNameByID(string TabelName, string ColName, string IDName, string Value)
        {
            CommonAccess com = new CommonAccess();
            return com.getNameByID(TabelName, ColName, IDName, Value);
        }

        /// <summary>
        /// 根据条件更新数据表
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="strSet"></param>
        /// <param name="Where"></param>
        /// <returns></returns>
        public int UpdateTableByWhere(string TableName, string strSet, string Where)
        {
            CommonAccess com = new CommonAccess();
            return com.UpdateTableByWhere(TableName, strSet, Where);
        }

        #region//填报更新列值
        /// <summary>
        /// 填报更新列值（ljn,2013/6/21）
        /// </summary>
        /// <param name="ColomnName">列名</param>
        /// <param name="ColomnValue">列值</param>
        /// <param name="Fill_ID">填报ID</param>
        /// <param name="StandardID">评价标准ID（用于判断监测项目的评价值）</param>
        /// <returns></returns>
        public bool UpdateCommonFill(string ColomnName, string ColomnValue, string Fill_ID, string StandardID)
        {
            CommonAccess com = new CommonAccess();
            return com.UpdateCommonFill(ColomnName, ColomnValue, Fill_ID, StandardID);
        }
        #endregion

        /// <summary>
        /// 监测项目评价值的更新
        /// </summary>
        /// <param name="FillIDs">填报表ID</param>
        /// <param name="FillTB">填报表名</param>
        /// <param name="FillItemTB">填报监测表名</param>
        /// <param name="PointTB">点位表名</param>
        /// <param name="PointColName">填报表关联点位的字段名</param>
        /// <returns></returns>
        public bool ModifyJUDGE(string FillIDs, string FillTB, string FillItemTB, string PointTB, string PointColName)
        {
            CommonAccess com = new CommonAccess();
            return com.ModifyJUDGE(FillIDs, FillTB, FillItemTB, PointTB, PointColName);
        }
        /// <summary>
        /// 用于点位新增、修改时判断是否存在相同的数据
        /// </summary>
        /// <param name="TableName">点位表名</param>
        /// <param name="Year">年份</param>
        /// <param name="Months">月份</param>
        /// <param name="ColName">点位名称</param>
        /// <param name="ColValue">点位名称值</param>
        /// <param name="ColCode">点位编码</param>
        /// <param name="ColCodeName">点位编码值</param>
        /// <param name="ColID">点位ID</param>
        /// <param name="ColIDValue">点位ID</param>
        /// <param name="flag">监测标识 0：按月监测  1：按季度监测  2：按年监测</param>
        /// <returns></returns>
        public string isExistDatas(string TableName, string Year, string Months, string ColName, string ColValue, string ColCode, string ColCodeName,  string ColID, string ColIDValue,int flag) 
        {
            CommonAccess com = new CommonAccess();
            return com.isExistDatas(TableName, Year, Months, ColName, ColValue, ColCode, ColCodeName, ColID, ColIDValue,flag);
        }

        public string IsExistData(string TableName, string Year, string Months, string ColName, string ColValue, string ColCode, string ColCodeName, string ColID, string ColIDValue, string flag) 
        {
            CommonAccess com = new CommonAccess();
            return com.IsExistData(TableName, Year, Months, ColName, ColValue, ColCode, ColCodeName, ColID, ColIDValue, flag);
        }

        public string isExistRepeat(string TableName, string Year, string Months, string ColName, string ColValue, string ColCode, string ColCodeName, string ColID, string ColIDValue, string TYPE_ID)
        {
            CommonAccess com = new CommonAccess();
            return com.isExistRepeat(TableName, Year, Months, ColName, ColValue, ColCode, ColCodeName, ColID, ColIDValue, TYPE_ID); 
        }
        public string Is_EnterPrise(string head_id, string type,string Type_id)
        {
            CommonAccess com = new CommonAccess();
            return com.Is_EnterPrise(head_id, type, Type_id);
        }
        public string Update_EnterPrise(string type, string Type_id)
        {
            CommonAccess com = new CommonAccess();
            return com.Update_EnterPrise(type, Type_id);
        }
         
         
        /// <summary>
        /// 判断是否存在某年某的点位信息
        /// </summary>
        /// <param name="TableName">表名</param>
        /// <param name="Year">年份</param>
        /// <param name="Month">月份</param>
        /// <returns></returns>
        public bool ExistPointData(string TableName, string Year, string Month)
        {
            CommonAccess com = new CommonAccess();
            return com.ExistPointData(TableName, Year, Month);
        }

        /// <summary>
        /// 点位复制逻辑
        /// </summary>
        /// <param name="TableName">点位主表名</param>
        /// <param name="strSerial">断面、测点 序列号</param>
        /// <param name="strSerialV">垂线 序列号</param>
        /// <param name="strSerialVItem">监测项目 序列号</param>
        /// <param name="Year_From"></param>
        /// <param name="Month_From"></param>
        /// <param name="Year_To"></param>
        /// <param name="Month_To"></param>
        /// <returns></returns>
        public string CopyPointData(string TableName, string strSerial, string strSerialV, string strSerialVItem, string Year_From, string Month_From, string Year_To, string Month_To)
        {
            CommonAccess com = new CommonAccess();
            return com.CopyPointData(TableName, strSerial, strSerialV, strSerialVItem, Year_From, Month_From, Year_To, Month_To);
        }

        public DataTable GetPointFillInfo(string PF_ID, char s)
        {
            CommonAccess com = new CommonAccess();
            return com.GetPointFillInfo(PF_ID, s);
        }

        /// <summary>
        /// 更改数据填报的状态
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="strTable"></param>
        /// <param name="Status"></param>
        /// <returns></returns>
        public int UpdateFillStatus(string ID, string strTable, string Status)
        {
            CommonAccess com = new CommonAccess();
            return com.UpdateFillStatus(ID, strTable, Status);
        }

        /// <summary>
        /// 获取某年月填报数据的监测值
        /// </summary>
        /// <param name="strYear"></param>
        /// <param name="strMonth"></param>
        /// <param name="strTable">填报主表</param>
        /// <param name="strItmeTable">填报项目表</param>
        /// <param name="strPointTable">测点表</param>
        /// <param name="m">1：三级结构  2：二级结构</param>
        /// <returns></returns>
        public DataTable GetFillValue(string strYear, string strMonth, string strTable, string strItmeTable, string strPointTable, string m)
        {
            CommonAccess com = new CommonAccess();
            return com.GetFillValue(strYear, strMonth, strTable, strItmeTable, strPointTable, m);
        }
        /// <summary>
        /// 返回环境质量的监测点位信息
        /// </summary>
        /// <param name="strTableName">点位表表名</param>
        /// <param name="strCode">点位编码字段名</param>
        /// <param name="strName">点位名称字段名</param>
        /// <param name="strPointName">查询的关键字</param>
        /// <returns></returns>
        public DataTable getPointInfo(string strTableName, string strCode, string strName, string strPointName)
        {
            CommonAccess com = new CommonAccess();
            return com.getPointInfo(strTableName, strCode, strName, strPointName);
        }
        /// <summary>
        /// 获取企业的点位信息
        /// </summary>
        /// <param name="strCompanyID"></param>
        /// <returns></returns>
        public DataTable getCompanyPointInfo(string strCompanyID, string strPointName)
        {
            CommonAccess com = new CommonAccess();
            return com.getCompanyPointInfo(strCompanyID, strPointName);
        }

        #region//获取环境质量监测数据展现信息
        public DataTable GetEnvMoniotrData(string strMonitorType, string strPointNames, string strDateStart, string strDateEnd, string strItemIDs)
        {
            CommonAccess com = new CommonAccess();
            return com.GetEnvMoniotrData(strMonitorType, strPointNames, strDateStart, strDateEnd, strItemIDs);
        }
        #endregion

        #region//获取污染源企业监测数据展现信息
        public DataTable GetPollMoniotrData(string strContractType, string strCompanyID, string strMonitorID, string strPointNames, string strDateStart, string strDateEnd, string strItemIDs)
        {
            CommonAccess com = new CommonAccess();
            return com.GetPollMoniotrData(strContractType, strCompanyID, strMonitorID, strPointNames, strDateStart, strDateEnd, strItemIDs);
        }
        #endregion
    }
}
