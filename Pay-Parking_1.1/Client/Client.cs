using System;
using System.Collections.Generic;
using System.Text;
using Pay_Parking.Vehicles;

namespace Pay_Parking.Clients
{
    //currently not in use
    class Client
    {
        /// <summary>
        /// Client's Vehicle
        /// </summary>
        public IVehicle Vehicle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="vehicle"></param>
        public Client(IVehicle vehicle)
        {
            Vehicle = vehicle;
        }
    }
}
