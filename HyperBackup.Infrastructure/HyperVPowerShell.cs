using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Management.Automation;
using System.Text;
using HyperBackup.Core;
using HyperBackup.Core.Interfaces;

namespace HyperBackup.Infrastructure
{
    public class HyperVPowerShell : IHyperVCommands
    {
        private static Collection<PSObject> Invoke(PowerShell instance)
        {
            var result = instance.Invoke();

            // Check the error stream for exceptions and throw the first one
            if (instance.Streams.Error.Count > 0)
                throw instance.Streams.Error[0].Exception;

            return result;
        }

        public List<string> GetVirtualMachines()
        {
            try
            {
                using (var instance = PowerShell.Create())
                {
                    // Build
                    instance.AddCommand("Get-VM");

                    // Execute
                    var output = Invoke(instance);

                    // Evaluate
                    var result = output.Select(x => x.Properties["VMName"].Value.ToString()).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw new PowerShellException(ex);
            }
        }

        public void ExportVirtualMachine(string name, string path)
        {
            try
            {
                using (var instance = PowerShell.Create())
                {
                    // Build
                    instance.AddCommand("Export-VM");
                    instance.AddParameter("Name", name);
                    instance.AddParameter("Path", path);

                    // Execute
                    Invoke(instance);
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("canceled"))
                    throw new PowerShellException("ExportCanceled", "The export operation was canceled by the user.", ex);

                throw new PowerShellException(ex);
            }
        }
    }
}
