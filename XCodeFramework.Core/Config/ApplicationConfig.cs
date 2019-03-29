using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Config
{
    public class ApplicationConfig : ConfigurationSection
    {
        private const string RedisConfigCachePropertyName = "redisCache";

        [ConfigurationProperty(RedisConfigCachePropertyName, IsRequired = false)]
        public RedisCacheElement RedisCacheConfig
        {
            get { return (RedisCacheElement)base[RedisConfigCachePropertyName]; }
            set { base[RedisConfigCachePropertyName] = value; }
        }
    }
}
