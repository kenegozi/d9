namespace D9.SQLQueryGenerator.Runtime.Model.Field
{
	/// <summary>
	/// A field
	/// </summary>
	public interface IAliasedField : IField
	{
		/// <summary>
		/// Sets alias on the current <see cref="IField"/>
		/// </summary>
		/// <param name="alias">The new alias</param>
		/// <returns>The aliased field</returns>
		IField As(string alias);
	}
}