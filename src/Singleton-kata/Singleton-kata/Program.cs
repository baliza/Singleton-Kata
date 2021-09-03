using NUnit.Framework;
using System;
using System.Linq;
using System.Reflection;

namespace Singleton_kata
{
	internal class Program
	{
		public sealed class Adam : Male
		{
			private static Adam _adam;

			private Adam() : base("Adam", null, null)
			{
			}

			public static Adam GetInstance()
			{
				if (_adam == null)
					_adam = new Adam();
				return _adam;
			}
		}

		public sealed class Eve : Female
		{
			private Adam _adam;
			private static Eve _eve;

			private Eve(Adam adam) : base("Eve", null, null)
			{
				if (adam == null)
					throw new ArgumentNullException("Eva needs a rib of Adam to be born");
				_adam = adam;
			}

			public static Eve GetInstance(Adam adam)
			{
				if (_eve == null)
					_eve = new Eve(adam);
				return _eve;
			}
		}

		public class Male : Human
		{
			public Male(string name, Female mother, Male father) : base(name, mother, father)
			{
			}
		}

		public class Female : Human
		{
			public Female(string name, Female mother, Male father) : base(name, mother, father)
			{
			}
		}

		public abstract class Human
		{
			public readonly string Name;
			public readonly Female Mother;
			public readonly Male Father;

			public Human(string name, Female mother, Male father)
			{
				//if (string.IsNullOrEmpty(name))
				//	throw new ArgumentNullException("Every Human needs a name");
				//if (mother == null)
				//	throw new ArgumentNullException("Every Human needs a mother");
				//if (father == null)
				//	throw new ArgumentNullException("Every Human needs a father");
				Name = name;
				Mother = mother;
				Father = father;
			}
		}

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

			public static void Adam_is_unique_and_only_GetInstance_can_return_adam()
			{
				// GetInstance() is the only static method on Adam
				Assert.AreEqual(1, typeof(Adam).GetMethods().Where(x => x.IsStatic).Count());

				// Adam does not have public or internal constructors
				Assert.IsFalse(typeof(Adam).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
				  .Any(x => x.IsPublic || x.IsAssembly));
			}

			public static void Adam_is_unique_and_cannot_be_overriden()
			{
				Assert.IsTrue(typeof(Adam).IsSealed);
			}

			public static void Adam_is_a_human()
			{
				Assert.IsTrue(Adam.GetInstance() is Human);
			}

			public static void Adam_is_a_male()
			{
				Assert.IsTrue(Adam.GetInstance() is Male);
			}

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

			public static void Eve_can_only_be_create_of_a_rib_of_adam()
			{
				try
				{
					Eve.GetInstance(null);
					Assert.Fail();
				}
				catch (ArgumentNullException ex)
				{
					Assert.IsTrue(true);
				}
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
				Assert.AreEqual("Enos", enos.Name);
				Assert.AreEqual(seth, enos.Father);
				Assert.AreEqual(azura, enos.Mother);
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
				try
				{
					var _ = new Male("Seth", null, null);
					Assert.Fail();
				}
				catch (ArgumentNullException) { }

				try
				{
					var _ = new Male("Abel", eve, null);
					Assert.Fail();
				}
				catch (ArgumentNullException) { }

				try
				{
					var _ = new Male("Seth", null, adam);
					Assert.Fail();
				}
				catch (ArgumentNullException) { }

				try
				{
					var _ = new Female("Azura", null, null);
					Assert.Fail();
				}
				catch (ArgumentNullException) { }

				try
				{
					var _ = new Female("Awan", eve, null);
					Assert.Fail();
				}
				catch (ArgumentNullException) { }

				try
				{
					var _ = new Female("Dina", null, adam);
					Assert.Fail();
				}
				catch (ArgumentNullException) { }

				try
				{
					var _ = new Female("Eve", null, null);
					Assert.Fail();
				}
				catch (ArgumentNullException) { }

				try
				{
					var _ = new Male("Adam", null, null);
					Assert.Fail();
				}
				catch (ArgumentNullException) { }
			}
		}

		private static void Main(string[] args)
		{
			//SampleTests.Adam_is_unique();
			//SampleTests.Adam_is_unique_and_only_GetInstance_can_return_adam();
			//SampleTests.Adam_is_unique_and_cannot_be_overriden();
			//SampleTests.Adam_is_a_human();
			//SampleTests.Adam_is_a_male();

			//SampleTests.Eve_is_unique_and_created_from_a_rib_of_adam();
			//SampleTests.Eve_can_only_be_create_of_a_rib_of_adam();
			//SampleTests.Eve_is_a_human();
			//SampleTests.Eve_is_a_female();
			//SampleTests.Reproduction_always_result_in_a_male_or_female();
			//SampleTests.Humans_can_reproduce_when_there_is_a_name_a_mother_and_a_father();
			SampleTests.Father_and_mother_are_essential_for_reproduction();

			Console.WriteLine("Hello World!");
		}
	}
}