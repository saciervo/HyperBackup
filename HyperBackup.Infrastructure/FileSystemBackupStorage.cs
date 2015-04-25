using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyperBackup.Core.Interfaces;

namespace HyperBackup.Infrastructure
{
    public class FileSystemBackupStorage : IBackupStorage
    {
        private readonly IApplicationConfig _applicationConfig;

        public DirectoryInfo BackupDirectory
        {
            get
            {
                var backupDirectory = _applicationConfig.BackupPath;
                if (!backupDirectory.Exists)
                    backupDirectory.Create();

                return backupDirectory;
            }
        }

        public DirectoryInfo ProgressDirectory
        {
            get
            {
                return new DirectoryInfo(Path.Combine(BackupDirectory.FullName, ".progress"));
            }
        }

        public FileSystemBackupStorage(IApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig;
        }

        public string CreateProgressDirectory()
        {
            if (ProgressDirectory.Exists)
                throw new ApplicationException("Backup already in progress");

            ProgressDirectory.Create();
            ProgressDirectory.Attributes = FileAttributes.Directory | FileAttributes.Hidden;

            return ProgressDirectory.FullName;
        }

        public void DeleteProgressDirectory()
        {
            ProgressDirectory.Delete(true);
        }

        public void FinalizeExport(string name)
        {
            var currentMachinePath = Path.Combine(BackupDirectory.FullName, name);
            var exportedMachinePath = Path.Combine(ProgressDirectory.FullName, name);
            var tempMachinePath = string.Concat(currentMachinePath, ".temp");

            Directory.Move(exportedMachinePath, tempMachinePath);
            Directory.Delete(currentMachinePath, true);
            Directory.Move(tempMachinePath, currentMachinePath);
        }
    }
}
