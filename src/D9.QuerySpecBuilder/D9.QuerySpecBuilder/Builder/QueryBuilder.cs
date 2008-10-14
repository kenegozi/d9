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
		internal QueryBuilder Wrapper;

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

