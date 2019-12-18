using System;
using System.Collections.Generic;
using System.Text;

namespace NickUnitTestDemo_2
{
    public class Pub
    {
        private ICheckInFee _checkInFee;
        private decimal _inCome = 0;

        public Pub(ICheckInFee checkInFee)
        {
            this._checkInFee = checkInFee;
        }

        public static int Test()
        {
            return 99;
        }

        /// <summary>
        /// 入場
        /// </summary>
        /// <param name="customers"></param>
        /// <returns>收費的人數</returns>
        public int CheckIn(List<Customer> customers)
        {
            var result = 0;
            
            foreach (var customer in customers)
            {
                var isFemale = !customer.IsMale;
                //for fake
                var isLadyNight = DateTime.Today.DayOfWeek == DayOfWeek.Friday;


                //女生免費入場
                if (isFemale)
                {
                    continue;
                }
                else
                {
                    //for stub, validate status: income value
                    //for mock, validate only male
                    this._inCome += this._checkInFee.GetFee(customer);

                    result++;
                }
            }

            //for stub, validate return value
            return result;
        }

        public int CheckInLadyNight(List<Customer> customers)
        {
            var result = 0;

            foreach (var customer in customers)
            {
                var isFemale = !customer.IsMale;
                //for fake
                var isLadyNight = DateTime.Today.DayOfWeek == DayOfWeek.Friday;


                //女生免費入場
                //if (isFemale)

                //禮拜五女生免費入場
                if (isLadyNight && isFemale)
                {
                    continue;
                }
                else
                {
                    //for stub, validate status: income value
                    //for mock, validate only male
                    this._inCome += this._checkInFee.GetFee(customer);

                    result++;
                }
            }

            //for stub, validate return value
            return result;
        }

        public decimal GetInCome()
        {
            return this._inCome;
        }
    }
}
