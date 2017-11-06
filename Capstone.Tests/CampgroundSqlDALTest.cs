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
    public class CampgroundSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=campground;Integrated Security=True";
        private int campgrounds;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command;
                conn.Open();

                command = new SqlCommand("SELECT COUNT(*) FROM campground", conn);
                campgrounds = (int)command.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void GetAllCampgroundsTest()
        {
            CampgroundSqlDAL campgroundDAL = new CampgroundSqlDAL(connectionString);
            List<Campground> campgroundsList = campgroundDAL.GetAllCampgrounds();

            Assert.IsNotNull(campgroundsList);
            Assert.AreEqual(campgrounds, campgroundsList.Count);
        }


    }
}
