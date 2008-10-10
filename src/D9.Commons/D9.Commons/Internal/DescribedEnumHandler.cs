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

		public string GetDescriptionFrom(Enum value)
		{
			return toDescription[value];
		}

		public Enum GetValueFrom(string title)
		{
			return fromDescription[title];
		}

	}
}