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

using System.Collections.Generic;
using D9.QuerySpecBuilder.Builder.Clauses;
using D9.QuerySpecBuilder.Builder.Expressions;
using D9.QuerySpecBuilder.Helpers;

namespace D9.QuerySpecBuilder.Builder
{
	public class QueryBuilder
	{
		SelectClause selectClause;
		FromClause fromClause;
		WhereClause whereClause;
		GroupByClause groupByClause;
		HavingClause havingClause;
		OrderByClause orderByClause;
		readonly IList<QueryBuilder> subqueries = new List<QueryBuilder>();
		internal bool IsSubQuery { get; set; }

		public SelectClause Select
		{
			get
			{
				if (selectClause == null)
					selectClause = new SelectClause(this);
				return selectClause;
			}
		}

		public FromClause From
		{
			get
			{
				if (fromClause == null)
					fromClause = new FromClause(this);
				return fromClause;
			}
		}

		public WhereClause Where
		{
			get
			{
				if (whereClause == null)
					whereClause = new WhereClause(this);
				return whereClause;
			}
		}

		public GroupByClause GroupBy
		{
			get
			{
				if (groupByClause == null)
					groupByClause = new GroupByClause(this);
				return groupByClause;
			}
		}

		public HavingClause Having
		{
			get
			{
				if (havingClause == null)
					havingClause = new HavingClause(this);
				return havingClause;
			}
		}

		public OrderByClause OrderBy
		{
			get
			{
				if (orderByClause == null)
					orderByClause = new OrderByClause(this);
				return orderByClause;
			}
		}

		/// <summary>
		/// Gets the SQL string described by this builder.
		/// </summary>
		/// <returns>SQL query string</returns>
		public virtual string GetQueryString()
		{
			if ((Limit.Max.HasValue || Limit.First.HasValue) &&
				isWrapped == false)
			{
				return WrapWithLimit().GetQueryString();
			}
			return GetQueryStringForCurrentQuery();

		}

		private string GetQueryStringForCurrentQuery()
		{
			var buf = new IndentingStringBuilder();
			if (selectClause == null)
				selectClause = new SelectClause(this);
			if (selectClause != null)
				buf.AppendLine(selectClause.GetQueryString());

			if (fromClause != null)
				buf.AppendLine(fromClause.GetQueryString());

			if (whereClause != null)
				buf.AppendLine(whereClause.GetQueryString());

			if (groupByClause != null)
				buf.AppendLine(groupByClause.GetQueryString());

			if (havingClause != null)
				buf.AppendLine(havingClause.GetQueryString());

			if (orderByClause != null)
			{
				if (IsSubQuery == false)
					buf.AppendLine(orderByClause.GetQueryString());
			}

			return buf.ToString();

		}

		const string ROWNUM_FIELD = "QUERY_BUILDER_ROWNUM_FIELD";
		const string INTERNAL_QUERY = "QUERY_BUILDER_INTERNAL_QUERY";

		class LimitInfo
		{
			public int? First;
			public int? Max;
		}

		readonly LimitInfo Limit = new LimitInfo();

		public QueryBuilder SetFirstResult(int firstResult)
		{
			Limit.First = firstResult;
			return this;
		}

		public QueryBuilder SetMaxResults(int maxResults)
		{
			Limit.Max = maxResults;
			return this;
		}

		QueryBuilder WrapWithLimit()
		{
			var orderBy = OrderBy.GetQueryString();
			OrderBy.Clear();

			var wrapper = new QueryBuilder();

			foreach (var expression in Select.Expressions)
			{
				var exp = (Expression)expression;
				wrapper.Select.Add(INTERNAL_QUERY + "." + exp.Alias);
			}
			wrapper.From.Query(this, INTERNAL_QUERY);
			Select.Add("ROW_NUMBER() OVER(" + orderBy + " )", ROWNUM_FIELD);

			string where;
			if (Limit.First.HasValue && Limit.Max.HasValue)
				where = " BETWEEN " + (Limit.First.Value + 1) + " AND " + (Limit.First.Value + Limit.Max.Value);
			else if (Limit.First.HasValue && Limit.Max.HasValue == false)
				where = " >= " + (Limit.First.Value + 1);
			else
				where = " <= " + Limit.Max.Value;
			wrapper.Where.Add(ROWNUM_FIELD + where);

			isWrapped = true;
			return wrapper;
		}

		private bool isWrapped;

		public IList<QueryBuilder> SubQueries { get { return subqueries; } }

	}
}

