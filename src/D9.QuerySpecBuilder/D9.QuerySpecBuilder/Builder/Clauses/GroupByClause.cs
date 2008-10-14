using D9.QuerySpecBuilder.Builder.Expressions;

namespace D9.QuerySpecBuilder.Builder.Clauses
{
	/// <summary>
	/// represents a GROUP BY clause
	/// </summary>
	public class GroupByClause : AbstractClause
	{
		/// <summary>
		/// Creates a new GROUP BY clause for a given <paramref name="owningQuery"/>
		/// </summary>
		/// <param name="owningQuery">The query</param>
		public GroupByClause(QueryBuilder owningQuery)
			: base(owningQuery)
		{
		}

		protected override string ClauseVerb
		{
			get { return "GROUP BY"; }
		}

		/// <summary>
		/// Adds a new expression to the GROUP BY clause
		/// </summary>
		/// <param name="expression">group expression</param>
		/// <returns>The current <see cref="GroupByClause"/></returns>
		public GroupByClause Add(string expression)
		{
			Expressions.Add(new Expression(expression));
			return this;
		}
	}
}