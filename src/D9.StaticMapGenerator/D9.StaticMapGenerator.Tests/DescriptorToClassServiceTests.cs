using D9.StaticMapGenerator.GeneratedClassMetadata;
using D9.StaticMapGenerator.Services;
using NUnit.Framework;

namespace D9.StaticMapGenerator.Tests
{
	[TestFixture]
	public class DescriptorToClassServiceTests
	{
		[Test]
		public void Execute_WhenHasClassBasedMember_Works()
		{
			ClassDescriptor cls = new ClassDescriptor();
			cls.Name = "the_class";
			cls.Members.Add(new MemberDescriptor("the_type", "member_name"));

			IDescriptorToClassService service = new DescriptorToClassService();

			string classDef = service.Execute(cls);

			Assert.IsTrue(classDef.IndexOf("class the_class") > -1);
			Assert.IsTrue(classDef.IndexOf("the_type member_name = new the_type();") > -1);
		}

		[Test]
		public void Execute_WhenHasStringMember_Works()
		{
			ClassDescriptor cls = new ClassDescriptor();
			cls.Name = "the_class";
			cls.Members.Add(new MemberDescriptor("string", "member_name", "\"value\""));

			IDescriptorToClassService service = new DescriptorToClassService();

			string classDef = service.Execute(cls);

			Assert.IsTrue(classDef.IndexOf("class the_class") > -1);
			Assert.IsTrue(classDef.IndexOf("string member_name = \"value\";") > -1);
		}
	}
}