using System;
using System.Collections.Generic;
using System.Text;
using Pay_Parking.Vehicles;

namespace Pay_Parking.ParkingLots
{
    class ParkedVehicle : IParkedVehicle
    {
        /// <summary>
        /// Vehicle ID
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Parking entry time
        /// </summary>
        public DateTime ParkTimestamp { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <param name="parkTimestamp"></param>
        public ParkedVehicle(string licensePlate, DateTime parkTimestamp)
        {
            LicensePlate = licensePlate;
            ParkTimestamp = parkTimestamp;
        }
    }
}
