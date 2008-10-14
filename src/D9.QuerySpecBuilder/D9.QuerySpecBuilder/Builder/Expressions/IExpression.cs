namespace D9.QuerySpecBuilder.Builder.Expressions
{
	/// <summary>
	/// Represents a <c>sql</c> expression
	/// </summary>
    public interface IExpression
    {
		/// <summary>
		/// Returns the SQL query fragment represented by the current expression
		/// </summary>
		/// <returns>SQL query fragment</returns>
        string GetQueryString();
    }
}

