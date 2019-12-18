using System;
using System.Collections.Generic;
using System.Text;

namespace NickUnitTestDemo_2
{
    public class CheckInFee : ICheckInFee
    {
        public decimal GetFee(Customer customer)
        {
            return 100;
        }
    }
}
