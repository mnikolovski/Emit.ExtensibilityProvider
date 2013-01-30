using System;
using System.IO;
using Emit.ExtensibilityProvider.Bootstrapping;

namespace Emit.ExtensibilityProvider.Concrete
{
    internal class BootstrapperTaskExecution : IBootstrapperTask
    {
        /// <summary>
        /// Bootstrapper task for execution
        /// </summary>
        private IBootstrapperTask Task { get; set; }

        /// <summary>
        /// Bootstrapper task type
        /// </summary>
        public Type Type
        {
            get
            {
                return Task.GetType();
            }
        }

        /// <summary>
        /// Mark if the task has been executed
        /// </summary>
        public bool IsExecuted { get; private set; }

        public BootstrapperTaskExecution(IBootstrapperTask task)
        {
            if (task == null)
            {
                throw new InvalidDataException("BootstrapperTask must have a value");
            }

            Task = task;
        }

        /// <summary>
        /// Bootstrap task execution mode
        /// </summary>
        public ExecuteType ExecuteType
        {
            get
            {
                return Task.ExecuteType;
            }
            set
            {
                throw new NotSupportedException("Can not change bootstrapper task one time execution flag in preloaded task");
            }
        }

        /// <summary>
        /// When the bootstrap task should execute
        /// </summary>
        public ExecuteMode ExecuteMode
        {
            get
            {
                return Task.ExecuteMode;
            }
            set
            {
                throw new NotSupportedException("Can not change bootstrapper task execution mode in preloaded task");   
            }
        }

        /// <summary>
        /// Execute bootstrap task
        /// </summary>
        public void ExecuteTask()
        {
            if (IsExecuted && ExecuteType == ExecuteType.Initalize) return;

            Task.ExecuteTask();
            IsExecuted = true;
        }
    }
}
