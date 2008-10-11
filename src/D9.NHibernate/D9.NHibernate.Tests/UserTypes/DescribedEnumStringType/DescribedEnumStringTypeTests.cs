using System;
using System.Collections;
using D9.Commons;
using D9.Commons.Internal;
using NHibernate;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;

namespace D9.NHibernate.Tests.UserTypes.DescribedEnumStringType
{
	[TestFixture]
	public class DescribedEnumStringTypeTests : AbstractNHibernateDbTest
	{
		public override void TestFixtureSetUp()
		{
			base.TestFixtureSetUp();
			Enums.Initialise(typeof(Colours));
		}

		[Test]
		public void A()
		{
			Execute(session =>
			        	{
			        		var item = new ClassWithDescribedEnum {Colour = Colours.Blue, Id = 1};
			        		session.Save(item);
			        	}
				);

			var colourDesc = ExecuteAndExtractFirstFromReader("SELECT Colour FROM ClassWithDescribedEnum WHERE Id = 1",
			                                                  reader => reader.GetString(0));

			Assert.That(colourDesc, Is.EqualTo("B"));
		}

		[Test]
		public void B()
		{
			Execute(session =>
			        	{
			        		var newItem = new ClassWithDescribedEnum { Colour = Colours.Blue, Id = 1 };
			        		session.Save(newItem);
			        	}
				);

			ClassWithDescribedEnum item = null;

			Execute(session => item = session.Load<ClassWithDescribedEnum>(1));

			Assert.That(item.Colour, Is.EqualTo(Colours.Blue));

		}

		protected void Execute(Action<ISession> action)
		{
			using (var session = sessionFactory.OpenSession())
			using (var tx = session.BeginTransaction())
			{
				action(session);
				session.Flush();
				tx.Commit();
			}

		}

		/// <summary>
		/// Mapping files used in the Fixture
		/// </summary>
		protected override IList Mappings
		{
			get { return new[] { "UserTypes.DescribedEnumStringType.ClassWithDescribedEnum.hbm.xml" }; }
		}
	}
}