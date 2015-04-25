using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Management.Automation;
using HyperBackup.Core.Interfaces;

namespace HyperBackup.Infrastructure
{
    public class HyperVPowerShell : IHyperVCommands
    {
        private static Collection<PSObject> Invoke(PowerShell instance)
        {
            try
            {
                var result = instance.Invoke();

                // check the other output streams (for example, the error stream)
                if (instance.Streams.Error.Count > 0)
                {
                    // error records were written to the error stream.
                    // do something with the items found.
                    foreach (var record in instance.Streams.Error)
                    {
                        Console.WriteLine(record.Exception);
                        Console.WriteLine(record.Exception.StackTrace);
                    }
                }

                return result;
            }
            catch (Exception)
            {
                throw;
            }
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
    }
}
