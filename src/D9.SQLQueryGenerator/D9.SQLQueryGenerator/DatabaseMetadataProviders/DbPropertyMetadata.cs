using System;

namespace D9.SQLQueryGenerator.DatabaseMetadataProviders
{
	/// <summary>
	/// Metadata
	/// </summary>
	public class DbPropertyMetadata
	{
		public readonly string Column;
		public readonly bool IsNullable;
		public readonly string Schema;
		public readonly string Table;
		public readonly Type Type;

		public DbPropertyMetadata(string schema, string table, string column, string type, bool isNullable)
		{
			Schema = schema;
			Table = table;
			Column = column;
			Type = Type.GetType(type);
			IsNullable = isNullable;

			if (Type == null)
				Console.WriteLine(schema + "." + table + "." + type);
		}

		public override string ToString()
		{
			return string.Format("{0}.{1}.{2}.{3}.{4}",
			                     Schema, Table, Column, Type.Name, IsNullable ? "Nullable" : "Not Nullable");
		}

		public override bool Equals(object obj)
		{
			if (obj == null) return false;
			return ToString().Equals(obj.ToString());
		}

		public override int GetHashCode()
		{
			return ToString().GetHashCode();
		}
	}
}