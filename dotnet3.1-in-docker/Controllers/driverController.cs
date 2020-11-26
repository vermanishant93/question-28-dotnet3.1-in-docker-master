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
namespace dotnet3._1_in_docker.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class driverController : Controller
    {
        InMemoryDB inmemoryobj = new InMemoryDB();
        string Function_To_Use = "2";      //1: SQL Server, 2: SQLite
        private readonly ILogger<driverController> _logger;

        public driverController(ILogger<driverController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        //#region Register        
        //[HttpPost("register")]
        //public IActionResult register(Model.Classes.registerDriver classes)
        //{            
        //    JArray DataArray = new JArray();
        //    JObject JO = new JObject();
        //    try
        //    {
        //        string name = classes.name;
        //        string email = classes.email;
        //        string phone_number = classes.phone_number;
        //        string license_number = classes.license_number;
        //        string car_number = classes.car_number;
        //        string isError = "0";
        //        string Error_Text = string.Empty;

        //        #region Validations
        //        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(phone_number)
        //            || string.IsNullOrEmpty(license_number) || string.IsNullOrEmpty(car_number))
        //        {
        //            isError = "1";
        //            Error_Text = "Please Enter all the required fields";

        //        }

        //        if (email.IndexOf('@') == -1)
        //        {
        //            isError = "1";
        //            Error_Text = "Email is invalid";
        //        }

        //        if (phone_number.Length != 10)
        //        {
        //            isError = "1";
        //            Error_Text = "Phone Number is invalid";
        //        }
        //        if (isError == "1")
        //        {
        //            JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", Error_Text));
        //            return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //        }
        //        #endregion

        //        /*
        //        string Param = classes.Param;
        //        string Driver_ID = classes.Driver_ID;
        //        string Latitude_Driver = classes.Latitude_Driver;
        //        string Longitude_Driver = classes.Longitude_Driver;
        //        string Latitude_Customer = classes.Latitude_Customer;
        //        string Longitude_Customer = classes.Longitude_Customer;
        //        */

        //        string Param = "Insert_Details";
        //        string Driver_ID = string.Empty;
        //        string Latitude_Driver = string.Empty;
        //        string Longitude_Driver = string.Empty;
        //        string Latitude_Customer = string.Empty;
        //        string Longitude_Customer = string.Empty;

        //        DataTable DTOuterData;
        //        if (Function_To_Use == "1")
        //        {
        //            DTOuterData = inmemoryobj.RegisterDriver_SQLServer(name, email, phone_number, license_number, car_number,
        //            Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer);
        //        }
        //        else
        //        {
        //            DTOuterData = inmemoryobj.RegisterDriver_SQLLite(name, email, phone_number, license_number, car_number,
        //            Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer, inmemoryobj.myConnection);
        //        }

        //        if (DTOuterData.Rows.Count > 0)
        //        {
        //            foreach (DataRow dataRow_outer in DTOuterData.Rows)
        //            {
        //                JObject Data_OBJ_Outer = new JObject();
        //                foreach (DataColumn col in DTOuterData.Columns)
        //                {
        //                    if (col.ColumnName == "Err_Msg")
        //                    {
        //                        string err_Msg = string.Empty;
        //                        err_Msg = dataRow_outer[col.ColumnName].ToString();
        //                        //error.InsertErrorDetails(Base_API_Name, err_Msg, dataObj, Is_Job);

        //                        //return new JObject(new JProperty("StatusCode", 404), new JProperty("Message", err_Msg), new JProperty("API_Name", Base_API_Name), new JProperty("Data", DataArray));
        //                        JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", err_Msg));
        //                        return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //                    }
        //                    else
        //                    {
        //                        if (Data_OBJ_Outer.Property(col.ColumnName) != null)
        //                        {
        //                            //Don't Add in this case. Duplicate Key
        //                        }
        //                        else
        //                        {
        //                            Data_OBJ_Outer.Add(new JProperty(col.ColumnName, dataRow_outer[col.ColumnName]));
        //                        }
        //                    }
        //                }

        //                JO = Data_OBJ_Outer;
        //            }
        //            return StatusCode(201, JO);
        //        }
        //        else
        //        {
        //            JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", "No Records Found"));
        //            return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        string Err_Msg = string.Empty;
        //        Err_Msg = ex.ToString();
        //        if (Err_Msg.IndexOf("System.FormatException") != -1)
        //        {
        //            Err_Msg = "Invalid Format. Please Check!!";
        //        }

        //        JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", Err_Msg));
        //        return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //    }
        //}
        //#endregion

        #region Register        
        [HttpPost("register")]
        public IActionResult register(JObject jObjData)
        {
            JArray DataArray = new JArray();
            JObject JO = new JObject();
            try
            {

                string name = jObjData.Property("name") != null ? jObjData.Property("name").Value.ToString() : null;
                string email = jObjData.Property("email") != null ? jObjData.Property("email").Value.ToString() : null;
                string phone_number = jObjData.Property("phone_number") != null ? jObjData.Property("phone_number").Value.ToString() : null;
                string license_number = jObjData.Property("license_number") != null ? jObjData.Property("license_number").Value.ToString() : null;
                string car_number = jObjData.Property("car_number") != null ? jObjData.Property("car_number").Value.ToString() : null;
                
                string isError = "0";
                string Error_Text = string.Empty;

                #region Validations
                if (string.IsNullOrEmpty(name))
                {
                    isError = "1";
                    Error_Text = "Please Enter name";

                }else if (string.IsNullOrEmpty(email))
                {
                    isError = "1";
                    Error_Text = "Please Enter email";
                }else if (string.IsNullOrEmpty(phone_number))
                {
                    isError = "1";
                    Error_Text = "Please Enter phone Number";
                }else if (string.IsNullOrEmpty(license_number))
                {
                    isError = "1";
                    Error_Text = "Please Enter License Number";
                }else if (string.IsNullOrEmpty(car_number))
                {
                    isError = "1";
                    Error_Text = "Please Enter Car Number";
                }
                else if (email.IndexOf('@') == -1)
                {
                    isError = "1";
                    Error_Text = "Email is invalid";
                }else if (phone_number.Length != 10)
                {
                    isError = "1";
                    Error_Text = "Phone Number is invalid";
                }
                
                if (isError == "1")
                {
                    JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", Error_Text));
                    return StatusCode((int)HttpStatusCode.BadRequest, JO);
                }
                #endregion

                /*
                string Param = classes.Param;
                string Driver_ID = classes.Driver_ID;
                string Latitude_Driver = classes.Latitude_Driver;
                string Longitude_Driver = classes.Longitude_Driver;
                string Latitude_Customer = classes.Latitude_Customer;
                string Longitude_Customer = classes.Longitude_Customer;
                */

                string Param = "Insert_Details";
                string Driver_ID = string.Empty;
                string Latitude_Driver = string.Empty;
                string Longitude_Driver = string.Empty;
                string Latitude_Customer = string.Empty;
                string Longitude_Customer = string.Empty;

                DataTable DTOuterData;
                if (Function_To_Use == "1")
                {
                    DTOuterData = inmemoryobj.RegisterDriver_SQLServer(name, email, phone_number, license_number, car_number,
                    Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer);
                }
                else
                {
                    DTOuterData = inmemoryobj.RegisterDriver_SQLLite(name, email, phone_number, license_number, car_number,
                    Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer, inmemoryobj.myConnection);
                }

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
                                //error.InsertErrorDetails(Base_API_Name, err_Msg, dataObj, Is_Job);

                                //return new JObject(new JProperty("StatusCode", 404), new JProperty("Message", err_Msg), new JProperty("API_Name", Base_API_Name), new JProperty("Data", DataArray));
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

                        JO = Data_OBJ_Outer;
                    }
                    return StatusCode(201, JO);
                }
                else
                {
                    JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", "No Records Found"));
                    return StatusCode((int)HttpStatusCode.BadRequest, JO);
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

        //#region Send Location        
        //[HttpPost("{id}/sendLocation")]
        //public IActionResult sendLocation(int id, Model.Classes.DriverLocation classes)
        //{
        //    JArray DataArray = new JArray();
        //    JObject JO = new JObject();
        //    try
        //    {
        //        double num = 0;
        //        string Latitude_Driver = classes.latitude.ToString();
        //        string Longitude_Driver = classes.longitude.ToString();

        //        bool isDouble_Latitude = Double.TryParse(Latitude_Driver, out num);
        //        bool isDouble_Longitude = Double.TryParse(Longitude_Driver, out num);

        //        /*
        //        string Param = classes.Param;
        //        string Driver_ID = classes.Driver_ID;
        //        string Latitude_Driver = classes.Latitude_Driver;
        //        string Longitude_Driver = classes.Longitude_Driver;
        //        string Latitude_Customer = classes.Latitude_Customer;
        //        string Longitude_Customer = classes.Longitude_Customer;
        //        */

        //        string Param = "Insert_Location";
        //        string name = string.Empty;
        //        string email = string.Empty;
        //        string phone_number = string.Empty;
        //        string license_number = string.Empty;
        //        string car_number = string.Empty;
        //        string Driver_ID = string.Empty;
        //        string Latitude_Customer = string.Empty;
        //        string Longitude_Customer = string.Empty;
        //        string isError = "0";
        //        string Error_Text = string.Empty;
        //        Driver_ID = id.ToString();

        //        #region Validations
        //        if (isDouble_Latitude == false || isDouble_Longitude == false)
        //        {
        //            isError = "1";
        //            Error_Text = "Please Enter valid Latitude/Longitude";
        //        }

        //        if (string.IsNullOrEmpty(Latitude_Driver) || string.IsNullOrEmpty(Longitude_Driver) || string.IsNullOrEmpty(Driver_ID))
        //        {
        //            isError = "1";
        //            Error_Text = "Please Enter all the required fields";
        //        }

        //        if (Latitude_Driver.IndexOf('.') == -1 || Longitude_Driver.IndexOf('.') == -1)
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
        //        if (Function_To_Use == "1")
        //        {
        //            DTOuterData = inmemoryobj.RegisterDriver_SQLServer(name, email, phone_number, license_number, car_number,
        //            Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer);
        //        }
        //        else
        //        {
        //            DTOuterData = inmemoryobj.RegisterDriver_SQLLite(name, email, phone_number, license_number, car_number,
        //            Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer, inmemoryobj.myConnection);
        //        }                

        //        if (DTOuterData.Rows.Count > 0)
        //        {
        //            foreach (DataRow dataRow_outer in DTOuterData.Rows)
        //            {
        //                JObject Data_OBJ_Outer = new JObject();
        //                foreach (DataColumn col in DTOuterData.Columns)
        //                {
        //                    if (col.ColumnName == "Err_Msg")
        //                    {
        //                        string err_Msg = string.Empty;
        //                        err_Msg = dataRow_outer[col.ColumnName].ToString();
        //                        //error.InsertErrorDetails(Base_API_Name, err_Msg, dataObj, Is_Job);

        //                        //return new JObject(new JProperty("StatusCode", 404), new JProperty("Message", err_Msg), new JProperty("API_Name", Base_API_Name), new JProperty("Data", DataArray));
        //                        JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", err_Msg));
        //                        return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //                    }
        //                    else
        //                    {
        //                        if (Data_OBJ_Outer.Property(col.ColumnName) != null)
        //                        {
        //                            //Don't Add in this case. Duplicate Key
        //                        }
        //                        else
        //                        {
        //                            Data_OBJ_Outer.Add(new JProperty(col.ColumnName, dataRow_outer[col.ColumnName]));
        //                        }
        //                    }
        //                }

        //                JO = Data_OBJ_Outer;
        //            }
        //            return StatusCode(202, JO);
        //        }
        //        else
        //        {
        //            JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", "No Records Found"));
        //            return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        string Err_Msg = string.Empty;
        //        Err_Msg = ex.ToString();
        //        if (Err_Msg.IndexOf("System.FormatException") != -1)
        //        {
        //            Err_Msg = "Invalid Format. Please Check!!";
        //        }

        //        JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", Err_Msg));
        //        return StatusCode((int)HttpStatusCode.BadRequest, JO);
        //    }
        //}
        //#endregion

        #region Send Location        
        [HttpPost("{id}/sendLocation")]
        public IActionResult sendLocation(int id, JObject jObjData)
        {
            JArray DataArray = new JArray();
            JObject JO = new JObject();
            try
            {
                double num = 0;
                string Latitude_Driver = jObjData.Property("latitude") != null ? jObjData.Property("latitude").Value.ToString() : null;
                string Longitude_Driver = jObjData.Property("longitude") != null ? jObjData.Property("longitude").Value.ToString() : null;

                bool isDouble_Latitude = Double.TryParse(Latitude_Driver, out num);
                bool isDouble_Longitude = Double.TryParse(Longitude_Driver, out num);

                /*
                string Param = classes.Param;
                string Driver_ID = classes.Driver_ID;
                string Latitude_Driver = classes.Latitude_Driver;
                string Longitude_Driver = classes.Longitude_Driver;
                string Latitude_Customer = classes.Latitude_Customer;
                string Longitude_Customer = classes.Longitude_Customer;
                */

                string Param = "Insert_Location";
                string name = string.Empty;
                string email = string.Empty;
                string phone_number = string.Empty;
                string license_number = string.Empty;
                string car_number = string.Empty;
                string Driver_ID = string.Empty;
                string Latitude_Customer = string.Empty;
                string Longitude_Customer = string.Empty;
                string isError = "0";
                string Error_Text = string.Empty;
                Driver_ID = id.ToString();

                #region Validations
                if (isDouble_Latitude == false || isDouble_Longitude == false)
                {
                    isError = "1";
                    Error_Text = "Please Enter valid Latitude/Longitude";
                }else if (string.IsNullOrEmpty(Latitude_Driver) || string.IsNullOrEmpty(Longitude_Driver) || string.IsNullOrEmpty(Driver_ID))
                {
                    isError = "1";
                    Error_Text = "Please Enter all the required fields";
                }else if (Latitude_Driver.IndexOf('.') == -1 || Longitude_Driver.IndexOf('.') == -1)
                {
                    isError = "1";
                    Error_Text = "Latitude / Longitude is invalid";
                }

                if (isError == "1")
                {
                    JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", Error_Text));
                    return StatusCode((int)HttpStatusCode.BadRequest, JO);
                }
                #endregion

                DataTable DTOuterData;
                if (Function_To_Use == "1")
                {
                    DTOuterData = inmemoryobj.RegisterDriver_SQLServer(name, email, phone_number, license_number, car_number,
                    Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer);
                }
                else
                {
                    DTOuterData = inmemoryobj.RegisterDriver_SQLLite(name, email, phone_number, license_number, car_number,
                    Param, Driver_ID, Latitude_Driver, Longitude_Driver, Latitude_Customer, Longitude_Customer, inmemoryobj.myConnection);
                }

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
                                //error.InsertErrorDetails(Base_API_Name, err_Msg, dataObj, Is_Job);

                                //return new JObject(new JProperty("StatusCode", 404), new JProperty("Message", err_Msg), new JProperty("API_Name", Base_API_Name), new JProperty("Data", DataArray));
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

                        JO = Data_OBJ_Outer;
                    }
                    return StatusCode(202, JO);
                }
                else
                {
                    JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", "No Records Found"));
                    return StatusCode((int)HttpStatusCode.BadRequest, JO);
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