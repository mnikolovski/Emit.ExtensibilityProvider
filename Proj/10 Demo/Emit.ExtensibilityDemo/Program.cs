using System;
using Emit.DependencyExports.Definition;
using Emit.ExtensibilityDemo.Bootstrap.Tasks;
using Emit.ExtensibilityDemo.DependencyImports;
using Emit.ExtensibilityProvider.Bootstrapping;
using Emit.ExtensibilityProvider.Concrete;

namespace Emit.ExtensibilityDemo
{
    class Program
    {
        static readonly SystemBootstrapper SystemBootstrapper = new SystemBootstrapper();

        static void Main()
        {
            ConfigureViaCode();
            BasicImportSample();
            ConstraintedImportSample();
            BasicManuallyImportedSample();
            ConstraintedManuallyImportedSample();
            Console.ReadKey();
        }
        
        /// <summary>
        /// Register bootstrap task via code
        /// </summary>
        private static void ConfigureViaCode()
        {
            var task = new ViaCodeBootstrapTask();
            task.ExecuteMode = ExecuteMode.AfterBootstrap;
            task.ExecuteType = ExecuteType.Always;
            SystemBootstrapper.AddBoostrappingTask(task);
        }

        /// <summary>
        /// Shows basic MEF import sample 
        /// </summary>
        private static void BasicImportSample()
        {
            Console.WriteLine("---Running-BasicImportSample---");
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
            Console.WriteLine("---Running-ConstraintedImportSample---");
            // create the instance
            var userService = new UserService();
            // fill the context's imports
            SystemBootstrapper.Execute(userService);
            // execute some method...
            userService.FormsAuthController.Login(@"New user", @"New user");
            userService.WindowsAuthController.Login(@"New user", @"Passw0rd");
        }

        /// <summary>
        /// Show basic MEF manually resolved instance sample
        /// </summary>
        private static void BasicManuallyImportedSample()
        {
            Console.WriteLine("---Running-BasicManuallyImportedSample---");
            // resolve an instance
            var userService = new UserService();
            userService.UserController = SystemBootstrapper.GetInstance<IUserController>(); 
            // execute some method...
            userService.UserController.AddUser(@"New user");             
        }

        /// <summary>
        /// Shows constrainted MEF manually resolved instance sample 
        /// </summary>
        private static void ConstraintedManuallyImportedSample()
        {
            Console.WriteLine("---Running-ConstraintedManuallyImportedSample---");
            // create the instance
            var userService = new UserService();
            userService.UserController = SystemBootstrapper.GetInstance<IUserController>();
            userService.WindowsAuthController = SystemBootstrapper.GetInstance<IAuthController>(new[] { typeof(IWindowsAuthController) });
            userService.FormsAuthController = SystemBootstrapper.GetInstance<IAuthController>(new[] { typeof(IFormsAuthController) }); 
            // execute some method...
            userService.FormsAuthController.Login(@"New user", @"New user");
            userService.WindowsAuthController.Login(@"New user", @"Passw0rd");
        }
    }
}
