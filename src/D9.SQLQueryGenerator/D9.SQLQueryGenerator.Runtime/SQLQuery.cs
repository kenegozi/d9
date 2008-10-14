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

using D9.SQLQueryGenerator.Runtime.Clauses;
using D9.SQLQueryGenerator.Runtime.Model.Field;
using D9.SQLQueryGenerator.Runtime.Queries;

namespace D9.SQLQueryGenerator.Runtime
{
	/// <summary>
	/// Build a SQL query
	/// </summary>
	public class SQLQuery
	{
		/// <summary>
		/// SELECT clause
		/// </summary>
		/// <param name="fields">SELECT columns</param>
		/// <returns><see cref="SQLSelectQuery"/></returns>
		public static SQLSelectQuery Select(params IFormatableField[] fields)
		{
			var selectClause = new SelectClause(fields);
			var q = new SQLSelectQuery(selectClause);
			return q;
		}

		/// <summary>
		/// A SQL string represented by the current <see cref="SQLQuery"/>
		/// </summary>
		/// <param name="q">The current <see cref="SQLQuery"/></param>
		/// <returns>SQL string</returns>
		public static implicit operator string(SQLQuery q)
		{
			return q.ToString();
		}
	}
}