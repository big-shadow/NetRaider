using System;
using System.Configuration;

namespace NetRaider.Configuration
{
    /// <summary>
    /// Used to statically access lazy loaded resources in configuration files.
    /// </summary>
    public static class Config
    {
        // Lazy loading dictionary for settings in the app.config.
        private static ConfigurationItem _item;

        // Deserialize the configuration if it hasn't been loaded.
        private static bool _loaded = false;

        /// <summary>
        /// A dictionary with key value pairs.
        /// </summary>
        public static ConfigurationItem Item
        {
            get
            {
                if (!_loaded)
                {
                    _item = new ConfigurationItem();
                    _loaded = true;
                }

                return _item;
            }
        }

        public sealed class ConfigurationItem
        {
            public string this[string property]
            {
                get
                {
                    string item = ConfigurationManager.AppSettings[property];

                    if (item == null || string.IsNullOrEmpty(item))
                    {
                        throw new Exception(string.Format("Configuration item {0} does not exist.", property));
                    }

                    return item;
                }
            }
        }
    }
}