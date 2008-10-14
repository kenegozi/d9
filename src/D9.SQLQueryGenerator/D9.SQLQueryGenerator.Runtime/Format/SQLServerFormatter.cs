using D9.SQLQueryGenerator.Runtime.Model.Field;
using D9.SQLQueryGenerator.Runtime.Model.Table;

namespace D9.SQLQueryGenerator.Runtime.Format
{
	/// <summary>
	/// formatter for SQL Server dialect
	/// </summary>
	public class SQLServerFormatter : IFormatter
	{
		#region IFormatter Members

		/// <summary>
		/// Formats a field
		/// </summary>
		/// <param name="field">The field</param>
		/// <returns>SQL Fragment</returns>
		public string Format(IFormatableField field)
		{
			return field.Table + ".[" + field.Name + "]";
		}

		/// <summary>
		/// Formats a field for select clause
		/// </summary>
		/// <param name="field">The field</param>
		/// <returns>SQL Fragment</returns>
		public string FormatForSelectClause(IFormatableField field)
		{
			string fieldName = Format(field);
			if (field.Alias != null)
				fieldName += " AS [" + field.Alias + "]";

			return fieldName;
		}

		/// <summary>
		/// Formats a table name
		/// </summary>
		/// <param name="table">The table</param>
		/// <returns>SQL Fragment</returns>
		public string Format(IFormatableTable table)
		{
			if (table.Alias != null)
				return "[" + table.Alias + "]";

			return GetTableNameFrom(table);
		}

		/// <summary>
		/// Formats a table name
		/// </summary>
		/// <param name="table">The table</param>
		/// <returns>SQL Fragment</returns>
		public string FormatForFromClause(IFormatableTable table)
		{
			string tableName = GetTableNameFrom(table);

			if (table.Alias != null)
				tableName += " AS [" + table.Alias + "]";

			return tableName;
		}

		#endregion

		private static string GetTableNameFrom(IFormatableTable table)
		{
			return "[" + table.Schema + "].[" + table.Name + "]";
		}
	}
}