using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyperBackup.Core.Interfaces;
using HyperBackup.Infrastructure;
using Ninject;

namespace HyperBackup.DependencyResolution
{
    public static class HyperBackupKernel
    {
        public static IKernel GetKernel()
        {
            var kernel = new StandardKernel();

            // Bindings
            kernel.Bind<IApplicationConfig>().To<ApplicationConfig>();
            kernel.Bind<IHyperVCommands>().To<HyperVPowerShell>();
            kernel.Bind<IBackupStorage>().To<FileSystemBackupStorage>();

            return kernel;
        }
    }
}
