using System.ComponentModel.Composition;
using Emit.DependencyExports.Definition;
using Emit.ExtensibilityProvider.Extensibility;

namespace Emit.ExtensibilityDemo.DependencyImports
{
    public class UserService
    {
        [Import]
        public IUserController UserController { get; set; }

        [ConstraintImport(typeof(IWindowsAuthController))]
        public IAuthController WindowsAuthController { get; set; }

        [ConstraintImport(typeof(IFormsAuthController))]
        public IAuthController FormsAuthController { get; set; }
    }
}
