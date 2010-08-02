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
			return Enum<T>.From(value);
		}

		static string GetDescriptionOf(T value)
		{
			return Enum<T>.GetDescriptionOf(value);
		}

	}
}