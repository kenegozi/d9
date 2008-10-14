namespace D9.SQLQueryGenerator.Runtime.Expressions
{
	public class OrderByExpression
	{
		readonly Model.Field.AbstractField field;
		bool desc;

		public OrderByExpression(Model.Field.AbstractField field) 
		{
			this.field = field;
		}

		public override string ToString()
		{
			return "\t\t\t\t" + field + (desc ? " DESC" : string.Empty);
		}

		public static explicit operator string (OrderByExpression order)
		{
			return order.ToString();
		}

		public OrderByExpression Desc
		{
			get
			{
				desc = true;
				return this;
			}
		}
	}

	public static class Order
	{
		public static OrderByExpression By(Model.Field.AbstractField field)
		{
			return new OrderByExpression(field);
		}
	}
}
