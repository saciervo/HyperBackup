using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyperBackup.Core.Application;
using HyperBackup.Core.Interfaces;
using HyperBackup.DependencyResolution;
using Ninject;

namespace HyperBackup
{
    class Program
    {
        private static bool _hasConsole;

        static void Main(string[] args)
        {
            try
            {
                LoadArguments(args);

                var kernel = HyperBackupKernel.GetKernel(logToConsole:_hasConsole);
                var log = kernel.Get<ILog>();

                try
                {
                    log.Info("Starting Backups.");

                    var backupService = new BackupService(log, kernel.Get<IHyperVCommands>(), kernel.Get<IBackupStorage>());
                    backupService.ExportAllVirtualMachines();

                    log.Info("All Backups finished.");
                }
                catch (Exception ex)
                {
                    log.Fatal("An unexpected Exception occured: " + ex);
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            finally
            {
                if (_hasConsole)
                {
                    Console.WriteLine("Press any key to exit program...");
                    Console.ReadLine();
                }
            }
        }

        private static void LoadArguments(string[] args)
        {
             _hasConsole = !args.Contains("-noconsole");
        }
    }

}
