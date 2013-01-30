using System.IO;

namespace Emit.ExtensibilityProvider.Helpers
{
    public static class IoHelper
    {
        /// <summary>
        /// Combine two paths into one
        /// </summary>
        /// <param name="firstPath"></param>
        /// <param name="secondPath"></param>
        /// <returns></returns>
        public static string CombinePaths(string firstPath, string secondPath)
        {
            var _rightSlash = firstPath.Contains("/");
 
            if (firstPath == null)
            {
                throw new InvalidDataException(@"First path must not be null");
            }
            if (secondPath == null)
            {
                throw new InvalidDataException(@"Second path must not be null");                
            }

            var _path = Path.Combine(firstPath, secondPath);
            if (_rightSlash)
            {
                _path = _path.Replace('\\', '/');
            }

            return _path;
        }

        /// <summary>
        /// Return a path without the filename
        /// </summary>
        /// <param name="fullPath"></param>
        /// <returns></returns>
        public static string GetPathWithoutFileName(string fullPath)
        {
            var _cleanPath = Path.GetDirectoryName(fullPath);
            return _cleanPath;
        }
    }
}
