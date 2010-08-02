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

using System;
using System.IO;
using D9.StaticMapGenerator.DirectoryStructure;

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// A component for building a directory structure representation out of a given root path
	/// </summary>
	public class BuildDirectoryStructureService : IBuildDirectoryStructureService
	{
		readonly IFileSystemAdapter fileSystem;
		string[] ignore = new string[0];

		/// <summary>
		/// Creates a new instance of BuildDirectoryStructureService which will use the
		/// given <see cref="IFileSystemAdapter"/>
		/// </summary>
		/// <param name="fileSystem">A <see cref="IFileSystemAdapter"/> for the component to use</param>
		public BuildDirectoryStructureService(IFileSystemAdapter fileSystem)
		{
			this.fileSystem = fileSystem;
		}

		public IDirInfo Execute(string rootPath)
		{
			return Process(rootPath, rootPath);
		}

		public IDirInfo Execute(string rootPath, string[] ignoreDirectories)
		{
			ignore = ignoreDirectories;
			return Execute(rootPath);
		}

		private IDirInfo Process(string rootPath, string dirPath)
		{
			string url = NormalizeUrl(rootPath, dirPath);
			IDirInfo dir = new ResourceDirInfo(url);

			foreach (string fileName in fileSystem.GetScriptFiles(dirPath))
			{
				dir.Files.Add(fileName);
			}
			foreach (string fileName in fileSystem.GetStyleFiles(dirPath))
			{
				dir.Files.Add(fileName);
			}
			foreach (string fileName in fileSystem.GetImageFiles(dirPath))
			{
				dir.Files.Add(fileName);
			}

			foreach (string subdirPath in fileSystem.GetDirectories(dirPath))
			{
				string directoryname = fileSystem.GetDirectoryName(subdirPath);
				if (Array.Exists(ignore, delegate(string ignored) { return ignored == directoryname; }))
					continue;
				IDirInfo subdir = Process(rootPath, subdirPath);
				dir.AddSubDirectory(subdir);
				if (subdir.Files.Count > 0)
					subdir.HasFiles = true;
			}
			return dir;
		}

		static string NormalizeUrl(string rootPath, string dirPath)
		{
			string url = dirPath
				.Replace(rootPath, "")
				.Replace(Path.DirectorySeparatorChar, '/')
				.Trim('/');

			if (url == string.Empty)
				return url;

			return "/" + url;
		}
	}
}