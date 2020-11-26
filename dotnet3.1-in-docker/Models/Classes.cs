using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet3._1_in_docker.Model
{
    public class Classes
    {   
        public class register
        {
            //[Required]
            public string Param { get; set; }
            public string Driver_ID { get; set; }
            public string Latitude_Driver { get; set; }
            public string Longitude_Driver { get; set; }
            public string Latitude_Customer { get; set; }
            public string Longitude_Customer { get; set; }
            public string name { get; set; }
            //[Required]
            public string email { get; set; }
            //[Required]
            public string phone_number { get; set; }
            //[Required]
            public string license_number { get; set; }
            //[Required]
            public string car_number { get; set; }
            
        }

        public class registerDriver
        {
            //[Required]            


            /*
            public string name {
                get
                {
                    if (name == null)
                    {
                        name = string.Empty;
                    }
                    return name;
                }
                //set; 
            
            }
            */

            //[Required]
            public string name { get; set; }
            public string email { get; set; }
            //[Required]
            public string phone_number { get; set; }
            //[Required]
            public string license_number { get; set; }
            //[Required]
            public string car_number { get; set; }

        }

        public class DriverLocation
        {
            //[Required]
            public string latitude { get; set; }
            //[Required]
            public string longitude { get; set; }
        }
    }
}
