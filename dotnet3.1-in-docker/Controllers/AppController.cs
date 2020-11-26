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
    //[Route("[controller]")]
    
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> _logger;
        public AppController(ILogger<AppController> logger)
        {
            _logger = logger;
        }
        [HttpGet()]
        public string Get()
        {
            return "Hello World!";
        }

        [HttpGet("DualRequest/{id}")]
        public IActionResult DummyRequest(int id, Model.Classes.DriverLocation classes)
        {
            // Code here...
            string Latitude = classes.latitude.ToString();
            string Longitude = classes.longitude.ToString();
            JObject myObject = new JObject();
            myObject.Add(new JProperty("Text", "Hello: " + id));
            myObject.Add(new JProperty("Latitude", Latitude));
            myObject.Add(new JProperty("Longitude", Longitude));            
            
            if (id == 1)
            {
                return StatusCode((int)HttpStatusCode.Accepted, myObject);
            }
            else if (id == 2)
            {
                return StatusCode((int)HttpStatusCode.OK, myObject);
            }
            else if (id == 3)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, myObject);
            }
            else if (id == 4)
            {
                return StatusCode(201, myObject);
            }
            else
            {
                return StatusCode(200, myObject);
            }
        }

        [HttpGet("DualRequestObj/{id}")]
        public IActionResult DummyRequestObj(int id, JObject jObjData)
        {
            // Code here...            
            string Latitude = jObjData.Property("Latitude") != null ? jObjData.Property("Latitude").Value.ToString() : null;
            string Longitude = jObjData.Property("Longitude") != null ? jObjData.Property("Longitude").Value.ToString() : null;

            JObject myObject = new JObject();
            myObject.Add(new JProperty("Text", "Hello: " + id));
            myObject.Add(new JProperty("Latitude", Latitude));
            myObject.Add(new JProperty("Longitude", Longitude));

            if (id == 1)
            {
                return StatusCode((int)HttpStatusCode.Accepted, myObject);
            }
            else if (id == 2)
            {
                return StatusCode((int)HttpStatusCode.OK, myObject);
            }
            else if (id == 3)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, myObject);
            }
            else if (id == 4)
            {
                return StatusCode(201, myObject);
            }
            else
            {
                return StatusCode(200, myObject);
            }
        }
    }
}
