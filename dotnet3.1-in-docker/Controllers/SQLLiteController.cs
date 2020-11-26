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
    public class SQLLiteController : Controller
    {
        InMemoryDB inmemoryobj = new InMemoryDB();
        string Function_To_Use = "1";      //1: SQL Server, 2: SQLite/memcache/Redif
        private readonly ILogger<SQLLiteController> _logger;

        public SQLLiteController(ILogger<SQLLiteController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Insert Data
        [HttpPost("InsertData")]
        public IActionResult InsertData(Model.Classes.DriverLocation classes)
        {
            JArray DataArray = new JArray();
            JObject JO = new JObject();
            try
            {

                string Latitude_Customer = classes.latitude;
                string Longitude_Customer = classes.longitude;


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
            catch (Exception ex)
            {
                JO = new JObject(new JProperty("status", "failure"), new JProperty("reason", ex.ToString()));
                return StatusCode((int)HttpStatusCode.BadRequest, JO);
            }
        }
        #endregion
    }
}