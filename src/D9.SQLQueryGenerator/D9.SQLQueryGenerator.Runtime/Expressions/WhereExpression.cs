namespace D9.SQLQueryGenerator.Runtime.Expressions
{
	public class WhereExpression
	{
		public WhereExpression(string expression)
		{
			Expression = "(" + expression + ")";
		}

		public WhereExpression(Model.Field.AbstractField field, string operatorSign, object other)
		{
			string otherExpression = other.ToString();
			if (other is string)
				otherExpression = "N'" + otherExpression.Replace("'", "''") + "'";
			Expression = "(" + field + operatorSign + otherExpression + ")";
		}

		public readonly string Expression;

		public override string ToString()
		{
			return Expression;
		}

		public virtual string ToOnString()
		{
			return "\t\t\t\t\t" + Expression;
		}

		public virtual string ToWhereString()
		{
			return "\t\t\t\t" + Expression;
		}

		public static WhereExpression operator |(WhereExpression where, WhereExpression other)
		{
			return new ComplexWhereExpression(where, " OR ", other);
		}

		public static WhereExpression operator &(WhereExpression where, WhereExpression other)
		{
			return new ComplexWhereExpression(where, " AND ", other);
		}
	
		public static WhereExpression operator !(WhereExpression where)
		{
			return new WhereExpression("NOT " + where);
		}

		public static bool operator true(WhereExpression where)
		{
			return false;
		}

		public static bool operator false(WhereExpression where)
		{
			return false;
		}
	}

	public class ComplexWhereExpression : WhereExpression
	{
		public ComplexWhereExpression(WhereExpression where, string operatorSign, WhereExpression other) : base(where + operatorSign + other) 
		{
		}
	}
}
