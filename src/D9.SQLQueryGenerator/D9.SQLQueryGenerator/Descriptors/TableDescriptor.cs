using System;
using System.Collections.Generic;
using D9.SQLQueryGenerator.DatabaseMetadataProviders;
using D9.SQLQueryGenerator.Helpers;

namespace D9.SQLQueryGenerator.Descriptors
{
	/// <summary>
	/// DB Table descriptor
	/// </summary>
	public class TableDescriptor
	{
		///<summary>
		///Class name for the table's
		///</summary>
		public string ClassName { get; private set; }

		/// <summary>
		/// The table's name
		/// </summary>
		public string Name { get; private set; }

		private readonly IDictionary<string, DbPropertyMetadata> properties = new Dictionary<string, DbPropertyMetadata>();

		/// <summary>
		/// The table's schema
		/// </summary>
		public string Schema { get; private set; }

		/// <summary>
		/// Creates a new instance of TableDescriptor
		/// </summary>
		/// <param name="table"></param>
		/// <param name="schemaInClassName"></param>
		public TableDescriptor(string table, bool schemaInClassName)
			: this(null, table, schemaInClassName)
		{
		}

		/// <summary>
		/// Creates a new instance of TableDescriptor
		/// </summary>
		/// <param name="schema"></param>
		/// <param name="table"></param>
		/// <param name="schemaInClassName"></param>
		public TableDescriptor(string schema, string table, bool schemaInClassName)
		{
			Name = table;
			Schema = schema;
			ClassName = GetClassNameFrom(Schema, Name, schemaInClassName);
		}

		/// <summary>
		/// Properties of this table
		/// </summary>
		public ICollection<DbPropertyMetadata> Properties
		{
			get { return properties.Values; }
		}


		private static string GetClassNameFrom(string schema, string name, bool schemaInClassName)
		{
			if (schemaInClassName == false)
				return Formatter.FormatClassNameFrom(name);

			string className = schema == null
								? name
								: schema + "." + name;

			return Formatter.FormatClassNameFrom(className);
		}

		/// <summary>
		/// Add a <paramref name="property"/> to the table
		/// </summary>
		/// <param name="property">The property to add</param>
		public void Add(DbPropertyMetadata property)
		{
			if (properties.ContainsKey(property.Column))
				throw new ArgumentException(string.Format(
												"Duplicate property found: {0}.{1}", Name, property.Column),
											"property");

			properties.Add(property.Column, property);
		}
	}
}