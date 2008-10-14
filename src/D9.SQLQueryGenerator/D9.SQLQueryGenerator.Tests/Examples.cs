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

using System;
using D9.SQLQueryGenerator.Runtime;
using D9.SQLQueryGenerator.Runtime.Clauses;
using D9.SQLQueryGenerator.Runtime.Expressions;
using D9.SQLQueryGenerator.Runtime.Model;
using D9.SQLQueryGenerator.Tests.GeneratedClasses;
using NUnit.Framework;

namespace D9.SQLQueryGenerator.Tests
{
	[TestFixture]
	public class Examples
	{
		private static void Spit(string sql)
		{
			Console.WriteLine(sql
			                  	.Replace("\t", "[**]")
			                  	.Replace("" + Environment.NewLine, "~" + Environment.NewLine)
			                  	.Replace(" ", "%"));
		}

		[Test]
		public void ComplexWhereClause()
		{
			#region expected

			string expectedQ1 =
				@"SELECT
				[dbo].[Blogs].[Id]
FROM
				[dbo].[Blogs]
WHERE
				(([dbo].[Blogs].[Id] > 2) OR ([dbo].[Blogs].[Name] = N'Ken'))
";

			#endregion

			var from = new FromClause(SQL.Blogs);
			var whereIdGraterThan2 = new WhereClause(SQL.Blogs.Id > 2);
			var whereIdNameIsKen = new WhereClause(SQL.Blogs.Name == "Ken");

			SQLQuery q1 = SQLQuery
				.Select(SQL.Blogs.Id)
				.From(from)
				.Where(whereIdGraterThan2 || whereIdNameIsKen);

			Assert.AreEqual(expectedQ1, q1.ToString());
		}

		[Test]
		public void ComplexWhereExpressions()
		{
			#region expected

			string expectedQ1 =
				@"SELECT
				[dbo].[Blogs].[Id]
FROM
				[dbo].[Blogs]
WHERE
				(([dbo].[Blogs].[Id] > 2) OR ([dbo].[Blogs].[Name] = N'Ken'))
";

			#endregion

			SQLQuery q1 = SQLQuery
				.Select(SQL.Blogs.Id)
				.From(SQL.Blogs)
				.Where(SQL.Blogs.Id > 2 || SQL.Blogs.Name == "Ken");

			Assert.AreEqual(expectedQ1, q1.ToString());
		}

		[Test]
		public void OrderBy()
		{
			#region expected

			string expected =
				@"SELECT
				[dbo].[Blogs].[Id]
FROM
				[dbo].[Blogs]
WHERE
				([dbo].[Blogs].[Id] > 2)
ORDER BY
				[dbo].[Blogs].[Id],
				[dbo].[Blogs].[Name] DESC
";

			#endregion

			SQLQuery q = SQLQuery
				.Select(SQL.Blogs.Id)
				.From(SQL.Blogs)
				.Where(SQL.Blogs.Id > 2)
				.OrderBy(Order.By(SQL.Blogs.Id), Order.By(SQL.Blogs.Name).Desc);

			Assert.AreEqual(expected, q.ToString());
		}

		[Test]
		public void ReusingClauses()
		{
			#region expected

			string expectedQ1 =
				@"SELECT
				[dbo].[Blogs].[Id]
FROM
				[dbo].[Blogs]
WHERE
				([dbo].[Blogs].[Id] = 2)
";
			string expectedQ2 =
				@"SELECT
				[dbo].[Blogs].[Name]
FROM
				[dbo].[Blogs]
WHERE
				([dbo].[Blogs].[Id] = 2)
";

			#endregion

			var from = new FromClause(SQL.Blogs);
			var where = new WhereClause(SQL.Blogs.Id == 2);

			SQLQuery q1 = SQLQuery
				.Select(SQL.Blogs.Id)
				.From(from)
				.Where(where);

			SQLQuery q2 = SQLQuery
				.Select(SQL.Blogs.Name)
				.From(from)
				.Where(where);

			Assert.AreEqual(expectedQ1, q1.ToString());
			Assert.AreEqual(expectedQ2, q2.ToString());
		}

