using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Description;
using System.Web.Providers.Entities;
using dotnet3._1_in_docker.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using static dotnet3._1_in_docker.InMemoryDB;

namespace dotnet3._1_in_docker.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class passengerController : Controller
    {
        InMemoryDB inmemoryobj = new InMemoryDB();
        string Function_To_Use = "2";      //1: SQL Server, 2: SQLite/memcache/Redif
        private readonly ILogger<passengerController> _logger;

        public passengerController(ILogger<passengerController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        
        //#region available_cabs
        //[HttpPost("available_cabs")]
        //public IActionResult available_cabs(Model.Classes.DriverLocation classes)
        //{
        //    JArray DataArray = new JArray();
        //    JObject JO = new JObject();
        //    try
        //    {
                
        //        string Latitude_Customer = classes.latitude;
        //        string Longitude_Customer = classes.longitude;

        //        Double num = 0;
        //        bool isDouble_Latitude = Double.TryParse(Latitude_Customer,out num);
        //        bool isDouble_Longitude = Double.TryParse(Longitude_Customer, out num);

        //        Console.WriteLine(Latitude_Customer);
        //        /*
        //        string Param = classes.Param;
        //        string Driver_ID = classes.Driver_ID;
        //        string Latitude_Driver = classes.Latitude_Driver;
        //        string Longitude_Driver = classes.Longitude_Driver;
        //        string Latitude_Customer = classes.Latitude_Customer;
        //        string Longitude_Customer = classes.Longitude_Customer;
        //        */

        //        string Param = "Get_Nearby_Drivers";
        //        string name = string.Empty;
        //        string email = string.Empty;
        //        string phone_number = string.Empty;
        //        string license_number = string.Empty;
        //        string car_number = string.Empty;
        //        string Driver_ID = string.Empty;
        //        string Latitude_Driver = string.Empty;
        //        string Longitude_Driver = string.Empty;

        //        string isError = "0";
        //        string Error_Text = string.Empty;
        //        #region Validations
        //        if(isDouble_Latitude==false || isDouble_Longitude == false)
        //        {
        //            isError = "1";
        //            Error_Text = "Please Enter valid Latitude/Longitude";
        //        }

        //        if (string.IsNullOrEmpty(Latitude_Customer) || string.IsNullOrEmpty(Longitude_Customer))
        //        {
        //            isError = "1";
        //            Error_Text = "Please Enter all the required fields";
        //        }

                

        //        if (Latitude_Customer.IndexOf('.') == -1 || Longitude_Customer.IndexOf('.') == -1)
        //        {
        //            isError = "1";
        //            Error_Text = "Latitude / Longitude is invalid";
        //        }

        //        if (isError == "1")
        //        {
        //            JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", Error_Text));
        //            return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //        }
        //        #endregion

        //        DataTable DTOuterData;
        //        string cabs_Found = "0";
        //        if (Function_To_Use == "1")
        //        {
        //            DTOuterData = inmemoryobj.RegisterDriver_SQLServer(name, email, phone_number, license_number, car_number,
        //            Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer);

        //            if (DTOuterData.Rows.Count > 0)
        //            {
        //                foreach (DataRow dataRow_outer in DTOuterData.Rows)
        //                {
        //                    JObject Data_OBJ_Outer = new JObject();
        //                    foreach (DataColumn col in DTOuterData.Columns)
        //                    {
        //                        if (col.ColumnName == "Err_Msg")
        //                        {
        //                            string err_Msg = string.Empty;
        //                            err_Msg = dataRow_outer[col.ColumnName].ToString();
        //                            JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", err_Msg));
        //                            return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //                        }
        //                        else
        //                        {
        //                            if (Data_OBJ_Outer.Property(col.ColumnName) != null)
        //                            {
        //                                //Don't Add in this case. Duplicate Key
        //                            }
        //                            else
        //                            {
        //                                Data_OBJ_Outer.Add(new JProperty(col.ColumnName, dataRow_outer[col.ColumnName]));
        //                            }
        //                        }
        //                    }

        //                    DataArray.Add(Data_OBJ_Outer);
        //                }
        //                JO = new JObject(new JProperty("available_cabs", DataArray));
        //                return StatusCode(200, JO);
        //            }
        //            else
        //            {
        //                JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", "No cabs available"));
        //                return StatusCode((int)HttpStatusCode.OK, JO);
        //            }
        //        }
        //        else
        //        {
        //            /*
        //            DTOuterData = inmemoryobj.RegisterDriver_SQLLite(name, email, phone_number, license_number, car_number,
        //            Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer, inmemoryobj.myConnection);
        //            */
        //            DTOuterData = inmemoryobj.Get_Driver_Details_SQLLite();

        //            if (DTOuterData.Rows.Count > 0)
        //            {
        //                foreach (DataRow dataRow_outer in DTOuterData.Rows)
        //                {
        //                    JObject Data_OBJ_Outer = new JObject();
        //                    string Driver_Latitude_Returned = string.Empty;
        //                    string Driver_Longitude_Returned = string.Empty;
        //                    string Kms_Calculated = string.Empty;
        //                    string name_Returned = string.Empty;
        //                    string car_number_Returned = string.Empty;
        //                    string phone_number_Returned = string.Empty;
        //                    string To_Include_Record = "0";
        //                    foreach (DataColumn col in DTOuterData.Columns)
        //                    {
        //                        if (col.ColumnName == "Err_Msg")
        //                        {
        //                            string err_Msg = string.Empty;
        //                            err_Msg = dataRow_outer[col.ColumnName].ToString();
        //                            JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", err_Msg));
        //                            return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //                        }
        //                        else
        //                        {
        //                            if (Data_OBJ_Outer.Property(col.ColumnName) != null)
        //                            {
        //                                //Don't Add in this case. Duplicate Key
        //                            }
        //                            else
        //                            {
        //                                //Data_OBJ_Outer.Add(new JProperty(col.ColumnName, dataRow_outer[col.ColumnName]));
        //                            }
        //                        }
        //                    }

        //                    if (dataRow_outer.Table.Columns["Err_Msg"] != null)
        //                    {
        //                        string err_Msg = string.Empty;
        //                        err_Msg = dataRow_outer["Err_Msg"].ToString().Trim();
        //                        JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", err_Msg));
        //                        return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //                    }
        //                    else
        //                    {
        //                        if (dataRow_outer.Table.Columns["Name"] != null)
        //                        {
        //                            name_Returned = dataRow_outer["Name"].ToString().Trim();
        //                        }
        //                        if (dataRow_outer.Table.Columns["Car_Number"] != null)
        //                        {
        //                            car_number_Returned = dataRow_outer["Car_Number"].ToString().Trim();
        //                        }
        //                        if (dataRow_outer.Table.Columns["phone_number"] != null)
        //                        {
        //                            phone_number_Returned = dataRow_outer["phone_number"].ToString().Trim();
        //                        }
        //                        if (dataRow_outer.Table.Columns["Driver_Latitude"] != null)
        //                        {
        //                            Driver_Latitude_Returned = dataRow_outer["Driver_Latitude"].ToString().Trim();
        //                        }
        //                        if (dataRow_outer.Table.Columns["Driver_Longitude"] != null)
        //                        {
        //                            Driver_Longitude_Returned = dataRow_outer["Driver_Longitude"].ToString().Trim();
        //                        }

        //                        Position pos1 = new Position();
        //                        pos1.Latitude = Convert.ToDouble(Latitude_Customer);
        //                        pos1.Longitude = Convert.ToDouble(Longitude_Customer);

        //                        Position pos2 = new Position();
        //                        pos2.Latitude = Convert.ToDouble(Driver_Latitude_Returned);
        //                        pos2.Longitude = Convert.ToDouble(Driver_Longitude_Returned);

        //                        double result = 0.0;
        //                        result=inmemoryobj.Distance(pos1, pos2, DistanceType.Kilometers);

        //                        if (result <= 4.0)
        //                        {
        //                            To_Include_Record = "1";
        //                        }

        //                        if (To_Include_Record == "1")
        //                        {
        //                            cabs_Found = "1";
        //                            //cabs_Found = "0";
        //                            Data_OBJ_Outer.Add(new JProperty("name", name_Returned));
        //                            Data_OBJ_Outer.Add(new JProperty("car_number", car_number_Returned));
        //                            Data_OBJ_Outer.Add(new JProperty("phone_number", phone_number_Returned));
        //                            //Data_OBJ_Outer.Add(new JProperty("KM", result));
        //                        }
                                
        //                    }

                                
        //                    DataArray.Add(Data_OBJ_Outer);
        //                }
        //                if (cabs_Found == "1") 
        //                {
        //                    JO = new JObject(new JProperty("available_cabs", DataArray));
        //                }
        //                else
        //                {
        //                    JO = new JObject(new JProperty("message", "No cabs available"));
        //                }
                        
        //                return StatusCode(200, JO);
        //            }
        //            else
        //            {
        //                JO = new JObject(new JProperty("message", "No cabs available"));
        //                return StatusCode((int)HttpStatusCode.OK, JO);
        //            }
        //        }

                
        //    }
        //    catch (Exception ex)
        //    {
        //        JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", ex.ToString()));
        //        return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //    }
        //}
        //#endregion
        
        #region available_cabs
        [HttpPost("available_cabs")]
        public IActionResult available_cabs(JObject jObjData)
        {
            JArray DataArray = new JArray();
            JObject JO = new JObject();
            try
            {
                string Latitude_Customer = jObjData.Property("latitude") != null ? jObjData.Property("latitude").Value.ToString() : null;
                string Longitude_Customer = jObjData.Property("longitude") != null ? jObjData.Property("longitude").Value.ToString() : null;

                Double num = 0;
                bool isDouble_Latitude = Double.TryParse(Latitude_Customer, out num);
                bool isDouble_Longitude = Double.TryParse(Longitude_Customer, out num);

                Console.WriteLine(Latitude_Customer);
                /*
                string Param = classes.Param;
                string Driver_ID = classes.Driver_ID;
                string Latitude_Driver = classes.Latitude_Driver;
                string Longitude_Driver = classes.Longitude_Driver;
                string Latitude_Customer = classes.Latitude_Customer;
                string Longitude_Customer = classes.Longitude_Customer;
                */

                string Param = "Get_Nearby_Drivers";
                string name = string.Empty;
                string email = string.Empty;
                string phone_number = string.Empty;
                string license_number = string.Empty;
                string car_number = string.Empty;
                string Driver_ID = string.Empty;
                string Latitude_Driver = string.Empty;
                string Longitude_Driver = string.Empty;

                string isError = "0";
                string Error_Text = string.Empty;
                #region Validations
                if (isDouble_Latitude == false || isDouble_Longitude == false)
                {
                    isError = "1";
                    Error_Text = "Please Enter valid Latitude/Longitude";
                }else if (string.IsNullOrEmpty(Latitude_Customer) || string.IsNullOrEmpty(Longitude_Customer))
                {
                    isError = "1";
                    Error_Text = "Please Enter all the required fields";
                }else if (Latitude_Customer.IndexOf('.') == -1 || Longitude_Customer.IndexOf('.') == -1)
                {
                    isError = "1";
                    Error_Text = "Latitude / Longitude is invalid";
                }else if (isError == "1")
                {
                    JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", Error_Text));
                    return StatusCode((int)HttpStatusCode.BadRequest, JO);
                }
                #endregion

                DataTable DTOuterData;
                string cabs_Found = "0";
                if (Function_To_Use == "1")
                {
                    DTOuterData = inmemoryobj.RegisterDriver_SQLServer(name, email, phone_number, license_number, car_number,
                    Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer);

                    if (DTOuterData.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow_outer in DTOuterData.Rows)
                        {
                            JObject Data_OBJ_Outer = new JObject();
                            foreach (DataColumn col in DTOuterData.Columns)
                            {
                                if (col.ColumnName == "Err_Msg")
                                {
                                    string err_Msg = string.Empty;
                                    err_Msg = dataRow_outer[col.ColumnName].ToString();
                                    JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", err_Msg));
                                    return StatusCode((int)HttpStatusCode.BadRequest, JO);
                                }
                                else
                                {
                                    if (Data_OBJ_Outer.Property(col.ColumnName) != null)
                                    {
                                        //Don't Add in this case. Duplicate Key
                                    }
                                    else
                                    {
                                        Data_OBJ_Outer.Add(new JProperty(col.ColumnName, dataRow_outer[col.ColumnName]));
                                    }
                                }
                            }

                            DataArray.Add(Data_OBJ_Outer);
                        }
                        JO = new JObject(new JProperty("available_cabs", DataArray));
                        return StatusCode(200, JO);
                    }
                    else
                    {
                        JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", "No cabs available"));
                        return StatusCode((int)HttpStatusCode.OK, JO);
                    }
                }
                else
                {
                    /*
                    DTOuterData = inmemoryobj.RegisterDriver_SQLLite(name, email, phone_number, license_number, car_number,
                    Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer, inmemoryobj.myConnection);
                    */
                    DTOuterData = inmemoryobj.Get_Driver_Details_SQLLite();

                    if (DTOuterData.Rows.Count > 0)
                    {
                        foreach (DataRow dataRow_outer in DTOuterData.Rows)
                        {
                            JObject Data_OBJ_Outer = new JObject();
                            string Driver_Latitude_Returned = string.Empty;
                            string Driver_Longitude_Returned = string.Empty;
                            string Kms_Calculated = string.Empty;
                            string name_Returned = string.Empty;
                            string car_number_Returned = string.Empty;
                            string phone_number_Returned = string.Empty;
                            string To_Include_Record = "0";
                            foreach (DataColumn col in DTOuterData.Columns)
                            {
                                if (col.ColumnName == "Err_Msg")
                                {
                                    string err_Msg = string.Empty;
                                    err_Msg = dataRow_outer[col.ColumnName].ToString();
                                    JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", err_Msg));
                                    return StatusCode((int)HttpStatusCode.BadRequest, JO);
                                }
                                else
                                {
                                    if (Data_OBJ_Outer.Property(col.ColumnName) != null)
                                    {
                                        //Don't Add in this case. Duplicate Key
                                    }
                                    else
                                    {
                                        //Data_OBJ_Outer.Add(new JProperty(col.ColumnName, dataRow_outer[col.ColumnName]));
                                    }
                                }
                            }

                            if (dataRow_outer.Table.Columns["Err_Msg"] != null)
                            {
                                string err_Msg = string.Empty;
                                err_Msg = dataRow_outer["Err_Msg"].ToString().Trim();
                                JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", err_Msg));
                                return StatusCode((int)HttpStatusCode.BadRequest, JO);
                            }
                            else
                            {
                                if (dataRow_outer.Table.Columns["Name"] != null)
                                {
                                    name_Returned = dataRow_outer["Name"].ToString().Trim();
                                }
                                if (dataRow_outer.Table.Columns["Car_Number"] != null)
                                {
                                    car_number_Returned = dataRow_outer["Car_Number"].ToString().Trim();
                                }
                                if (dataRow_outer.Table.Columns["phone_number"] != null)
                                {
                                    phone_number_Returned = dataRow_outer["phone_number"].ToString().Trim();
                                }
                                if (dataRow_outer.Table.Columns["Driver_Latitude"] != null)
                                {
                                    Driver_Latitude_Returned = dataRow_outer["Driver_Latitude"].ToString().Trim();
                                }
                                if (dataRow_outer.Table.Columns["Driver_Longitude"] != null)
                                {
                                    Driver_Longitude_Returned = dataRow_outer["Driver_Longitude"].ToString().Trim();
                                }

                                Position pos1 = new Position();
                                pos1.Latitude = Convert.ToDouble(Latitude_Customer);
                                pos1.Longitude = Convert.ToDouble(Longitude_Customer);

                                Position pos2 = new Position();
                                pos2.Latitude = Convert.ToDouble(Driver_Latitude_Returned);
                                pos2.Longitude = Convert.ToDouble(Driver_Longitude_Returned);

                                double result = 0.0;
                                result = inmemoryobj.Distance(pos1, pos2, DistanceType.Kilometers);

                                if (result <= 4.0)
                                {
                                    To_Include_Record = "1";
                                }

                                if (To_Include_Record == "1")
                                {
                                    cabs_Found = "1";
                                    //cabs_Found = "0";
                                    Data_OBJ_Outer.Add(new JProperty("name", name_Returned));
                                    Data_OBJ_Outer.Add(new JProperty("car_number", car_number_Returned));
                                    Data_OBJ_Outer.Add(new JProperty("phone_number", phone_number_Returned));
                                    //Data_OBJ_Outer.Add(new JProperty("KM", result));
                                }

                            }


                            DataArray.Add(Data_OBJ_Outer);
                        }
                        if (cabs_Found == "1")
                        {
                            JO = new JObject(new JProperty("available_cabs", DataArray));
                        }
                        else
                        {
                            JO = new JObject(new JProperty("message", "No cabs available"));
                        }

                        return StatusCode(200, JO);
                    }
                    else
                    {
                        JO = new JObject(new JProperty("message", "No cabs available"));
                        return StatusCode((int)HttpStatusCode.OK, JO);
                    }
                }


            }
            catch (Exception ex)
            {
                string Err_Msg = string.Empty;
                Err_Msg = ex.ToString();
                if (Err_Msg.IndexOf("System.FormatException") != -1)
                {
                    Err_Msg = "Invalid Format. Please Check!!";
                }
                
                JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", Err_Msg));
                return StatusCode((int)HttpStatusCode.BadRequest, JO);
            }
        }
        #endregion
    }
}