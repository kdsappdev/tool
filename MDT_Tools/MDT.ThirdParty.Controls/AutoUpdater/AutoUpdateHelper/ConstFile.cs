﻿/*****************************************************************
 * Copyright (C) Knights Warrior Corporation. All rights reserved.
 * 
 * Author:   圣殿骑士（Knights Warrior） 
 * Email:    KnightsWarrior@msn.com
 * Website:  http://www.cnblogs.com/KnightsWarrior/       http://knightswarrior.blog.51cto.com/
 * Create Date:  5/8/2010 
 * Usage:
 *
 * RevisionHistory
 * Date         Author               Description
 * 
*****************************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KnightsWarriorAutoupdater
{
    public class ConstFile
    {
        public const string TEMPFOLDERNAME = "temp";
        public const string CONFIGFILEKEY = "config_";
        public const string FILENAME = "control\\AutoUpdater.config";
        public const string ROOLBACKFILE = "MDT Smart Kit.exe";
        public const string MESSAGETITLE = "AutoUpdate Program";
        public const string CANCELORNOT = "MDT Smart Kit Update is in progress. Do you really want to cancel?";
        public const string APPLYTHEUPDATE = "Program need to restart to apply the update,Please click OK to restart the program!";
        public const string NOTNETWORK = "MDT Smart Kit update is unsuccessful. MDT Smart Kit will now restart. Please try to update again.";
    }
}
