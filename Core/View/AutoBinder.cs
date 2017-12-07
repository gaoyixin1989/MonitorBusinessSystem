using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Reflection;


namespace i3.Core.View
{
    /// <summary>
    /// 功能描述：实现控件与对象之间的自动绑定
    /// 创建时间：2011-4-6 20:50:43
    /// 创建人  ：陈国迎
    /// </summary>
    public class AutoBinder
    {
        /// <summary>
        /// 将对象绑定到控件
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="container">控件容器(page，或者自定义控件)</param>
        public static bool BindObjectToControls(object obj, Control container)
        {
            if (obj == null)
            {
                return false;
            }

            Type objType                      = obj.GetType();
            PropertyInfo[] objPropertiesArray = objType.GetProperties();

            foreach (PropertyInfo objProperty in objPropertiesArray)
            {
                Control control = container.FindControl(objProperty.Name);

                if (control == null)
                {
                    continue;
                }

                if (control is ListControl)
                {
                    ListControl listControl = (ListControl)control;
                    string propertyValue    = objProperty.GetValue(obj, null).ToString();
                    ListItem listItem       = listControl.Items.FindByValue(propertyValue);
                    
                    if (listItem != null)
                    {
                        listControl.SelectedIndex = listControl.Items.IndexOf(listItem);
                    }

                }
                else
                {
                    Type controlType                      = control.GetType();
                    PropertyInfo[] controlPropertiesArray = controlType.GetProperties();

                    bool success = false;
                    success      = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Checked", typeof(bool));

                    if (!success)
                    {
                        success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "SelectedDate", typeof(DateTime));
                    }

                    if (!success)
                    {
                        success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Value", typeof(String));
                    }

                    if (!success)
                    {
                        success = FindAndSetControlProperty(obj, objProperty, control, controlPropertiesArray, "Text", typeof(String));
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// 设置控件属性
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="objProperty">对象属性</param>
        /// <param name="control">控件</param>
        /// <param name="controlPropertiesArray">控件属性集</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="type">对象类型</param>
        /// <returns>是否成功</returns>
        private static bool FindAndSetControlProperty(object obj, PropertyInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type)
        {
            foreach (PropertyInfo controlProperty in controlPropertiesArray)
            {
                if (controlProperty.Name == propertyName && controlProperty.PropertyType == type)
                {
                    controlProperty.SetValue(control, Convert.ChangeType(objProperty.GetValue(obj, null), type), null);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 将控件的值填充至对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="container">控件容器(page，或者自定义控件)</param>
        public static object BindControlsToObject(object obj, Control container)
        {
            if (obj == null)
            {
                return null;
            }

            Type objType                      = obj.GetType();
            PropertyInfo[] objPropertiesArray = objType.GetProperties();

            foreach (PropertyInfo objProperty in objPropertiesArray)
            {
                Control control = container.FindControl(objProperty.Name);

                if (control == null)
                {
                    continue;
                }

                if (control is ListControl)
                {
                    ListControl listControl = (ListControl)control;
                    if (listControl.SelectedItem != null)
                    {
                        objProperty.SetValue(obj, Convert.ChangeType(listControl.SelectedItem.Value, objProperty.PropertyType), null);
                    }
                }
                else
                {
                    Type controlType                      = control.GetType();
                    PropertyInfo[] controlPropertiesArray = controlType.GetProperties();

                    bool success = false;
                    success      = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Checked", typeof(bool));

                    if (!success)
                    {
                        success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "SelectedDate", typeof(DateTime));
                    }

                    if (!success)
                    {
                        success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Value", typeof(String));
                    }

                    if (!success)
                    {
                        success = FindAndGetControlProperty(obj, objProperty, control, controlPropertiesArray, "Text", typeof(String));
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// 获得控件属性
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="objProperty">对象属性</param>
        /// <param name="control">控件</param>
        /// <param name="controlPropertiesArray">控件属性集</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="type">对象类型</param>
        /// <returns>是否匹配</returns>
        private static bool FindAndGetControlProperty(object obj, PropertyInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type)
        {
            // 在整个控件属性中进行迭代
            foreach (PropertyInfo controlProperty in controlPropertiesArray)
            {
                // 检查匹配的名称和类型
                if (controlProperty.Name == "Text" && controlProperty.PropertyType == typeof(String))
                {
                    // 将控件的属性设置为业务对象属性值
                    try
                    {
                        objProperty.SetValue(obj, Convert.ChangeType(controlProperty.GetValue(control, null), objProperty.PropertyType), null);
                        return true;
                    }
                    catch
                    {
                        // 无法将来自窗体控件的数据转换为objProperty.PropertyType
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// 将控件的值填充至对象
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="container">控件容器(page，或者自定义控件)</param>
        public static object BindControlsToObjectWithNoNullValues(object obj, Control container,string flag)
        {
            if (obj == null)
            {
                return null;
            }

            Type objType = obj.GetType();
            PropertyInfo[] objPropertiesArray = objType.GetProperties();

            foreach (PropertyInfo objProperty in objPropertiesArray)
            {
                Control control = container.FindControl(objProperty.Name);

                if (control == null)
                {
                    continue;
                }

                if (control is ListControl)
                {
                    ListControl listControl = (ListControl)control;
                    if (listControl.SelectedItem != null)
                    {
                        //如果非空，则填充指定的值
                        if (!String.IsNullOrEmpty(listControl.SelectedItem.Value))
                        {
                            objProperty.SetValue(obj, Convert.ChangeType(listControl.SelectedItem.Value, objProperty.PropertyType), null);
                        }
                        else
                        {
                            objProperty.SetValue(obj, Convert.ChangeType(flag, objProperty.PropertyType), null);
                        }
                    }
                }
                else
                {
                    Type controlType = control.GetType();
                    PropertyInfo[] controlPropertiesArray = controlType.GetProperties();

                    bool success = false;
                    success = FindAndGetControlPropertyWithNoNullValues(obj, objProperty, control, controlPropertiesArray, "Checked", typeof(bool), flag);

                    if (!success)
                    {
                        success = FindAndGetControlPropertyWithNoNullValues(obj, objProperty, control, controlPropertiesArray, "SelectedDate", typeof(DateTime), flag);
                    }

                    if (!success)
                    {
                        success = FindAndGetControlPropertyWithNoNullValues(obj, objProperty, control, controlPropertiesArray, "Value", typeof(String), flag);
                    }

                    if (!success)
                    {
                        success = FindAndGetControlPropertyWithNoNullValues(obj, objProperty, control, controlPropertiesArray, "Text", typeof(String), flag);
                    }
                }
            }

            return obj;
        }

        /// <summary>
        /// 获得控件属性
        /// </summary>
        /// <param name="obj">对象</param>
        /// <param name="objProperty">对象属性</param>
        /// <param name="control">控件</param>
        /// <param name="controlPropertiesArray">控件属性集</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="type">对象类型</param>
        /// <returns>是否匹配</returns>
        private static bool FindAndGetControlPropertyWithNoNullValues(object obj, PropertyInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type,string flag)
        {
            // 在整个控件属性中进行迭代
            foreach (PropertyInfo controlProperty in controlPropertiesArray)
            {
                // 检查匹配的名称和类型
                if (controlProperty.Name == "Text" && controlProperty.PropertyType == typeof(String))
                {
                    // 将控件的属性设置为业务对象属性值
                    try
                    {
                        if(!String.IsNullOrEmpty(controlProperty.GetValue(control, null).ToString()))
                        {
                            objProperty.SetValue(obj, Convert.ChangeType(controlProperty.GetValue(control, null), objProperty.PropertyType), null);
                        }
                        else
                        {
                            objProperty.SetValue(obj, Convert.ChangeType(flag, objProperty.PropertyType), null);
                        }
                        return true;
                    }
                    catch
                    {
                        // 无法将来自窗体控件的数据转换为objProperty.PropertyType
                        return false;
                    }
                }
            }
            return true;
        }

    }
}

