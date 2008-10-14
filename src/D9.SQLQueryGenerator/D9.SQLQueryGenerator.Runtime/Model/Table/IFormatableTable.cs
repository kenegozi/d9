namespace D9.SQLQueryGenerator.Runtime.Model.Table
{
	/// <summary>
	/// 
	/// </summary>
	public interface IFormatableTable
	{
		/// <summary>
		/// The table's schema
		/// </summary>
		string Schema { get; }

		/// <summary>
		/// The table's name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The table's alias in the current context
		/// </summary>
		string Alias { get; }
	}
}