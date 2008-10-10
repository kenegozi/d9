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
		/// <param name="enumTypes">The enum types to initiailse</param>
		public static void Initialise(params Type[] enumTypes)
		{
			Initialise((IEnumerable<Type>)enumTypes);
		}

		/// <summary>
		/// Initialises enum types to be used with the <see cref="Enums"></see>
		/// </summary>
		/// <param name="enumTypes">The enum types to initiailse</param>
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
		/// <returns>An enum value matching the given string value, as description (using <see cref="DescriptionAttribute">DescriptionAttribute</see>) or as value</returns>
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