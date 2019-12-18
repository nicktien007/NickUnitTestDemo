using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NickUnitTestDemo_2
{
    class Program
    {
        static void Main(string[] args)
        {
            ICheckInFee checkInFee = new CheckInFee();
            var pub = new Pub(checkInFee);

            var customers = new List<Customer>
            {
                new Customer {IsMale = true},
                new Customer {IsMale = false},
                new Customer {IsMale = false}
            };

            var checkInCount = pub.CheckIn(customers);
            var fee = pub.GetInCome();

            Console.WriteLine(fee);
            Console.WriteLine(checkInCount);
        }
    }
}
