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
using D9.StaticMapGenerator.Services;

namespace D9.StaticMapGenerator.Tests
{
	/// <summary>
	/// Fake <see cref="IFileSystemAdapter"/> for tests
	/// </summary>
	public class FakeFileSystemAdapter : FileSystemAdapter
	{
		readonly Dictionary<string, string[]> directories = new Dictionary<string, string[]>();
		readonly Dictionary<string, string[]> scriptFiles = new Dictionary<string, string[]>();
		readonly Dictionary<string, string[]> styleFiles = new Dictionary<string, string[]>();
		readonly Dictionary<string, string[]> imageFiles = new Dictionary<string, string[]>();

		/// <summary>
		/// Creates a new FakeFileSystemAdapter
		/// </summary>
		public FakeFileSystemAdapter()
		{
			directories.Add(@"C:\", new string[] { @"C:\grandpa", @"C:\grandma" });
			directories.Add(@"C:\grandma", new string[] { @"C:\grandma\mom", @"C:\grandma\dad" });
			directories.Add(@"C:\grandma\dad", new string[] { @"C:\grandma\dad\Joe" });

			scriptFiles.Add(@"C:\grandpa", new string[] { @"script1.js" });
			scriptFiles.Add(@"C:\grandma\dad", new string[] { @"script2.js" });

			styleFiles.Add(@"C:\grandpa", new string[] { @"style1.css" });

			imageFiles.Add(@"C:\grandpa", new string[] { @"image1.jpg" });
			imageFiles.Add(@"C:\grandma\mom", new string[] { @"image2.gif" });
		}

		public override string[] GetScriptFiles(string dir)
		{
			if (scriptFiles.ContainsKey(dir))
				return scriptFiles[dir];
			return new string[0];
		}
		public override string[] GetStyleFiles(string dir)
		{
			if (styleFiles.ContainsKey(dir))
				return styleFiles[dir];
			return new string[0];
		}
		public override string[] GetImageFiles(string dir)
		{
			if (imageFiles.ContainsKey(dir))
				return imageFiles[dir];
			return new string[0];
		}
		public override string[] GetDirectories(string dir)
		{
			if (directories.ContainsKey(dir))
				return directories[dir];
			return new string[0];
		}
	}
}