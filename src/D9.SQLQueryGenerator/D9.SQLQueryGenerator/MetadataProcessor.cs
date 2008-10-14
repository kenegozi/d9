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