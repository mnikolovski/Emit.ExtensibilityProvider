using System;
using System.Collections.Generic;
using System.Text;

namespace Emit.ExtensibilityProvider.Extensibility
{
    internal static class ExtensibilityExtensions
    {
        public static string CreateTypeDescriptior(this IEnumerable<Type> types)
        {
            var _sb = new StringBuilder();
            foreach (var _type in types)
            {
                _sb.Append(_type.FullName + "#");
            }
            #if DEBUG
            return _sb.ToString();       
            #else
            return CreateDependencyDescriptor(_sb.ToString());
            #endif
        }

        /// <summary>
        /// Create descriptor 
        /// </summary>
        /// <param name="dependencyDescription"></param>
        /// <returns></returns>
        private static string CreateDependencyDescriptor(string dependencyDescription)
        {
            var data = System.Text.Encoding.ASCII.GetBytes(dependencyDescription);
            var descriptor = System.Convert.ToBase64String(data);
            return descriptor;
        }
    }
}
