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

using D9.StaticMapGenerator.GeneratedClassMetadata;
using D9.StaticMapGenerator.Services;
using NUnit.Framework;

namespace D9.StaticMapGenerator.Tests
{
	[TestFixture]
	public class DescriptorToClassServiceTests
	{
		[Test]
		public void Execute_WhenHasClassBasedMember_Works()
		{
			ClassDescriptor cls = new ClassDescriptor();
			cls.Name = "the_class";
			cls.Members.Add(new MemberDescriptor("the_type", "member_name"));

			IDescriptorToClassService service = new DescriptorToClassService();

			string classDef = service.Execute(cls);

			Assert.IsTrue(classDef.IndexOf("class the_class") > -1);
			Assert.IsTrue(classDef.IndexOf("the_type member_name = new the_type();") > -1);
		}

		[Test]
		public void Execute_WhenHasStringMember_Works()
		{
			ClassDescriptor cls = new ClassDescriptor();
			cls.Name = "the_class";
			cls.Members.Add(new MemberDescriptor("string", "member_name", "\"value\""));

			IDescriptorToClassService service = new DescriptorToClassService();

			string classDef = service.Execute(cls);

			Assert.IsTrue(classDef.IndexOf("class the_class") > -1);
			Assert.IsTrue(classDef.IndexOf("string member_name = \"value\";") > -1);
		}
	}
}