using D9.StaticMapGenerator.GeneratedClassMetadata;

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// A component used for mapping a class descriptor to a class names
	/// </summary>
	public interface IDescriptorToClassService
	{
		/// <summary>
		/// Map an instance of <see cref="ClassDescriptor"/> to a class name
		/// </summary>
		/// <param name="classDescriptor">The given <see cref="ClassDescriptor"/></param>
		/// <returns>Class name</returns>
		string Execute(ClassDescriptor classDescriptor);
	}
}