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
            var names = _hyperVCommands.GetVirtualMachines();
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
            catch (ExceptionCollection collection)
            {
                if (collection.Exceptions != null)
                {
                    foreach (var ex in collection.Exceptions)
                    {
                        _log.Error("Export failed.", ex as Exception);
                    }
                }
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
