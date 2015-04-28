using System;
using System.Configuration;
using System.IO;
using HyperBackup.Core.Interfaces;

namespace HyperBackup.Infrastructure.Configuration
{
    public class ApplicationConfig : IApplicationConfig
    {
        public string BackupPath { get { return ConfigurationManager.AppSettings["BackupPath"]; } }
        public bool LogToConsole { get { return Convert.ToBoolean(ConfigurationManager.AppSettings["LogToConsole"]); } }
        public string FileLogLevel { get { return ConfigurationManager.AppSettings["FileLogLevel"]; } }
        public string ConsoleLogLevel { get { return ConfigurationManager.AppSettings["ConsoleLogLevel"]; } }
    }
}
