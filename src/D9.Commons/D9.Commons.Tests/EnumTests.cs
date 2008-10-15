#region License

// Copyright (c) 2008 Ken Egozi (ken@kenegozi.com)
// All rights reserved.
// 
// Redistribution and use in source and binary forms, with or without modification,
// are permitted provided that the following conditions are met:
// 
//     * Redistributions of source code must retain the above copyright notice,
//     this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice,
//     this list of conditions and the following disclaimer in the documentation
//     and/or other materials provided with the distribution.
//     * Neither the name of the D9 project nor the names of its
//     contributors may be used to endorse or promote products derived from this
//     software without specific prior written permission.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
// ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
// WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
// DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE
// FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL
// DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR
// SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER
// CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY,
// OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
// THIS SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

#endregion License

using D9.Commons.Internal;
using NUnit.Framework;
using NUnit.Framework.ExtensionMethods;
using Description = System.ComponentModel.DescriptionAttribute;

namespace D9.Commons.Tests
{
	// ReSharper disable AccessToStaticMemberViaDerivedType
	[TestFixture]
	public class EnumTests
	{
		[Test]
		public void TwoDifferentEnumerations_WorksWithoutInterfering()
		{
			var value1 = Enum<DescribedEnumeration>.From(VALUE_1_DESCRIPTION);
			var value4 = Enum<TotallyDifferentEnumeration>.From(VALUE_4_DESCRIPTION);

			value1.Should(Be.EqualTo(DescribedEnumeration.Value1));
			value4.Should(Be.EqualTo(TotallyDifferentEnumeration.Value4));
		}

		[Test]
		public void TwoSimilarEnumerations_WorksWithoutInterfering()
		{
			var value1FromEnum1 = Enum<DescribedEnumeration>.From(VALUE_1_DESCRIPTION);
			var value1FromEnum2 = Enum<MixedDescribedEnumeration>.From(VALUE_1_DESCRIPTION);

			value1FromEnum1.Should(Be.EqualTo(DescribedEnumeration.Value1));
			value1FromEnum2.Should(Be.EqualTo(MixedDescribedEnumeration.Value1));
		}

		#region enum declerations
		public const string VALUE_1_DESCRIPTION = "Value1";
		public const string VALUE_2_DESCRIPTION = "Value2";
		public const string VALUE_3_DESCRIPTION = "Value3";
		public const string VALUE_4_DESCRIPTION = "Value4";

		public enum DescribedEnumeration
		{
			[@Description(VALUE_1_DESCRIPTION)]
			Value1,

			[@Description(VALUE_2_DESCRIPTION)]
			Value2
		}
		public enum NonDescribedEnumeration
		{
			Value
		}
		public enum MixedDescribedEnumeration
		{
			[@Description(VALUE_1_DESCRIPTION)]
			Value1,

			Value2
		}
		public enum TotallyDifferentEnumeration
		{
			[@Description(VALUE_3_DESCRIPTION)]
			Value3,

			[@Description(VALUE_4_DESCRIPTION)]
			Value4
		}

		#endregion

	}
}