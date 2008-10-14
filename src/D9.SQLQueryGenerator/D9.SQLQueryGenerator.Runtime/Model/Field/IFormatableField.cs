namespace D9.SQLQueryGenerator.Runtime.Model.Field
{
	/// <summary>
	/// A field in a table
	/// </summary>
	public interface IFormatableField
	{
		/// <summary>
		/// The table this field belongs to
		/// </summary>
		Table.AbstractTable Table { get; }

		/// <summary>
		/// The field's name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The field's alias
		/// </summary>
		string Alias { get; }
	}
}