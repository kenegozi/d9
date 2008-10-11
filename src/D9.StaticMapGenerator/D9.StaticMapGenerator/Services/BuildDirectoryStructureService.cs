using System;
using System.IO;
using D9.StaticMapGenerator.DirectoryStructure;

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// A component for building a directory structure representation out of a given root path
	/// </summary>
	public class BuildDirectoryStructureService : IBuildDirectoryStructureService
	{
		readonly IFileSystemAdapter fileSystem;
		string[] ignore = new string[0];

		/// <summary>
		/// Creates a new instance of BuildDirectoryStructureService which will use the
		/// given <see cref="IFileSystemAdapter"/>
		/// </summary>
		/// <param name="fileSystem">A <see cref="IFileSystemAdapter"/> for the component to use</param>
		public BuildDirectoryStructureService(IFileSystemAdapter fileSystem)
		{
			this.fileSystem = fileSystem;
		}

		public IDirInfo Execute(string rootPath)
		{
			return Process(rootPath, rootPath);
		}

		public IDirInfo Execute(string rootPath, string[] ignoreDirectories)
		{
			ignore = ignoreDirectories;
			return Execute(rootPath);
		}

		private IDirInfo Process(string rootPath, string dirPath)
		{
			string url = NormalizeUrl(rootPath, dirPath);
			IDirInfo dir = new ResourceDirInfo(url);

			foreach (string fileName in fileSystem.GetScriptFiles(dirPath))
			{
				dir.Files.Add(fileName);
			}
			foreach (string fileName in fileSystem.GetStyleFiles(dirPath))
			{
				dir.Files.Add(fileName);
			}
			foreach (string fileName in fileSystem.GetImageFiles(dirPath))
			{
				dir.Files.Add(fileName);
			}

			foreach (string subdirPath in fileSystem.GetDirectories(dirPath))
			{
				string directoryname = fileSystem.GetDirectoryName(subdirPath);
				if (Array.Exists(ignore, delegate(string ignored) { return ignored == directoryname; }))
					continue;
				IDirInfo subdir = Process(rootPath, subdirPath);
				dir.AddSubDirectory(subdir);
				if (subdir.Files.Count > 0)
					subdir.HasFiles = true;
			}
			return dir;
		}

		static string NormalizeUrl(string rootPath, string dirPath)
		{
			string url = dirPath
				.Replace(rootPath, "")
				.Replace(Path.DirectorySeparatorChar, '/')
				.Trim('/');

			if (url == string.Empty)
				return url;

			return "/" + url;
		}
	}
}