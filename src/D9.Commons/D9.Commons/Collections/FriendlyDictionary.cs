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