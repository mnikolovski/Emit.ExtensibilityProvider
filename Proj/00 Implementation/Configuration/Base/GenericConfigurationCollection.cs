using System.Collections.Generic;
using System.Configuration;

namespace Emit.ExtensibilityProvider.Configuration.Base
{
    public class GenericConfigurationElementCollection<T> : ConfigurationElementCollection, IEnumerable<T> where T : ConfigurationElement, new()
    {
        readonly List<T> _elements = new List<T>();

        protected override ConfigurationElement CreateNewElement()
        {
            T _newElement = new T();
            _elements.Add(_newElement);
            return _newElement;
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return _elements.Find(e => e.Equals(element));
        }

        public new IEnumerator<T> GetEnumerator()
        {
            return _elements.GetEnumerator();
        }
    }
}
