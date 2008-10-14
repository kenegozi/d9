using System.Collections.Generic;
using D9.SQLQueryGenerator.DatabaseMetadataProviders;
using D9.SQLQueryGenerator.Descriptors;
using D9.SQLQueryGenerator.Helpers;

namespace D9.SQLQueryGenerator
{
	/// <summary>
	/// Processes metadata
	/// </summary>
	public class MetadataProcessor
	{
		private IDictionary<string, TableDescriptor> tables;

		/// <summary>
		/// Extracts a <see cref="TableDescriptor"/> out of a metadata
		/// </summary>
		/// <param name="propertyMetadata">property</param>
		/// <param name="schemaInClassName">schema</param>
		/// <returns><see cref="TableDescriptor"/></returns>
		public TableDescriptor GetTableDescriptorFrom(DbPropertyMetadata propertyMetadata, bool schemaInClassName)
		{
			var table = new TableDescriptor(propertyMetadata.Schema, propertyMetadata.Table, schemaInClassName);
			if (tables.ContainsKey(table.ClassName))
				return tables[table.ClassName];

			tables.Add(table.ClassName, table);
			return table;
		}

		/// <summary>
		/// Processes metadata
		/// </summary>
		/// <param name="propertyMetadata">property</param>
		/// <param name="schemaInClassName">schema</param>
		public void Process(DbPropertyMetadata propertyMetadata, bool schemaInClassName)
		{
			TableDescriptor table = GetTableDescriptorFrom(propertyMetadata, schemaInClassName);
			table.Add(propertyMetadata);
		}

		/// <summary>
		/// Extracts a bunch of <see cref="TableDescriptor"/> instances out of a metadata
		/// </summary>
		/// <param name="metadata">metadata</param>
		/// <param name="schemaInClassName">schema</param>
		/// <returns><see cref="TableDescriptor"/> instances</returns>
		public IDictionary<string, TableDescriptor> GetTableDescriptorsFrom(IEnumerable<DbPropertyMetadata> metadata,
		                                                                    bool schemaInClassName)
		{
			tables = new Dictionary<string, TableDescriptor>(new CaseInsensitiveStringComparer());

			foreach (DbPropertyMetadata propertyMetadata in metadata)
			{
				Process(propertyMetadata, schemaInClassName);
			}
			return tables;
		}
	}
}