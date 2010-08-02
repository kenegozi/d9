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
using System.Collections;
using System.Collections.Generic;

namespace D9.Commons.Collections
{
	public class FriendlyDictionary<TKey, TValue> : IDictionary<TKey, TValue>
	{
		private readonly Dictionary<TKey, TValue> _inner;
		#region Implementation of IEnumerable

		public FriendlyDictionary()
		{
			_inner = new Dictionary<TKey, TValue>();
		}

		public FriendlyDictionary(IDictionary<TKey, TValue> dictionary)
		{
			_inner = new Dictionary<TKey, TValue>(dictionary);
		}

		public FriendlyDictionary(IDictionary<TKey, TValue> dictionary, IEqualityComparer<TKey> comparer)
		{
			_inner = new Dictionary<TKey, TValue>(dictionary, comparer);
		}

		public FriendlyDictionary(IEqualityComparer<TKey> comparer)
		{
			_inner = new Dictionary<TKey, TValue>(comparer);
		}

		public FriendlyDictionary(int capacity)
		{
			_inner = new Dictionary<TKey, TValue>(capacity);
		}

		public FriendlyDictionary(int capacity, IEqualityComparer<TKey> comparer)
		{
			_inner = new Dictionary<TKey, TValue>(capacity, comparer);
		}

		public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
		{
			return _inner.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}

		#endregion

		#region Implementation of ICollection<KeyValuePair<TKey,TValue>>

		public void Add(KeyValuePair<TKey, TValue> item)
		{
			try
			{
				((ICollection<KeyValuePair<TKey, TValue>>)_inner).Add(item);
			}
			catch (ArgumentException ex)
			{
				var betterEx = new ArgumentException(string.Format("Cannot add item with key=[{0}] as it already exists", item.Key), ex);
				betterEx.Data["Key"] = item.Key;
				betterEx.Data["Value"] = item.Value;
				throw betterEx;
			}
		}

		public void Clear()
		{
			((ICollection<KeyValuePair<TKey, TValue>>)_inner).Clear();
		}

		public bool Contains(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)_inner).Contains(item);
		}

		public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
		{
			((ICollection<KeyValuePair<TKey, TValue>>)_inner).CopyTo(array, arrayIndex);
		}

		public bool Remove(KeyValuePair<TKey, TValue> item)
		{
			return ((ICollection<KeyValuePair<TKey, TValue>>)_inner).Remove(item);
		}

		public int Count
		{
			get { return _inner.Count; }
		}

		public bool IsReadOnly
		{
			get { return ((ICollection<KeyValuePair<TKey, TValue>>)_inner).IsReadOnly; }
		}

		#endregion

		#region Implementation of IDictionary<TKey,TValue>

		public bool ContainsKey(TKey key)
		{
			return _inner.ContainsKey(key);
		}

		public void Add(TKey key, TValue value)
		{
			try
			{
				_inner.Add(key, value);
			}
			catch (ArgumentException ex)
			{
				var betterEx = new ArgumentException(string.Format("Cannot add item with key=[{0}] as it already exists", key), ex);
				betterEx.Data["Key"] = key;
				betterEx.Data["Value"] = value;
				throw betterEx;
			}
		}

		public bool Remove(TKey key)
		{
			return _inner.Remove(key);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _inner.TryGetValue(key, out value);
		}

		public TValue this[TKey key]
		{
			get
			{
				TValue item;
				if (_inner.TryGetValue(key, out item))
					return item;
				var ex = new KeyNotFoundException(string.Format("Missing key [{0}]", key));
				ex.Data["Key"] = key;
				throw ex;
			}
			set { _inner[key] = value; }
		}

		public ICollection<TKey> Keys
		{
			get { return _inner.Keys; }
		}

		public ICollection<TValue> Values
		{
			get { return _inner.Values; }
		}

		#endregion
	}
}