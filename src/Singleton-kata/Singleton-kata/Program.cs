using System;
using NUnit.Framework;

namespace Singleton_kata
{
    class Program
    {

        public class Adam
        {
            static Adam adam;
            private Adam()
            {

            }
            public static Adam GetInstance()
            {
                //this is not right
                return new Adam();

                //this is the right way to do singleton
                if (adam == null)
                    adam = new Adam();
                return adam;
            }

        }
        public class Eve { }
        public class Male { }
        public class Female { }
        public abstract class Human { }

        public static class SampleTests
        {
            public static void Adam_is_unique()
            {
                Adam adam = Adam.GetInstance();
                Adam anotherAdam = Adam.GetInstance();

                Assert.IsTrue(adam is Adam);
                Assert.AreEqual(adam, anotherAdam);
            }

            // Implement all the tests below one by one!

            /*
            public static void Adam_is_unique_and_only_GetInstance_can_return_adam()
            {   
                // GetInstance() is the only static method on Adam
                Assert.AreEqual(1, typeof(Adam).GetMethods().Where(x => x.IsStatic).Count());

                // Adam does not have public or internal constructors
                Assert.IsFalse(typeof(Adam).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                  .Any(x => x.IsPublic || x.IsAssembly ));
            }


            public static void Adam_is_unique_and_cannot_be_overriden()
            {
                Assert.IsTrue(typeof(Adam).IsSealed);
            }

            [Test]
            public static void Adam_is_a_human()
            {
                Assert.IsTrue(Adam.GetInstance() is Human); 
            }

            [Test]
            public static void Adam_is_a_male()
            { 
                Assert.IsTrue(Adam.GetInstance() is Male);
            }

            [Test]
            public static void Eve_is_unique_and_created_from_a_rib_of_adam()
            {
                Adam adam = Adam.GetInstance();
                Eve eve = Eve.GetInstance(adam);
                Eve anotherEve = Eve.GetInstance(adam);

                Assert.IsTrue(eve is Eve);
                Assert.AreEqual(eve, anotherEve);

                // GetInstance() is the only static method on Eve
                Assert.AreEqual(1, typeof(Eve).GetMethods().Where(x => x.IsStatic).Count());

                // Eve has no public or internal constructor
                Assert.IsFalse(typeof(Eve).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                  .Any(x => x.IsPublic || x.IsAssembly));

                // Eve cannot be overridden
                Assert.IsTrue(typeof(Eve).IsSealed);
            }

            [Test]
            public static void Eve_can_only_be_create_of_a_rib_of_adam()
            {
                Assert.Throws<ArgumentNullException>(() => Eve.GetInstance(null));
            }

            public static void Eve_is_a_human()
            {
                Assert.IsTrue(Eve.GetInstance(Adam.GetInstance()) is Human);
            }

            public static void Eve_is_a_female()
            {
                Assert.IsTrue(Eve.GetInstance(Adam.GetInstance()) is Female);
            }

            public static void Reproduction_always_result_in_a_male_or_female()
            {
                Assert.IsTrue(typeof(Human).IsAbstract);
            }

            public static void Humans_can_reproduce_when_there_is_a_name_a_mother_and_a_father()
            {
                var adam = Adam.GetInstance();
                var eve = Eve.GetInstance(adam);
                var seth = new Male("Seth", eve, adam);
                var azura = new Female("Azura", eve, adam);
                var enos = new Male("Enos", azura, seth);

                Assert.AreEqual("Eve", eve.Name);
                Assert.AreEqual("Adam", adam.Name);
                Assert.AreEqual("Seth", seth.Name);
                Assert.AreEqual("Azura", azura.Name);
                Assert.AreEqual("Enos", ((Human)enos).Name);
                Assert.AreEqual(seth, ((Human)enos).Father);
                Assert.AreEqual(azura, ((Human)enos).Mother);
            }

            public static void Father_and_mother_are_essential_for_reproduction()
            {
                // There is just 1 way to reproduce 
                Assert.AreEqual(1, typeof(Male).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                  .Where(x => x.IsPublic || x.IsAssembly).Count());
                Assert.AreEqual(1, typeof(Female).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).
                  Where(x => x.IsPublic || x.IsAssembly).Count());

                var adam = Adam.GetInstance();
                var eve = Eve.GetInstance(adam);
                Assert.Throws<ArgumentNullException>(() => new Male("Seth", null, null));
                Assert.Throws<ArgumentNullException>(()=> new Male("Abel", eve, null));
                Assert.Throws<ArgumentNullException>(() => new Male("Seth", null, adam));
                Assert.Throws<ArgumentNullException>(() => new Female("Azura", null, null));
                Assert.Throws<ArgumentNullException>(() => new Female("Awan", eve, null));
                Assert.Throws<ArgumentNullException>(() => new Female("Dina", null, adam));
                Assert.Throws<ArgumentNullException>(() => new Female("Eve", null, null));
                Assert.Throws<ArgumentNullException>(() => new Male("Adam", null, null));
            }
            }
            */

            static void Main(string[] args)
            {
                SampleTests.Adam_is_unique();
                Console.WriteLine("Hello World!");
            }
        }

    }
}
