using System.Configuration;
using Emit.ExtensibilityProvider.Bootstrapping;

namespace Emit.ExtensibilityProvider.Configuration
{
    public sealed class BootstrapperTaskConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Unique task name
        /// </summary>
        [ConfigurationProperty("name", IsKey = true, IsRequired = true)]
        public string Name
        {
            get
            {
                return (string)base["name"];
            }
            set
            {
                base["name"] = value;
            }
        }

        /// <summary>
        /// Type of the task
        /// </summary>
        [ConfigurationProperty("type", IsKey = false, IsRequired = true)]
        public string Type
        {
            get
            {
                return (string)base["type"];
            }
            set
            {
                base["type"] = value;
            }
        }

        /// <summary>
        /// Marks if a task can be executed multiple times
        /// </summary>
        [ConfigurationProperty("executeType", IsKey = false, IsRequired = true, DefaultValue = ExecuteType.Initalize)]
        public ExecuteType ExecuteType
        {
            get
            {
                return (ExecuteType)base["executeType"];
            }
            set
            {
                base["executeType"] = value;
            }
        }

        /// <summary>
        /// Marks if a task can be executed multiple times
        /// </summary>
        [ConfigurationProperty("executeMode", IsKey = false, IsRequired = true, DefaultValue = ExecuteMode.BeforeBootstrap)]
        public ExecuteMode ExecutionMode
        {
            get
            {
                return (ExecuteMode)base["executeMode"];
            }
            set
            {
                base["executeMode"] = value;
            }
        }
    }
}
