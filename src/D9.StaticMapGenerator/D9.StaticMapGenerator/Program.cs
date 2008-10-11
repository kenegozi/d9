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