using System;
using System.Collections.Generic;
using System.Linq;
using D9.QuerySpecBuilder.Builder.Expressions;
using D9.QuerySpecBuilder.Helpers;

namespace D9.QuerySpecBuilder.Builder.Clauses
{
	/// <summary>
	/// Base class for SQL clauses
	/// </summary>
	public abstract class AbstractClause
	{
		private readonly List<IExpression> expressions = new List<IExpression>();
		/// <summary>
		/// The query to which the current clause belongs to
		/// </summary>
		protected readonly QueryBuilder owningQuery;

		/// <summary>
		/// Creates a new clause for a given <paramref name="owningQuery"/>
		/// </summary>
		/// <param name="owningQuery">The query</param>
		protected AbstractClause(QueryBuilder owningQuery)
		{
			this.owningQuery = owningQuery;
		}

		/// <summary>
		/// When implemented in a derived class, gets the clause's SQL name
		/// </summary>
		protected abstract string ClauseVerb { get; }

		/// <summary>
		/// Back to the query
		/// </summary>
		public QueryBuilder End
		{
			get { return owningQuery; }
		}

		/// <summary>
		/// The expression in the current clause
		/// </summary>
		protected virtual List<IExpression> Expressions
		{
			get { return expressions; }
		}

		/// <summary>
		/// What separates between expressions in this type of clause
		/// </summary>
		protected virtual string ExpressionsSeparator
		{
			get { return ("," + Environment.NewLine); }
		}

		/// <summary>
		/// Builds the SQL fragment representing this clause's expressions
		/// </summary>
		/// <returns>SQL fragment</returns>
		protected virtual string GetExpressionsString()
		{
			return string.Join(ExpressionsSeparator,
							   Expressions.Select(delegate(IExpression e) { return e.GetQueryString(); }).ToArray());
		}

		/// <summary>
		/// Builds the SQL fragment representing this clause
		/// </summary>
		/// <returns>SQL fragment</returns>
		public virtual string GetQueryString()
		{
			var builder = new IndentingStringBuilder();
			builder.AppendLine(ClauseVerb, new object[0]);
			builder.In(1);
			builder.Append(GetExpressionsString(), new object[0]);
			builder.Out(1);
			return builder.ToString();
		}
	}
}