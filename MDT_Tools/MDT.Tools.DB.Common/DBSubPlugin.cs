﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using MDT.Tools.Core.Plugin;

namespace MDT.Tools.DB.Common
{
    public abstract class DBSubPlugin : AbstractPlugin
    {

        #region load,unload
        protected override void load()
        {
            subscribe(PluginShareHelper.DBPlugin_BroadCast_CheckTableNumberIsGreaterThan0, this);
            _dbContextMenuStrip = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.CmcSubPlugin) as ContextMenuStrip;
            AddContextMenu();
        }
        public override void BeforeTerminating()
        {
            unsubscribe(PluginShareHelper.DBPlugin_BroadCast_CheckTableNumberIsGreaterThan0, this);
             
        }
        #endregion

        #region onNotify
        public override void onNotify(string name, object o)
        {
            base.onNotify(name, o);
            if (PluginShareHelper.DBPlugin_BroadCast_CheckTableNumberIsGreaterThan0.Equals(name) && o.GetType().IsValueType)
            {
                var flag = (bool)o;
                SetEnable(flag);
            }
        }
        protected delegate void SimpleBool(bool flag);
        protected void SetEnable(bool flag)
        {
            if (Application.MainMenu.InvokeRequired)
            {
                var s = new SimpleBool(SetEnable);
                Application.MainMenu.Invoke(s, new object[] { flag });
            }
            else
            {
                _tsiGen.Enabled = flag;
				_dbContextMenuStrip.Enabled = flag;
            }
        }
        #endregion

       

        #region 增加上下文菜单
        protected readonly ToolStripMenuItem _tsiGen = new ToolStripMenuItem();
        protected ContextMenuStrip _dbContextMenuStrip=null;
        protected delegate void Simple();
        protected virtual void AddContextMenu()
        {
            if (Application.MainMenu.InvokeRequired)
            {
                var s = new Simple(AddContextMenu);
                Application.MainMenu.Invoke(s, null);
            }
            else
            {
                 if (_dbContextMenuStrip != null)
                 {
                     _tsiGen.Enabled = false;
                     _tsiGen.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                     _dbContextMenuStrip.Items.Add(_tsiGen);
                 }
            }
        }
        #endregion

        public virtual void process(DataRow[] drTable, AbstractHandler handler)
        {
            getDBShare(handler);
            handler.process(drTable, handler.dsTableColumn, handler.dsTablePrimaryKey);
        }

        protected virtual void getDBShare(AbstractHandler handler)
        {
            var dbName = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_DBCurrentDBName) as string;
            var dbType = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_DBCurrentDBType) as string;
            var dbConnectionString = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_DBCurrentDBConnectionString) as string;
            var dsTable = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_DBCurrentDBAllTable) as DataSet;
            var dsTableColumn = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_DBCurrentDBAllTablesColumns) as DataSet;
            var dsTablePrimaryKey = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_DBCurrentDBTablesPrimaryKeys) as DataSet;

            var dBtable = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_DBtable) as string;
            var dBtablesColumns = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_DBtablesColumns) as string;

            var dBviews = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_DBviews) as string;
            var dBtablesPrimaryKeys = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_DBtablesPrimaryKeys) as string;
            var tsslMessage = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_tsslMessage) as ToolStripStatusLabel;
            var tspbLoadDBProgress = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_tspbLoadDBProgress) as ToolStripProgressBar;
            var originalEncoding = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_OriginalEncoding) as string;
            var targetEncoding = getObject(PluginShareHelper.DBPluginKey, PluginShareHelper.DBPlugin_TargetEncoding) as string;

            handler.tsiGen = _tsiGen;
            handler.DBtable = dBtable;
            handler.DBtablesColumns = dBtablesColumns;
            handler.DBviews = dBviews;
            handler.DBtablesPrimaryKeys = dBtablesPrimaryKeys;
            handler.dbName = dbName;
            handler.dbType = dbType;
            handler.dbConnectionString = dbConnectionString;
            handler.tsslMessage = tsslMessage;
            handler.tspbLoadDBProgress = tspbLoadDBProgress;
            handler.MainContextMenu = _dbContextMenuStrip;
            handler.Panel = Application.Panel;
            handler.dsTable = dsTable;
            handler.dsTableColumn = dsTableColumn;
            handler.dsTablePrimaryKey = dsTablePrimaryKey;
            handler.PluginName = PluginName + "(V" + Version + ")";
            if (!string.IsNullOrEmpty(originalEncoding) && !string.IsNullOrEmpty(targetEncoding))
            {
                handler.OriginalEncoding = Encoding.GetEncoding(originalEncoding);
                handler.TargetEncoding = Encoding.GetEncoding(targetEncoding);
            }
          
        }
    }
}
