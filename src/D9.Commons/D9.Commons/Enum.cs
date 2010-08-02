#region License

// Copyright (c) 2008-2010 Ken Egozi (ken@kenegozi.com)
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

using System;
using System.ComponentModel;
using D9.Commons.Internal;

namespace D9.Commons
{
	/// <summary>
	/// Provide access to enum helpers
	/// </summary>
	public static class Enum<T>
	{
		private static readonly Type enumType;
		private static readonly DescribedEnumHandler<T> handler;

		static Enum()
		{
			enumType = typeof (T);
			if (enumType.IsEnum == false)
				throw new ArgumentException(string.Format("{0} is not an enum", enumType));

			handler = new DescribedEnumHandler<T>();
		}

		/// <summary>
		/// Extract the description for a given enum value
		/// </summary>
		/// <param name="value">An enum value</param>
		/// <returns>It's description, or it's name if there's no registered description for the given value</returns>
		public static string GetDescriptionOf(T value)
		{
			return handler.GetDescriptionFrom(value);
		}

		/// <summary>
		/// Gets the enum value for a given description or value
		/// </summary>
		/// <param name="descriptionOrName">The enum value or description</param>
		/// <returns>An enum value matching the given string value, as description (using <see cref="DescriptionAttribute"/>) or as value</returns>
		public static T From(string descriptionOrName)
		{
			return handler.GetValueFrom(descriptionOrName);
		}
	}
}