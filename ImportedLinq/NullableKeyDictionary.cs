using System.Collections;
using System.Collections.Generic;

namespace ImportedLinq
{
    internal class NullableKeyDictionary<TKey, TValue> : IReadOnlyDictionary<TKey, TValue>
    {
        private readonly IReadOnlyDictionary<TKey, TValue> source;
        private readonly bool hasNullKey;
        private readonly TValue nullValue;

        public NullableKeyDictionary(IReadOnlyDictionary<TKey, TValue> source)
        {
            this.source = source;
            nullValue = default;
            hasNullKey = false;
        }

        public NullableKeyDictionary(IReadOnlyDictionary<TKey, TValue> source, TValue nullValue)
        {
            this.source = source;
            this.nullValue = nullValue;
            hasNullKey = true;
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (KeyValuePair<TKey, TValue> it in source)
            {
                yield return it;
            }

            if (hasNullKey)
            {
                yield return new KeyValuePair<TKey, TValue>(default, nullValue);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int Count => hasNullKey ? source.Count + 1 : source.Count;

        public bool ContainsKey(TKey key)
        {
            if (hasNullKey && key == null)
            {
                return true;
            }
            else
            {
                return source.ContainsKey(key);
            }
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            if (hasNullKey && key == null)
            {
                value = nullValue;
                return true;
            }
            else
            {
                return TryGetValue(key, out value);
            }
        }

        public TValue this[TKey key] => key == null && hasNullKey
            ? nullValue
            : source[key];

        public IEnumerable<TKey> Keys
        {
            get
            {
                foreach (TKey key in source.Keys)
                {
                    yield return key;
                }

                if (hasNullKey)
                {
                    yield return default;
                }
            }
        }

        public IEnumerable<TValue> Values
        {
            get
            {
                foreach (TValue value in source.Values)
                {
                    yield return value;
                }

                if (hasNullKey)
                {
                    yield return nullValue;
                }
            }
        }
    }
}