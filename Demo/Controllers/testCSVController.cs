using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Grpc.Core;
using System.Configuration;
using System.Data.SqlClient;
//using System.Web.HttpContext.Current.Server.MapPath();

namespace Demo.Controllers
{
    public class testCSVController : ApiController
    {
        [HttpPost]
        [Route("import/device")]
        //public DataTable test = ReadCSV();
        public DataTable ReadCSV()
        {
            DataTable dt = new DataTable();
            string strPath = System.Web.HttpContext.Current.Server.MapPath("csv/customer.csv");
            //string strPath = File.OpenText(Server.MapPath("csv/customer.csv"));
            dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(string)),
            new DataColumn("Look", typeof(string)),
            new DataColumn("Itemname",typeof(string)) });
            string csvData = File.ReadAllText("D:/Api/Demo/Demo/csv/customer.csv");
            foreach (string row in csvData.Split('\n'))
            {
                if (!string.IsNullOrEmpty(row))
                {
                    dt.Rows.Add();
                    int i = 0;
                    foreach (string cell in row.Split(','))
                    {
                       dt.Rows[dt.Rows.Count - 1][i] = cell;
                        using (SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=wednesday;Integrated Security=true"))
                        {
                            using (SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(con))
                            {
                                //Set the database table name
                                sqlBulkCopy.DestinationTableName = "dbo.test1";
                                con.Open();
                                sqlBulkCopy.WriteToServer(dt);
                                con.Close();
                            }
                        }
                        i++;
                   }
                }
            }
            
            return dt;

        }
    }
}