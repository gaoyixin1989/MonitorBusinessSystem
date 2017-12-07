using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using i3.DataAccess.Channels.Env.Import;
using NPOI.HSSF;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.POIFS;
using NPOI.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;

namespace i3.BusinessLogic.Channels.Env.Import
{
    public class ImportExcelLogic : LogicBase
    {
        ImportExcel access = new ImportExcel();
        public ImportExcelLogic()
        {
            access = new ImportExcel();
        }
        /// <summary>
        /// 将Excel表导入数据库中
        /// </summary>
        /// <param name="strXmlUrl">xml模板位置</param>
        /// <param name="sheet">EXCEL工作薄NPOI对象</param>
        /// <returns></returns>
        public bool importExcel(string strXmlUrl, ISheet sheet)
        {
            return access.importExcel(strXmlUrl, sheet);
        } 
        /// <summary>
        /// 将Excel表导入数据库中，只能支持按年度、月份、日期、小时导入【支持导入噪声、降尘、空气类】
        /// </summary>
        /// <param name="strXmlUrl"></param>
        /// <param name="sheet"></param>
        /// <returns></returns>
        public bool importExcelSpecial(string strXmlUrl, ISheet sheet)
        {
            return access.importExcelSpecial(strXmlUrl, sheet);
        }
        public bool ExcelSpecial(string strXmlUrl, ISheet sheet)
        {
            return access.ExcelSpecial(strXmlUrl, sheet);
        }
        public bool ExcelSpecialAir(string strXmlUrl, ISheet sheet)
        {
            return access.ExcelSpecialAir(strXmlUrl, sheet);
        } 
        /// <summary>
        /// 获取需要导出的工作簿对象
        /// </summary>
        /// <param name="strXmlUrl">配置文件路径</param>
        /// <param name="strExcelTempleUrl">需要导出的Excel模板路径</param>
        /// <param name="strSheetName">工作薄名称</param>
        /// <param name="strYear">年份</param>
        /// <param name="strMonth">月份</param>
        /// <returns></returns>
        public ISheet GetExportExcelSheet(string strXmlUrl, string strExcelTempleUrl, string strSheetName, string strYear, string strMonth)
        {
            return access.GetExportExcelSheet(strXmlUrl, strExcelTempleUrl, strSheetName, strYear, strMonth);
        }
         /// <summary>
        /// 获取需要导出的工作簿对象【支持导入噪声、降尘、空气类】
        /// </summary>
        /// <param name="strXmlUrl">配置文件路径</param>
        /// <param name="strExcelTempleUrl">需要导出的Excel模板路径</param>
        /// <param name="strSheetName">工作薄名称</param>
        /// <param name="strYear">年份</param>
        /// <param name="strMonth">月份</param>
        /// <returns></returns>
        public ISheet GetExportExcelSheetSpecial(string strXmlUrl, string strExcelTempleUrl, string strSheetName, string strYear, string strMonth)
        {
            return access.GetExportExcelSheetSpecial(strXmlUrl, strExcelTempleUrl, strSheetName, strYear, strMonth);
        }
        /// <summary>
        /// 合法性验证
        /// </summary>
        /// <returns>是否成功</returns>
        public override bool Validate()
        {
            return true;
        }
    }
}
