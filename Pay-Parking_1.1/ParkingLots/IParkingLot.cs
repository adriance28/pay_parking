using System;
using System.Collections.Generic;
using System.Text;
using Pay_Parking.Taxes;
using Pay_Parking.Vehicles;

namespace Pay_Parking.ParkingLots
{
    interface IParkingLot
    { 
        int AvailableParkingSpaces { get; }
        ITax Tax { get; set; }

        List<IParkedVehicle> GetParkedVehicles();
        IParkedVehicle RequestPark(IVehicle vehicle);
        ParkSummary RequestLeave(string licensePlate, string ExitDateInput);
    }
}
