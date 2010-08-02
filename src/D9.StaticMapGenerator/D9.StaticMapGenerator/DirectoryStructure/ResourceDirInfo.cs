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
	public class ResourceDirInfo : IDirInfo
	{
		readonly string url;
		string name;
		IDirInfo parent;
		readonly List<IDirInfo> directories = new List<IDirInfo>();
		readonly List<string> files = new List<string>();
		bool hasFiles;

		/// <summary>
		/// Creates a new ResourceDirInfo instance 
		/// </summary>
		/// <param name="url">The base <paramref name="url"/></param>
		public ResourceDirInfo(string url)
		{
			this.url = url;
		}

		/// <summary>
		/// Remove directories without any resources
		/// </summary>
		public void RemoveEmptyDirectories()
		{
			directories.RemoveAll(EmptyDirectories);

			foreach (IDirInfo subDir in directories)
			{
				subDir.RemoveEmptyDirectories();
			}
		}

		private static bool EmptyDirectories(IDirInfo dir)
		{
			return !dir.HasFiles;
		}

		/// <summary>
		/// The <c>url</c>
		/// </summary>
		public string Url { get { return url; } }

		/// <summary>
		/// Directory name
		/// </summary>
		public string Name
		{
			get
			{
				if (name == null)
				{
					string[] parts = url.Split('/');
					name = parts[parts.Length - 1];
				}
				return name;
			}
		}

		/// <summary>
		/// Sub directories
		/// </summary>
		public IDirInfo[] Directories { get { return directories.ToArray(); } }

		/// <summary>
		/// Adds a subdirectory under the current one
		/// </summary>
		/// <param name="subDirectory"></param>
		public void AddSubDirectory(IDirInfo subDirectory)
		{
			directories.Add(subDirectory);
			subDirectory.Parent = this;
		}

		/// <summary>
		/// Filenames within current directories
		/// </summary>
		public List<string> Files { get { return files; } }

		/// <summary>
		/// True if has resources. 
		/// <remarks>Setting this to true will set all parents in the directory tree to true also</remarks>
		/// </summary>
		public bool HasFiles
		{
			get { return hasFiles; }
			set
			{
				hasFiles = value;
				if (hasFiles && parent != null && !parent.HasFiles)
					parent.HasFiles = true;
			}
		}

		/// <summary>
		/// The parent directory
		/// </summary>
		public IDirInfo Parent
		{
			get { return parent; }
			set { parent = value; }
		}

	}
}