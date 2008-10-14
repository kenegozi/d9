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

namespace D9.SQLQueryGenerator.Runtime.Clauses
{
	public class FromClause : AbstractSqlClause
	{
		readonly Model.Table.AbstractTable table;
		readonly System.Collections.Generic.List<Expressions.JoinExpression> joins = new System.Collections.Generic.List<Expressions.JoinExpression>();
		public FromClause(Model.Table.AbstractTable table)
		{
			this.table = table;
		}

		public FromClause Join(Expressions.JoinExpression join)
		{
			joins.Add(join);
			return this;
		}

		public FromClause Join(Model.Table.AbstractTable table, Expressions.WhereExpression on)
		{
			joins.Add(new Expressions.JoinExpression(table, on));
			return this;
		}

		public override string ToString()
		{
			System.Text.StringBuilder from = new System.Text.StringBuilder()
				.AppendLine("\t\t\t\t" + Format.Formatting.FormatForFromClause(table));

			foreach (Expressions.JoinExpression join in joins)
			{
				from.AppendLine(join.ToString());
			}

			from
				.Insert(0, "FROM" + System.Environment.NewLine);

			return from.ToString();
		}	
	}
}