		[Test]
		public void SimpleSelect()
		{
			#region expected

			string expected =
				@"SELECT
				[dbo].[Blogs].[Id],
				[dbo].[Blogs].[Name]
FROM
				[dbo].[Blogs]
";

			#endregion

			SQLQuery q = SQLQuery
				.Select(SQL.Blogs.Id, SQL.Blogs.Name)
				.From(SQL.Blogs);

			Assert.AreEqual(expected, q.ToString());
		}

		[Test]
		public void SimpleSelectWithJoin()
		{
			#region expected

			string expected =
				@"SELECT
				[dbo].[Blogs].[Id] AS [MyId],
				[dbo].[Blogs].[Name]
FROM
				[dbo].[Blogs]
	JOIN		[dbo].[Posts] ON
					([dbo].[Posts].[BlogId] = [dbo].[Blogs].[Id])
";

			#endregion

			SQLQuery q = SQLQuery
				.Select(SQL.Blogs.Id.As("MyId"), SQL.Blogs.Name)
				.From(SQL.Blogs)
				.Join(SQL.Posts, SQL.Posts.BlogId == SQL.Blogs.Id);

			Spit(expected);
			Spit(q);

			Assert.AreEqual(expected, q.ToString());
		}

		[Test]
		public void SimpleSelectWithJoinAndTableAlias()
		{
			#region expected

			string expected =
				@"SELECT
				[Message].[Id],
				[Message].[ParentId],
				[Message].[Content]
FROM
				[dbo].[ForumMessages] AS [Message]
	JOIN		[dbo].[ForumMessages] AS [Parent] ON
					([Message].[ParentId] = [Parent].[Id])
";

			#endregion

			Tables_ForumMessages Message = SQL.ForumMessages.As("Message");
			Tables_ForumMessages Parent = SQL.ForumMessages.As("Parent");

			SQLQuery q = SQLQuery
				.Select(Message.Id, Message.ParentId, Message.Content)
				.From(Message)
				.Join(Parent, Message.ParentId == Parent.Id);

			Assert.AreEqual(expected, q.ToString());
		}

		[Test]
		public void SimpleSelectWithSimpleFieldAlias()
		{
			#region expected

			string expected =
				@"SELECT
				[dbo].[Blogs].[Id] AS [MyId],
				[dbo].[Blogs].[Name]
FROM
				[dbo].[Blogs]
";

			#endregion

			SQLQuery q = SQLQuery
				.Select(SQL.Blogs.Id.As("MyId"), SQL.Blogs.Name)
				.From(SQL.Blogs);

			Assert.AreEqual(expected, q.ToString());
		}

		[Test]
		public void SimpleSelectWithTableAlias()
		{
			#region expected

			string expected =
				@"SELECT
				[MyBlogs].[Id] AS [MyId],
				[MyBlogs].[Name]
FROM
				[dbo].[Blogs] AS [MyBlogs]
";

			#endregion

			Tables_Blogs MyBlogs = SQL.Blogs.As("MyBlogs");

			SQLQuery q = SQLQuery
				.Select(MyBlogs.Id.As("MyId"), MyBlogs.Name)
				.From(MyBlogs);

			Assert.AreEqual(expected, q.ToString());
		}

		[Test]
		public void SimpleSelectWithWhere()
		{
			#region expected

			string expected =
				@"SELECT
				[dbo].[Blogs].[Id],
				[dbo].[Blogs].[Name]
FROM
				[dbo].[Blogs]
WHERE
				([dbo].[Blogs].[Id] = 20)
";

			#endregion

			SQLQuery q = SQLQuery
				.Select(SQL.Blogs.Id, SQL.Blogs.Name)
				.From(SQL.Blogs)
				.Where(SQL.Blogs.Id == 20);

			Assert.AreEqual(expected, q.ToString());
		}

		[Test]
		public void SimpleSelectWithWhereAndParameter()
		{
			#region expected

			string expected =
				@"SELECT
				[dbo].[Blogs].[Id],
				[dbo].[Blogs].[Name]
FROM
				[dbo].[Blogs]
WHERE
				([dbo].[Blogs].[Id] = @BlogId)
";

			#endregion

			var blogId = new Parameter<int>("BlogId");

			SQLQuery q = SQLQuery
				.Select(SQL.Blogs.Id, SQL.Blogs.Name)
				.From(SQL.Blogs)
				.Where(SQL.Blogs.Id == blogId);

			Assert.AreEqual(expected, q.ToString());
		}
	}
}