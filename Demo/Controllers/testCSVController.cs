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
                using (SqlConnection SScon = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=Device;Integrated Security=true"))
                {

                    string ManufacturerID="";
                    string FirmwareID = "";
                    string GatewareID = "";
                    string ApplicationID = "";
                    string ModelID = "";
                    string CMTypeID = "";
                    string Device_StatusID = "";
                    string ContractID = "";

                    string ManufacturerName = "";
                    string FirmwareName = "";
                    string GatewareName = "";
                    string ApplicationName = "";
                    string ModelName = "";
                    string CMTypeName = "";
                    string Device_StatusName = "";
                    string ContractName = "";
                    //SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=DeviceIntegrated Security=true");
                    string filepath = "D:/Api/Demo/Demo/csv/device.csv";

                    string[] readAllLine = File.ReadAllLines(filepath);
                    DataTable dtTmp = CsvToDatatable(readAllLine);
                    for (int col = 0; col < dtTmp.Columns.Count; col++)
                    {
                        ManufacturerName = dtTmp.Columns[0].ToString().Trim();
                        FirmwareName = dtTmp.Columns[1].ToString().Trim();
                        GatewareName = dtTmp.Columns[2].ToString().Trim();
                        ApplicationName = dtTmp.Columns[3].ToString().Trim();
                        ModelName = dtTmp.Columns[4].ToString().Trim();
                        CMTypeName = dtTmp.Columns[5].ToString().Trim();
                        Device_StatusName = dtTmp.Columns[6].ToString().Trim();
                        ContractName = dtTmp.Columns[7].ToString().Trim();
                    }

                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                    {
                        for (int k = 0; k < dtTmp.Columns.Count; k++)
                        {
                            ManufacturerID = dtTmp.Rows[0][0].ToString();
                            FirmwareID = dtTmp.Rows[0][1].ToString();
                            GatewareID = dtTmp.Rows[0][2].ToString();
                            ApplicationID = dtTmp.Rows[0][3].ToString();
                            ModelID = dtTmp.Rows[0][4].ToString();
                            CMTypeID = dtTmp.Rows[0][5].ToString();
                            Device_StatusID = dtTmp.Rows[0][6].ToString();
                            ContractID = dtTmp.Rows[0][7].ToString();
                        }

                    }





                    
                    if (csv.manufacturerID == ManufacturerID){
                        dtTmp.Columns.Remove(ManufacturerName);
                        
                        
                    }
                    if (csv.FirmwareID == FirmwareID){
                        dtTmp.Columns.Remove("FirmwareName");
                        dtTmp.AcceptChanges();
                    }
                    if (csv.GatewareID == GatewareID){
                        dtTmp.Columns.Remove("GatewareName");
                        dtTmp.AcceptChanges();
                    }
                    if (csv.ApplicationID == ApplicationID){
                        dtTmp.Columns.Remove("ApplicationName");
                        dtTmp.AcceptChanges();
                    }
                    if (csv.ModelID == ModelID){
                        dtTmp.Columns.Remove("ModelName");
                        dtTmp.AcceptChanges();
                    }
                    if (csv.CMTypeID == CMTypeID){
                        dtTmp.Columns.Remove("CMTypeName");
                        dtTmp.AcceptChanges();
                    }
                    if (csv.Device_StatusID == Device_StatusID){
                        dtTmp.Columns.Remove("Device_StatusName");
                        dtTmp.AcceptChanges();
                    }
                    if (csv.ContractID == ContractID){
                        dtTmp.Columns.Remove("ContractName");
                        dtTmp.AcceptChanges();
                    }
                    else 
                    {
                        string gg = "bad";
                    }

                    dtTmp.AcceptChanges();
                    DataTable test = dtTmp;


                    //foreach (DataRow row in dtTmp.Rows)
                    //{
                    //    ManufacturerID = row["ManufacturerID"].ToString();
                    //    FirmwareID = row["FirmwareID "].ToString();
                    //    GatewareID = row["GatewareID "].ToString();
                    //    ApplicationID = row["ApplicationID"].ToString();
                    //    ModelID = row["ModelID"].ToString();
                    //    CMTypeID = row["FirmwareID "].ToString();
                    //    Device_StatusID = row["Device_StatusID"].ToString();
                    //    ContractID = row["ContractID"].ToString();
                    //}
















                    //    StreamReader csvData = new StreamReader(filepath);
                    //    string[] value = csvData.ReadLine().Split(',');
                    //    DataTable dt = new DataTable();
                    //    //value = csvData.ReadLine().Split(',');
                    //    DataRow row;
                    //    foreach (string header in value)
                    //    {
                    //        dt.Columns.Add(new DataColumn(header));

                    //    }
                    //    while (!csvData.EndOfStream)
                    //    {
                    //        for (int i = 0; i < value.Length; i++)
                    //        {
                    //            value = csvData.ReadLine().Split(',');

                    //            if (dt.Rows[i]["ManufacturerID"].ToString() == csv.manufacturerID) { }
                    //            if (dt.Rows[i]["FirmwareID"].ToString() == csv.FirmwareID) { }
                    //            if (dt.Rows[i]["GatewareID"].ToString() == csv.GatewareID) { }
                    //            if (dt.Rows[i]["ApplicationID"].ToString() == csv.ApplicationID) { }
                    //            if (dt.Rows[i]["ModelID"].ToString() == csv.ModelID) { }
                    //            if (dt.Rows[i]["CMTypeID"].ToString() == csv.CMTypeID) { }
                    //            if (dt.Rows[i]["Device_StatusID"].ToString() == csv.Device_StatusID) { }
                    //            if (dt.Rows[i]["ContractID"].ToString() == csv.ContractID) { }
                    //        }

                    //    }


                    //}

                    //while (!csvData.EndOfStream)
                    //{
                    //    value = csvData.ReadLine().Split(',');
                    //    if (value.Length == dt.Columns.Count)
                    //    {
                    //        row = dt.NewRow();
                    //        row.ItemArray = value;
                    //        dt.Rows.Add(row);
                    //    }
                    //}
                    //SqlBulkCopy SqlBulkCopy = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.TableLock);
                    //SqlBulkCopy.DestinationTableName = "test1";
                    //SqlBulkCopy.BatchSize = dt.Rows.Count;
                    //con.Open();
                    //SqlBulkCopy.WriteToServer(dt);
                    //SqlBulkCopy.Close();
                    //con.Close();


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