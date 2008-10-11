using System.Collections.Generic;

namespace D9.StaticMapGenerator.GeneratedClassMetadata
{
	/// <summary>
	/// Describes a generated class
	/// </summary>
	public class ClassDescriptor
	{
		/// <summary>
		/// The class' name
		/// </summary>
		public string Name { get; set;}

		/// <summary>
		/// Base class and interfaces
		/// </summary>
		public string Bases{ get; set;}

		/// <summary>
		/// The class' members
		/// </summary>
		public List<MemberDescriptor> Members { get; private set;}

		/// <summary>
		/// Creates a new instance of ClassDescriptor
		/// </summary>
		public ClassDescriptor()
		{
			Members = new List<MemberDescriptor>();
		}
	}
}