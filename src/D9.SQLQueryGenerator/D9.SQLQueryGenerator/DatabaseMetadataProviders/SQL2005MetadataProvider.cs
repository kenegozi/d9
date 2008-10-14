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
using System.Data;
using System.Data.SqlClient;

namespace D9.SQLQueryGenerator.DatabaseMetadataProviders
{
	/// <summary>
	/// Extract metadata from SQL Server 2005 databases
	/// </summary>
	public class SQL2005MetadataProvider : IDatabaseMetadataProvider
	{
		#region SQL

		private const string SQL =
			@"
SELECT
	schemas.name AS [Schema],
	tables.name AS [Table],
	columns.name AS [Column],
	CASE 
		WHEN columns.system_type_id = 34	THEN 'System.Byte[]'
		WHEN columns.system_type_id = 35	THEN 'System.String'
		WHEN columns.system_type_id = 36	THEN 'System.Guid'
		WHEN columns.system_type_id = 48	THEN 'System.Byte'
		WHEN columns.system_type_id = 52	THEN 'System.Int16'
		WHEN columns.system_type_id = 56	THEN 'System.Int32'
		WHEN columns.system_type_id = 58	THEN 'System.DateTime'
		WHEN columns.system_type_id = 59	THEN 'System.Double'
		WHEN columns.system_type_id = 60	THEN 'System.Decimal'
		WHEN columns.system_type_id = 61	THEN 'System.DateTime'
		WHEN columns.system_type_id = 62	THEN 'System.Double'
		WHEN columns.system_type_id = 98	THEN 'System.Object'
		WHEN columns.system_type_id = 99	THEN 'System.String'
		WHEN columns.system_type_id = 104	THEN 'System.Boolean'
		WHEN columns.system_type_id = 106	THEN 'System.Decimal'
		WHEN columns.system_type_id = 108	THEN 'System.Decimal'
		WHEN columns.system_type_id = 122	THEN 'System.Decimal'
		WHEN columns.system_type_id = 127	THEN 'System.Int64'
		WHEN columns.system_type_id = 165	THEN 'System.Byte[]'
		WHEN columns.system_type_id = 167	THEN 'System.String'
		WHEN columns.system_type_id = 173	THEN 'System.Byte[]'
		WHEN columns.system_type_id = 175	THEN 'System.String'
		WHEN columns.system_type_id = 189	THEN 'System.Int64'
		WHEN columns.system_type_id = 231	THEN 'System.String'
		WHEN columns.system_type_id = 239	THEN 'System.String'
		WHEN columns.system_type_id = 241	THEN 'System.String'
		WHEN columns.system_type_id = 241	THEN 'System.String'
	END AS [Type],
	columns.is_nullable AS [IsNullable]

FROM
				sys.tables tables
	INNER JOIN	sys.schemas schemas ON (tables.schema_id = schemas.schema_id )
	INNER JOIN	sys.columns columns ON (columns.object_id = tables.object_id)

WHERE
				tables.name <> 'sysdiagrams' 
	AND			tables.name <> 'dtproperties'

ORDER BY [Schema], [Table], [Column], [Type]";

		#endregion

		private readonly string connectionString;

		/// <summary>
		/// New provider for a given connection string
		/// </summary>
		/// <param name="connectionString">The connection string</param>
		public SQL2005MetadataProvider(string connectionString)
		{
			this.connectionString = connectionString;
		}

		/// <summary>
		/// New provider with SQL security
		/// </summary>
		/// <param name="server">server</param>
		/// <param name="dbName">DB</param>
		/// <param name="userId">user</param>
		/// <param name="password">password</param>
		public SQL2005MetadataProvider(string server, string dbName, string userId, string password)
		{
			connectionString = string.Format(
				"Server={0};Initial Catalog={1};User ID={2};Password={3};",
				server, dbName, userId, password);
		}

		/// <summary>
		/// New provider with windows security
		/// </summary>
		/// <param name="server">server</param>
		/// <param name="dbName">DB</param>
		public SQL2005MetadataProvider(string server, string dbName)
		{
			connectionString = string.Format(
				"Server={0};Initial Catalog={1};Integrated Security=SSPI;",
				server, dbName);
		}

		#region IDatabaseMetadataProvider Members

		public IEnumerable<DbPropertyMetadata> ExtractMetadata()
		{
			var properties = new List<DbPropertyMetadata>();

			using (var con = new SqlConnection(connectionString))
			using (IDbCommand cmd = new SqlCommand(SQL, con))
			{
				con.Open();
				using (IDataReader reader = cmd.ExecuteReader())
				{
					while (reader.Read())
						properties.Add(new DbPropertyMetadata(
						               	reader.GetString(0),
						               	reader.GetString(1),
						               	reader.GetString(2),
						               	reader.GetString(3),
						               	reader.GetBoolean(4)));
				}
			}

			return properties;
		}

		#endregion
	}
}