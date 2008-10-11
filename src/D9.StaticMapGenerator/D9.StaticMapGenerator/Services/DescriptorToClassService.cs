using System.Text;
using D9.StaticMapGenerator.GeneratedClassMetadata;

namespace D9.StaticMapGenerator.Services
{
	/// <summary>
	/// A component used for mapping a class descriptor to a class names
	/// </summary>
	public class DescriptorToClassService : IDescriptorToClassService
	{
		public string Execute(ClassDescriptor classDescriptor)
		{
			StringBuilder classBuilder = new StringBuilder();
			classBuilder.AppendFormat(@"	public partial class {0} {1}
	{{
", classDescriptor.Name, classDescriptor.Bases);

			foreach (MemberDescriptor member in classDescriptor.Members)
			{
				classBuilder.AppendFormat(@"		public readonly {0} {1} = {2};
", member.Type, member.Name, member.Value);
			}

			classBuilder.AppendLine("	}");
			return classBuilder.ToString();
		}
	}
}