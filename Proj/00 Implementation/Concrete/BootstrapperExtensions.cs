using System;
using System.Collections.Generic;
using Emit.ExtensibilityProvider.Bootstrapping;

namespace Emit.ExtensibilityProvider.Concrete
{
    internal static class BootstrapperExtensions
    {
        public static bool SatissfyExecutionCondition(this IBootstrapperTask task, ExecuteMode executeMode, Dictionary<Type, BootstrapperTaskExecution> bootstrapperTasksExecutionPool)
        {
            // if execution mode does not match return false
            if (task.ExecuteMode != executeMode) return false;

            // get the task type
            var _taskType = task.GetType();

            // if not in the pool return true
            if (!bootstrapperTasksExecutionPool.ContainsKey(_taskType)) return true;

            // get the task from the pool
            var _executedTask = bootstrapperTasksExecutionPool[_taskType];

            // if in the pool and can not be execute again return false
            if (_executedTask.IsExecuted && _executedTask.ExecuteType == ExecuteType.Initalize) return false;

            // if in the pool and can execute mutiple times
            if (_executedTask.IsExecuted && _executedTask.ExecuteType == ExecuteType.Always) return true;

            return false;
        }
    }
}
