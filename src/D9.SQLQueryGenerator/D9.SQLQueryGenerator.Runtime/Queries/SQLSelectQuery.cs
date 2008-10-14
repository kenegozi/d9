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

using System.Text;
using D9.SQLQueryGenerator.Runtime.Clauses;
using D9.SQLQueryGenerator.Runtime.Expressions;
using D9.SQLQueryGenerator.Runtime.Model.Table;

namespace D9.SQLQueryGenerator.Runtime.Queries
{
	/// <summary>
	/// Represents a SQL Query
	/// </summary>
	public class SQLSelectQuery : SQLQuery
	{
		private readonly SelectClause selectClause;
		private FromClause fromClause;
		private OrderByClause orderByClause;
		private WhereClause whereClause;

		/// <summary>
		/// Creates a new SQLSelectQuery with the given <see cref="SelectClause"/>
		/// </summary>
		/// <param name="selectClause">The select clause of the new query</param>
		public SQLSelectQuery(SelectClause selectClause)
		{
			this.selectClause = selectClause;
		}

		public override string ToString()
		{
			StringBuilder select = new StringBuilder()
				.Append(selectClause);

			if (fromClause != null)
				select.Append(fromClause);

			if (whereClause != null)
				select.Append(whereClause);

			if (orderByClause != null)
				select.Append(orderByClause);

			return select.ToString();
		}

		/// <summary>
		/// Adds a FROM clause based on a table
		/// </summary>
		/// <param name="table">The table to include in the FROM clause</param>
		/// <returns>The current <see cref="SQLSelectQuery"/></returns>
		public SQLSelectQuery From(AbstractTable table)
		{
			return From(new FromClause(table));
		}

		/// <summary>
		/// Adds a FROM clause
		/// </summary>
		/// <param name="from">The from clause</param>
		/// <returns>The current <see cref="SQLSelectQuery"/></returns>
		public SQLSelectQuery From(FromClause from)
		{
			fromClause = from;
			return this;
		}

		/// <summary>
		/// Join
		/// </summary>
		/// <param name="table">Table to JOIN</param>
		/// <param name="on">the ON expression for the JOIN</param>
		/// <returns>The current <see cref="SQLSelectQuery"/></returns>
		public SQLSelectQuery Join(AbstractTable table, WhereExpression on)
		{
			fromClause.Join(new JoinExpression(table, on));
			return this;
		}

		/// <summary>
		/// Adds a WHERE clause
		/// </summary>
		/// <param name="where">WHERE expression</param>
		/// <returns>The current <see cref="SQLSelectQuery"/></returns>
		public SQLSelectQuery Where(WhereExpression where)
		{
			return Where(new WhereClause(where));
		}

		/// <summary>
		/// Adds a WHERE clause
		/// </summary>
		/// <param name="where">WHERE clause</param>
		/// <returns>The current <see cref="SQLSelectQuery"/></returns>
		public SQLSelectQuery Where(WhereClause where)
		{
			whereClause = where;
			return this;
		}

		/// <summary>
		/// Adds an ORDER BY clause
		/// </summary>
		/// <param name="order">ORDER BY</param>
		/// <returns>The current <see cref="SQLSelectQuery"/></returns>
		public SQLSelectQuery OrderBy(OrderByClause order)
		{
			orderByClause = order;
			return this;
		}

		/// <summary>
		/// Adds an ORDER BY clause
		/// </summary>
		/// <param name="orders">ORDER BY expressions</param>
		/// <returns>The current <see cref="SQLSelectQuery"/></returns>
		public SQLSelectQuery OrderBy(params OrderByExpression[] orders)
		{
			return OrderBy(new OrderByClause(orders));
		}

		/// <summary>
		/// A SQL string represented by the current <see cref="SQLSelectQuery"/>
		/// </summary>
		/// <param name="q">The current <see cref="SQLSelectQuery"/></param>
		/// <returns>SQL string</returns>
		public static implicit operator string(SQLSelectQuery q)
		{
			return q.ToString();
		}
	}
}