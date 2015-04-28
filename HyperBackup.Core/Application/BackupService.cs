using System;
using System.ComponentModel.Design;
using HyperBackup.Core.Interfaces;

namespace HyperBackup.Core.Application
{
    public class BackupService
    {
        private readonly ILog _log;
        private readonly IHyperVCommands _hyperVCommands;
        private readonly IBackupStorage _backupStorage;

        public BackupService(ILog log, IHyperVCommands hyperVCommands, IBackupStorage backupStorage)
        {
            _log = log;
            _hyperVCommands = hyperVCommands;
            _backupStorage = backupStorage;
        }

        public void ExportAllVirtualMachines()
        {
            _log.Info("Get the names of the virtual machines from Hyper-V.");
            var names = _hyperVCommands.GetVirtualMachines();
            _log.Debug("Virtual machines found: " + string.Join(", ", names));

            foreach (var name in names)
            {
                ExportVirtualMachine(name);
            }
        }

        public void ExportVirtualMachine(string name)
        {
            try
            {
                _log.Info("Export Machine: " + name);

                var progressDirectoryPath = _backupStorage.CreateProgressDirectory();
                _hyperVCommands.ExportVirtualMachine(name, progressDirectoryPath);
                _backupStorage.FinalizeExport(name);

                _log.Info("Export successful.");
            }
            catch (PowerShellException  ex)
            {
                _log.Error(string.Concat("Export failed: ", ex.Message), ex.InnerException);
            }
            catch (Exception ex)
            {
                _log.Error("Export failed.", ex);
            }
            finally
            {
                _backupStorage.DeleteProgressDirectory();
            }
        }
    }
}
