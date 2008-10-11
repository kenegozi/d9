using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace D9.Commons.Internal
{
	/// <summary>
	/// Used to cache enum values descriptions mapper
	/// </summary>
	public class DescribedEnumHandler
	{
		private readonly IDictionary<Enum, string> toDescription = new Dictionary<Enum, string>();
		private readonly IDictionary<string, Enum> fromDescription = new Dictionary<string, Enum>();

		private const BindingFlags PUBLIC_STATIC = BindingFlags.Public | BindingFlags.Static;

		/// <summary>
		/// Creates a new DescribedEnumHandler instance for a given enum type
		/// </summary>
		/// <param name="type">The enum type</param>
		public DescribedEnumHandler(Type type)
		{
			var enumEntrys = from f in type.GetFields(PUBLIC_STATIC)
			                 let attributes = f.GetCustomAttributes(typeof (DescriptionAttribute), false)
			                 let description =
			                 	attributes.Length == 1
			                 		? ((DescriptionAttribute) attributes[0]).Description
			                 		: f.Name
			                 select new
			                        	{
			                        		Value = (Enum) Enum.Parse(type, f.Name),
			                        		Description = description
			                        	};

			foreach (var enumEntry in enumEntrys)
			{
				toDescription[enumEntry.Value] = enumEntry.Description;
				fromDescription[enumEntry.Description] = enumEntry.Value;
			}
		}

		/// <summary>
		/// Extracts the description for the given enum value.
		/// <remarks>if no description was set for the given value, it's name will be retrieved</remarks>
		/// </summary>
		/// <param name="value">The enum value</param>
		/// <returns>The value's description</returns>
		public string GetDescriptionFrom(Enum value)
		{
			return toDescription[value];
		}

		/// <summary>
		/// Parse the given string and return the enum value for with the given string acts as description
		/// </summary>
		/// <param name="description">The given description</param>
		/// <returns>A matching enum value</returns>
		public Enum GetValueFrom(string description)
		{
			return fromDescription[description];
		}

	}
}