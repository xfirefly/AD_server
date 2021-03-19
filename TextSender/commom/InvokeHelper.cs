/*******************************************************************************
 * InvokeHelper.cs
 * A thread-safe control invoker helper class.
 * -----------------------------------------------------------------------------
 * Project:Conmajia.Controls
 * Author:Conmajia
 * Url:conmajia@gmail.com
 * History:
 *      4th Aug., 2012
 *      Added support for "Non-control" controls (such as ToolStripItem).
 *      
 *      4th Aug., 2012
 *      Initiated.
 ******************************************************************************/
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// http://blog.csdn.net/conmajia/article/details/7831251
/// </summary>
namespace Common
{
    /// <summary>
    /// A thread-safe control invoker helper class.
    /// </summary>
    public class InvokeHelper
    {
        #region delegates
        private delegate object MethodInvoker(Control control, string methodName, params object[] args);

        private delegate object PropertyGetInvoker(Control control, object noncontrol, string propertyName);
        private delegate void PropertySetInvoker(Control control, object noncontrol, string propertyName, object value);
        #endregion

        #region static methods
        // helpers
        private static PropertyInfo GetPropertyInfo(Control control, object noncontrol, string propertyName)
        {
            if (control != null && !string.IsNullOrEmpty(propertyName))
            {
                PropertyInfo pi = null;
                Type t = null;

                if (noncontrol != null)
                    t = noncontrol.GetType();
                else
                    t = control.GetType();

                pi = t.GetProperty(propertyName);

                if (pi == null)
                    throw new InvalidOperationException(
                        string.Format(
                        "Can't find property {0} in {1}.",
                        propertyName,
                        t.ToString()
                        ));

                return pi;
            }
            else
                throw new ArgumentNullException("Invalid argument.");
        }

        // outlines
        public static object Invoke(Control control, string methodName, params object[] args)
        {
            if (control != null && !string.IsNullOrEmpty(methodName))
                if (control.InvokeRequired)
                    return control.Invoke(
                        new MethodInvoker(Invoke),
                        control,
                        methodName,
                        args
                        );
                else
                {
                    MethodInfo mi = null;

                    if (args != null && args.Length > 0)
                    {
                        Type[] types = new Type[args.Length];
                        for (int i = 0; i < args.Length; i++)
                        {
                            if (args[i] != null)
                                types[i] = args[i].GetType();
                        }

                        mi = control.GetType().GetMethod(methodName, types);
                    }
                    else
                        mi = control.GetType().GetMethod(methodName);

                    // check method info you get
                    if (mi != null)
                        return mi.Invoke(control, args);
                    else
                        throw new InvalidOperationException("Invalid method.");
                }
            else
                throw new ArgumentNullException("Invalid argument.");
        }

        public static object Get(Control control, string propertyName)
        {
            return Get(control, null, propertyName);
        }
        public static object Get(Control control, object noncontrol, string propertyName)
        {
            if (control != null && !string.IsNullOrEmpty(propertyName))
                if (control.InvokeRequired)
                    return control.Invoke(new PropertyGetInvoker(Get),
                        control,
                        noncontrol,
                        propertyName
                        );
                else
                {
                    PropertyInfo pi = GetPropertyInfo(control, noncontrol, propertyName);
                    object invokee = (noncontrol == null) ? control : noncontrol;

                    if (pi != null)
                        if (pi.CanRead)
                            return pi.GetValue(invokee, null);
                        else
                            throw new FieldAccessException(
                                string.Format(
                                "{0}.{1} is a write-only property.",
                                invokee.GetType().ToString(),
                                propertyName
                                ));

                    return null;
                }
            else
                throw new ArgumentNullException("Invalid argument.");
        }

        public static void Set(Control control, string propertyName, object value)
        {
            Set(control, null, propertyName, value);
        }
        public static void Set(Control control, object noncontrol, string propertyName, object value)
        {
            if (control != null && !string.IsNullOrEmpty(propertyName))
                if (control.InvokeRequired)
                    control.Invoke(new PropertySetInvoker(Set),
                        control,
                        noncontrol,
                        propertyName,
                        value
                        );
                else
                {
                    PropertyInfo pi = GetPropertyInfo(control, noncontrol, propertyName);
                    object invokee = (noncontrol == null) ? control : noncontrol;

                    if (pi != null)
                        if (pi.CanWrite)
                            pi.SetValue(invokee, value, null);
                        else
                            throw new FieldAccessException(
                                string.Format(
                                "{0}.{1} is a read-only property.",
                                invokee.GetType().ToString(),
                                propertyName
                                ));
                }
            else
                throw new ArgumentNullException("Invalid argument.");
        }
        #endregion
    }
}