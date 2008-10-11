namespace D9.StaticMapGenerator.GeneratedClassMetadata
{
	/// <summary>
	/// Describes a generated member
	/// </summary>
	public class MemberDescriptor
	{
		/// <summary>
		/// Creates a new instance of MemberDescriptor, for a member with an initial value
		/// </summary>
		/// <param name="type">The member's type</param>
		/// <param name="name">The member's name</param>
		/// <param name="value">The member's initial value</param>
		public MemberDescriptor(string type, string name, string value)
		{
			Type = type;
			Name = name;
			Value = value;
		}

		/// <summary>
		/// Creates a new instance of MemberDescriptor, for a member without an initial value
		/// </summary>
		/// <param name="type">The member's type</param>
		/// <param name="name">The member's name</param>
		public MemberDescriptor(string type, string name)
			: this(type, name, "new " + type + "()")
		{
		}
		 
		///	<summary>
		///	The member's type name
		///	</summary>
		public string Type { get; private set; }

		///	<summary>
		///	The member's name
		///	</summary>
		public string Name { get; private set; }

		///	<summary>
		///	The member's initial value
		///	</summary>
		public string Value { get; private set; }
	}
}