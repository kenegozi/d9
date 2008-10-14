using System;
using System.Collections.Generic;

namespace D9.SQLQueryGenerator.Helpers
{
	/// <summary>
	/// Compare string in a case insensitive manner
	/// </summary>
	public class CaseInsensitiveStringComparer : IEqualityComparer<string>
	{
		#region IEqualityComparer<string> Members

		public bool Equals(string x, string y)
		{
			return x.Equals(y, StringComparison.InvariantCultureIgnoreCase);
		}

		public int GetHashCode(string obj)
		{
			return obj.ToLowerInvariant().GetHashCode();
		}

		#endregion
	}
}