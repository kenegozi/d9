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