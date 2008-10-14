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

using System;
using System.Text;

namespace D9.QuerySpecBuilder.Helpers
{
	/// <summary>
	/// A string builder that can maintain indentation
	/// </summary>
	public class IndentingStringBuilder
	{
		private readonly StringBuilder buf;
		private int indent;
		private readonly int initialIndentation;

		/// <summary>
		/// Creates a new IndentingStringBuilder with initial indentation of 0
		/// </summary>
		public IndentingStringBuilder()
			: this(0)
		{
		}

		/// <summary>
		/// Creates a new IndentingStringBuilder with initial indentation of <paramref name="initialIndentation"/>
		/// </summary>
		/// <param name="initialIndentation">The indentation of the whole string builder</param>
		public IndentingStringBuilder(int initialIndentation)
		{
			buf = new StringBuilder();
			this.initialIndentation = initialIndentation;
			indent = initialIndentation;
		}

		/// <summary>
		/// Adds a formatted string fluently
		/// </summary>
		/// <param name="format">The string format</param>
		/// <param name="args">The format arguments</param>
		/// <returns>self</returns>
		public IndentingStringBuilder Append(string format, params object[] args)
		{
			buf.Append(ApplyFormatOn(format, args));
			return this;
		}

		/// <summary>
		/// Adds a formatted string and a new line, fluently
		/// </summary>
		/// <param name="format">The string format</param>
		/// <param name="args">The format arguments</param>
		/// <returns>self</returns>
		public IndentingStringBuilder AppendLine(string format, params object[] args)
		{
			buf.AppendLine(ApplyFormatOn(format, args));
			return this;
		}

		private string ApplyFormatOn(string format, params object[] args)
		{
			var str = indent.Times('\t');
			var str2 = string.Format(format, args).TrimStart(new char[] { '\r', '\n' }).TrimEnd(new char[] { '\r', '\n' });
			var newValue = Environment.NewLine + str;
			var str4 = str2.Replace(Environment.NewLine, newValue);
			return (str + str4);
		}

		/// <summary>
		/// Increase indentation in amount 
		/// </summary>
		/// <param name="amount">Number of character</param>
		/// <returns>self</returns>
		public IndentingStringBuilder In(int amount)
		{
			indent += amount;
			return this;
		}

		/// <summary>
		/// Decrease indentation in amount 
		/// </summary>
		/// <param name="amount">Number of character</param>
		/// <returns>self</returns>
		public IndentingStringBuilder Out(int amount)
		{
			indent -= amount;
			if (indent < initialIndentation)
			{
				indent = initialIndentation;
			}
			return this;
		}

		public override string ToString()
		{
			return buf.ToString();
		}
	}
}

