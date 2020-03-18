using System;
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

namespace Demo.Controllers
{
    [EnableCors("*", "*", "*")]
    public class UpdateCsvController : ApiController
    {
        [HttpPut]
        [Route("update/csv")]
        public HttpResponseMessage ReadCSVFile([FromBody] csv csv)
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
                    string ImeiName = "";
                    string serial_name = "";

                    string filepath = "D:/Api/Demo/Demo/csv/device.csv";

                    string[] readAllLine = File.ReadAllLines(filepath);
                    DataTable dtTmp = CsvToDatatable(readAllLine);
                    DataTable dtSave = dtTmp.Clone();



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




                    DataTable dtUnSave = dtTmp.Clone();
                    DataTable data = new DataTable();
                    List<string> strImei = new List<string>();
                    List<string> strImeiCsv = new List<string>();
                    string query = "select IMEI from Device";
                    SqlCommand command = new SqlCommand(query, con);

                    con.Open();


                    SqlDataReader drImei = command.ExecuteReader();
                    while (drImei.Read())
                    {
                        strImei.Add(drImei.GetValue(0).ToString().Trim());
                    }
                    con.Close();
                    int a = dtTmp.Rows.Count;

                    for (int i = 0; i < dtTmp.Rows.Count; i++)


                    {
                        if (dtTmp != null)
                        {

                            for (int dtSql = 0; dtSql < strImei.Count; dtSql++)
                            {
                                if (strImei != null)
                                {
                                    string db = strImei[dtSql];
                                    string csvImei = dtTmp.Rows[i]["IMEI"].ToString().Trim();
                                    if (csvImei == db)
                                    {
                                        //ถ้าตรงเก็บใน SAVE
                                        DataRow dr = dtSave.NewRow();
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
                                        dtSave.Rows.Add(dr);
                                        dtSave.AcceptChanges();
                                        DataRow drdr = dtTmp.Rows[i];
                                        drdr.Delete();
                                        dtTmp.AcceptChanges();



                                    }

                                }
                            }

                        }
                        else
                        {
                            break;
                        }

                    }

                    for (int y = 0; y < dtSave.Rows.Count; y++)
                    {

                        SqlCommand cmd = new SqlCommand("BulkUpDate", con);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ManufacturerID", dtSave.Rows[y]["" + ManufacturerName].ToString());
                        cmd.Parameters.AddWithValue("@Firmware", dtSave.Rows[y]["" + FirmwareName].ToString());
                        cmd.Parameters.AddWithValue("@GateWayID", dtSave.Rows[y]["" + GatewayName].ToString());
                        cmd.Parameters.AddWithValue("@ApplicationID", dtSave.Rows[y]["" + ApplicationName].ToString());
                        cmd.Parameters.AddWithValue("@ModelID", dtSave.Rows[y]["" + ModelName].ToString());
                        cmd.Parameters.AddWithValue("@Communication_Media_TypeID", dtSave.Rows[y]["" + CMTypeName].ToString());
                        cmd.Parameters.AddWithValue("@Device_StatusID", dtSave.Rows[y]["" + Device_StatusName].ToString());
                        cmd.Parameters.AddWithValue("@ContractID", dtSave.Rows[y]["" + ContractName].ToString());
                        cmd.Parameters.AddWithValue("@IMEI", dtSave.Rows[y]["" + ImeiName].ToString());
                        cmd.Parameters.AddWithValue("@Serial_Number", dtSave.Rows[y]["" + serial_name].ToString());


                        cmd.Connection = con;
                        con.Open();
                        cmd.ExecuteNonQuery();
                        con.Close();



                    }









                    int pp = 2;






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

            for (int i = 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(',');
                dt.Rows.Add(line);
            }

            return dt;

        }







    }
}