using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Capstone.Models;
using Capstone;
using System.Configuration;

namespace Capstone.DAL
{
    public class ParkSqlDAL
    {
        private string connectionString;

        public ParkSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Park> GetAllParks()
        {
            List<Park> parkList = new List<Park>();

            try
            {

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand command = new SqlCommand("SELECT * FROM park ORDER BY name ASC;", conn);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        parkList.Add(CreateFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }

            return parkList;
        }
       
        
        private Park CreateFromRow(SqlDataReader results)
        {
            Park park = new Park();
            park.Park_id = Convert.ToInt32(results["park_id"]);
            park.Park_name = Convert.ToString(results["name"]);
            park.Park_location = Convert.ToString(results["location"]);
            park.Established_dateTime = Convert.ToDateTime(results["establish_date"]);
            park.Area = Convert.ToInt32(results["area"]);
            park.Annual_visit_count = Convert.ToInt32(results["visitors"]);
            park.Description = Convert.ToString(results["description"]);

            return park;
        }
    }
}
