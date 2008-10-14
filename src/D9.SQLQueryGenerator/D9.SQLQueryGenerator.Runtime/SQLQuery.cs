using D9.SQLQueryGenerator.Runtime.Clauses;
using D9.SQLQueryGenerator.Runtime.Model.Field;
using D9.SQLQueryGenerator.Runtime.Queries;

namespace D9.SQLQueryGenerator.Runtime
{
	/// <summary>
	/// Build a SQL query
	/// </summary>
	public class SQLQuery
	{
		/// <summary>
		/// SELECT clause
		/// </summary>
		/// <param name="fields">SELECT columns</param>
		/// <returns><see cref="SQLSelectQuery"/></returns>
		public static SQLSelectQuery Select(params IFormatableField[] fields)
		{
			var selectClause = new SelectClause(fields);
			var q = new SQLSelectQuery(selectClause);
			return q;
		}

		/// <summary>
		/// A SQL string represented by the current <see cref="SQLQuery"/>
		/// </summary>
		/// <param name="q">The current <see cref="SQLQuery"/></param>
		/// <returns>SQL string</returns>
		public static implicit operator string(SQLQuery q)
		{
			return q.ToString();
		}
	}
}