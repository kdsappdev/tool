﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MDT.Tools.Core.Utils;

namespace MDT.Tools.DB.Common
{
    public class CodeGenHelper
    {
        public CodeGenHelper()
        { }
        private  IbatisConfigHelper ibatisConfigHelper = new IbatisConfigHelper();
        public   void ReadConfig(string path)
        {
            ibatisConfigHelper.ReadConfig(path);
        }

        public static string GetDbProviderString(string dbType)
        {
            string str = "";
            switch (dbType)
            {
                case "Oracle":
                    str = "OracleHelper";
                    break;
                case "Sql Server":
                    str = "SqlHelper";
                    break;
            }
            return str;
        }


        public static string StrFirstAndSecondToLower(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str[0].ToString().ToLower() + str[1].ToString().ToLower() + str.Substring(2, str.Length - 2);
            }
            return str;
        }
        public static string StrFirstToUpperRemoveUnderline(string str)
        {
            string temp = str;
            if (!string.IsNullOrEmpty(str))
            {
                temp = temp.Trim(new[] { '#', ' ', ';', '\r', '\n' });
                string[] temps = temp.ToLower().Split(new char[] { '_' });
                temp = "";
                foreach (string s in temps)
                {
                    temp += StrFirstToUpper(s);
                }
            }
            return temp;
        }
        public static string StrFirstToUpperRemoveFirstUnderline(string str)
        {
            string temp = str;
            if (!string.IsNullOrEmpty(str))
            {
                temp = temp.Trim(new[] { '#', ' ', ';', '\r', '\n' });
                string[] temps = temp.ToLower().Split(new char[] { '_' });
                if (temps.Length > 1)
                {
                    temp = "";
                    for (int i = 1; i < temps.Length; i++)
                    {
                        temp += StrFirstToUpper(temps[i]);
                    }
                }
                else
                {
                    temp = StrFirstToUpper(temp.ToLower());
                }

            }
            return temp;
        }
        public static string StrFirstToUpperAndUnderline(string str)
        {
            string temp = str;
            if (!string.IsNullOrEmpty(str))
            {
                temp = temp.Trim(new[] { '#', ' ', ';', '\r', '\n' });
                string[] temps = temp.ToLower().Split(new char[] { '_' });
                temp = "";
                foreach (string s in temps)
                {
                    temp += StrFirstToUpper(s)+"_";
                }
                temp = temp.TrimEnd('_');
            }
            return temp;
        }
        public static string StrFirstToLowerRemoveUnderline(string str)
        {
            string temp = str;
            if (!string.IsNullOrEmpty(str))
            {
                temp =  temp.Trim(new[] { '#', ' ', ';', '\r', '\n' });
                string[] temps = temp.ToLower().Split(new char[] { '_' });
               
                if (temps.Length > 0)
                {
                    temp = "";
                    temp += StrFirstToLower(temps[0]);
                    for (int i = 1; i < temps.Length; i++)
                    {
                        temp += StrFirstToUpper(temps[i]);
                    }
                    temp = temp.TrimEnd('_');
                }
                
            }
            return temp;
        }
        public static string StrFieldWith_(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
               
                str ="_"+ StrFirstToLowerRemoveUnderline(str);
            }
            return str;
        }
        public static string StrProperty(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = StrFirstToUpperRemoveUnderline(str);
            }
            return str;
        }
        public static string StrFirstToUpper(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {               
                str = str[0].ToString().ToUpper() + str.Substring(1, str.Length - 1);
            }
            return str;
        }
        
        public static string StrFirstToLower(string str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                str = str[0].ToString().ToLower() + str.Substring(1, str.Length - 1);
            }
            return str;
        }
        public const string Ibatis = "ibatis";
        
        public   string GetClassName(string tableName,string codeRule)
        {
            string className = (codeRule == Ibatis ? !string.IsNullOrEmpty(ibatisConfigHelper.GetClassName(tableName))?ibatisConfigHelper.GetClassName(tableName):StrFirstToUpperRemoveUnderline(tableName) : StrFirstToUpperRemoveUnderline(tableName));
            return className;
        }
        public string GetClassName2(string tableName, string codeRule)
        {
             
            string className = (codeRule == Ibatis ? !string.IsNullOrEmpty(ibatisConfigHelper.GetClassName(tableName)) ? ibatisConfigHelper.GetClassName(tableName) : StrFirstToUpperRemoveFirstUnderline(tableName) : StrFirstToUpperRemoveFirstUnderline(tableName));
           
            return className;
        }
        public static bool IsNullOrEmpty(string str)
        {
            return string.IsNullOrEmpty(str);
        }

        public static string GetDefaultValueByDataType(string str, string defaultValue)
        {
            string temp = "\"\"";
            if (!string.IsNullOrEmpty(str))
            {
                if (!string.IsNullOrEmpty(defaultValue))
                defaultValue=defaultValue.Trim(new char[]{'\'',' ','\r','\n','\t'});
           
                switch (str)
                {
                    case "string":
                        temp = !string.IsNullOrEmpty(defaultValue) && !defaultValue.Equals("EMPTY_CLOB()")
                                   ? "\"" + defaultValue + "\""
                                   : "\"\"";
                        break;
                    case "decimal":
                    case "decimal?":
                        temp = !string.IsNullOrEmpty(defaultValue) ? defaultValue + "M" : "0M";
                        break;
                    case "int?":
                    case "int":
                        temp = !string.IsNullOrEmpty(defaultValue) ? defaultValue : "0";
                        break;
                    case "long":
                    case "long?":
                        temp = !string.IsNullOrEmpty(defaultValue) ? defaultValue : "0";
                        break;
                    case "DateTime?":
                    case "DateTime":
                        temp = "DateTime.Now";                        
                        break;
                    case "BigDecimal":
                        temp = !string.IsNullOrEmpty(defaultValue) ? "new BigDecimal(" + defaultValue + ")" : "new BigDecimal(0)";
                        break;
                    case "Timestamp":
                        temp = " new Timestamp(System.currentTimeMillis())";
                        break;
                    case "Date":
                        temp = " new Date()";
                        break;
                    default:
                        temp = "\"\"";
                        break;
                }
            }
            return temp;
        }
    }

}
