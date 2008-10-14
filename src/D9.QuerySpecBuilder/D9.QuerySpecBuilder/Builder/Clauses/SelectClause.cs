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
using System.Collections.Generic;
using System.Linq;
using D9.QuerySpecBuilder.Builder.Expressions;
using D9.QuerySpecBuilder.Helpers;

namespace D9.QuerySpecBuilder.Builder.Clauses
{
	/// <summary>
	/// Represents a SELECT clause
	/// </summary>
	public class SelectClause
	{
		private readonly List<IExpression> expressions = new List<IExpression>();
		private readonly QueryBuilder owningQuery;
		private int? top;

		/// <summary>
		/// Creates a new SELECT clause
		/// </summary>
		/// <param name="owningQuery"></param>
		public SelectClause(QueryBuilder owningQuery)
		{
			this.owningQuery = owningQuery;
		}

		/// <summary>
		/// SELECT (or SELECT TOP)
		/// </summary>
		protected virtual string ClauseVerb
		{
			get
			{
				if (!top.HasValue)
				{
					return "SELECT";
				}
				return ("SELECT TOP " + top);
			}
		}

		/// <summary>
		/// Back to query
		/// </summary>
		public QueryBuilder End
		{
			get { return owningQuery; }
		}

		/// <summary>
		/// The expression in the current clause
		/// </summary>
		internal List<IExpression> Expressions
		{
			get { return expressions; }
		}

		/// <summary>
		/// What separates between expressions in this type of clause
		/// </summary>
		protected virtual string ExpressionsSeparator
		{
			get { return ("," + Environment.NewLine); }
		}

		/// <summary>
		/// Adds a field definition to the SELECT clause
		/// </summary>
		/// <param name="expression">Field definition</param>
		/// <returns>The current clause</returns>
		public SelectClause Add(string expression)
		{
			Expressions.Add(new Expression(expression));
			return this;
		}

		/// <summary>
		/// Adds a a sub query as field definition to the SELECT clause
		/// </summary>
		/// <param name="query">The sub query</param>
		/// <param name="alias">field alias</param>
		/// <returns>The current clause</returns>
		public SelectClause Add(QueryBuilder query, string alias)
		{
			owningQuery.SubQueries.Add(query);
			query.IsSubQuery = true;
			Expressions.Add(new Expression(query).As(alias));
			return this;
		}

		/// <summary>
		/// Adds an aliased field definition to the SELECT clause
		/// </summary>
		/// <param name="expression">field</param>
		/// <param name="alias">alias</param>
		/// <returns>The current clause</returns>
		public SelectClause Add(string expression, string alias)
		{
			Expressions.Add(new Expression(expression).As(alias));
			return this;
		}

		/// <summary>
		/// Builds the SQL fragment representing this clause's expressions
		/// </summary>
		/// <returns>SQL fragment</returns>
		protected virtual string GetExpressionsString()
		{
			if (Expressions.Count == 0)
			{
				return "*";
			}
			return string.Join(ExpressionsSeparator,
			                   Expressions.Select(delegate(IExpression e) { return e.GetQueryString(); }).ToArray());
		}

		/// <summary>
		/// Builds the SQL fragment representing this clause
		/// </summary>
		/// <returns>SQL fragment</returns>
		public virtual string GetQueryString()
		{
			var builder = new IndentingStringBuilder();
			builder.AppendLine(ClauseVerb, new object[0]);
			builder.In(1);
			builder.Append(GetExpressionsString(), new object[0]);
			builder.Out(1);
			return builder.ToString();
		}

		/// <summary>
		/// Sets TOP modifier on the SELECT clause
		/// </summary>
		/// <param name="rows">Maximum number of rows to return</param>
		/// <returns>The current <see cref="SelectClause"/></returns>
		public SelectClause Top(int rows)
		{
			top = rows;
			return this;
		}
	}
}