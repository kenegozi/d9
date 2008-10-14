#region License

// Copyright (c) 2008 Ken Egozi (ken@kenegozi.com)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of the D9 project nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion License

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