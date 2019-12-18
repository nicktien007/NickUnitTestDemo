using System;
using System.Collections.Generic;
using System.Text;

namespace NickUnitTestDemo_2
{
    public interface ICheckInFee
    {
        decimal GetFee(Customer customer);
    }
}
