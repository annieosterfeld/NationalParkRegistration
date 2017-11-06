using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.Models;
using System.Data.SqlClient;
using Capstone.DAL;

namespace Capstone
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sample Code to get a connection string from the
            // App.Config file
            // Use this so that you don't need to copy your connection string all over your code!
            string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;

            //ParkSystemCLI cli = new ParkSystemCLI();
            //cli.RunCLI();
            ParkSystemCLI2 cli = new ParkSystemCLI2();
            cli.RunCLI();







            //CampgroundSqlDAL campground = new CampgroundSqlDAL(connectionString);
            //List<Campground> newList = campground.GetAllCampgrounds();
            //foreach (Campground value in newList)
            //{
            //    Console.WriteLine(value.ToString());
            //}


            //SiteSqlDAL site = new SiteSqlDAL(connectionString);
            //List<Site> newSite = site.GetAllSites();
            //foreach (Site value in newSite)
            //{
            //    Console.WriteLine(value.ToString());
            //}

            //SiteSqlDAL thisSite = new SiteSqlDAL(connectionString);
            //List<Site> top5 = thisSite.GetTop5Sites();
            //foreach (Site value in top5)
            //{
            //    Console.WriteLine(value.ToString());
            //}

            //List<Site> available = thisSite.GetAvailableSites(1, Convert.ToDateTime("2017-10-03"), Convert.ToDateTime("2017-10-08"));
            //foreach (Site value in available)
            //{
            //    string cost =  thisSite.GetCost(1, Convert.ToDateTime("10/3/2017"), Convert.ToDateTime("10/8/2017"));
            //    Console.WriteLine(value.ToString() + cost);
            //}

            //ReservationSqlDAL reservation = new ReservationSqlDAL(connectionString);
            //Console.WriteLine("The reservation has been made and the confirmation ID is: " + reservation.CreateReservation(1, "Rick", Convert.ToDateTime("10/3/2017"), Convert.ToDateTime("10/8/2017")).ToString());

            //ParkSqlDAL parkSqlDAL = new ParkSqlDAL(connectionString);
            //List<Park> parkList = parkSqlDAL.GetAllParks();
            //foreach(Park park in parkList)
            //{
            //    Console.WriteLine(park.ToString());
            //}
        }
    }
}
