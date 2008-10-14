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