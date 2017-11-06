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
    [TestClass]
    public class ParkSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=campground;Integrated Security=True";
        private int parks;

        [TestInitialize]
        public void Initialize()
        {
            tran = new TransactionScope();
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand command;
                conn.Open();

                command = new SqlCommand("SELECT COUNT(*) FROM park", conn);
                parks = (int)command.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Complete();
            //tran.Dispose();
        }

        [TestMethod]
        public void GetAllParksTest()
        {
            ParkSqlDAL parkDAL = new ParkSqlDAL(connectionString);
            List<Park> parkList = parkDAL.GetAllParks();

            Assert.IsNotNull(parkList);
            Assert.AreEqual(parks, parkList.Count);
        }


    }
}
