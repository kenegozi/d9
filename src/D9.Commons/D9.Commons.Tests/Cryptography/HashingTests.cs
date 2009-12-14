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

using D9.Commons.Cryptography;
using NUnit.Framework;
using NUnit.Framework.ExtensionMethods;

// ReSharper disable AccessToStaticMemberViaDerivedType
namespace D9.Commons.Tests.Cryptography
{
	[TestFixture]
	public class HashingTests
	{
		[Test]
		public void DefaultHashing_Is_SHA256()
		{
			var data = "THE_DATA_TO_HASH";
			var usingSha256 = Hashing.HashData(data, HashType.SHA256);
			var usingDefault = Hashing.HashData(data);

			usingDefault.Should(Be.EqualTo(usingSha256));
		}

		[Test]
		public void Hashing_WithSHA256_Works()
		{
			var data = "THE_DATA_TO_HASH";
			var expected = "a63d4e91ab97d567d5230d208bd875bd6757cb38823e9ef66f0012f50b9bdf90";
			var usingSha256 = Hashing.HashData(data, HashType.SHA256);

			usingSha256.Should(Be.EqualTo(expected));
		}

		[Test]
		public void DefaultHashing_WithBinaryData_IsSHA256()
		{
			var data = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 };
			var usingSha256 = Hashing.HashData(data, HashType.SHA256);
			var usingDefault = Hashing.HashData(data);

			usingDefault.Should(Be.EqualTo(usingSha256));
		}
		
		[Test]
		public void HashingBinary_WithSHA256_Works()
		{
			var data = new byte[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 0, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19};
			var expected = new byte[]
			{
				218, 46, 78, 211, 85, 138, 14, 226, 52, 249, 239, 130, 180, 17, 204, 24, 229, 160, 213, 246, 163, 207, 
				82, 214, 244, 237, 111, 51, 243, 102, 2, 115
			};
			var usingSha256 = Hashing.HashData(data, HashType.SHA256);

			usingSha256.Should(Be.EqualTo(expected));
		}
	}
}