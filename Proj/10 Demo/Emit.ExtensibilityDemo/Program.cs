using System;
using Emit.ExtensibilityDemo.DependencyImports;
using Emit.ExtensibilityProvider.Concrete;

namespace Emit.ExtensibilityDemo
{
    class Program
    {
        static readonly SystemBootstrapper SystemBootstrapper = new SystemBootstrapper();

        static void Main()
        {
            BasicImportSample();
            ConstraintedImportSample();
            Console.ReadKey();
        }

        /// <summary>
        /// Shows basic MEF import sample 
        /// </summary>
        private static void BasicImportSample()
        {
            // create the instance
            var userService = new UserService();
            // fill the context's imports
            SystemBootstrapper.Execute(userService);
            // execute some method...
            userService.UserController.AddUser(@"New user");            
        }

        /// <summary>
        /// Shows constrainted MEF import sample 
        /// </summary>
        private static void ConstraintedImportSample()
        {
            // create the instance
            var userService = new UserService();
            // fill the context's imports
            SystemBootstrapper.Execute(userService);
            // execute some method...
            userService.FormsAuthController.Login(@"New user", @"New user");
            userService.WindowsAuthController.Login(@"New user", @"Passw0rd");
        }
    }
}
