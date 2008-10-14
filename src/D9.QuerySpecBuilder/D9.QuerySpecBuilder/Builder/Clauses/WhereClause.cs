using System;
using D9.QuerySpecBuilder.Builder.Expressions;

namespace D9.QuerySpecBuilder.Builder.Clauses
{
	/// <summary>
	/// represents a WHERE clause
	/// </summary>
	public class WhereClause : AbstractClause
	{
		/// <summary>
		/// Creates a new WHERE clause for a given <paramref name="owningQuery"/>
		/// </summary>
		/// <param name="owningQuery">The query</param>
		public WhereClause(QueryBuilder owningQuery)
			: base(owningQuery)
		{
		}

		protected override string ClauseVerb
		{
			get { return "WHERE"; }
		}

		protected override string ExpressionsSeparator
		{
			get { return (Environment.NewLine + "and "); }
		}

		/// <summary>
		/// Adds a new expression to the WHERE clause
		/// </summary>
		/// <param name="expression">where expression</param>
		/// <returns>The current <see cref="WhereClause"/></returns>
		public WhereClause Add(string expression)
		{
			Expressions.Add(new Expression(expression));
			return this;
		}

		protected override string GetExpressionsString()
		{
			return ("\t" + base.GetExpressionsString());
		}

		public override string GetQueryString()
		{
			if (Expressions.Count == 0)
			{
				return string.Empty;
			}
			return base.GetQueryString();
		}
	}
}