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

