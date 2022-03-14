using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinuxLog
{
    public class ArgumentParser
    {
        #region Parser Methods

        //Method which will parse the string input and return a hashtable
        public static Hashtable Parse(String[] args)
        {
            Hashtable argTable = new Hashtable();
            try
            {
                string[] keyvalue;
                string value;

                if ((null == args) || args.Length == 0)
                {
                    AddKeyValuePair(argTable, "Invalid", "true");
                    return argTable;
                }


                foreach (string s in args)
                {

                    if ((s.Contains("=") && !s.Contains("?")))
                    {
                        // Strip off "/"
                        string key = s.Substring(1, s.Length - 1);
                        keyvalue = key.Split('=');
                        key = keyvalue[0].ToString();
                        value = keyvalue[1].ToString();

                        if (value == null || value.Trim() == string.Empty)
                        {
                            AddKeyValuePair(argTable, "Invalid", "true");
                            return argTable;
                        }

                        AddKeyValuePair(argTable, key, value);

                    }
                    else
                    {
                        if (s.Contains("?"))
                        {
                            AddKeyValuePair(argTable, "?", "true");
                        }
                        else if (s.Contains("/AUTO") || s.Contains("-AUTO"))
                        {
                            AddKeyValuePair(argTable, "AUTO", "true");
                        }
                        else
                        {
                            // otherwise this is invalid value
                            AddKeyValuePair(argTable, "Invalid", "true");
                        }
                    }
                }
                // return the hashtable with the command line arguments in it.
                return argTable;
            }
            catch (ArgumentOutOfRangeException outOfRangeEx)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Arguments out of range, please check error log" + outOfRangeEx.Message + Environment.NewLine + outOfRangeEx.StackTrace);
                System.Console.ResetColor();
                return argTable;
            }
            catch (Exception ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                System.Console.ResetColor();
                return argTable;
            }
        }

        //Add the arguments in the form of a keyvalue pair to Hashtable
        public static void AddKeyValuePair(Hashtable argTable, string key, string value)
        {
            try
            {
                if (!argTable.ContainsKey(key.ToUpper()))
                {
                    // add this to table
                    argTable.Add(key.ToUpper(), value);
                }
                else
                {
                    //substitute the value with the latest one
                    argTable[key.ToUpper()] = value;
                }
            }
            catch (ArgumentException argEx)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(argEx.Message + Environment.NewLine + argEx.StackTrace);
                System.Console.ResetColor();
            }
            catch (Exception ex)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(ex.Message + Environment.NewLine + ex.StackTrace);
                System.Console.ResetColor();
            }
        }
        #endregion
    }
}
