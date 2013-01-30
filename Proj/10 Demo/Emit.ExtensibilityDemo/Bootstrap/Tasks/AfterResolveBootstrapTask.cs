using System;
using Emit.ExtensibilityProvider.Bootstrapping;

namespace Emit.ExtensibilityDemo.Bootstrap.Tasks
{
    internal class AfterResolveBootstrapTask : IBootstrapperTask
    {
        public ExecuteType ExecuteType { get; set; }
        public ExecuteMode ExecuteMode { get; set; }

        public void ExecuteTask()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var message = string.Format(@"{0}|INFO|After resolve - executes on each resolve", DateTime.Now);
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
