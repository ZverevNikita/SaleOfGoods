using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NikitaZverev;

namespace ZverevNikita
{
    [TestClass]
    public class UnitTest1
    {
        static Calc MyCalc = null;
        [ClassInitialize]
        static public void Init(TestContext tc)
        {
            MyCalc = new Calc();
        }
        [ClassCleanup]
        static public void Done()
        {
            MyCalc = null;
        }
        [TestMethod]
        public void Div_2div2_1expected()
        {
            //arrange
            int a = 2;
            int b = 2;
            float expected = 1;

            //act
            float actual = MyCalc.Div(a, b);

            //assert
            Assert.AreEqual(actual, expected);
        }
        [TestMethod]
        public void Div_2div2_2notexpected()
        {
            //arrange
            int a = 2;
            int b = 2;
            float expected = 2;

            //act
            float actual = MyCalc.Div(a, b);

            //assert
            Assert.AreNotEqual(actual, expected);
        }
        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException), "Деление на 0")]
        public void Div_2div0_exceptionexpected()
        {
            //arrange
            float a = 2;
            float b = 0;
            float expected = 2;

            //act
            float actual = MyCalc.Div(a, b);

            //assert
            Assert.AreNotEqual(actual, expected);
        }
        public class ServiceEmptyCost : Exception
        {
            public ServiceEmptyCost(string Mesage) : base(Mesage) { }
        }

        public class ServiceInvalidDiscount : Exception
        {
            public ServiceInvalidDiscount(string Mesage) : base(Mesage) { }
        }
        public class Core
        {
            public static spenkinEntities DB = new spenkinEntities();
            public static void SaveService(Service SavedService)
            {
                if (SavedService.Cost <= 0)
                    throw new ServiceEmptyCost("Не заполнена цена");

                if (SavedService.Discount < 0 || SavedService.Discount > 1)
                    throw new ServiceInvalidDiscount("Скидка должна быть в диапазоне 0..1");

                if (SavedService.ID == 0)
                    DB.Service.Add(SavedService);

                DB.SaveChanges();
            }
        }
        [TestMethod]
        [ExpectedException(typeof(ServiceEmptyCost), "Не заполнена цена")]
        public void TestMethod1()
        {
            var NewService = new Service();
            Core.SaveService(NewService);
            Assert.Fail();
        }
    }

    public class Service
    {
        public int Cost { get; internal set; }
        public int Discount { get; internal set; }
        public int ID { get; internal set; }
    }

    public class spenkinEntities
    {
        public object Service { get; internal set; }

        internal void SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}

