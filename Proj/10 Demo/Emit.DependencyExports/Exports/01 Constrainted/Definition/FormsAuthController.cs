using System;
using Emit.DependencyExports.Definition;
using Emit.ExtensibilityProvider.Extensibility;

namespace Emit.DependencyExports.Concrete
{
    [ConstraintExport(typeof(IAuthController), typeof(IFormsAuthController))]
    public class FormsAuthController : IFormsAuthController
    {
        public void AddUser(string username)
        {
            var message = string.Format(@"{0}|INFO|Adding new user:{1}", DateTime.Now, username);
            Console.WriteLine(message);
        }

        public bool Login(string username, string password)
        {
            var message = string.Format(@"{0}|INFO|User:{1} logins with forms auth", DateTime.Now, username);
            Console.WriteLine(message);

            if (username == password)
            {
                message = string.Format(@"{0}|INFO|User:{1} provided invalid forms credentials", DateTime.Now, username);
                Console.WriteLine(message);
                return false;
            }

            return true;
        }
    }
}
