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

using System;
using System.Collections.Generic;
using D9.StaticMapGenerator.DirectoryStructure;
using D9.StaticMapGenerator.GeneratedClassMetadata;
using Microsoft.CSharp;

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// A component for generating <see cref="ClassDescriptor"/> instances
	/// out of a directory structure
	/// </summary>
	public class DirectoryStructureToClassDescriptorsService : IDirectoryStructureToClassDescriptorsService
	{
		public ClassDescriptor[] Execute(IDirInfo site)
		{
			List<ClassDescriptor> classDescriptors = new List<ClassDescriptor>();
			PutAllClassDescriptorFromIn(site, classDescriptors);
			return classDescriptors.ToArray();
		}

		static void PutAllClassDescriptorFromIn(IDirInfo dir, ICollection<ClassDescriptor> classDescriptors)
		{
			classDescriptors.Add(GetClassDescriptorFrom(dir));

			foreach (IDirInfo subDir in dir.Directories)
			{
				PutAllClassDescriptorFromIn(subDir, classDescriptors);
			}
		}

		static ClassDescriptor GetClassDescriptorFrom(IDirInfo dir)
		{
			ClassDescriptor classDescriptor = new ClassDescriptor();

			classDescriptor.Name = UrlToClassName(dir.Url);

			foreach (string file in dir.Files)
			{
				string fileName = Normalize(file);
				string url = dir.Url + "/" + file.ToLowerInvariant();
				classDescriptor.Members.Add(new MemberDescriptor(
				                            	"string", fileName, "\"" + url + "\""));
			}

			foreach (IDirInfo subDir in dir.Directories)
			{
				string name = subDir.Name;
				string name1 = name;
				if (dir.Files.Exists(delegate (string fileName) {
				                                                	return fileName == name1;
				}))
				{
					name = subDir.Name + "Directory";
				}

				classDescriptor.Members.Add(new MemberDescriptor(UrlToClassName(subDir.Url), Normalize(name)));
			}
			return classDescriptor;
		}

		static readonly CSharpCodeProvider cSharp = new CSharpCodeProvider();

		static string Normalize(string url)
		{
			string candidate = url
				.Replace(' ', '_')
				.Replace('/', '_')
				.Replace('-', '_')
				.Replace('.', '_');

			if (cSharp.IsValidIdentifier(candidate) == false)
				candidate = "_" + candidate;

			if (cSharp.IsValidIdentifier(candidate) == false)
				throw new Exception(string.Format("Could not make {0} into a valid csharp identifier", url));
			
			return candidate;
		}

		static string UrlToClassName(string url)
		{
			if (url == "")
				return "Root";

			return "Root" + Normalize(url);			
		}

	}
}