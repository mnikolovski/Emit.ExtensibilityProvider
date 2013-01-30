using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Emit.ExtensibilityProvider.Helpers
{
    public static class ReflectionHelper
    {
        /// <summary>
        /// Load all types both hoste and referenced
        /// </summary>
        /// <param name="ass"></param>
        /// <returns></returns>
        private static List<Type> LoadAllTypesFromAssembly(this Assembly ass)
        {
            var _types = ass.GetTypes().ToList();
            _types.AddRange(ass.GetReferencedAssemblies()
                               .Select(Assembly.Load)
                               .SelectMany(assembly => assembly.GetTypes()));

            return _types;
        }

        /// <summary>
        /// Load all types in the executing assembly
        /// </summary>
        /// <returns></returns>
        public static List<Type> GetRegisteredTypesInAssemblyFromLocation(string assemblyLocation, bool isRelativePath = false)
        {
            if (isRelativePath)
            {
                assemblyLocation = IoHelper.CombinePaths(AppDomain.CurrentDomain.BaseDirectory, assemblyLocation);
            }

            var _types = Assembly.LoadFrom(assemblyLocation).LoadAllTypesFromAssembly();
            return _types;
        }

        /// <summary>
        /// Get path of the current running assemlby
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentAssemblyRunPath()
        {
            var _filePath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var _pathWithExcludedFilename = IoHelper.GetPathWithoutFileName(_filePath);
            return _pathWithExcludedFilename;
        }
    }
}
