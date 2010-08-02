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
