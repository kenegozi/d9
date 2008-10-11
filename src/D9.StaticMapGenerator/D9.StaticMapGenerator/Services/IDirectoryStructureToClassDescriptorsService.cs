using D9.StaticMapGenerator.DirectoryStructure;
using D9.StaticMapGenerator.GeneratedClassMetadata;

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// A component for generating <see cref="ClassDescriptor"/> instances
	/// out of a directory structure
	/// </summary>
	public interface IDirectoryStructureToClassDescriptorsService
	{
		/// <summary>
		/// Generates <see cref="ClassDescriptor"/> instances out of given directory
		/// structure
		/// </summary>
		/// <param name="site">The directory structure as the site's root</param>
		/// <returns><see cref="ClassDescriptor"/> instances representing the given
		/// directory structure as site map</returns>
		ClassDescriptor[] Execute(IDirInfo site);
	}
}