using System;
using System.Collections.Generic;
using System.Text;

namespace Pay_Parking
{
    public class InvalidLicenseNumberException : Exception
    {
        public InvalidLicenseNumberException() : base("Invalid license number!")
        {
        }
    }

    public class NoSpacesInParkingLotExpcetion : Exception
    {
        public NoSpacesInParkingLotExpcetion() : base("No available spaces in parking lot!")
        {
        }
    }

    public class NoVehiclesInParkingLotExpcetion : Exception
    {
        public NoVehiclesInParkingLotExpcetion() : base("There are no vehicles in the parking lot!")
        {
        }
    }

    public class InvalidLicenseNumberFromParkingException : Exception
    {
        public InvalidLicenseNumberFromParkingException() : base("Invalid license number - this vehicle is not in the Parking Lot!")
        {
        }
    }

    public class InvalidDateFormatException : Exception
    {
        public InvalidDateFormatException() : base("Invalid date format.")
        {
        }
    }
}
