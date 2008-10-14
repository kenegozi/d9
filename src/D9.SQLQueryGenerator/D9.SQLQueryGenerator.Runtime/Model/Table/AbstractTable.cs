using D9.SQLQueryGenerator.Runtime.Format;

namespace D9.SQLQueryGenerator.Runtime.Model.Table
{
	/// <summary>
	/// Basic logic for tables
	/// </summary>
	public abstract class AbstractTable : IFormatableTable
	{
		private readonly string alias;
		private readonly string name;
		private readonly string schema;

		/// <summary>
		/// Creates a new aliased table
		/// </summary>
		/// <param name="schema">Schema</param>
		/// <param name="name">Name</param>
		/// <param name="alias">Alias</param>
		protected AbstractTable(string schema, string name, string alias)
		{
			this.schema = schema;
			this.name = name;
			this.alias = alias;
		}

		/// <summary>
		/// Creates a new table
		/// </summary>
		/// <param name="schema">Schema</param>
		/// <param name="name">Name</param>
		protected AbstractTable(string schema, string name)
		{
			this.schema = schema;
			this.name = name;
		}

		#region IFormatableTable Members

		string IFormatableTable.Schema
		{
			get { return schema; }
		}

		string IFormatableTable.Name
		{
			get { return name; }
		}

		string IFormatableTable.Alias
		{
			get { return alias; }
		}

		#endregion

		public override string ToString()
		{
			return Formatting.Format(this);
		}
	}
}