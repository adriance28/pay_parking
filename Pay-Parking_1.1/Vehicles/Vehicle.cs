using System;
using System.Collections.Generic;
using System.Text;

namespace Pay_Parking.Vehicles
{
    class Vehicle : IVehicle
    {

        /// <summary>
        /// Vehicle identification
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="licensePlate"></param>
        public Vehicle(string licensePlate)
        {
            LicensePlate = licensePlate;
        }
    }
}
