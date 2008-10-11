using System.Collections.Generic;

namespace D9.StaticMapGenerator.DirectoryStructure
{
	/// <summary>
	/// Represents a directory with possible static web resources
	/// </summary>
	public interface IDirInfo
	{
		/// <summary>
		/// The <c>url</c> for the current directory 
		/// </summary>
		string Url { get; }

		/// <summary>
		/// Directory name
		/// </summary>
		string Name { get; }

		/// <summary>
		/// The parent directory
		/// </summary>
		IDirInfo Parent { get; set; }

		/// <summary>
		/// True if has resources. 
		/// <remarks>Should this value be true for this directory, it should also be true to all parent directories</remarks>
		/// </summary>
		bool HasFiles { get; set; }

		/// <summary>
		/// Sub directories
		/// </summary>
		IDirInfo[] Directories { get; }

		/// <summary>
		/// Filenames within current directories
		/// </summary>
		List<string> Files { get; }

		/// <summary>
		/// Adds a subdirectory under the current one
		/// </summary>
		/// <param name="subDirectory"></param>
		void AddSubDirectory(IDirInfo subDirectory);

		/// <summary>
		/// Remove directories without any resources
		/// </summary>
		void RemoveEmptyDirectories();
	}
}