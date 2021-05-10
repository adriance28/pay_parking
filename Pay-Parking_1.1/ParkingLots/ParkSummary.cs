using System;
using System.Collections.Generic;
using System.Text;

namespace Pay_Parking.ParkingLots
{
    class ParkSummary
    {
        /// <summary>
        /// Vehicle ID
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Parking entry time
        /// </summary>
        public DateTime EnterTimestamp { get; set; }

        /// <summary>
        /// Parking exit time
        /// </summary>
        public DateTime LeaveTimestamp { get; set; }

        /// <summary>
        /// Time spent in parking
        /// </summary>
        public decimal StationaryTime { get; set; }

        /// <summary>
        /// Payment time
        /// </summary>
        public int TaxTime { get; set; }

        /// <summary>
        /// Total parking cost (tax_time*price)
        /// </summary>
        public decimal TotalCost { get; set; }
    }
}
