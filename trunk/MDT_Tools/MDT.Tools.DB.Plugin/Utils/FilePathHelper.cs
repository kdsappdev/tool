﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
using MDT.Tools.Core.Utils;

namespace MDT.Tools.DB.Plugin.Utils
{
    internal class FilePathHelper
    {
        private FilePathHelper()
        { }
        public static readonly string SystemConfig = Application.StartupPath + "\\MDT.Tools.DB.Plugin\\config.ini";
        public static readonly string SaveDBDataPath = Application.StartupPath + "\\MDT.Tools.DB.Plugin\\data\\";


        public static void WriteXml(DataSet ds)
        {

            if (ds != null)
            {
                try
                {
                    foreach (DataTable dt in ds.Tables)
                    {
                        string path = FilePathHelper.SaveDBDataPath + dt.TableName + ".data";
                        FileHelper.CreateDirectory(path);
                        dt.WriteXml(path, XmlWriteMode.WriteSchema);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
        public static DataSet ReadXml(DataSet ds, string dbConfigName, string dataType)
        {
            if (ds == null)
            {
                ds = new DataSet();
            }
            bool status = isExist(dbConfigName, dataType);
            if (status)
            {
                try
                {
                    DataTable dt = new DataTable();
                    string path = FilePathHelper.SaveDBDataPath + dbConfigName + dataType + ".data";
                    FileHelper.CreateDirectory(path);
                    dt.ReadXml(path);
                    ds.Tables.Add(dt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
            return ds;
        }
        public static bool isExist(string dbConfigName, string dataType)
        {
            bool status = false;
            if (!string.IsNullOrEmpty(dbConfigName) && !string.IsNullOrEmpty(dataType))
            {
                string path = FilePathHelper.SaveDBDataPath + dbConfigName + dataType + ".data";
                FileHelper.CreateDirectory(path);
                if (File.Exists(path))
                {
                    status = true;
                }
            }
            return status;
        }
    }
}
