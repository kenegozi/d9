using D9.StaticMapGenerator.GeneratedClassMetadata;

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// A component used for mapping class descriptors to class names
	/// </summary>
	public interface IDescriptorsToClassesService
	{
		/// <summary>
		/// Map a bunch of <see cref="ClassDescriptor"/> instances to class names
		/// </summary>
		/// <param name="classDescriptors">The given <see cref="ClassDescriptor"/>
		/// instances</param>
		/// <returns>Class names as string array</returns>
		string[] Execute(ClassDescriptor[] classDescriptors);
	}
}