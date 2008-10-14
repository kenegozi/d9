// ReSharper disable AccessToStaticMemberViaDerivedType

using D9.QuerySpecBuilder.Builder.Clauses;
using NUnit.Framework;
using NUnit.Framework.ExtensionMethods;

namespace D9.QuerySpecBuilder.Tests.ClausesTests
{
	[TestFixture]
	public class SelectClauseTests
	{
		[Test]
		public void Add_WhenAddingLiteral_Adds()
		{
			var expected = @"SELECT
	Id,
	Name";
			var clause = new SelectClause(null);
			clause.Add("Id");
			clause.Add("Name");
			clause.GetQueryString().Should(Be.EqualTo(expected));
		}

		[Test]
		public void Add_WhenAddingSubQuery_Adds()
		{
			var expected = @"SELECT
	Id,
	Name";
			var clause = new SelectClause(null);
			clause.Add("Id");
			clause.Add("Name");
			clause.GetQueryString().Should(Be.EqualTo(expected));
		}

	}
}

// ReSharper restore AccessToStaticMemberViaDerivedType
