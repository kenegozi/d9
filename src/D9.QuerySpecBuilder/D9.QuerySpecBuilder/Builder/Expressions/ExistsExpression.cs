namespace D9.QuerySpecBuilder.Builder.Expressions
{
	/// <summary>
	/// Represents an EXISTS expression
	/// </summary>
    public class ExistsExpression
    {
        private readonly QueryBuilder queryBuilder;

		/// <summary>
		/// Created a new EXISTS expression for the given <see cref="QueryBuilder"/>
		/// </summary>
		/// <param name="queryBuilder">The query builder to wrap in EXISTS clause</param>
        public ExistsExpression(QueryBuilder queryBuilder)
        {
            this.queryBuilder = queryBuilder;
        }
    }
}

