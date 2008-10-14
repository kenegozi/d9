// ReSharper disable AccessToStaticMemberViaDerivedType

using NUnit.Framework;
using NUnit.Framework.ExtensionMethods;

namespace D9.QuerySpecBuilder.Tests.ClausesTests
{
	[TestFixture]
	public class JoinExpressionTests
	{
		[Test]
		public void BuildLeftJoin()
		{
			var expected = "LEFT JOIN\tOrders AS o ON (o.Id = line.OrderId)";

			var join = Joins.Left.Table("Orders").As("o").On("o.Id = line.OrderId");

			join.GetQueryString().Should(Be.EqualTo(expected));
		}

		[Test]
		public void BuildRightJoin()
		{
			var expected = "RIGHT JOIN\tOrders AS o ON (o.Id = line.OrderId)";

			var join = Joins.Right.Table("Orders").As("o").On("o.Id = line.OrderId");

			join.GetQueryString().Should(Be.EqualTo(expected));
		}

		[Test]
		public void BuildInnerJoin()
		{
			var expected = "INNER JOIN\tOrders AS o ON (o.Id = line.OrderId)";

			var join = Joins.Table("Orders").As("o").On("o.Id = line.OrderId");

			join.GetQueryString().Should(Be.EqualTo(expected));
		}
	}
}
