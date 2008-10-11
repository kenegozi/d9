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

using D9.StaticMapGenerator.DirectoryStructure;
using D9.StaticMapGenerator.Services;
using NUnit.Framework;

namespace D9.StaticMapGenerator.Tests
{
	[TestFixture]
	public class BuildDirectoryStructureServiceTests
	{
		[Test]
		public void Execute_Always_Works()
		{
			IFileSystemAdapter fileSystem = new FakeFileSystemAdapter();
			IBuildDirectoryStructureService service = new BuildDirectoryStructureService(fileSystem);

			IDirInfo site = service.Execute("C:\\");

			Assert.AreEqual(2, site.Directories.Length);

			Assert.AreEqual("grandpa", site.Directories[0].Name);

			Assert.AreEqual("grandma", site.Directories[1].Name);

			string[] files = new string[]
			                 	{
			                 		@"script1.js" ,
			                 		@"script2.js" ,
			                 		@"style1.css",
			                 		@"image1.jpg" ,
			                 		@"image2.gif" 
			                 	};

			foreach (string file in files)
			{
				Assert.IsTrue(IsDirContainsFile(site, file), "File [{0}] is missed", file);
			}

			Assert.IsFalse(IsDirContainsFile(site, "Boggey"));


		}

		private static bool IsDirContainsFile(IDirInfo dir, string fileName)
		{
			if (dir.Files.Exists(delegate(string file)
									{
										return file == fileName;
									}))
				return true;

			foreach (IDirInfo sub in dir.Directories)
			{
				if (IsDirContainsFile(sub, fileName))
					return true;
			}
			return false;
		}
	}
}