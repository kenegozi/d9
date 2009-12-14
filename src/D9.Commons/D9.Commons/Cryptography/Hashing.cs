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
using System.Security.Cryptography;
using System.Text;

namespace D9.Commons.Cryptography
{
	/// <summary>
	/// Helping with data hashing
	/// </summary>
	public static class Hashing
	{
		private static readonly IDictionary<HashType, HashAlgorithm> hashAlgorithms = new Dictionary<HashType, HashAlgorithm>
      	{
      		{ HashType.MD5, new MD5CryptoServiceProvider() },
      		{ HashType.SHA1, new SHA1Managed() },
      		{ HashType.SHA256, new SHA256Managed() },
      		{ HashType.SHA384, new SHA384Managed() },
      		{ HashType.SHA512, new SHA512Managed() }
      	};

		/// <summary>
		/// Hashing a given string using SHA2.
		/// </summary>
		/// <param name="data">Data to hash</param>
		/// <returns>Hashed data</returns>
		public static string HashData(string data)
		{
			return HashData(data, HashType.SHA256);
		}

		/// <summary>
		/// Hashing a given string with any of the supported hash algorithms.
		/// </summary>
		/// <param name="data">Data to hash</param>
		/// <param name="hashType">Hashing algorithm to use</param>
		/// <returns>Hashed data</returns>
		public static string HashData(string data, HashType hashType)
		{
			var bytes = (new UnicodeEncoding()).GetBytes(data);
			var hashed = HashData(bytes, hashType);
			var sb = new StringBuilder(64);
			foreach (var b in hashed)
				sb.AppendFormat("{0:x2}", b);
			return sb.ToString();
		}

		/// <summary>
		/// Hashing a given binary data using SHA2.
		/// </summary>
		/// <param name="data">Data to hash</param>
		/// <returns>Hashed data</returns>
		public static byte[] HashData(byte[] data)
		{
			return HashData(data, HashType.SHA256);
		}

		/// <summary>
		/// Hashing a given binary data with any of the supported hash algorithms.
		/// </summary>
		/// <param name="data">Data to hash</param>
		/// <param name="hashType">Hashing algorithm to use</param>
		/// <returns>Hashed data</returns>
		public static byte[] HashData(byte[] data, HashType hashType)
		{
			var hash = GetHash(hashType);
			return hash.ComputeHash(data);
		}

		private static HashAlgorithm GetHash(HashType hashType)
		{
			return hashAlgorithms[hashType];
		}
	}
}