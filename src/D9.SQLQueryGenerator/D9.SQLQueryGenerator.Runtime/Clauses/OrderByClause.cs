namespace D9.SQLQueryGenerator.Runtime.Clauses
{
	using System.Collections.Generic;
	using Expressions;

	public class OrderByClause : AbstractSqlClause
	{
		public readonly OrderByExpression[] orders;

		public OrderByClause(params OrderByExpression[] orders)
		{
			this.orders = orders;
		}

		public override string ToString()
		{
			System.Text.StringBuilder where = new System.Text.StringBuilder()
				.AppendLine("ORDER BY");

			where.Append(string.Join(@",
", 
				OrdersToStrings(orders)));

			where.AppendLine();

			return where.ToString();
		}

		private static string[] OrdersToStrings(OrderByExpression[] orders)
		{
			List<string> strings = new List<string>(orders.Length);
			foreach (OrderByExpression order in orders)
			{
				strings.Add(order.ToString());
			}
			return strings.ToArray();
		}
	}
}
