using System;
using System.ComponentModel.Composition;

namespace Emit.ExtensibilityProvider.Extensibility
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ConstraintExportAttribute : ExportAttribute
    {
        /// <param name="exportType">Types that we export</param>
        /// <param name="types">Additional type constraints</param>
        public ConstraintExportAttribute(Type exportType, params Type[] types)
            : base(types.CreateTypeDescriptior(), exportType)
        {
            
        }
    }
}
