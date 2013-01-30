using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Linq;
using Emit.ExtensibilityProvider.Bootstrapping;
using Emit.ExtensibilityProvider.Configuration;
using Emit.ExtensibilityProvider.Extensibility;
using Emit.ExtensibilityProvider.Helpers;

namespace Emit.ExtensibilityProvider.Concrete
{
    /// <summary>
    /// System bootstrapper for context
    /// </summary>
    public class SystemBootstrapper : IBootstrapper 
    {
        private static readonly object Lock = new object();
        private static readonly List<Type> RegisteredTypes;
        private static readonly CompositionContainer CompositionContainer;
        private static List<IBootstrapperTask> BootstrapperTasks { get; set; }
        private static Dictionary<Type, BootstrapperTaskExecution> BootstrapperTasksExecutionPool { get; set; }
        public static bool IsBootLoaded { get; private set; }

        static SystemBootstrapper()
        {            
            // load the tasks
            BootstrapperTasks = BootstrapperConfiguration.Instance.RegisteredBootstrapperTasks;

            // create the execution pool
            BootstrapperTasksExecutionPool = new Dictionary<Type, BootstrapperTaskExecution>();

            ComposablePartCatalog _catalog;

            // get the types
            if (BootstrapperConfiguration.Instance.Source.UseBaseDirectory)
            {
                var _includePath = IoHelper.CombinePaths(ReflectionHelper.GetCurrentAssemblyRunPath(),
                                                         BootstrapperConfiguration.Instance.Source.SourcePath ??
                                                         string.Empty);

                _catalog = new DirectoryCatalog(_includePath);
            }
            else
            {
                RegisteredTypes = ReflectionHelper.GetRegisteredTypesInAssemblyFromLocation(BootstrapperConfiguration.Instance.Source.SourcePath);
                _catalog = new TypeCatalog(RegisteredTypes);
            }

            // create the catalog container
            CompositionContainer = new CompositionContainer(_catalog);
        }

        private static void ExecuteBootstrapperTasks(ExecuteMode executeMode)
        {
            lock (Lock)
            {
                // get the task for execution that satissfy the execution conition
                var _tasksToExecute = BootstrapperTasks.Where(x => x.SatissfyExecutionCondition(executeMode, BootstrapperTasksExecutionPool)).ToList();

                // execute all of the tasks
                foreach (var _bootstrapperTask in _tasksToExecute)
                {
                    var _bootstrapperTaskExecution = new BootstrapperTaskExecution(_bootstrapperTask);
                    BootstrapperTasksExecutionPool[_bootstrapperTask.GetType()] = _bootstrapperTaskExecution;
                    _bootstrapperTaskExecution.ExecuteTask();
                }
            }
        }

        /// <summary>
        /// Execute bootstrapping activities
        /// </summary>
        public void Execute<T>(T context) where T : class
        {
            // execute bootstrapping tasks before wireing up
            ExecuteBootstrapperTasks(ExecuteMode.BeforeBootstrap);

            // wireup the imports/exports
            CompositionContainer.ComposeParts(context);

            // execute bootstrapping tasks after wireing up
            ExecuteBootstrapperTasks(ExecuteMode.AfterBootstrap);

            if (!IsBootLoaded)
            {
                IsBootLoaded = true;
            }
        }

        /// <summary>
        /// Add bootstrapping task to the bootstrapper
        /// </summary>
        /// <param name="bootstrapperTask"></param>
        public void AddBoostrappingTask(IBootstrapperTask bootstrapperTask)
        {
            lock (Lock)
            {
                BootstrapperTasks.Add(bootstrapperTask);
            }
        }

        /// <summary>
        /// Get instance of bootstrappers registered instances
        /// </summary>
        /// <typeparam name="TX"></typeparam>
        /// <returns></returns>
        public TX GetInstance<TX>(Type[] constraints = null)
        {
            var _type = default(TX);
            if (CompositionContainer == null) return _type;

            if (constraints == null ||
                constraints.Length == 0)
            {
                _type = CompositionContainer.GetExportedValue<TX>();
            }
            else
            {
                _type = CompositionContainer.GetExportedValue<TX>(constraints.CreateTypeDescriptior());
            }

            return _type;
        }
    }
}
