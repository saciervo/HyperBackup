using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Management.Automation;
using HyperBackup.Core.Interfaces;

namespace HyperBackup.Infrastructure
{
    public class HyperVPowerShell : IHyperVCommands
    {
        private static Collection<PSObject> Invoke(PowerShell instance)
        {
            var result = instance.Invoke();

            // check the other output streams (for example, the error stream)
            if (instance.Streams.Error.Count > 0)
            {
                // Error records were written to the error stream.
                // For now, just create a collection of the exceptions.
                var exceptions = new ArrayList();
                foreach (var record in instance.Streams.Error)
                {
                    exceptions.Add(record.Exception);
                }

                throw new ExceptionCollection(exceptions);
            }

            return result;
        }

        public List<string> GetVirtualMachines()
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
                throw new ApplicationException("ExportVirtualMachine", ex);
            }

        }
    }
}
