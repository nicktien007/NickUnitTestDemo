using System;
using System.Collections.Generic;
using Microsoft.QualityTools.Testing.Fakes;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NickUnitTestDemo_2;
using Rhino.Mocks;

namespace NickUnitTestDemo.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private List<Customer> _customers;
        [TestInitialize]
        public void Init()
        {
            this._customers = new List<Customer>
            {
                new Customer {IsMale = true},
                new Customer {IsMale = false},
                new Customer {IsMale = false}
            };
        }

        [TestMethod]
        public void Test_Charge_Customer_Count()
        {
            //arrange
            ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
            Pub target = new Pub(stubCheckInFee);

            stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);

           

            decimal expected = 1;

            //act
            var actual = target.CheckIn(this._customers);

            //assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Income()
        {
            ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();

            Pub target = new Pub(stubCheckInFee);

            stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);

            var inComeBeforeCheckIn = target.GetInCome();
            Assert.AreEqual(0, inComeBeforeCheckIn);

            decimal expectedIncome = 100;

            //act
            var chargeCustomerCount = target.CheckIn(this._customers);

            var actualIncome = target.GetInCome();

            //assert
            Assert.AreEqual(expectedIncome, actualIncome);
        }

        [TestMethod]
        public void Test_CheckIn_Charge_Only_Male()
        {
            //arrange mock
            var customers = new List<Customer>();

            //2男1女
            var customer1 = new Customer { IsMale = true };
            var customer2 = new Customer { IsMale = true };
            var customer3 = new Customer { IsMale = false };

            customers.Add(customer1);
            customers.Add(customer2);
            customers.Add(customer3);

            MockRepository mock = new MockRepository();
            ICheckInFee stubCheckInFee = mock.StrictMock<ICheckInFee>();

            using (mock.Record())
            {
                //期望呼叫ICheckInFee的GetFee()次數為2次
                stubCheckInFee.GetFee(customer1);

                LastCall
                    .IgnoreArguments()
                    .Return((decimal)100)
                    .Repeat.Times(2);
            }

            using (mock.Playback())
            {
                var target = new Pub(stubCheckInFee);

                var count = target.CheckIn(customers);
            }
        }
        [TestMethod]
        public void Test_Friday_Charge_Customer_Count()
        {
            using (ShimsContext.Create())
            {
                System.Fakes.ShimDateTime.TodayGet = 
                    () => new DateTime(2012, 10, 19);

                //NickUnitTestDemo_2.Fakes.ShimPub.Test = () => 2;

                //arrange
                ICheckInFee stubCheckInFee = MockRepository.GenerateStub<ICheckInFee>();
                Pub target = new Pub(stubCheckInFee);

                stubCheckInFee.Stub(x => x.GetFee(Arg<Customer>.Is.Anything)).Return(100);

                var customers = new List<Customer>
                {
                    new Customer{ IsMale=true},
                    new Customer{ IsMale=false},
                    new Customer{ IsMale=false},
                };

                decimal expected = 1;

                //act
                var actual = target.CheckInLadyNight(customers);

                //assert
                Assert.AreEqual(expected, actual);
            }
        }

    }
}
