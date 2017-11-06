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
using System.Threading;

namespace Capstone.Tests
{
    [TestClass]
    public class ReservationSqlDALTest
    {
        private TransactionScope tran;
        private string connectionString = @"Data Source=.\SQLEXPRESS;Initial Catalog=campground;Integrated Security=True";
        private int reservationIdColumn;

        [TestInitialize]
        public void Initialize()
        {
            try
            {

                tran = new TransactionScope();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


        }

        [TestCleanup]
        public void CleanUp()
        {
            tran.Dispose();
        }

        [TestMethod]
        public void CreateReservationTest()
        {


            ReservationSqlDAL reservationDAL = new ReservationSqlDAL(connectionString);
            int reservationId = reservationDAL.CreateReservation(1, "TestName", new DateTime(1990, 08, 28), new DateTime(1990, 09, 14));
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                //SqlCommand command;
                conn.Open();

                //command = new SqlCommand("INSERT INTO reservation VALUES (1, TestName, '08/28/1990', '09/14/1990', GETDATE(); SELECT CAST (SCOPE_IDENTITY() AS int)", conn);
                SqlCommand command = new SqlCommand("SELECT * FROM reservation WHERE name = 'TestName'; SELECT CAST(SCOPE_IDENTITY() AS int)", conn);
                reservationIdColumn = (int)command.ExecuteScalar();
            }
            Assert.AreEqual(reservationId, reservationIdColumn);

            // Assert.IsTrue(true);

        }
    }
}
