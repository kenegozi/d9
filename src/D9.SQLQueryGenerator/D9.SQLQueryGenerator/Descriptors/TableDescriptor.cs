#region License

// Copyright (c) 2008 Ken Egozi (ken@kenegozi.com)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of the D9 project nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion License

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