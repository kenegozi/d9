#region License

// Copyright (c) 2008-2010 Ken Egozi (ken@kenegozi.com)
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
using D9.QuerySpecBuilder.Builder.Expressions;

namespace D9.QuerySpecBuilder.Builder.Clauses
{
	/// <summary>
	/// represents a WHERE clause
	/// </summary>
	public class WhereClause : AbstractClause
	{
		/// <summary>
		/// Creates a new WHERE clause for a given <paramref name="owningQuery"/>
		/// </summary>
		/// <param name="owningQuery">The query</param>
		public WhereClause(QueryBuilder owningQuery)
			: base(owningQuery)
		{
		}

		protected override string ClauseVerb
		{
			get { return "WHERE"; }
		}

		protected override string ExpressionsSeparator
		{
			get { return (Environment.NewLine + "and "); }
		}

		/// <summary>
		/// Adds a new expression to the WHERE clause
		/// </summary>
		/// <param name="expression">where expression</param>
		/// <returns>The current <see cref="WhereClause"/></returns>
		public WhereClause Add(string expression)
		{
			Expressions.Add(new Expression(expression));
			return this;
		}

		protected override string GetExpressionsString()
		{
			return ("\t" + base.GetExpressionsString());
		}

		public override string GetQueryString()
		{
			if (Expressions.Count == 0)
			{
				return string.Empty;
			}
			return base.GetQueryString();
		}
	}
}