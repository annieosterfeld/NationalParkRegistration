using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Models;
using Capstone.DAL;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Capstone.Tests
{
    [TestClass()]
    public class SiteSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=campground;Integrated Security=True";
        private int sites;
        //private int fiveSites;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand command;

                command = new SqlCommand("SELECT COUNT(*) FROM site", conn);
                sites = (int)command.ExecuteScalar();
                command = new SqlCommand("SELECT TOP 5 * FROM site ORDER BY max_occupancy DESC", conn);
                //fiveSites = (int)command.ExecuteScalar();
            }
        }
        [TestCleanup]
        [TestMethod]
        public void GetAllSitesTest()
        {
            SiteSqlDAL siteDAL = new SiteSqlDAL(connectionString);
            List<Site> allSites = siteDAL.GetAllSites();

            Assert.IsNotNull(allSites);
            Assert.AreEqual(sites, allSites.Count);
        }
        [TestMethod]
        public void GetTop5SitesTest()
        {
            SiteSqlDAL siteDal = new SiteSqlDAL(connectionString);
            List<Site> top5 = siteDal.GetTop5Sites();

            Assert.AreEqual(5, top5.Count);
        }
    }
}
