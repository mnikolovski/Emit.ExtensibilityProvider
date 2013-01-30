using System;
using System.Collections.Generic;
using System.Configuration;
using Emit.ExtensibilityProvider.Bootstrapping;
using Emit.ExtensibilityProvider.Configuration.Base;

namespace Emit.ExtensibilityProvider.Configuration
{
    public class BootstrapperConfiguration : GenericConfigurationSection<BootstrapperConfiguration>
    {
        /// <summary>
        /// Represents the confgiruation section name of the custom configuration
        /// </summary>
        public override string ConfigurationName
        {
            get { return "BootstrapperConfiguration"; }
        }

        /// <summary>
        /// Configuration of the bootstrapper
        /// </summary>
        [ConfigurationProperty("source", IsRequired = true)]
        public BootstrapperConfigurationElement Source
        {
            get
            {
                return (BootstrapperConfigurationElement)this["source"];   
            }
        }

        /// <summary>
        /// Repository collection element
        /// </summary>
        [ConfigurationProperty("bootstrapperTasks", IsRequired = false)]
        [ConfigurationCollection(typeof(BootstrapperTaskConfigurationCollection), AddItemName = "bootstrapperTask", ClearItemsName = "clear", RemoveItemName = "remove")]
        public BootstrapperTaskConfigurationCollection RegisteredBootstrapperTasksCollection
        {
            get { return (BootstrapperTaskConfigurationCollection)this["bootstrapperTasks"]; }
        }

        /// <summary>
        /// Return the registered daemons
        /// </summary>
        public List<IBootstrapperTask> RegisteredBootstrapperTasks
        { 
            get
            {
                if(RegisteredBootstrapperTasksCollection == null) return new List<IBootstrapperTask>();

                var _bootStrapperTasks = new List<IBootstrapperTask>();

                foreach (var _bootStrapperTaskElement in RegisteredBootstrapperTasksCollection)
                {
                    var _bootstrapperType = Type.GetType(_bootStrapperTaskElement.Type, true, false);
                    var _bootstrapperTask = (IBootstrapperTask)Activator.CreateInstance(_bootstrapperType);
                    _bootstrapperTask.ExecuteType = _bootStrapperTaskElement.ExecuteType;
                    _bootstrapperTask.ExecuteMode = _bootStrapperTaskElement.ExecutionMode;
                    _bootStrapperTasks.Add(_bootstrapperTask);
                }

                return _bootStrapperTasks;
            }    
        }
    }
}
