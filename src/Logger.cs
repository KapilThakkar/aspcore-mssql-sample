#region Copyright ©2016, Click2Cloud Inc. - All Rights Reserved
/* ------------------------------------------------------------------- *
*                            Click2Cloud Inc.                          *
*                  Copyright ©2016 - All Rights reserved               *
*                                                                      *
* Apache 2.0 License                                                   *
* You may obtain a copy of the License at                              * 
* http://www.apache.org/licenses/LICENSE-2.0                           *
* Unless required by applicable law or agreed to in writing,           *
* software distributed under the License is distributed on an "AS IS"  *
* BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express  *
* or implied. See the License for the specific language governing      *
* permissions and limitations under the License.                       *
*                                                                      *
* -------------------------------------------------------------------  */
#endregion Copyright ©2016, Click2Cloud Inc. - All Rights Reserved

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;

namespace Click2Cloud.Samples.AspNetCore.MvcSQLDb
{
    public class Logger
    {
        #region Public Methods

        /// <summary>
        /// Log Error
        /// <param name="message">Message to log</param>
        /// <param name="module">Module Name</param>
        /// </summary>
        public static void Error(string message, string module)
        {
            WriteEntry(message, "ERROR", module);
        }

        /// <summary>
        /// Log Error
        /// <param name="ex">Exception object</param>
        /// <param name="module">Module Name</param>
        /// </summary>
        public static void Error(Exception ex, string module)
        {
            WriteEntry(ex.Message, "ERROR  ", module, ex.StackTrace);
        }

        /// <summary>
        /// Log Warning
        /// <param name="message"></param>
        /// <param name="module"></param>
        /// </summary>
        public static void Warning(string message, string module)
        {
            WriteEntry(message, "WARNING", module);
        }

        /// <summary>
        /// Log Info
        /// <param name="message"></param>
        /// <param name="module"></param>
        /// </summary>
        public static void Info(string message, string module)
        {
            WriteEntry(message, "INFO   ", module);
        }

        #endregion Public Methods

        #region Private Methods

        private static void WriteEntry(string message, string type, string module, string stackTrace = "")
        {
            try
            {
                //Openshift specific log file location
                string logFilePath = Environment.GetEnvironmentVariable("HOME") + @"\logs\applicationlog.log";
                DirectoryInfo logFileDirectory = Directory.GetParent(logFilePath);

                //Check if file and directory exist if not then create it
                if (!Directory.Exists(logFileDirectory.FullName)) { Directory.CreateDirectory(logFileDirectory.FullName); }
                if (!File.Exists(logFilePath)) { File.Create(logFilePath).Close(); }

                //Lock file for multiple asyn call
                object objectLock = new object();
                lock (objectLock)
                {
                    //load log file in stream in append mode
                    FileStream streamLogFile = new FileStream(logFilePath, FileMode.Append, FileAccess.Write);

                    //create and add new trace listener
                    TextWriterTraceListener myListener = new TextWriterTraceListener(streamLogFile);
                    Trace.Listeners.Add(myListener);

                    //write log into logger file
                    Trace.WriteLine(string.Format("{0} | {1} | {2} | {3}",
                                          DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                                          type,
                                          module,
                                          message));
                    if (!string.IsNullOrEmpty(stackTrace)) { Trace.WriteLine("------------------------------------------" + Environment.NewLine + stackTrace + Environment.NewLine + "------------------------------------------"); }

                    //flush trace and close log stream
                    Trace.Flush();
                    streamLogFile.Close();
                }
            }
            catch (Exception) { }
        }

        #endregion Private Methods
    }
}
