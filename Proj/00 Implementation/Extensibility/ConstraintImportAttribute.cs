using System;
using System.ComponentModel.Composition;

namespace Emit.ExtensibilityProvider.Extensibility
{
    [AttributeUsageAttribute(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class ConstraintImportAttribute : ImportAttribute
    {
        /// <param name="types">Additional type constraints</param>
        public ConstraintImportAttribute(params Type[] types)
            : base(types.CreateTypeDescriptior())
        {
            
        }
    }
}
