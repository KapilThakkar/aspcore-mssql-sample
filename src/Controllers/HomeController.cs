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

using Microsoft.AspNet.Mvc;
using Click2Cloud.Samples.AspNetCore.MvcSQLDb.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Linq;

namespace Click2Cloud.Samples.AspNetCore.MvcSQLDb.Web
{
    public class HomeController : Controller
    {
        private static string TABLE_NAME = "Persons";

        public IActionResult Index()
        {
            string error = string.Empty;
            List<Person> personCollection = this.ListPersons(out error);

            if (!string.IsNullOrEmpty(error)) { ViewBag.ClusterIPError = error; }
            return View(personCollection);
        }

        [HttpPost]
        public IActionResult Index(Microsoft.AspNet.Http.Internal.FormCollection formCollection)
        {
            try
            {
                DataTable datatable = new DataTable();
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionSetting.CONNECTION_STRING))
                {
                    string query = string.Format("Insert into {0}(Name,Address,Contactno,Picture) Values ('{1}','{2}','{3}','{4}')", TABLE_NAME, Request.Form["personname"].ToString(), Request.Form["address"].ToString(), Request.Form["contactno"].ToString(), string.Empty);
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        sqlConnection.Open();
                        dataAdapter.Fill(datatable);
                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex) { ViewBag.ClusterIPError = "Unable to add records. Please verify your connection."; Logger.Error(ex, "Index"); }


            string error = string.Empty;
            List<Person> personCollection = this.ListPersons(out error);

            if (!string.IsNullOrEmpty(error)) { ViewBag.ClusterIPError = error; }
            return View(personCollection);
        }

        public List<Person> ListPersons(out string error)
        {
            List<Person> personCollection = null;
            error = string.Empty;
            try
            {
                DataTable datatable = new DataTable();
                using (SqlConnection sqlConnection = new SqlConnection(ConnectionSetting.CONNECTION_STRING))
                {
                    string query = string.Format("SELECT * FROM {0}", TABLE_NAME);
                    using (SqlCommand command = new SqlCommand(query, sqlConnection))
                    {
                        SqlDataAdapter dataAdapter = new SqlDataAdapter(command);
                        sqlConnection.Open();
                        dataAdapter.Fill(datatable);
                        sqlConnection.Close();
                    }
                }

                personCollection = (from DataRow dr in datatable.Rows
                                    select new Person
                                    {
                                        Id = Convert.ToInt32(dr["PersonID"].ToString()),
                                        Name = dr["Name"].ToString(),
                                        Address = dr["Address"].ToString(),
                                        ContactNo = dr["ContactNo"].ToString(),
                                        Picture = dr["Picture"].ToString() == string.Empty ? "~/pics/no_picture.jpg" : "~/pics/" + dr["Picture"].ToString(),
                                    }).ToList();
            }
            catch (Exception ex) { error = ex.Message; }
            return personCollection;
        }
    }
}
