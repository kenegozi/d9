using D9.QuerySpecBuilder.Builder.Expressions;

namespace D9.QuerySpecBuilder.Builder.Clauses
{
	/// <summary>
	/// represents an ORDER BY clause
	/// </summary>
	public class OrderByClause : AbstractClause
	{
		/// <summary>
		/// Creates a new ORDER BY clause for a given <paramref name="owningQuery"/>
		/// </summary>
		/// <param name="owningQuery">The query</param>
		public OrderByClause(QueryBuilder owningQuery)
			: base(owningQuery)
		{
		}

		protected override string ClauseVerb
		{
			get { return "ORDER BY"; }
		}

		/// <summary>
		/// Adds a new expression to the ORDER BY clause
		/// </summary>
		/// <param name="expression">order expression</param>
		/// <returns>The current <see cref="OrderByClause"/></returns>
		public OrderByClause Add(string expression)
		{
			Expressions.Add(new Expression(expression));
			return this;
		}

		/// <summary>
		/// Clear all orders from the current clause 
		/// <remarks>(used in sub selects)</remarks>
		/// </summary>
		public virtual void Clear()
		{
			Expressions.Clear();
		}
	}
}