using System;
using System.Diagnostics;
using System.Reflection;
using Saving_Accelerator_Tools2.IServices.Base;

namespace Saving_Accelerator_Tools2.ProgramSerivces
{
    public class ApplicationInfoService : IApplicationInfoService
    {
        public ApplicationInfoService()
        {
        }

        public Version GetVersion()
        {
            // Set the app version in Saving Accelerator Tools2 > Properties > Package > PackageVersion
            string assemblyLocation = Assembly.GetExecutingAssembly().Location;
            var version = FileVersionInfo.GetVersionInfo(assemblyLocation).FileVersion;
            return new Version(version);
        }
    }
}
