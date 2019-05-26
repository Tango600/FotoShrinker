using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;

namespace FotoShrinker
{
    public class Snippets
    {
        [System.Serializable]
        public class Snippet
        {
            public string Now { get; set; } = DateTime.Now.ToString("HH.mm.ss.ms");
            public long Size { get; set; } = 0;
            public int Width { get; set; } = 0;
            public int Height { get; set; } = 0;
            public string Path { get; set; } = "";
            public string Version { get; set; } = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            public string PCName { get; set; } = Environment.MachineName;
            public string UserName { get; set; } = Environment.UserName;
            public bool Resized { get; set; } = false;
        }

        public static string[] GetFields()
        {
            return typeof(Snippet).GetProperties().Select(f => "{Snippet." + f.Name + "}").ToArray();
        }

        public static string FormatWith(string Text, params object[] Objs)
        {
            string Result = Text;
            CultureInfo provider = CultureInfo.CurrentUICulture;  // new CultureInfo("ru-RU");

            int i = 0;
            foreach (object ItemObject in Objs)
            {
                if (ItemObject != null)
                {
                    Type myType = ItemObject.GetType();
                    PropertyInfo[] myField = myType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetField | BindingFlags.GetProperty | BindingFlags.NonPublic);

                    if (myType.BaseType.Name == "Array" || ItemObject.GetType().Name.IndexOf("List") != -1)
                    {
                        foreach (var O in ((IEnumerable<object>)ItemObject).Select(f => (object)f))
                        {
                            Result = FormatWith(Result, O);
                        }
                    }
                    else
                    {
                        bool utilization = false;
                        if (myField.Length > 0)
                        {
                            foreach (PropertyInfo ItemMyField in myField)
                            {
                                object lValue = null;
                                string name = myType.Name + "." + ItemMyField.Name;
                                try
                                {
                                    lValue = ItemMyField.GetValue(ItemObject, null);
                                }
                                catch
                                {
                                    lValue = null;
                                }
                                if (lValue != null)
                                {
                                    string value = "";
                                    if (lValue.GetType() == typeof(decimal))
                                    {
                                        provider.NumberFormat.NumberDecimalSeparator = ".";
                                        value = ((decimal)lValue).ToString("0.00", provider);
                                    }
                                    else
                                    {
                                        value = Convert.ToString(lValue, provider);
                                    }
                                    if (Result.ToLower().IndexOf("{" + name.ToLower() + "}") >= 0)
                                    {
                                        utilization = true;
                                        Result = Regex.Replace(Result, "{" + name.ToLower() + "}", value,
                                            RegexOptions.IgnoreCase);
                                    }
                                    else
                                    {
                                        if (Result.ToLower().IndexOf("{" + ItemMyField.Name.ToLower() + "}") >= 0)
                                        {
                                            utilization = true;
                                            Result = Regex.Replace(Result, "{" + ItemMyField.Name.ToLower() + "}", value,
                                                RegexOptions.IgnoreCase);
                                        }
                                    }
                                }
                            }
                        }

                        try
                        {
                            if (!utilization)
                            {
                                if (Result.IndexOf("<" + i.ToString() + ">") >= 0)
                                {
                                    Result = Regex.Replace(Result, "{" + myType.Name.ToString().ToLower() + "<" + i.ToString() + ">}", Convert.ToString(ItemObject, provider),
                                        RegexOptions.IgnoreCase);
                                }
                                else
                                {
                                    if (Result.IndexOf("{" + i.ToString() + "}") >= 0)
                                    {
                                        Result = Result.Replace("{" + i.ToString() + "}", Convert.ToString(ItemObject, provider));
                                    }
                                    else
                                        Result = Regex.Replace(Result, "{" + myType.Name.ToString().ToLower() + "}",
                                            Convert.ToString(ItemObject, provider), RegexOptions.IgnoreCase);
                                }
                            }
                        }
                        catch
                        {
                            //
                        }
                    }
                }
                i++;
            }
            return Result;
        }
    }
}
