using System;
using System.Configuration;
using System.IO;

namespace Emit.ExtensibilityProvider.Configuration.Base
{
    /// <summary>
    /// Represents generic configuration section which allow configuration changes
    /// </summary>
    public abstract class GenericConfigurationSection<T> : ConfigurationSection where T : GenericConfigurationSection<T>, new()
                                                                                
    {
        /// <summary>
        /// Represents the file watcher that will watch the main configiration
        /// </summary>
        private static FileSystemWatcher FileWatcher { get; set; }
        
        /// <summary>
        /// Represents the confgiruation section name of the custom configuration
        /// </summary>
        public abstract string ConfigurationName {get;}

        /// <summary>
        /// Represents the configuration instance
        /// </summary>
        private static T _mInstance;
        public static T Instance
        {
            get
            {
                if (_mInstance == null)
                {
                    _mInstance = new T();
                    _mInstance = ConfigurationManager.GetSection(_mInstance.ConfigurationName) as T;
                }

                return _mInstance;
            } 
            protected set
            {
                _mInstance = value;
            }
        }

        /// <summary>
        /// Static constructor
        /// </summary>
        protected GenericConfigurationSection()
        {
            if (FileWatcher == null)
            {
                StartWatcingForChanges();
            }
        }

        /// <summary>
        /// Activate config watcher for tracking configuration changes and refreshing them
        /// </summary>
        void StartWatcingForChanges()
        {
            var _configFilePath = Path.GetDirectoryName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);
            var _configFilename = Path.GetFileName(AppDomain.CurrentDomain.SetupInformation.ConfigurationFile);

            FileWatcher = new FileSystemWatcher(_configFilePath);
            FileWatcher.Filter = string.Format("{0}", _configFilename);
            FileWatcher.NotifyFilter = NotifyFilters.LastWrite;
            FileWatcher.Changed += ConfigFile_Updated;
            FileWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Callback executed after the config file is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ConfigFile_Updated(object sender, FileSystemEventArgs e)
        {
            // refresh the conf
            Refresh();
        }

        /// <summary>
        /// Refresh the configuration file
        /// </summary>
        void Refresh()
        {
            if (string.IsNullOrEmpty(ConfigurationName) || Instance == null) return;
            ConfigurationManager.RefreshSection(ConfigurationName);
            Instance = ConfigurationManager.GetSection(ConfigurationName) as T;
        }
    }
}
