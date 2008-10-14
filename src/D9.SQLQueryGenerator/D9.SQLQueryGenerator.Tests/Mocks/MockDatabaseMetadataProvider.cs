using System.Collections.Generic;
using D9.SQLQueryGenerator.DatabaseMetadataProviders;

namespace D9.SQLQueryGenerator.Tests.Mocks
{
	public class MockDatabaseMetadataProvider : IDatabaseMetadataProvider
	{
		#region IDatabaseMetadataProvider Members

		public IEnumerable<DbPropertyMetadata> ExtractMetadata()
		{
			return new[]
			       	{
			       		new DbPropertyMetadata("schema", "Blog", "Id", "int", false),
			       		new DbPropertyMetadata("schema", "Blog", "Name", "string", false),
			       		new DbPropertyMetadata("schema", "Post", "Id", "int", false),
			       		new DbPropertyMetadata("schema", "Post", "BlogId", "int", false),
			       		new DbPropertyMetadata("schema", "Post", "Title", "string", false),
			       		new DbPropertyMetadata("schema", "Post", "Description", "string", true)
			       	};
		}

		#endregion
	}
}