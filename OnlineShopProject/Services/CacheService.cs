using OnlineShop.Db.Models;
using OnlineShopProject.Interfaces;
using Serilog.Events;
using System.Collections.Concurrent;

namespace OnlineShopProject.Services
{
    public class CacheService<TKey, TValue> : IDisposable
    { 
        private ConcurrentDictionary<TKey, TtkValue> _cache;
        private Timer _timer;
        public CacheService(long removeInterval)
        {
            _cache = new ConcurrentDictionary<TKey, TtkValue>();
            _timer = new Timer(s => _ = ClearLauncher(), false, removeInterval, removeInterval);
        }
        private SemaphoreSlim _lock = new SemaphoreSlim(1);
        private async Task ClearLauncher()
        {
            await _lock.WaitAsync().ConfigureAwait(false);
            try
            {
                Clear();
            }
            finally
            {
                _lock.Release();
            }
        }
        private void Clear()
        {
            if (Monitor.TryEnter(_timer))
            {
                try
                {
                    long time = Environment.TickCount64;
                    foreach (var item in _cache)
                    {
                        if (item.Value.Ttk < time)
                        {
                            _cache.TryRemove(item.Key, out _);
                        }
                    }
                }
                finally
                {
                    Monitor.Exit(_timer);
                }
            }
            
        }
        public void AddOrUpdate(TKey key, TValue value, TimeSpan time)
        {
            TtkValue ttk = new TtkValue(value, time.Ticks);
            _cache.AddOrUpdate(key, ttk, (k, v) => ttk);
        }
        public void TryAdd(TKey key, TValue value, TimeSpan time)
        {
            if (!TryGet(key, out _))
            {
                TtkValue val = new TtkValue(value, time.Ticks);
                _cache.TryAdd(key, val);
            }
        }
        public bool TryGet(TKey key, out TValue value)
        {
            value = default(TValue);
            TtkValue dictValue;
            if (_cache.TryGetValue(key, out dictValue))
            {
                if (dictValue.IsExpire())
                {
                    KeyValuePair<TKey, TtkValue> pair = new KeyValuePair<TKey, TtkValue>(key, dictValue);
                    _cache.TryRemove(pair);
                    return false;
                }
                value = dictValue.Value;
                return true;
            }
            return false;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~CacheService()
        {
            Dispose(false);
        }
        private bool isDisposed = false;
        private void Dispose(bool disposing)
        {
            if (isDisposed) return;
            if (disposing) { }
            _lock.Dispose();
            _timer.Dispose();
            isDisposed = true;
        }
        private class TtkValue
        {
            public TValue Value { get; set; }
            public long Ttk { get; set; }
            public TtkValue(TValue value, long ttk)
            {
                Value = value;
                Ttk = ttk + Environment.TickCount64;
            }
            public bool IsExpire()
            {
                return Ttk < Environment.TickCount64;
            }
        }
    }
}
