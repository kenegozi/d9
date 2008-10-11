using D9.StaticMapGenerator.DirectoryStructure;

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// A component for building a directory structure representation out of a given root path
	/// </summary>
	public interface IBuildDirectoryStructureService
	{
		/// <summary>
		/// Builds a directory structure representation out of a given root path
		/// </summary>
		/// <param name="rootPath">The given root path</param>
		/// <returns><see cref="IDirInfo"/> instance representing the directory structure</returns>
		IDirInfo Execute(string rootPath);

		/// <summary>
		/// Builds a directory structure representation out of a given root path, ignoring the given directories
		/// </summary>
		/// <param name="rootPath">The given root path</param>
		/// <param name="ignoreDirectories">Directory names to ignore</param>
		/// <returns><see cref="IDirInfo"/> instance representing the directory structure</returns>
		IDirInfo Execute(string rootPath, string[] ignoreDirectories);
	}
}