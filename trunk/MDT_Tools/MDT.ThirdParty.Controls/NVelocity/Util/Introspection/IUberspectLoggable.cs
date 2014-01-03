/*
* Licensed to the Apache Software Foundation (ASF) under one
* or more contributor license agreements.  See the NOTICE file
* distributed with this work for additional information
* regarding copyright ownership.  The ASF licenses this file
* to you under the Apache License, Version 2.0 (the
* "License"); you may not use this file except in compliance
* with the License.  You may obtain a copy of the License at
*
*   http://www.apache.org/licenses/LICENSE-2.0
*
* Unless required by applicable law or agreed to in writing,
* software distributed under the License is distributed on an
* "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY
* KIND, either express or implied.  See the License for the
* specific language governing permissions and limitations
* under the License.    
*/

namespace NVelocity.Util.Introspection
{
    using Runtime;
    using Runtime.Log;

    /// <summary>  Marker interface to let an uberspector indicate it can and wants to
    /// Log
    /// 
    /// Thanks to Paulo for the suggestion
    /// 
    /// </summary>
    /// <author>  <a href="mailto:nbubna@apache.org">Nathan Bubna</a>
    /// </author>
    /// <author>  <a href="mailto:geirm@apache.org">Geir Magnusson Jr.</a>
    /// </author>
    /// <version>  $Id: UberspectLoggable.java 463298 2006-10-12 16:10:32Z henning $
    /// 
    /// </version>
    public interface IUberspectLoggable
    {
        /// <summary> Sets the logger.  This will be called before any calls to the
        /// uberspector
        /// </summary>
        /// <param name="Log">
        /// </param>
        Log Log
        {
            set;

        }
    }
}