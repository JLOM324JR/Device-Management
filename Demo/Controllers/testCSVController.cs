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
using System.Text.RegularExpressions;
//using System.Web.HttpContext.Current.Server.MapPath();

namespace Demo.Controllers
{
    public class testCSVController : ApiController
    {
        [HttpPost]
        [Route("import/devices")]

        public HttpResponseMessage ReadCSVFile([FromBody] csv csv)
        {
            string msg = "";
            
            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=Device;Integrated Security=true"))
                {

                    string ManufacturerID="";
                    string FirmwareID = "";
                    string GatewayID = "";
                    string ApplicationID = "";
                    string ModelID = "";
                    string CMTypeID = "";
                    string Device_StatusID = "";
                    string ContractID = "";

                    string ManufacturerName = "";
                    string FirmwareName = "";
                    string GatewayName = "";
                    string ApplicationName = "";
                    string ModelName = "";
                    string CMTypeName = "";
                    string Device_StatusName = "";
                    string ContractName = "";
                    
                    string filepath = "D:/Api/Demo/Demo/csv/device.csv";

                    string[] readAllLine = File.ReadAllLines(filepath);
                    DataTable dtTmp = CsvToDatatable(readAllLine);
                    for (int col = 0; col < dtTmp.Columns.Count; col++)
                    {
                        ManufacturerName = dtTmp.Columns[0].ToString();
                        FirmwareName = dtTmp.Columns[1].ToString();
                        GatewayName = dtTmp.Columns[2].ToString();
                        ApplicationName = dtTmp.Columns[3].ToString();
                        ModelName = dtTmp.Columns[4].ToString();
                        CMTypeName = dtTmp.Columns[5].ToString();
                        Device_StatusName = dtTmp.Columns[6].ToString();
                        ContractName = dtTmp.Columns[7].ToString();
                    }

                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                    {
                        for (int k = 0; k < dtTmp.Columns.Count; k++)
                        {
                            ManufacturerID = dtTmp.Rows[0][0].ToString();
                            FirmwareID = dtTmp.Rows[0][1].ToString();
                            GatewayID = dtTmp.Rows[0][2].ToString();
                            ApplicationID = dtTmp.Rows[0][3].ToString();
                            ModelID = dtTmp.Rows[0][4].ToString();
                            CMTypeID = dtTmp.Rows[0][5].ToString();
                            Device_StatusID = dtTmp.Rows[0][6].ToString();
                            ContractID = dtTmp.Rows[0][7].ToString();
                        }

                    }





                    
                    if (csv.manufacturerID == ManufacturerID){
                        //ManufacturerName = ManufacturerName.Replace(" ", "");
                        //ManufacturerName = ManufacturerName.Replace(" ", String.Empty);
                        dtTmp.Columns.Remove(""+ ManufacturerName);
                        
                        
                    }
                    if (csv.FirmwareID == FirmwareID){
                        dtTmp.Columns.Remove(""+ FirmwareName);
                        dtTmp.AcceptChanges();
                    }
                    if (csv.GatewareID == GatewayID)
                    {
                        dtTmp.Columns.Remove(""+ GatewayName);
                        dtTmp.AcceptChanges();
                    }
                    if (csv.ApplicationID == ApplicationID){
                        dtTmp.Columns.Remove(""+ ApplicationName);
                        dtTmp.AcceptChanges();
                    }
                    if (csv.ModelID == ModelID){
                        dtTmp.Columns.Remove(""+ ModelName);
                        dtTmp.AcceptChanges();
                    }
                    if (csv.CMTypeID == CMTypeID){
                        dtTmp.Columns.Remove(""+ CMTypeName);
                        dtTmp.AcceptChanges();
                    }
                    if (csv.Device_StatusID == Device_StatusID){
                        dtTmp.Columns.Remove(""+ Device_StatusName);
                        dtTmp.AcceptChanges();
                    }
                    if (csv.ContractID == ContractID){
                        dtTmp.Columns.Remove(""+ ContractName);
                        dtTmp.AcceptChanges();
                    }
                    else 
                    {
                        string gg = "bad";
                    }

                    dtTmp.AcceptChanges();
                    //DataTable dtCsv = new DataTable();
                    //dtCsv.Rows.Add(dtTmp);

                    using (SqlBulkCopy SqlBulkCopy = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.TableLock))
                    {

                        SqlBulkCopy.DestinationTableName = "Csv";
                        SqlBulkCopy.BatchSize = dtTmp.Rows.Count;
                        con.Open();
                        SqlBulkCopy.WriteToServer(dtTmp);
                        SqlBulkCopy.Close();
                        con.Close();

                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK, Content = new StringContent(msg) };
        }

        public DataTable CsvToDatatable(string[] lines)
        {
            DataTable dt = new DataTable();
            string[] value = lines[0].Split(',');
            foreach (string header in value)
            {
                dt.Columns.Add(new DataColumn(header));
            }

            for(int i =1;i<lines.Length;i++ )
            {
                string[] line = lines[i].Split(',');
                dt.Rows.Add(line);
            }

            return dt;

        }
       

       




    }
}