using Pay_Parking.Taxes;
using Pay_Parking.Vehicles;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace Pay_Parking.ParkingLots
{
    class ParkingLot : IParkingLot
    {
        /// <summary>
        /// List of all vehicles parked in
        /// </summary>
        private List<IParkedVehicle> ParkedVehicles;

        /// <summary>
        /// Parking capacity
        /// </summary>
        private int TotalParkingSpaces;

        /// <summary>
        /// Available parking spaces
        /// </summary>
        public int AvailableParkingSpaces { get; private set; }

        /// <summary>
        /// Parking tax
        /// </summary>
        public ITax Tax { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="totalParkingSpaces"></param>
        /// <param name="HourTax"></param>
        public ParkingLot(int totalParkingSpaces, ITax HourTax)
        {
            TotalParkingSpaces = totalParkingSpaces;
            Tax = HourTax;

            AvailableParkingSpaces = totalParkingSpaces;
            ParkedVehicles = new List<IParkedVehicle>();
        }

        /// <summary>
        /// Get list of parked vehicles
        /// </summary>
        /// <returns> list of parked vehicles </returns>
        public List<IParkedVehicle> GetParkedVehicles()
        {
            return ParkedVehicles;
        }

        /// <summary>
        /// Vehicle entry in parking lot
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns> parked vehicle </returns>
        public IParkedVehicle RequestPark(IVehicle vehicle)
        {
            if (!LicensePlateIsValid(vehicle))
            {
                throw new InvalidLicenseNumberException();
            }

            IParkedVehicle newParkedVehicle = new ParkedVehicle(vehicle.LicensePlate, DateTime.UtcNow);

            ParkedVehicles.Add(newParkedVehicle);
            AvailableParkingSpaces -= 1;

            return newParkedVehicle;
        }

        /// <summary>
        /// Vehicle exit from parking lot
        /// </summary>
        /// <param name="parkedVehicle"></param>
        /// <returns> ParkSummary - parking details </returns>
        public ParkSummary RequestLeave(string LicensePlate, string ExitDateInput)
        {
            int parkedVehicleIndex = ParkedVehicles.FindIndex(p => p.LicensePlate == LicensePlate);

            if (!IsVehicleInParking(parkedVehicleIndex))
            {
                throw new InvalidLicenseNumberFromParkingException();
            }
            IParkedVehicle parkedVehicle = ParkedVehicles[parkedVehicleIndex];

            if (!IsValidDateFormat(ExitDateInput))
            {
                throw new InvalidDateFormatException();
            }

            DateTime exitTime = DateTime.ParseExact(ExitDateInput, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture);

            ParkedVehicles.Remove(parkedVehicle);
            AvailableParkingSpaces += 1;

            return CreateParkSummary(LicensePlate, parkedVehicle.ParkTimestamp, exitTime);
        }

        /// <summary>
        /// Create Park Summary - for car exit reporting
        /// </summary>
        /// <param name="LicensePlate"></param>
        /// <param name="ParkTime"></param>
        /// <param name="ExitTime"></param>
        /// <returns> ParkSummary </returns>
        private ParkSummary CreateParkSummary(string LicensePlate, DateTime ParkTime, DateTime ExitTime)
        {
            ParkSummary parkSummary = new ParkSummary();
            TimeSpan parkingTime = ExitTime - ParkTime;
            int hours = (int)Math.Ceiling(parkingTime.TotalHours);
            decimal PaymentAmount = Tax.CalculatePayment(hours);

            parkSummary.LicensePlate = LicensePlate;
            parkSummary.EnterTimestamp = ParkTime;
            parkSummary.LeaveTimestamp = ExitTime;
            parkSummary.StationaryTime = Math.Round((decimal)parkingTime.TotalHours, 4);
            parkSummary.TaxTime = hours;
            parkSummary.TotalCost = PaymentAmount;

            return parkSummary;
        }

        /// <summary>
        /// Check if license plate is valid (contains just letters and numbers, no spaces and no special characters)
        /// </summary>
        /// <param name="vehicle"></param>
        /// <returns> boolean </returns>
        private bool LicensePlateIsValid(IVehicle vehicle)
        {
            bool isValidLicensePlate = Regex.IsMatch(vehicle.LicensePlate, "^[a-zA-Z0-9]*$");

            return isValidLicensePlate;
        }

        /// <summary>
        /// Check if vehicle is in the parking lot
        /// </summary>
        /// <param name="parkedVehicle"></param>
        /// <returns></returns>
        private bool IsVehicleInParking(int ParkedVehicleIndex)
        {
            bool isVehicleInParking = ParkedVehicleIndex >= 0 ? true : false;

            return isVehicleInParking;
        }

        /// <summary>
        /// Check if the date format is correct (to do: use date format chack instead of regex)
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        private bool IsValidDateFormat(string dateTime)
        {
            bool isValidDateFormat = Regex.IsMatch(dateTime, "[0-2][0-9][0-9][0-9]-[0-1][0-9]-[0-3][0-9] [0-2][0-9]:[0-5][0-9]");

            return isValidDateFormat;
        }
    }
}

