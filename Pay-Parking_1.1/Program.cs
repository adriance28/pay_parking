using System;
using System.Collections.Generic;
using Pay_Parking.ParkingLots;
using Pay_Parking.Taxes;
using Pay_Parking.Vehicles;

namespace Pay_Parking
{
    class Program
    {
        /// <summary>
        /// Main console application method
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            IParkingLot parkingLot = CreateParkingLot();

            string choice = "";

            while (choice != "0")
            {
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("-------------------------------------------------");
                Console.WriteLine("Main menu");
                Console.WriteLine("");
                Console.WriteLine("Select option");
                Console.WriteLine("1. Enter Parking Lot");
                Console.WriteLine("2. Exit Parking Lot");
                Console.WriteLine("3. List Parking Lot vehicles");
                Console.WriteLine("0. Exit application");
                Console.WriteLine("");

                choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        EnterParkingLot(parkingLot);
                        break;
                    case "2":
                        ExitParkingLot(parkingLot);
                        break;
                    case "3":
                        ListParkingLot(parkingLot);
                        break;
                    case "0":
                        break;
                    default:
                        Console.WriteLine("Option not available. Please choose one of the options below");
                        break;
                }
            }

            Console.WriteLine("Good bye, and have a nice day!");
        }

        /// <summary>
        /// CreateParkingLot - start application with creating the parking lot according to requirements
        /// </summary>
        /// <returns> ParkingLot </returns>
        private static IParkingLot CreateParkingLot()
        {
            Console.WriteLine("Pay-Parking Project - Adrian Oltean (VS 1.1 - 07 Mai 2021)");
            Console.WriteLine("-------------------------------------------------");
            Console.WriteLine("Parking Lot created - 10 available parking spaces, 10 lei tax first hour, 5 lei tax following hours");
            int HourTaxValue = 5;
            int NumberOfParkingSpaces = 10;

            ITax HourTax = new Tax(true, HourTaxValue);

            IParkingLot parkingLot = new ParkingLot(NumberOfParkingSpaces, HourTax);

            return parkingLot;
        }

        /// <summary>
        /// New vehicle input in ParkingLot
        /// </summary>
        /// <param name="parkingLot"></param>
        private static void EnterParkingLot(IParkingLot parkingLot)
        {
            if (parkingLot.AvailableParkingSpaces < 1)
            {
                Console.WriteLine("No available spaces in parking lot!");

                return;
            }

            Console.WriteLine("Insert License Plate for entry");
            string licensePlate = Console.ReadLine();
            IVehicle newVehicle = new Vehicle(licensePlate);

            try
            {
                IParkedVehicle parkedVehicle = parkingLot.RequestPark(newVehicle);
                Console.WriteLine("Vehicle " + parkedVehicle.LicensePlate + " registered into the parking lot at: " + parkedVehicle.ParkTimestamp);
            }
            catch (InvalidLicenseNumberException exception)
            {
                Console.WriteLine(exception.Message);
            }

        }

        /// <summary>
        /// Vehicle is removed from ParkedVehicle list and summary is created 
        /// </summary>
        /// <param name="parkingLot"></param>
        private static void ExitParkingLot(IParkingLot parkingLot)
        {
            try
            {
                Console.WriteLine("Insert License Plate for exit");
                string licensePlate = Console.ReadLine();
                Console.WriteLine("Insert exit time in format 'yyyy-MM-dd HH:mm'");
                string exitDateInput = Console.ReadLine();

                ParkSummary parkSummary = parkingLot.RequestLeave(licensePlate, exitDateInput);
                Console.WriteLine("Vehicle removed from the parking lot");
                Console.WriteLine("");

                ListParkingSummary(parkSummary);
            }
            catch (InvalidDateFormatException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (InvalidLicenseNumberFromParkingException exception)
            {
                Console.WriteLine(exception.Message);
            }
        }

        /// <summary>
        /// Show Parking Summary
        /// </summary>
        /// <param name="parkSummary"></param>
        private static void ListParkingSummary(ParkSummary parkSummary)
        {
            Console.WriteLine("Parking summary:");
            Console.WriteLine(
                "(V) Vehicle: . . . . . . .  " + parkSummary.LicensePlate.ToUpper() + "\n" +
                "(E) Entry: . . . . . . . .  " + parkSummary.EnterTimestamp + "\n" +
                "(X) Exit: . . . . . . . . . " + parkSummary.LeaveTimestamp + "\n" +
                "(S) Stay Time: . . . . . .  " + parkSummary.StationaryTime + "\n" +
                "(T) Tax Time: . . . . . . . " + parkSummary.TaxTime + "\n" +
                "(C) Cost: . . . . . . . . . " + parkSummary.TotalCost
                );
        }

        /// <summary>
        /// List vehicles currently in parking lot
        /// </summary>
        /// <param name="parkingLot"></param>
        public static void ListParkingLot(IParkingLot parkingLot)
        {
            List<IParkedVehicle> parkedVehicleList = parkingLot.GetParkedVehicles();

            Console.WriteLine("Parking Lot Report");
            Console.WriteLine("-------------------------------------------------");

            for (int i = 0; i < parkedVehicleList.Count; i++)
            {
                int index = i + 1;
                Console.WriteLine("Vehicle " + index + " having the License plate " + parkedVehicleList[i].LicensePlate.ToUpper() +
                    " has been parked since " + parkedVehicleList[i].ParkTimestamp);
            }
        }
    }
}

