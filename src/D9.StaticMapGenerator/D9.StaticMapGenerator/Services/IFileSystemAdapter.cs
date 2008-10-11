namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// Provide access to the file system
	/// </summary>
	public interface IFileSystemAdapter
	{
		/// <summary>
		/// Gets script files (*.js) from a given directory
		/// </summary>
		/// <param name="dir">The given directory</param>
		/// <returns>A string array of filenames</returns>
		string[] GetScriptFiles(string dir);

		/// <summary>
		/// Gets style files (*.css) from a given directory
		/// </summary>
		/// <param name="dir">The given directory</param>
		/// <returns>A string array of filenames</returns>
		string[] GetStyleFiles(string dir);

		/// <summary>
		/// Gets image files (*.png <c>etc</c>.) from a given directory
		/// </summary>
		/// <param name="dir">The given directory</param>
		/// <returns>A string array of filenames</returns>
		string[] GetImageFiles(string dir);

		/// <summary>
		/// Gets a list of subdirectories of a given directory
		/// </summary>
		/// <param name="dir">The given directory</param>
		/// <returns>A string array of directory names</returns>
		string[] GetDirectories(string dir);

		/// <summary>
		/// Extract the directory name off a given directory path
		/// </summary>
		/// <param name="dirPath">The directory path</param>
		/// <returns>The directory's name</returns>
		string GetDirectoryName(string dirPath);
	}
}