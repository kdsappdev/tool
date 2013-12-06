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


namespace NVelocity.Runtime.Log
{
    /// <summary>  Logger used in case of failure. Does nothing.
    /// 
    /// </summary>
    /// <author>  <a href="mailto:geirm@optonline.net">Geir Magnusson Jr.</a>
    /// </author>
    /// <deprecated> Use NullLogChute.
    /// </deprecated>
    /// <version>  $Id: NullLogSystem.java 463298 2006-10-12 16:10:32Z henning $
    /// </version>
    public class NullLogSystem : NullLogChute, ILogSystem
    {
        /// <param name="level">
        /// </param>
        /// <param name="message">
        /// </param>
        /// <deprecated> Use Log(level, message).
        /// </deprecated>
        public virtual void LogVelocityMessage(int level, System.String message)
        {
        }
    }
}