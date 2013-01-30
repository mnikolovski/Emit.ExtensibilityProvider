using System;
using Emit.ExtensibilityProvider.Bootstrapping;

namespace Emit.ExtensibilityDemo.Bootstrap.Tasks
{
    internal class ViaCodeBootstrapTask : IBootstrapperTask
    {
        public ExecuteType ExecuteType { get; set; }
        public ExecuteMode ExecuteMode { get; set; }

        public void ExecuteTask()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var message = string.Format(@"{0}|INFO|Configured Via Code - BootstrapTask", DateTime.Now);
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
