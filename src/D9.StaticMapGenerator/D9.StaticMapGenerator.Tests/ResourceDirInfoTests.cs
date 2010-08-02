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

using D9.StaticMapGenerator.DirectoryStructure;
using NUnit.Framework;

namespace D9.StaticMapGenerator.Tests
{
	[TestFixture]
	public class ResourceDirInfoTests
	{
		ResourceDirInfo site;
		ResourceDirInfo son1;
		ResourceDirInfo son2;
		ResourceDirInfo grandson;

		[SetUp]
		public void SetUp()
		{
			site = new ResourceDirInfo("/");
			son1 = new ResourceDirInfo("/son1/");
			son2 = new ResourceDirInfo("/son2/");
			grandson = new ResourceDirInfo("/son1/grandson/");

			site.AddSubDirectory(son1);
			site.AddSubDirectory(son2);
			son1.AddSubDirectory(grandson);
		}

		[Test]
		public void RemoveEmptyDirectories_WhenAllHasFiles_DoesNothing()
		{
			site.HasFiles = true;
			son1.HasFiles = true;
			son2.HasFiles = true;
			grandson.HasFiles = true;

			site.RemoveEmptyDirectories();

			Assert.AreEqual(2, site.Directories.Length);
			Assert.AreEqual(1, son1.Directories.Length);
		}

		[Test]
		public void RemoveEmptyDirectories_WhenHasEmptyLeafs_RemovesLeafs()
		{
			site.HasFiles = true;
			son1.HasFiles = true;
			son2.HasFiles = false;
			grandson.HasFiles = false;

			site.RemoveEmptyDirectories();

			Assert.AreEqual(1, site.Directories.Length);
			Assert.AreEqual(son1, site.Directories[0]);
			Assert.AreEqual(0, son1.Directories.Length);
		}

		[Test]
		public void RemoveEmptyDirectories_WhenHasEmptyNodes_RemovesNodes()
		{
			site.HasFiles = true;
			son1.HasFiles = false;
			son2.HasFiles = true;
			grandson.HasFiles = false;

			site.RemoveEmptyDirectories();

			Assert.AreEqual(1, site.Directories.Length);
			Assert.AreEqual(son2, site.Directories[0]);
			Assert.AreEqual(0, son2.Directories.Length);
		}

		[Test]
		public void SetTrue_Always_SetAncestors()
		{
			grandson.HasFiles = true;

			Assert.IsTrue(grandson.HasFiles);
			Assert.IsTrue(son1.HasFiles);
			Assert.IsTrue(site.HasFiles);
			Assert.IsFalse(son2.HasFiles);
		}
	}
}