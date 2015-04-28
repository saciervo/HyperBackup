using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyperBackup.Core
{
    public class PowerShellException : Exception
    {
        public string FullyQualifiedExceptionId { get; private set; }

        public PowerShellException(string fullyQualifiedExceptionId, string message, Exception innerException)
            : base(message, innerException)
        {
            FullyQualifiedExceptionId = fullyQualifiedExceptionId;
        }

        public PowerShellException(Exception innnerException)
            : base("An unknown exception occured. See InnerException for details.", innnerException)
        {
            FullyQualifiedExceptionId = "Unknown";
        }
    }
}
