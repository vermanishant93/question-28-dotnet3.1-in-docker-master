using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet3._1_in_docker
{
    public class CommonData
    {
        //string stringcon = ConfigurationManager.ConnectionStrings["Constring_Backend"].ToString();
        string stringcon = "Data Source = 104.211.90.202; Initial Catalog = MASAPP_DB; User Id = sa; Password=Crystal@12345678;";
        private SqlDataAdapter da = new SqlDataAdapter();
        private SqlCommand cmd = new SqlCommand();

        public enum DistanceType { Miles, Kilometers };

        /// <summary>
        /// Specifies a Latitude / Longitude point.
        /// </summary>
        public struct Position
        {
            public double Latitude;
            public double Longitude;
        }

        //Haversine Formula
        public double Distance(Position pos1, Position pos2, DistanceType type)
        {
            double R = (type == DistanceType.Miles) ? 3960 : 6371;

            double dLat = this.toRadian(pos2.Latitude - pos1.Latitude);
            double dLon = this.toRadian(pos2.Longitude - pos1.Longitude);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                Math.Cos(this.toRadian(pos1.Latitude)) * Math.Cos(this.toRadian(pos2.Latitude)) *
                Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            double c = 2 * Math.Asin(Math.Min(1, Math.Sqrt(a)));
            double d = R * c;

            return d;
        }

       
        /// Convert to Radians.       
        private double toRadian(double val)
        {
            return (Math.PI / 180) * val;
        }
    }
}
