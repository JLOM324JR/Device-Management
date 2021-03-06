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
            ContactModel gg = new ContactModel();
            string msg = "";
            
            DataSet dsData = new DataSet();

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
                    string ImeiName = "";
                    string serial_name = "";


                    string dataimport = import.dataAll.ToString();

                    string[] trans = new[] { import.dataAll };
                    DataTable dtTmp = CsvToDatatable(trans);
                    //clone dataTable dtTmp
                    //DataTable dtClone_dtTmp = dtTmp.Copy();


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
                        ImeiName = dtTmp.Columns[8].ToString();
                        serial_name = dtTmp.Columns[9].ToString();
                    }

                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                    {
                        for (int k = 0; k < dtTmp.Columns.Count; k++)
                        {
                            ManufacturerID = dtTmp.Rows[0][0].ToString().Trim();
                            FirmwareID = dtTmp.Rows[0][1].ToString().Trim();
                            GatewayID = dtTmp.Rows[0][2].ToString().Trim();
                            ApplicationID = dtTmp.Rows[0][3].ToString().Trim();
                            ModelID = dtTmp.Rows[0][4].ToString().Trim();
                            CMTypeID = dtTmp.Rows[0][5].ToString().Trim();
                            Device_StatusID = dtTmp.Rows[0][6].ToString().Trim();
                            ContractID = dtTmp.Rows[0][7].ToString().Trim();
                        }

                    }




                    DataTable data = new DataTable();
                    DataTable dtUnSave = dtTmp.Clone();
                    DataTable dtSave = dtTmp.Clone();
                    List<string> strImei = new List<string>();
                    List<string> strImeiCsv = new List<string>();
                    if (import.InsertManufacturer == ManufacturerID)
                    {
                        if (import.InsertFirmware == FirmwareID)
                        {
                            if (import.InsertGateWayID == GatewayID)
                            {
                                if (import.InsertAppID == ApplicationID)
                                {
                                    if (import.InsertModelID == ModelID)
                                    {
                                        if (import.CmTypeId == CMTypeID)
                                        {
                                            if (import.statusDevice == Device_StatusID)
                                            {
                                                if (import.contractnumberid == ContractID)
                                                {
                                                    string query = "select IMEI from Device";
                                                    SqlCommand cmd = new SqlCommand(query, con);
                                                    con.Open();
                                                    SqlDataReader drImei = cmd.ExecuteReader();

                                                    while (drImei.Read())
                                                    {
                                                        strImei.Add(drImei.GetValue(0).ToString().Trim());
                                                    }

                                                    int a = dtTmp.Rows.Count;


                                                    //check dupicate imei in csv and Device database(Device Table)
                                                    for (int i = 0; i < dtTmp.Rows.Count; i++)
                                                    {
                                                        DataRow drdr = dtTmp.Rows[i];

                                                        for (int dtSql = 0; dtSql < strImei.Count; dtSql++)
                                                        {

                                                            if (strImei != null)
                                                            {
                                                                string db = strImei[dtSql];
                                                                string csvImei = dtTmp.Rows[i]["IMEI"].ToString().Trim();
                                                                int kk = i;
                                                                if (csvImei == db)
                                                                {
                                                                    DataRow dr = dtUnSave.NewRow();

                                                                    dr["" + ManufacturerName] = dtTmp.Rows[i]["" + ManufacturerName].ToString().Trim();
                                                                    dr["" + FirmwareName] = dtTmp.Rows[i]["" + FirmwareName].ToString().Trim();
                                                                    dr["" + GatewayName] = dtTmp.Rows[i]["" + GatewayName].ToString().Trim();
                                                                    dr["" + ApplicationName] = dtTmp.Rows[i]["" + ApplicationName].ToString().Trim();
                                                                    dr["" + ModelName] = dtTmp.Rows[i]["" + ModelName].ToString().Trim();
                                                                    dr["" + CMTypeName] = dtTmp.Rows[i]["" + CMTypeName].ToString().Trim();
                                                                    dr["" + Device_StatusName] = dtTmp.Rows[i]["" + Device_StatusName].ToString().Trim();
                                                                    dr["" + ContractName] = dtTmp.Rows[i]["" + ContractName].ToString().Trim();
                                                                    dr["" + ImeiName] = dtTmp.Rows[i]["" + ImeiName].ToString().Trim();
                                                                    dr["" + serial_name] = dtTmp.Rows[i]["" + serial_name].ToString().Trim();
                                                                    dtUnSave.Rows.Add(dr);

                                                                    drdr.Delete();
                                                                    dtTmp.AcceptChanges();
                                                                    break;
                                                                }//end if (csvImei == db)
                                                            }//end if (strImei != null)

                                                        }

                                                        int jj = i;

                                                    }//for (int i = 0; i < dtTmp.Rows.Count; i++)
                                                }//if (import.contractnumberid == ContractID)
                                            }

                                        }

                                    }

                                }

                            }

                        }
                        


                        dtTmp.AcceptChanges();

                        //check dupicate imei in csv and Device database(Device Table)
                        for (int i = 0; i < dtTmp.Rows.Count; i++)
                        {
                            DataRow drdr = dtTmp.Rows[i];

                            for (int dtSql = 0; dtSql < strImei.Count; dtSql++)
                            {

                                if (strImei != null)
                                {
                                    string db = strImei[dtSql];
                                    string csvImei = dtTmp.Rows[i]["IMEI"].ToString().Trim();
                                    int kk = i;
                                    if (csvImei == db)
                                    {
                                        DataRow dr = dtUnSave.NewRow();

                                        dr["" + ManufacturerName] = dtTmp.Rows[i]["" + ManufacturerName].ToString().Trim();
                                        dr["" + FirmwareName] = dtTmp.Rows[i]["" + FirmwareName].ToString().Trim();
                                        dr["" + GatewayName] = dtTmp.Rows[i]["" + GatewayName].ToString().Trim();
                                        dr["" + ApplicationName] = dtTmp.Rows[i]["" + ApplicationName].ToString().Trim();
                                        dr["" + ModelName] = dtTmp.Rows[i]["" + ModelName].ToString().Trim();
                                        dr["" + CMTypeName] = dtTmp.Rows[i]["" + CMTypeName].ToString().Trim();
                                        dr["" + Device_StatusName] = dtTmp.Rows[i]["" + Device_StatusName].ToString().Trim();
                                        dr["" + ContractName] = dtTmp.Rows[i]["" + ContractName].ToString().Trim();
                                        dr["" + ImeiName] = dtTmp.Rows[i]["" + ImeiName].ToString().Trim();
                                        dr["" + serial_name] = dtTmp.Rows[i]["" + serial_name].ToString().Trim();
                                        dtUnSave.Rows.Add(dr);

                                        drdr.Delete();
                                        dtTmp.AcceptChanges();
                                        break;
                                    }//end if (csvImei == db)
                                }//end if (strImei != null)

                            }

                            int jj = i;

                        }//for (int i = 0; i < dtTmp.Rows.Count; i++)













                        //check dupicate imei in csv and Device database(Device Table)


                        //check dupicate imei in datatable dtTmp
                        for (int i = 0; i < dtTmp.Rows.Count; i++)
                        {
                            for (int j = 0; j < dtTmp.Rows.Count; j++)
                            {
                                string testA = dtTmp.Rows[i]["" + ImeiName].ToString().Trim();
                                if (dtTmp.Rows[i] != dtTmp.Rows[j])
                                {
                                    string testB = dtTmp.Rows[j]["" + ImeiName].ToString().Trim();
                                    if (testA == testB)
                                    {
                                        DataRow dr_dupicate = dtUnSave.NewRow();
                                        dr_dupicate["" + ManufacturerName] = dtTmp.Rows[i]["" + ManufacturerName].ToString().Trim();
                                        dr_dupicate["" + FirmwareName] = dtTmp.Rows[i]["" + FirmwareName].ToString().Trim();
                                        dr_dupicate["" + GatewayName] = dtTmp.Rows[i]["" + GatewayName].ToString().Trim();
                                        dr_dupicate["" + ApplicationName] = dtTmp.Rows[i]["" + ApplicationName].ToString().Trim();
                                        dr_dupicate["" + ModelName] = dtTmp.Rows[i]["" + ModelName].ToString().Trim();
                                        dr_dupicate["" + CMTypeName] = dtTmp.Rows[i]["" + CMTypeName].ToString().Trim();
                                        dr_dupicate["" + Device_StatusName] = dtTmp.Rows[i]["" + Device_StatusName].ToString().Trim();
                                        dr_dupicate["" + ContractName] = dtTmp.Rows[i]["" + ContractName].ToString().Trim();
                                        dr_dupicate["" + ImeiName] = dtTmp.Rows[i]["" + ImeiName].ToString().Trim();
                                        dr_dupicate["" + serial_name] = dtTmp.Rows[i]["" + serial_name].ToString().Trim();
                                        dtUnSave.Rows.Add(dr_dupicate);
                                        DataRow drdr = dtTmp.Rows[i];
                                        drdr.Delete();
                                        dtTmp.AcceptChanges();
                                    }
                                }
                            }

                        }




                        gg.responseData = dtUnSave.Copy();

                        dsData.Tables.Add(gg.responseData);


                        using (SqlBulkCopy SqlBulkCopy = new SqlBulkCopy(con.ConnectionString, SqlBulkCopyOptions.TableLock))
                        {

                            SqlBulkCopy.DestinationTableName = "Device";
                            SqlBulkCopy.BatchSize = dtTmp.Rows.Count;

                            SqlBulkCopy.WriteToServer(dtTmp);
                            SqlBulkCopy.Close();
                            con.Close();

                        }
                    }

                    else
                    {
                        string pp = "bad";
                    }
                }
            }
            catch (Exception ex)
            {
                msg = ex.ToString();
            }
            return Request.CreateResponse(HttpStatusCode.OK, dsData);
        }

        public DataTable CsvToDatatable(string[] lines)
        {
            DataTable dt = new DataTable();





            string[] value_cutone = lines[0].Split('[', ']');



            string[] stringSeparators = new string[] { "\\r\\n" };
            string[] lines2 = value_cutone[1].Split(stringSeparators, StringSplitOptions.None);

            string[] testss = lines2[0].Split(',', '\"');







            for (int i = 1; i < testss.Length; i++)
            {
                dt.Columns.Add(new DataColumn(testss[i]));
            }

            for (int i = 1; i < ((lines2.Length) - 1); i++)
            {
                string[] lineline = lines2[i].Split(',');
                dt.Rows.Add(lineline);

            }

            return dt;
            ;
        }







    }
}