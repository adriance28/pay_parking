using System;
using System.Collections.Generic;
using System.Text;

namespace Pay_Parking.Taxes
{
    interface ITax
    {
        bool IsFirstHourDouble { get; set; }
        decimal Price { get; set; }

        decimal CalculatePayment(int hours);
    }
}
