using Emit.ExtensibilityProvider.Configuration.Base;

namespace Emit.ExtensibilityProvider.Configuration
{
    public sealed class BootstrapperTaskConfigurationCollection : GenericConfigurationElementCollection<BootstrapperTaskConfigurationElement>
    {
        protected override object GetElementKey(System.Configuration.ConfigurationElement element)
        {
            return ((BootstrapperTaskConfigurationElement)element).Name;
        }
    }
}
