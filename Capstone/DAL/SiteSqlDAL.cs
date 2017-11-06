using Capstone.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capstone.DAL
{
    public class SiteSqlDAL
    {
        private string connectionString;

        public SiteSqlDAL(string dbConnectionString)
        {
            connectionString = dbConnectionString;
        }

        public List<Site> GetAllSites(Campground selectedCampground)
        {
            List<Site> sites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT * FROM site WHERE campground_id = @campground_id", conn);
                    command.Parameters.AddWithValue("@campground_id", selectedCampground.Campground_id);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        sites.Add(GetSiteFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return sites;
        }
        public List<Site> GetTop5Sites(Campground selectedCampground)
        {
            List<Site> top5Sites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT TOP 5 * FROM site WHERE campground_id = @campground_id ORDER BY max_occupancy DESC;", conn);
                    command.Parameters.AddWithValue("@campground_id", selectedCampground.Campground_id);
                    SqlDataReader results = command.ExecuteReader();
                    while (results.Read())
                    {
                        top5Sites.Add(GetSiteFromRow(results));
                    }
                }
            }
            catch (SqlException)
            {
                throw;
            }
            return top5Sites;
        }
        public List<Site> GetAvailableSites(int campground_id, DateTime from_date, DateTime to_date)
        {
            List<Site> availableSites = new List<Site>();
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT *  from site where campground_id = @campground_id AND site.site_id NOT IN (SELECT site.site_id " +
                        "FROM site JOIN reservation ON reservation.site_id = site.site_id   " +
                        "AND (@from_date BETWEEN from_date AND to_date " +
                        "OR @to_date  BETWEEN from_date AND to_date) " +
                        "AND (from_date  BETWEEN  @from_date  AND @to_date " +
                        "OR to_date  BETWEEN @from_date AND @to_date) " +
                        // "AND (@from_date = from_date " +
                        //"OR @to_date = to_date) " +
                        "GROUP BY site.site_id)", conn);
                    //selecting anything outside of the bounds, 
                    //want to exclude anything that is inside the bounds
                    //want to select all sites except ones with reservations in the bounds
                    command.Parameters.AddWithValue("@from_date", from_date);
                    command.Parameters.AddWithValue("@to_date", to_date);
                    command.Parameters.AddWithValue("@campground_id", campground_id);
                    SqlDataReader results = command.ExecuteReader();

                    while (results.Read())
                    {
                        availableSites.Add(GetSiteFromRow(results));
                    }
                }
            }
            catch (SqlException e)
            {
                throw;
            }


            return availableSites;
        }
        public string GetCost(int campground_id, DateTime from_date, DateTime to_date)
        {
            int numOfDays = (to_date - from_date).Days;
            decimal cost = 0;
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand command = new SqlCommand("SELECT daily_fee FROM campground WHERE campground_id = @campground_id;", conn);
                    command.Parameters.AddWithValue("@campground_id", campground_id);

                    cost += (decimal)command.ExecuteScalar();
                }
            }
            catch (SqlException)
            {
                throw;
            }
            cost *= numOfDays;
            string costString = String.Format("{0:C}", cost);

            return costString;
        }

        private Site GetSiteFromRow(SqlDataReader results)
        {
            Site newSite = new Site();

            newSite.Site_id = Convert.ToInt32(results["site_id"]);
            newSite.Camground_id = Convert.ToInt32(results["campground_id"]);
            newSite.Site_number = Convert.ToInt32(results["site_number"]);
            newSite.Max_occupancy = Convert.ToInt32(results["max_occupancy"]);
            newSite.Accessible = Convert.ToBoolean(results["accessible"]);
            newSite.Max_rv_length = Convert.ToInt32(results["max_rv_length"]);
            newSite.Utilities = Convert.ToBoolean(results["utilities"]);

            return newSite;
        }
    }
}