using HyperBackup.Core.Interfaces;

namespace HyperBackup.Core.Application
{
    public class BackupService
    {
        private readonly IHyperVCommands _hyperVCommands;
        private readonly IBackupStorage _backupStorage;

        public BackupService(IHyperVCommands hyperVCommands, IBackupStorage backupStorage)
        {
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
                var progressDirectoryPath = _backupStorage.CreateProgressDirectory();
                _hyperVCommands.ExportVirtualMachine(name, progressDirectoryPath);
                _backupStorage.FinalizeExport(name);
            }
            finally
            {
                _backupStorage.DeleteProgressDirectory();
            }
        }
    }
}
