using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyperBackup.Core;
using HyperBackup.Core.Application;
using HyperBackup.Core.Interfaces;
using HyperBackup.DependencyResolution;
using Ninject;

namespace HyperBackup
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var kernel = HyperBackupKernel.GetKernel();

                var service = new BackupService(kernel.Get<IHyperVCommands>(), kernel.Get<IBackupStorage>());

                service.ExportAllVirtualMachines();

                Console.WriteLine("Done!");
                Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An exception occured: " + ex);
            }
        }
    }

}
