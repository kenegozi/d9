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

namespace D9.StaticMapGenerator.GeneratedClassMetadata
{
	/// <summary>
	/// Describes a generated member
	/// </summary>
	public class MemberDescriptor
	{
		/// <summary>
		/// Creates a new instance of MemberDescriptor, for a member with an initial value
		/// </summary>
		/// <param name="type">The member's type</param>
		/// <param name="name">The member's name</param>
		/// <param name="value">The member's initial value</param>
		public MemberDescriptor(string type, string name, string value)
		{
			Type = type;
			Name = name;
			Value = value;
		}

		/// <summary>
		/// Creates a new instance of MemberDescriptor, for a member without an initial value
		/// </summary>
		/// <param name="type">The member's type</param>
		/// <param name="name">The member's name</param>
		public MemberDescriptor(string type, string name)
			: this(type, name, "new " + type + "()")
		{
		}
		 
		///	<summary>
		///	The member's type name
		///	</summary>
		public string Type { get; private set; }

		///	<summary>
		///	The member's name
		///	</summary>
		public string Name { get; private set; }

		///	<summary>
		///	The member's initial value
		///	</summary>
		public string Value { get; private set; }
	}
}