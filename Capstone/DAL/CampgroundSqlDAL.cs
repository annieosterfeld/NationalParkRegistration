using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class CampgroundSqlDAL
    {
        private string connectionString;

        public CampgroundSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Campground> GetCampgrounds(Park selectedPark)
        {
            List<Campground> campgrounds = new List<Campground>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM campground WHERE park_id = @park_id", conn);
                    command.Parameters.AddWithValue("@park_id", selectedPark.Park_id);

                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        campgrounds.Add(GetCampgroundFromRow(results));
                    }
                }

            }
            catch (SqlException)
            {
                throw;
            }

            return campgrounds;
        }

        //public string GetCost(int campground_id, DateTime from_date, DateTime to_date)
        //{
        //    int numOfDays = (to_date - from_date).Days;
        //    decimal cost = 0;
        //    try
        //    {
        //        using (SqlConnection conn = new SqlConnection(connectionString))
        //        {
        //            conn.Open();
        //            SqlCommand command = new SqlCommand("SELECT daily_fee FROM campground WHERE campground_id = @campground_id;", conn);
        //            command.Parameters.AddWithValue("@campground_id", campground_id);

        //            cost += (decimal)command.ExecuteScalar();
        //        }
        //    }
        //    catch (SqlException)
        //    {
        //        throw;
        //    }
        //    cost *= numOfDays;
        //    string costString = $"${cost}";

        //    return costString;
        //}
        private Campground GetCampgroundFromRow(SqlDataReader results)
        {
            Campground newCampground = new Campground();

            newCampground.Campground_id = Convert.ToInt32(results["campground_id"]);
            newCampground.Park_id = Convert.ToInt32(results["park_id"]);
            newCampground.Campground_name = Convert.ToString(results["name"]);
            newCampground.Open_from_month = Convert.ToInt32(results["open_from_mm"]);
            newCampground.Open_to_month = Convert.ToInt32(results["open_to_mm"]);
            newCampground.Daily_fee = Convert.ToInt32(results["daily_fee"]);

            return newCampground;
        }
    }
}
