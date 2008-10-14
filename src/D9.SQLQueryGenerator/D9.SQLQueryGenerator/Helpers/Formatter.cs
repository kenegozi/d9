using System.Text.RegularExpressions;

namespace D9.SQLQueryGenerator.Helpers
{
	/// <summary>
	/// Formatting class names
	/// </summary>
	public static class Formatter
	{
		private static readonly Regex invalidChars = new Regex("[^\\w\\d_]");

		/// <summary>
		/// Format a valid class name
		/// </summary>
		/// <param name="name">input</param>
		/// <returns>Valid class name</returns>
		public static string FormatClassNameFrom(string name)
		{
			return invalidChars.Replace(name, "_");
		}
	}
}