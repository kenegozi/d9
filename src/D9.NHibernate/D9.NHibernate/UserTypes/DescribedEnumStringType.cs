using System;
using System.Data;
using D9.Commons;
using NHibernate;
using NHibernate.Type;

namespace D9.NHibernate.UserTypes
{
	public class DescribedEnumStringType<T> : EnumStringType
		where T : struct 
	{
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