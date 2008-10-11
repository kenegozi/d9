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
			typeof(DescribedEnumeration).GetFields();

			var handler = new DescribedEnumHandler(typeof(DescribedEnumeration));

			var value = handler.GetDescriptionFrom(DescribedEnumeration.Value1);
			
			value.Should(Be.EqualTo(VALUE_1_DESCRIPTION));
		}

		[Test]
		public void GetDescriptionOf_DescribedItemInMixedEnum_ReturnsTheDescribedValue()
		{
			var handler = new DescribedEnumHandler(typeof(MixedDescribedEnumeration));

			var value = handler.GetDescriptionFrom(MixedDescribedEnumeration.Value1);

			value.Should(Be.EqualTo(VALUE_1_DESCRIPTION));
		}

		[Test]
		public void GetDescriptionOf_NonDescribedItemInMixedEnum_ReturnsTheRawValue()
		{
			var handler = new DescribedEnumHandler(typeof(MixedDescribedEnumeration));

			var value = handler.GetDescriptionFrom(MixedDescribedEnumeration.Value2);

			value.Should(Be.EqualTo(MixedDescribedEnumeration.Value2.ToString()));
		}

		[Test]
		public void GetDescriptionOf_NonDescribedEnum_ReturnsTheRawValue()
		{
			var handler = new DescribedEnumHandler(typeof(NonDescribedEnumeration));

			var value = handler.GetDescriptionFrom(NonDescribedEnumeration.Value);

			value.Should(Be.EqualTo(NonDescribedEnumeration.Value.ToString()));
		}
		
		enum DescribedEnumeration
		{
			[@Description(VALUE_1_DESCRIPTION)]
			Value1,

			[@Description(VALUE_2_DESCRIPTION)]
			Value2
		}
		enum NonDescribedEnumeration
		{
			Value
		}
		enum MixedDescribedEnumeration
		{
			[@Description(VALUE_1_DESCRIPTION)]
			Value1,

			Value2
		}
	}
}