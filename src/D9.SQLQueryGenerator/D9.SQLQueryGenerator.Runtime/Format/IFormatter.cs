using D9.SQLQueryGenerator.Runtime.Model.Field;
using D9.SQLQueryGenerator.Runtime.Model.Table;

namespace D9.SQLQueryGenerator.Runtime.Format
{
	/// <summary>
	/// Represents a formatter to various SQL dialects
	/// </summary>
	public interface IFormatter
	{
		/// <summary>
		/// Formats a field
		/// </summary>
		/// <param name="field">The field</param>
		/// <returns>SQL Fragment</returns>
		string Format(IFormatableField field);

		/// <summary>
		/// Formats a field for select clause
		/// </summary>
		/// <param name="field">The field</param>
		/// <returns>SQL Fragment</returns>
		string FormatForSelectClause(IFormatableField field);

		/// <summary>
		/// Formats a table name
		/// </summary>
		/// <param name="table">The table</param>
		/// <returns>SQL Fragment</returns>
		string Format(IFormatableTable table);

		/// <summary>
		/// Formats a table name
		/// </summary>
		/// <param name="table">The table</param>
		/// <returns>SQL Fragment</returns>
		string FormatForFromClause(IFormatableTable table);
	}
}