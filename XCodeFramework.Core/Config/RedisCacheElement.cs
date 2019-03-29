using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCodeFramework.Core.Config
{
    public class RedisCacheElement : ConfigurationElement
    {
        private const string EnablePropertyName = "enabled";

        private const string ConnectionPropertyName = "connectionString";

        [ConfigurationProperty(EnablePropertyName,IsRequired = true)]
        public bool Enabled
        {
            get { return (bool)base[EnablePropertyName]; }
            set { base[EnablePropertyName] = value; }
        }

        [ConfigurationProperty(ConnectionPropertyName, IsRequired = true)]
        public string ConnectionString
        {
            get { return (string)base[ConnectionPropertyName]; }
            set { base[ConnectionPropertyName] = value; }
        }


    }
}
