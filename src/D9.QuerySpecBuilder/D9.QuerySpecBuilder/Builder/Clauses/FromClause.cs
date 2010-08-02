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

using System.Collections.Generic;
using D9.QuerySpecBuilder.Builder.Expressions;
using D9.QuerySpecBuilder.Helpers;

namespace D9.QuerySpecBuilder.Builder.Clauses
{
	/// <summary>
	/// represents a FROM clause
	/// </summary>
	public class FromClause : AbstractClause
	{
		private readonly List<JoinExpression> joins;
		private Expression root;

		/// <summary>
		/// Creates a new FROM clause for a given <paramref name="owningQuery"/>
		/// </summary>
		/// <param name="owningQuery">The query</param>
		public FromClause(QueryBuilder owningQuery)
			: base(owningQuery)
		{
			joins = new List<JoinExpression>();
		}

		protected override string ClauseVerb
		{
			get { return "FROM"; }
		}

		/// <summary>
		/// Adds a new join expression to the WHERE clause
		/// </summary>
		/// <param name="join">join expression</param>
		/// <returns>The current <see cref="FromClause"/></returns>
		public FromClause Add(JoinExpression join)
		{
			joins.Add(join);
			return this;
		}

		public override string GetQueryString()
		{
			var builder = new IndentingStringBuilder();
			builder.AppendLine(ClauseVerb);
			if (joins.Count == 0)
			{
				builder.In(1).AppendLine(root.GetQueryString()).Out(1);
			}
			else
			{
				builder.In(4).AppendLine(root.GetQueryString()).Out(3);
				foreach (JoinExpression expression in joins)
				{
					builder.AppendLine(expression.GetQueryString());
				}
				builder.Out(1);
			}
			return builder.ToString();
		}

		/// <summary>
		/// Sets a sub query as the source of a FROM clause
		/// </summary>
		/// <param name="query">The sub query</param>
		/// <param name="alias">The table expression's alias</param>
		/// <returns>the current <see cref="FromClause"/></returns>
		public FromClause Query(QueryBuilder query, string alias)
		{
			owningQuery.SubQueries.Add(query);
			query.IsSubQuery = true;
			root = new Expression(query).Parenthesise().As(alias);
			return this;
		}

		/// <summary>
		/// Sets a table (or view) as the source of a FROM clause
		/// </summary>
		/// <param name="table">The table (or view) name</param>
		/// <returns>the current <see cref="FromClause"/></returns>
		public FromClause Table(string table)
		{
			root = new Expression(table);
			return this;
		}

		/// <summary>
		/// Sets an aliased table (or view) as the source of a FROM clause
		/// </summary>
		/// <param name="table">The table (or view) name</param>
		/// <param name="alias">The alias</param>
		/// <returns>the current <see cref="FromClause"/></returns>
		public FromClause Table(string table, string alias)
		{
			root = new Expression(table).As(alias);
			return this;
		}
	}
}