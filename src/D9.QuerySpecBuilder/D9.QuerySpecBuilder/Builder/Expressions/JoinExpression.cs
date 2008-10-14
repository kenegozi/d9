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

namespace D9.QuerySpecBuilder.Builder.Expressions
{
	/// <summary>
	/// A SQL JOIN expression
	/// </summary>
	public class JoinExpression : IExpression
	{
		private IExpression alias;
		private JoinType joinType = JoinType.Inner;
		private IExpression on;
		private IExpression table;

		#region IExpression Members

		/// <summary>
		/// Extract the SQL string represented by the current expression
		/// </summary>
		/// <returns>SQL String</returns>
		public virtual string GetQueryString()
		{
			string str = (alias == null) ? table.GetQueryString() : (table + " AS " + alias);
			return string.Format("{0}{1} ON ({2})", joinType, str, on);
		}

		#endregion

		/// <summary>
		/// Sets alias on the joined table
		/// </summary>
		/// <param name="expression">alias</param>
		/// <returns>the current <see cref="JoinExpression"/></returns>
		public JoinExpression As(string expression)
		{
			alias = new Expression(expression);
			return this;
		}

		/// <summary>
		/// Sets current join as OUTER LEFT JOIN
		/// </summary>
		/// <returns>the current <see cref="JoinExpression"/></returns>
		public JoinExpression Left()
		{
			joinType = JoinType.Left;
			return this;
		}

		/// <summary>
		/// Sets the ON part of the current JOIN
		/// </summary>
		/// <param name="expression">Join condition</param>
		/// <returns>the current <see cref="JoinExpression"/></returns>
		public JoinExpression On(string expression)
		{
			on = new Expression(expression);
			return this;
		}

		/// <summary>
		/// Sets current join as OUTER RIGHT JOIN
		/// </summary>
		/// <returns>the current <see cref="JoinExpression"/></returns>
		public JoinExpression Right()
		{
			joinType = JoinType.Right;
			return this;
		}

		/// <summary>
		/// Sets the joined table
		/// </summary>
		/// <param name="expression">The joined table</param>
		/// <returns>the current <see cref="JoinExpression"/></returns>
		public JoinExpression Table(string expression)
		{
			table = new Expression(expression);
			return this;
		}

		public override string ToString()
		{
			return GetQueryString();
		}
	}
}