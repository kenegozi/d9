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

using System.ComponentModel;
using D9.SQLQueryGenerator.Runtime.Expressions;
using D9.SQLQueryGenerator.Runtime.Format;
using D9.SQLQueryGenerator.Runtime.Model.Table;

namespace D9.SQLQueryGenerator.Runtime.Model.Field
{
	/// <summary>
	/// Base implementation for fields
	/// </summary>
	public abstract class AbstractField : IAliasedField
	{
		private readonly string alias;
		private readonly string name;
		private readonly AbstractTable table;

		///	<summary>
		///	New aliased field
		///	</summary>
		///	<param name="table">Table</param>
		///	<param name="name">Name</param>
		///	<param name="alias">Alias</param>
		protected AbstractField(AbstractTable table, string name, string alias) : this(table, name)
		{
			this.alias = alias;
		}

		/// <summary>
		///	New field
		///	</summary>
		///	<param name="table">Table</param>
		///	<param name="name">Name</param>
		/// <param name="name"></param>
		protected AbstractField(AbstractTable table, string name)
		{
			this.table = table;
			this.name = name;
		}

		#region IAliasedField Members

		AbstractTable IFormatableField.Table
		{
			get { return table; }
		}

		string IFormatableField.Name
		{
			get { return name; }
		}

		string IFormatableField.Alias
		{
			get { return alias; }
		}

		public abstract IField As(string alias);

		#endregion

		public override string ToString()
		{
			return Formatting.Format(this);
		}
	}

	/// <summary>
	/// Abstract field of type T
	/// </summary>
	/// <typeparam name="T">The field's type</typeparam>
	public class AbstractField<T> : AbstractField, IOperateable<T>
	{
		/// <summary>
		///	New aliased field
		///	</summary>
		///	<param name="table">Table</param>
		///	<param name="name">Name</param>
		///	<param name="alias">Alias</param>
		public AbstractField(AbstractTable table, string name, string alias)
			: base(table, name, alias)
		{
		}

		/// <summary>
		///	New field
		///	</summary>
		///	<param name="table">Table</param>
		///	<param name="name">Name</param>
		public AbstractField(AbstractTable table, string name)
			: base(table, name)
		{
		}

		/// <summary>
		/// field == other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator ==(AbstractField<T> field, T other)
		{
			return new WhereExpression(field, " = ", other);
		}

		/// <summary>
		/// field == other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator ==(AbstractField<T> field, IOperateable<T> other)
		{
			return new WhereExpression(field, " = ", other);
		}

		/// <summary>
		/// field != other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator !=(AbstractField<T> field, T other)
		{
			return new WhereExpression(field, " <> ", other);
		}

		/// <summary>
		/// field != other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator !=(AbstractField<T> field, IOperateable<T> other)
		{
			return new WhereExpression(field, " <> ", other);
		}

		/// <summary>
		/// field <c>&gt;</c> other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator >(AbstractField<T> field, T other)
		{
			return new WhereExpression(field, " > ", other);
		}

		/// <summary>
		/// field <c>&gt;</c> other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator >(AbstractField<T> field, IOperateable<T> other)
		{
			return new WhereExpression(field, " > ", other);
		}

		/// <summary>
		/// field <c>&lt;</c> other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator <(AbstractField<T> field, T other)
		{
			return new WhereExpression(field, " < ", other);
		}

		/// <summary>
		/// field <c>&lt;</c> other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator <(AbstractField<T> field, IOperateable<T> other)
		{
			return new WhereExpression(field, " < ", other);
		}

		/// <summary>
		/// field <c>&gt;</c>= other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator >=(AbstractField<T> field, T other)
		{
			return new WhereExpression(field, " >= ", other);
		}

		/// <summary>
		/// field <c>&gt;</c>= other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator >=(AbstractField<T> field, IOperateable<T> other)
		{
			return new WhereExpression(field, " >= ", other);
		}

		/// <summary>
		/// field <c>&lt;</c>= other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator <=(AbstractField<T> field, T other)
		{
			return new WhereExpression(field, " <= ", other);
		}

		/// <summary>
		/// field <c>&lt;</c>= other
		/// </summary>
		/// <param name="field">field</param>
		/// <param name="other">other</paam>
		/// <returns>self</returns>
		public static WhereExpression operator <=(AbstractField<T> field, IOperateable<T> other)
		{
			return new WhereExpression(field, " <= ", other);
		}

		public override IField As(string alias)
		{
			IFormatableField thisField = this;
			return new AbstractField<T>(thisField.Table, thisField.Name, alias);
		}

		[Browsable(false)]
		[Localizable(false)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		[Browsable(false)]
		[Localizable(false)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}