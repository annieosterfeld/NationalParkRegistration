using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capstone.DAL;
using Capstone.Models;
using System.Configuration;

namespace Capstone
{
    public class ParkSystemCLI2
    {
        private string connectionString = ConfigurationManager.ConnectionStrings["CapstoneDatabase"].ConnectionString;
        Park selectedPark = new Park();
        Reservation reservation = new Reservation();
        string input = "1";
        string[] result = new string[3];

        public void RunCLI()
        {
            while (input != "q")
            {
                switch (input)
                {
                    case "1":
                        selectedPark = GetParks();
                        break;
                    case "2":
                        ParkInfo(selectedPark);
                        break;
                    case "3":
                        ViewCampgrounds(selectedPark);
                        break;
                    case "4":
                        SearchForReservationMenu(selectedPark);
                        break;
                    case "5":
                        MakeReservationMenu();
                        break;
                }

            }
        }

        private Park GetParks()
        {
            ParkSqlDAL dal = new ParkSqlDAL(connectionString);
            List<Park> parks = dal.GetAllParks();
            //Park selectedPark = new Park();


            bool menuOpen = true;
            while (menuOpen)
            {
                int menuCounter = 1;
                Console.WriteLine("Select a Park for Further Details: ");
                foreach (Park park in parks)
                {
                    Console.WriteLine(menuCounter++ + ")" + park.Park_name);
                }
                Console.WriteLine("Q) Quit");

                string userInput = Console.ReadLine();
                // Console.Clear();

                bool number = int.TryParse(userInput, out int parkNumber);
                if (!(number))
                {
                    if (userInput.ToLower() == "q")
                    {
                        input = "q";
                        menuOpen = false;
                        return selectedPark;
                    }
                    else
                    {
                        Console.WriteLine("Please select a valid menu option. Press enter to try again.");
                        Console.ReadLine();
                    }
                }
                else
                {
                    if (parkNumber <= parks.Count() && parkNumber != 0)
                    {
                        selectedPark = parks[parkNumber - 1];
                        Console.WriteLine(selectedPark.ToString());
                        menuOpen = false;
                        input = "2";
                    }
                    else
                    {
                        Console.WriteLine("Please select a valid menu option. Press enter to try again.");
                        Console.ReadLine();
                    }
                }
                Console.Clear();
            }
            return selectedPark;
        }

        private void ParkInfo(Park selectedPark) //Park Info
        {
            Console.WriteLine(selectedPark.ToString());
            Console.WriteLine("Select A Command");
            Console.WriteLine("1) View Campgrounds");
            Console.WriteLine("2) Search For Reservation");
            Console.WriteLine("3) Return to Previous Screen");
            string parkInfoInput = Console.ReadLine();

            switch (parkInfoInput)
            {
                case "1":
                    input = "3";
                    Console.Clear();
                    break;
                case "2":
                    input = "4";
                    Console.Clear();
                    break;
                case "3":
                    input = "1";
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Please select a valid input. Press enter to try again.");
                    Console.ReadLine();
                    Console.Clear();
                    break;
            }
        }
        private void ViewCampgrounds(Park selectedPark)
        {
            CampgroundSqlDAL campgroundDAL = new CampgroundSqlDAL(connectionString);
            List<Campground> campgrounds = campgroundDAL.GetCampgrounds(selectedPark);
            Console.WriteLine($"#:".PadRight(8) + "Campground Name".PadRight(41) +"Open From:".PadRight(16) +"Open To:".PadRight(16) +"Daily Fee".PadRight(5));
            foreach (Campground campground in campgrounds)
            {
                Console.WriteLine(campground.ToString());
            }
            Console.WriteLine("Select A Command");
            Console.WriteLine("1) Search for Available Reservation");
            Console.WriteLine("2) Return To Previous Screen");

            string campgroundMenuInput = Console.ReadLine();

            switch (campgroundMenuInput)
            {
                case "1":
                    input = "4";
                    Console.Clear();
                    break;
                case "2":
                    input = "2";
                    Console.Clear();
                    break;
                default:
                    Console.WriteLine("Please select a valid input. Press enter to try again.");
                    Console.ReadLine();
                    Console.Clear();
                    break;
            }
        }

