namespace D9.SQLQueryGenerator.Runtime.Clauses
{
	public class WhereClause : AbstractSqlClause
	{
		public readonly Expressions.WhereExpression Expression;

		public WhereClause(Expressions.WhereExpression whereExpression)
		{
			this.Expression = whereExpression;
		}

		public override string ToString()
		{
			System.Text.StringBuilder where = new System.Text.StringBuilder()
				.AppendLine("WHERE")
				.AppendLine(Expression.ToWhereString());

			return where.ToString();
		}

		public static WhereClause operator |(WhereClause where, WhereClause other)
		{
			return new WhereClause(where.Expression | other.Expression);
		}
		public static WhereClause operator &(WhereClause where, WhereClause other)
		{
			return new WhereClause(where.Expression & other.Expression);
		}
		public static WhereClause operator !(WhereClause where)
		{
			return new WhereClause(!where.Expression);
		}
		public static bool operator true(WhereClause where)
		{
			return false;
		}
		public static bool operator false(WhereClause where)
		{
			return false;
		}
	}
}
