using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace NetClock.Application.Common
{
    public class LazyConcurrentDictionary<TKey, TValue>
    {
        private readonly ConcurrentDictionary<TKey, Lazy<TValue>> _dictionary;

        public LazyConcurrentDictionary()
        {
            _dictionary = new ConcurrentDictionary<TKey, Lazy<TValue>>();
        }

        public LazyConcurrentDictionary(IEqualityComparer<TKey> comparer)
        {
            _dictionary = new ConcurrentDictionary<TKey, Lazy<TValue>>(comparer);
        }

        public int Count => _dictionary.Count;

        public TValue GetOrAdd(TKey key, Func<TKey, TValue> valueFactory)
        {
            var lazyResult = _dictionary.GetOrAdd(
                key,
                k => new Lazy<TValue>(() => valueFactory(k), LazyThreadSafetyMode.ExecutionAndPublication));

            return lazyResult.Value;
        }

        public TValue AddOrUpdate(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            var lazyResult = _dictionary.AddOrUpdate(
                key,
                new Lazy<TValue>(() => addValue),
                (k, currentValue) => new Lazy<TValue>(
                    () => updateValueFactory(k, currentValue.Value),
                    LazyThreadSafetyMode.ExecutionAndPublication));

            return lazyResult.Value;
        }

        public TValue AddOrUpdate(
            TKey key,
            Func<TKey, TValue> addValueFactory,
            Func<TKey, TValue, TValue> updateValueFactory)
        {
            var lazyResult = _dictionary.AddOrUpdate(
                key,
                k => new Lazy<TValue>(() => addValueFactory(k)),
                (k, currentValue) => new Lazy<TValue>(
                    () => updateValueFactory(k, currentValue.Value),
                    LazyThreadSafetyMode.ExecutionAndPublication));

            return lazyResult.Value;
        }
    }
}
