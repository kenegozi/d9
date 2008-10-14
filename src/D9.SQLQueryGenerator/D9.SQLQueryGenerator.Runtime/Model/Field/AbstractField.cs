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