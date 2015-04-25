using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyperBackup.Core.Interfaces;
using HyperBackup.Infrastructure;
using HyperBackup.Infrastructure.Configuration;
using HyperBackup.Infrastructure.Logging;
using Ninject;

namespace HyperBackup.DependencyResolution
{
    public static class HyperBackupKernel
    {
        public static IKernel GetKernel(bool logToConsole = false)
        {
            var kernel = new StandardKernel();

            // Bindings
            kernel.Bind<IApplicationConfig>().To<ApplicationConfig>().InSingletonScope();
            kernel.Bind<ILog>().To<NLogWrapper>().WithConstructorArgument("logToConsole", logToConsole);
            kernel.Bind<IHyperVCommands>().To<HyperVPowerShell>().InSingletonScope();
            kernel.Bind<IBackupStorage>().To<FileSystemBackupStorage>();

            return kernel;
        }
    }
}
