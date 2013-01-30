using System;
using System.ComponentModel.Composition;
using Emit.DependencyExports.Definition;

namespace Emit.DependencyExports.Concrete
{
    [Export(typeof(IUserController))]
    public class UserControllers : IUserController
    {
        public void AddUser(string username)
        {
            var message = string.Format(@"{0}|INFO|Adding new user:{1}", DateTime.Now, username);
            Console.WriteLine(message);
        }
    }
}
