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
using D9.StaticMapGenerator.GeneratedClassMetadata;
using D9.StaticMapGenerator.Services;
using NUnit.Framework;

namespace D9.StaticMapGenerator.Tests
{
	[TestFixture]
	public class DirectoryStructureToClassDescriptorsServiceTests
	{
		ResourceDirInfo site;
		ResourceDirInfo son1;
		ResourceDirInfo grandson;
		IDirectoryStructureToClassDescriptorsService service;

		[SetUp]
		public void SetUp()
		{
			site = new ResourceDirInfo("");
			son1 = new ResourceDirInfo("/son1");
			grandson = new ResourceDirInfo("/son1/grandson");

			site.Files.Add("script1.js");
			site.AddSubDirectory(son1);
			son1.Files.Add("script2.js");
			son1.Files.Add("style.css");
			son1.AddSubDirectory(grandson);
			grandson.Files.Add("image.jpg");
			grandson.Files.Add("image.png");
			grandson.Files.Add("image.gif");

			grandson.HasFiles = true;


			service = new DirectoryStructureToClassDescriptorsService();
		}

		[Test]
		public void Execute_Always_CreatesClasses()
		{
			ClassDescriptor[] classDefs = service.Execute(site);

			Assert.AreEqual("Root", classDefs[0].Name);
			Assert.AreEqual("Root_son1", classDefs[1].Name);
			Assert.AreEqual("Root_son1_grandson", classDefs[2].Name);
		}

		[Test]
		public void Execute_WhenHasFiles_CreatesClassesWithFileMembers()
		{
			ClassDescriptor[] classDefs = service.Execute(site);

			Assert.AreEqual(classDefs[0].Members[0].Value, "\"/script1.js\"");
			Assert.AreEqual(classDefs[0].Members[0].Type, "string");
			Assert.AreEqual(classDefs[0].Members[0].Name, "script1_js");

			Assert.AreEqual(classDefs[1].Members[0].Value, "\"/son1/script2.js\"");
			Assert.AreEqual(classDefs[1].Members[0].Type, "string");
			Assert.AreEqual(classDefs[1].Members[0].Name, "script2_js");

			Assert.AreEqual(classDefs[1].Members[1].Value, "\"/son1/style.css\"");
			Assert.AreEqual(classDefs[1].Members[1].Type, "string");
			Assert.AreEqual(classDefs[1].Members[1].Name, "style_css");

			Assert.AreEqual(classDefs[2].Members[0].Value, "\"/son1/grandson/image.jpg\"");
			Assert.AreEqual(classDefs[2].Members[0].Type, "string");
			Assert.AreEqual(classDefs[2].Members[0].Name, "image_jpg");

			Assert.AreEqual(classDefs[2].Members[1].Value, "\"/son1/grandson/image.png\"");
			Assert.AreEqual(classDefs[2].Members[1].Type, "string");
			Assert.AreEqual(classDefs[2].Members[1].Name, "image_png");

			Assert.AreEqual(classDefs[2].Members[2].Value, "\"/son1/grandson/image.gif\"");
			Assert.AreEqual(classDefs[2].Members[2].Type, "string");
			Assert.AreEqual(classDefs[2].Members[2].Name, "image_gif");

		}
	}
}