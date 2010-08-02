#region License

// Copyright (c) 2008-2010 Ken Egozi (ken@kenegozi.com)
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

using D9.QuerySpecBuilder.Builder;
using NUnit.Framework;
using NUnit.Framework.ExtensionMethods;

namespace D9.QuerySpecBuilder.Tests
{
	[TestFixture]
	public class EndToEndTests
	{
		[Test]
		public void SelectStar()
		{
			var expected =
@"SELECT
	*
FROM
	Customers
";
			var q = new QueryBuilder()
				.From
					.Table("Customers")
					.End
				;

			q.GetQueryString().Should(Be.EqualTo(expected));
		}

		[Test, Ignore("Currently not needed")]
		public void SelectSelectFields()
		{
			var expected =
@"SELECT
	(
		SELECT
			Id
		FROM
			TBL
	) AS TblId
FROM
	Customers
";
			var inner = new QueryBuilder()
				.Select
					.Add("Id")
					.End
				.From
					.Table("TBL")
					.End;

			var q = new QueryBuilder()
				.Select
					.Add(inner, "TblId")
					.End
                .From
					.Table("Customers")
					.End;

			q.GetQueryString().Should(Be.EqualTo(expected));
		}

		[Test]
		public void SelectSimpleFields()
		{
			var expected =
@"SELECT
	Id,
	Name
FROM
	Customers
";
			var q = new QueryBuilder();
			var s = q.Select;
			s.Add("Id");
			s.Add("Name");
			var f = q.From;
			f.Table("Customers");

			q.GetQueryString().Should(Be.EqualTo(expected));
		}

		[Test]
		public void SimpleJoins()
		{
			var expected =
@"SELECT
	*
FROM
				Customers AS c
	INNER JOIN	Orders AS o ON (o.CustomerId = c.Id)
";
			var q = new QueryBuilder()
				.From
					.Table("Customers", "c")
					.Add(Joins.Table("Orders").As("o").On("o.CustomerId = c.Id"))
					.End
				;

			q.GetQueryString().Should(Be.EqualTo(expected));
		}

		[Test]
		public void SelectFromQuery()
		{
			var expected =
@"SELECT
	i.Id
FROM
	(SELECT
		*
	FROM
		Customers
	) AS i
";
			var inner = new QueryBuilder()
				.From
					.Table("Customers")
					.End
				;
			var q = new QueryBuilder()
				.Select
					.Add("i.Id")
					.End
				.From
					.Query(inner, "i")
					.End
				;

			q.GetQueryString().Should(Be.EqualTo(expected));
		}

		[Test]
		public void SelectStarWhere()
		{
			var expected =
@"SELECT
	*
FROM
	Customers
WHERE
		Name LIKE @Name
";
			var q = new QueryBuilder()
				.From
					.Table("Customers")
					.End
				.Where
					.Add("Name LIKE @Name")
					.End
				;

			q.GetQueryString().Should(Be.EqualTo(expected));
		}

		[Test]
		public void SelectStarOrder()
		{
			var expected =
@"SELECT
	*
FROM
	Customers
ORDER BY
	Id,
	Name
";
			var q = new QueryBuilder()
				.From
					.Table("Customers")
					.End
				.OrderBy
					.Add("Id")
					.Add("Name")
					.End
				;

			q.GetQueryString().Should(Be.EqualTo(expected));
		}

		[Test]
		public void SelectStarGroup()
		{
			var expected =
@"SELECT
	*
FROM
	Customers
GROUP BY
	Id,
	Name
";
			var q = new QueryBuilder()
				.From
					.Table("Customers")
					.End
				.GroupBy
					.Add("Id")
					.Add("Name")
					.End
				;

			q.GetQueryString().Should(Be.EqualTo(expected));
		}

		[Test, Ignore("Currently not needed")]
		public void SubQueries_AreBeingRegistered()
		{
		}

		[Test]
		public void SubQueries_DoesNotRenderOrderByClauseWithoutTop()
		{
			var expected =
@"SELECT
	COUNT (*)
FROM
	(SELECT
		*
	FROM
		Customers
	) AS inner
";
			var inner = new QueryBuilder()
				.From
					.Table("Customers")
					.End
				.OrderBy
					.Add("SHOULD NOT BE RENDERED")
					.End
				;

			var q = new QueryBuilder()
				.Select
					.Add("COUNT (*)")
					.End
				.From
					.Query(inner, "inner")
					.End
				;

			q.GetQueryString().Should(Be.EqualTo(expected));

		}
	}
}