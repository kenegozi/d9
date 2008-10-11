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
				.Replace('/', '_')
				.Replace('-', '_')
				.Replace('.', '_');

			while (cSharp.IsValidIdentifier(candidate) == false)
				candidate = "_" + candidate;

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