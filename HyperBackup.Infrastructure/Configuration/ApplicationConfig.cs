using System.Configuration;
using System.IO;
using HyperBackup.Core.Interfaces;

namespace HyperBackup.Infrastructure.Configuration
{
    public class ApplicationConfig : IApplicationConfig
    {
        public DirectoryInfo BackupPath { get { return new DirectoryInfo(ConfigurationManager.AppSettings["BackupPath"]); } }
    }
}