        private void SearchForReservationMenu(Park selectedPark)
        {
            SiteSqlDAL siteSqlDAL = new SiteSqlDAL(connectionString);
            CampgroundSqlDAL campgroundDAL = new CampgroundSqlDAL(connectionString);
            List<Campground> campgrounds = campgroundDAL.GetCampgrounds(selectedPark);
            Console.WriteLine($"#:".PadRight(8) + "Campground Name".PadRight(41) + "Open From:".PadRight(16) + "Open To:".PadRight(16) + "Daily Fee".PadRight(5));
            foreach (Campground campground in campgrounds)
            {
                Console.WriteLine(campground.ToString());
            }

            Console.Write("Which campground? (enter 0 to cancel)");
            result[0] = Console.ReadLine();
            if (result[0] == "0")
            {
                input = "3";
                Console.Clear();
                return;
            }
            Console.Write("What is the arrival date? (MM/DD/YYYY)");
            result[1] = Console.ReadLine();
            Console.Write("What is the departure date? (MM/DD/YYYY)");
            result[2] = Console.ReadLine();



            bool validCampgroundId = int.TryParse(result[0], out int campground_id);
            bool validFromDate = DateTime.TryParse(result[1], out DateTime from_date);
            bool validToDate = DateTime.TryParse(result[2], out DateTime to_date);

            if (DateTime.Compare(from_date, to_date) >= 0)
            {
                Console.WriteLine("Invalid Input, departure date is before arrival date. Press Enter to try again.");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            if (!validCampgroundId || !validFromDate || !validToDate)
            {
                Console.WriteLine("Invalid Input, Check Campground ID or Date Formats. Press Enter to try again.");
                Console.ReadLine();
                Console.Clear();
                return;
            }

            bool containsId = false;
            foreach (Campground campground in campgrounds)
            {
                if (campground.Campground_id == campground_id)
                {
                    containsId = true;
                }
            }
            if (containsId)
            {
                reservation.Reservation_from_date = from_date;
                reservation.Reservation_to_date = to_date;
                input = "5";
                Console.Clear();
            }
            //else
            //{
            //    Console.Clear();
            //    Console.WriteLine("Invalid Input, Check Campground ID or Date Formats.");
            //    Console.WriteLine();
            //    input = "4";
            //    Console.Clear();
            //}
        }

        private void MakeReservationMenu()
        {
            SiteSqlDAL siteSqlDAL = new SiteSqlDAL(connectionString);
            List<Site> availableSites = siteSqlDAL.GetAvailableSites(Convert.ToInt32(result[0]), reservation.Reservation_from_date, reservation.Reservation_to_date);
            Console.WriteLine($"Site ID:".PadRight(11) + "Site #:".PadRight(11) + "Max Occupancy:".PadRight(16) + "Wheelchair Accessible:".PadRight(25) + "Max RV Length:".PadRight(16) + "Utilities:".PadRight(14) + "Total Cost:".PadRight(10));
            foreach (Site site in availableSites)
            {
                Console.WriteLine(site.ToString() + siteSqlDAL.GetCost(Convert.ToInt32(result[0]), reservation.Reservation_from_date, reservation.Reservation_to_date));

            }
            Console.WriteLine("What site should be reserved? (enter 0 to cancel)");
            bool siteInputValid = int.TryParse(Console.ReadLine(), out int siteInput);
            if (siteInputValid)
            {
                if (siteInput == 0)
                {
                    input = "4";
                    Console.Clear();
                    return;
                }
                foreach (Site site in availableSites)
                {
                    if (site.Site_id == siteInput)
                    {
                        reservation.Site_id = siteInput;
                        Console.WriteLine("What name should the reservation be made under?");
                        reservation.Reservation_name = Console.ReadLine();
                        Console.WriteLine($"The reservation has been made and the confirmation id is: {BookReservation(reservation)}");
                        Console.WriteLine("Press enter to go to the main screen.");
                        Console.ReadLine();
                        Console.Clear();
                        input = "1";
                    }
                }
            }
            else
            {
                Console.WriteLine("Please enter a valid input.");
                Console.Clear();
                input = "5";
            }
        }

        private int BookReservation(Reservation reservation)
        {
            ReservationSqlDAL reservationDal = new ReservationSqlDAL(connectionString);
            return reservationDal.CreateReservation(reservation);
        }
    }
}

