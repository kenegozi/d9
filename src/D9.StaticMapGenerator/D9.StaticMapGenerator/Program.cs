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
using System.IO;
using D9.StaticMapGenerator.DirectoryStructure;
using D9.StaticMapGenerator.GeneratedClassMetadata;
using D9.StaticMapGenerator.Services;

namespace D9.StaticMapGenerator
{
	class Program
	{
		const string GENERATED_NAMESPACE = "StaticSiteMap";
		const string GENERATED_FILENAME = "Static.Site.Generated.cs";

		static void RegisterComponents()
		{
			IoC.Register<IFileSystemAdapter, FileSystemAdapter>();
			IoC.Register<IBuildDirectoryStructureService, BuildDirectoryStructureService>();
			IoC.Register<IDirectoryStructureToClassDescriptorsService, DirectoryStructureToClassDescriptorsService>();
			IoC.Register<IDescriptorToClassService, DescriptorToClassService>();
			IoC.Register<IDescriptorsToClassesService, DescriptorsToClassesService>();
		}

		static void Main(string[] args)
		{
			RegisterComponents();

			string generatedNamespace = GENERATED_NAMESPACE;

			string[] ignore = new string[] { ".svn", "obj", "bin" };
			string dirPath = null;

			foreach (string arg in args)
			{
				string[] parts = arg.Split(':');
				switch (parts[0])
				{
					case "/site":
						dirPath = string.Join(":", parts, 1, parts.Length - 1)
							.Trim('\"');
						break;

					case "/ignore":
						ignore = parts[1].Split('|');
						break;

					case "/namespace":
						generatedNamespace = parts[1];
						break;
				}
			
			}

			if (dirPath == null)
			{
				DirectoryInfo currentDir = new DirectoryInfo(Environment.CurrentDirectory);
				if (currentDir.Name.Equals("bin", StringComparison.InvariantCultureIgnoreCase))
					currentDir = currentDir.Parent;

				if (currentDir == null)
					throw new Exception("Cannot find root directory");
				dirPath = currentDir.FullName;
			}

			string generatedFilePath = Path.Combine(dirPath, GENERATED_FILENAME);

			
			IBuildDirectoryStructureService buildDirectoryStructureService = IoC.Resolve<IBuildDirectoryStructureService>();
			
			IDirInfo site = buildDirectoryStructureService.Execute(dirPath, ignore);

			site.RemoveEmptyDirectories();
			
			IDirectoryStructureToClassDescriptorsService directoryStructureToClassDescriptorsService = IoC.Resolve<IDirectoryStructureToClassDescriptorsService>();

			ClassDescriptor[] classDescriptors = directoryStructureToClassDescriptorsService.Execute(site);

			IDescriptorsToClassesService descriptorsToClassesService = IoC.Resolve<IDescriptorsToClassesService>();

			string[] classes = descriptorsToClassesService.Execute(classDescriptors);

			string generatedClasses = string.Join(Environment.NewLine, classes);

			string generatedFile = string.Format(@"
namespace {0}
{{
	public static class Static
	{{
		public static Root Site = new Root();
	}}
{1}
}}
", generatedNamespace, generatedClasses);
				
			File.WriteAllText(generatedFilePath, generatedFile);

		}
	}
}