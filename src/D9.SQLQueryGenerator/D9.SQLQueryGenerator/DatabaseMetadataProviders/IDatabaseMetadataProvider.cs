using System.Collections.Generic;

namespace D9.SQLQueryGenerator.DatabaseMetadataProviders
{
	/// <summary>
	/// Extracts metadata from a DB
	/// </summary>
	public interface IDatabaseMetadataProvider
	{
		/// <summary>
		/// Extracts metadata from a DB
		/// </summary>
		/// <returns>Metadata</returns>
		IEnumerable<DbPropertyMetadata> ExtractMetadata();
	}
}