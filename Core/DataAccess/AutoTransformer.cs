using System;
using System.Collections;
using System.Text;
using System.Data;
using System.Reflection;
using System.Collections.Generic;

namespace i3.Core.DataAccess
{
    /// <summary>
    /// 功能描述：DataTable和Object的自动转化
    /// 创建日期：2011-4-6 20:39:17
    /// 创建人  ：陈国迎
    /// </summary>
    public class AutoTransformer
    {
        /// <summary>
        /// 将DataTable转化为指定对象（取第一行数据)
        /// </summary>
        /// <param name="objTable">DataTable</param>
        /// <param name="obj">对象</param>
        /// <returns>转化后的对象</returns>
        public T ConvertTableToOjbect<T>(DataTable objTable, T obj)
        {
            if (objTable.Rows.Count > 0)
            {
                Type objType = obj.GetType();
                PropertyInfo[] propertyList = objType.GetProperties();

                foreach (PropertyInfo property in propertyList)
                {

                    foreach (DataColumn column in objTable.Columns)
                    {
                        if (column.ColumnName.ToUpper() == property.Name.ToUpper() && !objTable.Rows[0][column.ColumnName].GetType().FullName.Equals("System.DBNull"))
                        {
                            //目前只支持四种数据类型String、Byte[]、DataTime、Int32
                            switch (column.DataType.Name)
                            {
                                case "Int32":
                                    property.SetValue(obj, objTable.Rows[0][column.ColumnName], null);
                                    break;
                                case "DateTime":
                                    property.SetValue(obj, GetStringFromObject(objTable.Rows[0][column.ColumnName]), null);
                                    break;
                                case "Byte[]":
                                    //property.SetValue(obj, objTable.Rows[0][column.ColumnName].ToString().Trim(), null);
                                    break;
                                case "String":
                                    property.SetValue(obj, objTable.Rows[0][column.ColumnName].ToString().Trim(), null);
                                    break;
                                default:
                                    property.SetValue(obj, objTable.Rows[0][column.ColumnName].ToString().Trim(), null);
                                    break;
                            }
                            break;
                        }
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// 将DataTable转化成对象列表（取所有数据)
        /// </summary>
        /// <param name="objTable">DataTable</param>
        /// <param name="obj">对象</param>
        /// <returns>转化后的对象列表</returns>
        public List<T> ConvertTableToOjbectList<T>(DataTable objTable, T obj)
        {
            List<T> objectsList = new List<T>();

            //开关，指示是否添加该对象
            bool bolAdd = false;

            if (objTable.Rows.Count > 0)
            {
                Type objType = obj.GetType();
                PropertyInfo[] propertyList = objType.GetProperties();

                for (int i = 0; i < objTable.Rows.Count; i++)
                {
                    //对象属指针变量，创建新对象
                    obj = (T)Activator.CreateInstance(objType);

                    foreach (PropertyInfo property in propertyList)
                    {
                        foreach (DataColumn column in objTable.Columns)
                        {
                            if (column.ColumnName.ToUpper() == property.Name.ToUpper() && !objTable.Rows[i][column.ColumnName].GetType().FullName.Equals("System.DBNull"))
                            {
                                bolAdd = true;
                                property.SetValue(obj, GetStringFromObject(objTable.Rows[i][column.ColumnName]), null);
                            }
                        }
                    }

                    if (bolAdd)
                    {
                        objectsList.Add(obj);
                    }
                }
            }

            return objectsList;
        }

        /// <summary>
        /// 判断是否是Null或者空格
        /// 有三种情况，Null，空格+字符，只有空格
        /// </summary>
        /// <param name="objColumnData"></param>
        /// <returns></returns>
        private string GetStringFromObject(object objColumnData)
        {
            if (null == objColumnData || string.IsNullOrEmpty(objColumnData.ToString()))
                return "";
            else
                return objColumnData.ToString();
        }
    }

}
