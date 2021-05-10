using System;
using System.Collections.Generic;
using System.Text;

namespace Pay_Parking.Taxes
{
    public class Tax : ITax
    {
        /// <summary>
        /// Define if tax is for fist hour of stay
        /// </summary>
        public bool IsFirstHourDouble { get; set; }

        /// <summary>
        /// Price - set price per hour
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="isFirstHour"></param>
        /// <param name="price"></param>
        public Tax(bool isFirstHour, decimal price)
        {
            IsFirstHourDouble = isFirstHour;
            Price = price;
        }

        /// <summary>
        /// Calculate total payment for time spent
        /// </summary>
        /// <param name="hours"></param>
        /// <returns> total payment amount</returns>
        public decimal CalculatePayment(int hours)
        {
            decimal totalPayment = 0;
            //if first hour of parking than price is double
            if (IsFirstHourDouble)
            {
                totalPayment = Price + (Price * hours);
            }
            else
            {
                totalPayment = Price * hours;
            }

            return totalPayment;
        }
    }
}
