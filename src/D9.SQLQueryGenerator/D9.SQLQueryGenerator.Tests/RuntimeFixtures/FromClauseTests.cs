using D9.SQLQueryGenerator.Runtime.Clauses;
using D9.SQLQueryGenerator.Tests.GeneratedClasses;
using NUnit.Framework;

namespace D9.SQLQueryGenerator.Tests.RuntimeFixtures
{
	[TestFixture]
	public class FromClauseTests
	{
		[Test]
		public void ToString_Always_GeneratesCorrectSQLClause()
		{
			var table = new Tables_Blogs();

			var from = new FromClause(table);

			string expected =
				@"FROM
				[dbo].[Blogs]
";

			Assert.AreEqual(expected, from.ToString());
		}

		[Test]
		public void ToString_WithAlias_GeneratesCorrectSQLClause()
		{
			Tables_Blogs table = new Tables_Blogs().As("MyTable");

			var from = new FromClause(table);

			string expected =
				@"FROM
				[dbo].[Blogs] AS [MyTable]
";

			Assert.AreEqual(expected, from.ToString());
		}

		[Test]
		public void ToString_WithJoin_GeneratesCorrectSQLClause()
		{
			var blogs = new Tables_Blogs();
			var posts = new Tables_Posts();

			FromClause from = new FromClause(blogs).Join(posts, blogs.Id == posts.BlogId);

			string expected =
				@"FROM
				[dbo].[Blogs]
	JOIN		[dbo].[Posts] ON
					([dbo].[Blogs].[Id] = [dbo].[Posts].[BlogId])
";

			Assert.AreEqual(expected, from.ToString());
		}
	}
}