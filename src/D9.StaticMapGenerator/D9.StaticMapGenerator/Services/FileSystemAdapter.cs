using System.Collections.Generic;
using System.IO;

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// Provide access to the file system
	/// </summary>
	public class FileSystemAdapter : IFileSystemAdapter
	{
		#region IFileSystemAdapter
		public virtual string[] GetScriptFiles(string dir)
		{
			return GetFiles(dir, "*.js");
		}

		public virtual string[] GetStyleFiles(string dir)
		{
			return GetFiles(dir, "*.css");
		}

		public virtual string[] GetImageFiles(string dir)
		{
			List<string> imageFiles = new List<string>();
			imageFiles.AddRange(GetFiles(dir, "*.jpg"));
			imageFiles.AddRange(GetFiles(dir, "*.gif"));
			imageFiles.AddRange(GetFiles(dir, "*.png"));
			return imageFiles.ToArray();
		}

		public virtual string[] GetDirectories(string dir)
		{
			return Directory.GetDirectories(dir);
		}

		public string GetDirectoryName(string dirPath)
		{
			DirectoryInfo directoryInfo = new DirectoryInfo(dirPath);
			return directoryInfo.Name;
		}
		#endregion

		#region helpers
		private static string[] GetFileNamesFrom(IEnumerable<string> filePaths)
		{
			List<string> files = new List<string>();
			foreach (string filePath in filePaths)
			{
				files.Add(Path.GetFileName(filePath));
			}
			return files.ToArray();
		}


		private static string[] GetFiles(string dir, string pattern)
		{
			return GetFileNamesFrom(Directory.GetFiles(dir, pattern, SearchOption.TopDirectoryOnly));
		}
		#endregion


	}
}