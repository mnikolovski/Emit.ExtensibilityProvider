using System;

namespace Emit.ExtensibilityProvider.Bootstrapping
{
    /// <summary>
    /// Bootstrapper definition
    /// </summary>
    public interface IBootstrapper
    {
        /// <summary>
        /// Run bootstrapping on a context
        /// </summary>
        /// <param name="executionContext">Where the bootstrapping will occurs</param>
        void Execute<T>(T executionContext) where T : class;

        /// <summary>
        /// Add bootstrapping task to the bootstrapper
        /// </summary>
        /// <param name="bootstrapperTask"></param>
        void AddBoostrappingTask(IBootstrapperTask bootstrapperTask);

        /// <summary>
        /// Get instance of bootstrappers registered instances
        /// </summary>
        /// <typeparam name="TX"></typeparam>
        /// <returns></returns>
        TX GetInstance<TX>(Type[] constraints = null);
    }
}
