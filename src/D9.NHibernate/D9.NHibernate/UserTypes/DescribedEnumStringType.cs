using System;
using System.ComponentModel;
using System.Data;
using D9.Commons;
using NHibernate;
using NHibernate.Type;

namespace D9.NHibernate.UserTypes
{
	/// <summary>
	/// <c>NHibernate</c> type representing an enum that should be persisted using it's
	/// <see cref="DescriptionAttribute"/> values
	/// </summary>
	/// <typeparam name="T">The enum type</typeparam>
	public class DescribedEnumStringType<T> : EnumStringType
		where T : struct 
	{
		/// <summary>
		/// <c>NHibernate</c> type representing an enum that should be persisted using it's
		/// <see cref="DescriptionAttribute"/> values
		/// </summary>
		public DescribedEnumStringType()
			: base(typeof(T))
		{
		}


		public override object GetInstance(object code)
		{
			try
			{
				return FromString(code as string);
			}
			catch (ArgumentOutOfRangeException ae)
			{
				throw new HibernateException(string.Format("Can't Parse {0} as [{1}]", code, typeof(T)), ae);
			}
		}

		public override object GetValue(object code)
		{
			return GetDescriptionOf((T)code);
		}

		public override void Set(IDbCommand cmd, object value, int index)
		{
			var par = (IDataParameter)cmd.Parameters[index];
			if (value == null)
			{
				par.Value = DBNull.Value;
			}
			else
			{
				par.Value = GetDescriptionOf((T)value);
			}
		}

		static object FromString(string value)
		{
			return value.ToEnum<T>();
		}

		static string GetDescriptionOf(T value)
		{
			return Enums.GetDescriptionOf(value);
		}

	}
}