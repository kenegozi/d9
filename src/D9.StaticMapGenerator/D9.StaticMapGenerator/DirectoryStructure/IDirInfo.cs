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

using System.Collections.Generic;

namespace D9.StaticMapGenerator.DirectoryStructure
{
	/// <summary>
	/// Represents a directory with possible static web resources
	/// </summary>
	public interface IDirInfo
	{
		/// <summary>
		/// The <c>url</c> for the current directory 
		/// </summary>
		string Url { get; }

		/// <summary>
		/// Directory name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The parent directory
		/// </summary>
		IDirInfo Parent { get; set; }

		/// <summary>
		/// True if has resources. 
		/// <remarks>Should this value be true for this directory, it should also be true to all parent directories</remarks>
		/// </summary>
		bool HasFiles { get; set; }

		/// <summary>
		/// Sub directories
		/// </summary>
		IDirInfo[] Directories { get; }

		/// <summary>
		/// Filenames within current directories
		/// </summary>
		List<string> Files { get; }

		/// <summary>
		/// Adds a subdirectory under the current one
		/// </summary>
		/// <param name="subDirectory"></param>
		void AddSubDirectory(IDirInfo subDirectory);

		/// <summary>
		/// Remove directories without any resources
		/// </summary>
		void RemoveEmptyDirectories();
	}
}