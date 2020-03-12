﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;

using System.Configuration;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.Http.Cors;

//using System.Web.HttpContext.Current.Server.MapPath();

namespace Demo.Controllers
{
    [EnableCors("*", "*", "*")]
    public class importCSVController : ApiController
    {
        [HttpPost]
        [Route("import/csv")]

        public HttpResponseMessage ReadCSVFile([FromBody] ContactModel import)
        {
            string msg = "";

            try
            {
                using (SqlConnection con = new SqlConnection(@"Data Source=(local)\SQLEXPRESS;Initial Catalog=Device;Integrated Security=true"))
                {





                    string ManufacturerID = "";
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


                    string dataimport = import.dataAll.ToString();
                    
                    string[] trans = new[] { import.dataAll };
                    DataTable dtTmp = CsvToDatatable(trans);



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






                    if (import.InsertManufacturer == ManufacturerID)
                    {
                        
                        dtTmp.Columns.Remove("" + ManufacturerName);


                    }
                    if (import.InsertFirmware == FirmwareID)
                    {
                        dtTmp.Columns.Remove("" + FirmwareName);
                        dtTmp.AcceptChanges();
                    }
                    if (import.InsertGateWayID == GatewayID)
                    {
                        dtTmp.Columns.Remove("" + GatewayName);
                        dtTmp.AcceptChanges();
                    }
                    if (import.InsertAppID == ApplicationID)
                    {
                        dtTmp.Columns.Remove("" + ApplicationName);
                        dtTmp.AcceptChanges();
                    }
                    if (import.InsertModelID == ModelID)
                    {
                        dtTmp.Columns.Remove("" + ModelName);
                        dtTmp.AcceptChanges();
                    }
                    if (import.CmTypeId == CMTypeID)
                    {
                        dtTmp.Columns.Remove("" + CMTypeName);
                        dtTmp.AcceptChanges();
                    }
                    if (import.statusDevice == Device_StatusID)
                    {
                        dtTmp.Columns.Remove("" + Device_StatusName);
                        dtTmp.AcceptChanges();
                    }
                    if (import.contractnumberid == ContractID)
                    {
                        dtTmp.Columns.Remove("" + ContractName);
                        dtTmp.AcceptChanges();
                    }
                    else
                    {
                        string pp = "bad";
                    }

                    dtTmp.AcceptChanges();

                    DataTable data = new DataTable();


                    DataTable dtUnSave = dtTmp.Clone();
                    DataTable dtSave = dtTmp.Clone();
                    List<string> strImei = new List<string>();
                    List<string> strImeiCsv = new List<string>();
                    string query = "select IMEI from Csv";
                    SqlCommand cmd = new SqlCommand(query, con);

                    con.Open();

                    SqlDataReader drImei = cmd.ExecuteReader();






                    while (drImei.Read())
                    {
                        strImei.Add(drImei.GetValue(0).ToString().Trim());
                    }

                    int a = dtTmp.Rows.Count;


                    for (int i = 0; i < dtTmp.Rows.Count; i++)


                    {


                        for (int dtSql = 0; dtSql < strImei.Count; dtSql++)
                        {
                            string db = strImei[dtSql];
                            string csvImei = dtTmp.Rows[i]["IMEI"].ToString().Trim();
                            if (csvImei == db)
                            {
                                DataRow dr = dtUnSave.NewRow();
                                dr["IMEI"] = dtTmp.Rows[i]["IMEI"].ToString().Trim();
                                dr["Serial_Number"] = dtTmp.Rows[i]["Serial_Number"].ToString().Trim();
                                dtUnSave.Rows.Add(dr);
                                DataRow drdr = dtTmp.Rows[i];
                                drdr.Delete();
                                dtTmp.AcceptChanges();
                            }

                        }





                    }







                    using (SqlBulkCopy SqlBulkCopy = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.TableLock))
                    {

                        SqlBulkCopy.DestinationTableName = "Csv";
                        SqlBulkCopy.BatchSize = dtTmp.Rows.Count;

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
            
            
           
            
            
            string[] value_cutone = lines[0].Split('[',']');

            

            string[] stringSeparators = new string[] { "\\r\\n"};
            string[] lines2 = value_cutone[1].Split(stringSeparators, StringSplitOptions.None);
            
            string[] testss = lines2[0].Split(',', '\"');
            
           
            




            for (int i = 1; i < testss.Length; i++)
            {
                dt.Columns.Add(new DataColumn(testss[i]));
            }

            for (int i = 1; i < ((lines2.Length)-2); i++)
            {
                string[] lineline = lines2[i].Split(',');
                dt.Rows.Add(lineline);

            }

            return dt;
           ;
        }







    }
}



