using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XCodeFramework.Core.Config;

namespace XCodeFramework.Core.Cache
{
    public class RedisCacheManager : ICacheManager
    {
        private readonly string redisConnectionString;

        public volatile ConnectionMultiplexer redisConnection;

        private readonly object redisConnectionLock = new object();

        public RedisCacheManager(ApplicationConfig config)
        {
            if (string.IsNullOrWhiteSpace(config.RedisCacheConfig.ConnectionString))
            {
                throw new ArgumentException("redis config is empty", nameof(config));
            }
            this.redisConnectionString = config.RedisCacheConfig.ConnectionString;
            this.redisConnection = GetRedisConnection();
        }

        public ConnectionMultiplexer GetRedisConnection()
        {
            if (this.redisConnection != null && redisConnection.IsConnected)
            {
                return this.redisConnection;
            }

            lock (this.redisConnectionLock)
            {
                if (this.redisConnection != null)
                {
                    this.redisConnection.Dispose();
                }
                this.redisConnection = ConnectionMultiplexer.Connect(redisConnectionString);
            }

            return this.redisConnection;
        }

        public void Clear()
        {
            foreach (var endPoint in redisConnection.GetEndPoints())
            {
                var server = GetRedisConnection().GetServer(endPoint);
                foreach (var key in server.Keys())
                {
                    redisConnection.GetDatabase().KeyDelete(key);
                }
            }
        }

        public bool Contains(string key)
        {
            return redisConnection.GetDatabase().KeyExists(key);
        }

        public T Get<T>(string key)
        {
            var value = redisConnection.GetDatabase().StringGet(key);
            if (value.HasValue)
            {
                return Deserialize<T>(value);
            }
            else
            {
                return default(T);
            }
        }

        public void Remove(string key)
        {
            redisConnection.GetDatabase().KeyDelete(key);
        }

        public void Set(string key, object value, TimeSpan cacheTime)
        {
            if (value != null)
            {
                redisConnection.GetDatabase().StringSet(key, Serialize(value), cacheTime);
            }
        }

        #region private methods
        /// <summary>反序列化</summary>
        private T Deserialize<T>(byte[] value)
        {
            if (value == null)
            {
                return default(T);
            }
            var jsonString = Encoding.UTF8.GetString(value);

            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        /// <summary>序列化</summary>
        private byte[] Serialize(object item)
        {
            var jsonString = JsonConvert.SerializeObject(item);

            return Encoding.UTF8.GetBytes(jsonString);
        }
        #endregion
    }
}
