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

using System.Collections.Generic;
using System.IO;

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// Provide access to the file system
	/// </summary>
	public class FileSystemAdapter : IFileSystemAdapter
	{
		#region IFileSystemAdapter
		public virtual string[] GetScriptFiles(string dir)
		{
			return GetFiles(dir, "*.js");
		}

		public virtual string[] GetStyleFiles(string dir)
		{
			return GetFiles(dir, "*.css");
		}

		public virtual string[] GetImageFiles(string dir)
		{
			List<string> imageFiles = new List<string>();
			imageFiles.AddRange(GetFiles(dir, "*.jpg"));
			imageFiles.AddRange(GetFiles(dir, "*.gif"));
			imageFiles.AddRange(GetFiles(dir, "*.png"));
			return imageFiles.ToArray();
		}

		public virtual string[] GetDirectories(string dir)
		{
			return Directory.GetDirectories(dir);
		}

		public string GetDirectoryName(string dirPath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
			return directoryInfo.Name;
		}
		#endregion

		#region helpers
		private static string[] GetFileNamesFrom(IEnumerable<string> filePaths)
		{
			List<string> files = new List<string>();
			foreach (string filePath in filePaths)
			{
				files.Add(Path.GetFileName(filePath));
			}
			return files.ToArray();
		}


		private static string[] GetFiles(string dir, string pattern)
		{
			return GetFileNamesFrom(Directory.GetFiles(dir, pattern, SearchOption.TopDirectoryOnly));
		}
		#endregion


	}
}