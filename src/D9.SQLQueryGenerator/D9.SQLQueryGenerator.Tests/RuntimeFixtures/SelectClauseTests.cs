using D9.SQLQueryGenerator.Runtime.Clauses;
using D9.SQLQueryGenerator.Runtime.Model.Field;
using D9.SQLQueryGenerator.Tests.GeneratedClasses;
using NUnit.Framework;

namespace D9.SQLQueryGenerator.Tests.RuntimeFixtures
{
	[TestFixture]
	public class SelectClauseTests
	{
		[Test]
		public void ToString_WithAlias_GeneratesCorrectSQLClause()
		{
			var table = new Tables_Blogs();

			var fields = new IFormatableField[]
			             	{
			             		new Tables_Blogs_Id(table).As("MyId"),
			             		new Tables_Blogs_Name(table)
			             	};

			var select = new SelectClause(fields);

			string expected =
				@"SELECT
				[dbo].[Blogs].[Id] AS [MyId],
				[dbo].[Blogs].[Name]
";

			Assert.AreEqual(expected, select.ToString());
		}

		[Test]
		public void ToString_WithMoreThanOneField_GeneratesCorrectSQLClause()
		{
			var table = new Tables_Blogs();

			var fields = new IFormatableField[]
			             	{
			             		new Tables_Blogs_Id(table),
			             		new Tables_Blogs_Name(table)
			             	};

			var select = new SelectClause(fields);

			string expected =
				@"SELECT
				[dbo].[Blogs].[Id],
				[dbo].[Blogs].[Name]
";

			Assert.AreEqual(expected, select.ToString());
		}

		[Test]
		public void ToString_WithSingleField_GeneratesCorrectSQLClause()
		{
			var table = new Tables_Blogs();

			var fields = new IFormatableField[]
			             	{
			             		new Tables_Blogs_Id(table)
			             	};

			var select = new SelectClause(fields);

			string expected =
				@"SELECT
				[dbo].[Blogs].[Id]
";

			Assert.AreEqual(expected, select.ToString());
		}
	}
}