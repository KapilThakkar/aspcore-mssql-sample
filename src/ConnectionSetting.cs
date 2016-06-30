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

namespace Click2Cloud.Samples.AspNetCore.MvcSQLDb
{
    public static class ConnectionSetting
    {
        internal static string CONNECTION_STRING
        {
            get
            {
                if (!(string.IsNullOrEmpty(SQLDB_USER) || string.IsNullOrEmpty(SQLDB_PASSWORD)
                || string.IsNullOrEmpty(SQLDB_SERVER) || string.IsNullOrEmpty(SQLDB_DATABASE) 
                || string.IsNullOrEmpty(SQLDB_PORT)))
                {
                    string _connectionString = string.Format("Data Source={0},1433; Initial Catalog=MVCPersonDB; User ID=sa; Password=newuser@123", MSSQL_SERVICE_HOST, SQLDB_DATABASE,
                    SQLDB_USER, SQLDB_PASSWORD, SQLDB_PORT);

                    return _connectionString;
                }
                else
                {
                    throw new Exception("Environment variables not set.");
                }
            }
        }

        private static string MSSQL_SERVICE_HOST
        {
            get
            {
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MSSQL_SERVICE_HOST")))
                {
                    return Environment.GetEnvironmentVariable("MSSQL_SERVICE_HOST");
                }

                return string.Empty;
            }
        }

        private static string SQLDB_USER
        {
            get
            {
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SQLDB_USER")))
                {
                    return Environment.GetEnvironmentVariable("SQLDB_USER");
                }

                return string.Empty;
            }
        }

        private static string SQLDB_PASSWORD
        {
            get
            {
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SQLDB_PASSWORD")))
                {
                    return Environment.GetEnvironmentVariable("SQLDB_PASSWORD");
                }

                return string.Empty;
            }
        }

        internal static string SQLDB_DATABASE
        {
            get
            {
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SQLDB_DATABASE")))
                {
                    return Environment.GetEnvironmentVariable("SQLDB_DATABASE");
                }

                return string.Empty;
            }
        }

        internal static string SQLDB_PORT
        {
            get
            {
                if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SQLDB_PORT")))
                {
                    return Environment.GetEnvironmentVariable("SQLDB_PORT");
                }

                return string.Empty;
            }
        }
    }
}
