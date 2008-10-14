using D9.QuerySpecBuilder.Builder;
using D9.QuerySpecBuilder.Builder.Expressions;

namespace D9.QuerySpecBuilder
{
	public static class Subqueries
	{
		public static ExistsExpression Exists(QueryBuilder queryBuilder)
		{
			return new ExistsExpression(queryBuilder);
		}
	}
}

