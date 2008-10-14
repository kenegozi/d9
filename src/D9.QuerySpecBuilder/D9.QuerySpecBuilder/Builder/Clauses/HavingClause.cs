using D9.QuerySpecBuilder.Builder.Expressions;

namespace D9.QuerySpecBuilder.Builder.Clauses
{
	/// <summary>
	/// represents a HAVING clause
	/// </summary>
	public class HavingClause : AbstractClause
	{
		/// <summary>
		/// Creates a new HAVING clause for a given <paramref name="owningQuery"/>
		/// </summary>
		/// <param name="owningQuery">The query</param>
		public HavingClause(QueryBuilder owningQuery)
			: base(owningQuery)
		{
		}

		protected override string ClauseVerb
		{
			get { return "HAVING"; }
		}

		/// <summary>
		/// Adds a new expression to the HAVING clause
		/// </summary>
		/// <param name="expression">having expression</param>
		/// <returns>The current <see cref="HavingClause"/></returns>
		public HavingClause Add(string expression)
		{
			Expressions.Add(new Expression(expression));
			return this;
		}
	}
}