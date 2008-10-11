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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using D9.Commons.Internal;

namespace D9.Commons
{
	/// <summary>
	/// Provide access to enum helpers
	/// </summary>
	public static class Enums
	{
		private static readonly IDictionary handlers = new ListDictionary();

		/// <summary>
		/// Initialises enum types to be used with the <see cref="Enums"></see>
		/// </summary>
		/// <param name="assemblies">The assemblies to grab described enums from</param>
		public static void Initialise(params Assembly[] assemblies)
		{
			Initialise((IEnumerable<Assembly>)assemblies);
		}

		/// <summary>
		/// Initialises enum types to be used with the <see cref="Enums"></see>
		/// </summary>
		/// <param name="assemblies">The assemblies to grab described enums from</param>
		public static void Initialise(IEnumerable<Assembly> assemblies)
		{
			var enums = from assembly in assemblies
							  select assembly
								  into a
								  from type in a.GetTypes()

								  where type.IsEnum &&
										(from f in type.GetFields()
										 where f.GetCustomAttributes(typeof(DescriptionAttribute), false).Length == 1
										 select f
										).Count() > 0
								  orderby type.FullName
								  select type;
			Initialise(enums);
		}

		/// <summary>
		/// Initialises enum types to be used with the <see cref="Enums"></see>
		/// </summary>
		/// <param name="enumTypes">The enum types to initialise</param>
		public static void Initialise(params Type[] enumTypes)
		{
			Initialise((IEnumerable<Type>)enumTypes);
		}

		/// <summary>
		/// Initialises enum types to be used with the <see cref="Enums"></see>
		/// </summary>
		/// <param name="enumTypes">The enum types to initialise</param>
		public static void Initialise(IEnumerable<Type> enumTypes)
		{
			foreach (var type in enumTypes)
			{
				handlers.Add(type, new DescribedEnumHandler(type));
			}
		}

		/// <summary>
		/// Extract the description for a given enum value
		/// </summary>
		/// <param name="value">An enum value</param>
		/// <returns>It's description, or it's name if there's no registered description for the given value</returns>
		public static string GetDescriptionOf(object value)
		{
			var handler = handlers[value.GetType()] as DescribedEnumHandler;

			return handler != null
				? handler.GetDescriptionFrom((Enum)value)
				: value.ToString();
		}

		/// <summary>
		/// Gets the enum value for a given description or value
		/// </summary>
		/// <typeparam name="T">The enum type</typeparam>
		/// <param name="stringValue">The enum value or description</param>
		/// <returns>An enum value matching the given string value, as description (using <see cref="DescriptionAttribute"/>) or as value</returns>
		public static Enum ToEnum<T>(this string stringValue)
			where T : struct
		{
			var type = typeof(T);
			var handler = handlers[type] as DescribedEnumHandler;

			return handler != null
				? handler.GetValueFrom(stringValue)
				: (Enum)Enum.Parse(type, stringValue, false);
		}
	}
}