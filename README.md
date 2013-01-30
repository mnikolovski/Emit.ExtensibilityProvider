# What is Emit.ExtensibilityProvider?

Emit.ExtensibilityProvider is a component wrapper arround [Microsoft MEF(Managed Extensibility Framework)](http://mef.codeplex.com/).

Functionalities:
- Configure via .config
- Configure via code
- Add Bootstrapper Task via .config/code
- Bootstrapping Tasks before and after instance import
- Offers ConstraintExportAttribute and ConstraintImportAttribute for controlling exports/imports via multiple types

## Bootstrapper Task
```csharp
    internal class BeforeResolveBootstrapTask : IBootstrapperTask
    {
        public ExecuteType ExecuteType { get; set; }
        public ExecuteMode ExecuteMode { get; set; }

        public void ExecuteTask()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            var message = string.Format(@"{0}|INFO|Before resolve - one time execution", DateTime.Now);
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
```
## Configure via .config
```xml
    <configuration>
     <configSections>
       <section name="BootstrapperConfiguration" type="Emit.ExtensibilityProvider.Configuration.BootstrapperConfiguration, Emit.ExtensibilityProvider"/>
     </configSections>
     <BootstrapperConfiguration>
       <source useBaseDirectory = "true"/>
       <bootstrapperTasks>
         <bootstrapperTask name="BootStartedTask"
                           type="Emit.ExtensibilityDemo.Bootstrap.Tasks.BeforeResolveBootstrapTask, Emit.ExtensibilityDemo"
                           executeType="Initalize"
                           executeMode="BeforeBootstrap"/>
       </bootstrapperTasks>
     </BootstrapperConfiguration>
    </configuration>
```
## Configure via code
```csharp
    var task = new ViaCodeBootstrapTask();
    task.ExecuteMode = ExecuteMode.AfterBootstrap;
    task.ExecuteType = ExecuteType.Always;
    SystemBootstrapper.AddBoostrappingTask(task);
```
## Show me the code
Some class exports:
```csharp
    [Export(typeof(IUserController))]
    public class UserControllers : IUserController
    {
        public void AddUser(string username){}
    }

    [ConstraintExport(typeof(IAuthController), typeof(IFormsAuthController))]
    public class FormsAuthController : IFormsAuthController
    {
        public void AddUser(string username){}
        public bool Login(string username, string password) {}
    }
  
    [ConstraintExport(typeof(IAuthController), typeof(IWindowsAuthController))]
    public class WindowsAuthController : IWindowsAuthController
    {
        public void AddUser(string username){}
        public bool Login(string username, string password){}
    }	
```
Some class imports:
```csharp
    public class UserService
    {
        [Import]
        public IUserController UserController { get; set; }

        [ConstraintImport(typeof(IWindowsAuthController))]
        public IAuthController WindowsAuthController { get; set; }

        [ConstraintImport(typeof(IFormsAuthController))]
        public IAuthController FormsAuthController { get; set; }
    }
```	
Context fill:
```csharp
    // fill context with imports
    bootstrapper.Execute(userService);
    // return instance that implements IUserController
    bootstrapper.GetInstance<IUserController>(); 
    // return instance that implements IAuthController and is constrainted by IWindowsAuthController  
    bootstrapper.GetInstance<IAuthController>(new[] { typeof(IWindowsAuthController) })
    // return instance that implements IAuthController and is constrainted by IFormsAuthController
    bootstrapper.GetInstance<IAuthController>(new[] { typeof(IFormsAuthController) }); 
```
