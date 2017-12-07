using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Reflection;


namespace i3.Core.View
{
    /// <summary>
    /// ����������ʵ�ֿؼ������֮����Զ���
    /// ����ʱ�䣺2011-4-6 20:50:43
    /// ������  ���¹�ӭ
    /// </summary>
    public class AutoBinder
    {
        /// <summary>
        /// ������󶨵��ؼ�
        /// </summary>
        /// <param name="obj">����</param>
        /// <param name="container">�ؼ�����(page�������Զ���ؼ�)</param>
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
        /// ���ÿؼ�����
        /// </summary>
        /// <param name="obj">����</param>
        /// <param name="objProperty">��������</param>
        /// <param name="control">�ؼ�</param>
        /// <param name="controlPropertiesArray">�ؼ����Լ�</param>
        /// <param name="propertyName">��������</param>
        /// <param name="type">��������</param>
        /// <returns>�Ƿ�ɹ�</returns>
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
        /// ���ؼ���ֵ���������
        /// </summary>
        /// <param name="obj">����</param>
        /// <param name="container">�ؼ�����(page�������Զ���ؼ�)</param>
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
        /// ��ÿؼ�����
        /// </summary>
        /// <param name="obj">����</param>
        /// <param name="objProperty">��������</param>
        /// <param name="control">�ؼ�</param>
        /// <param name="controlPropertiesArray">�ؼ����Լ�</param>
        /// <param name="propertyName">��������</param>
        /// <param name="type">��������</param>
        /// <returns>�Ƿ�ƥ��</returns>
        private static bool FindAndGetControlProperty(object obj, PropertyInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type)
        {
            // �������ؼ������н��е���
            foreach (PropertyInfo controlProperty in controlPropertiesArray)
            {
                // ���ƥ������ƺ�����
                if (controlProperty.Name == "Text" && controlProperty.PropertyType == typeof(String))
                {
                    // ���ؼ�����������Ϊҵ���������ֵ
                    try
                    {
                        objProperty.SetValue(obj, Convert.ChangeType(controlProperty.GetValue(control, null), objProperty.PropertyType), null);
                        return true;
                    }
                    catch
                    {
                        // �޷������Դ���ؼ�������ת��ΪobjProperty.PropertyType
                        return false;
                    }
                }
            }
            return true;
        }

        /// <summary>
        /// ���ؼ���ֵ���������
        /// </summary>
        /// <param name="obj">����</param>
        /// <param name="container">�ؼ�����(page�������Զ���ؼ�)</param>
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
                        //����ǿգ������ָ����ֵ
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
        /// ��ÿؼ�����
        /// </summary>
        /// <param name="obj">����</param>
        /// <param name="objProperty">��������</param>
        /// <param name="control">�ؼ�</param>
        /// <param name="controlPropertiesArray">�ؼ����Լ�</param>
        /// <param name="propertyName">��������</param>
        /// <param name="type">��������</param>
        /// <returns>�Ƿ�ƥ��</returns>
        private static bool FindAndGetControlPropertyWithNoNullValues(object obj, PropertyInfo objProperty, Control control, PropertyInfo[] controlPropertiesArray, string propertyName, Type type,string flag)
        {
            // �������ؼ������н��е���
            foreach (PropertyInfo controlProperty in controlPropertiesArray)
            {
                // ���ƥ������ƺ�����
                if (controlProperty.Name == "Text" && controlProperty.PropertyType == typeof(String))
                {
                    // ���ؼ�����������Ϊҵ���������ֵ
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
                        // �޷������Դ���ؼ�������ת��ΪobjProperty.PropertyType
                        return false;
                    }
                }
            }
            return true;
        }

    }
}

