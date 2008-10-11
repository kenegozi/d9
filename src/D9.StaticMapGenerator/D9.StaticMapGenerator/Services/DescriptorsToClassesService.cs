using System.Collections.Generic;
using D9.StaticMapGenerator.GeneratedClassMetadata;

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// A component used for mapping class descriptors to class names
	/// </summary>
	public class DescriptorsToClassesService : IDescriptorsToClassesService
	{
		readonly IDescriptorToClassService descriptorToClassService;

		/// <summary>
		/// Creates a new instance of DescriptorsToClassesService using a given <see cref="IDescriptorToClassService"/>
		/// </summary>
		/// <param name="descriptorToClassService">An <see cref="IDescriptorToClassService"/> for the component to use</param>
		public DescriptorsToClassesService(IDescriptorToClassService descriptorToClassService)
		{
			this.descriptorToClassService = descriptorToClassService;
		}

		public string[] Execute(ClassDescriptor[] classDescriptors)
		{
			List<string> classes = new List<string>(classDescriptors.Length);
			foreach (ClassDescriptor descriptor in classDescriptors)
			{
				classes.Add(descriptorToClassService.Execute(descriptor));
			}
			return classes.ToArray();
		}
	}
}