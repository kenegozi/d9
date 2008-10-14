namespace D9.QuerySpecBuilder.Builder.Expressions
{
	/// <summary>
	/// Represents a <c>sql</c> expression
	/// </summary>
	public class Expression : IExpression
	{
		private readonly object expression;
		private bool parenthesise;

		/// <summary>
		/// Creates a new expression for a given query
		/// </summary>
		/// <param name="query">the query</param>
		public Expression(QueryBuilder query)
		{
			expression = query;
		}

		/// <summary>
		/// Creates a new expression for a given SQL literal
		/// </summary>
		/// <param name="literal">Raw SQL</param>
		public Expression(string literal)
		{
			expression = literal;
		}

		internal string Alias { get; private set; }

		#region IExpression Members

		public virtual string GetQueryString()
		{
			string expressionString = GetExpressionString();
			if (parenthesise)
			{
				expressionString = "(" + expressionString + ")";
			}
			if (Alias == null)
			{
				return expressionString;
			}
			return (expressionString + " AS " + Alias);
		}

		#endregion

		/// <summary>
		/// Sets alias on the current expression
		/// </summary>
		/// <param name="alias">The alias</param>
		/// <returns>The current Expression</returns>
		public Expression As(string alias)
		{
			Alias = alias;
			return this;
		}

		private string GetExpressionString()
		{
			if (expression is IExpression)
			{
				return ((IExpression) expression).GetQueryString();
			}
			if (expression is QueryBuilder)
			{
				return ((QueryBuilder) expression).GetQueryString();
			}
			return expression.ToString();
		}

		/// <summary>
		/// Sets the current Expression as Parenthesised
		/// </summary>
		/// <returns>the current Expression</returns>
		public Expression Parenthesise()
		{
			parenthesise = true;
			return this;
		}

		public override string ToString()
		{
			return GetQueryString();
		}
	}
}