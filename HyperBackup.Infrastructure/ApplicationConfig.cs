using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HyperBackup.Core.Interfaces;

namespace HyperBackup.Infrastructure
{
    public class ApplicationConfig : IApplicationConfig
    {
        public DirectoryInfo BackupPath { get { return new DirectoryInfo(ConfigurationManager.AppSettings["BackupPath"]); } }
    }
}
