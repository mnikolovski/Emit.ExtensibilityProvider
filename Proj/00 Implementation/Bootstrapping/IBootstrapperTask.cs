namespace Emit.ExtensibilityProvider.Bootstrapping
{
    /// <summary>
    /// Bootstrapper task definition
    /// </summary>
    public interface IBootstrapperTask
    {
        /// <summary>
        /// Bootstrap task execution mode
        /// </summary>
        ExecuteType ExecuteType { get; set; }

        /// <summary>
        /// When the bootstrap task should execute
        /// </summary>
        ExecuteMode ExecuteMode { get; set; }

        /// <summary>
        /// Execute bootstrap task
        /// </summary>
        void ExecuteTask();
    }

    /// <summary>
    /// Bootstrap task execution mode
    /// </summary>
    public enum ExecuteType
    {
        Initalize,
        Always
    }

    /// <summary>
    /// When the bootstrap task should execute
    /// </summary>
    public enum ExecuteMode
    {
        BeforeBootstrap,
        AfterBootstrap
    }
}
