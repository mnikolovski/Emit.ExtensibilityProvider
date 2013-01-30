using System.Configuration;

namespace Emit.ExtensibilityProvider.Configuration.Base.LocationAware
{
    public class FileSystemLocationConfigurationElement : ConfigurationElement
    {
        /// <summary>
        /// Relative or absolute path of the source
        /// </summary>
        [ConfigurationProperty("sourcePath", IsKey = true, IsRequired = false, DefaultValue = "")]
        public string SourcePath
        {
            get
            {
                return (string)base["sourcePath"];
            }
            set
            {
                base["sourcePath"] = value;
            }
        }

        /// <summary>
        /// Marks if the content should be search/loaded it the running assembly directory will be used
        /// </summary>
        [ConfigurationProperty("useBaseDirectory", IsKey = true, IsRequired = false, DefaultValue = true)]
        public bool UseBaseDirectory
        {
            get
            {
                return (bool)base["useBaseDirectory"];
            }
            set
            {
                base["useBaseDirectory"] = value;
            }            
        }
    }
}
