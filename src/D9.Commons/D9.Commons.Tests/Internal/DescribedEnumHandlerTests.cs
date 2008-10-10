using D9.Commons.Internal;
using NUnit.Framework;
using NUnit.Framework.ExtensionMethods;
using Description = System.ComponentModel.DescriptionAttribute;

namespace D9.Commons.Tests.Internal
{
	// ReSharper disable AccessToStaticMemberViaDerivedType
	[TestFixture]
	public class DescribedEnumHandlerTests
	{
		private const string VALUE_1_DESCRIPTION = "Value1";
		private const string VALUE_2_DESCRIPTION = "Value2";

		[Test]
		public void GetDescriptionOf_DescribedItem_ReturnsTheDescribedValue()
		{
			typeof (DescribedEnum).GetFields();

			var handler = new DescribedEnumHandler(typeof(DescribedEnum));

			var value = handler.GetDescriptionFrom(DescribedEnum.Value1);
			
			value.Should(Be.EqualTo(VALUE_1_DESCRIPTION));
		}

		[Test]
		public void GetDescriptionOf_DescribedItemInMixedEnum_ReturnsTheDescribedValue()
		{
			var handler = new DescribedEnumHandler(typeof(MixedDescribedEnum));

			var value = handler.GetDescriptionFrom(MixedDescribedEnum.Value1);

			value.Should(Be.EqualTo(VALUE_1_DESCRIPTION));
		}

		[Test]
		public void GetDescriptionOf_NonDescribedItemInMixedEnum_ReturnsTheRawValue()
		{
			var handler = new DescribedEnumHandler(typeof(MixedDescribedEnum));

			var value = handler.GetDescriptionFrom(MixedDescribedEnum.Value2);

			value.Should(Be.EqualTo(MixedDescribedEnum.Value2.ToString()));
		}

		[Test]
		public void GetDescriptionOf_NonDescribedEnum_ReturnsTheRawValue()
		{
			var handler = new DescribedEnumHandler(typeof(NonDescribedEnum));

			var value = handler.GetDescriptionFrom(NonDescribedEnum.Value);

			value.Should(Be.EqualTo(NonDescribedEnum.Value.ToString()));
		}
		
		enum DescribedEnum
		{
			[@Description(VALUE_1_DESCRIPTION)]
			Value1,

			[@Description(VALUE_2_DESCRIPTION)]
			Value2
		}
		enum NonDescribedEnum
		{
			Value
		}
		enum MixedDescribedEnum
		{
			[@Description(VALUE_1_DESCRIPTION)]
			Value1,

			Value2
		}
	}
}