// ReSharper disable AccessToStaticMemberViaDerivedType
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using D9.SQLQueryGenerator.DatabaseMetadataProviders;
using NUnit.Framework;
using NUnit.Framework.ExtensionMethods;

namespace D9.SQLQueryGenerator.Tests
{
	/// <summary>
	/// Testing that the SQL2005MetadataProvider can extract metadata from an actual DB
	/// </summary>
	/// <remarks>
	/// You'd need a DB on (local) named test with owner access granted to the user running the test
	/// </remarks>
	[TestFixture]
	public class SQL2005MetadataProviderTests
	{
		private const string CONNECTION_STRING = "SERVER=(local);Initial catalog=test;Integrated Security=SSPI";

		public void CreateSchema()
		{
			ExecuteSQL(delegate(SqlCommand cmd)
			           	{
			           		cmd.CommandText =
			           			@"
CREATE TABLE dbo.Blogs
(
	[Id] INT NOT NULL,
	[Title] NVARCHAR(50) NOT NULL,
	[AuthorId] INT NOT NULL
)

CREATE TABLE dbo.Posts
(
	[Id] INT NOT NULL,
	[CreationDate] DATETIME NOT NULL,
	[LastUpdated] DATETIME,
	[Title] NVARCHAR(50) NOT NULL,
	[Content] NVARCHAR(MAX),
	[BlogId] INT NOT NULL
)

CREATE TABLE dbo.Authors
(
	[Id] INT NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	[Email] NVARCHAR(50)
)

";
			           		cmd.ExecuteNonQuery();
			           	});
		}

		public void DropSchema()
		{
			var commands = new[]
			               	{
			               		"DROP TABLE dbo.Blogs",
			               		"DROP TABLE dbo.Posts",
			               		"DROP TABLE dbo.Authors"
			               	};

			TryExecuteCommands(commands);
		}

		[Test]
		[Ignore("Dependant on a real DB - Leave it out for the time being")]
		public void ExtractMetadata_Always_Work()
		{
			DropSchema();
			CreateSchema();

			DbPropertyMetadata[] expectedProperties = new DbPropertyMetadata[]
			                                          	{
			                                          		new DbPropertyMetadata("dbo", "Blogs", "Id", "System.Int32", false),
			                                          		new DbPropertyMetadata("dbo", "Blogs", "Title", "System.String", false),
			                                          		new DbPropertyMetadata("dbo", "Blogs", "AuthorId", "System.Int32", false)
			                                          		,
			                                          		new DbPropertyMetadata("dbo", "Posts", "Id", "System.Int32", false),
			                                          		new DbPropertyMetadata("dbo", "Posts", "Title", "System.String", false),
			                                          		new DbPropertyMetadata("dbo", "Posts", "Content", "System.String", true),
			                                          		new DbPropertyMetadata("dbo", "Posts", "BlogId", "System.Int32", false),
			                                          		new DbPropertyMetadata("dbo", "Posts", "CreationDate", "System.DateTime",
			                                          		                       false),
			                                          		new DbPropertyMetadata("dbo", "Posts", "LastUpdated", "System.DateTime",
			                                          		                       true),
			                                          		new DbPropertyMetadata("dbo", "Authors", "Id", "System.Int32", false),
			                                          		new DbPropertyMetadata("dbo", "Authors", "Name", "System.String", false),
			                                          		new DbPropertyMetadata("dbo", "Authors", "Email", "System.String", true),
			                                          	};

			var provider = new SQL2005MetadataProvider("test");
			IList<DbPropertyMetadata> actualProperties = provider.ExtractMetadata().ToList();

			Assert.AreEqual(expectedProperties.Length, Count(actualProperties));

			var bogousProperty = new DbPropertyMetadata("foo", "bar", "parp", "System.Object", true);
			
			actualProperties.Contains(bogousProperty).Should(Be.False);

			foreach (DbPropertyMetadata property in expectedProperties)
			{
				actualProperties.Contains(property).Should(Be.True, "Property {0} not found", property);
			}

			DropSchema();
		}

		#region helpers

		private static void TryExecuteCommands(IEnumerable<string> commands)
		{
			foreach (string cmdText in commands)
			{
				try
				{
					string cmdText1 = cmdText;
					ExecuteSQL(delegate(SqlCommand cmd)
					           	{
					           		cmd.CommandText = cmdText1;
					           		cmd.ExecuteNonQuery();
					           	});
				}
				catch (SqlException ex)
				{
					Console.WriteLine("\"{0}\" threw [{1}]", cmdText, ex.Message);
				}
			}
		}

		private static int Count(IEnumerable enumerable)
		{
			int count = 0;

			IEnumerator enumerator = enumerable.GetEnumerator();

			while (enumerator.MoveNext())
				++count;

			return count;
		}

		private static SqlConnection GetTestDb()
		{
			return new SqlConnection(CONNECTION_STRING);
		}

		private static void ExecuteSQL(SqlCommandExecutor handler)
		{
			using (SqlConnection con = GetTestDb())
			{
				con.Open();
				using (SqlTransaction trans = con.BeginTransaction())
				using (var cmd = new SqlCommand())
				{
					cmd.Connection = con;
					cmd.Transaction = trans;
					handler(cmd);
					trans.Commit();
				}
			}
		}

		private delegate void SqlCommandExecutor(SqlCommand cmd);

		#endregion
	}
}