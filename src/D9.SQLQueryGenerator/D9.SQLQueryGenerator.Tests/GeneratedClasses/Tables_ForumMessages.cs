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

namespace D9.SQLQueryGenerator.Tests.GeneratedClasses
{
	public class Tables_ForumMessages : Runtime.Model.Table.AbstractTable
	{
		public Tables_ForumMessages(string alias) : base("dbo", "ForumMessages", alias)
		{
			Id = new Tables_ForumMessages_Id(this);
			ParentId = new Tables_ForumMessages_ParentId(this);
			Content = new Tables_ForumMessages_Content(this);
		}

		public Tables_ForumMessages() : this(null)
		{
		}

		public readonly Tables_ForumMessages_Id Id;
		public readonly Tables_ForumMessages_ParentId ParentId;
		public readonly Tables_ForumMessages_Content Content;

		public Tables_ForumMessages As(string alias)
		{
			return new Tables_ForumMessages(alias);
		}
	}
}
