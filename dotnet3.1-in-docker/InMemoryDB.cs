using System;
using System.IO;
using System.Collections.Generic;
using System.Text;

using Microsoft.Data.Sqlite;

using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace dotnet3._1_in_docker
{
    public class InMemoryDB: Exception
    {
        string stringcon = "Data Source = 104.211.90.202; Initial Catalog = MASAPP_DB; User Id = sa; Password=Crystal@12345678;";
        private SqlCommand cmd = new SqlCommand();
        private SQLiteCommand sqlcmd = new SQLiteCommand();

        public SQLiteConnection myConnection;

        public InMemoryDB()
        {
            SQLiteConnection sqlite_conn;
            sqlite_conn = CreateConnection();
            CreateTable(sqlite_conn);
            //InsertData(sqlite_conn);
            //ReadData(sqlite_conn);
        }

        public InMemoryDB(string message)
               : base(message)
        {

        }

        static SQLiteConnection CreateConnection()
        {
            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source=database.db; Version = 3; New = True; Compress = True; ");
         
            // Open the connection:         
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {

            }
            return sqlite_conn;
        }

        static void CreateTable(SQLiteConnection conn)
        {

            SQLiteCommand sqlite_cmd;
            //string Createsql = "DROP TABLE IF EXISTS SampleTable;CREATE TABLE SampleTable (Col1 VARCHAR(20), Col2 INT)";
            //string Createsql1 = "DROP TABLE IF EXISTS SampleTable1; CREATE TABLE SampleTable1 (Col1 VARCHAR(20), Col2 INT)";

            //string Driver_Details_Insert = "CREATE TABLE if not exists Driver_Details ([ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name varchar(100), Email varchar(100), phone_number varchar(100), license_number varchar(100), Car_Number varchar(100))";
            string Driver_Details_Insert = "CREATE TABLE if not exists Driver_Details ([ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, Name varchar(100), Email varchar(100), phone_number varchar(100), license_number varchar(100), Car_Number varchar(100))";
            string Driver_Location_Insert = "CREATE TABLE if not exists Driver_Location (ID INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,Driver_ID int, Latitude varchar(100), Longitude varchar(100))";
            
            //string Driver_Details_Index = string.Empty;
            //Driver_Details_Index = "CREATE UNIQUE INDEX if not exists pk_index ON 'Driver_Details'([Email],[phone_number],[license_number],[Car_Number])";


            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = Driver_Details_Insert;
            sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = Driver_Location_Insert;
            sqlite_cmd.ExecuteNonQuery();
            //sqlite_cmd.CommandText = Driver_Details_Index;
            //sqlite_cmd.ExecuteNonQuery();

        }

        static void InsertData(SQLiteConnection conn)
        {
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test Text ', 1); ";
           sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test1 Text1 ', 2); ";
           sqlite_cmd.ExecuteNonQuery();
            sqlite_cmd.CommandText = "INSERT INTO SampleTable (Col1, Col2) VALUES('Test2 Text2 ', 3); ";
           sqlite_cmd.ExecuteNonQuery();


            sqlite_cmd.CommandText = "INSERT INTO SampleTable1 (Col1, Col2) VALUES('Test3 Text3 ', 3); ";           
            sqlite_cmd.ExecuteNonQuery();

        }

        static void ReadData(SQLiteConnection conn)
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM SampleTable";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
        }
        /*
        public InMemoryDB()
        {
            myConnection = new SQLiteConnection("Data Source=database.sqlite3");
            if (!File.Exists("./database.sqlite3"))
            {
                SQLiteConnection.CreateFile("database.sqlite3");
                System.Console.WriteLine("Database file created");
            }
        }
        */
        public void OpenConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Open)
            {
                myConnection.Open();
            }
        }

        public void CloseConnection()
        {
            if (myConnection.State != System.Data.ConnectionState.Closed)
            {
                myConnection.Close();
            }
        }
       
        public DataTable RegisterDriver_SQLServer(string name, string email, string phone_number, string license_number
            , string car_number, string Param, string Driver_ID, string Latitude_Driver, string Longitude_Driver
            , string Latitude_Customer, string Longitude_Customer)
        {
            DataTable Dt = new DataTable();
            SqlConnection con = new SqlConnection(stringcon);
            try
            {

                if (con.State == ConnectionState.Closed)
                    con.Open();
                cmd = new SqlCommand("REGISTER_DRIVER", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@email", email);
                cmd.Parameters.AddWithValue("@phone_number", phone_number);
                cmd.Parameters.AddWithValue("@license_number", license_number);
                cmd.Parameters.AddWithValue("@car_number", car_number);
                cmd.Parameters.AddWithValue("@Param", Param);
                cmd.Parameters.AddWithValue("@Driver_ID", Driver_ID);
                cmd.Parameters.AddWithValue("@Latitude_Driver", Latitude_Driver);
                cmd.Parameters.AddWithValue("@Longitude_Driver", Longitude_Driver);
                cmd.Parameters.AddWithValue("@Latitude_Customer", Latitude_Customer);
                cmd.Parameters.AddWithValue("@Longitude_Customer", Longitude_Customer);
                cmd.CommandTimeout = 0;
                SqlDataReader dr = cmd.ExecuteReader();
                Dt.Load(dr);
                con.Close();
            }
            catch (System.Exception ex)
            {
                ex.ToString();
                throw;
            }
            finally
            {
                con.Close();
            }
            return Dt;
        }

        ///*
        public DataTable RegisterDriver_SQLLite(string name, string email, string phone_number, string license_number
            , string car_number, string Param, string Driver_ID, string Latitude_Driver, string Longitude_Driver
            , string Latitude_Customer, string Longitude_Customer, SQLiteConnection myConnection)
        {
            DataTable Dt = new DataTable();
            if (Param == "Insert_Details")
            {
                //Dt.Columns.Add(new DataColumn("Return_Status", typeof(Int64)));
                Dt.Columns.Add(new DataColumn("id", typeof(Int64)));
                Dt.Columns.Add(new DataColumn("name", typeof(string)));
                Dt.Columns.Add(new DataColumn("email", typeof(string)));
                Dt.Columns.Add(new DataColumn("phone_number", typeof(string)));
                Dt.Columns.Add(new DataColumn("license_number", typeof(string)));
                Dt.Columns.Add(new DataColumn("car_number", typeof(string)));
            }
            else if (Param == "Insert_Location")
            {                
                Dt.Columns.Add(new DataColumn("status", typeof(string)));                
            }            
            //SqlConnection con = new SqlConnection(stringcon);
            SQLiteConnection con;
            con = CreateConnection();
            try
            {

                #region Validations
                //Validations
                string ValidateQuery = string.Empty;
                if (Param == "Insert_Details")
                {
                    string IsDuplicateFound = "0";
                    sqlcmd = new SQLiteCommand("SELECT 1 from Driver_Details WHERE Email = @email OR phone_number=@phone_number OR license_number=@license_number OR car_number=@car_number", con);
                    sqlcmd.Parameters.AddWithValue("@name", name);
                    sqlcmd.Parameters.AddWithValue("@email", email);
                    sqlcmd.Parameters.AddWithValue("@phone_number", phone_number);
                    sqlcmd.Parameters.AddWithValue("@license_number", license_number);
                    sqlcmd.Parameters.AddWithValue("@car_number", car_number);
                    int count = Convert.ToInt32(sqlcmd.ExecuteScalar());
                    if (count > 0)
                    {
                        IsDuplicateFound = "1";
                    }

                    if (IsDuplicateFound == "1")
                    {                        
                        throw new InMemoryDB("Duplicate Values Found. Please Check!!");
                    }
                }
                #endregion
                
                
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }

                
                string Query = string.Empty;
                //Execute Query Params
                if (Param == "Insert_Details")
                {
                    Query = "Insert Into Driver_Details(`Name`,`Email`,`phone_number`,`license_number`,`car_number`) VALUES (@name,@email,@phone_number,@license_number,@car_number)";
                    sqlcmd = new SQLiteCommand(Query, con);

                    sqlcmd.Parameters.AddWithValue("@name", name);
                    sqlcmd.Parameters.AddWithValue("@email", email);
                    sqlcmd.Parameters.AddWithValue("@phone_number", phone_number);
                    sqlcmd.Parameters.AddWithValue("@license_number", license_number);
                    sqlcmd.Parameters.AddWithValue("@car_number", car_number);
                }else if (Param == "Insert_Location")
                {

                    sqlcmd = new SQLiteCommand("SELECT count(*) from Driver_Location WHERE Driver_ID = @Driver_ID", con);
                    sqlcmd.Parameters.AddWithValue("@Driver_ID", Driver_ID);
                    int count = Convert.ToInt32(sqlcmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // UPDATE STATEMENT
                        Query = "UPDATE Driver_Location SET Latitude = @Latitude_Driver,Longitude = @Longitude_Driver WHERE Driver_ID = @Driver_ID";

                    }
                    else
                    {
                        //INSERT STATEMENT
                        Query = "INSERT INTO Driver_Location(Driver_ID,Latitude,Longitude) VALUES(@Driver_ID, @Latitude_Driver,@Longitude_Driver)";
                    }
                    //Query = @"IF EXISTS(SELECT * FROM Driver_Location WHERE Driver_ID = @Driver_ID) UPDATE Driver_Location SET Latitude = @Latitude_Driver,Longitude = @Longitude_Driver WHERE Driver_ID = @Driver_ID ELSE INSERT INTO Driver_Location(Driver_ID,Latitude,Longitude) VALUES(@Driver_ID, @Latitude_Driver,@Longitude_Driver);";

                    sqlcmd = new SQLiteCommand(Query, con);

                    sqlcmd.Parameters.AddWithValue("@Driver_ID", Driver_ID);
                    sqlcmd.Parameters.AddWithValue("@Latitude_Driver", Latitude_Driver);
                    sqlcmd.Parameters.AddWithValue("@Longitude_Driver", Longitude_Driver);
                }
                    
                sqlcmd.CommandTimeout = 0;
                var result = sqlcmd.ExecuteNonQuery();

                int DriverID = 0;

                //To Fetch New ID Creater
                if (Param == "Insert_Details")
                {
                    DriverID = (int)con.LastInsertRowId;
                }   

                //con.Close();
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
                Console.WriteLine("Rows Added : {0}", result);
                
                // To Return DataRow
                DataRow row = Dt.NewRow();
                if (Param == "Insert_Details")
                {
                    row["id"] = DriverID;
                    row["name"] = name;
                    row["email"] = email;
                    row["phone_number"] = phone_number;
                    row["license_number"] = license_number;
                    row["car_number"] = car_number;
                }
                else if (Param == "Insert_Location")
                {
                    row["status"] = "success";
                }                

                Dt.Rows.Add(row);
                return Dt;

            }
            catch (System.Exception ex)
            {
                DataTable Dt_Err = new DataTable();
                string Err_Msg = string.Empty;
                Err_Msg = ex.ToString();
                if (Err_Msg.IndexOf("constraint failed") != -1)
                {
                    Err_Msg = "Duplicate Values Found. Please Check";
                }else if (Err_Msg.IndexOf("Duplicate Values Found")!= -1)
                {
                    Err_Msg = "Duplicate Values Found. Please Check";
                }
                Dt_Err.Columns.Add(new DataColumn("Err_Msg", typeof(string)));
                DataRow row = Dt_Err.NewRow();
                row["Err_Msg"] = Err_Msg;
                Dt_Err.Rows.Add(row);
                return Dt_Err;
                //ex.ToString();
                //throw;
            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
            }
            return Dt;
        }
        // */

        public DataTable Get_Driver_Details_SQLLite()
        {
            DataTable Dt = new DataTable();            
            SQLiteConnection con;
            con = CreateConnection();
            try
            {
                if (con.State != System.Data.ConnectionState.Open)
                {
                    con.Open();
                }

                string Query = string.Empty;
                
                Query = "SELECT a.Latitude Driver_Latitude, a.Longitude Driver_Longitude, b.Name, b.car_number, b.phone_number from Driver_Location a LEFT JOIN Driver_Details b on a.Driver_ID=b.ID where b.ID IS NOT NULL";
                //sqlcmd = new SQLiteCommand("SELECT b.Latitude Driver_Latitude, b.Longitude Driver_Longitude, a.Name, a.car_number, a.phone_number from Driver_Location a LEFT JOIN Driver_Details b on a.Driver_ID=b.ID", con);
                sqlcmd = new SQLiteCommand(Query, con);
                sqlcmd.CommandTimeout = 0;
                Dt.Load(sqlcmd.ExecuteReader());
                
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
                return Dt;

            }
            catch (System.Exception ex)
            {
                DataTable Dt_Err = new DataTable();
                string Err_Msg = string.Empty;
                Err_Msg = ex.ToString();
                if (Err_Msg.IndexOf("constraint failed") != -1)
                {
                    Err_Msg = "Duplicate Values Found. Please Check";
                }
                Dt_Err.Columns.Add(new DataColumn("Err_Msg", typeof(string)));
                DataRow row = Dt_Err.NewRow();
                row["Err_Msg"] = Err_Msg;
                Dt_Err.Rows.Add(row);
                return Dt_Err;
                //ex.ToString();
                //throw;
            }
            finally
            {
                if (con.State != System.Data.ConnectionState.Closed)
                {
                    con.Close();
                }
            }            
        }


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
