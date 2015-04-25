using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperBackup.Core.Interfaces
{
    public interface IApplicationConfig
    {
        DirectoryInfo BackupPath { get; }
    }
}
