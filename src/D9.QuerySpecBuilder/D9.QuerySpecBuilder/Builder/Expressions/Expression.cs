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

namespace D9.QuerySpecBuilder.Builder.Expressions
{
	/// <summary>
	/// Represents a <c>sql</c> expression
	/// </summary>
	public class Expression : IExpression
	{
		private readonly object expression;
		private bool parenthesise;

		/// <summary>
		/// Creates a new expression for a given query
		/// </summary>
		/// <param name="query">the query</param>
		public Expression(QueryBuilder query)
		{
			expression = query;
		}

		/// <summary>
		/// Creates a new expression for a given SQL literal
		/// </summary>
		/// <param name="literal">Raw SQL</param>
		public Expression(string literal)
		{
			expression = literal;
		}

		internal string Alias { get; private set; }

		#region IExpression Members

		public virtual string GetQueryString()
		{
			string expressionString = GetExpressionString();
			if (parenthesise)
			{
				expressionString = "(" + expressionString + ")";
			}
			if (Alias == null)
			{
				return expressionString;
			}
			return (expressionString + " AS " + Alias);
		}

		#endregion

		/// <summary>
		/// Sets alias on the current expression
		/// </summary>
		/// <param name="alias">The alias</param>
		/// <returns>The current Expression</returns>
		public Expression As(string alias)
		{
			Alias = alias;
			return this;
		}

		private string GetExpressionString()
		{
			if (expression is IExpression)
			{
				return ((IExpression) expression).GetQueryString();
			}
			if (expression is QueryBuilder)
			{
				return ((QueryBuilder) expression).GetQueryString();
			}
			return expression.ToString();
		}

		/// <summary>
		/// Sets the current Expression as Parenthesised
		/// </summary>
		/// <returns>the current Expression</returns>
		public Expression Parenthesise()
		{
			parenthesise = true;
			return this;
		}

		public override string ToString()
		{
			return GetQueryString();
		}
	}
}