namespace D9.QuerySpecBuilder.Builder.Expressions
{
	/// <summary>
	/// A SQL JOIN expression
	/// </summary>
	public class JoinExpression : IExpression
	{
		private IExpression alias;
		private JoinType joinType = JoinType.Inner;
		private IExpression on;
		private IExpression table;

		#region IExpression Members

		/// <summary>
		/// Extract the SQL string represented by the current expression
		/// </summary>
		/// <returns>SQL String</returns>
		public virtual string GetQueryString()
		{
			string str = (alias == null) ? table.GetQueryString() : (table + " AS " + alias);
			return string.Format("{0}{1} ON ({2})", joinType, str, on);
		}

		#endregion

		/// <summary>
		/// Sets alias on the joined table
		/// </summary>
		/// <param name="expression">alias</param>
		/// <returns>the current <see cref="JoinExpression"/></returns>
		public JoinExpression As(string expression)
		{
			alias = new Expression(expression);
			return this;
		}

		/// <summary>
		/// Sets current join as OUTER LEFT JOIN
		/// </summary>
		/// <returns>the current <see cref="JoinExpression"/></returns>
		public JoinExpression Left()
		{
			joinType = JoinType.Left;
			return this;
		}

		/// <summary>
		/// Sets the ON part of the current JOIN
		/// </summary>
		/// <param name="expression">Join condition</param>
		/// <returns>the current <see cref="JoinExpression"/></returns>
		public JoinExpression On(string expression)
		{
			on = new Expression(expression);
			return this;
		}

		/// <summary>
		/// Sets current join as OUTER RIGHT JOIN
		/// </summary>
		/// <returns>the current <see cref="JoinExpression"/></returns>
		public JoinExpression Right()
		{
			joinType = JoinType.Right;
			return this;
		}

		/// <summary>
		/// Sets the joined table
		/// </summary>
		/// <param name="expression">The joined table</param>
		/// <returns>the current <see cref="JoinExpression"/></returns>
		public JoinExpression Table(string expression)
		{
			table = new Expression(expression);
			return this;
		}

		public override string ToString()
		{
			return GetQueryString();
		}
	}
}